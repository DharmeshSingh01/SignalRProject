﻿using Microsoft.AspNetCore.SignalR;

namespace SignalRProject.Hubs
{
    public class UserHub :Hub
    {
        public static int TotalViews { get; set; }
        public static int TotalUsers { get; set; }
        public async Task NewWindowLoaded()
        {
            TotalViews++;
            await Clients.All.SendAsync("updateTotalVies", TotalViews);
        }
        public override Task OnConnectedAsync()
        {
            TotalUsers++;
            Clients.All.SendAsync("updateTotalUsers",TotalUsers).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
           
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUsers--;
            Clients.All.SendAsync("updateTotalUsers", TotalUsers).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }
    }
}
