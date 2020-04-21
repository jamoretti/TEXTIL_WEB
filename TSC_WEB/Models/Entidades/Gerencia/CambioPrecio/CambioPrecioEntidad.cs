using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Models.Entidades.Sistema;

namespace TSC_WEB.Models.Entidades.Gerencia.CambioPrecio
{
    public class CambioPrecioEntidad : EntidadMaestra
    {
        public string cod_cliente { get; set; }
        public string cod_clientenumero { get; set; }

        public string cliente { get; set; }
        public int pedido { get; set; }
        public decimal antiguo_precio { get; set; }
        public decimal nuevo_precio { get; set; }
        public string estado { get; set; }
        public string usuario { get; set; }
        public string fecha_solicitud { get; set; }


    }
}