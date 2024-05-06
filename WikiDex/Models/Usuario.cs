using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiDex.Models
{
    public class Usuario
    {
        public int Us_IdUsuario { get; set; }
        public string Us_Nombre { get; set; }
        public string Us_Contraseña { get; set; }
        public int Us_RolUsuario { get; set; }

        public string Us_ConfirmarClave { get; set; }
    }
}