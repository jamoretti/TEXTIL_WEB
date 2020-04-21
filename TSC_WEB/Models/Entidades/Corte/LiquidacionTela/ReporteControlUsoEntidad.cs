using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte.LiquidacionTela
{
    public class ReporteControlUsoEntidad
    {



        public string partida { get; set; }
        public int ficha { get; set; }
        public string fecha { get; set; }
        public string tizado { get; set; }
        public string tendido { get; set; }
        public string corte { get; set; }
        public int cantidadprogramada { get; set; }
        public int version { get; set; }

        public bool etapas {
            get
            {
                return (tizado != string.Empty && tendido != string.Empty && corte != string.Empty) ? true : false;
            }
        }



    }
}