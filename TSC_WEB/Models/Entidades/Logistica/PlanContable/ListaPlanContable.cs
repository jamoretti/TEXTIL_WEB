using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.PlanContable
{
    public class ListaPlanContable
    {
        public string CODPERIODO { get; set; }
        public string PERIODO { get; set; }
        public string CODCC { get; set; }
        public string CC { get; set; }
        public string CODPLAN { get; set; }
        public string PLANCONT { get; set; }
        public string PRESUPUESTO { get; set; }
        public string CONSUMIDO { get; set; }
        public string SIMBOLO { get; set; }
    }
}