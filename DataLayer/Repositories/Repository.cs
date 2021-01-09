using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public abstract class Repository<TValue>  where TValue : class
    {
        private readonly DatingAppContext context;

        public Repository(DatingAppContext context)
        {
            this.context = context;
        }

        public DbSet<TValue> items => context.Set<TValue>();

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
