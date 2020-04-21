using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSC_WEB.Models.Modelos.Seguridad;
using TSC_WEB.Models.Entidades.Seguridad;
using TSC_WEB.Models.Modelos.Seguridad.Requerimientos;

namespace TSC_WEB.Controllers
{
    public class SeguridadController : Controller
    {
        //HOLIS
        #region INSTANCIAS
        CambioFechaModelo objCambioFecha = new CambioFechaModelo();
        ResponsblesModelo objResponsablesM = new ResponsblesModelo();

        #endregion

        #region VISTAS
        public ActionResult VerCambioFecha()
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

        public ActionResult RequerimientosProgramador()
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
        #endregion

        #region METODOS
            public JsonResult VerCambioFechaLista()
        {
            return Json(objCambioFecha.Listar(),JsonRequestBehavior.AllowGet);
        }


                #region     REQUERIMIENTOS
                
                public JsonResult ListarResponsbles()
                {
                    return Json(objResponsablesM.ListarResponsables(),JsonRequestBehavior.AllowGet);
                }
                    
            
                #endregion

        #endregion
    }
}