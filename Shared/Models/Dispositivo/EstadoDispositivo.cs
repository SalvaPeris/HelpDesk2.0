using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class EstadoDispositivo
	{
		public Guid EstadoDispositivoId { get; set; }
		public string Estado { get; set; }
		public string EstadoPor { get; set; }
		public string EstadoPorNombreCompleto { get; set; }
		public DateTime FechaEstado { get; set; }
		public string Observaciones { get; set; }
		public Guid DispositivoId { get; set; }

		[JsonIgnore]
		public Dispositivo Dispositivo { get; set; }
	}
}
