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
    public partial class frmEstudiantes : Form
    {
        public frmEstudiantes()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Aqui tengo que poner que siempre se ponga en mayuscula
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

        private void frmEstudiantes_Load(object sender, EventArgs e)
        {
           
        }
    }
}
