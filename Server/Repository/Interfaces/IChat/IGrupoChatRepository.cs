using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;

namespace HelpDesk.Server.Repository
{
	public interface IGrupoChatRepository
	{
		Task<GrupoChat> GetGrupoChatPorId(Guid grupoId);
		Task<ICollection<GrupoChat>> GetGruposChat(Guid usuarioId);
		Task<GrupoChat> GetGrupoChatPorUsuarios(ICollection<GrupoChatUsuario> usuarios);

		Task<ICollection<GrupoChatUsuario>> GetUsuariosGrupoChatPorId(Guid grupoId);

		Task<int> Nuevo(GrupoChat grupoChat);
		Task<int> Actualizar(GrupoChat grupoChat);
		Task<int> Eliminar(GrupoChat grupoChat);
		Task<int> Guardar();
	}
}

