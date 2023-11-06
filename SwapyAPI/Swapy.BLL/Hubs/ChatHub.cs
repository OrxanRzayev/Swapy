using Microsoft.AspNetCore.SignalR;
using Swapy.Common.Models;

namespace Swapy.BLL.Hubs
{
    public class ChatHub : Hub
    {
        private static List<ConnectedClient> _connectedClients = new List<ConnectedClient>();

        public override Task OnConnectedAsync()
        {
            var userId = string.Empty;
            var queryString = Context.GetHttpContext().Request.QueryString;
            if (queryString.HasValue) userId = queryString.Value.Split('=').Last();

            _connectedClients.Add(new ConnectedClient
            {
                ConnectionId = Context.ConnectionId,
                UserId = userId
            });

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var client = _connectedClients.FirstOrDefault(c => c.ConnectionId == Context.ConnectionId);
            
            if (client != null) _connectedClients.Remove(client);

            return base.OnDisconnectedAsync(exception);
        }

        public static List<ConnectedClient> GetConnectedClients()
        {
            return _connectedClients;
        }

        public List<ConnectedClient> GetConnectedClientsForUser(string userId)
        {
            return _connectedClients.Where(c => c.UserId == userId).ToList();
        }
    }
}
