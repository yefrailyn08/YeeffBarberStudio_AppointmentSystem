using System.Drawing;
using System.Data;
using Microsoft.EntityFrameworkCore;
using YeeffBarber_AppointmentSystem.Data.Context;
using YeeffBarber_AppointmentSystem.UI.Servicios;
using YeeffBarber_AppointmentSystem.Data.Modelos;

namespace YeeffBarber_AppointmentSystem.UI
{
    public partial class CitasForm : Form
    {
        private DataGridView? dgvCitas;
        private Button? btnActualizar;
        private Button? btnEliminar;
        private Label? lblTitulo;
        private Button? btnVolver;
        private CitaService? _citaService;
        private List<int> _citaIds = new List<int>();

        public CitasForm()
        {
            InitializeComponent();
            InitializeServices();
            CargarCitas();
        }

        private void InitializeServices()
        {
            try
            {
                var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=YeeffBarberDb;Integrated Security=True;TrustServerCertificate=True;")
                    .Options;
                var context = new AppDbContext(options);
                context.Database.EnsureCreated();
                _citaService = new CitaService(context);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar servicios: {ex.Message}\n\nDetalles: {ex.InnerException?.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            btnActualizar.Size = new Size(110, 45);
            btnActualizar.Cursor = Cursors.Hand;
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.Click += (s, e) => CargarCitas();

            btnEliminar = new Button();
            btnEliminar.Text = "ELIMINAR";
            btnEliminar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnEliminar.BackColor = Color.FromArgb(180, 50, 50);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Location = new Point(140, 750);
            btnEliminar.Size = new Size(110, 45);
            btnEliminar.Cursor = Cursors.Hand;
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.Click += btnEliminar_Click;

            btnVolver = new Button();
            btnVolver.Text = "VOLVER";
            btnVolver.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnVolver.BackColor = Color.FromArgb(60, 60, 60);
            btnVolver.ForeColor = Color.White;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Location = new Point(260, 750);
            btnVolver.Size = new Size(113, 45);
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.Click += btnVolver_Click;

            this.Controls.Add(lblTitulo);
            this.Controls.Add(dgvCitas);
            this.Controls.Add(btnActualizar);
            this.Controls.Add(btnEliminar);
            this.Controls.Add(btnVolver);
        }

        private async void CargarCitas()
        {
            try
            {
                if (_citaService == null)
                    return;

                var citas = await _citaService.GetAll();

                var dt = new System.Data.DataTable();
                dt.Columns.Add("Cliente", typeof(string));
                dt.Columns.Add("Telefono", typeof(string));
                dt.Columns.Add("Servicio", typeof(string));
                dt.Columns.Add("Fecha", typeof(string));
                dt.Columns.Add("Hora", typeof(string));

                _citaIds.Clear();
                foreach (var cita in citas)
                {
                    try
                    {
                        DataRow row = dt.NewRow();
                        row["Cliente"] = cita.NombreCompleto;
                        row["Telefono"] = cita.Telefono;
                        row["Servicio"] = cita.Servicio?.Nombre ?? "N/A";
                        row["Fecha"] = cita.FechaHora.ToString("dd/MM/yyyy");
                        row["Hora"] = cita.FechaHora.ToString("HH:mm");
                        dt.Rows.Add(row);
                        _citaIds.Add(cita.Id);
                    }
                    catch
                    {
                        // Skip row with error
                    }
                }

                if (dgvCitas != null)
                    dgvCitas.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No hay citas registradas todavía.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar citas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (dgvCitas == null || dgvCitas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor selecciona una cita para eliminar.", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar esta cita?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    int selectedIndex = dgvCitas.SelectedRows[0].Index;
                    int citaId = _citaIds[selectedIndex];

                    var eliminado = await _citaService!.Eliminar(citaId);
                    
                    if (eliminado)
                    {
                        MessageBox.Show("Cita eliminada exitosamente.", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarCitas();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la cita.", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar la cita: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnVolver_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
