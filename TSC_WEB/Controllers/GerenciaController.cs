using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSC_WEB.Models.Modelos.Gerencia.CambioTaller;
using TSC_WEB.Models.Entidades.OperacionesProceso.CambioTaller;
using TSC_WEB.Models.Modelos.OperacionesProceso.CambioTaller;
using TSC_WEB.Models.Modelos.Gerencia.CambioPrecio;
using System.Threading.Tasks;
using TSC_WEB.Models.Entidades.Sistema;

namespace TSC_WEB.Controllers
{
    public class GerenciaController : Controller
    {
        #region INSTANCIAS
        CambioTallerGerenciaModelo  objTallerGM = new CambioTallerGerenciaModelo();
        CambioPrecioModelo objPrecioM = new CambioPrecioModelo();
        //CambioTallerModelo objTallerM = new CambioTallerModelo();
        TallerDestinoModelo objTallerD = new TallerDestinoModelo();
        #endregion


        #region VISTAS
        public ActionResult AprobacionCambioTaller()
        {
            if (Session["usuario"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }
        public ActionResult AprobacionCambioPrecio()
        {
            if (Session["usuario"] != null)
            {
                return View(objPrecioM.ListarMotivos());
            }
            else
            {
                return Redirect("/");
            }
        }
        #endregion

        #region METODOS

            //APROBACION CAMBIO DE TALLER
            [HttpGet]
            public JsonResult ListarCambioTaller()
            {
                return Json(objTallerGM.Listar(),JsonRequestBehavior.AllowGet);
            }
            [HttpGet]
            public JsonResult ListarMRechazoCTaller()
            {
                return Json(objTallerGM.ListarMotivoRechazo(),  JsonRequestBehavior.AllowGet);
            }
            //CAMBIA ESTADO SOLICITUD CAMBIO TALLER
            public JsonResult SetUpdateCambioTaller(CambioTallerEntidad objFile)
            {
                bool resul = false;
                List<TallerDestinoEntidad> taller = objTallerD.Listar();
                var tallerOrigen = taller.Where(x => Convert.ToInt64(x.CodigoTaller) == Convert.ToInt64(objFile.Codigo_Taller_Original)).ToList();
                var tallerDestino = taller.Where(x => Convert.ToInt64(x.CodigoTaller) == Convert.ToInt64(objFile.Codigo_Taller_Destino)).ToList();
                var nombreTallerOrigen = tallerOrigen[0].NombreTaller;
                var nombreTallerDestino = tallerDestino[0].NombreTaller;
                objFile.Usuario_Modificacion = Session["Usuario"].ToString();
                resul = objTallerGM.GetUpdateCambioTaller(objFile);
                string varmensaje = "";
                string vartexto = "";

                if (objFile.Estado_Aprobacion == Utilitarios.EstadoAprobacion.Aprobado)
                {
                    varmensaje = Utilitarios.EstadoAprobacion.MensajeAprobado;
                    vartexto = "";
                }
                else
                {
                    varmensaje = Utilitarios.EstadoAprobacion.MensajeRechazado;
                    vartexto = ",el motivo de rechazo ha sido por: ";
                }
                var t = Task.Run(() =>
                {
                    objTallerGM.EnviarCorreo(objFile.Codigo_Ficha, "Estimado se ha " + varmensaje + " el cambio de taller" + "\n" + "de la ficha N°: " + objFile.Codigo_Ficha + " \n" + "Perteneciente al taller: " + nombreTallerOrigen + " \n" + "para ser enviado al taller: " + nombreTallerDestino + " " + vartexto + objFile.MensajeRechazado);
                });
                return Json(resul);
            }

            //APROBACION CAMBIO DE PRECIO
            [HttpGet]
            public JsonResult AprobarCambioPrecio(int pedido, string codcodigo, string estado, string motivo, int? idmotivo = null)
            {
                return Json(objPrecioM.CambiarEstado(pedido, codcodigo, estado, motivo, idmotivo), JsonRequestBehavior.AllowGet);
            }
            
            // LISTA SOLICITUDES
            [HttpGet]
            public JsonResult ListaSolicitudes()
            {
                return Json(objPrecioM.ListarPendientes(),JsonRequestBehavior.AllowGet);
            }
            
        #endregion
    }
}