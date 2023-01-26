using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
	public class SingleJornadaViewModel : ISingleJornadaViewModel
	{
		public long JornadaId { get; set; }
		public DateTime FechaJornada { get; set; }
		public Usuario Usuario { get; set; }
		public Guid UsuarioId { get; set; }
		public string UsuarioNombreCompleto { get; set; }
		public string ImagenUsuario { get; set; }
		public virtual ICollection<Fichaje> Fichajes { get; set; }
		public int AusenciaCuenta { get; set; }
		public virtual ICollection<JornadaAusencia> Ausencias { get; set; }

		public string Mensaje { get; set; }
		public NotificationSeverity NotificacionSeveridad { get; set; }

		private readonly HttpClient _httpClient;

		public SingleJornadaViewModel()
		{
		}

		public SingleJornadaViewModel(HttpClient httpClient)
		{
			this._httpClient = httpClient;
		}

		/// <summary>
		/// Trae la jornada de un usuario concreto
		/// </summary>
		/// <param name="identificador"></param>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetJornadaPorId(string identificador)
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("jornada/getjornadaporid?jornadaid=" + identificador);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<Jornada>());
			}

			return _response;
		}

		/// <summary>
        /// Devuelve la jornada actual
        /// </summary>
        /// <returns></returns>
		public Jornada GetSingleJornada()
		{
			return this;
		}

		/// <summary>
        /// Se setea la jornada
        /// </summary>
        /// <param name="jornada"></param>
		public void SetSingleJornada(Jornada jornada)
		{
			this.JornadaId = jornada.JornadaId;
			this.FechaJornada = jornada.FechaJornada;
			this.Usuario = jornada.Usuario;
			this.UsuarioId = jornada.UsuarioId;
			this.UsuarioNombreCompleto = jornada.UsuarioNombreCompleto;
			this.ImagenUsuario = jornada.ImagenUsuario;
			this.Fichajes = jornada.Fichajes;
			this.AusenciaCuenta = jornada.AusenciaCuenta;
			this.Ausencias = jornada.Ausencias;
		}

		/// <summary>
		/// Se anula SingleJornadaViewModel
		/// </summary>
		public void Anular()
		{
			JornadaId = 0;
			FechaJornada = DateTime.Now;
			Usuario = null;
			UsuarioId = new Guid();
			ImagenUsuario = null;
			Fichajes = null;
			AusenciaCuenta = 0;
			Ausencias = null;
		}

		/// <summary>
		/// Crea nueva jornada
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> NuevaJornada()
		{
			HttpResponseMessage _response = await _httpClient.PutAsJsonAsync("jornada/nuevo", this);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<Jornada>());
			}
			return _response;
		}

		/// <summary>
		/// Elimina la jornada
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> EliminarJornada()
		{
			long _dispositivoId = this.JornadaId;
			return await _httpClient.PutAsJsonAsync("jornada/eliminar", this);
		}

		/// <summary>
		/// Actualiza la jornada
		/// </summary>
		/// <returns></returns>
		public async Task<HttpResponseMessage> ActualizaJornada()
		{
			HttpResponseMessage _response = await _httpClient.PutAsJsonAsync("jornada/actualizar", this);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<Jornada>());
			}

			return _response;
		}

		private void CargarObjetoActual(SingleJornadaViewModel singleJornadaViewModel)
		{
			this.JornadaId = singleJornadaViewModel.JornadaId;
			this.FechaJornada = singleJornadaViewModel.FechaJornada;
			this.Usuario = singleJornadaViewModel.Usuario;
			this.UsuarioId = singleJornadaViewModel.UsuarioId;
			this.UsuarioNombreCompleto = singleJornadaViewModel.UsuarioNombreCompleto;
			this.ImagenUsuario = singleJornadaViewModel.ImagenUsuario;
			this.Fichajes = singleJornadaViewModel.Fichajes;
			this.AusenciaCuenta = singleJornadaViewModel.AusenciaCuenta;
			this.Ausencias = singleJornadaViewModel.Ausencias;
		}

		public static implicit operator SingleJornadaViewModel(Jornada jornada)
		{
			return new SingleJornadaViewModel
			{
				JornadaId = jornada.JornadaId,
				FechaJornada = jornada.FechaJornada,
				Usuario = jornada.Usuario,
				UsuarioId = jornada.UsuarioId,
				UsuarioNombreCompleto = jornada.UsuarioNombreCompleto,
				ImagenUsuario = jornada.ImagenUsuario,
				Fichajes = jornada.Fichajes,
				AusenciaCuenta = jornada.AusenciaCuenta,
				Ausencias = jornada.Ausencias

			};
		}

		public static implicit operator Jornada(SingleJornadaViewModel singleJornadaViewModel)
		{
			return new Jornada
			{
				JornadaId = singleJornadaViewModel.JornadaId,
				FechaJornada = singleJornadaViewModel.FechaJornada,
				Usuario = singleJornadaViewModel.Usuario,
				UsuarioId = singleJornadaViewModel.UsuarioId,
				UsuarioNombreCompleto = singleJornadaViewModel.UsuarioNombreCompleto,
				ImagenUsuario = singleJornadaViewModel.ImagenUsuario,
				Fichajes = singleJornadaViewModel.Fichajes,
				AusenciaCuenta = singleJornadaViewModel.AusenciaCuenta,
				Ausencias = singleJornadaViewModel.Ausencias
			};
		}
	}
}
