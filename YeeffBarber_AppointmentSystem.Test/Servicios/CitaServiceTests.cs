using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using YeeffBarber_AppointmentSystem.Data.Context;
using YeeffBarber_AppointmentSystem.Data.Modelos;
using YeeffBarber_AppointmentSystem.UI.Servicios;

namespace YeeffBarber_AppointmentSystem.Test.Servicios
{
    public class CitaServiceTests
    {
        private readonly AppDbContext _context;
        private readonly CitaService _service;

        public CitaServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new AppDbContext(options);
            
            // Seed test data
            _context.Servicios.Add(new Servicio { Id = 1, Nombre = "Corte de pelo", Precio = 500, DuracionMinutos = 30, Activo = true });
            _context.Servicios.Add(new Servicio { Id = 2, Nombre = "Cerquillo y barba", Precio = 700, DuracionMinutos = 45, Activo = true });
            _context.SaveChanges();
            
            _service = new CitaService(_context);
        }

        [Fact]
        public void ValidarNombre_NombreValido_DevuelveTrue()
        {
            Assert.True(_service.ValidarNombre("Juan Pérez"));
        }

        [Fact]
        public void ValidarNombre_NombreVacio_DevuelveFalse()
        {
            Assert.False(_service.ValidarNombre(""));
        }

        [Fact]
        public void ValidarNombre_NombreNull_DevuelveFalse()
        {
            Assert.False(_service.ValidarNombre(null!));
        }

        [Fact]
        public void ValidarNombre_NombreSoloEspacios_DevuelveFalse()
        {
            Assert.False(_service.ValidarNombre("   "));
        }

        [Fact]
        public void ValidarNombre_NombreMuyCorto_DevuelveFalse()
        {
            Assert.False(_service.ValidarNombre("Jo"));
        }

        [Fact]
        public void ValidarTelefono_TelefonoValido_DevuelveTrue()
        {
            Assert.True(_service.ValidarTelefono("8091234567"));
        }

        [Fact]
        public void ValidarTelefono_TelefonoVacio_DevuelveFalse()
        {
            Assert.False(_service.ValidarTelefono(""));
        }

        [Fact]
        public void ValidarTelefono_TelefonoNull_DevuelveFalse()
        {
            Assert.False(_service.ValidarTelefono(null!));
        }

        [Fact]
        public void ValidarTelefono_TelefonoCorto_DevuelveFalse()
        {
            Assert.False(_service.ValidarTelefono("809123"));
        }

        [Fact]
        public async Task GetAll_DevuelveCitas()
        {
            var cita = new Cita
            {
                NombreCompleto = "Juan Pérez",
                Telefono = "8091234567",
                ServicioId = 1,
                FechaHora = DateTime.Today.AddDays(1)
            };
            await _service.Guardar(cita);

            var citas = await _service.GetAll();
            
            Assert.NotEmpty(citas);
            Assert.Single(citas);
        }

        [Fact]
        public async Task GetAll_DevuelveCitaConServicio()
        {
            var cita = new Cita
            {
                NombreCompleto = "Juan Pérez",
                Telefono = "8091234567",
                ServicioId = 1,
                FechaHora = DateTime.Today.AddDays(1)
            };
            await _service.Guardar(cita);

            var citas = await _service.GetAll();
            
            Assert.NotEmpty(citas);
            Assert.NotNull(citas[0].Servicio);
            Assert.Equal("Corte de pelo", citas[0].Servicio!.Nombre);
        }

        [Fact]
        public void FormatearConfirmacion_DevuelveMensajeConDatos()
        {
            var fecha = new DateTime(2026, 5, 10, 10, 0, 0);
            var resultado = _service.FormatearConfirmacion(
                "Juan Pérez",
                "Corte de pelo",
                fecha
            );

            Assert.Contains("Juan Pérez", resultado);
            Assert.Contains("Corte de pelo", resultado);
            Assert.Contains("10/05/2026", resultado);
            Assert.Contains("10:00", resultado);
        }
    }
}
