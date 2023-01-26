using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
	public class SingleTicketViewModel : ISingleTicketViewModel
	{

		public string Mensaje { get; set; }
		public long TicketId { get; set; }
		[Required(ErrorMessage = "El título es necesario")]
		public string Titulo { get; set; }
		public bool Publico { get; set; }
		[Required(ErrorMessage = "El área del problema es necesaria")]
		public string Area { get; set; }
		public string TipoTicket { get; set; }
		[Required(ErrorMessage = "La descripción del problema es necesaria")]
		public string Observaciones { get; set; }
		public string RemotoApp { get; set; }
		public string RemotoID { get; set; }
		public string RemotoContrasena { get; set; }
		public string Imagen { get; set; }
		public string Gravedad { get; set; }
		public string Estado { get; set; }
		public string CreadoPor { get; set; }
		public string CreadoPorNombreCompleto { get; set; }
		public string AsignadoA { get; set; }
		public string AsignadoANombreCompleto { get; set; }
		[Required(ErrorMessage = "El teléfono es necesario")]
		public string TelefonoContacto { get; set; }
		public string EmailContacto { get; set; }
		public DateTime FechaCreado { get; set; }
		public string FechaSolucionado { get; set; }
		public string SolucionadoPor { get; set; }
		public string SolucionadoPorNombreCompleto { get; set; }

		public ICollection<SuplementoTicket> Suplementos { get; set; }
		public ICollection<EstadoTicket> Estados { get; set; }
		public ICollection<ZonaTicket> Zonas { get; set; }

		public NotificationSeverity NotificacionSeveridad { get; set; }
		private HttpClient _httpClient;

		public SingleTicketViewModel()
		{
		}

		public SingleTicketViewModel(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		/// <summary>
        /// Conseguir el Ticket actual
        /// </summary>
        /// <returns></returns>
		public Ticket GetSingleTicket()
		{
			Ticket ticket = new();
			ticket.TicketId = this.TicketId;
			ticket.Publico = this.Publico;
			ticket.Titulo = this.Titulo;
			ticket.Area = this.Area;
			ticket.Observaciones = this.Observaciones;
			ticket.RemotoApp = this.RemotoApp;
			ticket.RemotoID = this.RemotoID;
			ticket.RemotoContrasena = this.RemotoContrasena;
			ticket.Imagen = this.Imagen;
			ticket.Gravedad = this.Gravedad;
			ticket.Estado = this.Estado;
			ticket.CreadoPor = this.CreadoPor;
			ticket.CreadoPorNombreCompleto = this.CreadoPorNombreCompleto;
			ticket.AsignadoA = this.AsignadoA;
			ticket.AsignadoANombreCompleto = this.AsignadoANombreCompleto;
			ticket.TelefonoContacto = this.TelefonoContacto;
			ticket.EmailContacto = this.EmailContacto;
			ticket.FechaCreado = this.FechaCreado;
			ticket.FechaSolucionado = this.FechaSolucionado;
			ticket.Suplementos = this.Suplementos;
			ticket.Estados = this.Estados;
			ticket.Zonas = this.Zonas;

			return ticket;
		}

		/// <summary>
        /// Crea un nuevo Ticket
        /// </summary>
        /// <returns></returns>
		public async Task<HttpResponseMessage> NuevoTicket()
		{
			HttpResponseMessage _response = await _httpClient.PutAsJsonAsync("ticket/nuevo", this);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<Ticket>());
			}
			return _response;
		}

		/// <summary>
        /// Actualiza el ticket.
        /// </summary>
        /// <returns></returns>
		public async Task<HttpResponseMessage> ActualizarTicket()
		{
			return await _httpClient.PutAsJsonAsync("ticket/actualizar", this);
		}

		/// <summary>
        /// Actualiza el ticket y cambia el estado a solucionado.
        /// </summary>
        /// <returns></returns>
		public async Task<HttpResponseMessage> ActualizarYSolucionarTicket()
		{
			return await _httpClient.PutAsJsonAsync("ticket/actualizarysolucionar", this);
		}

		/// <summary>
        /// Elimina el ticket.
        /// </summary>
        /// <returns></returns>
		public async Task<HttpResponseMessage> EliminarTicket()
		{
			long _ticketId = this.TicketId;
			return await _httpClient.PutAsJsonAsync("ticket/eliminar", _ticketId);
		}

		/// <summary>
        /// Consigue el Ticket para editarlo
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
		public async Task<HttpResponseMessage> GetTicket(long ticketId)
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("ticket/getticket?ticketId=" + ticketId);

			if (_response.StatusCode == HttpStatusCode.OK)
            {
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<Ticket>());
			}
			return _response;
		}

		/// <summary>
		/// Inicia la lista de suplementos y añade suplementos
		/// </summary>
		/// <param name="suplementoTicket"></param>
		public void NuevoEstado(EstadoTicket estadoTicket)
		{
			if (Estados == null)
			{
				this.Estados = new List<EstadoTicket>();
			}
			this.Estados.Add(estadoTicket);
		}

		/// <summary>
		/// Asigna el nombre completo de un usuario a CreadoPor
		/// </summary>
		/// <param name="identificador"></param>
		/// <returns></returns>
		public async Task<HttpResponseMessage> SetNombreCompletoUsuarioPorDNICreadoPor(string identificador)
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("usuario/getnombrecompletopordni?identificador=" + identificador);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				Usuario usuario = await _response.Content.ReadFromJsonAsync<Usuario>();
				this.CreadoPorNombreCompleto = usuario.Nombre + " " + usuario.Apellidos;
			}
			return _response;
		}

		/// <summary>
		/// Asigna el nombre completo de un usuario a AsignadoA
		/// </summary>
		/// <param name="identificador"></param>
		/// <returns></returns>
		public async Task<HttpResponseMessage> SetNombreCompletoUsuarioPorDNIAsignadoA(string identificador)
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("usuario/getnombrecompletopordni?identificador=" + identificador);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				Usuario usuario = await _response.Content.ReadFromJsonAsync<Usuario>();
				this.AsignadoANombreCompleto = usuario.Nombre + " " + usuario.Apellidos;
			}
			return _response;
		}

		/// <summary>
		/// Carga el objeto actual
		/// </summary>
		/// <param name="singleTicketViewModel"></param>
		public void CargarObjetoActual(SingleTicketViewModel singleTicketViewModel)
		{
			this.TicketId = singleTicketViewModel.TicketId;
			this.Titulo = singleTicketViewModel.Titulo;
			this.Publico = singleTicketViewModel.Publico;
			this.Area = singleTicketViewModel.Area;
			this.TipoTicket = singleTicketViewModel.TipoTicket;
			this.Observaciones = singleTicketViewModel.Observaciones;
			this.RemotoApp = singleTicketViewModel.RemotoApp;
			this.RemotoID = singleTicketViewModel.RemotoID;
			this.RemotoContrasena = singleTicketViewModel.RemotoContrasena;
			this.Imagen = singleTicketViewModel.Imagen;
			this.Estado = singleTicketViewModel.Estado;
			this.Gravedad = singleTicketViewModel.Gravedad;
			this.CreadoPor = singleTicketViewModel.CreadoPor;
			this.CreadoPorNombreCompleto = singleTicketViewModel.CreadoPorNombreCompleto;
			this.AsignadoA = singleTicketViewModel.AsignadoA;
			this.AsignadoANombreCompleto = singleTicketViewModel.AsignadoANombreCompleto;
			this.TelefonoContacto = singleTicketViewModel.TelefonoContacto;
			this.EmailContacto = singleTicketViewModel.EmailContacto;
			this.FechaCreado = singleTicketViewModel.FechaCreado;
			this.FechaSolucionado = singleTicketViewModel.FechaSolucionado;
			this.Suplementos = singleTicketViewModel.Suplementos;
			this.Estados = singleTicketViewModel.Estados;
			this.Zonas = singleTicketViewModel.Zonas;
		}

		/// <summary>
        /// Deja el vista modelo a null
        /// </summary>
		public void Anular()
		{
			this.TicketId = 0;
			this.Titulo = null;
			this.Publico = false;
			this.Observaciones = null;
			this.Area = null;
			this.TipoTicket = null;
			this.RemotoApp = null;
			this.RemotoID = null;
			this.RemotoContrasena = null;
			this.Imagen = null;
			this.Estado = null;
			this.Gravedad = null;
			this.CreadoPor = null;
			this.CreadoPorNombreCompleto = null;
			this.AsignadoA = null;
			this.AsignadoANombreCompleto = null;
			this.TelefonoContacto = null;
			this.EmailContacto = null;
			this.FechaCreado = DateTime.Now;
			this.FechaSolucionado = null;
			this.Suplementos = null;
			this.Estados = null;
			this.Zonas = null;
		}

        public void NuevaZonaTicket(ZonaTicket zonaTicket)
        {
			if (Zonas == null)
			{
				this.Zonas = new List<ZonaTicket>();
			}
			this.Zonas.Add(zonaTicket);
		}

		public void ModificarZonaTicket(ZonaTicket zonaTicket, ZonaTicket zonaTicketModificado)
		{
            ZonaTicket _zona = this.Zonas.FirstOrDefault(z => z == zonaTicket);
            _zona.Nombre = zonaTicketModificado.Nombre;
			_zona.Situacion = zonaTicketModificado.Situacion;
			_zona.Observaciones = zonaTicketModificado.Observaciones;
        }

		/// <summary>
        /// Añade un nuevo suplemento
        /// </summary>
        /// <param name="suplementoTicket"></param>
		public void NuevoSuplemento(SuplementoTicket suplementoTicket)
		{
			if (Suplementos == null)
			{
				this.Suplementos = new List<SuplementoTicket>();
			}
			this.Suplementos.Add(suplementoTicket);
		}

		/// <summary>
        /// Se modifica el suplemento
        /// </summary>
        /// <param name="suplementoTicket"></param>
        /// <param name="suplementoTicketModificado"></param>
		public void ModificarSuplemento(SuplementoTicket suplementoTicket, SuplementoTicket suplementoTicketModificado)
		{
			SuplementoTicket _suplemento = this.Suplementos.FirstOrDefault(s => s == suplementoTicket);
			_suplemento.Comentario = suplementoTicketModificado.Comentario;
			_suplemento.FechaCreacion = suplementoTicketModificado.FechaCreacion;
			_suplemento.Imagen = suplementoTicketModificado.Imagen;
		}

		public static implicit operator SingleTicketViewModel(Ticket ticket)
		{
			return new SingleTicketViewModel
			{
				TicketId = ticket.TicketId,
				Titulo = ticket.Titulo,
				Publico = ticket.Publico,
				Area = ticket.Area,
				TipoTicket = ticket.TipoTicket,
				Observaciones = ticket.Observaciones,
				RemotoApp = ticket.RemotoApp,
				RemotoID = ticket.RemotoID,
				RemotoContrasena = ticket.RemotoContrasena,
				Imagen = ticket.Imagen,
				Estado = ticket.Estado,
				Gravedad = ticket.Gravedad,
				CreadoPor = ticket.CreadoPor,
				CreadoPorNombreCompleto = ticket.CreadoPorNombreCompleto,
				AsignadoA = ticket.AsignadoA,
				AsignadoANombreCompleto = ticket.AsignadoANombreCompleto,
				TelefonoContacto = ticket.TelefonoContacto,
				EmailContacto = ticket.EmailContacto,
				FechaCreado = ticket.FechaCreado,
				FechaSolucionado = ticket.FechaSolucionado,
				Suplementos = ticket.Suplementos,
				Estados = ticket.Estados,
				Zonas = ticket.Zonas
			};
		}

		public static implicit operator Ticket(SingleTicketViewModel singleTicketViewModel)
		{
			return new Ticket
			{
				TicketId = singleTicketViewModel.TicketId,
				Titulo = singleTicketViewModel.Titulo,
				Publico = singleTicketViewModel.Publico,
				Area = singleTicketViewModel.Area,
				TipoTicket = singleTicketViewModel.TipoTicket,
				Observaciones = singleTicketViewModel.Observaciones,
				RemotoApp = singleTicketViewModel.RemotoApp,
				RemotoID = singleTicketViewModel.RemotoID,
				RemotoContrasena = singleTicketViewModel.RemotoContrasena,
				Imagen = singleTicketViewModel.Imagen,
				Estado = singleTicketViewModel.Estado,
				Gravedad = singleTicketViewModel.Gravedad,
				CreadoPor = singleTicketViewModel.CreadoPor,
				CreadoPorNombreCompleto = singleTicketViewModel.CreadoPorNombreCompleto,
				AsignadoA = singleTicketViewModel.AsignadoA,
				AsignadoANombreCompleto = singleTicketViewModel.AsignadoANombreCompleto,
				TelefonoContacto = singleTicketViewModel.TelefonoContacto,
				EmailContacto = singleTicketViewModel.EmailContacto,
				FechaCreado = singleTicketViewModel.FechaCreado,
				FechaSolucionado = singleTicketViewModel.FechaSolucionado,
				Suplementos = singleTicketViewModel.Suplementos,
				Estados = singleTicketViewModel.Estados,
				Zonas = singleTicketViewModel.Zonas
			};
		}
	}
}

