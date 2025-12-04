using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class frmInscripciones : Form
    {
        private CN_Inscripciones cnInscripciones = new CN_Inscripciones();
        private int idEstudianteSeleccionado = 0;
        private int idSeccionSeleccionada = 0;

        public frmInscripciones()
        {
            InitializeComponent();
        }

        // ----------------------------------------------------------------------------------
        // EVENTO: LOAD DEL FORMULARIO
        // ----------------------------------------------------------------------------------
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Configurar DataGridView
            dgvMateriasInscritas.ReadOnly = true;
            dgvMateriasInscritas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMateriasInscritas.MultiSelect = false;

            // Cargar estudiantes
            CargarEstudiantes();

            // Inicializar controles
            cmbMaterias.Enabled = false;
            txtCarrera.ReadOnly = true;
            txtCreditos.ReadOnly = true;

            // Agregar botón de inscribir
            AgregarBotonInscribir();

            // Agregar botón de dar de baja
            AgregarBotonDarDeBaja();
        }

        // ----------------------------------------------------------------------------------
        // CARGAR ESTUDIANTES EN EL COMBOBOX
        // ----------------------------------------------------------------------------------
        private void CargarEstudiantes()
        {
            try
            {
                DataTable dtEstudiantes = cnInscripciones.ObtenerEstudiantesParaCmb();

                if (dtEstudiantes == null || dtEstudiantes.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron estudiantes.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbEstudiantes.Enabled = false;
                    return;
                }

                // Añadir fila de selección
                DataRow newRow = dtEstudiantes.NewRow();
                newRow["IdEstudiante"] = 0;
                newRow["NombreCompleto"] = "-- SELECCIONE ESTUDIANTE --";
                newRow["Carrera"] = "";
                dtEstudiantes.Rows.InsertAt(newRow, 0);

                cmbEstudiantes.DataSource = dtEstudiantes;
                cmbEstudiantes.DisplayMember = "NombreCompleto";
                cmbEstudiantes.ValueMember = "IdEstudiante";
                cmbEstudiantes.SelectedIndex = 0;

                // Conectar evento
                cmbEstudiantes.SelectedIndexChanged += cmbEstudiantes_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar estudiantes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ----------------------------------------------------------------------------------
        // EVENTO: CAMBIO DE ESTUDIANTE
        // ----------------------------------------------------------------------------------
        private void cmbEstudiantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEstudiantes.SelectedValue != null && cmbEstudiantes.SelectedValue is int)
            {
                int nuevoId = (int)cmbEstudiantes.SelectedValue;

                if (nuevoId != idEstudianteSeleccionado)
                {
                    idEstudianteSeleccionado = nuevoId;

                    if (idEstudianteSeleccionado > 0)
                    {
                        // Obtener datos del estudiante
                        DataRowView row = (DataRowView)cmbEstudiantes.SelectedItem;
                        txtCarrera.Text = row["Carrera"].ToString();

                        // Cargar materias disponibles
                        CargarMateriasDisponibles();

                        // Cargar materias ya inscritas
                        CargarMateriasInscritas();
                    }
                    else
                    {
                        // Reset
                        txtCarrera.Clear();
                        txtCreditos.Clear();
                        cmbMaterias.DataSource = null;
                        cmbMaterias.Enabled = false;
                        dgvMateriasInscritas.DataSource = null;
                    }
                }
            }
        }

        // ----------------------------------------------------------------------------------
        // CARGAR MATERIAS DISPONIBLES
        // ----------------------------------------------------------------------------------
        private void CargarMateriasDisponibles()
        {
            try
            {
                DataTable dtMaterias = cnInscripciones.ObtenerMateriasDisponibles(idEstudianteSeleccionado);

                if (dtMaterias == null || dtMaterias.Rows.Count == 0)
                {
                    MessageBox.Show("No hay materias disponibles para inscribir.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbMaterias.DataSource = null;
                    cmbMaterias.Enabled = false;
                    return;
                }

                // Añadir placeholder
                DataRow newRow = dtMaterias.NewRow();
                newRow["IdSeccion"] = 0;
                newRow["Display"] = "-- SELECCIONE MATERIA --";
                newRow["Creditos"] = 0;
                dtMaterias.Rows.InsertAt(newRow, 0);

                cmbMaterias.DataSource = dtMaterias;
                cmbMaterias.DisplayMember = "Display";
                cmbMaterias.ValueMember = "IdSeccion";
                cmbMaterias.SelectedIndex = 0;
                cmbMaterias.Enabled = true;

                // Conectar evento
                cmbMaterias.SelectedIndexChanged += cmbMaterias_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar materias disponibles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ----------------------------------------------------------------------------------
        // EVENTO: CAMBIO DE MATERIA
        // ----------------------------------------------------------------------------------
        private void cmbMaterias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMaterias.SelectedValue != null && cmbMaterias.SelectedValue is int)
            {
                idSeccionSeleccionada = (int)cmbMaterias.SelectedValue;

                if (idSeccionSeleccionada > 0)
                {
                    // Mostrar créditos
                    DataRowView row = (DataRowView)cmbMaterias.SelectedItem;
                    txtCreditos.Text = row["Creditos"].ToString() + " créditos";
                }
                else
                {
                    txtCreditos.Clear();
                }
            }
        }

        // ----------------------------------------------------------------------------------
        // CARGAR MATERIAS INSCRITAS
        // ----------------------------------------------------------------------------------
        private void CargarMateriasInscritas()
        {
            try
            {
                DataTable dtInscritas = cnInscripciones.ObtenerMateriasInscritas(idEstudianteSeleccionado);

                dgvMateriasInscritas.DataSource = dtInscritas;

                // Ocultar columna IdMatricula
                if (dgvMateriasInscritas.Columns.Contains("IdMatricula"))
                {
                    dgvMateriasInscritas.Columns["IdMatricula"].Visible = false;
                }

                // Aplicar colores según el estado
                foreach (DataGridViewRow row in dgvMateriasInscritas.Rows)
                {
                    if (row.Cells["Estado"].Value != null)
                    {
                        string estado = row.Cells["Estado"].Value.ToString();

                        if (estado == "Aprobado")
                        {
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                        }
                        else if (estado == "Reprobado")
                        {
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                        }
                        else // Cursando
                        {
                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                        }
                    }
                }

                dgvMateriasInscritas.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar materias inscritas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ----------------------------------------------------------------------------------
        // AGREGAR BOTÓN DE INSCRIBIR (Dinámicamente)
        // ----------------------------------------------------------------------------------
        private void AgregarBotonInscribir()
        {
            var btnInscribir = new Guna.UI2.WinForms.Guna2Button
            {
                Name = "btnInscribir",
                Text = "INSCRIBIR MATERIA",
                FillColor = Color.FromArgb(90, 139, 76),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(200, 45),
                Location = new Point(60, 510)
            };

            btnInscribir.Click += BtnInscribir_Click;
            guna2Panel2.Controls.Add(btnInscribir);
        }

        // ----------------------------------------------------------------------------------
        // EVENTO: BOTÓN INSCRIBIR
        // ----------------------------------------------------------------------------------
        private void BtnInscribir_Click(object sender, EventArgs e)
        {
            if (idEstudianteSeleccionado <= 0)
            {
                MessageBox.Show("Debe seleccionar un estudiante.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (idSeccionSeleccionada <= 0)
            {
                MessageBox.Show("Debe seleccionar una materia.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime fechaInscripcion = dtpFecha.Value;
            string mensaje;

            try
            {
                if (cnInscripciones.InscribirMateria(idEstudianteSeleccionado, idSeccionSeleccionada, fechaInscripcion, out mensaje))
                {
                    MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Recargar ambas listas
                    CargarMateriasDisponibles();
                    CargarMateriasInscritas();

                    // Limpiar selección
                    txtCreditos.Clear();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inscribir: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ----------------------------------------------------------------------------------
        // AGREGAR BOTÓN DE DAR DE BAJA (Dinámicamente)
        // ----------------------------------------------------------------------------------
        private void AgregarBotonDarDeBaja()
        {
            var btnDarDeBaja = new Guna.UI2.WinForms.Guna2Button
            {
                Name = "btnDarDeBaja",
                Text = "DAR DE BAJA",
                FillColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(150, 40),
                Location = new Point(520, 580)
            };

            btnDarDeBaja.Click += BtnDarDeBaja_Click;
            guna2Panel1.Controls.Add(btnDarDeBaja);
        }

        // ----------------------------------------------------------------------------------
        // EVENTO: BOTÓN DAR DE BAJA
        // ----------------------------------------------------------------------------------
        private void BtnDarDeBaja_Click(object sender, EventArgs e)
        {
            if (dgvMateriasInscritas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una materia inscrita para dar de baja.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var filaSeleccionada = dgvMateriasInscritas.SelectedRows[0];

            // Verificar que la materia esté en estado "Cursando"
            string estado = filaSeleccionada.Cells["Estado"].Value?.ToString();

            if (estado != "Cursando")
            {
                MessageBox.Show("Solo puede dar de baja materias en estado 'Cursando'.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmar
            string nombreMateria = filaSeleccionada.Cells["Materia"].Value?.ToString();
            var resultado = MessageBox.Show(
                $"¿Está seguro de que desea dar de baja la materia '{nombreMateria}'?",
                "Confirmar Baja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado != DialogResult.Yes)
            {
                return;
            }

            int idMatricula = Convert.ToInt32(filaSeleccionada.Cells["IdMatricula"].Value);
            string mensaje;

            try
            {
                if (cnInscripciones.DarDeBajaMateria(idMatricula, out mensaje))
                {
                    MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Recargar ambas listas
                    CargarMateriasDisponibles();
                    CargarMateriasInscritas();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al dar de baja: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCarrera_TextChanged(object sender, EventArgs e)
        {

        }
    }
}