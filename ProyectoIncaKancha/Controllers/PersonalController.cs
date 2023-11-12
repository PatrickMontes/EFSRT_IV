using ProyectoIncaKancha.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoIncaKancha.Permisos;

namespace ProyectoIncaKancha.Controllers
{
    [PermisosRol(Models.Rol.Administrador)]
    public class PersonalController : Controller
    {
        public Usuarios buscar(string codigo)
        {
            if (int.TryParse(codigo, out int codigoInt))
            {
                return listadoDeUsuarios().FirstOrDefault(m => m.id == codigoInt);
            }
            else
            {
                return null;
            }
        }

        /**LISTAR USUARIOSS**/
        public IEnumerable<Usuarios> listadoDeUsuarios()
        {
            List<Usuarios> usuarios = new List<Usuarios>();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["sql"].ConnectionString))
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand("PListarUsuarios", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    usuarios.Add(new Usuarios()
                    {
                        id = dr.GetInt32(dr.GetOrdinal("id")),
                        Nombres = dr.GetString(dr.GetOrdinal("Nombres")),
                        Apellidos = dr.GetString(dr.GetOrdinal("Apellidos")),
                        Correo = dr.GetString(dr.GetOrdinal("Correo")),
                        Clave = dr.GetString(dr.GetOrdinal("clave")),
                        Dni = dr.GetString(dr.GetOrdinal("Dni")),
                        IdRol = (Rol)dr.GetInt32(dr.GetOrdinal("IdRol"))
                    });
                }

                dr.Close();
            }

            return usuarios;
        }

        public ActionResult ListaUsuarios()
        {
            return View(listadoDeUsuarios());
        }
        /**INSERTAR USUARIOS**/
        public string InsertarUsuario(Usuarios reg)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["sql"].ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertarUsuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombres", reg.Nombres);
                    cmd.Parameters.AddWithValue("@Apellidos", reg.Apellidos);
                    cmd.Parameters.AddWithValue("@Correo", reg.Correo);
                    cmd.Parameters.AddWithValue("@Clave", reg.Clave);
                    cmd.Parameters.AddWithValue("@Dni", reg.Dni);
                    cmd.Parameters.AddWithValue("@IdRol", reg.IdRol);

                    cn.Open();

                    int i = cmd.ExecuteNonQuery();

                    mensaje = $"Se ha insertado {i} usuario";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {
                    cn.Close();
                }
            }

            return mensaje;
        }

        public ActionResult Create()
        {
            return View(new Usuarios());
        }

        [HttpPost]
        public ActionResult Create(Usuarios reg)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(Enum.GetValues(typeof(Rol)));
                return View(reg);
            }

            ViewBag.Mensaje = InsertarUsuario(reg);
            ViewBag.Roles = new SelectList(Enum.GetValues(typeof(Rol)));
            return View(reg);
        }

        /***EDITAR USUARIOS**/
        public string actualizar(Usuarios reg)
        {
            // Ejecuta el procedimiento almacenado y retorna el mensaje del proceso
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["sql"].ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ActualizarUsuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", reg.id);
                    cmd.Parameters.AddWithValue("@Nombres", reg.Nombres);
                    cmd.Parameters.AddWithValue("@Apellidos", reg.Apellidos);
                    cmd.Parameters.AddWithValue("@Correo", reg.Correo);
                    cmd.Parameters.AddWithValue("@Clave", reg.Clave);
                    cmd.Parameters.AddWithValue("@Dni", reg.Dni);
                    cmd.Parameters.AddWithValue("@IdRol", reg.IdRol);

                    cn.Open();

                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado {i} registro";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {
                    cn.Close();
                }
            }
            return mensaje;
        }

        public ActionResult Edit(int? id)
        {

            if (!id.HasValue)
            {
                return RedirectToAction("Edit");
            }

            Usuarios reg = buscar(id.ToString());

            if (reg == null)
            {

                return RedirectToAction("Edit");
            }

            ViewBag.Roles = new SelectList(Enum.GetValues(typeof(Rol)));
            return View(reg);
        }
        [HttpPost]
        public ActionResult Edit(Usuarios reg)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(Enum.GetValues(typeof(Rol)));
                return View(reg);
            }

            ViewBag.mensaje = actualizar(reg);
            ViewBag.Roles = new SelectList(Enum.GetValues(typeof(Rol)));

            return View(reg);
        }

        //METODOS PARA ELIMINAR EL USUARIO
        public string EliminarUsuario(int id)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["sql"].ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarUsuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    cn.Open();

                    int i = cmd.ExecuteNonQuery();

                    mensaje = $"Se ha eliminado {i} usuario";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {
                    cn.Close();
                }
            }

            return mensaje;
        }

        [HttpPost]
        public ActionResult EliminarUsuarios(int id)
        {
            ViewBag.Mensaje = EliminarUsuario(id);
            return RedirectToAction("ListaUsuarios");
        }
    }
}
