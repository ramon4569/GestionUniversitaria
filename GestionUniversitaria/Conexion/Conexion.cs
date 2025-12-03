using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace CapaDatos.Conexion
{
    public class Conexion
    {
        // La cadena de conexión como campo privado
        private readonly string cadenaConexion = "Server=.;Database=GestionAcademicaUCE;Integrated Security=True;TrustServerCertificate=True;";

        public Conexion()
        {
            // Validar que la cadena de conexión sea válida al instanciar
            try
            {
                // Verificar que la cadena sea válida creando temporalmente una conexión
                using (var testConn = new SqlConnection(cadenaConexion))
                {
                    // No hacemos nada, solo validamos que la cadena sea correcta
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Error al inicializar la conexión. Verifique la cadena de conexión en la clase Conexion.cs y asegúrese de que el servidor SQL esté activo.",
                    ex);
            }
        }

        /// <summary>
        /// Devuelve una NUEVA instancia de SqlConnection cada vez que se llama.
        /// Esto permite usar el patrón 'using' correctamente.
        /// </summary>
        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }

        // MÉTODOS LEGACY (para compatibilidad con código antiguo que no usa 'using')
        // Estos métodos mantienen una conexión interna para abrir/cerrar manualmente
        private SqlConnection conexionManual;

        public void Abrir()
        {
            if (conexionManual == null)
            {
                conexionManual = new SqlConnection(cadenaConexion);
            }

            if (conexionManual.State == ConnectionState.Closed)
            {
                conexionManual.Open();
            }
        }

        public void Cerrar()
        {
            if (conexionManual != null && conexionManual.State == ConnectionState.Open)
            {
                conexionManual.Close();
            }
        }

        public SqlConnection ObtenerConexionManual()
        {
            if (conexionManual == null)
            {
                conexionManual = new SqlConnection(cadenaConexion);
            }
            return conexionManual;
        }
    }
}