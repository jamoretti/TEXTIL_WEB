using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Models.Entidades.DesarrolloProducto.FichaTecnica;

namespace TSC_WEB.Models.Entidades.DesarrolloProducto.FichaTecnica
{
    public class ReporteEntidad
    {
        public FichaTecnicaEntidad objFichaEntidad {get;set;}
        public List<RutasEntidad>  objRutasEntidadLista { get; set; }
        public List<RutasTelaPrincipalEntidad> objRutasTelaPrincipalLista { get; set; }
        public List<ObservacionesEntidad> objObservacionesLista { get; set; }
        public List<CombinacionesEntidad> objCombinacionesLista { get; set; }
        public List<AviosEntidad>       objAviosLista { get; set; }
        public List<SecuenciaOperacionesEntidad> objSecuenciaOperacionesLista { get; set; }

        public bool op { get; set; }

    }
}