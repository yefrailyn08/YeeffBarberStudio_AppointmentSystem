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
        public async Task Get_CitaExistente_DevuelveCita()
        {
            var cita = new Cita
            {
                NombreCompleto = "Juan Pérez",
                Telefono = "8091234567",
                ServicioId = 1,
                FechaHora = DateTime.Today.AddDays(1)
            };
            await _service.Guardar(cita);

            var citaEncontrada = await _service.Get(cita.Id);
            
            Assert.NotNull(citaEncontrada);
            Assert.Equal("Juan Pérez", citaEncontrada!.NombreCompleto);
        }

[Fact]
        public async Task Get_ServicioInexistente_DevuelveNull()
        {
            var servicio = await _service.Get(999);
            
            Assert.Null(servicio);
        }

        [Fact]
        public async Task ExisteCitaEnFechaYHora_HoraOcupada_DevuelveTrue()
        {
            var fechaHora = new DateTime(2026, 5, 15, 9, 0, 0);
            var cita = new Cita
            {
                NombreCompleto = "Juan Pérez",
                Telefono = "8091234567",
                ServicioId = 1,
                FechaHora = fechaHora
            };
            await _service.Guardar(cita);

            var existe = await _service.ExisteCitaEnFechaYHora(fechaHora);
            
            Assert.True(existe);
        }

        [Fact]
        public async Task ExisteCitaEnFechaYHora_HoraLibre_DevuelveFalse()
        {
            var fechaHora = new DateTime(2026, 5, 15, 10, 0, 0);

            var existe = await _service.ExisteCitaEnFechaYHora(fechaHora);
            
            Assert.False(existe);
        }

        [Fact]
        public async Task ObtenerHorasOcupadas_DevuelveHorasDelDia()
        {
            var cita1 = new Cita
            {
                NombreCompleto = "Juan Pérez",
                Telefono = "8091234567",
                ServicioId = 1,
                FechaHora = new DateTime(2026, 5, 15, 9, 0, 0)
            };
            var cita2 = new Cita
            {
                NombreCompleto = "María García",
                Telefono = "8099876543",
                ServicioId = 2,
                FechaHora = new DateTime(2026, 5, 15, 14, 0, 0)
            };
            await _service.Guardar(cita1);
            await _service.Guardar(cita2);

            var horasOcupadas = await _service.ObtenerHorasOcupadas(new DateTime(2026, 5, 15));
            
            Assert.Equal(2, horasOcupadas.Count);
            Assert.Contains(horasOcupadas, h => h.Contains("9:00"));
            Assert.Contains(horasOcupadas, h => h.Contains("2:00"));
        }

        [Fact]
        public async Task Eliminar_CitaExistente_DevuelveTrue()
        {
            var cita = new Cita
            {
                NombreCompleto = "Juan Pérez",
                Telefono = "8091234567",
                ServicioId = 1,
                FechaHora = DateTime.Today.AddDays(1)
            };
            await _service.Guardar(cita);

            var resultado = await _service.Eliminar(cita.Id);
            
            Assert.True(resultado);
        }

        [Fact]
        public async Task Eliminar_CitaInexistente_DevuelveFalse()
        {
            var resultado = await _service.Eliminar(999);
            
            Assert.False(resultado);
        }

        [Fact]
        public void EsDiaLibre_Martes_DevuelveTrue()
        {
            var martes = new DateTime(2026, 5, 12); // Un martes
            Assert.True(_service.EsDiaLibre(martes));
        }

        [Fact]
        public void EsDiaLibre_OtroDia_DevuelveFalse()
        {
            var lunes = new DateTime(2026, 5, 11);
            var miercoles = new DateTime(2026, 5, 13);
            
            Assert.False(_service.EsDiaLibre(lunes));
            Assert.False(_service.EsDiaLibre(miercoles));
        }

        [Fact]
        public void EsDomingo_Domingo_DevuelveTrue()
        {
            var domingo = new DateTime(2026, 5, 17); // Un domingo
            Assert.True(domingo.DayOfWeek == DayOfWeek.Sunday);
        }

        [Fact]
        public void EsDomingo_OtroDia_DevuelveFalse()
        {
            var lunes = new DateTime(2026, 5, 11);
            Assert.False(lunes.DayOfWeek == DayOfWeek.Sunday);
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
