using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HelpDesk.Server.DB;
using HelpDesk.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace HelpDesk.Server.Repository
{
	public class TicketRepository : ITicketRepository
	{
		private readonly HelpDeskContext _context; 

		public TicketRepository(HelpDeskContext helpDeskContext)
		{
            this._context = helpDeskContext;
		}

        public Task<int> Actualizar(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //DBContext maneja las conexiones por tí, por tanto, no hace falta hacer el dispose.
            throw new NotImplementedException();
        }

        public async Task<int> Eliminar(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
            return await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Ticket>> GetTickets(int Skip, int Top)
        {
            return await _context.Tickets
                .OrderByDescending(t => t.FechaCreado)
                .Skip(Skip).Take(Top)
                .Select(t => new Ticket() {
                    TicketId = t.TicketId,
                    Area = t.Area,
                    Titulo = t.Titulo,
                    Observaciones = t.Observaciones,
                    Estado = t.Estado,
                    Gravedad = t.Gravedad,
                    FechaCreado = t.FechaCreado,
                    AsignadoANombreCompleto = t.AsignadoANombreCompleto,
                    SuplementosCuenta = t.Suplementos.Count,
                    ZonasCuenta = t.Zonas.Count,
                    EstadosCuenta = t.Estados.Count
                })
                .ToListAsync();
        }

        public async Task<ICollection<Ticket>> GetTicketsSuperAdministrador(int Skip, int Top)
        {
            return await _context.Tickets
                .OrderByDescending(t => t.FechaCreado)
                .Skip(Skip).Take(Top)
                .Select(t => new Ticket()
                {
                    TicketId = t.TicketId,
                    Area = t.Area,
                    Titulo = t.Titulo,
                    Observaciones = t.Observaciones,
                    Estado = t.Estado,
                    Gravedad = t.Gravedad,
                    FechaCreado = t.FechaCreado,
                    AsignadoANombreCompleto = t.AsignadoANombreCompleto,
                    SuplementosCuenta = t.Suplementos.Count,
                    ZonasCuenta = t.Zonas.Count,
                    EstadosCuenta = t.Estados.Count
                })
                .ToListAsync();
        }

        public async Task<ICollection<Ticket>> GetTicketsAdministrador(string Departamento, int Skip, int Top)
        {
            return await _context.Tickets
                    .Where(t => t.Area == Departamento)
                    .OrderByDescending(t => t.FechaCreado)
                    .Skip(Skip).Take(Top)
                    .Select(t => new Ticket()
                    {
                        TicketId = t.TicketId,
                        Area = t.Area,
                        Titulo = t.Titulo,
                        Observaciones = t.Observaciones,
                        Estado = t.Estado,
                        Gravedad = t.Gravedad,
                        FechaCreado = t.FechaCreado,
                        AsignadoANombreCompleto = t.AsignadoANombreCompleto,
                        SuplementosCuenta = t.Suplementos.Count,
                        ZonasCuenta = t.Zonas.Count,
                        EstadosCuenta = t.Estados.Count
                    })
                    .ToListAsync();
        }

        public async Task<ICollection<Ticket>> GetTicketsPropios(string Usuario, int Skip, int Top)
        {
            return await _context.Tickets
                    .Where(t => t.CreadoPor == Usuario)
                    .OrderByDescending(t => t.FechaCreado)
                    .Skip(Skip).Take(Top)
                    .Select(t => new Ticket()
                    {
                        TicketId = t.TicketId,
                        Area = t.Area,
                        Titulo = t.Titulo,
                        Observaciones = t.Observaciones,
                        Estado = t.Estado,
                        Gravedad = t.Gravedad,
                        FechaCreado = t.FechaCreado,
                        AsignadoANombreCompleto = t.AsignadoANombreCompleto,
                        SuplementosCuenta = t.Suplementos.Count,
                        ZonasCuenta = t.Zonas.Count,
                        EstadosCuenta = t.Estados.Count
                    })
                    .ToListAsync();
        }

        public async Task<ICollection<Ticket>> GetTicketsAsignados(string Usuario, int Skip, int Top)
        {
            return await _context.Tickets
                    .Where(t => t.AsignadoA == Usuario)
                    .OrderByDescending(t => t.FechaCreado)
                    .Skip(Skip).Take(Top)
                    .Select(t => new Ticket()
                    {
                        TicketId = t.TicketId,
                        Area = t.Area,
                        Titulo = t.Titulo,
                        Observaciones = t.Observaciones,
                        Estado = t.Estado,
                        Gravedad = t.Gravedad,
                        FechaCreado = t.FechaCreado,
                        AsignadoANombreCompleto = t.AsignadoANombreCompleto,
                        SuplementosCuenta = t.Suplementos.Count,
                        ZonasCuenta = t.Zonas.Count,
                        EstadosCuenta = t.Estados.Count
                    })
                    .ToListAsync();
        }

        public async Task<int> CargarTotalTicketsSuperAdministrador(string Estado)
        {
            return await _context.Tickets
                    .Where(t => t.Estado == Estado)
                    .CountAsync();
        }

        public async Task<int> CargarTotalTicketsPropiosSuperAdministrador(string Estado, string Usuario)
        {
            return await _context.Tickets
                    .Where(t => t.Estado == Estado)
                    .Where(t => t.CreadoPor == Usuario)
                    .CountAsync();
        }

        public async Task<int> CargarTotalTicketsAsignadosSuperAdministrador(string Estado, string Usuario)
        {
            return await _context.Tickets
                    .Where(t => t.Estado == Estado)
                    .Where(t => t.AsignadoANombreCompleto == Usuario)
                    .CountAsync();
        }

        public async Task<int> CargarTotalTicketsSuperAdministrador()
        {
            return await _context.Tickets
                    .CountAsync();
        }

        public async Task<int> CargarTotalTicketsPropiosSuperAdministrador(string Usuario)
        {
            return await _context.Tickets
                    .Where(t => t.CreadoPor == Usuario)
                    .CountAsync();
        }

        public async Task<int> CargarTotalTicketsAsignadosSuperAdministrador(string Usuario)
        {
            return await _context.Tickets
                    .Where(t => t.AsignadoANombreCompleto == Usuario)
                    .CountAsync();
        }

        public async Task<int> CargarTotalTicketsAdministrador(string Estado, string Departamento)
        {
            return await _context.Tickets
                    .Where(t => t.Area == Departamento)
                    .Where(t => t.Estado == Estado)
                    .CountAsync();
        }

        public async Task<int> CargarTotalTicketsPropiosAdministrador(string Estado, string Usuario)
        {
            return await _context.Tickets
                    .Where(t => t.CreadoPor == Usuario)
                    .Where(t => t.Estado == Estado)
                    .CountAsync();
        }

        public async Task<int> CargarTotalTicketsAsignadosAdministrador(string Estado, string Usuario)
        {
            return await _context.Tickets
                    .Where(t => t.AsignadoANombreCompleto == Usuario)
                    .Where(t => t.Estado == Estado)
                    .CountAsync();
        }

        public async Task<int> CargarTotalTicketsAdministrador(string Departamento)
        {
            return await _context.Tickets
                    .Where(t => t.Area == Departamento)
                    .CountAsync();
        }

        public async Task<int> CargarTotalTicketsPropiosAdministrador(string Usuario)
        {
            return await _context.Tickets
                    .Where(t => t.CreadoPor == Usuario)
                    .CountAsync();
        }

        public async Task<int> CargarTotalTicketsAsignadosAdministrador(string Usuario)
        {
            return await _context.Tickets
                    .Where(t => t.AsignadoANombreCompleto == Usuario)
                    .CountAsync();
        }

        public async Task<int> CargarTotalTicketsUsuario(string Estado, string Departamento)
        {
            return await _context.Tickets
                    .Where(t => t.Publico == true)
                    .Where(t => t.Area == Departamento)
                    .Where(t => t.Estado == Estado)                  
                    .CountAsync();
        }

        public async Task<int> CargarTotalTicketsPropiosUsuario(string Estado, string Usuario)
        {
            return await _context.Tickets
                    .Where(t => t.CreadoPor == Usuario)
                    .Where(t => t.Estado == Estado)
                    .CountAsync();
        }

        public async Task<int> CargarTotalTicketsAsignadosUsuario(string Estado, string Usuario)
        {
            return await _context.Tickets
                    .Where(t => t.AsignadoANombreCompleto == Usuario)
                    .Where(t => t.Estado == Estado)
                    .CountAsync();
        }

        public async Task<int> CargarTotalTicketsUsuario(string Departamento)
        {
            return await _context.Tickets
                    .Where(t => t.Publico == true)
                    .Where(t => t.Area == Departamento)
                    .CountAsync();
        }

        public async Task<int> CargarTotalTicketsPropiosUsuario(string Usuario)
        {
            return await _context.Tickets
                    .Where(t => t.CreadoPor == Usuario)
                    .CountAsync();
        }

        public async Task<int> CargarTotalTicketsAsignadosUsuario(string Usuario)
        {
            return await _context.Tickets
                    .Where(t => t.AsignadoANombreCompleto == Usuario)
                    .CountAsync();
        }

        public async Task<ICollection<Ticket>> GetTicketsDepartamentoPorEstado(string Estado, string Departamento, int Skip, int Top)
        {
            return await _context.Tickets
                .Where(t => t.Area == Departamento)
                .Where(t => t.Estado == Estado)
                .OrderByDescending(t => t.FechaCreado)
                .Skip(Skip).Take(Top)
                .Select(t => new Ticket()
                {
                    TicketId = t.TicketId,
                    Area = t.Area,
                    Titulo = t.Titulo,
                    Observaciones = t.Observaciones,
                    Estado = t.Estado,
                    Gravedad = t.Gravedad,
                    FechaCreado = t.FechaCreado,
                    AsignadoANombreCompleto = t.AsignadoANombreCompleto,
                    SuplementosCuenta = t.Suplementos.Count,
                    ZonasCuenta = t.Zonas.Count,
                    EstadosCuenta = t.Estados.Count
                })
                .ToListAsync();
        }

        public async Task<ICollection<Ticket>> GetTicketsPorEstadoSuperAdministrador(string Estado, int Skip, int Top)
        {
            return await _context.Tickets
                .Where(t => t.Estado == Estado)
                .OrderByDescending(t => t.FechaCreado)
                .Skip(Skip).Take(Top)
                .Select(t => new Ticket()
                {
                    TicketId = t.TicketId,
                    Area = t.Area,
                    Titulo = t.Titulo,
                    Observaciones = t.Observaciones,
                    Estado = t.Estado,
                    Gravedad = t.Gravedad,
                    FechaCreado = t.FechaCreado,
                    AsignadoANombreCompleto = t.AsignadoANombreCompleto,
                    SuplementosCuenta = t.Suplementos.Count,
                    ZonasCuenta = t.Zonas.Count,
                    EstadosCuenta = t.Estados.Count
                })
                .ToListAsync();
        }

        public async Task<ICollection<Ticket>> GetTicketsPropiosPorEstado(string Estado, string Usuario, int Skip, int Top)
        {
            return await _context.Tickets
                .Where(t => t.CreadoPor == Usuario)
                .Where(t => t.Estado == Estado)
                .OrderByDescending(t => t.FechaCreado)
                .Skip(Skip).Take(Top)
                .Select(t => new Ticket()
                {
                    TicketId = t.TicketId,
                    Area = t.Area,
                    Titulo = t.Titulo,
                    Observaciones = t.Observaciones,
                    Estado = t.Estado,
                    Gravedad = t.Gravedad,
                    FechaCreado = t.FechaCreado,
                    AsignadoANombreCompleto = t.AsignadoANombreCompleto,
                    SuplementosCuenta = t.Suplementos.Count,
                    ZonasCuenta = t.Zonas.Count,
                    EstadosCuenta = t.Estados.Count
                })
                .ToListAsync();
        }

        public async Task<ICollection<Ticket>> GetTicketsAsignadosPorEstado(string Estado, string Usuario, int Skip, int Top)
        {
            return await _context.Tickets
                .Where(t => t.AsignadoANombreCompleto == Usuario)
                .Where(t => t.Estado == Estado)
                .OrderByDescending(t => t.FechaCreado)
                .Skip(Skip).Take(Top)
                .Select(t => new Ticket()
                {
                    TicketId = t.TicketId,
                    Area = t.Area,
                    Titulo = t.Titulo,
                    Observaciones = t.Observaciones,
                    Estado = t.Estado,
                    Gravedad = t.Gravedad,
                    FechaCreado = t.FechaCreado,
                    AsignadoANombreCompleto = t.AsignadoANombreCompleto,
                    SuplementosCuenta = t.Suplementos.Count,
                    ZonasCuenta = t.Zonas.Count,
                    EstadosCuenta = t.Estados.Count
                })
                .ToListAsync();
        }

        public async Task<ICollection<Ticket>> GetTicketsPublicosDepartamentoPorEstado(string Estado, string Departamento, int Skip, int Top)
        {
            return await _context.Tickets
                .Where(t => t.Area == Departamento)
                .Where(t => t.Publico == true)
                .Where(t => t.Estado == Estado)
                .OrderByDescending(t => t.FechaCreado)
                .Select(t => new Ticket()
                {
                    TicketId = t.TicketId,
                    Area = t.Area,
                    Titulo = t.Titulo,
                    Observaciones = t.Observaciones,
                    Estado = t.Estado,
                    Gravedad = t.Gravedad,
                    FechaCreado = t.FechaCreado,
                    AsignadoANombreCompleto = t.AsignadoANombreCompleto,
                    SuplementosCuenta = t.Suplementos.Count,
                    ZonasCuenta = t.Zonas.Count,
                    EstadosCuenta = t.Estados.Count
                })
                .ToListAsync();
        }

        public async Task<int> Guardar()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Nuevo(Ticket ticket)
        {
            ticket.FechaCreado = DateTime.Now;
            _context.Tickets.Add(ticket);
            return await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Ticket>> GetTicketsPublicosPorDepartamento(string Departamento, int Skip, int Top)
        {
            return await _context.Tickets
                    .Where(t => t.Area == Departamento)
                    .Where(t => t.Publico == true)
                    .OrderByDescending(t => t.FechaCreado)
                    .Skip(Skip).Take(Top)
                    .Select(t => new Ticket()
                    {
                        TicketId = t.TicketId,
                        Area = t.Area,
                        Titulo = t.Titulo,
                        Observaciones = t.Observaciones,
                        Estado = t.Estado,
                        Gravedad = t.Gravedad,
                        FechaCreado = t.FechaCreado,
                        AsignadoANombreCompleto = t.AsignadoANombreCompleto,
                        SuplementosCuenta = t.Suplementos.Count,
                        ZonasCuenta = t.Zonas.Count,
                        EstadosCuenta = t.Estados.Count
                    })
                    .ToListAsync();
        }

        public async Task<ICollection<Ticket>> GetTicketsPorBusquedaSuperAdministrador(string Busqueda)
        {
            return await _context.Tickets
                    .Where(t => t.TicketId.ToString().Contains(Busqueda) ||
                    t.CreadoPorNombreCompleto.Contains(Busqueda) ||
                    t.Titulo.Contains(Busqueda))
                    .OrderByDescending(t => t.FechaCreado)
                    .Select(t => new Ticket()
                    {
                        TicketId = t.TicketId,
                        Area = t.Area,
                        Titulo = t.Titulo,
                        Observaciones = t.Observaciones,
                        Estado = t.Estado,
                        Gravedad = t.Gravedad,
                        FechaCreado = t.FechaCreado,
                        AsignadoANombreCompleto = t.AsignadoANombreCompleto,
                        SuplementosCuenta = t.Suplementos.Count,
                        ZonasCuenta = t.Zonas.Count,
                        EstadosCuenta = t.Estados.Count
                    })
                    .Take(10)
                    .ToListAsync();
        }

        public async Task<ICollection<Ticket>> GetTicketsPrivadosPorBusquedaAdministrador(string Usuario, string Busqueda)
        {
            return await _context.Tickets
                    .Where(t => t.CreadoPor == Usuario)
                    .Where(t => t.TicketId.ToString().Contains(Busqueda) ||
                    t.CreadoPorNombreCompleto.Contains(Busqueda) ||
                    t.Titulo.Contains(Busqueda))
                    .OrderByDescending(t => t.FechaCreado)
                    .Select(t => new Ticket()
                    {
                        TicketId = t.TicketId,
                        Area = t.Area,
                        Titulo = t.Titulo,
                        Observaciones = t.Observaciones,
                        Estado = t.Estado,
                        Gravedad = t.Gravedad,
                        FechaCreado = t.FechaCreado,
                        AsignadoANombreCompleto = t.AsignadoANombreCompleto,
                        SuplementosCuenta = t.Suplementos.Count,
                        ZonasCuenta = t.Zonas.Count,
                        EstadosCuenta = t.Estados.Count
                    })
                    .Take(10)
                    .ToListAsync();
        }

        public async Task<ICollection<Ticket>> GetTicketsPropiosPorBusqueda(string Usuario, string Busqueda)
        {
            return await _context.Tickets
                    .Where(t => t.CreadoPor == Usuario)
                    .Where(t => t.TicketId.ToString().Contains(Busqueda) ||
                    t.CreadoPorNombreCompleto.Contains(Busqueda) ||
                    t.Titulo.Contains(Busqueda))
                    .OrderByDescending(t => t.FechaCreado)
                    .Select(t => new Ticket()
                    {
                        TicketId = t.TicketId,
                        Area = t.Area,
                        Titulo = t.Titulo,
                        Observaciones = t.Observaciones,
                        Estado = t.Estado,
                        Gravedad = t.Gravedad,
                        FechaCreado = t.FechaCreado,
                        AsignadoANombreCompleto = t.AsignadoANombreCompleto,
                        SuplementosCuenta = t.Suplementos.Count,
                        ZonasCuenta = t.Zonas.Count,
                        EstadosCuenta = t.Estados.Count
                    })
                    .Take(10)
                    .ToListAsync();
        }

        public async Task<ICollection<Ticket>> GetTicketsAsignadosPorBusqueda(string Usuario, string Busqueda)
        {
            return await _context.Tickets
                    .Where(t => t.AsignadoANombreCompleto == Usuario)
                    .Where(t => t.TicketId.ToString().Contains(Busqueda) ||
                    t.CreadoPorNombreCompleto.Contains(Busqueda) ||
                    t.Titulo.Contains(Busqueda))
                    .OrderByDescending(t => t.FechaCreado)
                    .Select(t => new Ticket()
                    {
                        TicketId = t.TicketId,
                        Area = t.Area,
                        Titulo = t.Titulo,
                        Observaciones = t.Observaciones,
                        Estado = t.Estado,
                        Gravedad = t.Gravedad,
                        FechaCreado = t.FechaCreado,
                        AsignadoANombreCompleto = t.AsignadoANombreCompleto,
                        SuplementosCuenta = t.Suplementos.Count,
                        ZonasCuenta = t.Zonas.Count,
                        EstadosCuenta = t.Estados.Count
                    })
                    .Take(10)
                    .ToListAsync();
        }

        public async Task<ICollection<Ticket>> GetTicketsDepartamentoPorBusquedaAdministrador(string Departamento, string Busqueda)
        {
            return await _context.Tickets
                     .Where(t => t.Area == Departamento)
                     .Where(t => t.TicketId.ToString().Contains(Busqueda) ||
                         t.CreadoPorNombreCompleto.Contains(Busqueda) ||
                         t.Titulo.Contains(Busqueda))
                     .OrderByDescending(t => t.FechaCreado)
                     .Select(t => new Ticket()
                     {
                         TicketId = t.TicketId,
                         Area = t.Area,
                         Titulo = t.Titulo,
                         Observaciones = t.Observaciones,
                         Estado = t.Estado,
                         Gravedad = t.Gravedad,
                         FechaCreado = t.FechaCreado,
                         AsignadoANombreCompleto = t.AsignadoANombreCompleto,
                         SuplementosCuenta = t.Suplementos.Count,
                         ZonasCuenta = t.Zonas.Count,
                         EstadosCuenta = t.Estados.Count
                     })
                     .Take(10)
                     .ToListAsync();
        }

        public async Task<ICollection<Ticket>> GetTicketsDepartamentoPorBusquedaUsuario(string Departamento, string Busqueda)
        {
            return await _context.Tickets
                     .Where(t => t.Area == Departamento)
                     .Where(t => t.Publico == true)
                     .Where(t => t.TicketId.ToString().Contains(Busqueda) ||
                         t.CreadoPorNombreCompleto.Contains(Busqueda) ||
                         t.Titulo.Contains(Busqueda))
                     .OrderByDescending(t => t.FechaCreado)
                     .Select(t => new Ticket()
                     {
                         TicketId = t.TicketId,
                         Area = t.Area,
                         Titulo = t.Titulo,
                         Observaciones = t.Observaciones,
                         Estado = t.Estado,
                         Gravedad = t.Gravedad,
                         FechaCreado = t.FechaCreado,
                         AsignadoANombreCompleto = t.AsignadoANombreCompleto,
                         SuplementosCuenta = t.Suplementos.Count,
                         ZonasCuenta = t.Zonas.Count,
                         EstadosCuenta = t.Estados.Count
                     })
                     .Take(10)
                     .ToListAsync();
        }

        public async Task<Ticket> GetTicketPorIdSuperAdministrador(long ticketId)
        {
            return await _context.Tickets
                    .Where(t => t.TicketId == ticketId)
                    .Include(t => t.Suplementos)
                    .Include(t => t.Estados)
                    .Include(t => t.Zonas)
                    .FirstOrDefaultAsync();
        }

        public async Task<Ticket> GetTicketPorIdAdministrador(string Departamento, long ticketId)
        {
            return await _context.Tickets
                    .Where(t => t.Area == Departamento)
                    .Where(t => t.TicketId == ticketId)
                    .Include(t => t.Suplementos)
                    .Include(t => t.Estados)
                    .Include(t => t.Zonas)
                    .FirstOrDefaultAsync();
        }

        public async Task<Ticket> GetTicketPorIdUsuario(string Usuario, long ticketId)
        {
            return await _context.Tickets
                    .Where(t => t.CreadoPor == Usuario || t.Publico == true)
                    .Where(t => t.TicketId == ticketId)
                    .Include(t => t.Suplementos)
                    .Include(t => t.Estados)
                    .Include(t => t.Zonas)
                    .FirstOrDefaultAsync();
        }
    }
}

