using YeeffBarber_AppointmentSystem.UI.Servicios;

namespace YeeffBarber_AppointmentSystem.Test.Servicios
{
    public class ServicioServiceTests
    {
        [Fact]
        public void Constructor_InicializaSinServiciosSeleccionados()
        {
            var service = new ServicioService();
            Assert.False(service.HayServiciosSeleccionados());
        }

        [Fact]
        public void SeleccionarServicio_MarcaServicioComoSeleccionado()
        {
            var service = new ServicioService();
            service.SeleccionarServicio("Corte de pelo", true);
            Assert.True(service.HayServiciosSeleccionados());
        }

        [Fact]
        public void ObtenerServiciosSeleccionados_DevuelveSoloSeleccionados()
        {
            var service = new ServicioService();
            service.SeleccionarServicio("Corte de pelo", true);
            service.SeleccionarServicio("Corte de niños", true);

            var resultado = service.ObtenerServiciosSeleccionados();

            Assert.Contains("Corte de pelo", resultado);
            Assert.Contains("Corte de niños", resultado);
            Assert.DoesNotContain("Cerquillo y barba", resultado);
        }

        [Fact]
        public void ObtenerServiciosSeleccionados_DevuelveStringVacioSiNoHaySeleccion()
        {
            var service = new ServicioService();
            var resultado = service.ObtenerServiciosSeleccionados();
            Assert.Equal(string.Empty, resultado);
        }

        [Fact]
        public void HayServiciosSeleccionados_DevuelveFalseSiNingunoSeleccionado()
        {
            var service = new ServicioService();
            Assert.False(service.HayServiciosSeleccionados());
        }

        [Fact]
        public void HayServiciosSeleccionados_DevuelveTrueSiAlMenosUnoSeleccionado()
        {
            var service = new ServicioService();
            service.SeleccionarServicio("Cerquillo y barba", true);
            Assert.True(service.HayServiciosSeleccionados());
        }

        [Fact]
        public void GetServiciosDisponibles_DevuelveLosTresServicios()
        {
            var service = new ServicioService();
            var servicios = service.GetServiciosDisponibles();

            Assert.Equal(3, servicios.Count);
            Assert.Contains("Corte de pelo", servicios);
            Assert.Contains("Cerquillo y barba", servicios);
            Assert.Contains("Corte de niños", servicios);
        }

        [Fact]
        public void LimpiarSeleccion_DeseleccionaTodosLosServicios()
        {
            var service = new ServicioService();
            service.SeleccionarServicio("Corte de pelo", true);
            service.SeleccionarServicio("Cerquillo y barba", true);
            service.SeleccionarServicio("Corte de niños", true);

            service.LimpiarSeleccion();

            Assert.False(service.HayServiciosSeleccionados());
            Assert.Equal(string.Empty, service.ObtenerServiciosSeleccionados());
        }

        [Fact]
        public void SeleccionarServicio_NoAfectaServicioInexistente()
        {
            var service = new ServicioService();
            service.SeleccionarServicio("Servicio inexistente", true);
            Assert.False(service.HayServiciosSeleccionados());
        }

        [Fact]
        public void SeleccionarServicio_DeseleccionarNoAfectaOtros()
        {
            var service = new ServicioService();
            service.SeleccionarServicio("Corte de pelo", true);
            service.SeleccionarServicio("Corte de niños", true);
            service.SeleccionarServicio("Corte de pelo", false);

            var resultado = service.ObtenerServiciosSeleccionados();
            Assert.Contains("Corte de niños", resultado);
            Assert.DoesNotContain("Corte de pelo", resultado);
        }
    }
}
