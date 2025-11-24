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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            
            // He configurado que inicie con el Login.
            // Si en algún momento quieres probar directo el menú, cambia "new Login()" por "new frmMenuPrincipal()".
            Application.Run(new Login());
        }
    }
}
