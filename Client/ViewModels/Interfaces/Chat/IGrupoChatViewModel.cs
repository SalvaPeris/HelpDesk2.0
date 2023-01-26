using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen.Blazor;

namespace HelpDesk.ViewModels
{
	public interface IGrupoChatViewModel
	{
		public GrupoChat GrupoChat { get; set; }
		public EstadoChat EstadoPantallaChat { get; set; }

		public Task<HttpResponseMessage> GetHistorialChat(Guid usuarioId);
		public Task<HttpResponseMessage> GetHistorialChatGrupal(Guid usuarioId);

		public Task<HttpResponseMessage> NuevoMensaje(MensajeChat mensajeChat);

	}
}
