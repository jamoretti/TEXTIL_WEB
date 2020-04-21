using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.AcumuladoXCuenta
{
    public class ListaAcumulado
    {
        public string MODULO { get; set; }
        public string CODAUTORIZA { get; set; }
        public string CODIGO { get; set; }
        public string PROVEEDOR { get; set; }
        public string FECHA { get; set; }
        public string SIMBOLO { get; set; }
        public string SIMBOLO_S { get; set; }
        public string SIMBOLO_D { get; set; }
        public string CC { get; set; }
        public string TIPO_PAGO { get; set; }
        public string VALOR_SOLES { get; set; }
        public string VALOR_DOLAR { get; set; }
        public string VALOR_CONSUMIDO { get; set; }

        public decimal V_SOLES { get; set; }
        public decimal V_DOLAR { get; set; }
        public decimal V_CONSUMIDO { get; set; }
    }
}