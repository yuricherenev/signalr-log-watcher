using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogWatcher.Models;

namespace LogWatcher.Persistance
{
    public interface ILogRepository
    {
        Task<LogItem> GetLogItem(Guid id);
        void AddLogItem(LogItem logItem);
        void AddLogItems(List<LogItem> logItems);
    }
}