using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Models.Entidades.Sistema;

namespace TSC_WEB.Models.Entidades.DesarrolloProducto.Spec
{
    public class SpecEntidad : EntidadMaestra
    {
        public int idupload { get; set; }
        public string cliente { get; set; }
        public string alternativa { get; set; }
        public string estilo { get; set; }
        public string proyecto { get; set; }
        public string observacion { get; set; }
        public string usuariocre { get; set; }
        public int empresa { get; set; }
        public string archivo { get; set; }
        public int version { get; set; }
        public int aprobacion { get; set; }
        public string fechacre { get; set; }
        public string horacre { get; set; }
        public string rutaarchivo { get; set; }

        public string ARCHIVO_TELA  { get; set; }
        public string RUTAARCHIVOTELA { get; set; }
        public string USUARIOTELA { get; set; }
            


        // FOREINGS

    }
}