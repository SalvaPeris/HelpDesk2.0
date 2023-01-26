using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;

namespace HelpDesk.Server.Repository
{
	public interface IJornadaRepository : IDisposable
	{
		Task<ICollection<Jornada>> GetJornadasPorIdUsuario(Guid usuarioId, DateTime fechaComienzo, DateTime fechaFin);
		Task<ICollection<Jornada>> GetJornadasPorDepartamento(long departamentoId);

		Task<Jornada> GetJornadaPorId(long JornadaId);

		Task<int> NuevoFichaje(Jornada jornada, Guid usuarioId);
		Task<int> FinalizaFichaje(Jornada jornada);
		Task<int> ActualizarFichaje(Jornada jornada);
		Task<int> EliminarFichaje(Fichaje fichaje);
		Task<int> Guardar();
	}
}

