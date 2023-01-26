using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;

namespace HelpDesk.Server.Repository
{
	public interface IDispositivoRepository : IDisposable
	{
		Task<ICollection<Dispositivo>> GetDispositivosPorIdUsuario(Guid usuarioId);
		Task<ICollection<Dispositivo>> GetDispositivosPorDepartamento(long departamentoId);
		Task<ICollection<Dispositivo>> GetDispositivos();

		Task<Dispositivo> GetDispositivoPorId(Guid dispositivoId);

		Task<int> Nuevo(Dispositivo dispositivo);
		Task<int> Actualizar(Dispositivo dispositivo);
		Task<int> Eliminar(Dispositivo dispositivo);
		Task<int> Guardar();
	}
}

