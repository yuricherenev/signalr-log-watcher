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

        public void AddFile(LogFile file)
        {
            context.LogFiles.Add(file);
        }

        public async Task<LogFile> GetByNameAsync(string name, bool includeRelated = false)
        {
            if (!includeRelated)
                return await context.LogFiles.SingleOrDefaultAsync(f => f.FileName == name);

            return await context.LogFiles
                .Include(v => v.Logs)
                .SingleOrDefaultAsync(v => v.FileName == name);
        }

        public async Task<long> GetLastPositionAsync(string name)
        {
            var logFile = await context.LogFiles.SingleOrDefaultAsync(f => f.FileName == name);
            return logFile == null ? 0 : logFile.LastPosition;
        }
    }
}