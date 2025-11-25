using CapaDatos.Conexion;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.ServiciosCD
{
    public class InscripcionDAL
    {
        public void Inscribir(string matricula, string codigoMateria)
        {
            using (SqlConnection conn = new Conexion().ObtenerConexion())
            {
                conn.Open();

                string query = @"
                    INSERT INTO Inscripciones (IdEstudiante, IdMateria, FechaInscripcion) 
                    VALUES (
                        (SELECT IdEstudiante FROM Estudiantes WHERE Matricula = @Mat), 
                        (SELECT IdMateria FROM Materias WHERE Codigo = @Cod), 
                        GETDATE()
                    )";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Mat", matricula);
                    cmd.Parameters.AddWithValue("@Cod", codigoMateria);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 2. Validar si ya existe (JOIN)
        public bool ExisteInscripcion(string matricula, string codigoMateria)
        {
            using (SqlConnection conn = new Conexion().ObtenerConexion())
            {
                conn.Open();

                // Hacemos JOIN para conectar las tablas por sus IDs y verificar por los códigos de texto
                string query = @"
                    SELECT COUNT(*) 
                    FROM Inscripciones i
                    JOIN Estudiantes e ON i.IdEstudiante = e.IdEstudiante
                    JOIN Materias m ON i.IdMateria = m.IdMateria
                    WHERE e.Matricula = @Mat AND m.Codigo = @Cod";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Mat", matricula);
                    cmd.Parameters.AddWithValue("@Cod", codigoMateria);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        // 3. Registrar Calificación (UPDATE CON JOIN)
        public void ActualizarNota(string matricula, string codigoMateria, double nota)
        {
            using (SqlConnection conn = new Conexion().ObtenerConexion())
            {
                conn.Open();

                // Sintaxis especial de SQL Server para hacer UPDATE con JOINS
                string query = @"
                    UPDATE i
                    SET i.Calificacion = @Nota
                    FROM Inscripciones i
                    JOIN Estudiantes e ON i.IdEstudiante = e.IdEstudiante
                    JOIN Materias m ON i.IdMateria = m.IdMateria
                    WHERE e.Matricula = @Mat AND m.Codigo = @Cod";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nota", nota);
                    cmd.Parameters.AddWithValue("@Mat", matricula);
                    cmd.Parameters.AddWithValue("@Cod", codigoMateria);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}

