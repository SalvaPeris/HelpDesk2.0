using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HelpDesk.Server.DB;
using HelpDesk.Server.Repository;
using HelpDesk.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Radzen;

namespace HelpDesk.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TicketController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        private int result;
            
        public TicketController(HelpDeskContext helpDeskContext)
        {
            this._ticketRepository = new TicketRepository(helpDeskContext);
        }

        /// <summary>
        /// Devuelve los tickets tanto personales como departamentales con el Estado diferente de solucionado.
        /// </summary>
        /// <returns></returns>
        [HttpGet("MisTickets")]
        public async Task<ActionResult<ICollection<Ticket>>> MisTickets(string Estado, int Skip, int Top)
        {
            return await GetTicketsPropios(Estado, Skip, Top);
        }

        /// Devuelve los tickets tanto personales como departamentales con el Estado diferente de solucionado.
        /// </summary>
        /// <returns></returns>
        [HttpGet("TicketsAsignados")]
        public async Task<ActionResult<ICollection<Ticket>>> Asignados(string Estado, int Skip, int Top)
        {
            //return await GetTicketsNoEstado("Solucionado");
            return await GetTicketsAsignados(Estado, Skip, Top);
        }

        /// <summary>
        /// Devuelve los tickets tanto personales como departamentales con el Estado diferente de solucionado.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Tickets")]
        public async Task<ActionResult<ICollection<Ticket>>> Tickets(int Skip, int Top)
        {
            //return await GetTicketsNoEstado("Solucionado");
            return await GetTicketsDepartamento("Todos", Skip, Top);
        }

        /// <summary>
        /// Devuelve el número de tickets que 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpGet("TotalTickets")]
        public async Task<ActionResult<int>> CargarTotalTickets(string Estado)
        {
            int Cuenta = 0;

            if (User.IsInRole("SuperAdministrador"))
            {
                Cuenta = Estado switch
                {
                    "Activos" => await _ticketRepository.CargarTotalTicketsSuperAdministrador("activo"),
                    "Solucionados" => await _ticketRepository.CargarTotalTicketsSuperAdministrador("solucionado"),
                    "Vistos" => await _ticketRepository.CargarTotalTicketsSuperAdministrador("visto"),
                    "Pendientes" => await _ticketRepository.CargarTotalTicketsSuperAdministrador("en progreso"),
                    _ => await _ticketRepository.CargarTotalTicketsSuperAdministrador(),
                };
            }
            else if (User.IsInRole("Administrador"))
            {
                Cuenta = Estado switch
                {
                    "Activos" => await _ticketRepository.CargarTotalTicketsAdministrador("activo", User.FindFirstValue(ClaimTypes.GroupSid)),
                    "Solucionados" => await _ticketRepository.CargarTotalTicketsAdministrador("solucionado", User.FindFirstValue(ClaimTypes.GroupSid)),
                    "Vistos" => await _ticketRepository.CargarTotalTicketsAdministrador("visto", User.FindFirstValue(ClaimTypes.GroupSid)),
                    "Pendientes" => await _ticketRepository.CargarTotalTicketsAdministrador("en progreso", User.FindFirstValue(ClaimTypes.GroupSid)),
                    _ => await _ticketRepository.CargarTotalTicketsAdministrador(User.FindFirstValue(ClaimTypes.GroupSid)),
                };
            }
            else
            {
                Cuenta = Estado switch
                {
                    "Activos" => await _ticketRepository.CargarTotalTicketsUsuario("activo", User.FindFirstValue(ClaimTypes.GroupSid)),
                    "Solucionados" => await _ticketRepository.CargarTotalTicketsUsuario("solucionado", User.FindFirstValue(ClaimTypes.GroupSid)),
                    "Vistos" => await _ticketRepository.CargarTotalTicketsUsuario("visto", User.FindFirstValue(ClaimTypes.GroupSid)),
                    "Pendientes" => await _ticketRepository.CargarTotalTicketsUsuario("en progreso", User.FindFirstValue(ClaimTypes.GroupSid)),
                    _ => await _ticketRepository.CargarTotalTicketsUsuario(User.FindFirstValue(ClaimTypes.GroupSid)),
                };
            }

            if (Cuenta > 0 || Cuenta == 0)
            {
                return Ok(Cuenta);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Devuelve el número de tickets que 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpGet("TotalTicketsPropios")]
        public async Task<ActionResult<int>> CargarTotalTicketsPropios(string Estado)
        {
            int Cuenta = 0;

            if (User.IsInRole("SuperAdministrador"))
            {
                Cuenta = Estado switch
                {
                    "Activos" => await _ticketRepository.CargarTotalTicketsPropiosSuperAdministrador("activo", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    "Solucionados" => await _ticketRepository.CargarTotalTicketsPropiosSuperAdministrador("solucionado", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    "Vistos" => await _ticketRepository.CargarTotalTicketsPropiosSuperAdministrador("visto", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    "Pendientes" => await _ticketRepository.CargarTotalTicketsPropiosSuperAdministrador("en progreso", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    _ => await _ticketRepository.CargarTotalTicketsPropiosSuperAdministrador(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                };
            }
            else if (User.IsInRole("Administrador"))
            {
                Cuenta = Estado switch
                {
                    "Activos" => await _ticketRepository.CargarTotalTicketsPropiosAdministrador("activo", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    "Solucionados" => await _ticketRepository.CargarTotalTicketsPropiosAdministrador("solucionado", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    "Vistos" => await _ticketRepository.CargarTotalTicketsPropiosAdministrador("visto", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    "Pendientes" => await _ticketRepository.CargarTotalTicketsPropiosAdministrador("en progreso", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    _ => await _ticketRepository.CargarTotalTicketsPropiosAdministrador(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                };
            }
            else
            {
                Cuenta = Estado switch
                {
                    "Activos" => await _ticketRepository.CargarTotalTicketsPropiosUsuario("activo", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    "Solucionados" => await _ticketRepository.CargarTotalTicketsPropiosUsuario("solucionado", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    "Vistos" => await _ticketRepository.CargarTotalTicketsPropiosUsuario("visto", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    "Pendientes" => await _ticketRepository.CargarTotalTicketsPropiosUsuario("en progreso", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    _ => await _ticketRepository.CargarTotalTicketsPropiosUsuario(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                };
            }

            if (Cuenta > 0 || Cuenta == 0)
            {
                return Ok(Cuenta);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Devuelve el número de tickets que 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpGet("TotalTicketsAsignados")]
        public async Task<ActionResult<int>> CargarTotalTicketsAsignados(string Estado)
        {
            int Cuenta = 0;

            if (User.IsInRole("SuperAdministrador"))
            {
                Cuenta = Estado switch
                {
                    "Activos" => await _ticketRepository.CargarTotalTicketsAsignadosSuperAdministrador("activo", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    "Solucionados" => await _ticketRepository.CargarTotalTicketsAsignadosSuperAdministrador("solucionado", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    "Vistos" => await _ticketRepository.CargarTotalTicketsAsignadosSuperAdministrador("visto", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    "Pendientes" => await _ticketRepository.CargarTotalTicketsAsignadosSuperAdministrador("en progreso", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    _ => await _ticketRepository.CargarTotalTicketsAsignadosSuperAdministrador(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                };
            }
            else if (User.IsInRole("Administrador"))
            {
                Cuenta = Estado switch
                {
                    "Activos" => await _ticketRepository.CargarTotalTicketsAsignadosAdministrador("activo", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    "Solucionados" => await _ticketRepository.CargarTotalTicketsAsignadosAdministrador("solucionado", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    "Vistos" => await _ticketRepository.CargarTotalTicketsAsignadosAdministrador("visto", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    "Pendientes" => await _ticketRepository.CargarTotalTicketsAsignadosAdministrador("en progreso", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    _ => await _ticketRepository.CargarTotalTicketsAsignadosAdministrador(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                };
            }
            else
            {
                Cuenta = Estado switch
                {
                    "Activos" => await _ticketRepository.CargarTotalTicketsAsignadosUsuario("activo", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    "Solucionados" => await _ticketRepository.CargarTotalTicketsAsignadosUsuario("solucionado", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    "Vistos" => await _ticketRepository.CargarTotalTicketsAsignadosUsuario("visto", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    "Pendientes" => await _ticketRepository.CargarTotalTicketsAsignadosUsuario("en progreso", User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    _ => await _ticketRepository.CargarTotalTicketsAsignadosUsuario(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                };
            }

            if (Cuenta > 0 || Cuenta == 0)
            {
                return Ok(Cuenta);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Devuelve los tickets personales con el Estado pasado.
        /// </summary>
        /// <param name="Estado"></param>
        /// <returns></returns>
        private async Task<ActionResult<ICollection<Ticket>>> GetTicketsPropios(string Estado, int Skip, int Top)
        {
            ICollection<Ticket> _tickets;
            if(Estado == "Todos")
            {
               _tickets = await _ticketRepository.GetTicketsPropios(User.FindFirstValue(ClaimTypes.NameIdentifier), Skip, Top);
            }
            else
            {
               _tickets = await _ticketRepository.GetTicketsPropiosPorEstado(Estado, User.FindFirstValue(ClaimTypes.NameIdentifier), Skip, Top);
            }

            if (_tickets.Count >= 0)
            {
                return Ok(_tickets);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Devuelve los tickets personales con el Estado pasado.
        /// </summary>
        /// <param name="Estado"></param>
        /// <returns></returns>
        private async Task<ActionResult<ICollection<Ticket>>> GetTicketsAsignados(string Estado, int Skip, int Top)
        {
            ICollection<Ticket> _tickets;
            if (Estado == "Todos")
            {
                _tickets = await _ticketRepository.GetTicketsAsignados(User.FindFirstValue(ClaimTypes.NameIdentifier), Skip, Top);
            }
            else
            {
                _tickets = await _ticketRepository.GetTicketsAsignadosPorEstado(Estado, User.FindFirstValue(ClaimTypes.NameIdentifier), Skip, Top);
            }

            if (_tickets.Count > 0 || _tickets.Count == 0)
            {
                return Ok(_tickets);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Devuelve los tickets departamentales con el Estado pasado.
        /// </summary>
        /// <param name="Estado"></param>
        /// <returns></returns>
        public async Task<ActionResult<ICollection<Ticket>>> GetTicketsDepartamento(string Estado, int Skip, int Top)
        {
            ICollection<Ticket> _tickets;
            if (Estado == "Todos")
            {
                if (User.IsInRole("SuperAdministrador"))
                {
                    _tickets = await _ticketRepository.GetTicketsSuperAdministrador(Skip, Top);
                }
                else if (User.IsInRole("Administrador"))
                {
                    _tickets = await _ticketRepository.GetTicketsAdministrador(User.FindFirstValue(ClaimTypes.GroupSid), Skip, Top);
                }
                else
                {
                    _tickets = await _ticketRepository.GetTicketsPublicosPorDepartamento(User.FindFirstValue(ClaimTypes.GroupSid), Skip, Top);
                }
            }
            else
            {
                if (User.IsInRole("SuperAdministrador"))
                {
                    _tickets = await _ticketRepository.GetTicketsPorEstadoSuperAdministrador(Estado, Skip, Top);

                }
                else if (User.IsInRole("Administrador"))
                {
                    _tickets = await _ticketRepository.GetTicketsDepartamentoPorEstado(Estado, User.FindFirstValue(ClaimTypes.GroupSid), Skip, Top);
                }
                else
                {
                    _tickets = await _ticketRepository.GetTicketsPublicosDepartamentoPorEstado(Estado, User.FindFirstValue(ClaimTypes.GroupSid), Skip, Top);
                }
            }

            if (_tickets.Count > 0 || _tickets.Count == 0)
            {
                return Ok(_tickets);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Devuelve los tickets tanto personales como departamentales con cualquier Estado.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        public async Task<ActionResult<ICollection<Ticket>>> GetTicketsDepartamentoTodos(int Skip, int Top)
        {
            return await GetTicketsDepartamento("Todos", Skip, Top);
        }

        /// <summary>
        /// Devuelve los tickets tanto personales como departamentales con Estado activo.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Activos")]
        public async Task<ActionResult<ICollection<Ticket>>> GetTicketsDepartamentoActivos(int Skip, int Top)
        {
            return await GetTicketsDepartamento("Activo", Skip, Top);
        }

        /// <summary>
        /// Devuelve los tickets tanto personales como departamentales con Estado visto.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Vistos")]
        public async Task<ActionResult<ICollection<Ticket>>> GetTicketsDepartamentoVistos(int Skip, int Top)
        {
            return await GetTicketsDepartamento("Visto", Skip, Top);
        }

        /// <summary>
        /// Devuelve los tickets tanto personales como departamentales con Estado pendiente.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Pendientes")]
        public async Task<ActionResult<ICollection<Ticket>>> GetTicketsDepartamentoPendientes(int Skip, int Top)
        {
            return await GetTicketsDepartamento("En progreso", Skip, Top);
        }

        /// <summary>
        /// Devuelve los tickets departamentales con Estado solucionado.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Solucionados")]
        public async Task<ActionResult<ICollection<Ticket>>> GetTicketsDepartamentoSolucionados(int Skip, int Top)
        {
            return await GetTicketsDepartamento("Solucionado", Skip, Top);
        }

        /// <summary>
        /// Buscar aquí cuando la lista esté en Departamentales 
        /// </summary>
        /// <param name="busqueda"></param>
        /// <returns></returns>
        [HttpGet("BuscarDepartamentales")]
        public async Task<ActionResult<ICollection<Ticket>>> BuscarTicketDepartamento(string busqueda)
        {
            ICollection<Ticket> _tickets;

            if (User.IsInRole("SuperAdministrador"))
            {
                _tickets = await _ticketRepository.GetTicketsPorBusquedaSuperAdministrador(busqueda);
            }
            else if (User.IsInRole("Administrador"))
            {
                _tickets = await _ticketRepository.GetTicketsDepartamentoPorBusquedaAdministrador(User.FindFirstValue(ClaimTypes.GroupSid), busqueda);
            }
            else
            {
                _tickets = await _ticketRepository.GetTicketsDepartamentoPorBusquedaUsuario(User.FindFirstValue(ClaimTypes.GroupSid), busqueda);
            }

            if (_tickets.Count > 0 || _tickets.Count == 0)
            {
                return Ok(_tickets);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Buscar aquí cuando la lista esté en Míos 
        /// </summary>
        /// <param name="busqueda"></param>
        /// <returns></returns>
        [HttpGet("BuscarPropios")]
        public async Task<ActionResult<ICollection<Ticket>>> BuscarTicketPropios(string busqueda)
        {
            ICollection<Ticket> _tickets = await _ticketRepository.GetTicketsPropiosPorBusqueda(User.FindFirstValue(ClaimTypes.NameIdentifier), busqueda);

            if (_tickets.Count > 0 || _tickets.Count == 0)
            {
                return Ok(_tickets);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Buscar aquí cuando la lista esté en Asignados 
        /// </summary>
        /// <param name="busqueda"></param>
        /// <returns></returns>
        [HttpGet("BuscarAsignados")]
        public async Task<ActionResult<ICollection<Ticket>>> BuscarTicketAsignados(string busqueda)
        {
            ICollection<Ticket> _tickets = await _ticketRepository.GetTicketsAsignadosPorBusqueda(User.FindFirstValue(ClaimTypes.NameIdentifier), busqueda);

            if (_tickets.Count > 0 || _tickets.Count == 0)
            {
                return Ok(_tickets);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Actualiza el ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [HttpPut("Actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] Ticket ticket)
        {
            Ticket _ticketActualizar = await TraerTicket(ticket.TicketId);

            if (_ticketActualizar == null)
            {
                return null;
            }

            _ticketActualizar.Titulo = ticket.Titulo;
            _ticketActualizar.Publico = ticket.Publico;
            _ticketActualizar.Area = ticket.Area;
            _ticketActualizar.TipoTicket = ticket.TipoTicket;
            _ticketActualizar.Gravedad = ticket.Gravedad;
            _ticketActualizar.FechaCreado = ticket.FechaCreado;
            _ticketActualizar.FechaSolucionado = ticket.FechaSolucionado;
            _ticketActualizar.Imagen = ticket.Imagen;
            _ticketActualizar.Observaciones = ticket.Observaciones;
            _ticketActualizar.TelefonoContacto = ticket.TelefonoContacto;
            _ticketActualizar.Suplementos = ticket.Suplementos;
            _ticketActualizar.Zonas = ticket.Zonas;

            if(ticket.Estado != "Solucionado" && ticket.Estado != "En progreso")
            {
                _ticketActualizar.Area = ticket.Area;
            }

            if (ticket.CreadoPor != null && User.IsInRole("Administrador"))
            {
                _ticketActualizar.CreadoPor = ticket.CreadoPor;
                _ticketActualizar.CreadoPorNombreCompleto = ticket.CreadoPorNombreCompleto;
            }

            if(ticket.AsignadoA != _ticketActualizar.AsignadoA)
            {
                _ticketActualizar.AsignadoA = ticket.AsignadoA;
                _ticketActualizar.AsignadoANombreCompleto = ticket.AsignadoANombreCompleto;
                _ticketActualizar.Estados.Add(new()
                {
                    TicketId = ticket.TicketId,
                    Estado = "Asignado",
                    EstadoPor = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EstadoPorNombreCompleto = User.FindFirstValue(ClaimTypes.Name) + " " + User.FindFirstValue(ClaimTypes.Surname),
                    FechaEstado = DateTime.Now,
                    Observaciones = " a " + ticket.AsignadoANombreCompleto
                });
            }

            result = await _ticketRepository.Guardar();

            if(result >= 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Actualiza y cambia de Estado a solucionado
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [HttpPut("ActualizarYSolucionar")]
        public async Task<IActionResult> ActualizarYSolucionar([FromBody] Ticket ticket)
        {
            Ticket _ticketActualizar = await TraerTicket(ticket.TicketId);

            if (_ticketActualizar == null)
            {
                return null;
            }

            _ticketActualizar.Titulo = ticket.Titulo;
            _ticketActualizar.Publico = ticket.Publico;
            _ticketActualizar.Area = ticket.Area;
            _ticketActualizar.TipoTicket = ticket.TipoTicket;
            _ticketActualizar.Gravedad = ticket.Gravedad;
            _ticketActualizar.FechaCreado = ticket.FechaCreado;
            _ticketActualizar.FechaSolucionado = ticket.FechaSolucionado;
            _ticketActualizar.Imagen = ticket.Imagen;
            _ticketActualizar.Observaciones = ticket.Observaciones;
            _ticketActualizar.TelefonoContacto = ticket.TelefonoContacto;
            _ticketActualizar.Suplementos = ticket.Suplementos;
            _ticketActualizar.Zonas = ticket.Zonas;
            _ticketActualizar.Estado = "Solucionado";

            _ticketActualizar.Estados.Add(new()
            {
                TicketId = ticket.TicketId,
                Estado = "Solucionado",
                EstadoPor = User.FindFirstValue(ClaimTypes.NameIdentifier),
                EstadoPorNombreCompleto = User.FindFirstValue(ClaimTypes.Name) + " " + User.FindFirstValue(ClaimTypes.Surname),
                FechaEstado = DateTime.Now
            });

            if (ticket.CreadoPor != null && User.IsInRole("Administrador"))
            {
                _ticketActualizar.CreadoPor = ticket.CreadoPor;
                _ticketActualizar.CreadoPorNombreCompleto = ticket.CreadoPorNombreCompleto;
            }

            if (ticket.AsignadoA != _ticketActualizar.AsignadoA)
            {
                _ticketActualizar.AsignadoA = ticket.AsignadoA;
                _ticketActualizar.AsignadoANombreCompleto = ticket.AsignadoANombreCompleto;
                _ticketActualizar.Estados.Add(new()
                {
                    TicketId = ticket.TicketId,
                    Estado = "Asignado",
                    EstadoPor = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EstadoPorNombreCompleto = User.FindFirstValue(ClaimTypes.Name) + " " + User.FindFirstValue(ClaimTypes.Surname),
                    FechaEstado = DateTime.Now,
                    Observaciones = " a " + ticket.AsignadoANombreCompleto
                });
            }

            

            result = await _ticketRepository.Guardar();

            if (result >= 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Se actualiza el ticket a solucionar
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        [HttpPut("Solucionar")]
        public async Task<IActionResult> Solucionar([FromBody] long ticketId)
        {
            Ticket _ticket = new();

            _ticket = await TraerTicket(ticketId);

            if(_ticket.Estados == null)
            {
                _ticket.Estados = new List<EstadoTicket>();
            }

            _ticket.Estados.Add(new()
            {
                Estado = "Solucionado",
                EstadoPor = User.FindFirstValue(ClaimTypes.NameIdentifier),
                EstadoPorNombreCompleto = User.FindFirstValue(ClaimTypes.Name) + " " + User.FindFirstValue(ClaimTypes.Surname),
                FechaEstado = DateTime.Now
            });

            _ticket.SolucionadoPor = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _ticket.SolucionadoPorNombreCompleto = User.FindFirstValue(ClaimTypes.Name) + " " + User.FindFirstValue(ClaimTypes.Surname);
            _ticket.Estado = "Solucionado";

            result = await _ticketRepository.Guardar();

            if (result >= 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Se actualiza el ticket a pendiente
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        [HttpPut("Pendiente")]
        public async Task<IActionResult> Pendiente([FromBody] long ticketId)
        {
            Ticket _ticket = new();

            _ticket = await TraerTicket(ticketId);
            _ticket.Estado = "En progreso";


            if (_ticket.Estados == null)
            {
                _ticket.Estados = new List<EstadoTicket>();
            }

            _ticket.Estados.Add(new()
            {
                Estado = "En progreso",
                EstadoPor = User.FindFirstValue(ClaimTypes.NameIdentifier),
                EstadoPorNombreCompleto = User.FindFirstValue(ClaimTypes.Name) + " " + User.FindFirstValue(ClaimTypes.Surname),
                FechaEstado = DateTime.Now
            });

            result = await _ticketRepository.Guardar();

            if (result >= 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Se actualiza el ticket a activo
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        [HttpPut("Activar")]
        public async Task<IActionResult> Activar([FromBody] long ticketId)
        {
            Ticket _ticket = new();

            _ticket = await TraerTicket(ticketId);
            _ticket.Estado = "Activo";


            if (_ticket.Estados == null)
            {
                _ticket.Estados = new List<EstadoTicket>();
            }

            _ticket.Estados.Add(new()
            {
                Estado = "Activo",
                EstadoPor = User.FindFirstValue(ClaimTypes.NameIdentifier),
                EstadoPorNombreCompleto = User.FindFirstValue(ClaimTypes.Name) + " " + User.FindFirstValue(ClaimTypes.Surname),
                FechaEstado = DateTime.Now
            });

            result = await _ticketRepository.Guardar();

            if (result >= 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Se actualiza el ticket a visto
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        [HttpPut("Visto")]
        public async Task<IActionResult> Visto([FromBody] long ticketId)
        {
            Ticket _ticket = new();

            _ticket = await TraerTicket(ticketId);
            _ticket.Estado = "Visto";


            if (_ticket.Estados == null)
            {
                _ticket.Estados = new List<EstadoTicket>();
            }

            _ticket.Estados.Add(new()
            {
                Estado = "Visto",
                EstadoPor = User.FindFirstValue(ClaimTypes.NameIdentifier),
                EstadoPorNombreCompleto = User.FindFirstValue(ClaimTypes.Name) + " " + User.FindFirstValue(ClaimTypes.Surname),
                FechaEstado = DateTime.Now
            });

            result = await _ticketRepository.Guardar();

            if (result >= 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Consigue el ticket según su identificador
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        private async Task<Ticket> TraerTicket(long ticketId)
        {
            Ticket _ticket = new();

            if (User.IsInRole("SuperAdministrador"))
            {
                _ticket = await _ticketRepository.GetTicketPorIdSuperAdministrador(ticketId);
            }
            else if (User.IsInRole("Administrador"))
            {
                _ticket = await _ticketRepository.GetTicketPorIdAdministrador(User.FindFirstValue(ClaimTypes.GroupSid), ticketId);

                if (_ticket == null)
                {
                    _ticket = await _ticketRepository.GetTicketPorIdUsuario(User.FindFirstValue(ClaimTypes.NameIdentifier), ticketId);
                }
            }
            else
            {
                _ticket = await _ticketRepository.GetTicketPorIdUsuario(User.FindFirstValue(ClaimTypes.NameIdentifier), ticketId);
            }

            return _ticket;
        }

        /// <summary>
        /// Consigue el ticket según su identificador
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        [HttpGet("GetTicket")]
        public async Task<ActionResult<Ticket>> GetTicket(long ticketId)
        {
            Ticket _ticket = await TraerTicket(ticketId);

            if (_ticket != null)
            {
                return Ok(_ticket);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Crea nuevo ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [HttpPut("Nuevo")]
        public async Task<ActionResult<Ticket>> NuevoTicket([FromBody] Ticket ticket)
        {
            var result = await _ticketRepository.Nuevo(ticket);

            if (ticket.AsignadoANombreCompleto != null)
            {
                ticket.Estados.Add(new()
                {
                    Estado = "Asignado",
                    EstadoPor = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    EstadoPorNombreCompleto = User.FindFirstValue(ClaimTypes.Name) + " " + User.FindFirstValue(ClaimTypes.Surname),
                    FechaEstado = DateTime.Now,
                    Observaciones = " a " + ticket.AsignadoANombreCompleto
                });
            }

            if (result > 0)
            {
                return Ok(ticket);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Elimina el ticket indicado.
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        [HttpPut("Eliminar")]
        public async Task<IActionResult> EliminarTicket([FromBody] long ticketId)
        {
            Ticket _ticket = await TraerTicket(ticketId);

            if (_ticket == null)
            {
                return BadRequest();
            }

            var result = await _ticketRepository.Eliminar(_ticket);

            if (result > 0)
            {
                return Ok();
            } else
            {
                return BadRequest();
            }
        }
    }
}

