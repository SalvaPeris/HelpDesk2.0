using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class TipoAusencia
	{
		public long TipoAusenciaId { get; set; }
		public string NombreAusencia { get; set; }
		public string Observaciones { get; set; }

		[JsonIgnore]
		public ICollection<Ausencia> Ausencias { get; set; }
	}
}

