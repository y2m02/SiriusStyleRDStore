using System.Collections.Generic;
using System.Threading.Tasks;
using SiriusStyleRd.Entities.Models;

namespace SiriusStyleRd.Repository.Repositories
{
    public class BaseRepository
    {
        protected readonly SiriusStyleRdContext Context;

        public BaseRepository(SiriusStyleRdContext context)
        {
            Context = context;
        }

        public async Task Save()
        {
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        protected void AddPropertiesToModify<T>(T entity, List<string> properties)
        {
            properties.ForEach(propertyName => { Context.Entry(entity).Property(propertyName).IsModified = true; });
        }
    }
}