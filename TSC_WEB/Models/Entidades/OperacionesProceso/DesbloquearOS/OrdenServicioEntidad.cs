using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.OperacionesProceso.DesbloquearOS
{
    public class OrdenServicioEntidad
    {
        public int Numero_Orden { get; set; }
        public string Numero_Orden_String { get; set; }
        public int Situacion_Orden { get; set; }
        public int Codigo_Cancela { get; set; }
        public int Indicador_Inicial { get; set; }
    }
}