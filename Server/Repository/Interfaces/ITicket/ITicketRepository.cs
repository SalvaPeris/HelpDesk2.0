using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelpDesk.Shared.Models;
using Radzen;

namespace HelpDesk.Server.Repository
{
	public interface ITicketRepository : IDisposable
	{
        Task<ICollection<Ticket>> GetTicketsPorEstadoSuperAdministrador(string Estado, int Skip, int Top);
        Task<ICollection<Ticket>> GetTicketsPropiosPorEstado(string Estado, string Usuario, int Skip, int Top);
        Task<ICollection<Ticket>> GetTicketsPublicosDepartamentoPorEstado(string Estado, string Departamento, int Skip, int Top);
        Task<ICollection<Ticket>> GetTicketsDepartamentoPorEstado(string Estado, string Departamento, int Skip, int Top);
        Task<ICollection<Ticket>> GetTicketsAsignadosPorEstado(string Estado, string Usuario, int Skip, int Top);


        //GET TICKET POR Departamento
        Task<ICollection<Ticket>> GetTicketsSuperAdministrador(int Skip, int Top);
        Task<ICollection<Ticket>> GetTicketsAdministrador(string Departamento, int Skip, int Top);

        Task<ICollection<Ticket>> GetTicketsPropios(string Usuario, int Skip, int Top);
        Task<ICollection<Ticket>> GetTickets(int Skip, int Top);
        Task<ICollection<Ticket>> GetTicketsAsignados(string Usuario, int Skip, int Top);


        Task<int> CargarTotalTicketsSuperAdministrador(string Estado);
        Task<int> CargarTotalTicketsSuperAdministrador();
        Task<int> CargarTotalTicketsAdministrador(string Estado, string Departamento);
        Task<int> CargarTotalTicketsAdministrador(string Departamento);
        Task<int> CargarTotalTicketsUsuario(string Estado, string Departamento);
        Task<int> CargarTotalTicketsUsuario(string Departamento);

        Task<int> CargarTotalTicketsPropiosSuperAdministrador(string Estado, string Usuario);
        Task<int> CargarTotalTicketsPropiosSuperAdministrador(string Usuario);
        Task<int> CargarTotalTicketsPropiosAdministrador(string Estado, string Usuario);
        Task<int> CargarTotalTicketsPropiosAdministrador(string Usuario);
        Task<int> CargarTotalTicketsPropiosUsuario(string Estado, string Usuario);
        Task<int> CargarTotalTicketsPropiosUsuario(string Usuario);

        Task<int> CargarTotalTicketsAsignadosSuperAdministrador(string Estado, string Usuario);
        Task<int> CargarTotalTicketsAsignadosSuperAdministrador(string Usuario);
        Task<int> CargarTotalTicketsAsignadosAdministrador(string Estado, string Usuario);
        Task<int> CargarTotalTicketsAsignadosAdministrador(string Usuario);
        Task<int> CargarTotalTicketsAsignadosUsuario(string Estado, string Usuario);
        Task<int> CargarTotalTicketsAsignadosUsuario(string Usuario);


        Task<ICollection<Ticket>> GetTicketsPublicosPorDepartamento(string Departamento, int Skip, int Top);

        //GET TICKET POR ID
        Task<Ticket> GetTicketPorIdSuperAdministrador(long ticketId);
        Task<Ticket> GetTicketPorIdAdministrador(string Departamento, long ticketId);
        Task<Ticket> GetTicketPorIdUsuario(string Usuario, long ticketId);

        //GET TICKET POR BÚSQUEDA
        Task<ICollection<Ticket>> GetTicketsPorBusquedaSuperAdministrador(string busqueda);
        Task<ICollection<Ticket>> GetTicketsPropiosPorBusqueda(string Usuario, string busqueda);
        Task<ICollection<Ticket>> GetTicketsDepartamentoPorBusquedaAdministrador(string Departamento, string busqueda);
        Task<ICollection<Ticket>> GetTicketsDepartamentoPorBusquedaUsuario(string Departamento, string busqueda);
        Task<ICollection<Ticket>> GetTicketsAsignadosPorBusqueda(string Departamento, string busqueda);

        Task<int> Nuevo(Ticket ticket);
        Task<int> Eliminar(Ticket ticket);
        Task<int> Actualizar(Ticket ticket);
        Task<int> Guardar();
    }
}

