using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.Visualizar
{
    public class ListaReporte5
    {
        public string CODAUTORIZA { get; set; }
        public string PEDIDO { get; set; }
        public string PLAN_CONT { get; set; }
        public string FECHA { get; set; }
        public string DESCRIPCIONPAGO { get; set; }
        public decimal TOTAL_PEDIDO { get; set; }
        public string PROVEEDOR { get; set; }
        public string COMPRADOR { get; set; }
        public string CC { get; set; }
        public string SIMBOLO { get; set; }
        public decimal VALOR_DOLAR { get; set; }
        public string ESTADO { get; set; }
    }
}