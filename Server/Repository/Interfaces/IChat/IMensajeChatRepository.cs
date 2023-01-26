using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;

namespace HelpDesk.Server.Repository
{
	public interface IMensajeChatRepository : IDisposable
	{
		Task<ICollection<MensajeChat>> GetLastMensajesChatPorIdGrupo(Guid grupoId);

		Task<int> Nuevo(MensajeChat mensajeChat, Usuario usuario);
		Task<int> Actualizar(MensajeChat mensajeChat);
		Task<int> Eliminar(MensajeChat mensajeChat);
		Task<int> Guardar();
	}
}