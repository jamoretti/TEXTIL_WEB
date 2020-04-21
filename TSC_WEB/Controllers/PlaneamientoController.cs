using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSC_WEB.Models.Modelos.Planeamiento;

namespace TSC_WEB.Controllers
{
    public class PlaneamientoController : Controller
    {
        #region INSTANCIAS

        PedidosModelo objPedidosM = new PedidosModelo();

        #endregion

        #region VISTAS
        public ActionResult PedidosAmbulancia()
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
        [HttpGet]
        public JsonResult BuscarPedidosAmbulancia(int? pedido,string flag)
        {
            flag = flag == "0" ? null : "'"+flag+"'";
            return Json(objPedidosM.BuscarPedidos(flag), JsonRequestBehavior.AllowGet);
        }
            
        #endregion
    }
}