using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiriusStyleRd.Entities.Models;

namespace SiriusStyleRd.Repository.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(int categoryId);
        Task<Category> Create(Category category);
        Task<IEnumerable<Category>> BatchCreate(List<Category> categories);
        Task<Category> Update(Category category);
        Task<IEnumerable<Category>> BatchUpdate(List<Category> categories);
        Task<Category> Delete(Category category);
        Task<IEnumerable<Category>> BatchDelete(List<Category> categories);
        Task<IEnumerable<Category>> GetAllForDropDownList(int categoryId);
    }

    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(SiriusStyleRdContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await Context.Category
                .OrderBy(w => w.Description)
                .Where(w => w.DeletedOn == null)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Category> GetById(int categoryId)
        {
            return await Context.Category
                .SingleAsync(w => w.CategoryId == categoryId && w.DeletedOn == null)
                .ConfigureAwait(false);
        }

        public async Task<Category> Create(Category category)
        {
            await Context.Category.AddAsync(category);
            await Save();

            return category;
        }

        public async Task<IEnumerable<Category>> BatchCreate(List<Category> categories)
        {
            await Context.Category.AddRangeAsync(categories);
            await Save();

            return categories;
        }

        public async Task<Category> Update(Category category)
        {
            Context.Attach(category);
            AddPropertiesToModify(category, new List<string>
            {
                nameof(category.Description)
            });

            await Save();

            return category;
        }

        public async Task<IEnumerable<Category>> BatchUpdate(List<Category> categories)
        {
            foreach (var category in categories)
            {
                Context.Attach(category);
                AddPropertiesToModify(category, new List<string>
                {
                    nameof(category.Description)
                });

                await Save();
            }

            return categories;
        }

        public async Task<Category> Delete(Category category)
        {
            Context.Attach(category);
            AddPropertiesToModify(category, new List<string>
            {
                nameof(category.DeletedOn)
            });

            await Save();

            return category;
        }

        public async Task<IEnumerable<Category>> BatchDelete(List<Category> categories)
        {
            foreach (var category in categories)
            {
                Context.Attach(category);
                AddPropertiesToModify(category, new List<string>
                {
                    nameof(category.DeletedOn)
                });

                await Save();
            }

            return categories;
        }

        public async Task<IEnumerable<Category>> GetAllForDropDownList(int categoryId)
        {
            return await Context.Category
                .OrderBy(w=>w.Description)
                .Where(w => w.DeletedOn == null || w.CategoryId == categoryId)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}