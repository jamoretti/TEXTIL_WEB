using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TSC_WEB.Models.Entidades.Corte.LiquidacionTela;
using TSC_WEB.Models.Modelos.Corte.LiquidacionTela;
using Newtonsoft.Json;
using System.Data;

namespace TSC_WEB.Controllers
{
    public class CorteController : Controller
    {
        #region INSTANCIAS
        FichasModelo objFichas = new FichasModelo();
        ReporteControlUsoModelo objControlUsoM = new ReporteControlUsoModelo();
        #endregion

        #region VISTAS
        public ActionResult LiquidacionCorte()
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

        public ActionResult BuscarLiquidacion()
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

        public ActionResult MermasCorte()
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

        public ActionResult LiquidacionPorFichas()
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

        public ActionResult ReporteControlUso()
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

        public ActionResult ReporteControlUsoIndicador()
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
        #region BUSCAR LIQUIDACION
        // LISTA DE VERSIONES
        [HttpGet]
        public JsonResult ListarVersiones(string ficha)
        {
            return Json(objFichas.ListarVersiones(ficha,"","", "1"), JsonRequestBehavior.AllowGet);
        }
        // LISTAR CABECERA DE CORT_001
        [HttpGet]
        public JsonResult ListarCabeceraCort001(string ficha, string version, string tela)
        {
            return Json(objFichas.ListarCabeceraCort001(ficha, version, tela, "2"), JsonRequestBehavior.AllowGet);
        }
        // CANTIDA_PRENDAS / CANTIDAD PAÑOS
        [HttpGet]
        public JsonResult ObjCant(string ficha, string version, string tela)
        {
            return Json(objFichas.ObjCant(ficha, version, tela, "3"), JsonRequestBehavior.AllowGet);
        }
        //DATOS DE LA TELA REGISTRADOS
        [HttpGet]
        public JsonResult ListaDatosTela(string ficha, string version, string tela)
        {
            return Json(objFichas.ListaDatosTela(ficha, version, tela, "4"), JsonRequestBehavior.AllowGet);
        }
        //FICHAS - CONSUMO
        [HttpGet]
        public JsonResult ListaConsumos(string ficha, string version, string tela)
        {
            return Json(JsonConvert.SerializeObject(objFichas.ListaConsumos(ficha, version, tela, "15"), Formatting.Indented), JsonRequestBehavior.AllowGet);

        }
        //LISTA DE DATOS CALCULADOS POR EL TIZADOR
        [HttpGet]
        public JsonResult ListaTotal(string ficha, string version, string tela)
        {
            return Json(objFichas.ListaTotal(ficha,version,tela,"6"), JsonRequestBehavior.AllowGet);
        }
        //LISTA DE PROPORCIONES
        [HttpGet]
        public JsonResult ListaProporciones(string ficha, string version, string tela)
        {
            return Json(JsonConvert.SerializeObject(objFichas.ListaProporciones(ficha, version, tela, "7"), Formatting.Indented), JsonRequestBehavior.AllowGet);
        }
        //FICHAS X CANTIDADES
        [HttpGet]
        public JsonResult FichasxCantidades(string ficha, string version, string tela)
        {
            return Json(JsonConvert.SerializeObject(objFichas.FichasxCantidades(ficha, version, tela, "8"), Formatting.Indented), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region REGISTRAR TENDIDO

        // BUSCAR SI EXISTE REGISTRO DEL TIZADOR PARA PODER REGISTRAR ETAPAS
        [HttpGet]
        public JsonResult BuscarExiste(string ficha )
        {
            return Json(objFichas.BuscarExiste(ficha, "1", "P", "11"), JsonRequestBehavior.AllowGet);
        }
        // LISTA DE FICHAS X CANTIDADES - PARA REFERANCIA PARA EL TENDEDOR
        [HttpGet]
        public JsonResult BuscarFicha(string ficha,string version, string tela)
        {
            return Json(objFichas.Listarfichas(ficha, version, tela,"9"), JsonRequestBehavior.AllowGet);
        }
        // REGISTRAR ETAPAS CORT_006
        [HttpPost]
        public JsonResult RegistrarEtapas(string PARTIDA, string ETAPAS, decimal NUM_PANOS, decimal LARGO_PANOS, decimal PESO_PANOS, decimal ANCHO_TELA_REAL, decimal KXETAPAS, string U_REGISTRO, string VERSION, string TELA)
        {
            bool resul = false;
            string usuario = Session["usuario"].ToString();
            resul = objFichas.RegistrarCorte007(PARTIDA, ETAPAS, NUM_PANOS, LARGO_PANOS, PESO_PANOS, ANCHO_TELA_REAL, KXETAPAS, usuario, VERSION, TELA);
            return Json(resul);
        }
        //LISTA DE ETAPAS REGISTRADAS
        [HttpGet]
        public JsonResult ListarCorte007(string ficha, string version, string tela)
        {
            return Json(objFichas.ListarCorte(ficha, version, tela , "10"), JsonRequestBehavior.AllowGet);
        }
        // REGISTRAR CABECERA CORT_007
        [HttpPost]
        public JsonResult RegistrarCabecera(string PARTIDA, decimal MER_PUNTAS, decimal MER_RETAZOSMAS, decimal MER_RETAZOSMEN, decimal EMPALMES, decimal DEVO_PRIMERA, decimal DEVO_SEGUNDA, decimal CONOS, decimal PLASTICO, string VERSION, string TELA,decimal kgadicionales,int motivoadicional)
        {
            bool resul = false;
            string usuario = Session["usuario"].ToString();
            resul = objFichas.RegistrarCorte006(PARTIDA, MER_PUNTAS, MER_RETAZOSMAS, MER_RETAZOSMEN, EMPALMES, DEVO_PRIMERA, DEVO_SEGUNDA, CONOS, PLASTICO, usuario, VERSION, TELA, kgadicionales, motivoadicional);
            return Json(resul);
        }
        //MODIFICAR CORT007
        [HttpPost]
        public JsonResult Modificar_cort007(int id, decimal num_panos, decimal largo_panos, decimal peso_panos, decimal ancho_tela_real, decimal kxetapas)
        {
            bool resul = false;
            string usuario = Session["usuario"].ToString();
            resul = objFichas.ModificarCort007(id,num_panos,largo_panos,peso_panos,ancho_tela_real,kxetapas);
            return Json(resul);
        }

        //LISTA DE CABECERA DE CORT_006
        [HttpGet]
        public JsonResult BuscarCabecera(string ficha, string version, string tela)
        {
            return Json(objFichas.BuscarCabe(ficha, version, tela, "12"), JsonRequestBehavior.AllowGet);
        }

     
        //RETORNA LISTA PARA COPARACION DE KILOS TENDIDO
        [HttpGet]
        public JsonResult getComparacionKilos(int ficha, int version,string tela)
        {
            return Json(objFichas.ComparacionKilos(ficha, version,tela), JsonRequestBehavior.AllowGet);
        }

        // BUSCAR SI EXISTE REGISTRO DEL TENDEDOR PARA EL USUARIO DE CORTE PUEDA SEGUIR CON EL PROCESO
        [HttpGet]
        public JsonResult BuscarRegistro008(string ficha, string version, string tela)
        {
            return Json(objFichas.BuscarRegistro008(ficha, version, tela, "14"), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region REGISTRAR CORTE
        // BUSCAR SI EXISTE REGISTRO DE ETAPAS INGRESADAS POR EL TENDEDOR
        [HttpGet]
        public JsonResult BuscarExisteCort007(string ficha, string version, string tela)
        {
            return Json(objFichas.BuscarExisteCort007(ficha,version, tela, "13"), JsonRequestBehavior.AllowGet);
        }

        // REGISTRA CORT_008
        [HttpPost]
        public JsonResult RegistrarCorte008(string PARTIDA, decimal ORILLOS, decimal EXTREMOS, decimal ENTRECORTE, string U_REGISTRO, string version, string tela)
        {
            bool resul = false;
            string usuario = Session["usuario"].ToString();
            resul = objFichas.RegistrarCorte008(PARTIDA, ORILLOS, EXTREMOS, ENTRECORTE, usuario, version, tela);
            return Json(resul);
        }

        #endregion

        #region REPORTELIQUIDACION
        [HttpGet]
        public FileResult ExportarReporteLiquidacion(string partida)
        {
            var obj = objFichas.GenerarReporteLiquidacion(partida);

            return File(objFichas.ExportarReporteLiquidacion(obj) , "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteLiquidacion" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        }


        #endregion

        #region REPORTE CONTROL USO

        public JsonResult ReporteControlUsoListar(string partida,string fechainicio,string fechafin,int? ficha)
        {
            string mensaje = string.Empty;
            var resultado = new List<ReporteControlUsoEntidad>();
            try
            {
                resultado = objControlUsoM.Listar(fechainicio, fechafin, partida, ficha);
            }
            catch (Exception e)
            {
                mensaje = e.Message.ToString();
            }

            return Json(new { mensaje = mensaje, registros = resultado });

        }

        [HttpGet]
        public JsonResult ReporteControlUsoListar2(int semana1,int semana2 )
        {
            string mensaje = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                ds = objControlUsoM.Listar2(semana1, semana2);
            }
            catch (Exception e)
            {
                mensaje = e.Message.ToString();
            }

            return Json(new { mensaje = mensaje, registros = JsonConvert.SerializeObject(ds) },JsonRequestBehavior.AllowGet);

        }

        #endregion


        #endregion
    }

}