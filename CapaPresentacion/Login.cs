using CapaNegocio.Acceso;
using CapaNegocio.Interfaz;
using CapaNegocio.Excepciones;
using CapaDatos.Conexion;
using CapaNegocio.Servicios;
using CapaNegocio.ServiciosCD;
using CapaNegocio.Base;
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
    public partial class Login : Form
    {
        // Opción 1: Si el bueno está en Acceso
        private UsuarioService _authService = new UsuarioService();

        public Login()
        {
            InitializeComponent();
        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "USUARIO")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.Black;
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "USUARIO";
                txtUsuario.ForeColor = Color.DimGray;
            }
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            txtPass.PasswordChar = '\u2022';

            if (txtPass.Text == "CONTRASEÑA")
            {
                txtPass.Text = "";
                txtPass.ForeColor = Color.Black;
            }
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.Text = "CONTRASEÑA";
                txtPass.ForeColor = Color.DimGray;


                txtPass.PasswordChar = '\0';
            }

        }

        private void btnMostrar_CheckedChanged(object sender, EventArgs e)
        {

            if (((Guna.UI2.WinForms.Guna2ToggleSwitch)sender).Checked)
            {

                txtPass.PasswordChar = '\0';
            }
            else
            {

                txtPass.PasswordChar = '\u2022';
            }
        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            // 1. Obtener datos
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtPass.Text;

            // 2. Validar vacíos
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                // Asegúrate de tener este Label en el diseño o usa MessageBox
                if (lblError != null)
                {
                    lblError.Text = "Ingrese usuario y contraseña.";
                    lblError.Visible = true;
                }
                else
                {
                    MessageBox.Show("Ingrese usuario y contraseña.");
                }
                return;
            }

            try
            {
                // 3. Llamar al servicio
                Usuario usuarioLogueado = _authService.ValidarAcceso(usuario, contrasena);

                // 4. Éxito
                MessageBox.Show($"Bienvenido {usuarioLogueado.NombreCompleto}", "Sistema Académico");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                // 5. Error
                if (lblError != null)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
                else
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                txtPass.Clear();
                txtUsuario.Focus();
            }
        }

        // Método auxiliar para mostrar el error en el Label de Guna y ocultarlo luego si se desea
        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }

    }
}
