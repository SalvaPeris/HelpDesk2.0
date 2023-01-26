using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelpDesk.Shared.Models
{
	public class Jornada
	{
		public long JornadaId { get; set; }
		public DateTime FechaJornada { get; set; }

		[JsonIgnore]
		public Usuario Usuario { get; set; }
		public Guid UsuarioId { get; set; }
		public string UsuarioNombreCompleto { get; set; }
		public string ImagenUsuario { get; set; }

		public virtual ICollection<Fichaje> Fichajes { get; set; }

		public int AusenciaCuenta { get; set; }
		public virtual ICollection<JornadaAusencia> Ausencias { get; set; }

		public virtual ICollection<JornadaTurno> Turnos { get; set; }
	}
}