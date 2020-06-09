using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiriusStyleRdStore.Entities.Enums;
using SiriusStyleRdStore.Entities.Models;
using SiriusStyleRdStore.Utility.Extensions;

namespace SiriusStyleRdStore.Repositories.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetByCode(string productCode);
        Task<Product> Create(Product product);
        Task<IEnumerable<Product>> BatchCreate(List<Product> products);
        Task<Product> Update(Product product, bool updateImage);
        Task<IEnumerable<Product>> BatchUpdate(List<Product> products);
        Task<bool> CheckIfProductCodeExists(string productCode);
        Task<Product> UpdateStatus(Product product);
        Task<Product> AssignToOrder(Product product);
        Task<IEnumerable<Product>> AssignToOrder(List<Product> products);
        Task<IEnumerable<Product>> GetByOrderNumber(string orderNumber);
        Task<IEnumerable<Product>> GetAllForOrderDetails(string orderNumber);
        Task<IEnumerable<Product>> SetAsAvailable(string orderNumber);
    }

    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(SiriusStyleRdStoreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await Context.Product
                .Include(w => w.Category)
                .Include(w => w.Size)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Product> GetByCode(string productCode)
        {
            return await Context.Product
                .Include(w => w.Category)
                .Include(w => w.Size)
                .SingleAsync()
                .ConfigureAwait(false);
        }

        public async Task<Product> Create(Product product)
        {
            await Context.Product.AddAsync(product);
            await Save();

            return product;
        }

        public async Task<IEnumerable<Product>> BatchCreate(List<Product> products)
        {
            await Context.Product.AddRangeAsync(products);
            await Save();

            return products;
        }

        public async Task<Product> Update(Product product, bool updateImage)
        {
            Context.Attach(product);
            var properties = new List<string>
            {
                nameof(product.CategoryId),
                nameof(product.Description),
                nameof(product.SizeId),
                nameof(product.Price),
                nameof(product.Comments)
            };

            if (updateImage) properties.Add(nameof(product.Image));

            AddPropertiesToModify(product, properties);

            await Save();

            return product;
        }

        public async Task<IEnumerable<Product>> BatchUpdate(List<Product> products)
        {
            foreach (var product in products)
            {
                Context.Attach(product);
                AddPropertiesToModify(product, new List<string>
                {
                    nameof(product.CategoryId),
                    nameof(product.Description),
                    nameof(product.SizeId),
                    nameof(product.Price),
                    nameof(product.Image),
                    nameof(product.Comments)
                });
            }

            await Save();

            return products;
        }

        public async Task<bool> CheckIfProductCodeExists(string productCode)
        {
            var product = await Context.Product
                .SingleOrDefaultAsync(w => w.ProductCode == productCode)
                .ConfigureAwait(false);

            return product.HasValue();
        }

        public async Task<Product> UpdateStatus(Product product)
        {
            Context.Attach(product);
            AddPropertiesToModify(product, new List<string>
            {
                nameof(product.Status)
            });

            await Save();

            return product;
        }

        public async Task<Product> AssignToOrder(Product product)
        {
            Context.Attach(product);
            AddPropertiesToModify(product, new List<string>
            {
                nameof(product.OrderNumber),
                nameof(product.Status),
            });

            await Save();

            return product;
        }

        public async Task<IEnumerable<Product>> AssignToOrder(List<Product> products)
        {
            foreach (var product in products)
            {
                Context.Attach(product);
                AddPropertiesToModify(product, new List<string>
                {
                    nameof(product.OrderNumber),
                    nameof(product.Status),
                });
            }

            await Save();

            return products;
        }

        public async Task<IEnumerable<Product>> GetByOrderNumber(string orderNumber)
        {
            return await Context.Product
                .Include(w => w.Category)
                .Include(w => w.Size)
                .Where(w => w.OrderNumber == orderNumber)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Product>> GetAllForOrderDetails(string orderNumber)
        {
            return await Context.Product
                .Include(w => w.Category)
                .Include(w => w.Size)
                .Where(w =>
                    w.OrderNumber == null
                    || w.OrderNumber == orderNumber
                )
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Product>> SetAsAvailable(string orderNumber)
        {
            var products = (await GetByOrderNumber(orderNumber).ConfigureAwait(false)).ToList();

            foreach (var product in products)
            {
                product.Status = ProductStatus.Available;
                product.OrderNumber = null;

                Context.Attach(product);
                AddPropertiesToModify(product, new List<string>
                {
                    nameof(product.OrderNumber),
                    nameof(product.Status),
                });
            }

            await Save();

            return products;
        }
    }
}