using System.Threading.Tasks;

namespace LogWatcher.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LogDbContext context;
        public UnitOfWork(LogDbContext context)
        {
            this.context = context;
        }
        public async Task Complete()
        {
           await context.SaveChangesAsync();
        }
    }
}