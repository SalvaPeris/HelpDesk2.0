using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Server.DB;
using HelpDesk.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Server.Repository
{
	public class DispositivoRepository : IDispositivoRepository
	{
        private readonly HelpDeskContext _context;

		public DispositivoRepository(HelpDeskContext helpdeskContext)
		{
            this._context = helpdeskContext;
		}

        public void Dispose()
        {
            //No hace falta, DBContext ya maneja las conexiones.
            throw new NotImplementedException();
        }

        public async Task<int> Eliminar(Dispositivo dispositivo)
        {
            _context.Dispositivos.Remove(dispositivo);
            return await Guardar();
        }

        public async Task<Dispositivo> GetDispositivoPorId(Guid dispositivoId)
        {
            return await _context.Dispositivos
                .Include(d => d.Usuarios).ThenInclude(row => row.Usuario).FirstOrDefaultAsync(d => d.DispositivoId == dispositivoId);
        }

        public async Task<ICollection<Dispositivo>> GetDispositivosPorDepartamento(long departamentoId)
        {
            return await _context.Dispositivos
                .Include(d => d.Tipo)
                .ToListAsync();
        }

        public async Task<ICollection<Dispositivo>> GetDispositivos()
        {
            return await _context.Dispositivos
                .Select(d => new Dispositivo()
                {
                    DispositivoId = d.DispositivoId,
                    Tipo = d.Tipo,
                    Utilizado = d.Utilizado,
                    NumeroSerie = d.NumeroSerie,
                    Marca = d.Marca,
                    Modelo = d.Modelo,
                    FechaCreado = d.FechaCreado,
                    Observaciones = d.Observaciones,
                    Situacion = d.Situacion,
                    UsuariosCuenta = d.Usuarios.Count,
                    DepartamentoNombre = d.Departamento.Nombre,
                    EmpresaExternaNombre = d.EmpresaExterna.Nombre
                })
                .OrderBy(d => d.DispositivoId)
                .ToListAsync();
        }

        public async Task<ICollection<Dispositivo>> GetDispositivosPorIdUsuario(Guid usuarioId)
        {
            return await _context.Dispositivos
                .Where(d => d.Usuarios.Any(u => u.UsuarioId == usuarioId))
                .Select(d => new Dispositivo()
                {
                    DispositivoId = d.DispositivoId,
                    Tipo = d.Tipo,
                    Utilizado = d.Utilizado,
                    NumeroSerie = d.NumeroSerie,
                    Marca = d.Marca,
                    Modelo = d.Modelo,
                    FechaCreado = d.FechaCreado,
                    Observaciones = d.Observaciones,
                    Situacion = d.Situacion,
                    UsuariosCuenta = d.Usuarios.Count,
                    DepartamentoNombre = d.Departamento.Nombre,
                    EmpresaExternaNombre = d.EmpresaExterna.Nombre
                })
                .OrderBy(d => d.DispositivoId)
                .ToListAsync();
        }

        public async Task<int> Guardar()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Nuevo(Dispositivo dispositivo)
        {
            _context.Dispositivos.Add(dispositivo);
            return await Guardar();
        }

        public async Task<int> Actualizar(Dispositivo dispositivo)
        {
            _context.Dispositivos.Update(dispositivo);
            return await Guardar();
        }
    }
}

