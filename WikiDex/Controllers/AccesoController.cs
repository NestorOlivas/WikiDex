using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;

using WikiDex.Models;

namespace WikiDex.Controllers
{
    public class AccesoController : Controller
    {
        static string con = @"Data Source = localhost\SQLSERVER2019; Initial Catalog = Pokemon; User = sa; Password = 21030561";
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Registrar(Usuario usuario)
        {
            if (usuario.Us_Contraseña == usuario.Us_ConfirmarClave)
            {
                //Aplicar metodo de encriptacion 
            }
            else
            {
                //Mensaje donde indique que la contraseña no es igual
                //Limpiar cajas de clave y confirmar clave 
                return View(); // Retornar vista de login
            }

            using (SqlConnection cn = new SqlConnection(con))
            {

                SqlCommand cmd = new SqlCommand("SP_RegistrarUsuario", cn);
                cmd.Parameters.AddWithValue("Us_IdUsuario" ,usuario.Us_IdUsuario);
                cmd.Parameters.AddWithValue("Us_Nombre", usuario.Us_Nombre);
                cmd.Parameters.AddWithValue("Us_Contraseña", usuario.Us_Contraseña);
                cmd.Parameters.AddWithValue("Us_RolUsuario", usuario.Us_RolUsuario);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cmd.ExecuteNonQuery();

                //Mostrar mensaje de que el usuario se registro con exito 

            }

            return View(); // Redirigir a Login 

        }
        [HttpPost]
        public ActionResult Login(Usuario usuario) 
        { 
            //Usuario.Us_Contraseña = //Metodo (Usuario.Us_Contraseña) 
                //Llamar el metodo de encriptacion 

           using (SqlConnection cn = new SqlConnection (con))
            {
                SqlCommand cmd = new SqlCommand("SP_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("Us_Nombre", usuario.Us_Nombre);
                cmd.Parameters.AddWithValue("Us_Contraseña", usuario.Us_Contraseña);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                // Enviar mensaje si no existe el usuario
                // USuario invalido/No existe etc.
                // 
                if (Usuario.Us_IdUsuario != 0)
                {
                    Session["usuario"] = usuario;
                    return RedirectToAction("Index", "Home");

                }






            }
        }
    }
}