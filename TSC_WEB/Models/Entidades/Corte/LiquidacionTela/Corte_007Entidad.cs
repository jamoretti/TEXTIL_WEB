using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte
{
    public class Corte_007Entidad
    {
        public string PARTIDA { get; set; }
        public string ETAPAS { get; set; }
        public decimal NUM_PANOS { get; set; }
        public decimal LARGO_PANOS { get; set; }
        public decimal PESO_PANOS { get; set; }
        public decimal ANCHO_TELA_REAL { get; set; }
        public decimal KXETAPAS { get; set; }
        //public string U_REGISTRO { get; set; }
        public int id{ get; set; }

        public string u_registro { get; set; }

    }
}