using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YeeffBarber_AppointmentSystem.Data.Modelos
{
    public class Servicio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        public int DuracionMinutos { get; set; }

        public bool Activo { get; set; } = true;

        [DataType(DataType.DateTime)]
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}
