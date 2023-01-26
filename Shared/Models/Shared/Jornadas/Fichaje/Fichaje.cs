#nullable enable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class Fichaje
	{
		public long FichajeId { get; set; }
		public DateTime? HoraEntrada { get; set; }
		public DateTime? HoraSalida { get; set; }
		public string? FuenteEntrada { get; set; }
		public string? FuenteSalida { get; set; }
		public Localizacion? LocalizacionEntrada { get; set; }
		public Localizacion? LocalizacionSalida { get; set; }
        public string? Observaciones { get; set; }
		public ICollection<EstadoFichaje>? Estados { get; set; }

		[JsonIgnore]
		public Jornada? Jornada { get; set; }
		public long JornadaId { get; set; }
	}
}
