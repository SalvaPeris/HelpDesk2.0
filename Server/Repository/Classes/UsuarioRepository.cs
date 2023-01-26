using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Server.DB;
using HelpDesk.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Server.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly HelpDeskContext _context;

        public UsuarioRepository(HelpDeskContext helpDeskContext)
        {
            this._context = helpDeskContext;
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }


        public async Task<int> Eliminar(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            return await Guardar();
        }


        public Task<Usuario> GetUsuario(Guid usuarioId)
        {
            throw new NotImplementedException();
        }


        public async Task<Usuario> GetUsuarioPorDNI(string usuario)
        {
            return await _context.Usuarios.Where(u => u.Identificador == usuario).FirstOrDefaultAsync();
        }

        public async Task<Usuario> GetUsuarioChatPorDNI(string identificador)
        {
            return await _context.Usuarios
                .Where(u => u.Identificador == identificador)
                .Include(u => u.Departamento)
                .FirstOrDefaultAsync();
        }


        public async Task<Usuario> GetUsuarioPorId(Guid usuarioId)
        {
            return await _context.Usuarios.Where(u => u.UsuarioId == usuarioId).FirstOrDefaultAsync();
        }


        public async Task<Usuario> GetUsuarioChatPorId(Guid identificador)
        {
            return await _context.Usuarios
                .Where(u => u.UsuarioId == identificador)
                .Include(u => u.Departamento)
                .FirstOrDefaultAsync();
        }


        public async Task<ICollection<Usuario>> GetUsuariosPorDepartamento(string departamento)
        {
            return await _context.Usuarios.ToListAsync();
        }


        public async Task<ICollection<Usuario>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }


        public async Task<int> Guardar()
        {
            return await _context.SaveChangesAsync();
        }


        public async Task<int> Nuevo(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            return await Guardar();
        }


        public async Task<Usuario> GetUsuarioLogin(string identificador, string contrasena)
        {
            string _encryptedPassword = Crypto.Crypto.EncryptString(contrasena);
            //return await _context.Usuarios.Where(u => u.Identificador == usuario && u.Contrasena == _encryptedPassword).FirstOrDefaultAsync();
            return await _context.Usuarios.Where(u => u.Identificador == identificador).FirstOrDefaultAsync();
        }
    }
}
