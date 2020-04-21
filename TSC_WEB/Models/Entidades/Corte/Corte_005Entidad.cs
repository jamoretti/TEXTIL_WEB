using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte
{
    public class Corte_005Entidad
    {
        public string PARTIDA { get; set; }
        public int? UBICA { get; set; }
        public int? TALLAXPRENDA { get; set; }
        public decimal LARGO_TIZADO { get; set; }
        public decimal CONSUMO_PRENDA { get; set; }
        public decimal PESO_PANO { get; set; }
        public decimal EFICIE_TIZADO { get; set; }
        public DateTime F_REGISTRO { get; set; }
        public string U_REGISTRO { get; set; }
    }
}