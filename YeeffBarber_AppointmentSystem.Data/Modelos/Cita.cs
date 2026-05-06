using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YeeffBarber_AppointmentSystem.Data.Modelos
{
    public class Cita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        public int ServicioId { get; set; }

        [ForeignKey("ServicioId")]
        public virtual Servicio? Servicio { get; set; }

        public DateTime FechaHora { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        public bool TieneDatosCompletos()
        {
            return !string.IsNullOrWhiteSpace(NombreCompleto)
                && !string.IsNullOrWhiteSpace(Telefono)
                && ServicioId > 0
                && FechaHora != default;
        }

        public string Resumen()
        {
            return $"{NombreCompleto} - {Servicio?.Nombre ?? "Servicio"} - {FechaHora:dd/MM/yyyy HH:mm}";
        }
    }
}
