using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exposure_api.SignalRModule.Logic
{
    public class DataHub : Hub
    {
        public async Task SendData(string data)
        {
            await Clients.All.SendAsync("ReceiveData", data, Context.ConnectionId);
        }
    }
}
