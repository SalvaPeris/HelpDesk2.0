using System;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;

namespace HelpDesk.ViewModels
{
	public interface IAgendaViewModel
	{
		public Usuario[] Usuarios { get; set; }

		public Task GetUsuarios();
		public Task BuscarUsuario(string busqueda);
		public Task BuscarUsuarioPorLetra(string busqueda);

	}
}

