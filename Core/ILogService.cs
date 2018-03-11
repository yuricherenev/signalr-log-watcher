using System.Collections.Generic;
using System.Threading.Tasks;
using LogWatcher.Models;

namespace LogWatcher.Core
{
    public interface ILogService
    {
        ICollection<LogItem> Read();
    }
}