using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiriusStyleRd.Entities.Models;

namespace SiriusStyleRd.Repository.Repositories
{
    public interface IBaleRepository
    {
        Task<IEnumerable<Bale>> GetAll();
        Task<IEnumerable<Bale>> GetAllWithProducts();
        Task<Bale> GetById(int baleId);
        Task<Bale> Create(Bale bale);
        Task<IEnumerable<Bale>> BatchCreate(List<Bale> bales);
        Task<Bale> Update(Bale bale);
        Task<IEnumerable<Bale>> BatchUpdate(List<Bale> bales);
        Task<Bale> Delete(Bale bale);
        Task<IEnumerable<Bale>> BatchDelete(List<Bale> bales);
        Task<IEnumerable<Bale>> GetAllForDropDownList(int baleId);
        Task<IEnumerable<Bale>> GetAllNotCompleteUploaded();
    }

    public class BaleRepository : BaseRepository, IBaleRepository
    {
        public BaleRepository(SiriusStyleRdContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Bale>> GetAll()
        {
            return await Context.Bale
                .Where(w => w.DeletedOn == null)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Bale>> GetAllWithProducts()
        {
            return await Context.Bale
                .Include(w => w.Products)
                .ThenInclude(w => w.Order)
                .Where(w => w.DeletedOn == null)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Bale> GetById(int baleId)
        {
            return await Context.Bale
                .SingleAsync(w => w.BaleId == baleId && w.DeletedOn == null)
                .ConfigureAwait(false);
        }

        public async Task<Bale> Create(Bale bale)
        {
            await Context.Bale.AddAsync(bale);
            await Save();

            return bale;
        }

        public async Task<IEnumerable<Bale>> BatchCreate(List<Bale> bales)
        {
            await Context.Bale.AddRangeAsync(bales);
            await Save();

            return bales;
        }

        public async Task<Bale> Update(Bale bale)
        {
            Context.Attach(bale);
            AddPropertiesToModify(bale, new List<string>
            {
                nameof(bale.Description),
                nameof(bale.Price),
                nameof(bale.BoughtTo),
                nameof(bale.CompleteUploaded)
            });

            await Save();

            return bale;
        }

        public async Task<IEnumerable<Bale>> BatchUpdate(List<Bale> bales)
        {
            foreach (var bale in bales)
            {
                Context.Attach(bale);
                AddPropertiesToModify(bale, new List<string>
                {
                    nameof(bale.Description),
                    nameof(bale.Price),
                    nameof(bale.BoughtTo),
                    nameof(bale.CompleteUploaded)
                });

                await Save();
            }

            return bales;
        }

        public async Task<Bale> Delete(Bale bale)
        {
            Context.Attach(bale);
            AddPropertiesToModify(bale, new List<string>
            {
                nameof(bale.DeletedOn)
            });

            await Save();

            return bale;
        }

        public async Task<IEnumerable<Bale>> BatchDelete(List<Bale> bales)
        {
            foreach (var bale in bales)
            {
                Context.Attach(bale);
                AddPropertiesToModify(bale, new List<string>
                {
                    nameof(bale.DeletedOn)
                });

                await Save();
            }

            return bales;
        }

        public async Task<IEnumerable<Bale>> GetAllForDropDownList(int baleId)
        {
            return await Context.Bale
                .Where(w => w.DeletedOn == null || w.BaleId == baleId)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Bale>> GetAllNotCompleteUploaded()
        {
            return await Context.Bale
                .Where(w => w.DeletedOn == null && !w.CompleteUploaded)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}