using System.Threading.Tasks;

namespace LogWatcher.Persistance
{
    public interface IUnitOfWork
    {
        Task Complete();
    }
}