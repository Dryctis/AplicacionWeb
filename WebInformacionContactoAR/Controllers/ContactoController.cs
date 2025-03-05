using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using WebInformacionContactoAR.Models;
using System.Data.SqlClient;
using System.Data;

namespace WebInformacionContactoAR.Controllers
{
    public class ContactoController : Controller
    {
        private static string conexion = ConfigurationManager.ConnectionStrings["conexionBD"].ToString();

        private static List<Contacto> olista = new List<Contacto>();


        // GET: Contacto
        public ActionResult Inicio()
        {
            olista = new List<Contacto>();

            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM CONTACTO", oconexion);
                cmd.CommandType = CommandType.Text;
                oconexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Contacto nuevoContacto = new Contacto();

                      
                        nuevoContacto.Idcontacto = dr["IdContacto"] != DBNull.Value ? Convert.ToInt32(dr["IdContacto"]) : 0;
                        nuevoContacto.Nombre = dr["Nombre"] != DBNull.Value ? dr["Nombre"].ToString() : string.Empty;
                        nuevoContacto.Apellidos = dr["Apellidos"] != DBNull.Value ? dr["Apellidos"].ToString() : string.Empty;
                        nuevoContacto.Telefono = dr["Telefono"] != DBNull.Value ? dr["Telefono"].ToString() : string.Empty;
                        nuevoContacto.Correo = dr["Correo"] != DBNull.Value ? dr["Correo"].ToString() : string.Empty;
                        nuevoContacto.Salario = dr["Salario"] != DBNull.Value ? Convert.ToInt32(dr["Salario"]) : 0;
                        nuevoContacto.FechaNacimiento = dr["FechaNacimiento"] != DBNull.Value ? dr["FechaNacimiento"].ToString() : string.Empty;

                        olista.Add(nuevoContacto);
                    }
                }
            }

            return View(olista);
        }

        [HttpGet]
        public ActionResult Registrar()
        {
            return View();
        }



        [HttpGet]
        public ActionResult Editar(int? idcontacto)
        {
            if (idcontacto == null)
                return RedirectToAction("Inicio", "Contacto");

            Contacto ocontacto = olista.Where(c => c.Idcontacto == idcontacto).FirstOrDefault();    

            return View(ocontacto);
        }



        [HttpPost]
        public ActionResult Registrar(Contacto oContacto)
        {
            
            {
                using (SqlConnection oconexion = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_Registrar", oconexion);


                    cmd.Parameters.AddWithValue("Nombre", oContacto.Nombre);
                    cmd.Parameters.AddWithValue("Apellidos", oContacto.Apellidos);
                    cmd.Parameters.AddWithValue("Telefono", oContacto.Telefono);
                    cmd.Parameters.AddWithValue("Correo", oContacto.Correo);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                   
                }
                
                return RedirectToAction("Inicio", "Contacto");
            }
          
        }


        [HttpPost]
        public ActionResult Editar(Contacto oContacto)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Editar", oconexion);
                cmd.Parameters.AddWithValue("Idcontacto", oContacto.Idcontacto);
                cmd.Parameters.AddWithValue("Nombre", oContacto.Nombre);
                cmd.Parameters.AddWithValue("Apellidos", oContacto.Apellidos);
                cmd.Parameters.AddWithValue("Telefono", oContacto.Telefono);
                cmd.Parameters.AddWithValue("Correo", oContacto.Correo);
                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();




                return RedirectToAction("Inicio", "Contacto");
            }
        }

        [HttpGet]
        public ActionResult Eliminar(int? idcontacto)
        {
            if (idcontacto == null)
                return RedirectToAction("Inicio", "Contacto");

            Contacto ocontacto = olista.Where(c => c.Idcontacto == idcontacto).FirstOrDefault();

            return View(ocontacto);
        }

        [HttpPost]
        public ActionResult Eliminar(String IdContacto)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Eliminar", oconexion);
                cmd.Parameters.AddWithValue("Idcontacto", IdContacto);
                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();




                return RedirectToAction("Inicio", "Contacto");
            }
        }

        [HttpGet]
        public ActionResult Contacto()
        {
            return View();
        }


        [HttpGet]
        public ActionResult About()
        {
            return View();
        }
    }
}