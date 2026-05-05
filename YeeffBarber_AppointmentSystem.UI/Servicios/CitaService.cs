namespace YeeffBarber_AppointmentSystem.UI.Servicios
{
    public class CitaService
    {
        public bool ValidarNombre(string nombre)
        {
            return !string.IsNullOrWhiteSpace(nombre) && nombre.Length >= 3;
        }

        public bool ValidarTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                return false;

            var soloNumeros = telefono.Replace("-", "").Replace(" ", "").Replace("(", "").Replace(")", "");
            return soloNumeros.Length >= 10 && soloNumeros.All(char.IsDigit);
        }

        public bool ValidarFecha(DateTime fecha)
        {
            return fecha >= DateTime.Today;
        }

        public bool ValidarHora(string hora)
        {
            return !string.IsNullOrWhiteSpace(hora);
        }

        public string ValidarCitaCompleta(string nombre, string telefono, string servicios, DateTime fecha, string hora)
        {
            if (!ValidarNombre(nombre))
                return "El nombre debe tener al menos 3 caracteres";

            if (!ValidarTelefono(telefono))
                return "El teléfono debe tener al menos 10 dígitos";

            if (string.IsNullOrWhiteSpace(servicios))
                return "Debe seleccionar al menos un servicio";

            if (!ValidarFecha(fecha))
                return "La fecha debe ser hoy o una fecha futura";

            if (!ValidarHora(hora))
                return "Debe seleccionar una hora";

            return string.Empty;
        }

        public string FormatearConfirmacion(string nombre, string telefono, string servicios, DateTime fecha, string hora)
        {
            return $"¡Cita confirmada!\n\n" +
                   $"Cliente: {nombre}\n" +
                   $"Teléfono: {telefono}\n" +
                   $"Servicios: {servicios}\n" +
                   $"Fecha: {fecha:dd/MM/yyyy}\n" +
                   $"Hora: {hora}";
        }

        public string LimpiarTextoPlaceholder(string texto, string placeholder)
        {
            return texto == placeholder ? "" : texto;
        }
    }
}
