using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.DesarrolloProducto.FichaTecnica
{
    public class FichaTecnicaEntidad
    {
        public string fecha_generacion { get; set; }
        public string estilo_cliente {get;set;}
        public string descripcion_prenda {get;set;}
        public string cliente  {get;set;}
        public string estilo_propio {get;set;} 
        public int n_solicitud {get;set;}
        public string temporada  {get;set;}
        public string tipo_muestra {get;set;}
        public int alternativa  {get;set;}
        public int n_ruta  {get;set;}
        public string estampado  {get;set;}
        public string bordado  {get;set;}
        public string lavado  {get;set;}
        public string finura  {get;set;}
        public string pedido  {get;set;}
        public string tallas  {get;set;}
        public string po  {get;set;}
        public string grafico_prenda  {get;set;}
        public string narrativa  {get;set;}
        public string conteudo_atributo  {get;set;}
        public string densidad {get;set;}
        public string ancho_tela { get; set; }

    }
}