using System;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;

namespace HelpDesk.Server.Repository
{
	public interface IUsuarioChatRepository : IDisposable
	{
		Task<int> Conectar(Guid usuarioId);
		Task<int> Desconectar(Guid usuarioId);

		Task<int> Guardar();
	}
}

