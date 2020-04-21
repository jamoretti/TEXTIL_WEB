using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Sistema
{
    public class EmpresasEntidad : EntidadMaestra
    {
       public int codigo_empresa { get; set; }
       
       public string nome_empresa { get; set; }
    }
}
