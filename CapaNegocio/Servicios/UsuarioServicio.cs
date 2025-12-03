using CapaDatos.Conexion;
using CapaNegocio.Base;
using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CapaNegocio.Servicios
{
    /// <summary>
    /// Capa de Acceso a Datos para Usuarios (Autenticación)
    /// </summary>
    public class UsuarioDAL
    {
        private readonly Conexion conexion = new Conexion();

        /// <summary>
        /// Valida las credenciales del usuario en la base de datos
        /// </summary>
        public Usuario Login(string usuario, string clave)
        {
            string query = "SELECT IdUsuario, NombreUsuario, NombreCompleto, Rol FROM Usuarios WHERE NombreUsuario = @User AND Clave = @Pass";

            try
            {
                using (SqlConnection conn = conexion.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@User", usuario);
                        cmd.Parameters.AddWithValue("@Pass", clave);

                        conn.Open();

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
                }
                // Si no se encontró el usuario
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar credenciales en la base de datos: " + ex.Message, ex);
            }
        }
    }

    /// <summary>
    /// Capa de Lógica de Negocio para Autenticación
    /// </summary>
    public class UsuarioService
    {
        private readonly UsuarioDAL _usuarioDAL = new UsuarioDAL();

        /// <summary>
        /// Valida el acceso del usuario al sistema
        /// </summary>
        public Usuario ValidarAcceso(string usuario, string clave)
        {
            // 1. Validación de campos vacíos
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(clave))
            {
                throw new Exception("Usuario y contraseña son requeridos.");
            }

            // 2. Llamar a la capa de datos
            Usuario usuarioEncontrado = _usuarioDAL.Login(usuario, clave);

            // 3. Validar si existe
            if (usuarioEncontrado == null)
            {
                throw new Exception("Usuario o contraseña incorrectos.");
            }

            // 4. Retornar usuario validado
            return usuarioEncontrado;
        }
    }
}