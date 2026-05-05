namespace YeeffBarber_AppointmentSystem.UI.Servicios
{
    public class ServicioService
    {
        private readonly Dictionary<string, bool> _serviciosSeleccionados;

        public ServicioService()
        {
            _serviciosSeleccionados = new Dictionary<string, bool>
            {
                { "Corte de pelo", false },
                { "Cerquillo y barba", false },
                { "Corte de niños", false }
            };
        }

        public void SeleccionarServicio(string servicio, bool seleccionado)
        {
            if (_serviciosSeleccionados.ContainsKey(servicio))
            {
                _serviciosSeleccionados[servicio] = seleccionado;
            }
        }

        public string ObtenerServiciosSeleccionados()
        {
            var servicios = new List<string>();
            foreach (var kvp in _serviciosSeleccionados)
            {
                if (kvp.Value)
                {
                    servicios.Add(kvp.Key);
                }
            }
            return string.Join(", ", servicios);
        }

        public bool HayServiciosSeleccionados()
        {
            return _serviciosSeleccionados.Values.Any(s => s);
        }

        public List<string> GetServiciosDisponibles()
        {
            return _serviciosSeleccionados.Keys.ToList();
        }

        public void LimpiarSeleccion()
        {
            var keys = _serviciosSeleccionados.Keys.ToList();
            foreach (var key in keys)
            {
                _serviciosSeleccionados[key] = false;
            }
        }
    }
}
