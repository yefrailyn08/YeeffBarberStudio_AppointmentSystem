namespace YeeffBarber_AppointmentSystem.UI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        public Label lblTitulo;
        public Label lblServicios;
        public CheckBox chkCorte;
        public Label lblDescCorte;
        public CheckBox chkBarba;
        public Label lblDescBarba;
        public CheckBox chkNinos;
        public Label lblDescNinos;
        public Label lblPrecioCorte;
        public Label lblPrecioBarba;
        public Label lblPrecioNinos;
        public Label lblFecha;
        public Label lblFechaTitulo;
        public Label lblHoraTitulo;
        public Label lblTextoFecha;
        public Label lblTextoHora;
        public DateTimePicker dtpFecha;
        public ComboBox cmbHora;
        public Label lblDatos;
        public TextBox txtNombre;
        public TextBox txtTelefono;
        public Label lblNombre;
        public Label lblTelefono;
        public Button btnConfirmar;
        public Button btnVerCitas;
        public Label lblPrecio;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblServicios = new Label();
            chkCorte = new CheckBox();
            lblDescCorte = new Label();
            chkBarba = new CheckBox();
            lblDescBarba = new Label();
            chkNinos = new CheckBox();
            lblDescNinos = new Label();
            lblPrecioCorte = new Label();
            lblPrecioBarba = new Label();
            lblPrecioNinos = new Label();
            lblFecha = new Label();
            lblFechaTitulo = new Label();
            lblHoraTitulo = new Label();
            dtpFecha = new DateTimePicker();
            lblTextoFecha = new Label();
            lblTextoHora = new Label();
            cmbHora = new ComboBox();
            lblDatos = new Label();
            txtNombre = new TextBox();
            txtTelefono = new TextBox();
            btnConfirmar = new Button();
            btnVerCitas = new Button();
            lblNombre = new Label();
            lblTelefono = new Label();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(212, 175, 55);
            lblTitulo.Location = new Point(47, 25);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(320, 40);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "YEEFF BARBER STUDIO";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            lblTitulo.Click += lblTitulo_Click;
            // 
            // lblServicios
            // 
            lblServicios.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblServicios.ForeColor = Color.FromArgb(212, 175, 55);
            lblServicios.Location = new Point(30, 100);
            lblServicios.Name = "lblServicios";
            lblServicios.Size = new Size(150, 25);
            lblServicios.TabIndex = 1;
            lblServicios.Text = "SERVICIOS";
            // 
            // chkCorte
            // 
            chkCorte.BackColor = Color.FromArgb(18, 18, 18);
            chkCorte.FlatStyle = FlatStyle.Flat;
            chkCorte.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            chkCorte.ForeColor = Color.White;
            chkCorte.Location = new Point(30, 140);
            chkCorte.Name = "chkCorte";
            chkCorte.Size = new Size(200, 25);
            chkCorte.TabIndex = 2;
            chkCorte.Text = "Corte de pelo";
            chkCorte.UseVisualStyleBackColor = false;
            // 
            // lblDescCorte
            // 
            lblDescCorte.Font = new Font("Segoe UI", 11F);
            lblDescCorte.ForeColor = Color.LightGray;
            lblDescCorte.Location = new Point(35, 175);
            lblDescCorte.Name = "lblDescCorte";
            lblDescCorte.Size = new Size(330, 40);
            lblDescCorte.TabIndex = 4;
            lblDescCorte.Text = "Corte profesional + facial\na vapor";
            // 
            // chkBarba
            // 
            chkBarba.BackColor = Color.FromArgb(18, 18, 18);
            chkBarba.FlatStyle = FlatStyle.Flat;
            chkBarba.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            chkBarba.ForeColor = Color.White;
            chkBarba.Location = new Point(30, 235);
            chkBarba.Name = "chkBarba";
            chkBarba.Size = new Size(200, 25);
            chkBarba.TabIndex = 5;
            chkBarba.Text = "Cerquillo y barba";
            chkBarba.UseVisualStyleBackColor = false;
            // 
            // lblDescBarba
            // 
            lblDescBarba.Font = new Font("Segoe UI", 11F);
            lblDescBarba.ForeColor = Color.LightGray;
            lblDescBarba.Location = new Point(35, 265);
            lblDescBarba.Name = "lblDescBarba";
            lblDescBarba.Size = new Size(330, 40);
            lblDescBarba.TabIndex = 7;
            lblDescBarba.Text = "Perfilado y cuidado profesional\n+ facial a vapor";
            // 
            // chkNinos
            // 
            chkNinos.BackColor = Color.FromArgb(18, 18, 18);
            chkNinos.FlatStyle = FlatStyle.Flat;
            chkNinos.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            chkNinos.ForeColor = Color.White;
            chkNinos.Location = new Point(30, 330);
            chkNinos.Name = "chkNinos";
            chkNinos.Size = new Size(200, 25);
            chkNinos.TabIndex = 8;
            chkNinos.Text = "Corte de niños";
            chkNinos.UseVisualStyleBackColor = false;
            // 
            // lblDescNinos
            // 
            lblDescNinos.Font = new Font("Segoe UI", 11F);
            lblDescNinos.ForeColor = Color.LightGray;
            lblDescNinos.Location = new Point(35, 355);
            lblDescNinos.Name = "lblDescNinos";
            lblDescNinos.Size = new Size(330, 40);
            lblDescNinos.TabIndex = 10;
            lblDescNinos.Text = "Corte especializado para\nlos más pequeños";
            // 
            // lblPrecioCorte
            // 
            lblPrecioCorte.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblPrecioCorte.ForeColor = Color.FromArgb(212, 175, 55);
            lblPrecioCorte.Location = new Point(280, 140);
            lblPrecioCorte.Name = "lblPrecioCorte";
            lblPrecioCorte.Size = new Size(80, 25);
            lblPrecioCorte.TabIndex = 3;
            lblPrecioCorte.Text = "$700";
            // 
            // lblPrecioBarba
            // 
            lblPrecioBarba.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblPrecioBarba.ForeColor = Color.FromArgb(212, 175, 55);
            lblPrecioBarba.Location = new Point(280, 235);
            lblPrecioBarba.Name = "lblPrecioBarba";
            lblPrecioBarba.Size = new Size(80, 25);
            lblPrecioBarba.TabIndex = 6;
            lblPrecioBarba.Text = "$600";
            // 
            // lblPrecioNinos
            // 
            lblPrecioNinos.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblPrecioNinos.ForeColor = Color.FromArgb(212, 175, 55);
            lblPrecioNinos.Location = new Point(280, 320);
            lblPrecioNinos.Name = "lblPrecioNinos";
            lblPrecioNinos.Size = new Size(80, 25);
            lblPrecioNinos.TabIndex = 9;
            lblPrecioNinos.Text = "$600";
            // 
            // lblFecha
            // 
            lblFecha.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblFecha.ForeColor = Color.FromArgb(212, 175, 55);
            lblFecha.Location = new Point(30, 420);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(80, 25);
            lblFecha.TabIndex = 11;
            lblFecha.Text = "";
            // 
            // dtpFecha
            // 
            dtpFecha.Font = new Font("Segoe UI", 11F);
            dtpFecha.Location = new Point(30, 480);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(170, 27);
            dtpFecha.TabIndex = 12;
            // 
            // lblTextoFecha
            // 
            lblTextoFecha.Font = new Font("Segoe UI", 11F);
            lblTextoFecha.ForeColor = Color.LightGray;
            lblTextoFecha.Location = new Point(30, 455);
            lblTextoFecha.Name = "lblTextoFecha";
            lblTextoFecha.Size = new Size(170, 20);
            lblTextoFecha.TabIndex = 14;
            lblFechaTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblFechaTitulo.ForeColor = Color.FromArgb(212, 175, 55);
            lblFechaTitulo.Location = new Point(30, 420);
            lblFechaTitulo.Name = "lblFechaTitulo";
            lblFechaTitulo.Size = new Size(80, 25);
            lblFechaTitulo.TabIndex = 11;
            lblFechaTitulo.Text = "FECHA";
            // 
            lblHoraTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblHoraTitulo.ForeColor = Color.FromArgb(212, 175, 55);
            lblHoraTitulo.Location = new Point(210, 420);
            lblHoraTitulo.Name = "lblHoraTitulo";
            lblHoraTitulo.Size = new Size(80, 25);
            lblHoraTitulo.TabIndex = 16;
            lblHoraTitulo.Text = "HORA";
            // 
            lblTextoFecha.Text = "Fecha";
            // 
            // cmbHora
            // 
            cmbHora.BackColor = Color.FromArgb(30, 30, 30);
            cmbHora.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbHora.Font = new Font("Segoe UI", 11F);
            cmbHora.ForeColor = Color.White;
            cmbHora.Items.AddRange(new object[] { "10:00 AM", "10:30 AM", "11:00 AM", "11:30 AM", "12:00 PM", "12:30 PM", "01:00 PM", "01:30 PM", "02:00 PM", "02:30 PM", "03:00 PM", "03:30 PM", "04:00 PM", "04:30 PM", "05:00 PM", "05:30 PM", "06:00 PM", "06:30 PM", "07:00 PM" });
            cmbHora.Location = new Point(210, 480);
            cmbHora.Name = "cmbHora";
            cmbHora.Size = new Size(160, 28);
            cmbHora.TabIndex = 13;
            cmbHora.SelectedIndex = 0;
            // 
            // lblTextoHora
            // 
            lblTextoHora.Font = new Font("Segoe UI", 11F);
            lblTextoHora.ForeColor = Color.LightGray;
            lblTextoHora.Location = new Point(210, 455);
            lblTextoHora.Name = "lblTextoHora";
            lblTextoHora.Size = new Size(160, 20);
            lblTextoHora.TabIndex = 15;
            lblTextoHora.Text = "Hora";
            // 
            // lblDatos
            // 
            lblDatos.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblDatos.ForeColor = Color.FromArgb(212, 175, 55);
            lblDatos.Location = new Point(30, 540);
            lblDatos.Name = "lblDatos";
            lblDatos.Size = new Size(150, 25);
            lblDatos.TabIndex = 14;
            lblDatos.Text = "TUS DATOS";
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.FromArgb(30, 30, 30);
            txtNombre.BorderStyle = BorderStyle.FixedSingle;
            txtNombre.Font = new Font("Segoe UI", 12F);
            txtNombre.ForeColor = Color.White;
            txtNombre.Location = new Point(30, 600);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(340, 29);
            txtNombre.TabIndex = 16;
            // 
            // txtTelefono
            // 
            txtTelefono.BackColor = Color.FromArgb(30, 30, 30);
            txtTelefono.BorderStyle = BorderStyle.FixedSingle;
            txtTelefono.Font = new Font("Segoe UI", 12F);
            txtTelefono.ForeColor = Color.White;
            txtTelefono.Location = new Point(30, 680);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(340, 29);
            txtTelefono.TabIndex = 18;
            // 
            // btnConfirmar
            // 
            btnConfirmar.BackColor = Color.FromArgb(212, 175, 55);
            btnConfirmar.Cursor = Cursors.Hand;
            btnConfirmar.FlatAppearance.BorderSize = 0;
            btnConfirmar.FlatStyle = FlatStyle.Flat;
            btnConfirmar.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btnConfirmar.ForeColor = Color.Black;
            btnConfirmar.Location = new Point(26, 800);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(341, 55);
            btnConfirmar.TabIndex = 19;
            btnConfirmar.Text = "CONFIRMAR CITA";
            btnConfirmar.UseVisualStyleBackColor = false;
            // 
            // btnVerCitas
            // 
            btnVerCitas.BackColor = Color.FromArgb(60, 60, 60);
            btnVerCitas.Cursor = Cursors.Hand;
            btnVerCitas.FlatAppearance.BorderSize = 0;
            btnVerCitas.FlatStyle = FlatStyle.Flat;
            btnVerCitas.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnVerCitas.ForeColor = Color.White;
            btnVerCitas.Location = new Point(26, 865);
            btnVerCitas.Name = "btnVerCitas";
            btnVerCitas.Size = new Size(341, 45);
            btnVerCitas.TabIndex = 20;
            btnVerCitas.Text = "VER CITAS";
            btnVerCitas.UseVisualStyleBackColor = false;
            // 
            // lblNombre
            // 
            lblNombre.Font = new Font("Segoe UI", 11F);
            lblNombre.ForeColor = Color.LightGray;
            lblNombre.Location = new Point(30, 575);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(340, 20);
            lblNombre.TabIndex = 15;
            lblNombre.Text = "Nombre completo";
            // 
            // lblTelefono
            // 
            lblTelefono.Font = new Font("Segoe UI", 11F);
            lblTelefono.ForeColor = Color.LightGray;
            lblTelefono.Location = new Point(30, 655);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(340, 20);
            lblTelefono.TabIndex = 17;
            lblTelefono.Text = "Teléfono";
            // 
            // Form1
            // 
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(393, 950);
            Controls.Add(lblTitulo);
            Controls.Add(lblServicios);
            Controls.Add(chkCorte);
            Controls.Add(lblPrecioCorte);
            Controls.Add(lblDescCorte);
            Controls.Add(chkBarba);
            Controls.Add(lblPrecioBarba);
            Controls.Add(lblDescBarba);
            Controls.Add(chkNinos);
            Controls.Add(lblPrecioNinos);
            Controls.Add(lblDescNinos);
            Controls.Add(lblFechaTitulo);
            Controls.Add(lblHoraTitulo);
            Controls.Add(lblTextoFecha);
            Controls.Add(dtpFecha);
            Controls.Add(lblTextoHora);
            Controls.Add(cmbHora);
            Controls.Add(lblDatos);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblTelefono);
            Controls.Add(txtTelefono);
            Controls.Add(btnConfirmar);
            Controls.Add(btnVerCitas);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Yeeff Barber Studio";
            Load += Form1_Load_1;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}