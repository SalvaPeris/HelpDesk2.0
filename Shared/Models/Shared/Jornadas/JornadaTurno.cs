using System;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class JornadaTurno
	{
		public long JornadaId { get; set; }
		[JsonIgnore]
		public Jornada Jornada { get; set; }

		public long TurnoId { get; set; }
		public Turno Turno { get; set; }
	}
}