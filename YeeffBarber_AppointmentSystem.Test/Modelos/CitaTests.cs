using YeeffBarber_AppointmentSystem.Data.Modelos;

namespace YeeffBarber_AppointmentSystem.Test.Modelos
{
    public class CitaTests
    {
        [Fact]
        public void Constructor_InicializaFechaRegistro()
        {
            var cita = new Cita();
            Assert.True(cita.FechaRegistro <= DateTime.Now);
        }

        [Fact]
        public void Constructor_InicializaPropiedadesVacias()
        {
            var cita = new Cita();
            Assert.Equal(string.Empty, cita.NombreCompleto);
            Assert.Equal(string.Empty, cita.Telefono);
            Assert.Equal(string.Empty, cita.Servicios);
            Assert.Equal(0, cita.Id);
        }

        [Fact]
        public void TieneDatosCompletos_ConTodosLosDatos_DevuelveTrue()
        {
            var cita = new Cita
            {
                NombreCompleto = "Juan Pérez",
                Telefono = "8091234567",
                Servicios = "Corte de pelo",
                FechaHora = DateTime.Now.AddHours(1)
            };
            Assert.True(cita.TieneDatosCompletos());
        }

        [Fact]
        public void TieneDatosCompletos_SinNombre_DevuelveFalse()
        {
            var cita = new Cita
            {
                NombreCompleto = "",
                Telefono = "8091234567",
                Servicios = "Corte de pelo",
                FechaHora = DateTime.Now.AddHours(1)
            };
            Assert.False(cita.TieneDatosCompletos());
        }

        [Fact]
        public void TieneDatosCompletos_SinTelefono_DevuelveFalse()
        {
            var cita = new Cita
            {
                NombreCompleto = "Juan Pérez",
                Telefono = "",
                Servicios = "Corte de pelo",
                FechaHora = DateTime.Now.AddHours(1)
            };
            Assert.False(cita.TieneDatosCompletos());
        }

        [Fact]
        public void TieneDatosCompletos_SinServicios_DevuelveFalse()
        {
            var cita = new Cita
            {
                NombreCompleto = "Juan Pérez",
                Telefono = "8091234567",
                Servicios = "",
                FechaHora = DateTime.Now.AddHours(1)
            };
            Assert.False(cita.TieneDatosCompletos());
        }

        [Fact]
        public void TieneDatosCompletos_FechaHoraDefault_DevuelveFalse()
        {
            var cita = new Cita
            {
                NombreCompleto = "Juan Pérez",
                Telefono = "8091234567",
                Servicios = "Corte de pelo",
                FechaHora = default
            };
            Assert.False(cita.TieneDatosCompletos());
        }

        [Fact]
        public void TieneDatosCompletos_ConDatosNull_DevuelveFalse()
        {
            var cita = new Cita
            {
                NombreCompleto = null!,
                Telefono = null!,
                Servicios = null!,
                FechaHora = DateTime.Now
            };
            Assert.False(cita.TieneDatosCompletos());
        }

        [Fact]
        public void Resumen_GeneraStringConDatos()
        {
            var fecha = new DateTime(2026, 5, 10, 10, 30, 0);
            var cita = new Cita
            {
                NombreCompleto = "Juan Pérez",
                Servicios = "Corte de pelo",
                FechaHora = fecha
            };
            var resumen = cita.Resumen();
            Assert.Contains("Juan Pérez", resumen);
            Assert.Contains("Corte de pelo", resumen);
            Assert.Contains("10/05/2026", resumen);
            Assert.Contains("10:30", resumen);
        }

        [Fact]
        public void Id_SePuedeAsignar()
        {
            var cita = new Cita { Id = 5 };
            Assert.Equal(5, cita.Id);
        }

        [Fact]
        public void Propiedades_SePuedenModificar()
        {
            var cita = new Cita
            {
                NombreCompleto = "Maria",
                Telefono = "8099999999",
                Servicios = "Barba"
            };
            Assert.Equal("Maria", cita.NombreCompleto);
            Assert.Equal("8099999999", cita.Telefono);
            Assert.Equal("Barba", cita.Servicios);
        }
    }
}
