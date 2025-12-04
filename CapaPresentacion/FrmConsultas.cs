using System;
using CapaNegocio.Servicios;
using CapaNegocio.ServiciosCD;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmConsultas : Form
    {
        private ConsultasService _servicio = new ConsultasService();
        public FrmConsultas()
        {
            InitializeComponent();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            // Asegúrate de que el contenedor es el MenuPrincipal antes de intentar la conversión.
            if (this.Parent.Parent is frmMenuPrincipal menuPrincipal)
            {
                // 1. Cierra el formulario actual
                this.Close();

                // 2. Llama a un método del padre para mostrar la pantalla de bienvenida.
                // menuPrincipal.MostrarBienvenida(); 
            }
            else
            {
                // Si no está incrustado, simplemente cierra
                this.Close();
            }
        }

        private void FrmConsultas_Load(object sender, EventArgs e)
        {
            // ========== CONFIGURACIÓN DEL DATAGRIDVIEW ==========
            // IMPORTANTE: Establecer AutoGenerateColumns ANTES de cargar datos
            gunaDgvConsultas.AutoGenerateColumns = true;
            gunaDgvConsultas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gunaDgvConsultas.AllowUserToAddRows = false;
            gunaDgvConsultas.ReadOnly = true;
            gunaDgvConsultas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // ========== CARGAR DATOS INICIALES ==========
            CargarGrid(_servicio.ObtenerListadoGeneral());

            // ========== CONFIGURAR COMBOBOX DE CARRERAS ==========
            gunaCmbCarreras.Items.Clear();
            gunaCmbCarreras.Items.Add("Todas");

            // Facultad de Ciencias de la Salud
            gunaCmbCarreras.Items.Add("Medicina");
            gunaCmbCarreras.Items.Add("Odontología");
            gunaCmbCarreras.Items.Add("Bioanálisis");
            gunaCmbCarreras.Items.Add("Enfermería");
            gunaCmbCarreras.Items.Add("Farmacia");

            // Facultad de Ingenierías y Sistemas
            gunaCmbCarreras.Items.Add("Ingeniería de Sistemas");
            gunaCmbCarreras.Items.Add("Ingeniería Civil");
            gunaCmbCarreras.Items.Add("Ingeniería Industrial");
            gunaCmbCarreras.Items.Add("Ingeniería Electromecánica");
            gunaCmbCarreras.Items.Add("Agrimensura");

            // Facultad de Ciencias Jurídicas
            gunaCmbCarreras.Items.Add("Derecho");

            // Facultad de Ciencias Administrativas
            gunaCmbCarreras.Items.Add("Administración de Empresas");
            gunaCmbCarreras.Items.Add("Contabilidad");
            gunaCmbCarreras.Items.Add("Mercadeo");
            gunaCmbCarreras.Items.Add("Turismo");

            // Facultad de Arquitectura y Artes
            gunaCmbCarreras.Items.Add("Arquitectura");

            // Educación y Psicología
            gunaCmbCarreras.Items.Add("Psicología");
            gunaCmbCarreras.Items.Add("Educación");

            gunaCmbCarreras.SelectedIndex = 0;
        }


      
        private void CargarGrid(object datos)
        {
            try
            {
                gunaDgvConsultas.SuspendLayout();
                gunaDgvConsultas.DataSource = null;
                gunaDgvConsultas.Columns.Clear();
                gunaDgvConsultas.DataSource = datos;

                // Ocultar columnas individuales si existen
                if (gunaDgvConsultas.Columns.Contains("Nombre"))
                    gunaDgvConsultas.Columns["Nombre"].Visible = false;

                if (gunaDgvConsultas.Columns.Contains("Apellido"))
                    gunaDgvConsultas.Columns["Apellido"].Visible = false;

                // Agregar columna calculada
                if (gunaDgvConsultas.Columns.Contains("Nombre") &&
                    gunaDgvConsultas.Columns.Contains("Apellido"))
                {
                    DataGridViewTextBoxColumn colNombreCompleto = new DataGridViewTextBoxColumn
                    {
                        Name = "NombreCompleto",
                        HeaderText = "Nombre Completo",
                        MinimumWidth = 200,
                        DisplayIndex = 1 // Posición después de Matrícula
                    };
                    gunaDgvConsultas.Columns.Add(colNombreCompleto);

                    // Llenar la columna con datos
                    foreach (DataGridViewRow row in gunaDgvConsultas.Rows)
                    {
                        if (row.Cells["Nombre"].Value != null &&
                            row.Cells["Apellido"].Value != null)
                        {
                            row.Cells["NombreCompleto"].Value =
                                $"{row.Cells["Nombre"].Value} {row.Cells["Apellido"].Value}";
                        }
                    }
                }

                // Resto del código...
                gunaDgvConsultas.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}");
            }
        }


        private void btnRiesgo_Click(object sender, EventArgs e)
        {
            try
            {
                var lista = _servicio.ObtenerEstudiantesEnRiesgo();
                CargarGrid(lista);

                if (lblTitulo != null) lblTitulo.Text = "REPORTE: ALUMNOS EN RIESGO ACADÉMICO";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar reporte: " + ex.Message);
            }
        }

        private void btnPromedios_Click(object sender, EventArgs e)
        {
            try
            {
                var lista = _servicio.ObtenerPromedioPorCarrera();
                CargarGrid(lista);

                if (lblTitulo != null) lblTitulo.Text = "ESTADÍSTICAS: PROMEDIO GLOBAL POR CARRERA";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnReprobadas_Click(object sender, EventArgs e)
        {
            try
            {
                var lista = _servicio.ObtenerMateriasReprobadas();
                CargarGrid(lista);

                if (lblTitulo != null) lblTitulo.Text = "REPORTE: MATERIAS CON MAYOR ÍNDICE DE REPROBACIÓN";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnTop10_Click(object sender, EventArgs e)
        {
            try
            {
                var lista = _servicio.ObtenerTopEstudiantes();
                CargarGrid(lista);

                if (lblTitulo != null) lblTitulo.Text = "CUADRO DE HONOR: TOP 10 ESTUDIANTES";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnVerTodo_Click(object sender, EventArgs e)
        {
            CargarDatosIniciales();
        }

        private void CargarDatosIniciales()
        {
            var lista = _servicio.ObtenerListadoGeneral();
            CargarGrid(lista);
            if (lblTitulo != null) lblTitulo.Text = "LISTADO GENERAL DE ESTUDIANTES";
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            string texto = TxtBuscar.Text;

            // Llamamos al método de búsqueda del servicio
            var resultados = _servicio.BuscarEstudiante(texto);

            CargarGrid(resultados);
        }

        private void gunaCmbCarreras_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Validamos que haya algo seleccionado para evitar errores
            if (gunaCmbCarreras.SelectedItem == null) return;

            string seleccion = gunaCmbCarreras.SelectedItem.ToString();

            if (seleccion == "Todas")
            {
                // Si selecciona "Todas", mostramos la lista general completa
                CargarGrid(_servicio.ObtenerListadoGeneral());
                if (lblTitulo != null) lblTitulo.Text = "LISTADO GENERAL";
            }
            else
            {
                // Si selecciona una carrera específica, filtramos
                var listaFiltrada = _servicio.ObtenerPorCarrera(seleccion);
                CargarGrid(listaFiltrada);

                // Actualizamos el título para que se vea cool
                if (lblTitulo != null) lblTitulo.Text = $"ESTUDIANTES DE {seleccion.ToUpper()}";
            }
        }

        // =========================================================
        //                 FILTROS Y BÚSQUEDA
        // =========================================================


    }
}
