using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebSocketHost.Hubs
{
    public class NotifyHub : Hub
    {
        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
