using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace LogWatcher.Hubs
{
    public class Broadcaster : Hub
    {
        public Task Send(string message)
        {
            return Clients.All.InvokeAsync("Send", message);
        }
    }
}