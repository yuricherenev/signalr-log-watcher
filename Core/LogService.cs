using System;
using System.Collections.Generic;
using System.IO;
using LogWatcher.Models;
using Microsoft.AspNetCore.Hosting;

namespace LogWatcher.Core
{
    public class LogService : ILogService
    {
        private readonly IHostingEnvironment hostingEnvironment;
        public LogService(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public IEnumerable<LogItem> Read()
        {
            string fileName = $@"{hostingEnvironment.WebRootPath}\commonShort.log";
            Console.WriteLine("fileName: {0}", fileName);
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

            return list;
        }
    }
}