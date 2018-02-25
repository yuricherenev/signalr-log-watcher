using System;
using System.Collections.Generic;
using LogWatcher.Core;
using LogWatcher.Hubs;
using LogWatcher.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace LogWatcher.Controllers
{
    public class WatcherController : Controller
    {
        private readonly ILogService logService;
        private readonly IWatcherService watcherService;
        private readonly IHubContext<Broadcaster> messageHubContext;
        public WatcherController(
            ILogService logService,
            IWatcherService watcherService,
            IHubContext<Broadcaster> messageHubContext)
        {
            this.watcherService = watcherService;
            this.logService = logService;
            this.messageHubContext = messageHubContext;
        }

        [HttpGet("/api/logs")]
        public IActionResult GetAll()
        {
            watcherService.Watch();
            var list = logService.Read();
            messageHubContext.Clients.All.InvokeAsync("Send", "hello from getAll");
            return Ok();
        }
    }
}