using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace LogWatcher.Hubs
{
    public class Broadcaster : Hub
    {
        public Task Send(string message)
        {
            Console.WriteLine("send {0}", message);
            return Clients.All.InvokeAsync("send", message);
        }
    }
}