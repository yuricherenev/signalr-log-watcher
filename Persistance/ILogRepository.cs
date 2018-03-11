using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogWatcher.Models;

namespace LogWatcher.Persistance
{
    public interface ILogRepository
    {
        //Task<LogItem> GetLogItem(Guid id);
        void AddFile(LogFile file);
        Task<LogFile> GetByNameAsync(string name, bool includeRelated);

        Task<long> GetLastPositionAsync(string name);
    }
}