using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using YeeffBarber_AppointmentSystem.Data.Context;
using YeeffBarber_AppointmentSystem.Data.Modelos;
using YeeffBarber_AppointmentSystem.UI.Servicios;

namespace YeeffBarber_AppointmentSystem.UI
{
    public class LoginForm : Form
    {
        private TextBox? txtEmail;
        private TextBox? txtContrasena;
        private TextBox? txtNombre;
        private TextBox? txtConfirmarContrasena;
        private Button? btnIniciarSesion;
        private Button? btnRegistrarse;
        private Button? btnToggleModo;
        private Label? lblTitulo;
        private Label? lblModo;
        private LinkLabel? lnkOlvideContrasena;
        private CheckBox? chkMostrarContrasena;
        private bool esModoRegistro = false;
        private Panel? panelFormulario;
        private UsuarioService? _usuarioService;

        public LoginForm()
        {
            InitializeComponent();
            InitializeServices();
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
                _usuarioService = new UsuarioService(context);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar servicios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.Text = "YEEFF BARBER STUDIO - Iniciar Sesión";
            this.BackColor = Color.FromArgb(18, 18, 18);
            this.ClientSize = new Size(400, 550);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 10F);

            lblTitulo = new Label();
            lblTitulo.Text = "YEEFF BARBER";
            lblTitulo.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(212, 175, 55);
            lblTitulo.Location = new Point(60, 30);
            lblTitulo.Size = new Size(280, 45);
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            lblModo = new Label();
            lblModo.Text = "INICIAR SESIÓN";
            lblModo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblModo.ForeColor = Color.White;
            lblModo.Location = new Point(100, 90);
            lblModo.Size = new Size(200, 30);
            lblModo.TextAlign = ContentAlignment.MiddleCenter;

            panelFormulario = new Panel();
            panelFormulario.Location = new Point(40, 130);
            panelFormulario.Size = new Size(320, 280);
            panelFormulario.AutoScroll = true;

            CrearControlesFormulario();
            
            btnIniciarSesion = new Button();
            btnIniciarSesion.Text = "INICIAR SESIÓN";
            btnIniciarSesion.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnIniciarSesion.BackColor = Color.FromArgb(212, 175, 55);
            btnIniciarSesion.ForeColor = Color.Black;
            btnIniciarSesion.FlatStyle = FlatStyle.Flat;
            btnIniciarSesion.Location = new Point(40, 420);
            btnIniciarSesion.Size = new Size(320, 45);
            btnIniciarSesion.Cursor = Cursors.Hand;
            btnIniciarSesion.FlatAppearance.BorderSize = 0;
            btnIniciarSesion.Click += btnIniciarSesion_Click;

            btnRegistrarse = new Button();
            btnRegistrarse.Text = "¿No tienes cuenta? Regístrate";
            btnRegistrarse.Font = new Font("Segoe UI", 10F);
            btnRegistrarse.BackColor = Color.Transparent;
            btnRegistrarse.ForeColor = Color.FromArgb(212, 175, 55);
            btnRegistrarse.FlatStyle = FlatStyle.Flat;
            btnRegistrarse.Location = new Point(40, 475);
            btnRegistrarse.Size = new Size(320, 30);
            btnRegistrarse.Cursor = Cursors.Hand;
            btnRegistrarse.FlatAppearance.BorderSize = 0;
            btnRegistrarse.Click += btnRegistrarse_Click;

            lnkOlvideContrasena = new LinkLabel();
            lnkOlvideContrasena.Text = "¿Olvidaste tu contraseña?";
            lnkOlvideContrasena.Font = new Font("Segoe UI", 9F);
            lnkOlvideContrasena.ForeColor = Color.FromArgb(150, 150, 150);
            lnkOlvideContrasena.Location = new Point(100, 505);
            lnkOlvideContrasena.Size = new Size(200, 20);
            lnkOlvideContrasena.LinkColor = Color.FromArgb(212, 175, 55);
            lnkOlvideContrasena.TextAlign = ContentAlignment.MiddleCenter;
            lnkOlvideContrasena.Click += lnkOlvideContrasena_Click;

            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblModo);
            this.Controls.Add(panelFormulario);
            this.Controls.Add(btnIniciarSesion);
            this.Controls.Add(btnRegistrarse);
            this.Controls.Add(lnkOlvideContrasena);
        }

        private void CrearControlesFormulario()
        {
            panelFormulario!.Controls.Clear();

            if (esModoRegistro)
            {
                var lblNombre = CrearLabel("Nombre completo:", new Point(0, 20));
                txtNombre = CrearTextBox(new Point(0, 45), "Ingresa tu nombre");
                panelFormulario.Controls.Add(lblNombre);
                panelFormulario.Controls.Add(txtNombre);
            }

            var lblUsuario = CrearLabel(esModoRegistro ? "Nombre de usuario:" : "Nombre de usuario:", new Point(0, esModoRegistro ? 85 : 20));
            txtEmail = CrearTextBox(new Point(0, esModoRegistro ? 110 : 45), "Ingresa tu nombre de usuario");
            panelFormulario.Controls.Add(lblUsuario);
            panelFormulario.Controls.Add(txtEmail);

            var lblContrasena = CrearLabel("Contraseña:", new Point(0, esModoRegistro ? 150 : 85));
            txtContrasena = CrearTextBox(new Point(0, esModoRegistro ? 175 : 110), "Contraseña");
            txtContrasena.PasswordChar = '*';
            panelFormulario.Controls.Add(lblContrasena);
            panelFormulario.Controls.Add(txtContrasena);

            if (esModoRegistro)
            {
                var lblConfirmar = CrearLabel("Confirmar contraseña:", new Point(0, 215));
                txtConfirmarContrasena = CrearTextBox(new Point(0, 240), "Confirmar contraseña");
                txtConfirmarContrasena.PasswordChar = '*';
                panelFormulario.Controls.Add(lblConfirmar);
                panelFormulario.Controls.Add(txtConfirmarContrasena);

                chkMostrarContrasena = new CheckBox();
                chkMostrarContrasena.Text = "Mostrar contraseñas";
                chkMostrarContrasena.ForeColor = Color.White;
                chkMostrarContrasena.Location = new Point(0, 275);
                chkMostrarContrasena.AutoSize = true;
                chkMostrarContrasena.CheckedChanged += (s, e) =>
                {
                    if (txtContrasena != null)
                        txtContrasena.PasswordChar = chkMostrarContrasena!.Checked ? '\0' : '*';
                    if (txtConfirmarContrasena != null)
                        txtConfirmarContrasena.PasswordChar = chkMostrarContrasena!.Checked ? '\0' : '*';
                };
                panelFormulario.Controls.Add(chkMostrarContrasena);
            }
            else
            {
                chkMostrarContrasena = new CheckBox();
                chkMostrarContrasena.Text = "Mostrar contraseña";
                chkMostrarContrasena.ForeColor = Color.White;
                chkMostrarContrasena.Location = new Point(0, 145);
                chkMostrarContrasena.AutoSize = true;
                chkMostrarContrasena.CheckedChanged += (s, e) =>
                {
                    if (txtContrasena != null)
                        txtContrasena.PasswordChar = chkMostrarContrasena!.Checked ? '\0' : '*';
                };
                panelFormulario.Controls.Add(chkMostrarContrasena);
            }
        }

        private Label CrearLabel(string texto, Point location)
        {
            var label = new Label();
            label.Text = texto;
            label.ForeColor = Color.FromArgb(180, 180, 180);
            label.Font = new Font("Segoe UI", 10F);
            label.Location = location;
            label.Size = new Size(320, 20);
            return label;
        }

        private TextBox CrearTextBox(Point location, string placeholder)
        {
            var textBox = new TextBox();
            textBox.Location = location;
            textBox.Size = new Size(320, 28);
            textBox.Font = new Font("Segoe UI", 11F);
            textBox.BackColor = Color.FromArgb(40, 40, 40);
            textBox.ForeColor = Color.White;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;
            
            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.White;
                }
            };
            
            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                }
            };
            
            return textBox;
        }

        private void btnIniciarSesion_Click(object? sender, EventArgs e)
        {
            if (esModoRegistro)
            {
                Registrarse();
            }
            else
            {
                IniciarSesion();
            }
        }

        private async void IniciarSesion()
        {
            try
            {
                var nombreUsuario = txtEmail!.Text.Trim();
                var contrasena = txtContrasena!.Text;

                if (string.IsNullOrEmpty(nombreUsuario) || nombreUsuario == "Ingresa tu nombre de usuario")
                {
                    MessageBox.Show("Por favor ingresa tu nombre de usuario", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(contrasena))
                {
                    MessageBox.Show("Por favor ingresa tu contraseña", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var usuario = await _usuarioService!.IniciarSesion(nombreUsuario, contrasena);
                
                if (usuario != null)
                {
                    MessageBox.Show($"Bienvenido, {usuario.Nombre}!", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var formPrincipal = new Form1(usuario);
                    this.Hide();
                    formPrincipal.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Nombre de usuario o contraseña incorrectos", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Registrarse()
        {
            try
            {
                var nombre = txtNombre!.Text.Trim();
                var nombreUsuario = txtEmail!.Text.Trim();
                var contrasena = txtContrasena!.Text;
                var confirmarContrasena = txtConfirmarContrasena!.Text;

                if (string.IsNullOrEmpty(nombre) || nombre == "Ingresa tu nombre")
                {
                    MessageBox.Show("Por favor ingresa tu nombre", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(nombreUsuario) || nombreUsuario == "Ingresa tu nombre de usuario")
                {
                    MessageBox.Show("Por favor ingresa un nombre de usuario", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(contrasena))
                {
                    MessageBox.Show("Por favor ingresa una contraseña", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (contrasena.Length < 6)
                {
                    MessageBox.Show("La contraseña debe tener al menos 6 caracteres", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (contrasena != confirmarContrasena)
                {
                    MessageBox.Show("Las contraseñas no coinciden", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var registroExitoso = await _usuarioService!.Registrarse(nombre, nombreUsuario, null, contrasena);
                
                if (registroExitoso)
                {
                    MessageBox.Show("¡Registro exitoso! Ahora puedes iniciar sesión.", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CambiarModo();
                }
                else
                {
                    MessageBox.Show("El nombre de usuario ya está registrado", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrarse: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistrarse_Click(object? sender, EventArgs e)
        {
            CambiarModo();
        }

        private void CambiarModo()
        {
            esModoRegistro = !esModoRegistro;
            
            if (esModoRegistro)
            {
                lblModo!.Text = "REGISTRARSE";
                btnIniciarSesion!.Text = "REGISTRARME";
                btnRegistrarse!.Text = "¿Ya tienes cuenta? Inicia sesión";
                lnkOlvideContrasena!.Visible = false;
            }
            else
            {
                lblModo!.Text = "INICIAR SESIÓN";
                btnIniciarSesion!.Text = "INICIAR SESIÓN";
                btnRegistrarse!.Text = "¿No tienes cuenta? Regístrate";
                lnkOlvideContrasena!.Visible = true;
            }
            
            CrearControlesFormulario();
        }

        private void lnkOlvideContrasena_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Contacta al administrador para recuperar tu contraseña", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}