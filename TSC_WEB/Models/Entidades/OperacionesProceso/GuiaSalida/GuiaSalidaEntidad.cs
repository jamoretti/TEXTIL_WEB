using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.OperacionesProceso.GuiaSalida
{
    public class GuiaSalidaEntidad
    {
        public int Empresa { get; set; }
        public string NumeroDocumento { get; set; }
        public string Serie { get; set; }
        public string Especie { get; set; }
        public string CodigoDestinatario { get; set; }
        public string NombreDestinatario { get; set; }
        public string TipoOperacion { get; set; }
        public string NombreTipoOperacion { get; set; }
        public string CondicionPago { get; set; }
        public string PedidoVenta { get; set; }
        public string FechaHoraSalida { get; set; }
        public string FechaRecepcion { get; set; }
        public string FechaBase { get; set; }
        public int SitDF { get; set; }
        public string NombreSitDF { get; set; }
        public string Responsable { get; set; }
    }
}