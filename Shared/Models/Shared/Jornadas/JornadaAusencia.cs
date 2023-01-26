using System;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class JornadaAusencia
	{
		public long JornadaId { get; set; }
		[JsonIgnore]
		public Jornada Jornada { get; set; }

		public long AusenciaId { get; set; }
		public Ausencia Ausencia { get; set; }
	}
}