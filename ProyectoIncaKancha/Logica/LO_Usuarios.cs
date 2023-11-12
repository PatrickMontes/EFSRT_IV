using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ProyectoIncaKancha.Models;
using System.Data.SqlClient;
using System.Data;
using System.Net;

namespace ProyectoIncaKancha.Logica
{
    public class LO_Usuarios
    {

        public Usuarios EncontrarUsuario(string correo, string clave)
        {

            Usuarios objeto = new Usuarios();


            using (SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-OD7SNMV\\SQLEXPRESS;Initial Catalog=IncaKancha;Integrated Security=True"))
            {

                string query = "select Nombres,Apellidos,Correo,clave,Dni,IdRol from USUARIOS where Correo = @pcorreo and Clave = @pclave";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@pcorreo", correo);
                cmd.Parameters.AddWithValue("@pclave", clave);
                cmd.CommandType = CommandType.Text;


                conexion.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {

                        objeto = new Usuarios()
                        {
                            Nombres = dr["Nombres"].ToString(),
                            Apellidos = dr["Apellidos"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Clave = dr["clave"].ToString(),
                            Dni = dr["Dni"].ToString(),
                            IdRol = (Rol)dr["IdRol"],

                        };
                    }

                }
            }
            return objeto;

        }




    }
}
