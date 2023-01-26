using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;

namespace HelpDesk.Server.Repository
{
	public interface IUsuarioRepository : IDisposable
	{
		Task<Usuario> GetUsuario(Guid usuarioId);
		Task<Usuario> GetUsuarioPorDNI(string identificador);
		Task<Usuario> GetUsuarioChatPorDNI(string identificador);
		Task<Usuario> GetUsuarioPorId(Guid usuarioId);
		Task<Usuario> GetUsuarioChatPorId(Guid usuarioId);
		Task<Usuario> GetUsuarioLogin(string identificador, string contrasena);

		Task<ICollection<Usuario>> GetUsuariosPorDepartamento(string departamento);
		Task<ICollection<Usuario>> GetUsuarios();

		Task<int> Nuevo(Usuario usuario);
		Task<int> Eliminar(Usuario usuario);
		Task<int> Guardar();
	}
}

