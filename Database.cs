using System.Data.SqlClient;
using System.Windows.Forms;

namespace YeeffBarber_AppointmentSystem
{
    public class Database
    {
        private static string connectionString = @"Server=localhost\SQLEXPRESS;Database=YeeffBarberDb;Integrated Security=True;TrustServerCertificate=True;";

        public static void CrearTablaSiNoExiste()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Citas')
                    BEGIN
                        DROP TABLE Citas
                    END
                    CREATE TABLE Citas (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        NombreCompleto NVARCHAR(100) NOT NULL,
                        Telefono NVARCHAR(20) NOT NULL,
                        Servicios NVARCHAR(200) NOT NULL,
                        Fecha DATE NOT NULL,
                        Hora NVARCHAR(20) NOT NULL,
                        FechaRegistro DATETIME DEFAULT GETDATE()
                    )";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public static bool GuardarCita(string nombre, string telefono, string servicios, DateTime fecha, string hora)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Citas (NombreCliente, TelefonoCliente, ServicioID, FechaHora) VALUES (@nombre, @telefono, @servicios, @fecha + ' ' + @hora)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@telefono", telefono);
                    cmd.Parameters.AddWithValue("@servicios", servicios);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@hora", hora);
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}