
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using CapaDatos.Conexion;
using CapaNegocio.Base;
using CapaNegocio.Excepciones;

namespace CapaNegocio
{
    public class CN_Estudiante
    {
        private readonly Conexion conexionDB = new Conexion();

        // ------------------------------------------------------------------------------------------------
        // MÉTODOS: LECTURA
        // ------------------------------------------------------------------------------------------------

        public DataTable ListarEstudiantes()
        {
            DataTable dt = new DataTable();
            string query = "SELECT IdEstudiante, Matricula, Nombre, Apellido, Carrera, Email, Telefono FROM Estudiantes ORDER BY Apellido, Nombre";

            try
            {
                using (SqlConnection conn = conexionDB.ObtenerConexion())
                {
                    using (SqlCommand comando = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
                        dataAdapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de estudiantes de la base de datos.", ex);
            }
            return dt;
        }

        public DataTable ObtenerCarreras()
        {
            DataTable dt = new DataTable();
            string query = "SELECT DISTINCT Carrera FROM Estudiantes ORDER BY Carrera";

            try
            {
                using (SqlConnection conn = conexionDB.ObtenerConexion())
                {
                    using (SqlCommand comando = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
                        dataAdapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar la lista de carreras.", ex);
            }
            return dt;
        }

        public DataTable BuscarEstudiantes(string textoBusqueda)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT IdEstudiante, Matricula, Nombre, Apellido, Carrera, Email, Telefono 
                FROM Estudiantes 
                WHERE Matricula LIKE @Busqueda 
                   OR Nombre LIKE @Busqueda 
                   OR Apellido LIKE @Busqueda 
                ORDER BY Apellido, Nombre";

            try
            {
                using (SqlConnection conn = conexionDB.ObtenerConexion())
                {
                    using (SqlCommand comando = new SqlCommand(query, conn))
                    {
                        comando.Parameters.AddWithValue("@Busqueda", "%" + textoBusqueda + "%");
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
                        dataAdapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar la búsqueda de estudiantes.", ex);
            }
            return dt;
        }

        // ------------------------------------------------------------------------------------------------
        // MÉTODOS: ESCRITURA (CREATE/UPDATE)
        // ------------------------------------------------------------------------------------------------

        public bool GuardarEstudiante(CE_Estudiante estudiante, out string mensaje)
        {
            mensaje = "";

            // 1. VALIDACIÓN DE CAMPOS OBLIGATORIOS
            if (string.IsNullOrWhiteSpace(estudiante.Matricula) ||
                string.IsNullOrWhiteSpace(estudiante.Nombre) ||
                string.IsNullOrWhiteSpace(estudiante.Apellido) ||
                string.IsNullOrWhiteSpace(estudiante.Carrera) ||
                estudiante.Carrera == "-- SELECCIONE --")
            {
                mensaje = "Matrícula, Nombre, Apellido y Carrera son campos obligatorios. Seleccione una Carrera válida.";
                return false;
            }

            // 2. VALIDACIÓN DE DUPLICIDAD DE MATRÍCULA
            if (ExisteMatricula(estudiante.Matricula, estudiante.IdEstudiante))
            {
                throw new MatriculaDuplicadaException($"La matrícula '{estudiante.Matricula}' ya está registrada en el sistema.");
            }

            // 3. LÓGICA DE SQL
            string query = "";
            bool esNuevo = estudiante.IdEstudiante == 0;

            if (esNuevo)
            {
                query = @"INSERT INTO Estudiantes (Matricula, Nombre, Apellido, Carrera, Email, Telefono, SemestreActual) 
                          VALUES (@Matricula, @Nombre, @Apellido, @Carrera, @Email, @Telefono, @SemestreActual)";
            }
            else
            {
                query = @"UPDATE Estudiantes 
                          SET Matricula = @Matricula, Nombre = @Nombre, Apellido = @Apellido, 
                              Carrera = @Carrera, Email = @Email, Telefono = @Telefono
                          WHERE IdEstudiante = @IdEstudiante";
            }

            try
            {
                using (SqlConnection conn = conexionDB.ObtenerConexion())
                {
                    using (SqlCommand comando = new SqlCommand(query, conn))
                    {
                        // Parámetros comunes
                        comando.Parameters.AddWithValue("@Matricula", estudiante.Matricula);
                        comando.Parameters.AddWithValue("@Nombre", estudiante.Nombre);
                        comando.Parameters.AddWithValue("@Apellido", estudiante.Apellido);
                        comando.Parameters.AddWithValue("@Carrera", estudiante.Carrera);
                        comando.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(estudiante.Email) ? (object)DBNull.Value : estudiante.Email);
                        comando.Parameters.AddWithValue("@Telefono", string.IsNullOrWhiteSpace(estudiante.Telefono) ? (object)DBNull.Value : estudiante.Telefono);

                        // Parámetros específicos
                        if (esNuevo)
                        {
                            comando.Parameters.AddWithValue("@SemestreActual", estudiante.SemestreActual);
                        }
                        else
                        {
                            comando.Parameters.AddWithValue("@IdEstudiante", estudiante.IdEstudiante);
                        }

                        conn.Open();
                        int filasAfectadas = comando.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            mensaje = esNuevo ? "Estudiante registrado con éxito." : "Estudiante actualizado con éxito.";
                        }
                        else
                        {
                            mensaje = "La operación no se pudo completar. Ninguna fila fue afectada.";
                        }
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error en la Capa de Negocio (SQL): {ex.Message}";
                return false;
            }
        }

        // ------------------------------------------------------------------------------------------------
        // MÉTODO: Validaciones internas
        // ------------------------------------------------------------------------------------------------

        public bool ExisteMatricula(string matricula, int idEstudianteExcluir)
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM Estudiantes WHERE Matricula = @Matricula AND IdEstudiante <> @IdEstudianteExcluir";

            try
            {
                using (SqlConnection conn = conexionDB.ObtenerConexion())
                {
                    using (SqlCommand comando = new SqlCommand(query, conn))
                    {
                        comando.Parameters.AddWithValue("@Matricula", matricula);
                        comando.Parameters.AddWithValue("@IdEstudianteExcluir", idEstudianteExcluir);

                        conn.Open();
                        count = (int)comando.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al verificar matrícula: {ex.Message}");
                return true; // Por seguridad, asumimos que existe
            }

            return count > 0;
        }
    }
}