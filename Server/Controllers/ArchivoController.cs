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
    public class ArchivoController : Controller
    {
        private readonly IJornadaRepository _jornadaRepository;

        public ArchivoController(HelpDeskContext context)
        {
            this._jornadaRepository = new JornadaRepository(context);
        }

        #region "USUARIO ACTUAL"

        /// <summary>
        /// Trae los fichajes del usuario actual
        /// </summary>
        /// <returns></returns>
        [HttpGet("MisArchivos")]
        public async Task<ActionResult<ICollection<Archivo>>> GetMisFichajes(DateTime fechaComienzo, DateTime fechaFin)
        {
            ICollection<Jornada> jornadas = await _jornadaRepository.GetJornadasPorIdUsuario( new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)), fechaComienzo, fechaFin);

            if (jornadas.Count >= 0)
            {
                return Ok(jornadas);
            }
            else
            {
                return NoContent();
            }
        }

        #endregion "USUARIO ACTUAL"

        /// <summary>
        /// Trae los fichajes de los usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFichajesPorDepartamento")]
        public async Task<ActionResult<ICollection<Jornada>>> GetFichajesPorDepartamento()
        {
            ICollection<Jornada> jornadas = await _jornadaRepository.GetJornadasPorDepartamento(1);

            if (jornadas.Count >= 0)
            {
                return Ok(jornadas);
            }
            else
            {
                return NoContent();
            }
        }
    }
}

