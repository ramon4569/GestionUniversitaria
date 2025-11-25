using CapaNegocio.Acceso;
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
        private AutenticacionService _authService = new AutenticacionService();
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
            // 1. Obtener las credenciales de los Guna2TextBox
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtPass.Text;

            // 2. Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                // Usar un control Guna2HtmlLabel para mostrar el error visualmente
                lblError.Text = "Por favor, ingrese usuario y contraseña.";
                lblError.Visible = true;
                return; // Detener la ejecución
            }

            try
            {
                // 3. Llamar a la Capa de Negocio para validar
                bool credencialesValidas = _authService.ValidarCredenciales(usuario, contrasena);

                if (credencialesValidas)
                {
                    // 4. Autenticación exitosa: Abrir el Menú Principal

                    // Ocultar el formulario de login (no cerrarlo, solo ocultarlo)
                    this.Hide();

                    // Crear y mostrar el formulario principal
                    frmMenuPrincipal menuPrincipal = new frmMenuPrincipal();
                    menuPrincipal.Show();

                    // Nota: Podrías usar this.Close() y en Program.cs usar Application.Run(new frmMenuPrincipal())
                    // pero this.Hide() es más simple si quieres volver al login después de cerrar el menú.
                }
                else
                {
                    // 5. Credenciales inválidas: Mostrar mensaje de error
                    lblError.Text = "Credenciales incorrectas. Verifique su usuario o contraseña.";
                    lblError.Visible = true;
                    txtPass.Clear(); // Limpiar la contraseña por seguridad
                    txtUsuario.Focus(); // Poner el foco en el usuario para reintento
                }
            }
            catch (Exception ex)
            {
                // Manejo general de errores (ej. si la BLL lanza una excepción inesperada)
                MessageBox.Show("Ocurrió un error inesperado al intentar iniciar sesión: " + ex.Message,
                                "Error del Sistema",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}
