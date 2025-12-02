using System;
using System.Data;
using Microsoft.Data.SqlClient;
using CapaDatos.Conexion; // Necesario para usar la clase Conexion
using CapaNegocio.Base; // Necesario para usar la clase CE_Estudiante

namespace CapaNegocio
{
    // Esta clase ahora contiene toda la lógica de negocio Y la lógica de acceso a datos (SQL).
    public class CN_Estudiante
    {
        // Instancia de la clase de Conexión
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
                SqlCommand comando = new SqlCommand(query, conexionDB.ObtenerConexion());
                SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);

                conexionDB.Abrir();
                dataAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                // Manejar o relanzar la excepción
                throw new Exception("Error al obtener la lista de estudiantes de la base de datos.", ex);
            }
            finally
            {
                conexionDB.Cerrar();
            }
            return dt;
        }

        public DataTable ObtenerCarreras()
        {
            DataTable dt = new DataTable();
            string query = "SELECT DISTINCT Carrera FROM Estudiantes ORDER BY Carrera";

            try
            {
                SqlCommand comando = new SqlCommand(query, conexionDB.ObtenerConexion());
                SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);

                conexionDB.Abrir();
                dataAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar la lista de carreras.", ex);
            }
            finally
            {
                conexionDB.Cerrar();
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
                SqlCommand comando = new SqlCommand(query, conexionDB.ObtenerConexion());
                comando.Parameters.AddWithValue("@Busqueda", "%" + textoBusqueda + "%");
                SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);

                conexionDB.Abrir();
                dataAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar la búsqueda de estudiantes.", ex);
            }
            finally
            {
                conexionDB.Cerrar();
            }
            return dt;
        }

        // ------------------------------------------------------------------------------------------------
        // MÉTODOS: ESCRITURA (CREATE/UPDATE) - Contiene la Lógica de Negocio y SQL
        // ------------------------------------------------------------------------------------------------

        public bool GuardarEstudiante(CE_Estudiante estudiante, out string mensaje)
        {
            mensaje = "";

            // 1. VALIDACIÓN DE CAMPOS OBLIGATORIOS (Lógica de Negocio)
            if (string.IsNullOrWhiteSpace(estudiante.Matricula) ||
                string.IsNullOrWhiteSpace(estudiante.Nombre) ||
                string.IsNullOrWhiteSpace(estudiante.Apellido) ||
                string.IsNullOrWhiteSpace(estudiante.Carrera) ||
                estudiante.Carrera == "-- SELECCIONE --")
            {
                mensaje = "Matrícula, Nombre, Apellido y Carrera son campos obligatorios. Seleccione una Carrera válida.";
                return false;
            }

            // 2. VALIDACIÓN DE DUPLICIDAD DE MATRÍCULA (Lógica de Negocio)
            if (ExisteMatricula(estudiante.Matricula, estudiante.IdEstudiante))
            {
                mensaje = $"La matrícula '{estudiante.Matricula}' ya está registrada en el sistema.";
                return false;
            }

            // 3. LÓGICA DE SQL (Data Access)
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
                SqlCommand comando = new SqlCommand(query, conexionDB.ObtenerConexion());

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

                conexionDB.Abrir();
                int filasAfectadas = comando.ExecuteNonQuery();
                conexionDB.Cerrar();

                if (filasAfectadas > 0)
                {
                    mensaje = (estudiante.IdEstudiante == 0) ? "Estudiante registrado con éxito." : "Estudiante actualizado con éxito.";
                }
                else
                {
                    mensaje = "La operación no se pudo completar. Ninguna fila fue afectada.";
                }
                return filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                conexionDB.Cerrar();
                mensaje = $"Error en la Capa de Negocio (SQL): {ex.Message}";
                return false;
            }
        }


        // ------------------------------------------------------------------------------------------------
        // MÉTODO: Validaciones internas (Acceso a Datos)
        // ------------------------------------------------------------------------------------------------

        public bool ExisteMatricula(string matricula, int idEstudianteExcluir)
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM Estudiantes WHERE Matricula = @Matricula AND IdEstudiante <> @IdEstudianteExcluir";

            try
            {
                SqlCommand comando = new SqlCommand(query, conexionDB.ObtenerConexion());
                comando.Parameters.AddWithValue("@Matricula", matricula);
                comando.Parameters.AddWithValue("@IdEstudianteExcluir", idEstudianteExcluir);

                conexionDB.Abrir();
                count = (int)comando.ExecuteScalar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al verificar matrícula: {ex.Message}");
                // Si hay un error de conexión/SQL al validar, retornamos true para evitar un posible duplicado.
                return true;
            }
            finally
            {
                conexionDB.Cerrar();
            }

            return count > 0;
        }
    }
}