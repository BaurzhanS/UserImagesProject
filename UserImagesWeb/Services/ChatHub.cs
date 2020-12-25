using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserImagesService;

namespace UserImagesWeb.Services
{
    public class ChatHub : Hub
    {
        private readonly IUserService userService;

        public ChatHub(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task Send(string userEmail)
        {
            var users = userService.GetUsers();

            await this.Clients.All.SendAsync("Send", userEmail);
        }
    }
}
