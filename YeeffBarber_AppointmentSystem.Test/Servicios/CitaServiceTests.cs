using YeeffBarber_AppointmentSystem.UI.Servicios;

namespace YeeffBarber_AppointmentSystem.Test.Servicios
{
    public class CitaServiceTests
    {
        private readonly CitaService _service;

        public CitaServiceTests()
        {
            _service = new CitaService();
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
        public void ValidarTelefono_TelefonoConGuiones_DevuelveTrue()
        {
            Assert.True(_service.ValidarTelefono("809-123-4567"));
        }

        [Fact]
        public void ValidarTelefono_TelefonoConLetras_DevuelveFalse()
        {
            Assert.False(_service.ValidarTelefono("809abc4567"));
        }

        [Fact]
        public void ValidarFecha_FechaFutura_DevuelveTrue()
        {
            Assert.True(_service.ValidarFecha(DateTime.Today.AddDays(1)));
        }

        [Fact]
        public void ValidarFecha_FechaHoy_DevuelveTrue()
        {
            Assert.True(_service.ValidarFecha(DateTime.Today));
        }

        [Fact]
        public void ValidarFecha_FechaPasada_DevuelveFalse()
        {
            Assert.False(_service.ValidarFecha(DateTime.Today.AddDays(-1)));
        }

        [Fact]
        public void ValidarHora_HoraValida_DevuelveTrue()
        {
            Assert.True(_service.ValidarHora("10:00"));
        }

        [Fact]
        public void ValidarHora_HoraVacia_DevuelveFalse()
        {
            Assert.False(_service.ValidarHora(""));
        }

        [Fact]
        public void ValidarCitaCompleta_CitaValida_DevuelveStringVacio()
        {
            var resultado = _service.ValidarCitaCompleta(
                "Juan Pérez",
                "8091234567",
                "Corte de pelo",
                DateTime.Today.AddDays(1),
                "10:00"
            );
            Assert.Equal(string.Empty, resultado);
        }

        [Fact]
        public void ValidarCitaCompleta_NombreInvalido_DevuelveError()
        {
            var resultado = _service.ValidarCitaCompleta(
                "Jo",
                "8091234567",
                "Corte de pelo",
                DateTime.Today.AddDays(1),
                "10:00"
            );
            Assert.Contains("nombre", resultado, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void ValidarCitaCompleta_TelefonoInvalido_DevuelveError()
        {
            var resultado = _service.ValidarCitaCompleta(
                "Juan Pérez",
                "123",
                "Corte de pelo",
                DateTime.Today.AddDays(1),
                "10:00"
            );
            Assert.Contains("teléfono", resultado, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void ValidarCitaCompleta_SinServicios_DevuelveError()
        {
            var resultado = _service.ValidarCitaCompleta(
                "Juan Pérez",
                "8091234567",
                "",
                DateTime.Today.AddDays(1),
                "10:00"
            );
            Assert.Contains("servicio", resultado, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void ValidarCitaCompleta_FechaPasada_DevuelveError()
        {
            var resultado = _service.ValidarCitaCompleta(
                "Juan Pérez",
                "8091234567",
                "Corte de pelo",
                DateTime.Today.AddDays(-1),
                "10:00"
            );
            Assert.Contains("fecha", resultado, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void ValidarCitaCompleta_SinHora_DevuelveError()
        {
            var resultado = _service.ValidarCitaCompleta(
                "Juan Pérez",
                "8091234567",
                "Corte de pelo",
                DateTime.Today.AddDays(1),
                ""
            );
            Assert.Contains("hora", resultado, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void FormatearConfirmacion_DevuelveMensajeConDatos()
        {
            var fecha = new DateTime(2026, 5, 10);
            var resultado = _service.FormatearConfirmacion(
                "Juan Pérez",
                "8091234567",
                "Corte de pelo",
                fecha,
                "10:00"
            );

            Assert.Contains("Juan Pérez", resultado);
            Assert.Contains("8091234567", resultado);
            Assert.Contains("Corte de pelo", resultado);
            Assert.Contains("10/05/2026", resultado);
            Assert.Contains("10:00", resultado);
        }

        [Fact]
        public void LimpiarTextoPlaceholder_TextoIgualPlaceholder_DevuelveVacio()
        {
            var resultado = _service.LimpiarTextoPlaceholder("Nombre completo", "Nombre completo");
            Assert.Equal(string.Empty, resultado);
        }

        [Fact]
        public void LimpiarTextoPlaceholder_TextoDiferentePlaceholder_DevuelveTextoOriginal()
        {
            var resultado = _service.LimpiarTextoPlaceholder("Juan Pérez", "Nombre completo");
            Assert.Equal("Juan Pérez", resultado);
        }
    }
}
