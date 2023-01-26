#nullable enable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class Turno
	{
		public long TurnoId { get; set; }
		public DateTime? HoraComienzo { get; set; }
		public DateTime? HoraFin { get; set; }
		public string? Observaciones { get; set; }
		public ICollection<EstadoTurno>? Estados { get; set; }

		[JsonIgnore]
		public virtual ICollection<JornadaTurno>? Jornadas { get; set; }
	}
}