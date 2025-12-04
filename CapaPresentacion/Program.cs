using System;
using System.Windows.Forms;

namespace CapaPresentacion
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Iniciar con el Login
            Login loginForm = new Login();

            // ShowDialog() detiene la ejecución aquí hasta que el Login se cierre
            DialogResult resultado = loginForm.ShowDialog();

            // Si el login fue exitoso, NO HACEMOS NADA AQUÍ
            // Porque frmBarra ya abrió el menú principal
            // Solo dejamos que la aplicación siga corriendo
            if (resultado == DialogResult.OK)
            {
                // La barra de carga ya abrió el menú, así que solo mantenemos la aplicación viva
                // Application.Run() sin parámetros mantiene la aplicación corriendo
                // hasta que todas las ventanas se cierren
                Application.Run();
            }
            // Si el login falló o se canceló, la aplicación termina naturalmente
        }
    }
}