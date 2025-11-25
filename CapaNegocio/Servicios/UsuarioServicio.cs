using CapaDatos.Conexion;
using CapaNegocio.Base;
using System;
using System.Data;
using Microsoft.Data.SqlClient; // Asegúrate de estar usando solo una librería para SQL

namespace CapaNegocio.Servicios
{
    public class UsuarioDAL
    {
        // SOLUCIÓN: Instanciamos la conexión aquí dentro directamente.
        // No pedimos nada en un constructor.
        private Conexion conexion = new Conexion();

        public Usuario Login(string usuario, string clave)
        {
            try
            {
                conexion.Abrir();
                // ... (resto del código igual que antes) ...

                string query = "SELECT IdUsuario, NombreUsuario, NombreCompleto, Rol FROM Usuarios WHERE NombreUsuario = @User AND Clave = @Pass";

                using (SqlCommand cmd = new SqlCommand(query, conexion.ObtenerConexion()))
                {
                    cmd.Parameters.AddWithValue("@User", usuario);
                    cmd.Parameters.AddWithValue("@Pass", clave);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                NombreUsuario = reader["NombreUsuario"].ToString(),
                                NombreCompleto = reader["NombreCompleto"].ToString(),
                                Rol = reader["Rol"].ToString()
                            };
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en BD: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
        }
    }
}
