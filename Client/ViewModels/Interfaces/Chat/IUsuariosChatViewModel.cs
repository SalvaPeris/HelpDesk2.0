using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace HelpDesk.ViewModels
{
	public interface IUsuariosChatViewModel
	{
		public ICollection<UsuarioChat> Usuarios { get; set; }
		public ICollection<GrupoChat> Grupos { get; set; }
		public IDisposable EventChatHandler { get; set; }


		public HubConnection HubConnection { get; set; }

		public Task GetGrupos();
		public Task CrearHubConnection(Uri uri);

		public Task<HttpResponseMessage> ConectarUsuario();
		public Task<HttpResponseMessage> DesconectarUsuario();

	}
}
