using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using YeeffBarber_AppointmentSystem.Data.Context;
using YeeffBarber_AppointmentSystem.Data.Modelos;
using YeeffBarber_AppointmentSystem.UI.Servicios;

namespace YeeffBarber_AppointmentSystem.Test.Servicios
{
    public class ServicioServiceTests
    {
        private readonly AppDbContext _context;
        private readonly ServicioService _service;

        public ServicioServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new AppDbContext(options);
            
            // Seed test data
            _context.Servicios.Add(new Servicio { Id = 1, Nombre = "Corte de pelo", Precio = 500, DuracionMinutos = 30, Activo = true });
            _context.Servicios.Add(new Servicio { Id = 2, Nombre = "Cerquillo y barba", Precio = 700, DuracionMinutos = 45, Activo = true });
            _context.Servicios.Add(new Servicio { Id = 3, Nombre = "Corte de niños", Precio = 300, DuracionMinutos = 20, Activo = true });
            _context.Servicios.Add(new Servicio { Id = 4, Nombre = "Servicio inactivo", Precio = 100, DuracionMinutos = 10, Activo = false });
            _context.SaveChanges();
            
            _service = new ServicioService(_context);
        }

        [Fact]
        public async Task GetServiciosDisponibles_DevuelveSoloActivos()
        {
            var servicios = await _service.GetServiciosDisponibles();
            
            Assert.Equal(3, servicios.Count);
            Assert.DoesNotContain(servicios, s => s.Nombre == "Servicio inactivo");
        }

        [Fact]
        public async Task GetServiciosDisponibles_DevuelveLosTresServicios()
        {
            var servicios = await _service.GetServiciosDisponibles();
            
            Assert.Contains(servicios, s => s.Nombre == "Corte de pelo");
            Assert.Contains(servicios, s => s.Nombre == "Cerquillo y barba");
            Assert.Contains(servicios, s => s.Nombre == "Corte de niños");
        }

        [Fact]
        public async Task GuardarServicio_NuevoServicio_ReturnsTrue()
        {
            var servicio = new Servicio
            {
                Nombre = "Nuevo servicio",
                Precio = 400,
                DuracionMinutos = 25,
                Activo = true
            };

            var resultado = await _service.Guardar(servicio);
            
            Assert.True(resultado);
        }
    }
}
