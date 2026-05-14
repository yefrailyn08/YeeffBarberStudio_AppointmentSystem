using System.ComponentModel.DataAnnotations;

namespace YeeffBarber_AppointmentSystem.Data.Modelos
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string NombreUsuario { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Contrasena { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        public bool Activo { get; set; } = true;
    }
}