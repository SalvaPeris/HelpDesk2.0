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
	public class SingleDispositivoViewModel : ISingleDispositivoViewModel
	{
		public Guid DispositivoId { get; set; }
		[Required(ErrorMessage = "El número de serie es necesario")]
		public string NumeroSerie { get; set; }
		public string Marca { get; set; }
		public string Modelo { get; set; }
		public string Licencia { get; set; }
		public string Situacion { get; set; }
		public DateTime FechaInstalado { get; set; }
		public string CompradoA { get; set; }
		public string Observaciones { get; set; }
		public bool FuncionaBien { get; set; }
		public bool Utilizado { get; set; }
		public bool LlevaMantenimiento { get; set; }
		public DateTime FechaCreado { get; set; }
		public long TipoDispositivoId { get; set; }
		public string TipoNombre { get; set; }
		public TipoDispositivo Tipo { get; set; }

		public int UsuariosCuenta { get; set; }
		public ICollection<DispositivoUsuario> Usuarios { get; set; }

		public ICollection<EstadoDispositivo> Estados { get; set; }

		public string DepartamentoNombre { get; set; }
		public Departamento Departamento { get; set; }
		public long DepartamentoId { get; set; }

		public string EmpresaExternaNombre { get; set; }
		public EmpresaExterna EmpresaExterna { get; set; }
		public long EmpresaExternaId { get; set; }

		public string Mensaje { get; set; }
		public NotificationSeverity NotificacionSeveridad { get; set; }

		private readonly HttpClient _httpClient;

		public SingleDispositivoViewModel()
		{
		}

		public SingleDispositivoViewModel(HttpClient httpClient)
		{
			this._httpClient = httpClient;
		}

		/// <summary>
		/// Trae los dispositivos de un usuario concreto
		/// </summary>
		/// <param name="usuarioId"></param>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetDispositivoPorId(string identificador)
		{
			HttpResponseMessage _response = await _httpClient.GetAsync("dispositivo/getdispositivoporid?dispositivoid=" + identificador);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<Dispositivo>());
			}

			return _response;
		}

        public Dispositivo GetSingleDispositivo()
        {
			return this;
        }

        /*public void SetSingleDispositivo(Dispositivo dispositivo)
        {
			this.NumeroSerie = dispositivo.NumeroSerie;
        }*/

		/// <summary>
        /// Se anula DispositivoViewModel
        /// </summary>
        public void Anular()
        {
			DispositivoId = new Guid();
			NumeroSerie = null;
			Marca = null;
			Modelo = null;
			Licencia = null;
			Situacion = null;
			FechaInstalado = DateTime.Now;
			CompradoA = null;
			Observaciones = null;
			FuncionaBien = false;
			Utilizado = false;
			LlevaMantenimiento = false;
			FechaCreado = DateTime.Now;
			TipoDispositivoId = 1;
			TipoNombre = null;
			Tipo = null;
			UsuariosCuenta = 0;
			Usuarios = null;
			Estados = null;
			DepartamentoNombre = null;
			Departamento = null;
			DepartamentoId = 0;
			EmpresaExternaNombre = null;
			EmpresaExterna = null;
			EmpresaExternaId = 0;
		}

		/// <summary>
        /// Crea nuevo dispositivo
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> NuevoDispositivo()
        {
			HttpResponseMessage _response = await _httpClient.PutAsJsonAsync("dispositivo/nuevo", this);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<Dispositivo>());
			}
			return _response;
		}

		/// <summary>
        /// Elimina el dispositivo
        /// </summary>
        /// <returns></returns>
		public async Task<HttpResponseMessage> EliminarDispositivo()
        {
			Guid _dispositivoId = this.DispositivoId;
			return await _httpClient.PutAsJsonAsync("dispositivo/eliminar", this);
		}

		/// <summary>
        /// Actualiza el dispositivo
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ActualizaDispositivo()
        {
			HttpResponseMessage _response = await _httpClient.PutAsJsonAsync("dispositivo/actualizar", this);

			if (_response.StatusCode == HttpStatusCode.OK)
			{
				CargarObjetoActual(await _response.Content.ReadFromJsonAsync<Dispositivo>());
			}

			return _response;
		}

		private void CargarObjetoActual(SingleDispositivoViewModel singleDispositivoViewModel)
		{
			this.DispositivoId = singleDispositivoViewModel.DispositivoId;
			this.NumeroSerie = singleDispositivoViewModel.NumeroSerie;
			this.Marca = singleDispositivoViewModel.Marca;
			this.Modelo = singleDispositivoViewModel.Modelo;
			this.Licencia = singleDispositivoViewModel.Licencia;
			this.Situacion = singleDispositivoViewModel.Situacion;
			this.FechaInstalado = singleDispositivoViewModel.FechaInstalado;
			this.CompradoA = singleDispositivoViewModel.CompradoA;
			this.Observaciones = singleDispositivoViewModel.Observaciones;
			this.FuncionaBien = singleDispositivoViewModel.FuncionaBien;
			this.Utilizado = singleDispositivoViewModel.Utilizado;
			this.LlevaMantenimiento = singleDispositivoViewModel.LlevaMantenimiento;
			this.FechaCreado = singleDispositivoViewModel.FechaCreado;
			this.TipoDispositivoId = singleDispositivoViewModel.TipoDispositivoId;
			this.Usuarios = singleDispositivoViewModel.Usuarios;
			this.Estados = singleDispositivoViewModel.Estados;
			this.Departamento = singleDispositivoViewModel.Departamento;
			this.DepartamentoId = singleDispositivoViewModel.DepartamentoId;
			this.EmpresaExterna = singleDispositivoViewModel.EmpresaExterna;
			this.EmpresaExternaId = singleDispositivoViewModel.EmpresaExternaId;
		}

		public static implicit operator SingleDispositivoViewModel(Dispositivo dispositivo)
		{
			return new SingleDispositivoViewModel
			{
				DispositivoId = dispositivo.DispositivoId,
				NumeroSerie = dispositivo.NumeroSerie,
				Marca = dispositivo.Marca,
				Modelo = dispositivo.Modelo,
				Licencia = dispositivo.Licencia,
				Situacion = dispositivo.Situacion,
				FechaInstalado = dispositivo.FechaInstalado,
				CompradoA = dispositivo.CompradoA,
				Observaciones = dispositivo.Observaciones,
				FuncionaBien = dispositivo.FuncionaBien,
				Utilizado = dispositivo.Utilizado,
				LlevaMantenimiento = dispositivo.LlevaMantenimiento,
				FechaCreado = dispositivo.FechaCreado,
				Tipo = dispositivo.Tipo,
				TipoDispositivoId = dispositivo.TipoDispositivoId,
				Usuarios = dispositivo.Usuarios,
				Estados = dispositivo.Estados,
				Departamento = dispositivo.Departamento,
				DepartamentoId = dispositivo.DepartamentoId,
				EmpresaExterna = dispositivo.EmpresaExterna,
				EmpresaExternaId = dispositivo.EmpresaExternaId

		};
		}

		public static implicit operator Dispositivo(SingleDispositivoViewModel singleDispositivoViewModel)
		{
			return new Dispositivo
			{
				DispositivoId = singleDispositivoViewModel.DispositivoId,
				NumeroSerie = singleDispositivoViewModel.NumeroSerie,
				Marca = singleDispositivoViewModel.Marca,
				Modelo = singleDispositivoViewModel.Modelo,
				Licencia = singleDispositivoViewModel.Licencia,
				Situacion = singleDispositivoViewModel.Situacion,
				FechaInstalado = singleDispositivoViewModel.FechaInstalado,
				CompradoA = singleDispositivoViewModel.CompradoA,
				Observaciones = singleDispositivoViewModel.Observaciones,
				FuncionaBien = singleDispositivoViewModel.FuncionaBien,
				Utilizado = singleDispositivoViewModel.Utilizado,
				LlevaMantenimiento = singleDispositivoViewModel.LlevaMantenimiento,
				FechaCreado = singleDispositivoViewModel.FechaCreado,
				Tipo = singleDispositivoViewModel.Tipo,
				TipoDispositivoId = singleDispositivoViewModel.TipoDispositivoId,
				Usuarios = singleDispositivoViewModel.Usuarios,
				Estados = singleDispositivoViewModel.Estados,
				Departamento = singleDispositivoViewModel.Departamento,
				DepartamentoId = singleDispositivoViewModel.DepartamentoId,
				EmpresaExterna = singleDispositivoViewModel.EmpresaExterna,
				EmpresaExternaId = singleDispositivoViewModel.EmpresaExternaId

			};
		}
	}
}