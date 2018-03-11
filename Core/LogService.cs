using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LogWatcher.Models;
using LogWatcher.Persistance;
using Microsoft.AspNetCore.Hosting;

namespace LogWatcher.Core
{
    public class LogService : ILogService
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ILogRepository repository;
        private readonly IUnitOfWork unitOfWork;
        public LogService(
            IHostingEnvironment hostingEnvironment,
            ILogRepository repository,
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.hostingEnvironment = hostingEnvironment;
        }

        public ICollection<LogItem> Read()
        {
            string filePath = $@"{hostingEnvironment.WebRootPath}\commonShort.log";
            var logFile = LogReader.GetFileFromPath(filePath);
            return logFile.Logs;
        }
    }
}