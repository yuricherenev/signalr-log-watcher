using System;
using System.IO;
using LogWatcher.Hubs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;

namespace LogWatcher.Core
{
    public class WatcherService : IWatcherService
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ILogService logService;
        private readonly IHubContext<Broadcaster> messageHubContext;
        public WatcherService(
            ILogService logService,
            IHostingEnvironment hostingEnvironment,
            IHubContext<Broadcaster> messageHubContext)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.logService = logService;
            this.messageHubContext = messageHubContext;
        }
        public void Watch()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = hostingEnvironment.WebRootPath;
            watcher.NotifyFilter = NotifyFilters.LastWrite;

            watcher.Filter = "*.log";

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            var list = logService.Read();
            messageHubContext.Clients.All.InvokeAsync("Send", list);
        }
    }
}