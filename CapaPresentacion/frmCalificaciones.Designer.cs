namespace CapaPresentacion
{
    partial class frmCalificaciones
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            btnGuardarNota = new Guna.UI2.WinForms.Guna2Button();
            label2 = new Label();
            txtNota = new Guna.UI2.WinForms.Guna2TextBox();
            cmbMaterias = new Guna.UI2.WinForms.Guna2ComboBox();
            cmbEstudiantes = new Guna.UI2.WinForms.Guna2ComboBox();
            label1 = new Label();
            dgvRecord = new Guna.UI2.WinForms.Guna2DataGridView();
            label3 = new Label();
            lblIndice = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecord).BeginInit();
            SuspendLayout();
            // 
            // guna2Panel1
            // 
            guna2Panel1.Controls.Add(btnGuardarNota);
            guna2Panel1.Controls.Add(label2);
            guna2Panel1.Controls.Add(txtNota);
            guna2Panel1.Controls.Add(cmbMaterias);
            guna2Panel1.Controls.Add(cmbEstudiantes);
            guna2Panel1.Controls.Add(label1);
            guna2Panel1.CustomizableEdges = customizableEdges9;
            guna2Panel1.Dock = DockStyle.Left;
            guna2Panel1.Location = new Point(0, 0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges10;
            guna2Panel1.Size = new Size(350, 522);
            guna2Panel1.TabIndex = 0;
            // 
            // btnGuardarNota
            // 
            btnGuardarNota.CustomizableEdges = customizableEdges1;
            btnGuardarNota.DisabledState.BorderColor = Color.DarkGray;
            btnGuardarNota.DisabledState.CustomBorderColor = Color.DarkGray;
            btnGuardarNota.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnGuardarNota.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnGuardarNota.FillColor = Color.FromArgb(90, 139, 76);
            btnGuardarNota.Font = new Font("Segoe UI", 9F);
            btnGuardarNota.ForeColor = Color.White;
            btnGuardarNota.Location = new Point(87, 309);
            btnGuardarNota.Name = "btnGuardarNota";
            btnGuardarNota.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnGuardarNota.Size = new Size(180, 45);
            btnGuardarNota.TabIndex = 5;
            btnGuardarNota.Text = "REGISTRAR NOTA";
            btnGuardarNota.Click += btnGuardarNota_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(87, 209);
            label2.Name = "label2";
            label2.Size = new Size(78, 15);
            label2.TabIndex = 4;
            label2.Text = "Ingresar Nota";
            // 
            // txtNota
            // 
            txtNota.CustomizableEdges = customizableEdges3;
            txtNota.DefaultText = "";
            txtNota.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtNota.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtNota.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtNota.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtNota.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtNota.Font = new Font("Segoe UI", 9F);
            txtNota.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtNota.Location = new Point(87, 246);
            txtNota.Name = "txtNota";
            txtNota.PlaceholderText = "";
            txtNota.SelectedText = "";
            txtNota.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtNota.Size = new Size(200, 36);
            txtNota.TabIndex = 3;
            txtNota.TextChanged += txtNota_TextChanged;
            // 
            // cmbMaterias
            // 
            cmbMaterias.BackColor = Color.Transparent;
            cmbMaterias.CustomizableEdges = customizableEdges5;
            cmbMaterias.DrawMode = DrawMode.OwnerDrawFixed;
            cmbMaterias.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaterias.FocusedColor = Color.FromArgb(94, 148, 255);
            cmbMaterias.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cmbMaterias.Font = new Font("Segoe UI", 10F);
            cmbMaterias.ForeColor = Color.FromArgb(68, 88, 112);
            cmbMaterias.ItemHeight = 30;
            cmbMaterias.Location = new Point(87, 131);
            cmbMaterias.Name = "cmbMaterias";
            cmbMaterias.ShadowDecoration.CustomizableEdges = customizableEdges6;
            cmbMaterias.Size = new Size(140, 36);
            cmbMaterias.TabIndex = 2;
            cmbMaterias.SelectedIndexChanged += cmbMaterias_SelectedIndexChanged;
            // 
            // cmbEstudiantes
            // 
            cmbEstudiantes.BackColor = Color.Transparent;
            cmbEstudiantes.CustomizableEdges = customizableEdges7;
            cmbEstudiantes.DrawMode = DrawMode.OwnerDrawFixed;
            cmbEstudiantes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstudiantes.FocusedColor = Color.FromArgb(94, 148, 255);
            cmbEstudiantes.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cmbEstudiantes.Font = new Font("Segoe UI", 10F);
            cmbEstudiantes.ForeColor = Color.FromArgb(68, 88, 112);
            cmbEstudiantes.ItemHeight = 30;
            cmbEstudiantes.Location = new Point(87, 70);
            cmbEstudiantes.Name = "cmbEstudiantes";
            cmbEstudiantes.ShadowDecoration.CustomizableEdges = customizableEdges8;
            cmbEstudiantes.Size = new Size(140, 36);
            cmbEstudiantes.TabIndex = 1;
            cmbEstudiantes.SelectedIndexChanged += cmbEstudiantes_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(87, 35);
            label1.Name = "label1";
            label1.Size = new Size(138, 15);
            label1.TabIndex = 0;
            label1.Text = "ASIGNAR CALIFICACIÓN";
            // 
            // dgvRecord
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            dgvRecord.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvRecord.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvRecord.ColumnHeadersHeight = 4;
            dgvRecord.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvRecord.DefaultCellStyle = dataGridViewCellStyle3;
            dgvRecord.GridColor = Color.FromArgb(231, 229, 255);
            dgvRecord.Location = new Point(391, 92);
            dgvRecord.Name = "dgvRecord";
            dgvRecord.RowHeadersVisible = false;
            dgvRecord.Size = new Size(494, 331);
            dgvRecord.TabIndex = 1;
            dgvRecord.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgvRecord.ThemeStyle.AlternatingRowsStyle.Font = null;
            dgvRecord.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dgvRecord.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dgvRecord.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dgvRecord.ThemeStyle.BackColor = Color.White;
            dgvRecord.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dgvRecord.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dgvRecord.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvRecord.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            dgvRecord.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvRecord.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvRecord.ThemeStyle.HeaderStyle.Height = 4;
            dgvRecord.ThemeStyle.ReadOnly = false;
            dgvRecord.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgvRecord.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvRecord.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dgvRecord.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dgvRecord.ThemeStyle.RowsStyle.Height = 25;
            dgvRecord.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgvRecord.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dgvRecord.CellContentClick += dgvRecord_CellContentClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(391, 56);
            label3.Name = "label3";
            label3.Size = new Size(109, 15);
            label3.TabIndex = 2;
            label3.Text = "RÉCORD DE NOTAS";
            // 
            // lblIndice
            // 
            lblIndice.BackColor = Color.Transparent;
            lblIndice.Location = new Point(391, 454);
            lblIndice.Name = "lblIndice";
            lblIndice.Size = new Size(133, 17);
            lblIndice.TabIndex = 3;
            lblIndice.Text = "ÍNDICE ACADÉMICO: 0.0";
            lblIndice.Click += lblIndice_Click;
            // 
            // frmCalificaciones
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(212, 210, 195);
            ClientSize = new Size(918, 522);
            Controls.Add(lblIndice);
            Controls.Add(label3);
            Controls.Add(dgvRecord);
            Controls.Add(guna2Panel1);
            Name = "frmCalificaciones";
            Text = "frmCalificaciones";
            Load += frmCalificaciones_Load_1;
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecord).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2ComboBox cmbEstudiantes;
        private Label label1;
        private Guna.UI2.WinForms.Guna2Button btnGuardarNota;
        private Label label2;
        private Guna.UI2.WinForms.Guna2TextBox txtNota;
        private Guna.UI2.WinForms.Guna2ComboBox cmbMaterias;
        private Guna.UI2.WinForms.Guna2DataGridView dgvRecord;
        private Label label3;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblIndice;
    }
}