using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace HelpDesk.ViewModels
{
	public class UsuariosChatViewModel : IUsuariosChatViewModel, IAsyncDisposable
	{
        public ICollection<UsuarioChat> Usuarios { get; set; }
        public ICollection<GrupoChat> Grupos { get; set; }
        public IDisposable EventChatHandler { get; set; }
        public HubConnection HubConnection { get; set; } 
        private HttpClient _httpClient;
        

        public UsuariosChatViewModel()
        {
        }

        public UsuariosChatViewModel(HttpClient httpClient)
		{
            _httpClient = httpClient;
            Grupos = new List<GrupoChat>();
        }

        public async Task CrearHubConnection(Uri uri)
        {
            HubConnection = new HubConnectionBuilder()
                        .WithUrl(uri)
                        .Build();
            await this.HubConnection.StartAsync();
        }

        public async Task GetGrupos()
        {
            GruposChat gruposChat = await _httpClient.GetFromJsonAsync<GruposChat>("chat/getgrupos");
            CargarObjetoActual(gruposChat);

            /*if (_usuarios.Length > 0)
            {
                this.Mensaje = "Tickets cargados correctamente";
                this.NotificacionSeveridad = NotificationSeverity.Success;
            }
            else
            {
                this.Mensaje = "Error al cargar los tickets.";
                this.NotificacionSeveridad = NotificationSeverity.Error;
            }*/
        }

        public async Task<HttpResponseMessage> ConectarUsuario()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("chat/conectarusuario");
            return response;
            //CargarObjetoActual(_usuarios);

            /*if (_usuarios.Length > 0)
            {
                this.Mensaje = "Tickets cargados correctamente";
                this.NotificacionSeveridad = NotificationSeverity.Success;
            }
            else
            {
                this.Mensaje = "Error al cargar los tickets.";
                this.NotificacionSeveridad = NotificationSeverity.Error;
            }*/
        }

        public async Task<HttpResponseMessage> DesconectarUsuario()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("chat/desconectarusuario");
            return response;
            //CargarObjetoActual(_usuarios);

            /*if (_usuarios.Length > 0)
            {
                this.Mensaje = "Tickets cargados correctamente";
                this.NotificacionSeveridad = NotificationSeverity.Success;
            }
            else
            {
                this.Mensaje = "Error al cargar los tickets.";
                this.NotificacionSeveridad = NotificationSeverity.Error;
            }*/

        }

        private void CargarObjetoActual(UsuariosChatViewModel usuariosChatViewModel)
        {
            this.Usuarios = usuariosChatViewModel.Usuarios;
            this.Grupos = usuariosChatViewModel.Grupos;

        }

        public async ValueTask DisposeAsync()
        {
            await _httpClient.GetAsync("chat/desconectarusuario");
        }

        public static implicit operator UsuariosChatViewModel(GruposChat gruposChat)
        {
            return new UsuariosChatViewModel
            {
                Usuarios = gruposChat.Usuarios,
                Grupos = gruposChat.Grupos
            };
        }

        public static implicit operator GruposChat(UsuariosChatViewModel usuariosChatViewModel)
        {
            return new GruposChat()
            {
                Usuarios = usuariosChatViewModel.Usuarios,
                Grupos = usuariosChatViewModel.Grupos
            };
        }
    }
}

