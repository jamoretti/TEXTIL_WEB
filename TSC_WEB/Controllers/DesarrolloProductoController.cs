using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using TSC_WEB.Models.Modelos.DesarrolloProducto.FichaTecnica;
using TSC_WEB.Models.Entidades.DesarrolloProducto.FichaTecnica;

using TSC_WEB.Models.Modelos.Sistema;

using Newtonsoft.Json;
using Rotativa;
using Rotativa.Options;
using System.Security.Principal;
using System.IO;
using TSC_WEB.Models.Modelos.OperacionesProceso.SubirFichaTecnica;
using System.Threading.Tasks;
using TSC_WEB.Models.Entidades.DesarrolloProducto.Spec;
using TSC_WEB.Models.Modelos.DesarrolloProducto.Spec;
using TSC_WEB.Models.Modelos.DesarrolloProducto.RegistroMoldes;
using TSC_WEB.Models.Entidades.Sistema;


namespace TSC_WEB.Controllers
{
    public class DesarrolloProductoController : Controller
    {

        #region  INSTACIAS
        // INSTANCIAS
        OneDriveApiModelo objOneDrive = new OneDriveApiModelo();
        OneDriveApiModelo2 objOneDrive2 = new OneDriveApiModelo2();

        ReporteImprimirModelo objReporteImprimirM = new ReporteImprimirModelo();
        EtapasModelo objEtapasM = new EtapasModelo();
        ReporteEntidad objReporteEntidad = new ReporteEntidad();
        ArchivosModelo objArchivosM = new ArchivosModelo();
        ClientesModelo ObjClientes = new ClientesModelo();
        SpecModelo objFichasM = new  SpecModelo();
        RegistroMoldesModelo objRegistroM = new RegistroMoldesModelo();
        //LISTAS
        List<FichaTecnicaEntidad> objFichaTenicaLista = new List<FichaTecnicaEntidad>();
        #endregion


        #region VISTAS
        [HttpGet]
        public ActionResult ReporteFichaTecnica()
        {
            if (Session["usuario"] != null)
            {
                return View(objEtapasM.Listar());
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpGet]
        public ActionResult SubirSpec()
        {
            if (Session["usuario"] != null)
            {
                return View(objEtapasM.Listar());
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpGet]
        public ActionResult RegistroMoldes()
        {
            if (Session["usuario"] != null)
            {
                return View(objEtapasM.Listar());
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpGet]
        public ActionResult VisualizacionMoldes()
        {
            if (Session["usuario"] != null)
            {
                return View(objEtapasM.Listar());
            }
            else
            {
                return Redirect("/");
            }
        }

        #endregion

        #region METODOS

        [HttpGet]
        public ActionResult VistaReporteFichaTecnica(string pedido, string etapa)
        {


            ReporteEntidad resultado = (ReporteEntidad)Session["datasetmaestrofichatecnica"];
            //ASIGNAMOS 
            resultado.op = Session["opfichatecnica"] == "true" ? true : false;

            string _headerUrl = Url.Action("HeaderDinamico", "DesarrolloProducto", resultado.objFichaEntidad, "http");
            string _footerUrl = Url.Action("FooterDinamico", "DesarrolloProducto", null, "http");


            return new ViewAsPdf("VistaReporteFichaTecnica", resultado)
            {

                CustomSwitches = "--header-html " + _headerUrl + " --header-spacing 13 "
                + "--footer-html " + _footerUrl + " --footer-spacing 0"
                ,
                PageMargins = { Top = 70, Bottom = 20 },

            };
        }

        [HttpGet]
        public ActionResult HeaderDinamico(FichaTecnicaEntidad objFichatecnica)
        {
            return View(objFichatecnica);
        }
        
        [HttpGet]
        public ActionResult FooterDinamico()
        {
            ViewBag.usuario = WindowsIdentity.GetCurrent().Name;

            return View();
        }

        //FICHA TECNICA
        [HttpGet]
        public JsonResult ListarClientesFtec()
        {
            return Json(objFichasM.ListarClientes(), JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public FileContentResult getFile(string ruta)
        {

            var file = objArchivosM.getFile(ruta, "nombre");
            string extencion = Path.GetExtension(ruta);
            return File(file, "image/"+extencion);
            
        }


        [HttpPost]
        public JsonResult BuscarFichaTecnica(int pedido, int etapa)
        {
            ReporteEntidad resultado = objReporteImprimirM.BuscarReporte(pedido, etapa);
            Session["datasetmaestrofichatecnica"] = resultado;
            Session["pedidofichatecnica"] = pedido.ToString();
            Session["etapafichatecnica"] = etapa.ToString();
            Session["opfichatecnica"] = etapa != 0 ? "false" : "true";

            return Json(JsonConvert.SerializeObject(resultado, Formatting.Indented), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult BuscxEstilo(string estilo)
        {
            return Json(objFichasM.BuscxEstilo(estilo), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> RegistrarFichas(string estilo, string observacion, string cliente ,  HttpPostedFileBase archivo)
        {
            SpecEntidad objFichaE = new SpecEntidad();
            string usuario = Session["usuario"].ToString();
            int empresa = 1;
            //GUARDAMOS EL REGISTRO PARA OBTNER EL NOMBRE DEL ARCHIVO
            objFichaE = objFichasM.Registrar( estilo,  observacion, usuario, empresa);
            //VERIFICANDO SI HAY ERROR EN REGISTRAR
            if (!objFichaE.errorbool)
            {
                string rutaarchivo = @"D:/tmp_SPEC/";
                //CREANDO RUTA SI NO EXISTE
                try
                {
                    if (!Directory.Exists(rutaarchivo))
                    {
                        Directory.CreateDirectory(rutaarchivo);
                    }
                    objFichaE.archivo = objFichaE.archivo.Replace('/', '-').ToString();
                    rutaarchivo += objFichaE.archivo;
                    //GUARDANDO ARCHIVO
                    archivo.SaveAs(rutaarchivo);
                    //CONECTANDO CON API ONE DRIVE
                    try
                    {
                        //CONECTANDO ONE DRIVE
                        var resultado = await objOneDrive.ConectarApiOneDrive();
                        //SUBIENDO ARCHIVO
                        if (resultado)
                        {
                            //SUBIENDO ARCHIVO
                            var data = await objOneDrive.SubirArchivoSPEC(rutaarchivo, cliente);
                            if (data != null)
                            {
                                //RUTA Y NOMBRE DEL ARCHIVO CREADO 
                                string rutanombrearchivo = "SPEC/" + cliente + "/" + objFichaE.archivo;
                                var datac = await objOneDrive.Compartir(rutanombrearchivo);

                                string json = datac.OriginalJson;
                                var jsonParse = JsonConvert.DeserializeObject<OneDriveApiEntidadMaster>(json);

                                var ruta = jsonParse.link.webUrl;
                                //CAMBIANDO RUTA
                                objFichasM.ActualizarRutaSpec(ruta, objFichaE.idupload);
                                //BORRANDO EL ARCHIVO
                                System.IO.File.Delete(rutaarchivo);
                                //RETORNANDO VALOR
                                return Json(new { error = false, mensaje = "SPEC registrada correctamente" });
                            }
                            else
                            {
                                return Json(new { error = true, mensaje = "No se pudo subir archivo" });
                            }
                        }
                        else
                        {
                            return Json(new { error = true, mensaje = "No se pudo conectar con el API OneDrive" });
                        }
                    }
                    catch
                    {
                        return Json(new { error = true, mensaje = "Error con API OneDrive" });
                    }
                }
                catch
                {
                    return Json(new { error = true, mensaje = "Error en guardar el archivo en la carpeta raiz" });
                }
            }
            else
            {
                return Json(new { error = true, mensaje = objFichaE.error });
            }



        }

        //REGISTRA MOLDES
        public async Task<JsonResult> RegistrarMoldes(string estilo, string programa, string pedidos, string observacion, HttpPostedFileBase archivo)
        {
            try
            {
                
                    string usuario = Session["usuario"].ToString();
                    string rutaarchivo = @"D:/tmp_moldes/";
                    string extension = Path.GetExtension(archivo.FileName);
                    DateTime fecha = DateTime.Now;
                    string nombrearchivo =
//                        pedidos + "-" +
                        fecha.Day.ToString() +
                        fecha.Month.ToString() +
                        fecha.Year.ToString() +
                        fecha.Hour.ToString() +
                        fecha.Minute.ToString() +
                        fecha.Second.ToString() +
                        fecha.Millisecond.ToString() +
                        extension;
                    var data = await objOneDrive2.SubirCompartirArchivo(rutaarchivo, "Moldes", nombrearchivo, archivo);

                    //REGISTRAMOS
                    objRegistroM.Registrar( estilo,  programa,  pedidos, observacion, usuario, data.mensajesistema);
                    return Json(new { mensaje = "Molde Registrado correctamente", error = false });

            }catch(Exception ex){
                return Json(new {mensaje = ex.Message,error = true});
            }


        }

        //DELVUELVE PEDIDOS CON PROGRAMA
        public JsonResult getPedidosPrograma(string programa,string estilo)
        {
            return Json(objRegistroM.getPedidosPrograma(programa,estilo), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarMoldes(int? pedido,string fechai,string fechaf,string estilo,string programa)
        {
            return Json(objRegistroM.Listar(pedido,fechai,fechaf,estilo,programa),JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult BuscarRegistrosSpec(string idarea, string estilo, string estilocliente)
        {
            return Json(objFichasM.Listar(idarea, estilo, estilocliente), JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public async Task<JsonResult> RegistrarFTTELAS(int ID, string observacion, string cliente, HttpPostedFileBase archivo, string nomarchivo)
        {
            SpecEntidad objFichaE = new SpecEntidad();
            string usuario = Session["usuario"].ToString();
            int empresa = 1;
           //GUARDAMOS EL REGISTRO PARA OBTNER EL NOMBRE DEL ARCHIVO
          //  objFichaE = objFichasM.Registrar( estilo,  observacion, usuario, empresa);
            //VERIFICANDO SI HAY ERROR EN REGISTRAR

            string nuevonomarchivo = nomarchivo + "_" + ID + "_FT_TELA" + ".PDF";

            if (!objFichaE.errorbool)
            {
                string rutaarchivo = @"D:/tmp_SPEC/";
                //CREANDO RUTA SI NO EXISTE
                try
                {
                    if (!Directory.Exists(rutaarchivo))
                    {
                        Directory.CreateDirectory(rutaarchivo);
                    }
                    objFichaE.archivo = nuevonomarchivo.Replace('/', '-').ToString();
                    rutaarchivo += objFichaE.archivo;
                    //GUARDANDO ARCHIVO
                    archivo.SaveAs(rutaarchivo);
                    //CONECTANDO CON API ONE DRIVE
                    try
                    {
                        //CONECTANDO ONE DRIVE
                        var resultado = await objOneDrive.ConectarApiOneDrive();
                        //SUBIENDO ARCHIVO
                        if (resultado)
                        {
                            //SUBIENDO ARCHIVO
                            var data = await objOneDrive.SubirArchivoSPEC(rutaarchivo, cliente);
                            if (data != null)
                            {
                                //RUTA Y NOMBRE DEL ARCHIVO CREADO 
                                string rutanombrearchivo = "SPEC/" + cliente + "/" + objFichaE.archivo;
                                var datac = await objOneDrive.Compartir(rutanombrearchivo);

                                string json = datac.OriginalJson;
                                var jsonParse = JsonConvert.DeserializeObject<OneDriveApiEntidadMaster>(json);

                                var ruta = jsonParse.link.webUrl;
                                //CAMBIANDO RUTA
                                objFichasM.ActualizarFTTELA(ruta, usuario, ID, observacion);
                                //BORRANDO EL ARCHIVO
                                System.IO.File.Delete(rutaarchivo);
                                //RETORNANDO VALOR
                                return Json(new { error = false, mensaje = "FT TELA registrada correctamente" });
                            }
                            else
                            {
                                return Json(new { error = true, mensaje = "No se pudo subir archivo" });
                            }
                        }
                        else
                        {
                            return Json(new { error = true, mensaje = "No se pudo conectar con el API OneDrive" });
                        }
                    }
                    catch
                    {
                        return Json(new { error = true, mensaje = "Error con API OneDrive" });
                    }
                }
                catch
                {
                    return Json(new { error = true, mensaje = "Error en guardar el archivo en la carpeta raiz" });
                }
            }
            else
            {
                return Json(new { error = true, mensaje = objFichaE.error });
            }



        }




        #endregion




    }
}