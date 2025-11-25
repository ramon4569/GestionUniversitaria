namespace CapaPresentacion
{
    partial class frmEstudiantes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            label1 = new Label();
            panelRegistro = new Panel();
            panelListado = new Guna.UI2.WinForms.Guna2Panel();
            btnRegresar = new Guna.UI2.WinForms.Guna2PictureBox();
            groupBox1 = new GroupBox();
            txtEmail = new MaskedTextBox();
            txtTelefono = new MaskedTextBox();
            cmbCarrera = new ComboBox();
            txtApellido = new TextBox();
            txtNombre = new TextBox();
            txtMatricula = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(components);
            guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(components);
            guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(components);
            panelRegistro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnRegresar).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(28, 28, 28);
            label1.Location = new Point(33, 63);
            label1.Name = "label1";
            label1.Size = new Size(280, 30);
            label1.TabIndex = 0;
            label1.Text = "GESTIÓN DE ESTUDIANTES";
            // 
            // panelRegistro
            // 
            panelRegistro.Controls.Add(panelListado);
            panelRegistro.Controls.Add(btnRegresar);
            panelRegistro.Controls.Add(groupBox1);
            panelRegistro.Controls.Add(label1);
            panelRegistro.Dock = DockStyle.Fill;
            panelRegistro.Location = new Point(0, 0);
            panelRegistro.Name = "panelRegistro";
            panelRegistro.Size = new Size(934, 561);
            panelRegistro.TabIndex = 2;
            // 
            // panelListado
            // 
            panelListado.CustomizableEdges = customizableEdges1;
            panelListado.Dock = DockStyle.Right;
            panelListado.Location = new Point(330, 0);
            panelListado.Name = "panelListado";
            panelListado.ShadowDecoration.CustomizableEdges = customizableEdges2;
            panelListado.Size = new Size(604, 561);
            panelListado.TabIndex = 3;
            // 
            // btnRegresar
            // 
            btnRegresar.BackColor = Color.FromArgb(212, 210, 195);
            btnRegresar.CustomizableEdges = customizableEdges3;
            btnRegresar.FillColor = Color.Black;
            btnRegresar.Image = Properties.Resources.regresar;
            btnRegresar.ImageRotate = 0F;
            btnRegresar.Location = new Point(12, 12);
            btnRegresar.Name = "btnRegresar";
            btnRegresar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnRegresar.Size = new Size(30, 28);
            btnRegresar.SizeMode = PictureBoxSizeMode.Zoom;
            btnRegresar.TabIndex = 2;
            btnRegresar.TabStop = false;
            btnRegresar.Click += btnRegresar_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtEmail);
            groupBox1.Controls.Add(txtTelefono);
            groupBox1.Controls.Add(cmbCarrera);
            groupBox1.Controls.Add(txtApellido);
            groupBox1.Controls.Add(txtNombre);
            groupBox1.Controls.Add(txtMatricula);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(62, 114);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(211, 435);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(33, 332);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(100, 23);
            txtEmail.TabIndex = 11;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(32, 288);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(100, 23);
            txtTelefono.TabIndex = 10;
            // 
            // cmbCarrera
            // 
            cmbCarrera.FormattingEnabled = true;
            cmbCarrera.Location = new Point(32, 236);
            cmbCarrera.Name = "cmbCarrera";
            cmbCarrera.Size = new Size(121, 23);
            cmbCarrera.TabIndex = 9;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(32, 174);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(100, 23);
            txtApellido.TabIndex = 8;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(32, 120);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(100, 23);
            txtNombre.TabIndex = 7;
            // 
            // txtMatricula
            // 
            txtMatricula.Location = new Point(32, 69);
            txtMatricula.Name = "txtMatricula";
            txtMatricula.Size = new Size(100, 23);
            txtMatricula.TabIndex = 6;
            txtMatricula.TextChanged += textBox1_TextChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(32, 314);
            label7.Name = "label7";
            label7.Size = new Size(36, 15);
            label7.TabIndex = 5;
            label7.Text = "Email";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(32, 262);
            label6.Name = "label6";
            label6.Size = new Size(53, 15);
            label6.TabIndex = 4;
            label6.Text = "Telefono";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(32, 215);
            label5.Name = "label5";
            label5.Size = new Size(45, 15);
            label5.TabIndex = 3;
            label5.Text = "Carrera";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(32, 156);
            label4.Name = "label4";
            label4.Size = new Size(51, 15);
            label4.TabIndex = 2;
            label4.Text = "Apellido";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(32, 102);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 1;
            label3.Text = "Nombre";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(32, 51);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 0;
            label2.Text = "Matricula";
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 8;
            guna2Elipse1.TargetControl = panelListado;
            // 
            // guna2ShadowForm1
            // 
            guna2ShadowForm1.BorderRadius = 20;
            guna2ShadowForm1.TargetForm = this;
            // 
            // guna2AnimateWindow1
            // 
            guna2AnimateWindow1.TargetForm = this;
            // 
            // guna2DragControl1
            // 
            guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            guna2DragControl1.TargetControl = groupBox1;
            guna2DragControl1.UseTransparentDrag = true;
            // 
            // frmEstudiantes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(212, 210, 195);
            ClientSize = new Size(934, 561);
            Controls.Add(panelRegistro);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmEstudiantes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmEstudiantes";
            Load += frmEstudiantes_Load;
            panelRegistro.ResumeLayout(false);
            panelRegistro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)btnRegresar).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Panel panelRegistro;
        private GroupBox groupBox1;
        private TextBox txtMatricula;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private MaskedTextBox txtEmail;
        private MaskedTextBox txtTelefono;
        private ComboBox cmbCarrera;
        private TextBox txtApellido;
        private TextBox txtNombre;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2PictureBox btnRegresar;
        private Guna.UI2.WinForms.Guna2Panel panelListado;
    }
}