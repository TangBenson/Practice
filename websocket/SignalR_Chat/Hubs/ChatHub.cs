using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalR_Chat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            string conid = Context.ConnectionId;
            await Clients.All.SendAsync("ReceiveMessage", $"{user}-{conid}", message);
        }
    }

}