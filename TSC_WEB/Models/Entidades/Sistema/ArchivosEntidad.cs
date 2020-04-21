using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Sistema
{
    public class ArchivosEntidad
    {
        public string nombre { get; set; }
        public string extencion { get; set; }
        public byte[] file { get; set; }
 
    }
}