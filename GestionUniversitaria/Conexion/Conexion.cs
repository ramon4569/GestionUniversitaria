using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Conexion
{
    public class Conexion
    {
        private SqlConnection conexion;

        public Conexion()
        {
            
            string cadenaConexion = "Server=. ;Database=GestionAcademicaUCE;Integrated Security=True;TrustServerCertificate=True;";
            conexion = new SqlConnection(cadenaConexion);
        }

        // Método para abrir la conexión
        public void Abrir()
        {
            if (conexion.State == ConnectionState.Closed)
                conexion.Open();
        }

        // Método para cerrar la conexión
        public void Cerrar()
        {
            if (conexion.State == ConnectionState.Open)
                conexion.Close();
        }

        // Obtener la conexión (para usarla en los comandos SQL)
        public SqlConnection ObtenerConexion()
        {
            return conexion;
        }
    }
}
