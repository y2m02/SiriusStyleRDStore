using System.Collections.Generic;
using System.Threading.Tasks;
using SiriusStyleRdStore.Entities.Models;

namespace SiriusStyleRdStore.Repositories.Repositories
{
    public class BaseRepository
    {
        protected readonly SiriusStyleRdStoreContext Context;

        public BaseRepository(SiriusStyleRdStoreContext context)
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