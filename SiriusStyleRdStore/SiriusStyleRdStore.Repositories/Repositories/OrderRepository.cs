using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiriusStyleRdStore.Entities.Models;
using SiriusStyleRdStore.Utility.Extensions;

namespace SiriusStyleRdStore.Repositories.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAll();
        Task<Order> GetByNumber(string orderNumber);
        Task<Order> Create(Order order);
        Task<Order> Update(Order order);
        Task<bool> CheckIfOrderNumberExists(string orderNumber);
    }

    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(SiriusStyleRdStoreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await Context.Order
                .Include(w => w.Customer)
                .Include(w => w.Products)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Order> GetByNumber(string orderNumber)
        {
            return await Context.Order
                .Include(w => w.Customer)
                .Include(w => w.Products)
                .SingleAsync(w => w.OrderNumber == orderNumber)
                .ConfigureAwait(false);
        }

        public async Task<Order> Create(Order order)
        {
            await Context.Order.AddAsync(order);
            await Save();

            return order;
        }

        public async Task<Order> Update(Order order)
        {
            Context.Attach(order);
            AddPropertiesToModify(order, new List<string>
            {
                nameof(order.Status),
                nameof(order.CustomerId),
                nameof(order.ShippedOn),
                nameof(order.PaidOn),
                nameof(order.CanceledOn),
                nameof(order.ShippingCost),
                nameof(order.Discount),
                nameof(order.SubTotal),
                nameof(order.Total)
            });

            await Save();

            return order;
        }

        public async Task<bool> CheckIfOrderNumberExists(string orderNumber)
        {
            var order = await Context.Order
                .SingleOrDefaultAsync(w => w.OrderNumber == orderNumber)
                .ConfigureAwait(false);

            return order.HasValue();
        }
    }
}