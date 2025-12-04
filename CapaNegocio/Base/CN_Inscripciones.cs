using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using CapaDatos.Conexion;
using CapaNegocio.Base;

namespace CapaNegocio
{
    public class CN_Inscripciones
    {
        private readonly Conexion conexionDB = new Conexion();

        // ----------------------------------------------------------------------------------
        // OBTENER ESTUDIANTES PARA COMBOBOX
        // ----------------------------------------------------------------------------------
        public DataTable ObtenerEstudiantesParaCmb()
        {
            DataTable dt = new DataTable();
            string query = "SELECT IdEstudiante, Matricula, Nombre + ' ' + Apellido AS NombreCompleto, Carrera FROM Estudiantes ORDER BY NombreCompleto";

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
                throw new Exception("Error al obtener la lista de estudiantes: " + ex.Message, ex);
            }
            return dt;
        }

        // ----------------------------------------------------------------------------------
        // OBTENER MATERIAS DISPONIBLES (sin inscribir) PARA UN ESTUDIANTE
        // ----------------------------------------------------------------------------------
        public DataTable ObtenerMateriasDisponibles(int idEstudiante)
        {
            DataTable dt = new DataTable();

            // Obtener materias que el estudiante NO ha inscrito todavía
            string query = @"
                SELECT 
                    s.IdSeccion,
                    m.Codigo + ' - ' + m.Nombre + ' | Sección: ' + s.CodigoSeccion AS Display,
                    m.Creditos
                FROM Secciones s
                INNER JOIN Materias m ON s.IdMateria = m.IdMateria
                WHERE s.IdSeccion NOT IN (
                    SELECT IdSeccion 
                    FROM Matriculas 
                    WHERE IdEstudiante = @IdEstudiante
                )
                ORDER BY m.Codigo";

            try
            {
                using (SqlConnection conn = conexionDB.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEstudiante", idEstudiante);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener materias disponibles: " + ex.Message, ex);
            }

            return dt;
        }

        // ----------------------------------------------------------------------------------
        // OBTENER MATERIAS YA INSCRITAS POR EL ESTUDIANTE
        // ----------------------------------------------------------------------------------
        public DataTable ObtenerMateriasInscritas(int idEstudiante)
        {
            DataTable dt = new DataTable();

            string query = @"
                SELECT 
                    m.IdMatricula,
                    mat.Codigo,
                    mat.Nombre AS Materia,
                    s.CodigoSeccion AS Seccion,
                    mat.Creditos,
                    ISNULL(m.CalificacionFinal, 0) AS Nota,
                    CASE 
                        WHEN m.CalificacionFinal IS NULL THEN 'Cursando'
                        WHEN m.CalificacionFinal >= 70 THEN 'Aprobado'
                        ELSE 'Reprobado'
                    END AS Estado
                FROM Matriculas m
                INNER JOIN Secciones s ON m.IdSeccion = s.IdSeccion
                INNER JOIN Materias mat ON s.IdMateria = mat.IdMateria
                WHERE m.IdEstudiante = @IdEstudiante
                ORDER BY Estado DESC, mat.Codigo";

            try
            {
                using (SqlConnection conn = conexionDB.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEstudiante", idEstudiante);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener materias inscritas: " + ex.Message, ex);
            }

            return dt;
        }

        // ----------------------------------------------------------------------------------
        // INSCRIBIR ESTUDIANTE EN UNA SECCIÓN
        // ----------------------------------------------------------------------------------
        public bool InscribirMateria(int idEstudiante, int idSeccion, DateTime fechaInscripcion, out string mensaje)
        {
            mensaje = string.Empty;

            // 1. Validar que el estudiante no esté ya inscrito en esa sección
            if (YaEstaInscrito(idEstudiante, idSeccion))
            {
                mensaje = "El estudiante ya está inscrito en esta sección.";
                return false;
            }

            // 2. Realizar la inscripción
            string queryInsert = @"
                INSERT INTO Matriculas (IdEstudiante, IdSeccion, FechaInscripcion)
                VALUES (@IdEstudiante, @IdSeccion, @FechaInscripcion)";

            try
            {
                using (SqlConnection conn = conexionDB.ObtenerConexion())
                {
                    conn.Open();

                    // Insertar matrícula
                    using (SqlCommand cmdInsert = new SqlCommand(queryInsert, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@IdEstudiante", idEstudiante);
                        cmdInsert.Parameters.AddWithValue("@IdSeccion", idSeccion);
                        cmdInsert.Parameters.AddWithValue("@FechaInscripcion", fechaInscripcion);
                        cmdInsert.ExecuteNonQuery();
                    }

                    mensaje = "Materia inscrita con éxito.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al inscribir la materia: " + ex.Message;
                return false;
            }
        }

        // ----------------------------------------------------------------------------------
        // DAR DE BAJA (ELIMINAR INSCRIPCIÓN)
        // ----------------------------------------------------------------------------------
        public bool DarDeBajaMateria(int idMatricula, out string mensaje)
        {
            mensaje = string.Empty;

            string queryDelete = "DELETE FROM Matriculas WHERE IdMatricula = @IdMatricula";

            try
            {
                using (SqlConnection conn = conexionDB.ObtenerConexion())
                {
                    conn.Open();

                    // Eliminar matrícula
                    using (SqlCommand cmdDelete = new SqlCommand(queryDelete, conn))
                    {
                        cmdDelete.Parameters.AddWithValue("@IdMatricula", idMatricula);
                        int filasAfectadas = cmdDelete.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            mensaje = "Materia dada de baja con éxito.";
                            return true;
                        }
                        else
                        {
                            mensaje = "No se encontró la matrícula.";
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al dar de baja la materia: " + ex.Message;
                return false;
            }
        }

        // ----------------------------------------------------------------------------------
        // MÉTODOS AUXILIARES PRIVADOS
        // ----------------------------------------------------------------------------------

        private bool YaEstaInscrito(int idEstudiante, int idSeccion)
        {
            string query = "SELECT COUNT(*) FROM Matriculas WHERE IdEstudiante = @IdEstudiante AND IdSeccion = @IdSeccion";

            try
            {
                using (SqlConnection conn = conexionDB.ObtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEstudiante", idEstudiante);
                        cmd.Parameters.AddWithValue("@IdSeccion", idSeccion);
                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception)
            {
                return true; // Por seguridad, asumimos que ya está inscrito
            }
        }
    }
}