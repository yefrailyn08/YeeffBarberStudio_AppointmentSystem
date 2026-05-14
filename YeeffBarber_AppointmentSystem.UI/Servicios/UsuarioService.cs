using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using YeeffBarber_AppointmentSystem.Data.Context;
using YeeffBarber_AppointmentSystem.Data.Modelos;

namespace YeeffBarber_AppointmentSystem.UI.Servicios
{
    public class UsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> IniciarSesion(string email, string contrasena)
        {
            var hashedPassword = HashPassword(contrasena);
            
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Contrasena == hashedPassword && u.Activo);
        }

        public async Task<bool> Registrarse(string nombre, string email, string contrasena)
        {
            var existeEmail = await _context.Usuarios.AnyAsync(u => u.Email == email);
            
            if (existeEmail)
            {
                return false;
            }

            var hashedPassword = HashPassword(contrasena);
            
            var usuario = new Usuario
            {
                Nombre = nombre,
                Email = email,
                Contrasena = hashedPassword,
                FechaRegistro = DateTime.UtcNow,
                Activo = true
            };

            _context.Usuarios.Add(usuario);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Usuario?> GetByEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ExisteEmail(string email)
        {
            return await _context.Usuarios.AnyAsync(u => u.Email == email);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}