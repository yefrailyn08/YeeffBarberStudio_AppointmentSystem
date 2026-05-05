namespace YeeffBarber_AppointmentSystem.Data.Modelos
{
    public class Cita
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Servicios { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public DateTime FechaRegistro { get; set; }

        public Cita()
        {
            FechaRegistro = DateTime.Now;
        }

        public bool TieneDatosCompletos()
        {
            return !string.IsNullOrWhiteSpace(NombreCompleto)
                && !string.IsNullOrWhiteSpace(Telefono)
                && !string.IsNullOrWhiteSpace(Servicios)
                && FechaHora != default;
        }

        public string Resumen()
        {
            return $"{NombreCompleto} - {Servicios} - {FechaHora:dd/MM/yyyy HH:mm}";
        }
    }
}
