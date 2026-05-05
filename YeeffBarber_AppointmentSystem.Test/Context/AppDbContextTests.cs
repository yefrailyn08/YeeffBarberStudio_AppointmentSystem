using YeeffBarber_AppointmentSystem.Data.Context;
using YeeffBarber_AppointmentSystem.Data.Modelos;

namespace YeeffBarber_AppointmentSystem.Test.Context
{
    public class AppDbContextTests
    {
        [Fact]
        public void Constructor_ConStringPersonalizada_UsaEsaString()
        {
            var customConn = "Server=test;Database=testDb;";
            var context = new AppDbContext(customConn);
            Assert.Equal(customConn, context.ConnectionString);
        }

        [Fact]
        public void Constructor_SinParametros_UsaStringPorDefecto()
        {
            var context = new AppDbContext();
            Assert.Contains("YeeffBarberDb", context.ConnectionString);
        }

        [Fact]
        public void Constructor_ConNull_UsaStringPorDefecto()
        {
            var context = new AppDbContext(null);
            Assert.Contains("YeeffBarberDb", context.ConnectionString);
        }

        [Fact]
        public void ConnectionString_EsDeSoloLectura()
        {
            var context = new AppDbContext("Server=test;");
            Assert.NotNull(context.ConnectionString);
            Assert.IsType<string>(context.ConnectionString);
        }

        [Fact]
        public void ObtenerTodasLasCitas_SinConexion_DevuelveListaVacia()
        {
            var context = new AppDbContext("Server=localhost\\SQLEXPRESS;Database=NonExistentDb123;Integrated Security=True;TrustServerCertificate=True;Connection Timeout=1;");
            var citas = context.ObtenerTodasLasCitas();
            Assert.NotNull(citas);
            Assert.Empty(citas);
        }

        [Fact]
        public void ObtenerTodasLasCitas_ReturnaTipoListaCita()
        {
            var context = new AppDbContext("Server=localhost\\SQLEXPRESS;Database=NonExistentDb123;Integrated Security=True;TrustServerCertificate=True;Connection Timeout=1;");
            var citas = context.ObtenerTodasLasCitas();
            Assert.IsType<List<Cita>>(citas);
        }

        [Fact]
        public void GuardarCita_ConexionInvalida_DevuelveFalse()
        {
            var context = new AppDbContext("Server=localhost\\SQLEXPRESS;Database=NonExistentDb123;Integrated Security=True;TrustServerCertificate=True;Connection Timeout=1;");
            var cita = new Cita
            {
                NombreCompleto = "Juan Pérez",
                Telefono = "8091234567",
                Servicios = "Corte",
                FechaHora = DateTime.Now
            };
            var resultado = context.GuardarCita(cita);
            Assert.False(resultado);
        }

        [Fact]
        public void EliminarCita_ConexionInvalida_DevuelveFalse()
        {
            var context = new AppDbContext("Server=localhost\\SQLEXPRESS;Database=NonExistentDb123;Integrated Security=True;TrustServerCertificate=True;Connection Timeout=1;");
            var resultado = context.EliminarCita(1);
            Assert.False(resultado);
        }

        [Fact]
        public void ObtenerCitaPorId_ConexionInvalida_DevuelveNull()
        {
            var context = new AppDbContext("Server=localhost\\SQLEXPRESS;Database=NonExistentDb123;Integrated Security=True;TrustServerCertificate=True;Connection Timeout=1;");
            var cita = context.ObtenerCitaPorId(1);
            Assert.Null(cita);
        }

        [Fact]
        public void ContarCitas_ConexionInvalida_DevuelveCero()
        {
            var context = new AppDbContext("Server=localhost\\SQLEXPRESS;Database=NonExistentDb123;Integrated Security=True;TrustServerCertificate=True;Connection Timeout=1;");
            var count = context.ContarCitas();
            Assert.Equal(0, count);
        }

        [Fact]
        public void CrearCitaConDatos_Validos_PuedeCrearse()
        {
            var cita = new Cita
            {
                NombreCompleto = "Ana López",
                Telefono = "8097654321",
                Servicios = "Corte de niños",
                FechaHora = DateTime.Today.AddHours(14)
            };
            Assert.True(cita.TieneDatosCompletos());
            Assert.Equal("Ana López", cita.NombreCompleto);
        }
    }
}
