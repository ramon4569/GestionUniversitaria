namespace CapaPresentacion
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            button1 = new Button();
            lblTitulo = new Label();
            txtPass = new TextBox();
            txtUsuario = new TextBox();
            panel2 = new Panel();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            panel3 = new Panel();
            btnOcultar = new PictureBox();
            btnCerrar = new PictureBox();
            btnMinimizar = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnOcultar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnCerrar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnMinimizar).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(90, 139, 76);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 297);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(26, 272);
            label1.Name = "label1";
            label1.Size = new Size(143, 16);
            label1.TabIndex = 1;
            label1.Text = "© 2024 UNIVERSIDAD 1.0.0";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.LogoCandado;
            pictureBox1.Location = new Point(26, 115);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(138, 154);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.BackColor = Color.White;
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Times New Roman", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.Black;
            button1.Location = new Point(365, 239);
            button1.Name = "button1";
            button1.Size = new Size(86, 30);
            button1.TabIndex = 1;
            button1.Text = "Acceder";
            button1.UseVisualStyleBackColor = false;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Century Gothic", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(254, 62);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(90, 30);
            lblTitulo.TabIndex = 2;
            lblTitulo.Text = "LOGIN";
            // 
            // txtPass
            // 
            txtPass.BackColor = SystemColors.Control;
            txtPass.BorderStyle = BorderStyle.None;
            txtPass.Location = new Point(300, 199);
            txtPass.Multiline = true;
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(220, 23);
            txtPass.TabIndex = 2;
            txtPass.Text = "CONTRASEÑA";
            txtPass.Enter += txtPass_Enter;
            txtPass.Leave += txtPass_Leave;
            // 
            // txtUsuario
            // 
            txtUsuario.BackColor = SystemColors.Control;
            txtUsuario.BorderStyle = BorderStyle.None;
            txtUsuario.Location = new Point(300, 150);
            txtUsuario.Multiline = true;
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(220, 23);
            txtUsuario.TabIndex = 1;
            txtUsuario.Text = "USUARIO";
            txtUsuario.Enter += txtUsuario_Enter;
            txtUsuario.Leave += txtUsuario_Leave;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Gray;
            panel2.Location = new Point(300, 172);
            panel2.Name = "panel2";
            panel2.Size = new Size(220, 1);
            panel2.TabIndex = 5;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Usuario;
            pictureBox2.Location = new Point(254, 133);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(40, 40);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.LogoCandado;
            pictureBox3.Location = new Point(254, 182);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(40, 40);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Gray;
            panel3.Location = new Point(300, 221);
            panel3.Name = "panel3";
            panel3.Size = new Size(220, 1);
            panel3.TabIndex = 7;
            // 
            // btnOcultar
            // 
            btnOcultar.Image = Properties.Resources.OjitoCerrado;
            btnOcultar.Location = new Point(526, 187);
            btnOcultar.Name = "btnOcultar";
            btnOcultar.Size = new Size(35, 35);
            btnOcultar.SizeMode = PictureBoxSizeMode.Zoom;
            btnOcultar.TabIndex = 9;
            btnOcultar.TabStop = false;
            btnOcultar.Click += btnOcultar_Click;
            // 
            // btnCerrar
            // 
            btnCerrar.Image = Properties.Resources.icone_x_grise;
            btnCerrar.Location = new Point(590, 0);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(10, 10);
            btnCerrar.SizeMode = PictureBoxSizeMode.Zoom;
            btnCerrar.TabIndex = 10;
            btnCerrar.TabStop = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // btnMinimizar
            // 
            btnMinimizar.Image = Properties.Resources.Minimizar;
            btnMinimizar.Location = new Point(578, 0);
            btnMinimizar.Name = "btnMinimizar";
            btnMinimizar.Size = new Size(10, 10);
            btnMinimizar.SizeMode = PictureBoxSizeMode.Zoom;
            btnMinimizar.TabIndex = 11;
            btnMinimizar.TabStop = false;
            btnMinimizar.Click += btnMinimizar_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 297);
            Controls.Add(btnMinimizar);
            Controls.Add(btnCerrar);
            Controls.Add(btnOcultar);
            Controls.Add(pictureBox3);
            Controls.Add(panel3);
            Controls.Add(txtPass);
            Controls.Add(pictureBox2);
            Controls.Add(panel2);
            Controls.Add(txtUsuario);
            Controls.Add(lblTitulo);
            Controls.Add(button1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            MouseDown += Form1_MouseDown;
            MouseUp += Form1_MouseUp;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnOcultar).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnCerrar).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnMinimizar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private Button button1;
        private Label lblTitulo;
        private TextBox txtPass;
        private TextBox txtUsuario;
        private Panel panel2;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Panel panel3;
        private PictureBox btnOcultar;
        private PictureBox btnCerrar;
        private PictureBox btnMinimizar;
        private Label label1;
    }
}
