using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class Departamento
	{
		public long DepartamentoId { get; set; }
		public string Nombre { get; set; }
		public string Iniciales { get; set; }
		public bool TieneTickets { get; set; }
		[JsonIgnore]
		public ICollection<Usuario> Usuarios { get; set; }
	}
}

