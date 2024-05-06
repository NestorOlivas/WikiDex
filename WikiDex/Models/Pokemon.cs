using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiDex.Models
{
    public class Pokemon
    {
        public int Pok_Id { get; set; }
        public int Pok_Numero { get; set; }
        public string Pok_Nombre { get; set; }
        public int Pok_Tipo { get; set; }
        public int Pok_Tipo2 { get; set; }
        public int Pok_Generacion { get; set; }
        public int Pok_Habilidad { get; set; }
        public int Pok_Habilidad2 { get; set; }
        public int Pok_HabilidadOculta { get; set; }
        public int Pok_Crianza { get; set; }
        public string Pok_Descripcion { get; set; }
        public string Pok_Fortaleza { get; set; }
        public string Pok_Debilidad { get; set; }
        public int Pok_IdUsuario { get; set; }
    }
}