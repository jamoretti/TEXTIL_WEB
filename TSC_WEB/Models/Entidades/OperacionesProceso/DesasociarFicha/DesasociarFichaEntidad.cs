using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.OperacionesProceso.DesasociarFicha
{
    public class DesasociarFichaEntidad
    {
        public int operacion { get; set; }
        public string descripcionoperacion { get; set; }
        public string fechaemision { get; set; }
        public string guia { get; set; }
        public string serieguia { get; set; }
        public int almacen { get; set; }
        public string cantidad { get; set; }


    }
}