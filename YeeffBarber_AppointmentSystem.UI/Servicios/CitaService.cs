using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using YeeffBarber_AppointmentSystem.Data.Context;
using YeeffBarber_AppointmentSystem.Data.Modelos;

namespace YeeffBarber_AppointmentSystem.UI.Servicios
{
    public class CitaService : IService<Cita>
    {
        private readonly AppDbContext _context;

        public CitaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Guardar(Cita cita)
        {
            if (cita.Id > 0)
                return await Modificar(cita);
            else
                return await Insertar(cita);
        }

        private async Task<bool> Insertar(Cita cita)
        {
            _context.Citas.Add(cita);
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Cita cita)
        {
            _context.Citas.Update(cita);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Cita?> Get(int id)
        {
            return await _context.Citas
                .Include(c => c.Servicio)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> ExisteCitaEnFechaYHora(DateTime fechaHora)
        {
            return await _context.Citas
                .AnyAsync(c => c.FechaHora == fechaHora);
        }

        public async Task<List<string>> ObtenerHorasOcupadas(DateTime fecha)
        {
            var citasDelDia = await _context.Citas
                .Where(c => c.FechaHora.Date == fecha.Date)
                .Select(c => c.FechaHora.ToString("h:mm tt"))
                .ToListAsync();
            return citasDelDia;
        }

        public async Task<List<Cita>> GetAll()
        {
            try
            {
                return await _context.Citas
                    .Include(c => c.Servicio)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en GetAll: {ex.Message}", ex);
            }
        }

        // Validation methods used in UI
        public bool ValidarNombre(string nombre)
        {
            return !string.IsNullOrWhiteSpace(nombre) && nombre.Length >= 3;
        }

        public bool ValidarTelefono(string telefono)
        {
            return !string.IsNullOrWhiteSpace(telefono) && telefono.Length >= 10;
        }

        public string FormatearConfirmacion(string nombre, string servicio, DateTime fechaHora)
        {
            return $"Cita confirmada para {nombre} - Servicio: {servicio} - Fecha: {fechaHora:dd/MM/yyyy HH:mm}";
        }
    }
}
