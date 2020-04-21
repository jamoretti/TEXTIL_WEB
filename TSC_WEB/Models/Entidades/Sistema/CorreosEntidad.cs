using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Sistema
{
    public class CorreosEntidad
    {
        public int idcorreo { get; set; }
        public string correo { get; set; }
        public string tipo { get; set; }
        public string proceso { get; set; }
        public string Observacion { get; set; }
        public string dominio { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
    }
}