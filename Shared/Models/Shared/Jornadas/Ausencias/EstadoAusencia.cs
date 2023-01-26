using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public enum EstadoAusenciaTipo
	{
		Solicitada,
		Recibida,
		Pendiente,
		Aceptada,
		Denegada
	}

	public class EstadoAusencia
	{
		public long EstadoAusenciaId { get; set; }
		public EstadoAusenciaTipo TipoEstadoAusencia { get; set; }
		public string EstadoPor { get; set; }
		public string EstadoPorNombreCompleto { get; set; }
		public DateTime FechaEstado { get; set; }
		public string Observaciones { get; set; }

		public long AusenciaId { get; set; }
		[JsonIgnore]
		public Ausencia Ausencia { get; set; }
	}
}