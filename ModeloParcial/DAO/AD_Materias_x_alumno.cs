using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloParcial.DAO
{
    public class AD_Materias_x_alumno
    {
        public static bool InsertarMateriaAlumno(int idAlumno, int idMateria)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["cadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            SqlTransaction objTransaction = null;

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "INSERT INTO materias_x_alumnos VALUES (@idAlumno, @idMateria)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idAlumno", idAlumno);
                cmd.Parameters.AddWithValue("@idMateria", idMateria);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                objTransaction = cn.BeginTransaction("Alta AlumnoXMateria");
                cmd.Transaction = objTransaction;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();

                objTransaction.Commit();
                return true;      
            }
            catch (Exception)
            {
                objTransaction.Rollback();
                return false;
            }
            finally
            {
                cn.Close();
            }

        }
    }
}
