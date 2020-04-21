using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.AcumuladoXCuenta
{
    public class IndicadoresPresupuesto
    {
        public decimal PRESUPUESTO { get; set; }
        public decimal CONSUMIDO { get; set; }
        public decimal DISPONIBLE { get; set; }
        public string SIMBOLO { get; set; }

    }
}