using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogWatcher.Models;
using Microsoft.EntityFrameworkCore;

namespace LogWatcher.Persistance
{
    public class LogRepository : ILogRepository
    {
        private readonly LogDbContext context;
        public LogRepository(LogDbContext context)
        {
            this.context = context;
        }
        public async Task<LogItem> GetLogItem(Guid id)
        {
            return await context.LogItems.SingleOrDefaultAsync(log => log.Id == id);
        }

        public void AddLogItem(LogItem logItem)
        {
            context.LogItems.Add(logItem);
        }
        public void AddLogItems(List<LogItem> logItems)
        {
            context.LogItems.AddRange(logItems);
        }
    }
}