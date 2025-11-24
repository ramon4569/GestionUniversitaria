namespace CapaPresentacion
{
    public partial class Form1 : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;




        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
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
            // 1. SIEMPRE establece el carácter de ocultación con el punto sólido.
            // El carácter '\u2022' es el punto sólido.
            txtPass.PasswordChar = '\u2022';

            // 2. Solo borra el placeholder si el texto es "CONTRASEÑA".
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

                // 3. Quita el carácter de ocultación (lo deja vacío).
                txtPass.PasswordChar = '\0';
            }
            // Si el usuario escribió algo, el PasswordChar = '\u2022' se mantiene.
        }

        private void btnOcultar_Click(object sender, EventArgs e)
        {
            if (txtPass.UseSystemPasswordChar == true)
            {
                // 1. Mostrar la contraseña
                txtPass.UseSystemPasswordChar = false;

                // 2. Cambiar el icono a Ojo Abierto (para indicar que está visible)
                // Reemplaza 'Recursos.ojo_abierto' con el nombre de tu imagen importada
                btnOcultar.Image = Properties.Resources.OjitoCerrado;
            }
            else
            {
                // 1. Ocultar la contraseña
                txtPass.UseSystemPasswordChar = true;

                // 2. Cambiar el icono a Ojo Cerrado (para indicar que está oculta)
                // Reemplaza 'Recursos.ojo_cerrado' con el nombre de tu imagen importada
                btnOcultar.Image = Properties.Resources.ojoAbierto;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
