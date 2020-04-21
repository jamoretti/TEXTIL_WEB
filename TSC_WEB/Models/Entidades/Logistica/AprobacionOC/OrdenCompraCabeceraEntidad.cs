using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Logistica.AprobacionOC
{
    public class OrdenCompraCabeceraEntidad
    {
        public int codigo_empresa { get; set; }
        public int pedido_compra { get; set; }
        public string data_prev_entr { get; set; }
        public string datetime_pedido { get; set; }
        public string FORN_PED_FORNE9 { get; set; }
        public string FORN_PED_FORNE4 { get; set; }
        public string FORN_PED_FORNE2 { get; set; }
        public string COND_PGTO_COMPRA { get; set; }
        public string DESCR_COND_PGTO { get; set; }
        public int COD_MOEDA { get; set; }
        public string DESCRICAO { get; set; }
        public string SIMBOLO_MOEDA { get; set; }
        public string TOTAL_PEDIDO { get; set; }
        public string SITUACAO_PEDIDO { get; set; }
        public string NOME_FORNECEDOR { get; set; }
        public string CODIGO_COMPRADOR { get; set; }
        public string NOME_COMPRADOR { get; set; }

        // DETALLEs
        public string centrocosto { get; set; }
        public string periodo { get; set; }
        public decimal presupuesto { get; set; }
        public decimal valorusado { get; set; }
        public decimal disponible { get; set; }
        public decimal aprobado { get; set; }



    }
}