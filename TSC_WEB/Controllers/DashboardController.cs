using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//MODELOS
using TSC_WEB.Models.Modelos.Sistema;
//ENTIDADES
using TSC_WEB.Models.Entidades.Sistema;
using TSC_WEB.Util.Constantes;

namespace TSC_WEB.Controllers
{
    public class DashboardController : Controller
    {
        #region INSTANCIAS
        //MODELOS
        EmpresasModelo objEmpresasM = new EmpresasModelo(); // EMPRESAS
        UsuariosModelo objUsuariosM = new UsuariosModelo(); // USUARIOS
        ModulosModelo objModulosM = new ModulosModelo();    // MODULOS
        //AreasModelo objAreasM = new AreasModelo();          // AREAS
        //ENTIDADES
        AreasEntidad objAreasE = new AreasEntidad();        // AREAS

        #endregion

        #region VISTAS

        //DASHBOARD PAGINA INICIO
        public ActionResult Index()
        {
             if (Session["usuario"] != null)
            {
                if (URL_EXTERNO.URL != null)
                {
                    return Redirect(URL_EXTERNO.URL);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return Redirect("/");
            }
        }
        //LOGIN 
        public ActionResult Login()
        {
            if (Session["usuario"] == null)
            {
                return View(objEmpresasM.Listar());
            }
            else
            {
                return Redirect("/dashboard/index");
            }
        }

        #endregion

        #region METODOS

        //LOGIN
        [HttpPost]
        public JsonResult Login(string usuario, string clave,int idempresa)
        {
            UsuariosEntidad objUsuariosELogin = new UsuariosEntidad();

            objUsuariosELogin = objUsuariosM.Login(usuario, clave, false);

            if (objUsuariosELogin.usuario != null)
            {
                objUsuariosELogin = objUsuariosM.Login(usuario, clave, true);
                if (objUsuariosELogin.usuario != null)
                {
                    Session["usuario"] = objUsuariosELogin.usuario;
                    Session["nombre"] = objUsuariosELogin.nombre;
                    Session["codigo_cargo"] = objUsuariosELogin.codigo_cargo.ToString();
                    Session["empresa"] = idempresa;
                    //CARGANDO LOS MODULOS CORRESPONDIENTES POR USUARIO
                    Session["modulos"] = objModulosM.ListarModulos(objUsuariosELogin.codigo_cargo);

                    return Json(new { result = true, mensaje = "Acceso concedido" });
                }
                else
                {
                    return Json(new { result = false, mensaje = "Clave incorrecta" });
                }
            }
            else
            {
                return Json(new { result = false, mensaje = "Usuario no existe" });
            }
        }
        [HttpGet]
        //CERRAR SESION
        public ActionResult CerrarSesion()
        {
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();

            URL_EXTERNO.URL = null;

            return Redirect("/");
        }



        #endregion
    }
}