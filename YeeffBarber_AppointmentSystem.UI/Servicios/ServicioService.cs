using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using YeeffBarber_AppointmentSystem.Data.Context;
using YeeffBarber_AppointmentSystem.Data.Modelos;

namespace YeeffBarber_AppointmentSystem.UI.Servicios
{
    public class ServicioService : IService<Servicio>
    {
        private readonly AppDbContext _context;

        public ServicioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Guardar(Servicio servicio)
        {
            if (servicio == null)
                throw new ArgumentNullException("servicio");
                
            if (servicio.Id > 0)
                return await Modificar(servicio);
            else
                return await Insertar(servicio);
        }

        private async Task<bool> Insertar(Servicio servicio)
        {
            _context.Servicios.Add(servicio);
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Servicio servicio)
        {
            _context.Servicios.Update(servicio);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Servicio?> Get(int id)
        {
            return await _context.Servicios
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Servicio>> GetAll()
        {
            return await _context.Servicios
                .Where(s => s.Activo)
                .ToListAsync();
        }

        public async Task<List<Servicio>> GetServiciosDisponibles()
        {
            try
            {
                return await _context.Servicios
                    .Where(s => s.Activo)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener servicios disponibles: {ex.Message}", ex);
            }
        }
    }
}
