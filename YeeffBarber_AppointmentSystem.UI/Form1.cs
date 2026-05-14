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
        private Usuario? _usuarioLogueado;

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
            dtpFecha.ValueChanged += async (s, e) => await ActualizarHorasDisponibles();
            
            if (dtpFecha.Value.DayOfWeek == DayOfWeek.Tuesday)
            {
                MessageBox.Show("Los martes son día libre. Por favor selecciona otro día.", 
                    "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            ActualizarBienvenida();
            ConfigurarCamposTexto();
        }

        public Form1(Usuario usuario)
        {
            InitializeComponent();
            InitializeServices();
            LoadServicios();
            _usuarioLogueado = usuario;
            
            chkCorte.CheckedChanged += Service_CheckedChanged;
            chkBarba.CheckedChanged += Service_CheckedChanged;
            chkNinos.CheckedChanged += Service_CheckedChanged;
            btnConfirmar.Click += BtnConfirmar_Click;
            btnVerCitas.Click += (s, e) => new CitasForm().Show();
            dtpFecha.ValueChanged += async (s, e) => await ActualizarHorasDisponibles();
            
            if (dtpFecha.Value.DayOfWeek == DayOfWeek.Tuesday)
            {
                MessageBox.Show("Los martes son día libre. Por favor selecciona otro día.", 
                    "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            ActualizarBienvenida();
            ConfigurarCamposTexto();
        }

        private void ConfigurarCamposTexto()
        {
            _servicioSeleccionado = _serviciosDisponibles.FirstOrDefault();
            if (_serviciosDisponibles.Any(s => s.Nombre.Contains("Corte de pelo")))
                chkCorte.Checked = true;

            txtNombre.Text = "Nombre completo";
            txtNombre.ForeColor = Color.Gray;
            txtNombre.Enter += txtNombre_Enter;
            txtNombre.Leave += txtNombre_Leave;

            txtTelefono.Text = "Teléfono";
            txtTelefono.ForeColor = Color.Gray;
            txtTelefono.Enter += txtTelefono_Enter;
            txtTelefono.Leave += txtTelefono_Leave;
        }

        private void ActualizarBienvenida()
        {
            if (_usuarioLogueado != null)
            {
                lblTitulo.Text = $"BIENVENIDO, {_usuarioLogueado.Nombre.ToUpper()}";
            }
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

        private async Task ActualizarHorasDisponibles()
        {
            if (_citaService == null || cmbHora == null)
                return;

            // Si es martes, no mostrar horas disponibles
            if (dtpFecha.Value.DayOfWeek == DayOfWeek.Tuesday)
            {
                cmbHora.Items.Clear();
                cmbHora.Items.Add("Día libre");
                cmbHora.SelectedIndex = 0;
                return;
            }

            try
            {
                var horasOcupadas = await _citaService.ObtenerHorasOcupadas(dtpFecha.Value.Date);
                
                List<string> todasLasHoras;
                
                if (dtpFecha.Value.DayOfWeek == DayOfWeek.Saturday)
                {
                    // Sábado: 7AM-8PM sin cierre al mediodía
                    todasLasHoras = new List<string> { "7:00 AM", "8:00 AM", "9:00 AM", "10:00 AM", "11:00 AM", "12:00 PM", "2:00 PM", "3:00 PM", "4:00 PM", "5:00 PM", "6:00 PM", "7:00 PM", "8:00 PM" };
                }
                else if (dtpFecha.Value.DayOfWeek == DayOfWeek.Sunday)
                {
                    // Domingo: solo mañana 7AM-12PM
                    todasLasHoras = new List<string> { "7:00 AM", "8:00 AM", "9:00 AM", "10:00 AM", "11:00 AM" };
                }
                else
                {
                    // Lunes, Miércoles, Jueves, Viernes: 9AM-12PM y 2PM-8PM
                    todasLasHoras = new List<string> { "9:00 AM", "10:00 AM", "11:00 AM", "12:00 PM", "2:00 PM", "3:00 PM", "4:00 PM", "5:00 PM", "6:00 PM", "7:00 PM", "8:00 PM" };
                }
                
                var horasDisponibles = todasLasHoras.Where(h => !horasOcupadas.Contains(h)).ToList();
                
                var horaSeleccionada = cmbHora.SelectedItem?.ToString();
                
                cmbHora.Items.Clear();
                cmbHora.Items.AddRange(horasDisponibles.ToArray());
                
                if (horasDisponibles.Any())
                {
                    if (!string.IsNullOrEmpty(horaSeleccionada) && horasDisponibles.Contains(horaSeleccionada))
                        cmbHora.SelectedItem = horaSeleccionada;
                    else
                        cmbHora.SelectedIndex = 0;
                }
            }
            catch
            {
                // Si hay error, mantener todas las horas disponibles
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
                // Validar que no sea martes
                if (dtpFecha.Value.DayOfWeek == DayOfWeek.Tuesday)
                {
                    MessageBox.Show("Lo sentimos, los martes son día libre. Por favor selecciona otro día.", 
                        "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var horaStr = cmbHora.SelectedItem?.ToString() ?? "09:00 AM";
                var hora = DateTime.ParseExact(horaStr, "h:mm tt", CultureInfo.InvariantCulture);
                var fechaHora = dtpFecha.Value.Date.Add(hora.TimeOfDay);

                // Validar que la hora no esté ocupada
                var horaOcupada = await _citaService.ExisteCitaEnFechaYHora(fechaHora);
                if (horaOcupada)
                {
                    MessageBox.Show("Esta hora ya está ocupada. Por favor selecciona otra hora.", 
                        "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                var cita = new YeeffBarber_AppointmentSystem.Data.Modelos.Cita
                {
                    NombreCompleto = nombre,
                    Telefono = telefono,
                    ServicioId = _servicioSeleccionado.Id,
                    FechaHora = fechaHora
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
