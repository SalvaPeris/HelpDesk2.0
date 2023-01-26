
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
	public class UsuariosViewModel : IUsuariosViewModel
	{

		public Usuario[] Usuarios { get; set; }
		private HttpClient _httpClient;

		public UsuariosViewModel()
		{
		}

		public UsuariosViewModel(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		/// <summary>
        /// Devuelve los usuarios
        /// </summary>
        /// <returns></returns>
		public async Task<HttpResponseMessage> GetUsuarios()
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("usuario/getusuarios");

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<Usuario[]>());
			}
			return _response;
		}

		private void CargarObjetoActual(UsuariosViewModel usuariosViewModel)
		{
			this.Usuarios = usuariosViewModel.Usuarios;
		}

		public static implicit operator UsuariosViewModel(Usuario[] usuarios)
		{
			return new UsuariosViewModel
			{
				Usuarios = usuarios
			};
		}

		public static implicit operator Usuario[](UsuariosViewModel usuariosViewModel)
		{
			int usuariosLength = usuariosViewModel.Usuarios.Length;
			Usuario[] usuarios = new Usuario[usuariosLength];
			usuarios = usuariosViewModel.Usuarios;
			return usuarios;
		}
	}
}
