using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.DesarrolloProducto.RegistroMoldes
{
    public class RegistroMoldesEntidad
    {
        public int iddesa003 { get; set; }
        public string pedidos { get; set; }
        public string observacion { get; set; }
        public string usuario{ get; set; }
        public string fecha { get; set; }
        public string rutacompartida { get; set; }
        public string estado { get; set; }

        //AGREGADO
        public string programa { get; set; }
        public string estilo { get; set; }



    }
}