using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Models.Entidades.Sistema;

namespace TSC_WEB.Models.Entidades.Comercial
{
    public class Come_002Entidad : EntidadMaestra
    {

        public int IDUPLOAD             { get; set; }
        public int IDEMPRESA            { get; set; }
        public string PO                { get; set; }
        public int VERSIONUPLOAD        { get; set; }
        public string ARCHIVO           { get; set; }
        public string OBSERVACION       { get; set; }
        public string USUARIORCRE       { get; set; }
        public string FECHACREA          { get; set; }
        public string RUTAARCHIVO       { get; set; }
        public string cliente { get; set; }

        public string fechacre { get; set; }
        public string horacre { get; set; }
    }

}