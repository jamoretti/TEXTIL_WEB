using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.DesarrolloProducto.FichaTecnica
{
    public class SecuenciaOperacionesEntidad
    {
        public int codigo_estagio { get; set; }
        public int codigo_operacao { get; set; }
        public string codigo_parte_peca { get; set; }
        public string descricao { get; set; }
        public string desc_parte_peca { get; set; }
        public decimal minutos { get; set; }
        public string nome_operacao { get; set; }
        public string observacao { get; set; }
        public string observacao1 { get; set; }
        public int sequencia_estagio { get; set; }
        public int seq_operacao { get; set; }

        //AGREGADO TOTAL
        public decimal total { get; set; }

    }
}