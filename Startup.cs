﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LogWatcher.Core;
using LogWatcher.Hubs;
using LogWatcher.Persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LogWatcher
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            HostingEnvironment = env;
            Configuration = config;
        }

        public IHostingEnvironment HostingEnvironment { get; }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddDbContext<LogDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddSignalR();
            services.AddCors(options =>
                options.AddPolicy("AllowAny",
                    x => { x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); }));
            services.AddMvc();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();            
            services.AddSingleton<IWatcherService, WatcherService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseWatcher();
            app.UseCors("AllowAny");
            app.UseSignalR(routes =>
            {
                routes.MapHub<Broadcaster>("message");
            });
            app.UseMvc();
        }
    }
}