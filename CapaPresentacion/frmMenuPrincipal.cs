using System;
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
    public partial class frmMenuPrincipal : Form
    {
        // Variable para saber qué formulario está activo
        private Form formularioActivo = null;

        // Variable para controlar el estado del menú (True = Expandido, False = Minimizado)
        private bool sidebarExpand = true;

        public frmMenuPrincipal()
        {
            InitializeComponent();
        }

        // --- 1. DEFINIMOS LOS TAMAÑOS AQUÍ (Para no depender del diseñador) ---
        // Ajusta estos números según tus necesidades
        private const int ANCHO_MAXIMO = 250; // El ancho cuando está abierto
        private const int ANCHO_MINIMO = 60;  // El ancho cuando está cerrado (solo íconos)
        private const int VELOCIDAD = 20;     // Qué tan rápido se mueve (más alto = más rápido)

        private void SidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                // Si está expandido, reducimos el ancho
                panelMenuLateral.Width -= 10;

                // CONDICIÓN DE CIERRE (El hueco de la imagen)
                if (panelMenuLateral.Width <= 70)
                {
                    sidebarExpand = false;
                    SidebarTimer.Stop();
                }
            }
            else
            {
                // Si está cerrado, aumentamos el ancho
                panelMenuLateral.Width += 10;

                // CONDICIÓN DE APERTURA (El hueco de la imagen)
                if (panelMenuLateral.Width >= 250)
                {
                    sidebarExpand = true;
                    SidebarTimer.Stop();
                }
            }
        }

        // Evento del Botón Hamburguesa (Las 3 rayitas)
        private void btnMenu_Click_2(object sender, EventArgs e)
        {
            // Iniciamos el timer, la lógica de arriba decide si abrir o cerrar
            SidebarTimer.Start();
        }




        // --- LÓGICA PARA ABRIR FORMULARIOS HIJOS ---
        private void AbrirFormularioHijo(Form formularioHijo)
        {
            // Si hay uno abierto, lo cerramos
            if (formularioActivo != null)
                formularioActivo.Close();

            formularioActivo = formularioHijo;

            // Configuramos el formulario hijo para que se comporte como un control
            formularioHijo.TopLevel = false;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;
            formularioHijo.Dock = DockStyle.Fill;

            // Lo agregamos al panel contenedor (el beige)
            panelContenedor.Controls.Add(formularioHijo);
            panelContenedor.Tag = formularioHijo;

            // Lo mostramos
            formularioHijo.BringToFront();
            formularioHijo.Show();
        }

        // --- EVENTOS DE LOS BOTONES DE NAVEGACIÓN ---

        private void btnEstudiantes_Click_1(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new frmEstudiantes());
        }

        private void btnInscripciones_Click(object sender, EventArgs e)
        {
            // AbrirFormularioHijo(new frmInscripciones());
            MessageBox.Show("Aquí se abrirá Inscripciones");
        }

        private void btnCalificaciones_Click(object sender, EventArgs e)
        {
            // AbrirFormularioHijo(new frmCalificaciones());
            MessageBox.Show("Aquí se abrirá Calificaciones");
        }

        private void btnConsultas_Click_1(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FrmConsultas());
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
            "¿Está seguro que desea salir del Sistema de Gestión Académica?",
            "Confirmar Salida",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                // Esta línea detiene todos los hilos y cierra la aplicación por completo.
                Application.Exit();
            }
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {

        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {

        }

        private void panelContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new frmCalificaciones());
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new frmInscripciones());
        }
    }
}