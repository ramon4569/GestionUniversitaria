using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Reflection;
using CapaNegocio;
using CapaNegocio.Base;
using CapaNegocio.Excepciones;
using CapaNegocio.Excepciones.CapaNegocio.Excepciones;
// *** DIRECTIVA DUPLICADA ELIMINADA: CapaNegocio.Excepciones.CapaNegocio.Excepciones; ***

namespace CapaPresentacion
{
    public partial class frmCalificaciones : Form
    {
        private CN_Calificaciones cnCalificaciones = new CN_Calificaciones();
        private List<Inscripcion> recordAcademicoActual;

        private int idEstudianteSeleccionado = 0;
        private int idMatriculaSeleccionada = 0;


        public frmCalificaciones()
        {
            InitializeComponent();
        }

        // ----------------------------------------------------------------------
        // MÉTODOS DE INICIALIZACIÓN Y CARGA
        // ----------------------------------------------------------------------

        // El método frmCalificaciones_Load_1 es el que se llama al iniciar el formulario.
        private void frmCalificaciones_Load_1(object sender, EventArgs e)
        {
            // 1. Configuración de la Interfaz
            dgvRecord.ReadOnly = true;

            // 2. Cargar la lista de estudiantes al iniciar
            CargarEstudiantes();

            // 3. Inicializar ComboBox de Materias
            cmbMaterias.DataSource = null;
            cmbMaterias.Enabled = false;
        }

        private void CargarEstudiantes()
        {
            try
            {
                DataTable dtEstudiantes = cnCalificaciones.ObtenerEstudiantesParaCmb();

                if (dtEstudiantes == null || dtEstudiantes.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron estudiantes.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbEstudiantes.Enabled = false;
                    return;
                }

                // Añadir fila inicial para selección
                DataRow newRow = dtEstudiantes.NewRow();
                newRow["IdEstudiante"] = 0;
                newRow["NombreCompleto"] = "-- SELECCIONE ESTUDIANTE --";
                dtEstudiantes.Rows.InsertAt(newRow, 0);

                // Asignación de Data Source 
                cmbEstudiantes.DataSource = dtEstudiantes;
                cmbEstudiantes.DisplayMember = "NombreCompleto";
                cmbEstudiantes.ValueMember = "IdEstudiante";
                cmbEstudiantes.SelectedIndex = 0;

                cmbEstudiantes.Enabled = true;

            }
            catch (Exception ex)
            {
                // Este catch manejará el error de ConnectionString si persiste.
                MessageBox.Show("Error crítico al cargar la lista de estudiantes: " + ex.Message, "ERROR CRÍTICO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbEstudiantes.Enabled = false;
            }
        }

        private void ActualizarRecordDGV()
        {
            // 1. Asignar el Data Source del récord al DGV
            var dataParaDGV = recordAcademicoActual
                .Where(i => i.Materia != null)
                .Select(i => new
                {
                    Materia = i.Materia.Nombre,
                    Sección = i.CodigoSeccion,
                    Créditos = i.Materia.Creditos,
                    Nota_Final = i.CalificacionFinal.HasValue ? i.CalificacionFinal.Value.ToString("N2") : "PENDIENTE",
                    IdMatricula = i.IdMatricula
                })
                .ToList();

            dgvRecord.DataSource = dataParaDGV;

            // 2. Configurar el DGV (ocultar ID y formato)
            if (dgvRecord.Columns.Contains("IdMatricula")) dgvRecord.Columns["IdMatricula"].Visible = false;
            dgvRecord.AutoResizeColumns();


            // 3. Calcular y mostrar el índice
            lblIndice.Text = "ÍNDICE ACADÉMICO: " + cnCalificaciones.CalcularIndiceAcademico(recordAcademicoActual);
        }

        // ----------------------------------------------------------------------
        // EVENTOS DE COMBOBOX (LÓGICA CONSOLIDADA)
        // ----------------------------------------------------------------------

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
                        try
                        {
                            // 1. Obtener el récord completo del estudiante
                            recordAcademicoActual = cnCalificaciones.ObtenerRecordAcademico(idEstudianteSeleccionado);

                            // 2. Actualizar DGV y Índice
                            ActualizarRecordDGV();

                            // 3. Filtrar las materias pendientes para el ComboBox (Lógica movida aquí)
                            var materiasPendientes = recordAcademicoActual
                                .Where(i => i.Materia != null && !i.CalificacionFinal.HasValue)
                                .Select(i => new
                                {
                                    i.IdMatricula,
                                    Display = $"{i.Materia.Codigo} - {i.Materia.Nombre} | Sección: {i.CodigoSeccion}"
                                })
                                .ToList();

                            var placeholder = new { IdMatricula = 0, Display = "-- SELECCIONE SECCIÓN --" };
                            materiasPendientes.Insert(0, placeholder);

                            cmbMaterias.DataSource = materiasPendientes;
                            cmbMaterias.DisplayMember = "Display";
                            cmbMaterias.ValueMember = "IdMatricula";
                            cmbMaterias.SelectedIndex = 0;

                            cmbMaterias.Enabled = materiasPendientes.Count > 1;
                        }
                        catch (Exception ex)
                        {
                            // Manejo de error de carga (incluye el error de ConnectionString)
                            MessageBox.Show("Error al cargar las materias disponibles: " + ex.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbMaterias.DataSource = null;
                            cmbMaterias.Enabled = false;
                            recordAcademicoActual = new List<Inscripcion>();
                            ActualizarRecordDGV(); // Limpia la vista
                        }
                    }
                    else
                    {
                        // Resetear la interfaz si se selecciona el placeholder
                        cmbMaterias.DataSource = null;
                        cmbMaterias.Enabled = false;
                        recordAcademicoActual = new List<Inscripcion>();
                        ActualizarRecordDGV();
                        txtNota.Clear();
                    }
                }
            }
        }

        private void cmbMaterias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMaterias.SelectedValue != null && cmbMaterias.SelectedValue is int)
            {
                idMatriculaSeleccionada = (int)cmbMaterias.SelectedValue;
                if (idMatriculaSeleccionada > 0)
                {
                    txtNota.Focus();
                }
            }
            else
            {
                idMatriculaSeleccionada = 0;
            }
        }

        private void txtNota_TextChanged(object sender, EventArgs e)
        {
            // Lógica para limitar la entrada a números y quizás decimales
        }

        // ----------------------------------------------------------------------
        // EVENTO: Botón REGISTRAR NOTA
        // ----------------------------------------------------------------------

        private void btnGuardarNota_Click(object sender, EventArgs e)
        {
            if (idMatriculaSeleccionada <= 0)
            {
                MessageBox.Show("Debe seleccionar una sección pendiente para registrar la nota.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtNota.Text, out double nota))
            {
                MessageBox.Show("Por favor, ingrese una nota numérica válida (Ej. 85.5).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNota.Focus();
                return;
            }

            string mensaje;

            try
            {
                if (cnCalificaciones.GuardarCalificacion(idMatriculaSeleccionada, nota, out mensaje))
                {
                    MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Recargar los datos del estudiante actual para actualizar el récord y el ComboBox
                    cmbEstudiantes_SelectedIndexChanged(sender, e);

                    txtNota.Clear();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            // CAPTURA DE LA EXCEPCIÓN DE NEGOCIO (CalificacionInvalidaException)
            catch (CalificacionInvalidaException ex)
            {
                MessageBox.Show(ex.Message, "Validación de Calificación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNota.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error crítico al guardar la nota: {ex.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ----------------------------------------------------------------------
        // OTROS EVENTOS
        // ----------------------------------------------------------------------
        private void frmCalificaciones_Load(object sender, EventArgs e) { /* vacío */ }
        private void dgvRecord_CellContentClick(object sender, DataGridViewCellEventArgs e) { /* vacío */ }
        private void lblIndice_Click(object sender, EventArgs e) { /* vacío */ }
    }
}