using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LogWatcher.Core;
using LogWatcher.Hubs;
using LogWatcher.Models;
using LogWatcher.Persistance;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace LogWatcher.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogService logService;
       // private readonly IWatcherService watcherService;
        private readonly IHubContext<Broadcaster> messageHubContext;
        private readonly IMapper mapper;
        private readonly ILogRepository repository;

        public LogController(
            ILogService logService,
            IMapper mapper,
            ILogRepository repository,
            IHubContext<Broadcaster> messageHubContext)
        {
            this.logService = logService;
            this.messageHubContext = messageHubContext;
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpGet("/api/logs/all")]
        public ICollection<LogItem> GetAll()
        {
            var list = logService.Read();
            return list;
        }

    }
}