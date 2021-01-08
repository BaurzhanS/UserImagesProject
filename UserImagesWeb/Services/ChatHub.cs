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
            //var user = userService.FindByCondition(p=>p.Email==userEmail).FirstOrDefault();

            //if(user==null) throw new Exception("There is no such user");

            await this.Clients.All.SendAsync("Send", userEmail);
        }

        public async Task SendEmail(string userEmail)
        {
            //var user = userService.FindByCondition(p=>p.Email==userEmail).FirstOrDefault();

            //if(user==null) throw new Exception("There is no such user");

            await this.Clients.All.SendAsync("Send", userEmail);
        }
    }
}
