using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CapaNegocio.Base;
using CapaNegocio; // Clase que contiene toda la lógica de negocio y SQL
using System.Text.RegularExpressions; // Necesario para la validación del formato de matrícula


namespace CapaPresentacion
{
    public partial class frmEstudiantes : Form
    {
        // Variable para manejar la edición (0 = Nuevo, > 0 = IdEstudiante a editar)
        private int idEstudiante = 0;
        // Instancia de la Capa de Negocio
        private CN_Estudiante cnEstudiante = new CN_Estudiante();

        public frmEstudiantes()
        {
            InitializeComponent();
        }

        // ----------------------------------------------------------------------
        // MÉTODOS DE INICIALIZACIÓN Y CARGA (Load)
        // ----------------------------------------------------------------------

        private void frmEstudiantes_Load(object sender, EventArgs e)
        {
            // 1. Cargar las carreras en el ComboBox
            CargarCarreras();

            // 2. Cargar los estudiantes en el DataGridView
            CargarEstudiantes();

            // 3. Limpiar el formulario para un nuevo registro
            LimpiarFormulario();
        }

        private void CargarCarreras()
        {
            try
            {
                // La CN_Estudiante obtiene la lista de carreras de la DB
                DataTable dtCarreras = cnEstudiante.ObtenerCarreras();

                // Añadir una fila inicial para que el usuario seleccione
                DataRow newRow = dtCarreras.NewRow();
                newRow["Carrera"] = "-- SELECCIONE --";
                dtCarreras.Rows.InsertAt(newRow, 0);

                // Configurar ComboBox
                cmbCarrera.DataSource = dtCarreras;
                cmbCarrera.DisplayMember = "Carrera"; // Campo que se muestra
                cmbCarrera.ValueMember = "Carrera";   // Campo que se usa como valor
                cmbCarrera.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las carreras: " + ex.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarEstudiantes()
        {
            try
            {
                // La CN_Estudiante lista todos los estudiantes
                dgvEstudiantes.DataSource = cnEstudiante.ListarEstudiantes();
                ConfigurarDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los estudiantes: " + ex.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            // Ocultar columnas internas y dar formato a las visibles
            if (dgvEstudiantes.Columns.Contains("IdEstudiante"))
            {
                dgvEstudiantes.Columns["IdEstudiante"].Visible = false;
            }

            // Renombrar columnas para la vista del usuario
            if (dgvEstudiantes.Columns.Contains("Matricula")) dgvEstudiantes.Columns["Matricula"].HeaderText = "MATRÍCULA";
            if (dgvEstudiantes.Columns.Contains("Nombre")) dgvEstudiantes.Columns["Nombre"].HeaderText = "NOMBRE";
            if (dgvEstudiantes.Columns.Contains("Apellido")) dgvEstudiantes.Columns["Apellido"].HeaderText = "APELLIDO";
            if (dgvEstudiantes.Columns.Contains("Carrera")) dgvEstudiantes.Columns["Carrera"].HeaderText = "CARRERA";
            if (dgvEstudiantes.Columns.Contains("Email")) dgvEstudiantes.Columns["Email"].HeaderText = "EMAIL";
            if (dgvEstudiantes.Columns.Contains("Telefono")) dgvEstudiantes.Columns["Telefono"].HeaderText = "TELÉFONO";

            dgvEstudiantes.AutoResizeColumns();
        }

        // ----------------------------------------------------------------------
        // MÉTODOS DE ACCIÓN Y UTILIDAD
        // ----------------------------------------------------------------------

        private void LimpiarFormulario()
        {
            idEstudiante = 0; // Modo de inserción (INSERT)
            txtMatricula.Clear(); // Ahora la matrícula queda VACÍA al inicio
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();

            // Reinicia el combo al valor por defecto
            if (cmbCarrera.Items.Count > 0)
            {
                cmbCarrera.SelectedIndex = 0;
            }

            txtMatricula.Enabled = true; // La matrícula es editable para nuevos registros
            txtMatricula.Focus();

            // Se elimina la llamada a GenerarMatriculaPreview() para que el campo inicie vacío.
            // GenerarMatriculaPreview(); 
        }

        /// <summary>
        /// Genera una matrícula sugerida basada solo en el formato de año y placeholder numérico.
        /// (Mantenido solo como referencia, ya no se llama en LimpiarFormulario)
        /// Formato sugerido: LLYYYY-XXXX
        /// </summary>
        private void GenerarMatriculaPreview()
        {
            // Solo generar si estamos en modo de inserción (Nuevo)
            if (idEstudiante != 0) return;

            string year = DateTime.Now.ToString("yyyy");

            // Matrícula de ejemplo con el año actual y placeholder.
            txtMatricula.Text = $"LL{year}-XXXX";
        }

        /// <summary>
        /// Valida los campos obligatorios y el formato de la Matrícula, Email y Teléfono.
        /// </summary>
        /// <returns>True si la validación de UI es correcta.</returns>
        private bool ValidarCamposUI()
        {
            // ----------------------------------------------------------
            // 1. VALIDACIÓN DE MATRÍCULA (OBLIGATORIA Y FORMATO)
            // ----------------------------------------------------------
            string matricula = txtMatricula.Text.Trim();

            if (string.IsNullOrWhiteSpace(matricula))
            {
                MessageBox.Show("El campo Matrícula es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatricula.Focus();
                return false;
            }

            // Expresión Regular para el formato: [Letra][Letra][Año 4 dígitos]-[4 dígitos numéricos]
            // Ejemplo: RL2024-0404
            string patronMatricula = @"^[A-Z]{2}\d{4}-\d{4}$";

            if (!Regex.IsMatch(matricula, patronMatricula))
            {
                MessageBox.Show("El formato de Matrícula no es válido. Debe ser: [LL][AAAA]-[NNNN] (Ej: RL2024-0404)",
                                "Validación de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatricula.Focus();
                return false;
            }

            // ----------------------------------------------------------
            // 2. VALIDACIÓN DE OTROS CAMPOS OBLIGATORIOS
            // ----------------------------------------------------------

            if (cmbCarrera.SelectedValue == null || cmbCarrera.SelectedValue.ToString() == "-- SELECCIONE --")
            {
                MessageBox.Show("Debe seleccionar una Carrera válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCarrera.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El campo Nombre no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("El campo Apellido no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtApellido.Focus();
                return false;
            }

            // ----------------------------------------------------------
            // 3. VALIDACIÓN DE EMAIL (OBLIGATORIO Y FORMATO)
            // ----------------------------------------------------------
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("El campo Email es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // Expresión Regular para un email básico: nombre@dominio.ext
            string patronEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(email, patronEmail))
            {
                MessageBox.Show("El formato del Email no es válido (ej: usuario@dominio.com).",
                                "Validación de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // ----------------------------------------------------------
            // 4. VALIDACIÓN DE TELÉFONO (OBLIGATORIO Y FORMATO RD)
            // ----------------------------------------------------------
            string telefono = txtTelefono.Text.Trim();

            if (string.IsNullOrWhiteSpace(telefono))
            {
                MessageBox.Show("El campo Teléfono es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return false;
            }

            // Expresión Regular para formato de Teléfono Dominicano (809/829/849-XXX-XXXX)
            // Se acepta con o sin guiones, pero con 10 dígitos.
            // Patron: (809|829|849) seguido opcionalmente de guiones o espacios, y 7 dígitos.
            string patronTelefono = @"^(809|829|849)[\s-]?\d{3}[\s-]?\d{4}$";

            if (!Regex.IsMatch(telefono, patronTelefono))
            {
                MessageBox.Show("El formato de Teléfono no es válido. Debe ser 10 dígitos (Ej: 809-555-1234).",
                                "Validación de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return false;
            }


            return true;
        }

        // ----------------------------------------------------------------------
        // EVENTO: Botón GUARDAR (Insertar o Actualizar)
        // ----------------------------------------------------------------------

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        // ----------------------------------------------------------------------
        // EVENTO: Botón NUEVO (Limpiar Formulario)
        // ----------------------------------------------------------------------

        private void btnNuevo_Click(object sender, EventArgs e)
        {
        }

        // ----------------------------------------------------------------------
        // EVENTO: Clic en el DataGridView (Cargar datos para edición)
        // ----------------------------------------------------------------------

        private void dgvEstudiantes_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // ----------------------------------------------------------------------
        // EVENTO: Búsqueda en la caja de texto
        // ----------------------------------------------------------------------

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        // ----------------------------------------------------------------------
        // EVENTO: Convertir a Mayúsculas y Generar Matrícula
        // ----------------------------------------------------------------------

        /// <summary>
        /// Convierte el texto a mayúsculas.
        /// La lógica de generación de matrícula se movió a LimpiarFormulario().
        /// </summary>
        private void txt_ConvertToUpper_TextChanged(object sender, EventArgs e)
        {
            // 1. Convertir a Mayúsculas
            if (sender is Guna.UI2.WinForms.Guna2TextBox gunaTextBox)
            {
                int selectionStart = gunaTextBox.SelectionStart;
                gunaTextBox.Text = gunaTextBox.Text.ToUpper();
                gunaTextBox.SelectionStart = selectionStart; // Mantiene el cursor en su lugar

                // Se elimina la llamada a GenerarMatriculaPreview() para evitar problemas
                // con la actualización constante de la Matrícula. Ahora solo se genera una vez.
            }
        }

        // ----------------------------------------------------------------------
        // OTROS EVENTOS
        // ----------------------------------------------------------------------

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Métodos de eventos originales del diseñador (se dejan vacíos)
        private void textBox1_TextChanged(object sender, EventArgs e) { /* Este método fue reemplazado por txt_ConvertToUpper_TextChanged */ }
        private void guna2GroupBox1_Click(object sender, EventArgs e) { /* vacío */ }
        private void label1_Click(object sender, EventArgs e) { /* vacío */ }
        private void panelRegistro_Paint(object sender, PaintEventArgs e) { /* vacío */ }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
          
        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            LimpiarFormulario();

        }

        private void dgvEstudiantes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void txtBuscar_TextChanged_1(object sender, EventArgs e)
        {
           
        }

        private void btnGuardar_Click_2(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            // 0. Validación de la interfaz de usuario antes de llamar a la capa de negocio
            if (!ValidarCamposUI())
            {
                return;
            }

            // 1. Crear la Entidad Estudiante con los datos del formulario
            CE_Estudiante estudiante = new CE_Estudiante()
            {
                IdEstudiante = idEstudiante,
                Matricula = txtMatricula.Text.Trim(),
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                // Obtener el valor de la carrera seleccionada
                Carrera = cmbCarrera.SelectedValue?.ToString() ?? string.Empty,
                Email = txtEmail.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                // FIX: Asegurar que el SemestreActual se envíe como 1 para nuevos registros, 
                // ya que la DB tiene un constraint CHECK (SemestreActual >= 1).
                SemestreActual = (idEstudiante == 0) ? 1 : 0 // 0 no se usa en la actualización, solo lo pasamos para el INSERT.
            };

            // 2. Llamar a la Capa de Negocio para Guardar
            if (cnEstudiante.GuardarEstudiante(estudiante, out mensaje))
            {
                MessageBox.Show(mensaje, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recargar datos en el DGV y limpiar el formulario para la siguiente acción
                CargarEstudiantes();
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show(mensaje, "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click_2(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void dgvEstudiantes_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // Cargar los datos de la fila seleccionada al formulario para edición
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvEstudiantes.Rows[e.RowIndex];

                // Cargar el IdEstudiante para habilitar el modo de edición (UPDATE)
                idEstudiante = Convert.ToInt32(fila.Cells["IdEstudiante"].Value);

                // Cargar el resto de los campos
                txtMatricula.Text = fila.Cells["Matricula"].Value.ToString();
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                txtApellido.Text = fila.Cells["Apellido"].Value.ToString();
                txtEmail.Text = fila.Cells["Email"].Value.ToString();
                txtTelefono.Text = fila.Cells["Telefono"].Value.ToString();

                // Seleccionar la carrera en el ComboBox
                string carrera = fila.Cells["Carrera"].Value.ToString();
                cmbCarrera.SelectedValue = carrera;

                // Deshabilitar la edición de la matrícula si es una actualización
                txtMatricula.Enabled = false;
            }
        }

        private void txtBuscar_TextChanged_2(object sender, EventArgs e)
        {
            string textoBusqueda = txtBuscar.Text.Trim();

            try
            {
                // Llamar al método de búsqueda de la CN y actualizar el DataGridView
                dgvEstudiantes.DataSource = cnEstudiante.BuscarEstudiantes(textoBusqueda);
                ConfigurarDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message, "Error de Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}