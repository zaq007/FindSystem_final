using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace FindSystem.Hubs
{
    public class FindHub : Hub
    {
        static public Dictionary<int, string> clients = new Dictionary<int, string>();

        public override System.Threading.Tasks.Task OnConnected()
        {
            if (!clients.ContainsKey(WebMatrix.WebData.WebSecurity.CurrentUserId))
                clients.Add(WebMatrix.WebData.WebSecurity.CurrentUserId, Context.ConnectionId);
            else
                clients[WebMatrix.WebData.WebSecurity.CurrentUserId] = Context.ConnectionId;
            return base.OnConnected();
        }

        public void M()
        {

        }
    }
}