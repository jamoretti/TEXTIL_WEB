using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Corte
{
    public class Corte_001Entidad
    {
        public string PARTIDA               { get; set; }
        public string CLIENTES              { get; set; }
        public string PROGRAMAS             { get; set; }
        public string PEDIDOS               { get; set; }
        public string ESTILOS               { get; set; }
        public decimal ENV_TELA_MTS         { get; set; }
        public decimal ENV_TELA_KG          { get; set; }
        public int?  TOTAL_RESULT           { get; set; }
        public int?  RESULTADO              { get; set; }
        public decimal TOTAL_TALLXCONSU     { get; set; }
        public decimal VARIACION            { get; set; }
        public decimal KG_ASIGNADO          { get; set; }
        public decimal KG_SEGUN_TIZADO      { get; set; }
        public decimal RESUL_KG             { get; set; }
        public string LETRA_KG              { get; set; }
        public string F_REGISTRO { get; set; }
        public string U_REGISTRO            { get; set; }

        public decimal CONSU_MTS { get; set; }
        public decimal CONSU_KG { get; set; }
        public decimal UTILIDAD { get; set; }

        public int? VERSIONES { get; set; }
        public string TIPO_TELA { get; set; }
    }
}