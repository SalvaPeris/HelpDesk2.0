#nullable enable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class Ausencia
	{
		public long AusenciaId { get; set; }
		public DateTime? FechaComienzo { get; set; }
		public DateTime? FechaFin { get; set; }
		public string? Observaciones { get; set; }
		public ICollection<EstadoAusencia>? Estados { get; set; }

		[JsonIgnore]
		public virtual ICollection<JornadaAusencia>? Jornadas { get; set; }

		public TipoAusencia? TipoAusencia { get; set; }
		public long TipoAusenciaId { get; set; }
		public string? TipoAusenciaNombre { get; set; }
	}
}
