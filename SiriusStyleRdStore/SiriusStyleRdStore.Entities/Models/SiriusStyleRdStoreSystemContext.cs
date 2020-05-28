using Microsoft.EntityFrameworkCore;

namespace SiriusStyleRdStore.Entities.Models
{
    public class SiriusStyleRdStoreSystemContext : DbContext
    {
        public SiriusStyleRdStoreSystemContext()
        {
        }

        public SiriusStyleRdStoreSystemContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}