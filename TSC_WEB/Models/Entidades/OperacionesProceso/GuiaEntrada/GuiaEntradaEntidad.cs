using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.OperacionesProceso.GuiaEntrada
{
    public class GuiaEntradaEntidad
    {
        public int Documento { get; set; }
        public string Serie { get; set; }
        public int C_Codigo_Prov9 { get; set; }
        public int C_Codigo_Prov4 { get; set; }
        public int C_Codigo_Prov2 { get; set; }
        public string Descripcion_Emisor { get; set; }
        public int Codigo_Operacion { get; set; }
        public string Descripcion_Operacion { get; set; }
        public int Codigo_Transaccion { get; set; }
        public string Descripcion_Transaccion { get; set; }
        public string Fecha_Emision { get; set; }
        public string Fecha_Transaccion { get; set; }
        public int Estado_Entrada { get; set; }
        public string Descripcion_Estado_Entrada { get; set; }
        public string Numero_Ruc { get; set; }
        public string Responsable { get; set; }
    }
}