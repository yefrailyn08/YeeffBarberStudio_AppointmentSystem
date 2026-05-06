using Microsoft.EntityFrameworkCore;
using YeeffBarber_AppointmentSystem.Data.Modelos;

namespace YeeffBarber_AppointmentSystem.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cita> Citas { get; set; } = null!;
        public virtual DbSet<Servicio> Servicios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=YeeffBarberDb;Integrated Security=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cita>(entity =>
            {
                entity.ToTable("Citas");
                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsRequired();
                entity.Property(e => e.FechaHora).IsRequired();
                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("GETDATE()");
                
                entity.HasOne(d => d.Servicio)
                    .WithMany()
                    .HasForeignKey(d => d.ServicioId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.ToTable("Servicios");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500);
                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(18,2)");
                entity.Property(e => e.Activo)
                    .HasDefaultValue(true);
                entity.Property(e => e.FechaRegistro)
                    .HasDefaultValueSql("GETDATE()");
            });
        }
    }
}
