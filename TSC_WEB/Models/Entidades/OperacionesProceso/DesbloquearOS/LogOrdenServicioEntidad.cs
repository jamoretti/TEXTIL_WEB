using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.OperacionesProceso.DesbloquearOS
{
    public class LogOrdenServicioEntidad
    {
        public string Opcion { get; set; }
        public string Proceso { get; set; }
        public int Numero_Orden { get; set; }
        public int Situacion_Orden { get; set; }
        public string Responsable { get; set; }
        public int Estado_cambio { get; set; }
        public int Estado_final { get; set; }
        public int Estado_Inicial { get; set; }
        public DateTime Fecha_final { get; set; }
        public int Flag_regresa { get; set; }
        public string Pc_usuario { get; set; }
    }
}