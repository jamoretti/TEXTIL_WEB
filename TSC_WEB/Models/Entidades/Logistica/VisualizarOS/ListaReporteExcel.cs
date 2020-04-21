using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.VisualizarOS
{
    public class ListaReporteExcel
    {
        public string NUMERO_ORDEM { get; set; }
        public string COD_CC { get; set; }
        public string PLAN_CONT { get; set; }
        public string PROVEEDOR { get; set; }
        public string SERVICIO { get; set; }
        public string SIMBOLO { get; set; }
        public string TIPO_PAGO { get; set; }
        public string FECHA { get; set; }
        public int CANTIDAD { get; set; }
        public decimal TOTAL { get; set; }
        public string ESTADO { get; set; }
    }
}