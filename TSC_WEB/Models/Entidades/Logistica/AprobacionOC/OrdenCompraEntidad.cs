using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.AprobacionOC
{
    public class OrdenCompraEntidad
    {
        public int pedido { get; set; }
        public string fecha { get; set; }
        public string descripcionpago { get; set; }
        public string moneda { get; set; }
        //public decimal total_pedido { get; set; }
        public string total_pedido { get; set; }

        public string proveedor { get; set; }
        public string comprador { get; set; }
        public int niveles { get; set; }
        public string centrocosto { get; set; }
        public int accesocentrocosto{ get; set; }


    }
}