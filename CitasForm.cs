using System.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace YeeffBarber_AppointmentSystem
{
    public partial class CitasForm : Form
    {
        private DataGridView dgvCitas;
        private Button btnActualizar;
        private Label lblTitulo;
        private Button btnVolver;
        private string connectionString = @"Server=localhost\SQLEXPRESS;Database=YeeffBarberDb;Integrated Security=True;TrustServerCertificate=True;";

        public CitasForm()
        {
            InitializeComponent();
            CargarCitas();
        }

        private void InitializeComponent()
        {
            this.Text = "YEEFF BARBER STUDIO - Citas";
            this.BackColor = Color.FromArgb(18, 18, 18);
            this.ClientSize = new Size(393, 852);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 10F);

            lblTitulo = new Label();
            lblTitulo.Text = "CITAS REGISTRADAS";
            lblTitulo.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(212, 175, 55);
            lblTitulo.Location = new Point(47, 25);
            lblTitulo.Size = new Size(320, 40);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            dgvCitas = new DataGridView();
            dgvCitas.Location = new Point(20, 80);
            dgvCitas.Size = new Size(353, 650);
            dgvCitas.BackgroundColor = Color.FromArgb(30, 30, 30);
            dgvCitas.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            dgvCitas.DefaultCellStyle.ForeColor = Color.White;
            dgvCitas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(212, 175, 55);
            dgvCitas.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCitas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(212, 175, 55);
            dgvCitas.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvCitas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvCitas.EnableHeadersVisualStyles = false;
            dgvCitas.BorderStyle = BorderStyle.None;
            dgvCitas.RowHeadersVisible = false;
            dgvCitas.AllowUserToAddRows = false;
            dgvCitas.AllowUserToDeleteRows = false;
            dgvCitas.ReadOnly = true;
            dgvCitas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCitas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCitas.MultiSelect = false;

            btnActualizar = new Button();
            btnActualizar.Text = "ACTUALIZAR";
            btnActualizar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnActualizar.BackColor = Color.FromArgb(212, 175, 55);
            btnActualizar.ForeColor = Color.Black;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Location = new Point(20, 750);
            btnActualizar.Size = new Size(170, 45);
            btnActualizar.Cursor = Cursors.Hand;
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.Click += (s, e) => CargarCitas();

            btnVolver = new Button();
            btnVolver.Text = "VOLVER";
            btnVolver.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnVolver.BackColor = Color.FromArgb(60, 60, 60);
            btnVolver.ForeColor = Color.White;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Location = new Point(203, 750);
            btnVolver.Size = new Size(170, 45);
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.Click += btnVolver_Click;

            this.Controls.Add(lblTitulo);
            this.Controls.Add(dgvCitas);
            this.Controls.Add(btnActualizar);
            this.Controls.Add(btnVolver);
        }

        private void CargarCitas()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt.Columns.Add("Cliente", typeof(string));
                    dt.Columns.Add("Telefono", typeof(string));
                    dt.Columns.Add("Servicios", typeof(string));
                    dt.Columns.Add("Fecha", typeof(string));
                    dt.Columns.Add("Hora", typeof(string));
                    
                    string query = "SELECT * FROM Citas";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    System.Data.DataTable tempDt = new System.Data.DataTable();
                    adapter.Fill(tempDt);
                    
                    foreach (System.Data.DataRow tempRow in tempDt.Rows)
                    {
                        try
                        {
                            DataRow row = dt.NewRow();
                            row["Cliente"] = tempRow["NombreCliente"]?.ToString() ?? "";
                            row["Telefono"] = tempRow["TelefonoCliente"]?.ToString() ?? "";
                            row["Servicios"] = tempRow["ServicioID"]?.ToString() ?? "";
                            string fechaHora = tempRow["FechaHora"]?.ToString() ?? "";
                            if (fechaHora.Contains(" "))
                            {
                                string[] partes = fechaHora.Split(' ');
                                row["Fecha"] = partes[0];
                                row["Hora"] = partes.Length > 1 ? partes[1] : "";
                            }
                            else
                            {
                                row["Fecha"] = fechaHora;
                                row["Hora"] = "";
                            }
                            dt.Rows.Add(row);
                        }
                        catch
                        {
                            // Saltar fila con error
                        }
                    }

                    dgvCitas.DataSource = dt;
                    
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay citas registradas todavía.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar citas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}