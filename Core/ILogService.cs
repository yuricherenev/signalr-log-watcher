using System.Collections.Generic;
using LogWatcher.Models;

namespace LogWatcher.Core
{
    public interface ILogService
    {
        IEnumerable<LogItem> Read();
    }
}