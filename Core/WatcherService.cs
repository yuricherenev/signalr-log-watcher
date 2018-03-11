using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using LogWatcher.Hubs;
using LogWatcher.Models;
using LogWatcher.Persistance;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace LogWatcher.Core
{
    public class WatcherService : BackgroundService, IWatcherService
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IHubContext<Broadcaster> messageHubContext;
        private FileSystemWatcher watcher;
        private LogFile file;

        public WatcherService(
            IHostingEnvironment hostingEnvironment,
            IHubContext<Broadcaster> messageHubContext)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.messageHubContext = messageHubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine($"GracePeriodManagerService is starting.");
            stoppingToken.Register(() => Console.WriteLine($" GracePeriod background task is stopping."));

            Watch();
            await Task.Delay(5000, stoppingToken);

            Console.WriteLine($"GracePeriod background task is stopping.");
        }

        public void Watch()
        {
            Console.WriteLine("FileSystemWatcher run");
            watcher = new FileSystemWatcher();
            watcher.Path = hostingEnvironment.WebRootPath;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*.log";

            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                watcher.EnableRaisingEvents = false;

                if (file != null && file.FileName.Contains(e.FullPath))
                {
                    Console.WriteLine("file is not null {0}", file.LastPosition);
                    file = LogReader.GetFileFromPath(e.FullPath, file.LastPosition);
                }
                else
                {
                    Console.WriteLine("file is null");
                    file = LogReader.GetFileFromPath(e.FullPath);
                }

                var logs = file.Logs;
                messageHubContext.Clients.All.InvokeAsync("send", logs);
            }
            finally
            {
                watcher.EnableRaisingEvents = true;
            }
        }
    }
}