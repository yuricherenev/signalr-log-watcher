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

        public IEnumerable<LogItem> Read()
        {
            string fileName = $@"{hostingEnvironment.WebRootPath}\commonShort.log";

            var list = new List<LogItem>();
            using(StreamReader sr = File.OpenText(fileName))
            { 
                string s = String.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    var splittedLine = s.Split(']');
                    list.Add(new LogItem()
                    {
                        Header = splittedLine[0],
                            Description = splittedLine[1]
                    });
                }
            }

            repository.AddLogItems(list);
            //await unitOfWork.Complete();
            return list;
        }
    }
}