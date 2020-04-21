using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Sistema
{
    public class UsuariosEntidad : EntidadMaestra
    {
        public string nombre { get; set; }
        public int codigo_cargo { get; set; }
        public int cod_funcionario { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public string e_mail { get; set; }

    }
}