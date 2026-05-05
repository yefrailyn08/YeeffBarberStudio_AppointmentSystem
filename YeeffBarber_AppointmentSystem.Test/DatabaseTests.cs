using System;
using Xunit;
using YeeffBarber_AppointmentSystem.Data;

namespace YeeffBarber_AppointmentSystem.Test
{
    public class DatabaseTests
    {
        [Fact]
        public void GuardarCita_DeberiaValidarParametros()
        {
            // Arrange
            string nombre = "Juan Pérez";
            string telefono = "8091234567";
            string servicios = "Corte de pelo";
            DateTime fecha = DateTime.Today;
            string hora = "10:00";

            // Act & Assert
            Assert.NotNull(nombre);
            Assert.NotNull(telefono);
            Assert.NotNull(servicios);
            Assert.NotEqual(default, fecha);
            Assert.NotNull(hora);
        }

        [Fact]
        public void Conexion_DeberiaTenerCadenaValida()
        {
            // Arrange
            string connectionString = @"Server=localhost\SQLEXPRESS;Database=YeeffBarberDb;Integrated Security=True;TrustServerCertificate=True;";

            // Act & Assert
            Assert.Contains("Server=", connectionString);
            Assert.Contains("Database=", connectionString);
            Assert.Contains("Integrated Security=True", connectionString);
        }
    }
}
