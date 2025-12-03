using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using CapaNegocio;
using CapaNegocio.Base;
using CapaNegocio.Excepciones.CapaNegocio.Excepciones;

namespace CapaPresentacion
{
    public partial class frmCalificaciones : Form
    {
        private CN_Calificaciones cnCalificaciones = new CN_Calificaciones();
        private List<Inscripcion> recordAcademicoActual;

        private int idEstudianteSeleccionado = 0;
        private int idMatriculaSeleccionada = 0;
        private bool modoEdicion = false; // Bandera para saber si estamos editando

        public frmCalificaciones()
        {
            InitializeComponent();
        }

        // ----------------------------------------------------------------------
        // MÉTODOS DE INICIALIZACIÓN Y CARGA
        // ----------------------------------------------------------------------

        private void frmCalificaciones_Load_1(object sender, EventArgs e)
        {
            // 1. Configuración de la Interfaz
            dgvRecord.ReadOnly = true;

            // 2. Cargar la lista de estudiantes al iniciar
            CargarEstudiantes();

            // 3. Inicializar ComboBox de Materias
            cmbMaterias.DataSource = null;
            cmbMaterias.Enabled = false;

            // 4. Inicializar el label del índice
            ActualizarLabelIndice("0.00");

            // 5. Configurar botones
            btnEditarNota.Enabled = false;
            btnGuardarNota.Text = "REGISTRAR NOTA";
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
                MessageBox.Show("Error crítico al cargar la lista de estudiantes: " + ex.Message, "ERROR CRÍTICO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbEstudiantes.Enabled = false;
            }
        }

        private void ActualizarRecordDGV()
        {
            // 1. Validar que haya datos
            if (recordAcademicoActual == null || !recordAcademicoActual.Any())
            {
                dgvRecord.DataSource = null;
                ActualizarLabelIndice("0.00");
                return;
            }

            // 2. Preparar datos para el DataGridView
            var dataParaDGV = recordAcademicoActual
                .Where(i => i.Materia != null)
                .Select(i => new
                {
                    Materia = i.Materia.Nombre,
                    Código = i.Materia.Codigo,
                    Sección = i.CodigoSeccion,
                    Créditos = i.Materia.Creditos,
                    Nota_Final = i.CalificacionFinal.HasValue 
                        ? i.CalificacionFinal.Value.ToString("N2") 
                        : "PENDIENTE",
                    Estado = i.CalificacionFinal.HasValue 
                        ? (i.CalificacionFinal.Value >= 70 ? "APROBADO" : "REPROBADO")
                        : "PENDIENTE",
                    IdMatricula = i.IdMatricula
                })
                .ToList();

            dgvRecord.DataSource = dataParaDGV;

            // 3. Configurar el DGV (ocultar ID y formato)
            if (dgvRecord.Columns.Contains("IdMatricula"))
            {
                dgvRecord.Columns["IdMatricula"].Visible = false;
            }

            // 4. Aplicar colores según el estado
            foreach (DataGridViewRow row in dgvRecord.Rows)
            {
                if (row.Cells["Estado"].Value != null)
                {
                    string estado = row.Cells["Estado"].Value.ToString();
                    
                    if (estado == "APROBADO")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else if (estado == "REPROBADO")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                    }
                    else // PENDIENTE
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                }
            }

            dgvRecord.AutoResizeColumns();

            // 5. Calcular y mostrar el índice académico
            string indice = cnCalificaciones.CalcularIndiceAcademico(recordAcademicoActual);
            ActualizarLabelIndice(indice);
        }

        /// <summary>
        /// Actualiza el label del índice académico con formato y color
        /// </summary>
        private void ActualizarLabelIndice(string indiceTexto)
        {
            lblIndice.Text = $"ÍNDICE ACADÉMICO: {indiceTexto}";

            // Aplicar color según el valor del índice
            if (decimal.TryParse(indiceTexto, out decimal indiceValor))
            {
                if (indiceValor >= 3.5m)
                {
                    lblIndice.ForeColor = Color.DarkGreen; // Excelente
                }
                else if (indiceValor >= 3.0m)
                {
                    lblIndice.ForeColor = Color.Green; // Muy Bueno
                }
                else if (indiceValor >= 2.5m)
                {
                    lblIndice.ForeColor = Color.Orange; // Bueno
                }
                else if (indiceValor >= 2.0m)
                {
                    lblIndice.ForeColor = Color.DarkOrange; // Regular
                }
                else
                {
                    lblIndice.ForeColor = Color.Red; // En Riesgo
                }

                // Opcional: Hacer el label más grande si es excelente
                if (indiceValor >= 3.5m)
                {
                    lblIndice.Font = new Font(lblIndice.Font.FontFamily, 10, FontStyle.Bold);
                }
                else
                {
                    lblIndice.Font = new Font(lblIndice.Font.FontFamily, 9, FontStyle.Regular);
                }
            }
            else
            {
                lblIndice.ForeColor = Color.Black;
            }
        }

        // ----------------------------------------------------------------------
        // EVENTOS DE COMBOBOX
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

                            // 3. Filtrar las materias pendientes para el ComboBox de registro
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

                            // Si no hay materias pendientes, mostrar mensaje informativo
                            if (materiasPendientes.Count == 1) // Solo el placeholder
                            {
                                cmbMaterias.Enabled = false;
                                label2.Text = "No hay materias pendientes";
                                label2.ForeColor = Color.Gray;
                                lblInstruccion.Visible = true; // Mostrar instrucción de edición
                            }
                            else
                            {
                                cmbMaterias.Enabled = true;
                                label2.Text = "Ingresar Nota";
                                label2.ForeColor = Color.Black;
                                lblInstruccion.Visible = false;
                            }
                            
                            // 4. Habilitar botón de editar si hay materias calificadas
                            bool hayMateriasCalificadas = recordAcademicoActual.Any(i => i.CalificacionFinal.HasValue);
                            if (hayMateriasCalificadas)
                            {
                                btnEditarNota.Enabled = false; // Se habilitará al hacer click en una fila
                                btnEditarNota.FillColor = Color.Gray;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al cargar las materias disponibles: " + ex.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbMaterias.DataSource = null;
                            cmbMaterias.Enabled = false;
                            recordAcademicoActual = new List<Inscripcion>();
                            ActualizarRecordDGV();
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
                        btnEditarNota.Enabled = false;
                        btnEditarNota.FillColor = Color.Gray;
                        label2.Text = "Ingresar Nota";
                        label2.ForeColor = Color.Black;
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
            // Validación en tiempo real: solo números y un punto decimal
            if (!string.IsNullOrEmpty(txtNota.Text))
            {
                string texto = txtNota.Text;
                // Permitir solo dígitos y un punto
                if (!System.Text.RegularExpressions.Regex.IsMatch(texto, @"^[0-9]*\.?[0-9]*$"))
                {
                    // Remover el último carácter inválido
                    txtNota.Text = texto.Substring(0, texto.Length - 1);
                    txtNota.SelectionStart = txtNota.Text.Length;
                }
            }
        }

        // ----------------------------------------------------------------------
        // EVENTO: Botón REGISTRAR NOTA
        // ----------------------------------------------------------------------

        private void btnGuardarNota_Click(object sender, EventArgs e)
        {
            // Modo Edición
            if (modoEdicion)
            {
                EditarCalificacion();
            }
            // Modo Registro Normal
            else
            {
                RegistrarNuevaCalificacion();
            }
        }

        private void RegistrarNuevaCalificacion()
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

                    // Recargar los datos del estudiante actual
                    cmbEstudiantes_SelectedIndexChanged(this, EventArgs.Empty);

                    txtNota.Clear();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        private void EditarCalificacion()
        {
            if (idMatriculaSeleccionada <= 0)
            {
                MessageBox.Show("Debe seleccionar una materia desde el récord para editarla.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtNota.Text, out double nota))
            {
                MessageBox.Show("Por favor, ingrese una nota numérica válida (Ej. 85.5).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNota.Focus();
                return;
            }

            var resultado = MessageBox.Show(
                "¿Está seguro de que desea modificar esta calificación?", 
                "Confirmar Edición", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question
            );

            if (resultado != DialogResult.Yes)
            {
                return;
            }

            string mensaje;

            try
            {
                if (cnCalificaciones.GuardarCalificacion(idMatriculaSeleccionada, nota, out mensaje))
                {
                    MessageBox.Show("Calificación actualizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Salir del modo edición
                    CancelarEdicion();

                    // Recargar los datos
                    cmbEstudiantes_SelectedIndexChanged(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show(mensaje, "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (CalificacionInvalidaException ex)
            {
                MessageBox.Show(ex.Message, "Validación de Calificación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNota.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error crítico al actualizar la nota: {ex.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ----------------------------------------------------------------------
        // EVENTO: Botón EDITAR NOTA
        // ----------------------------------------------------------------------

        private void btnEditarNota_Click(object sender, EventArgs e)
        {
            // Si estamos en modo edición, este botón funciona como CANCELAR
            if (modoEdicion)
            {
                CancelarEdicion();
                return;
            }

            // Modo normal: iniciar edición
            if (dgvRecord.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una materia del récord para editar su calificación.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var filaSeleccionada = dgvRecord.SelectedRows[0];
            
            // Verificar si la materia tiene nota
            string notaTexto = filaSeleccionada.Cells["Nota_Final"].Value?.ToString();
            
            if (string.IsNullOrEmpty(notaTexto) || notaTexto == "PENDIENTE")
            {
                MessageBox.Show("No puede editar una materia sin calificación. Use 'REGISTRAR NOTA' para asignarle una nota.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el IdMatricula
            if (filaSeleccionada.Cells["IdMatricula"].Value != null)
            {
                idMatriculaSeleccionada = Convert.ToInt32(filaSeleccionada.Cells["IdMatricula"].Value);
                
                // Cargar la nota actual en el TextBox
                txtNota.Text = notaTexto;
                
                // Activar modo edición
                ActivarModoEdicion();
                
                // Enfocar el TextBox
                txtNota.Focus();
                txtNota.SelectAll();
            }
        }

        // ----------------------------------------------------------------------
        // EVENTO: Click en celda del DataGridView
        // ----------------------------------------------------------------------

        private void dgvRecord_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Habilitar el botón de editar solo si hay una fila seleccionada
            if (e.RowIndex >= 0 && !modoEdicion)
            {
                var notaTexto = dgvRecord.Rows[e.RowIndex].Cells["Nota_Final"].Value?.ToString();
                
                // Solo permitir edición si la nota existe (no es PENDIENTE)
                if (!string.IsNullOrEmpty(notaTexto) && notaTexto != "PENDIENTE")
                {
                    btnEditarNota.Enabled = true;
                    btnEditarNota.FillColor = Color.FromArgb(52, 152, 219); // Azul activo
                }
                else
                {
                    btnEditarNota.Enabled = false;
                    btnEditarNota.FillColor = Color.Gray; // Gris deshabilitado
                }
            }
        }

        // ----------------------------------------------------------------------
        // MÉTODOS AUXILIARES PARA MODO EDICIÓN
        // ----------------------------------------------------------------------

        private void ActivarModoEdicion()
        {
            modoEdicion = true;
            
            // Cambiar apariencia del botón
            btnGuardarNota.Text = "ACTUALIZAR NOTA";
            btnGuardarNota.FillColor = Color.FromArgb(230, 126, 34); // Naranja para indicar edición
            
            // Deshabilitar controles que no deben usarse en edición
            cmbEstudiantes.Enabled = false;
            cmbMaterias.Enabled = false;
            
            // Cambiar el botón de editar para que funcione como cancelar
            btnEditarNota.Text = "CANCELAR";
            btnEditarNota.FillColor = Color.FromArgb(231, 76, 60); // Rojo para cancelar
        }

        private void CancelarEdicion()
        {
            modoEdicion = false;
            
            // Restaurar apariencia original
            btnGuardarNota.Text = "REGISTRAR NOTA";
            btnGuardarNota.FillColor = Color.FromArgb(90, 139, 76);
            
            // Reactivar controles
            cmbEstudiantes.Enabled = true;
            cmbMaterias.Enabled = true;
            
            // Restaurar botón de editar
            btnEditarNota.Text = "EDITAR NOTA";
            btnEditarNota.FillColor = Color.FromArgb(52, 152, 219);
            btnEditarNota.Enabled = false;
            
            // Limpiar campos
            txtNota.Clear();
            idMatriculaSeleccionada = 0;
        }

        // ----------------------------------------------------------------------
        // OTROS EVENTOS (vacíos por compatibilidad)
        // ----------------------------------------------------------------------
        private void frmCalificaciones_Load(object sender, EventArgs e) { }
        private void dgvRecord_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void lblIndice_Click(object sender, EventArgs e) { }
    }
}