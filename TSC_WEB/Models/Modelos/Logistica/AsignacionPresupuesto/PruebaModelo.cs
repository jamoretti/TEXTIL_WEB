using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Models.Entidades.Logistica.AsignacionPresupuesto;

namespace TSC_WEB.Models.Modelos.Logistica.AsignacionPresupuesto
{
    public class PruebaModelo
    {
        public List<Prueba> ListarPrueba()
        {
            return new List<Prueba>()
            {
                new Prueba() { nom1 = "Jhohan1", nom2 = "Moran1" },
                new Prueba() { nom1 = "Anyelo2", nom2 = "Solis2" },
            };
        }
    }
}