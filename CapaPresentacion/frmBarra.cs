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
    public partial class frmBarra : Form
    {
        public frmBarra()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void frmBarra_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value += 2;
                label3.Text = progressBar1.Value.ToString() + "%";
            }
            else
            {
                timer1.Stop();

                // 1. Instanciar y mostrar el menú principal (frmMenuPrincipal)
                frmMenuPrincipal menuPrincipal = new frmMenuPrincipal();
                menuPrincipal.Show();

                // 2. Cerrar la barra de carga
                this.Close(); // <-- Usar Close() para cerrar la barra.
            }
        }
    }
}
