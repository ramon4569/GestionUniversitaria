using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos.Conexion;
using Microsoft.Data.SqlClient;
using CapaNegocio.Base;

// *** NAMESPACE CORRECTO: CapaNegocio ***
namespace CapaNegocio
{
    public class CN_Calificaciones
    {
        // Solo instanciamos la clase de conexión (no la conexión SQL)
        private readonly Conexion conexionDB = new Conexion();

        // ----------------------------------------------------------------------------------
        // LÓGICA DE NEGOCIO: Puntuación de Índice 
        // ----------------------------------------------------------------------------------

        private decimal CalcularPuntuacionIndice(double calificacion)
        {
            if (calificacion >= 90.00) return 4.00m; // A
            if (calificacion >= 80.00) return 3.00m; // B
            if (calificacion >= 70.00) return 2.00m; // C
            return 0.00m; // F
        }

        public string CalcularIndiceAcademico(List<Inscripcion> record)
        {
            if (record == null || !record.Any(i => i.CalificacionFinal.HasValue))
            {
                return "0.00";
            }

            var materiasCalificadas = record.Where(i => i.CalificacionFinal.HasValue && i.Materia != null).ToList();

            if (!materiasCalificadas.Any())
            {
                return "0.00";
            }

            decimal puntosDeHonor = 0;
            int creditosTomados = 0;

            foreach (var inscripcion in materiasCalificadas)
            {
                if (inscripcion.Materia != null && inscripcion.CalificacionFinal.HasValue)
                {
                    decimal puntuacion = CalcularPuntuacionIndice(inscripcion.CalificacionFinal.Value);
                    puntosDeHonor += puntuacion * inscripcion.Materia.Creditos;
                    creditosTomados += inscripcion.Materia.Creditos;
                }
            }

            if (creditosTomados == 0) return "0.00";

            decimal indice = puntosDeHonor / creditosTomados;
            return indice.ToString("N2"); // Formato con dos decimales
        }

        // ----------------------------------------------------------------------------------
        // ACCESO A DATOS: Lectura (Obtener Estudiantes para ComboBox)
        // ----------------------------------------------------------------------------------

        /// <summary>
        /// Obtiene la lista de todos los estudiantes para el ComboBox.
        /// </summary>
        public DataTable ObtenerEstudiantesParaCmb()
        {
            DataTable dt = new DataTable();
            string query = "SELECT IdEstudiante, Matricula, Nombre + ' ' + Apellido AS NombreCompleto FROM Estudiantes ORDER BY NombreCompleto";

            try
            {
                // Usa el patrón using directo para obtener la conexión
                using (SqlConnection conn = conexionDB.ObtenerConexion())
                {
                    using (SqlCommand comando = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(comando);
                        // DataAdapter.Fill() gestiona la apertura/cierre de 'conn'
                        dataAdapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de estudiantes. Revise la cadena de conexión en CapaDatos.", ex);
            }
            return dt;
        }

        // ----------------------------------------------------------------------------------
        // ACCESO A DATOS: Lectura (Obtener Récord Académico)
        // ----------------------------------------------------------------------------------

        /// <summary>
        /// Obtiene el récord completo de un estudiante (materias inscritas, calificadas o no).
        /// </summary>
        public List<Inscripcion> ObtenerRecordAcademico(int idEstudiante)
        {
            List<Inscripcion> record = new List<Inscripcion>();

            string query = @"
                SELECT 
                    M.IdMatricula, M.CalificacionFinal, 
                    S.CodigoSeccion,
                    T.IdMateria, T.Codigo, T.Nombre, T.Creditos
                FROM Matriculas M
                JOIN Secciones S ON M.IdSeccion = S.IdSeccion
                JOIN Materias T ON S.IdMateria = T.IdMateria
                WHERE M.IdEstudiante = @IdEstudiante
                ORDER BY S.CodigoSeccion DESC";

            try
            {
                using (SqlConnection conn = conexionDB.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEstudiante", idEstudiante);
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                record.Add(new Inscripcion
                                {
                                    IdMatricula = Convert.ToInt32(reader["IdMatricula"]),
                                    CalificacionFinal = reader["CalificacionFinal"] == DBNull.Value ? (double?)null : Convert.ToDouble(reader["CalificacionFinal"]),
                                    CodigoSeccion = reader["CodigoSeccion"].ToString(),
                                    Materia = new Materia
                                    {
                                        IdMateria = Convert.ToInt32(reader["IdMateria"]),
                                        Codigo = reader["Codigo"].ToString(),
                                        Nombre = reader["Nombre"].ToString(),
                                        Creditos = Convert.ToInt32(reader["Creditos"])
                                    }
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el récord académico del estudiante. Detalles: " + ex.Message, ex);
            }
            return record;
        }

        // ----------------------------------------------------------------------------------
        // ACCESO A DATOS: Escritura (Guardar Calificación)
        // ----------------------------------------------------------------------------------

        /// <summary>
        /// Actualiza la calificación final de una matrícula específica.
        /// </summary>
        public bool GuardarCalificacion(int idMatricula, double calificacion, out string mensaje)
        {
            mensaje = string.Empty;

            if (calificacion < 0 || calificacion > 100)
            {
                mensaje = "La calificación debe estar entre 0 y 100.";
                return false;
            }

            string query = "UPDATE Matriculas SET CalificacionFinal = @Calificacion WHERE IdMatricula = @IdMatricula";

            try
            {
                using (SqlConnection conn = conexionDB.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Calificacion", calificacion);
                        cmd.Parameters.AddWithValue("@IdMatricula", idMatricula);

                        conn.Open();
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            mensaje = "Calificación registrada con éxito.";
                            return true;
                        }
                        else
                        {
                            mensaje = "No se pudo encontrar la matrícula para actualizar la nota.";
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error de base de datos al guardar la calificación: {ex.Message}";
                return false;
            }
        }
    }
}