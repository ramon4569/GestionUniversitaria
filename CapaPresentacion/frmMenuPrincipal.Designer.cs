namespace CapaPresentacion
{
    partial class frmMenuPrincipal
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
            panelMenuLateral = new Panel();
            btnEstudiantes = new Button();
            btnSalir = new Button();
            btnCalificaciones = new Button();
            btnConsultas = new Button();
            panelLogo = new Panel();
            btnMenu = new PictureBox();
            pictureBox1 = new PictureBox();
            panelContenedor = new Panel();
            panel1 = new Panel();
            SidebarTimer = new System.Windows.Forms.Timer(components);
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            panelMenuLateral.SuspendLayout();
            panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnMenu).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelContenedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // panelMenuLateral
            // 
            panelMenuLateral.BackColor = Color.FromArgb(28, 28, 28);
            panelMenuLateral.Controls.Add(pictureBox4);
            panelMenuLateral.Controls.Add(pictureBox3);
            panelMenuLateral.Controls.Add(pictureBox2);
            panelMenuLateral.Controls.Add(btnEstudiantes);
            panelMenuLateral.Controls.Add(btnSalir);
            panelMenuLateral.Controls.Add(btnCalificaciones);
            panelMenuLateral.Controls.Add(btnConsultas);
            panelMenuLateral.Controls.Add(panelLogo);
            panelMenuLateral.Dock = DockStyle.Left;
            panelMenuLateral.ForeColor = Color.White;
            panelMenuLateral.Location = new Point(0, 0);
            panelMenuLateral.MaximumSize = new Size(250, 0);
            panelMenuLateral.MinimumSize = new Size(70, 0);
            panelMenuLateral.Name = "panelMenuLateral";
            panelMenuLateral.Size = new Size(250, 561);
            panelMenuLateral.TabIndex = 0;
            // 
            // btnEstudiantes
            // 
            btnEstudiantes.AutoSize = true;
            btnEstudiantes.Cursor = Cursors.Hand;
            btnEstudiantes.FlatAppearance.BorderSize = 0;
            btnEstudiantes.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 139, 76);
            btnEstudiantes.FlatStyle = FlatStyle.Flat;
            btnEstudiantes.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEstudiantes.Location = new Point(3, 94);
            btnEstudiantes.Name = "btnEstudiantes";
            btnEstudiantes.Size = new Size(250, 78);
            btnEstudiantes.TabIndex = 8;
            btnEstudiantes.Text = "Estudiantes";
            btnEstudiantes.UseVisualStyleBackColor = true;
            // 
            // btnSalir
            // 
            btnSalir.AutoSize = true;
            btnSalir.Cursor = Cursors.Hand;
            btnSalir.FlatAppearance.BorderSize = 0;
            btnSalir.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 139, 76);
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSalir.Location = new Point(0, 334);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(250, 78);
            btnSalir.TabIndex = 4;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            // 
            // btnCalificaciones
            // 
            btnCalificaciones.AutoSize = true;
            btnCalificaciones.Cursor = Cursors.Hand;
            btnCalificaciones.FlatAppearance.BorderSize = 0;
            btnCalificaciones.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 139, 76);
            btnCalificaciones.FlatStyle = FlatStyle.Flat;
            btnCalificaciones.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCalificaciones.Location = new Point(0, 256);
            btnCalificaciones.Name = "btnCalificaciones";
            btnCalificaciones.Size = new Size(250, 78);
            btnCalificaciones.TabIndex = 3;
            btnCalificaciones.Text = "Calificaciones";
            btnCalificaciones.UseVisualStyleBackColor = true;
            // 
            // btnConsultas
            // 
            btnConsultas.AutoSize = true;
            btnConsultas.Cursor = Cursors.Hand;
            btnConsultas.FlatAppearance.BorderSize = 0;
            btnConsultas.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 139, 76);
            btnConsultas.FlatStyle = FlatStyle.Flat;
            btnConsultas.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConsultas.Location = new Point(0, 178);
            btnConsultas.Name = "btnConsultas";
            btnConsultas.Size = new Size(250, 78);
            btnConsultas.TabIndex = 2;
            btnConsultas.Text = "Consultas";
            btnConsultas.UseVisualStyleBackColor = true;
            // 
            // panelLogo
            // 
            panelLogo.Controls.Add(btnMenu);
            panelLogo.Controls.Add(pictureBox1);
            panelLogo.Dock = DockStyle.Top;
            panelLogo.Location = new Point(0, 0);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(250, 100);
            panelLogo.TabIndex = 0;
            // 
            // btnMenu
            // 
            btnMenu.Image = Properties.Resources.image__8_1;
            btnMenu.Location = new Point(12, 26);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(46, 52);
            btnMenu.SizeMode = PictureBoxSizeMode.Zoom;
            btnMenu.TabIndex = 8;
            btnMenu.TabStop = false;
            btnMenu.Click += btnMenu_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(250, 100);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panelContenedor
            // 
            panelContenedor.BackColor = Color.FromArgb(212, 210, 195);
            panelContenedor.Controls.Add(panel1);
            panelContenedor.Dock = DockStyle.Fill;
            panelContenedor.Location = new Point(250, 0);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(834, 561);
            panelContenedor.TabIndex = 1;
            panelContenedor.Paint += panelContenedor_Paint;
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(834, 100);
            panel1.TabIndex = 0;
            // 
            // SidebarTimer
            // 
            SidebarTimer.Interval = 15;
            SidebarTimer.Tick += SidebarTimer_Tick;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.image__10_;
            pictureBox2.Location = new Point(12, 106);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(46, 52);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 9;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.image__8_;
            pictureBox3.Location = new Point(12, 187);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(46, 52);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 10;
            pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.image__9_;
            pictureBox4.Location = new Point(12, 262);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(46, 52);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 11;
            pictureBox4.TabStop = false;
            // 
            // frmMenuPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 561);
            Controls.Add(panelContenedor);
            Controls.Add(panelMenuLateral);
            Name = "frmMenuPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmMenuPrincipal";
            panelMenuLateral.ResumeLayout(false);
            panelMenuLateral.PerformLayout();
            panelLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)btnMenu).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelContenedor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMenuLateral;
        private Panel panelContenedor;
        private Panel panelLogo;
        private Panel panel1;
        private PictureBox pictureBox1;
        private PictureBox btnMenu;
        private System.Windows.Forms.Timer SidebarTimer;
        private Button btnSalir;
        private Button btnConsultas;
        private Button btnCalificaciones;
        private Button btnEstudiantes;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
    }
}