using System.Drawing;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using YeeffBarber_AppointmentSystem.Data.Context;
using YeeffBarber_AppointmentSystem.Data.Modelos;
using YeeffBarber_AppointmentSystem.UI.Servicios;

namespace YeeffBarber_AppointmentSystem.UI
{
    public partial class Form1 : Form
    {
        private CitaService? _citaService;
        private ServicioService? _servicioService;
        private List<Servicio> _serviciosDisponibles = new();
        private Servicio? _servicioSeleccionado;

        public Form1()
        {
            InitializeComponent();
            InitializeServices();
            LoadServicios();
            
            chkCorte.CheckedChanged += Service_CheckedChanged;
            chkBarba.CheckedChanged += Service_CheckedChanged;
            chkNinos.CheckedChanged += Service_CheckedChanged;
            btnConfirmar.Click += BtnConfirmar_Click;
            btnVerCitas.Click += (s, e) => new CitasForm().Show();

            _servicioSeleccionado = _serviciosDisponibles.FirstOrDefault();
            if (_serviciosDisponibles.Any(s => s.Nombre.Contains("Corte de pelo")))
                chkCorte.Checked = true;

            txtNombre.Text = "Nombre completo";
            txtNombre.ForeColor = Color.Gray;
            txtNombre.Enter += txtNombre_Enter;
            txtNombre.Leave += txtNombre_Leave;

            txtTelefono.Text = "TelÃ©fono";
            txtTelefono.ForeColor = Color.Gray;
            txtTelefono.Enter += txtTelefono_Enter;
            txtTelefono.Leave += txtTelefono_Leave;
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
                
                // Seed initial services if empty
                if (!context.Servicios.Any())
                {
                    context.Servicios.Add(new Servicio { Nombre = "Corte de pelo", Precio = 500, DuracionMinutos = 30, Activo = true, FechaRegistro = DateTime.UtcNow });
                    context.Servicios.Add(new Servicio { Nombre = "Cerquillo y barba", Precio = 700, DuracionMinutos = 45, Activo = true, FechaRegistro = DateTime.UtcNow });
                    context.Servicios.Add(new Servicio { Nombre = "Corte de niños", Precio = 300, DuracionMinutos = 20, Activo = true, FechaRegistro = DateTime.UtcNow });
                    context.SaveChanges();
                }
                
                _citaService = new CitaService(context);
                _servicioService = new ServicioService(context);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar servicios: {ex.Message}\n\nDetalles: {ex.InnerException?.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadServicios()
        {
            try
            {
                if (_servicioService == null)
                    return;
                _serviciosDisponibles = await _servicioService.GetServiciosDisponibles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar servicios: {ex.Message}\n\nDetalles: {ex.InnerException?.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNombre_Enter(object? sender, EventArgs e)
        {
            if (txtNombre.Text == "Nombre completo")
            {
                txtNombre.Text = "";
                txtNombre.ForeColor = Color.White;
            }
        }

        private void txtNombre_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                txtNombre.Text = "Nombre completo";
                txtNombre.ForeColor = Color.Gray;
            }
        }

        private void txtTelefono_Enter(object? sender, EventArgs e)
        {
            if (txtTelefono.Text == "TelÃ©fono")
            {
                txtTelefono.Text = "";
                txtTelefono.ForeColor = Color.White;
            }
        }

        private void txtTelefono_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                txtTelefono.Text = "TelÃ©fono";
                txtTelefono.ForeColor = Color.Gray;
            }
        }

        private void Service_CheckedChanged(object? sender, EventArgs e)
        {
            if (sender is CheckBox chk && chk.Checked)
            {
                string servicioNombre = chk.Text switch
                {
                    "Corte de pelo" => "Corte de pelo",
                    "Cerquillo y barba" => "Cerquillo y barba",
                    "Corte de niÃ±os" => "Corte de niÃ±os",
                    _ => ""
                };

                _servicioSeleccionado = _serviciosDisponibles
                    .FirstOrDefault(s => s.Nombre == servicioNombre);

                if (_servicioSeleccionado != null)
                {
                    var serviceNames = new[] { "Corte de pelo", "Cerquillo y barba", "Corte de niÃ±os" };
                    foreach (var name in serviceNames)
                    {
                        if (name != servicioNombre)
                        {
                            var cb = name switch
                            {
                                "Corte de pelo" => chkCorte,
                                "Cerquillo y barba" => chkBarba,
                                "Corte de niÃ±os" => chkNinos,
                                _ => null
                            };
                            if (cb != null) cb.Checked = false;
                        }
                    }
                }
            }
        }

        private async void BtnConfirmar_Click(object? sender, EventArgs e)
        {
            string nombre = txtNombre.Text == "Nombre completo" ? "" : txtNombre.Text;
            string telefono = txtTelefono.Text == "Telefono" ? "" : txtTelefono.Text;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Por favor ingresa tu nombre completo", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(telefono))
            {
                MessageBox.Show("Por favor ingresa tu telÃ©fono", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_citaService == null)
                return;

            if (!_citaService.ValidarNombre(nombre) || !_citaService.ValidarTelefono(telefono))
            {
                MessageBox.Show("Por favor verifica los datos ingresados", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_servicioSeleccionado == null)
            {
                MessageBox.Show("Por favor selecciona un servicio", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que el servicio tenga un ID válido
            if (_servicioSeleccionado.Id <= 0)
            {
                MessageBox.Show("El servicio seleccionado no es válido", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que los controles no sean null
            if (dtpFecha == null || cmbHora == null)
            {
                MessageBox.Show("Error: Controles de fecha/hora no inicializados", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar que haya un elemento seleccionado en cmbHora
            if (cmbHora.SelectedItem == null)
            {
                MessageBox.Show("Por favor selecciona una hora", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var horaStr = cmbHora.SelectedItem?.ToString() ?? "09:00 AM";
                var hora = DateTime.ParseExact(horaStr, "hh:mm tt", CultureInfo.InvariantCulture);
                
                var cita = new YeeffBarber_AppointmentSystem.Data.Modelos.Cita
                {
                    NombreCompleto = nombre,
                    Telefono = telefono,
                    ServicioId = _servicioSeleccionado.Id,
                    FechaHora = dtpFecha.Value.Date.Add(hora.TimeOfDay)
                };
                
                bool guardado = await _citaService.Guardar(cita);
                
                if (guardado && _citaService != null)
                {
                    string mensaje = _citaService.FormatearConfirmacion(nombre, _servicioSeleccionado.Nombre, cita.FechaHora);
                    MessageBox.Show(mensaje, "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al guardar la cita. Intenta de nuevo.", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar cita: {ex.Message}\n\nDetalles: {ex.InnerException?.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblTitulo_Click(object? sender, EventArgs e)
        {
        }

        private void Form1_Load_1(object? sender, EventArgs e)
        {
        }
    }
}
