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
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ZonaController : Controller
    {
        private readonly IZonaRepository _zonaRepository;

        public ZonaController(HelpDeskContext context)
        {
            this._zonaRepository = new ZonaRepository(context);
        }


        /*[HttpPut("Nuevo")]
        public async Task<SuplementoTicket> NuevoSuplemento([FromBody] SuplementoTicket suplementoTicket)
        {
            _context.SuplementosTicket.Add(suplementoTicket);
            await _context.SaveChangesAsync();
            return await Task.FromResult(suplementoTicket);
        }
        
        [HttpGet("GetArea")]
        public async Task<SuplementoTicket> GetSuplemento(long suplementoTicketId)
        {
            SuplementoTicket _suplementoTicket = new();
            Usuario _usuario = new();

            _usuario.Email = User.FindFirstValue(ClaimTypes.Name);
            _usuario.Rol = User.FindFirstValue(ClaimTypes.Role);

            if (_usuario.Rol == "Administrador")
            {
                _suplementoTicket = await _context.SuplementosTicket.FindAsync(suplementoTicketId);
            }
            else
            {
                _suplementoTicket = await _context.SuplementosTicket.Where(t => t.CreadoPor == _usuario.Email)
                    .Where(t => t.SuplementoTicketId == suplementoTicketId)
                    .FirstOrDefaultAsync();
            }
            return await Task.FromResult(_suplementoTicket);
        }*/

        [HttpGet("GetZonas")]
        public async Task<ICollection<Zona>> GetZonas()
        {
            ICollection<Zona> zonas = await _zonaRepository.GetZonas();
            return await Task.FromResult(zonas);
        }
    }
}