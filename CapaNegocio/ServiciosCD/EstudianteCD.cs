using CapaDatos.Conexion;
using CapaNegocio.Base;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.ServiciosCD
{
    public class EstudianteDAL
    {
        // 1. MÉTODO COMPLEJO (Para reportes y LINQ)
        public List<Estudiante> ObtenerTodosCompletos()
        {
            var diccionarioEstudiantes = new Dictionary<string, Estudiante>();

            // Nota: Asegúrate que tu clase Conexion devuelva System.Data.SqlClient.SqlConnection
            using (SqlConnection conn = new Conexion().ObtenerConexion())
            {
                conn.Open();

                string query = @"
                    SELECT 
                         e.Matricula, e.Nombre, e.Apellido, e.Carrera, e.Email, e.Telefono,
                         i.Calificacion,
                         m.Codigo, m.Nombre AS NomMateria, m.Creditos
                         FROM Estudiantes e
                         LEFT JOIN Inscripciones i ON e.IdEstudiante = i.IdEstudiante  -- CAMBIO AQUÍ (IDs)
                         LEFT JOIN Materias m ON i.IdMateria = m.IdMateria             -- CAMBIO AQUÍ (IDs)
                         ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string matricula = reader["Matricula"].ToString();

                            if (!diccionarioEstudiantes.ContainsKey(matricula))
                            {
                                var nuevoEstudiante = new Estudiante
                                {
                                    Matricula = matricula,
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellido = reader["Apellido"].ToString(),
                                    Carrera = reader["Carrera"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Telefono = reader["Telefono"].ToString(),
                                    MateriasCursadas = new List<Inscripcion>()
                                };
                                diccionarioEstudiantes.Add(matricula, nuevoEstudiante);
                            }

                            Estudiante estudianteActual = diccionarioEstudiantes[matricula];

                            if (reader["Codigo"] != DBNull.Value)
                            {
                                var inscripcion = new Inscripcion
                                {
                                    Calificacion = reader["Calificacion"] == DBNull.Value ? null : (double?)Convert.ToDouble(reader["Calificacion"]),
                                    Materia = new Materia
                                    {
                                        Codigo = reader["Codigo"].ToString(),
                                        Nombre = reader["NomMateria"].ToString(),
                                        Creditos = Convert.ToInt32(reader["Creditos"])
                                    }
                                };
                                estudianteActual.MateriasCursadas.Add(inscripcion);
                            }
                        }
                    }
                }
            }
            return new List<Estudiante>(diccionarioEstudiantes.Values);
        }

        // 2. EXISTE MATRÍCULA (Validación)
        public bool ExisteMatricula(string matricula)
        {
            using (SqlConnection conn = new Conexion().ObtenerConexion())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Estudiantes WHERE Matricula = @Matricula";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Matricula", matricula);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        // 3. INSERTAR (Guardar)
        public void Insertar(Estudiante est)
        {
            using (SqlConnection conn = new Conexion().ObtenerConexion())
            {
                conn.Open();
                string query = @"INSERT INTO Estudiantes (Matricula, Nombre, Apellido, Carrera, Email, Telefono) 
                                 VALUES (@Matricula, @Nombre, @Apellido, @Carrera, @Email, @Telefono)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Matricula", est.Matricula);
                    cmd.Parameters.AddWithValue("@Nombre", est.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", est.Apellido);
                    cmd.Parameters.AddWithValue("@Carrera", est.Carrera);
                    cmd.Parameters.AddWithValue("@Email", est.Email);
                    cmd.Parameters.AddWithValue("@Telefono", est.Telefono);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 4. OBTENER TODOS (Simple)
        public List<Estudiante> ObtenerTodos()
        {
            List<Estudiante> lista = new List<Estudiante>();

            using (SqlConnection conn = new Conexion().ObtenerConexion())
            {
                conn.Open();
                string query = "SELECT * FROM Estudiantes";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Estudiante est = new Estudiante
                            {
                                Matricula = reader["Matricula"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Carrera = reader["Carrera"].ToString(),
                                Email = reader["Email"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                MateriasCursadas = new List<Inscripcion>()
                            };
                            lista.Add(est);
                        }
                    }
                }
            }
            return lista;
        }

        // 5. BUSCAR POR MATRICULA (El que te faltaba y daba error)
        public Estudiante BuscarPorMatricula(string matricula)
        {
            using (SqlConnection conn = new Conexion().ObtenerConexion())
            {
                conn.Open();
                string query = "SELECT * FROM Estudiantes WHERE Matricula = @Matricula";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Matricula", matricula);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Estudiante
                            {
                                Matricula = reader["Matricula"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                Apellido = reader["Apellido"].ToString(),
                                Carrera = reader["Carrera"].ToString(),
                                Email = reader["Email"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                MateriasCursadas = new List<Inscripcion>()
                            };
                        }
                    }
                }
            }
            return null; // Retorna null si no lo encuentra
        }

    }
}
