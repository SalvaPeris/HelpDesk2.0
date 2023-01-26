using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using HelpDesk.Server.DB;
using HelpDesk.Server.Repository;
using HelpDesk.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace HelpDesk.Server.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IGrupoChatRepository _grupoChatRepository;

        public ChatHub(HelpDeskContext context)
        {
            this._grupoChatRepository = new GrupoChatRepository(context);
        }

        public async override Task<Task> OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.FindFirstValue(ClaimTypes.Name));
            Console.WriteLine("Se ha conectado " + Context.User.FindFirstValue(ClaimTypes.Name) + " con " + Context.ConnectionId);
            //ICollection<GrupoChat> grupos = await _grupoChatRepository.GetGruposChat();
            //Console.WriteLine("Número de grupos: " + grupos.Count);

            return base.OnConnectedAsync();
        }

        /*public async Task SendMessageAddedNewContact(string message)
        {
            await Clients.All.SendAsync("Connection", Context.User.FindFirstValue(ClaimTypes.Name), " se ha conectado.");
        }


        public async Task SendMessageAsync(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }


        public async Task ChatNotificationAsync(string mensajeChat, string receiverUserId, string senderUserId)
        {
            await Clients.All.SendAsync("ReceiveChatNotification", mensajeChat, receiverUserId, senderUserId);
        }


        public Task SendMessageToGroup(string sender, string receiver, string message)
        {
            //message send to receiver only
            return Clients.Group(receiver).SendAsync("ReceiveMessage", sender, message);
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", Context.User.FindFirstValue(ClaimTypes.Name), message);
        }*/

    }
}

