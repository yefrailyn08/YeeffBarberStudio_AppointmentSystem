using YeeffBarber_AppointmentSystem.Data.Modelos;
using System.Data.SqlClient;

namespace YeeffBarber_AppointmentSystem.Data.Context
{
    public class AppDbContext
    {
        public string ConnectionString { get; }

        public AppDbContext(string? connectionString = null)
        {
            ConnectionString = connectionString ?? @"Server=localhost\SQLEXPRESS;Database=YeeffBarberDb;Integrated Security=True;TrustServerCertificate=True;";
        }

        public List<Cita> ObtenerTodasLasCitas()
        {
            var citas = new List<Cita>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT Id, NombreCompleto, Telefono, Servicios, FechaHora, FechaRegistro FROM Citas";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            citas.Add(new Cita
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                NombreCompleto = reader["NombreCompleto"]?.ToString() ?? "",
                                Telefono = reader["Telefono"]?.ToString() ?? "",
                                Servicios = reader["Servicios"]?.ToString() ?? "",
                                FechaHora = Convert.ToDateTime(reader["FechaHora"]),
                                FechaRegistro = reader["FechaRegistro"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["FechaRegistro"])
                                    : DateTime.Now
                            });
                        }
                    }
                }
            }
            catch
            {
                // Retornar lista vacía si hay error de conexión
            }
            return citas;
        }

        public bool GuardarCita(Cita cita)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Citas (NombreCompleto, Telefono, Servicios, FechaHora) VALUES (@nombre, @telefono, @servicios, @fechaHora)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", cita.NombreCompleto);
                        cmd.Parameters.AddWithValue("@telefono", cita.Telefono);
                        cmd.Parameters.AddWithValue("@servicios", cita.Servicios);
                        cmd.Parameters.AddWithValue("@fechaHora", cita.FechaHora);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool EliminarCita(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Citas WHERE Id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public Cita? ObtenerCitaPorId(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT Id, NombreCompleto, Telefono, Servicios, FechaHora, FechaRegistro FROM Citas WHERE Id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Cita
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    NombreCompleto = reader["NombreCompleto"]?.ToString() ?? "",
                                    Telefono = reader["Telefono"]?.ToString() ?? "",
                                    Servicios = reader["Servicios"]?.ToString() ?? "",
                                    FechaHora = Convert.ToDateTime(reader["FechaHora"]),
                                    FechaRegistro = reader["FechaRegistro"] != DBNull.Value
                                        ? Convert.ToDateTime(reader["FechaRegistro"])
                                        : DateTime.Now
                                };
                            }
                        }
                    }
                }
            }
            catch
            {
                // Retornar null si hay error
            }
            return null;
        }

        public int ContarCitas()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Citas";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        return (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}
