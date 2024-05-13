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

        public static string Key = "2024";
        public static string Resultado = "";
        public readonly Encoding Encoder = Encoding.UTF8;
        // Acceso Login 
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
                ModelState.AddModelError(string.Empty, "La contraseña no coincide");
                //Retornar la vista con el login
                return View();
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

            using (SqlConnection cn = new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand("SP_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("Us_Nombre", usuario.Us_Nombre);
                cmd.Parameters.AddWithValue("Us_Contraseña", usuario.Us_Contraseña);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                usuario.Us_IdUsuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            }

                if (usuario.Us_IdUsuario != 0)
                {
                    Session["usuario"] = usuario;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                //Mensaje para indicar que el usuario no existe
                ModelState.AddModelError(string.Empty, "El usuario no existe");
                return View();
                }
            }

        static TripleDES CrearDES(string Key)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            TripleDES des = new TripleDESCryptoServiceProvider();
            var desKey = md5.ComputeHash(Encoding.UTF8.GetBytes(Key));
            des.Key = desKey;
            des.IV = new byte[des.BlockSize / 8];
            des.Padding = PaddingMode.PKCS7;
            des.Mode = CipherMode.ECB;
            return des;
        }

        static string encriptar(string textoplano)
        {
            var des = CrearDES(Key);
            var ct = des.CreateEncryptor();
            var entrada = Encoding.UTF8.GetBytes(textoplano);
            var salida = ct.TransformFinalBlock(entrada, 0, entrada.Length);
            return Convert.ToBase64String(salida);
        }

        static string desencriptar(string textocifrado)
        {
            var des = CrearDES(Key);
            var ct = des.CreateDecryptor();
            var entrada = Convert.FromBase64String(textocifrado);
            var salida = ct.TransformFinalBlock(entrada, 0, entrada.Length);
            return Encoding.UTF8.GetString(salida);
        }
    }
    }
