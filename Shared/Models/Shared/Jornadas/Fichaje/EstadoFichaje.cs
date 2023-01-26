using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public enum EstadoFichajeTipo
	{
		Aprobado,
		Denegado,
		Pendiente
	}

	public class EstadoFichaje
	{
		public long EstadoFichajeId { get; set; }
		public EstadoFichajeTipo TipoEstadoFichaje { get; set; }
		public string EstadoPor { get; set; }
		public string EstadoPorNombreCompleto { get; set; }
		public DateTime FechaEstado { get; set; }
		public string Observaciones { get; set; }

		public long FichajeId { get; set; }
		[JsonIgnore]
		public Fichaje Fichaje { get; set; }
	}
}