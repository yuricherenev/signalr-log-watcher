using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LogWatcher.Hubs;
using LogWatcher.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;

namespace LogWatcher.Core
{
    public class WatcherService : IWatcherService
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IHubContext<Broadcaster> messageHubContext;
        private DateTime lastTimeFileEventRaised;

        public WatcherService(
            IHostingEnvironment hostingEnvironment,
            IHubContext<Broadcaster> messageHubContext)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.messageHubContext = messageHubContext;
        }
        public void Watch()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = hostingEnvironment.WebRootPath;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*.log";

            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (DateTime.Now.Subtract(lastTimeFileEventRaised).TotalMilliseconds < 500)
                return;

            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);

            lastTimeFileEventRaised = DateTime.Now;

            var list = Read();
            messageHubContext.Clients.All.InvokeAsync("Send", list);
        }

        public async Task<IEnumerable<LogItem>> Read()
        {
            string fileName = $@"{hostingEnvironment.WebRootPath}\commonShort.log";

            var list = new List<LogItem>();

            //FileStream afile = new FileStream(fileName, FileMode.Open, fil);
            var lines = await File.ReadAllLinesAsync(fileName);
            
            foreach (var line in lines)
            {
                var splittedLine = line.Split(']');
                list.Add(new LogItem()
                {
                    Header = splittedLine[0],
                        Description = splittedLine[1]
                });
            }
            Console.WriteLine("File read");
            return list;
        }
    }
}