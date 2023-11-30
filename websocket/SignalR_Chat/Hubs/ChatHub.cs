using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalR_Chat.Hubs
{
    public class ChatHub : Hub //繼承SignalR的Hub，管理用戶的連接及訊息傳遞
    {
        public async Task SendMessage(string user, string message)
        {
            //發送給所有client端
            string conid = Context.ConnectionId;
            await Clients.All.SendAsync("ReceiveMessage", $"{user}-{conid}", message);
        }
    }

}