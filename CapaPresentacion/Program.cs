namespace CapaPresentacion
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // 1. Creamos el Login (pero no iniciamos la App con él todavía)
            Login login = new Login(); // Asegúrate de que tu clase se llame 'Login' o 'frmLogin'

            // 2. Lo mostramos como un Diálogo (Modal)
            // Esto pausa el código aquí hasta que el usuario entre o cierre la ventana.
            if (login.ShowDialog() == DialogResult.OK)
            {
                // 3. Si el Login dijo "OK" (Usuario válido), entonces AHORA SÍ arrancamos el Menú
                Application.Run(new frmMenuPrincipal());
            }
            else
            {
                // 4. Si cerró el Login sin entrar, matamos el proceso
                Application.Exit();
            }
        }
    }
}
