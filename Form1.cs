using System.Drawing;
using System.Data.SqlClient;

namespace YeeffBarber_AppointmentSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            chkCorte.CheckedChanged += Service_CheckedChanged;
            chkBarba.CheckedChanged += Service_CheckedChanged;
            chkNinos.CheckedChanged += Service_CheckedChanged;
            btnConfirmar.Click += BtnConfirmar_Click;
            btnVerCitas.Click += (s, e) => new CitasForm().Show();

            txtNombre.Text = "Nombre completo";
            txtNombre.ForeColor = Color.Gray;
            txtNombre.Enter += txtNombre_Enter;
            txtNombre.Leave += txtNombre_Leave;

            txtTelefono.Text = "Teléfono";
            txtTelefono.ForeColor = Color.Gray;
            txtTelefono.Enter += txtTelefono_Enter;
            txtTelefono.Leave += txtTelefono_Leave;
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
            if (txtTelefono.Text == "Teléfono")
            {
                txtTelefono.Text = "";
                txtTelefono.ForeColor = Color.White;
            }
        }

        private void txtTelefono_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                txtTelefono.Text = "Teléfono";
                txtTelefono.ForeColor = Color.Gray;
            }
        }

        private void Service_CheckedChanged(object? sender, EventArgs e)
        {
        }

        private void BtnConfirmar_Click(object? sender, EventArgs e)
        {
            string servicios = "";
            if (chkCorte.Checked) servicios += "Corte de pelo, ";
            if (chkBarba.Checked) servicios += "Cerquillo y barba, ";
            if (chkNinos.Checked) servicios += "Corte de niños, ";

            if (servicios == "")
            {
                MessageBox.Show("Por favor selecciona al menos un servicio", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = txtNombre.Text == "Nombre completo" ? "" : txtNombre.Text;
            string telefono = txtTelefono.Text == "Teléfono" ? "" : txtTelefono.Text;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Por favor ingresa tu nombre completo", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(telefono))
            {
                MessageBox.Show("Por favor ingresa tu teléfono", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            servicios = servicios.TrimEnd(',', ' ');
            
            bool guardado = Database.GuardarCita(nombre, telefono, servicios, dtpFecha.Value.Date, cmbHora.SelectedItem?.ToString() ?? "");
            
            if (guardado)
            {
                string mensaje = $"¡Cita confirmada!\n\n" +
                                $"Cliente: {nombre}\n" +
                                $"Teléfono: {telefono}\n" +
                                $"Servicios: {servicios}\n" +
                                $"Fecha: {dtpFecha.Value.Date:dd/MM/yyyy}\n" +
                                $"Hora: {cmbHora.SelectedItem}";
                MessageBox.Show(mensaje, "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al guardar la cita. Intenta de nuevo.", "Yeeff Barber Studio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}