using System;
using System.Collections.Generic;

namespace HelpDesk.Shared.Models
{
	public class GruposChat
	{
		//Se añade el int que es la cuenta de mensajes no leídos
		public ICollection<UsuarioChat> Usuarios { get; set; }

		//Se añade el int que es la cuenta de mensajes no leídos
		public ICollection<GrupoChat> Grupos { get; set; }
	}
}

