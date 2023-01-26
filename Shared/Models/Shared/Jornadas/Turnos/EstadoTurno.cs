using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public enum EstadoTurnoTipo
	{
		Aprobado,
		Denegado,
		Pendiente
	}

	public class EstadoTurno
	{
		public long EstadoTurnoId { get; set; }
		public EstadoTurnoTipo TipoEstadoTurno { get; set; }
		public string EstadoPor { get; set; }
		public string EstadoPorNombreCompleto { get; set; }
		public DateTime FechaEstado { get; set; }
		public string Observaciones { get; set; }

		public long TurnoId { get; set; }
		[JsonIgnore]
		public Turno Turno { get; set; }
	}
}