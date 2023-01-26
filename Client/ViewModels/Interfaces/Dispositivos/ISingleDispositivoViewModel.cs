using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.ViewModels
{
	public interface ISingleDispositivoViewModel
	{
        public Guid DispositivoId { get; set; }
        [Required(ErrorMessage = "El número de serie es necesario")]
        public string NumeroSerie { get; set; }
        public long TipoDispositivoId { get; set; }
        public TipoDispositivo Tipo { get; set; }
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
        public ICollection<DispositivoUsuario> Usuarios { get; set; }
        public ICollection<EstadoDispositivo> Estados { get; set; }
        public Departamento Departamento { get; set; }
        public long DepartamentoId { get; set; }
        public string DepartamentoNombre { get; set; }
        public EmpresaExterna EmpresaExterna { get; set; }
        public long EmpresaExternaId { get; set; }
        public string EmpresaExternaNombre { get; set; }

        public string Mensaje { get; set; }
        public NotificationSeverity NotificacionSeveridad { get; set; }

        public Dispositivo GetSingleDispositivo();

        public Task<HttpResponseMessage> GetDispositivoPorId(string identificador);

        public Task<HttpResponseMessage> NuevoDispositivo();
        public Task<HttpResponseMessage> EliminarDispositivo();
        public Task<HttpResponseMessage> ActualizaDispositivo();
        public void Anular();
	}
}