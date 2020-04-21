using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.AlterarSituacion
{
    public class ListaDispXCuenta
    {
        public string CUENTA { get; set; }
        public string PRESUPUESTO { get; set; }
        public string CONSUMIDO { get; set; }
        public string DISPONIBLE { get; set; }
        public string VALOR_OC { get; set; }
        public string SIM_PRE { get; set; }
        public string SIM_OC { get; set; }
    }
}