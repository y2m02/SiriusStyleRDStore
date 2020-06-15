using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiriusStyleRd.Entities.Models;

namespace SiriusStyleRd.Repository.Repositories
{
    public interface ISizeRepository
    {
        Task<IEnumerable<Size>> GetAll();
        Task<Size> GetById(int sizeId);
        Task<Size> Create(Size size);
        Task<IEnumerable<Size>> BatchCreate(List<Size> sizes);
        Task<Size> Update(Size size);
        Task<IEnumerable<Size>> BatchUpdate(List<Size> sizes);
        Task<Size> Delete(Size size);
        Task<IEnumerable<Size>> BatchDelete(List<Size> sizes);
        Task<IEnumerable<Size>> GetAllForDropDownList(int sizeId);
    }

    public class SizeRepository : BaseRepository, ISizeRepository
    {
        public SizeRepository(SiriusStyleRdContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Size>> GetAll()
        {
            return await Context.Size
                .OrderBy(w => w.Description)
                .Where(w => w.DeletedOn == null)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Size> GetById(int sizeId)
        {
            return await Context.Size
                .SingleAsync(w => w.SizeId == sizeId && w.DeletedOn == null)
                .ConfigureAwait(false);
        }

        public async Task<Size> Create(Size size)
        {
            await Context.Size.AddAsync(size);
            await Save();

            return size;
        }

        public async Task<IEnumerable<Size>> BatchCreate(List<Size> sizes)
        {
            await Context.Size.AddRangeAsync(sizes);
            await Save();

            return sizes;
        }

        public async Task<Size> Update(Size size)
        {
            Context.Attach(size);
            AddPropertiesToModify(size, new List<string>
            {
                nameof(size.Description)
            });

            await Save();

            return size;
        }

        public async Task<IEnumerable<Size>> BatchUpdate(List<Size> sizes)
        {
            foreach (var size in sizes)
            {
                Context.Attach(size);
                AddPropertiesToModify(size, new List<string>
                {
                    nameof(size.Description)
                });

                await Save();
            }

            return sizes;
        }

        public async Task<Size> Delete(Size size)
        {
            Context.Attach(size);
            AddPropertiesToModify(size, new List<string>
            {
                nameof(size.DeletedOn)
            });

            await Save();

            return size;
        }

        public async Task<IEnumerable<Size>> BatchDelete(List<Size> sizes)
        {
            foreach (var size in sizes)
            {
                Context.Attach(size);
                AddPropertiesToModify(size, new List<string>
                {
                    nameof(size.DeletedOn)
                });

                await Save();
            }

            return sizes;
        }

        public async Task<IEnumerable<Size>> GetAllForDropDownList(int sizeId)
        {
            return await Context.Size
                .OrderBy(w => w.Description)
                .Where(w => w.DeletedOn == null || w.SizeId == sizeId)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}