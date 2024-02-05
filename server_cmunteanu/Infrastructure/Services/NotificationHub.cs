using Confluent.Kafka;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string type, string message, string? additionalInfo)
        {
            await Clients.All.SendAsync("ReceiveNotification", type, message, additionalInfo);
        }
    }
}
