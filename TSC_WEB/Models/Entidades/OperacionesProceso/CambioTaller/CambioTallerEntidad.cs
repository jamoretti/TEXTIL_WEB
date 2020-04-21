using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.OperacionesProceso.CambioTaller
{
    public class CambioTallerEntidad
    {
        public int Codigo_Cambio_Taller { get; set; }
        public string Codigo_Taller_Original { get; set; }
        public string Nombre_Taller_Original { get; set; }
        public int Codigo_Ficha { get; set; }
        public string Codigo_Taller_Destino { get; set; }
        public string Nombre_Taller_Destino { get; set; }
        public string Codigo_Motivo { get; set; }
        public string Responsable { get; set; }
        public string Estado_Aprobacion { get; set; }
        public string Fecha_Creacion { get; set; }
        public string Codigo_Motivo_Rechazo { get; set; }
        public string Nombre_Motivo_Rechazo { get; set; }
        public string MensajeRechazado { get; set; }
        public string Cantidad_Ficha { get; set; }
        public string Fecha_Modificacion { get; set; }
        public string Usuario_Modificacion { get; set; }
    }
}