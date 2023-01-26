using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Server.DB;
using HelpDesk.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Server.Repository
{
    public class ArchivoRepository : IArchivoRepository
    {
        private readonly HelpDeskContext _context;

        public ArchivoRepository(HelpDeskContext helpdeskContext)
        {
            this._context = helpdeskContext;
        }

        public void Dispose()
        {
            //No hace falta, DBContext ya maneja las conexiones.
            throw new NotImplementedException();
        }
        /*
        public async Task<int> EliminarFichaje(Fichaje fichaje)
        {
            _context.Fichajes.Remove(fichaje);
            return await Guardar();
        }

        public async Task<ICollection<Jornada>> GetJornadasPorDepartamento(long departamentoId)
        {
            return await _context.Jornadas
                 .Include(j => j.Fichajes.OrderBy(f => f.HoraEntrada))
                 .Include(j => j.Ausencias).ThenInclude(row => row.Ausencia.TipoAusencia)
                 .Select(j => new Jornada()
                 {
                     JornadaId = j.JornadaId,
                     FechaJornada = j.FechaJornada,
                     Fichajes = j.Fichajes,
                     Ausencias = j.Ausencias,
                     UsuarioNombreCompleto = j.Usuario.Nombre + " " + j.Usuario.Apellidos,
                     ImagenUsuario = j.Usuario.FotoPerfil,
                     UsuarioId = j.UsuarioId
                 })
                 .OrderBy(j => j.JornadaId)
                 .ToListAsync();
        }
        */

        public async Task<ICollection<Archivo>> GetArchivosPorIdUsuario(string identificador)
        {
            var usuarioId = await _context.Usuarios.Where(u => u.Identificador == identificador).Select(u => u.UsuarioId).FirstOrDefaultAsync();

            return await _context.Archivos
                .Include(a => a.Usuarios).ThenInclude(row => row.UsuarioId)
                .ToListAsync();
        }

        /*
        public async Task<Jornada> GetJornadaPorId(long jornadaId)
        {
            return await _context.Jornadas
                  .Include(d => d.Ausencias).ThenInclude(row => row.Ausencia).FirstOrDefaultAsync(d => d.JornadaId == jornadaId);
        }

        public async Task<Fichaje> GetFichajePorId(long fichajeId)
        {
            return await _context.Fichajes.Where(f => f.FichajeId == fichajeId).FirstOrDefaultAsync();
        }

        public async Task<int> Guardar()
        {
            return await _context.SaveChangesAsync();
        }
        
        public async Task<int> NuevoFichaje(Jornada jornada, string identificador)
        {
            var usuarioId = await _context.Usuarios.Where(u => u.Identificador == identificador).Select(u => u.UsuarioId).FirstOrDefaultAsync();
            DateTime? FechaJornadaUnDiaMas = jornada.FechaJornada.AddDays(1);
            bool Existe = await _context.Jornadas.AnyAsync(j => j.FechaJornada >= jornada.FechaJornada && j.FechaJornada <= FechaJornadaUnDiaMas && j.UsuarioId == usuarioId);

            if (!Existe)
            {
                jornada.UsuarioId = usuarioId;
                _context.Jornadas.Add(jornada);
            }
            else
            {
                _context.Jornadas.Update(jornada);
            }
            return await Guardar();
        }

        public async Task<int> FinalizaFichaje(Jornada jornada)
        {
            _context.Jornadas.Update(jornada);
            return await Guardar();
        }

        public async Task<int> ActualizarFichaje(Jornada jornada)
        {
            _context.Jornadas.Update(jornada);
            return await Guardar();
        }
        */
    }
}