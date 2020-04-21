using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSC_WEB.Models.Modelos.Sistema;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Web.Configuration;
using Newtonsoft.Json;
using System.IO;


//ENTIDADES
using TSC_WEB.Models.Entidades.Sistema;
using TSC_WEB.Models.Entidades.OperacionesProceso.SubirFichaTecnica;
using TSC_WEB.Models.Entidades.OperacionesProceso.CambioTaller;
using TSC_WEB.Models.Entidades.OperacionesProceso.DesbloquearOS;
using TSC_WEB.Models.Entidades.OperacionesProceso.GuiaEntrada;
using TSC_WEB.Models.Entidades.OperacionesProceso.GuiaSalida;
//MODELOS
using TSC_WEB.Models.Modelos.OperacionesProceso.SubirFichaTecnica;
using TSC_WEB.Models.Modelos.OperacionesProceso.CambioTaller;
using TSC_WEB.Models.Modelos.OperacionesProceso.DesbloquearOs;
using TSC_WEB.Models.Modelos.OperacionesProceso.GuiaEntrada;
using TSC_WEB.Models.Modelos.OperacionesProceso.GuiaSalida;
using TSC_WEB.Models.Modelos.OperacionesProceso.DesasociarFicha;



namespace TSC_WEB.Controllers
{
    public class OperacionesController : Controller
    {
        #region INSTANCIAS

        //MODELOS
        OneDriveApiModelo   objOneDrive = new OneDriveApiModelo();
        OneDriveApiModelo2 objOneDrive2 = new OneDriveApiModelo2();
        AreasModelo objAreasM = new AreasModelo();
        FichaTecnicaModelo objFichasM = new FichaTecnicaModelo();
        CambioTallerModelo objCTallerM = new CambioTallerModelo();
        TallerDestinoModelo objTallerDestinoM = new TallerDestinoModelo();
        MotivoModelo objMotivoM = new MotivoModelo();
        OrdenServicioModelo objOrdenM = new OrdenServicioModelo();
        GuiaEntradaModelo objGEntradaM = new GuiaEntradaModelo();
        GuiaSalidaModelo objGSalidaM = new GuiaSalidaModelo();
        MotivoActualizacionModelo objMotivoActulizacionM = new MotivoActualizacionModelo();
        ClientesModelo objClientesFtecM = new ClientesModelo();
        ProgramasClientesModelo objProgramaCliFtecM = new ProgramasClientesModelo();
        DesasociarFichaModelo objDesasociarFichaM = new DesasociarFichaModelo();

        //ENTIDADES
        GuiaEntradaEntidad objGEntradaE = new GuiaEntradaEntidad();


        #endregion

        #region VISTAS
        //FICHA TECNICA
        public ActionResult SubirFichaTecnica()
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
        public ActionResult BuscarFichaTecnica()
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
        //SOLICITUD CAMBIO TALLER
        public ActionResult CambioTaller()
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
        //APERTURA GUIA ENTRADA
        public ActionResult AperturarGuiaEntrada()
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
        //APERTURA GUIA SALIDA
        public ActionResult AperturarGuiaSalida()
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
        //DESBLOQUEAR ORDEN SERVICIO
        public ActionResult DesbloquearOS()
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
        //DESASOCIAR FICHA
        public ActionResult DesasociarFicha()
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
        //OPERACIONES

            //FICHA TECNICA
            [HttpGet]
            public JsonResult ListarAreas()
            {
            return Json(objAreasM.ListarAreas(), JsonRequestBehavior.AllowGet);
            }
            [HttpGet]
            public  JsonResult BuscarRegistrosFichaTenica(string op, string idarea, string estilo, string pedido)
            {
                return Json(objFichasM.Listar(op, idarea, estilo, pedido), JsonRequestBehavior.AllowGet);
            }

            [HttpPost]
            public async Task<JsonResult> RegistrarFichas(int idarea, string estilo, string proyecto,string observacion,string cliente ,string programa,int? idmotivoactualizacion,HttpPostedFileBase archivo)
            {
                FichaTecnicaEntidad objFichaE = new FichaTecnicaEntidad();
                string usuario = Session["usuario"].ToString();
                int empresa = 1;
                //GUARDAMOS EL REGISTRO PARA OBTNER EL NOMBRE DEL ARCHIVO
                objFichaE = objFichasM.Registrar(idarea, estilo, proyecto, observacion, usuario, empresa,cliente,programa,idmotivoactualizacion);
                //VERIFICANDO SI HAY ERROR EN REGISTRAR
                if (!objFichaE.errorbool)
                {
                    string rutaarchivo = @"D:/tmp_fichatecnica";
                    //OBTENIENDO AREAS
                    string nombrearea = string.Empty;
                    switch (idarea)
                    {
                        case 1: nombrearea = "COSTURA"; break;
                        case 2: nombrearea = "CORTE"; break;
                        case 3: nombrearea = "ACABADOS"; break;
                        case 4: nombrearea = "LAVANDERIA"; break;
                        case 5: nombrearea = "TODOS"; break;
                    }

                    string nombrearchivonuevo = objFichaE.archivo.Replace('/', '-').ToString();
                    nombrearchivonuevo = nombrearchivonuevo.Replace('#', '-').ToString();

                    var data = await objOneDrive2.SubirCompartirArchivo(rutaarchivo, "ficha-tecnica/" + nombrearea, nombrearchivonuevo, archivo);

                    if(data.respuestabool){
                        objFichasM.ActualizarRuta(data.mensajesistema, objFichaE.idupload);
                        return Json(new { error = false, mensaje = "Ficha registrada correctamente" });
                    }
                    else
                    {
                        return Json(new { error = true, mensaje = data.mensajesistema });
                    }

                }
                else
                {
                    return Json(new { error = true, mensaje = "No se pudo registrar la ficha" });
                }

                    /*
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
                                //OBTENIENDO AREAS
                                string nombrearea = string.Empty;
                                switch (idarea)
                                {
                                    case 1: nombrearea = "COSTURA"; break;
                                    case 2: nombrearea = "CORTE"; break;
                                    case 3: nombrearea = "ACABADOS"; break;
                                    case 4: nombrearea = "LAVANDERIA"; break;
                                    case 5: nombrearea = "TODOS"; break;
                                }

                                //SUBIENDO ARCHIVO

                                    string ruta = await objOneDrive.Ejecutar(rutaarchivo,nombrearea, objFichaE.archivo);
                                    while(ruta == "error"){
                                        ruta = await objOneDrive.Ejecutar(rutaarchivo,nombrearea, objFichaE.archivo);

                                    }

                                    //CAMBIANDO RUTA
                                    objFichasM.ActualizarRuta(ruta, objFichaE.idupload);
                                    //BORRANDO EL ARCHIVO
                                    if (System.IO.File.Exists(rutaarchivo))
                                    {
                                        try
                                        {
                                            System.IO.File.Delete(rutaarchivo);
                                        }
                                        catch
                                        {
                                            ;
                                        }
                                    } 
                                    
                                    //RETORNANDO VALOR
                                    return Json(new { error = false, mensaje = "Ficha registrada correctamente" });
                                
                            }
                            else
                            {
                                return Json(new { error = true, mensaje="No se pudo conectar con el API OneDrive" });
                            }
                        }
                        catch
                        {
                            return Json(new { error = true , mensaje = "Error con API OneDrive"});
                        }
                    }
                    catch
                    {
                        return Json(new { error = true, mensaje = "Error en guardar el archivo en la carpeta raiz" }); 
                    }
                }
                else
                {
                    return Json(new { error = true , mensaje = objFichaE.error});
                }*/

            

            }
            [HttpGet]
            public JsonResult CambiarEAprobacion(int idupload)
            {
            return Json(new { result = objFichasM.CambiaEstadoAprobacion(idupload) ? true : false},JsonRequestBehavior.AllowGet );
            }
            [HttpGet]
            public JsonResult ValidaActualizacion(int idarea, string estilo, string proyecto)
            {
                return Json(new { actualizacion = objFichasM.ValidaExistencia(idarea,estilo,proyecto)},JsonRequestBehavior.AllowGet);
            }
            [HttpGet]
            public JsonResult ListarMActualizacion()
            {
                return Json(objMotivoActulizacionM.Listar(),JsonRequestBehavior.AllowGet);
            }
            [HttpGet]
            public JsonResult ListarClientesFtec()
            {
                return Json(objClientesFtecM.Listar(),JsonRequestBehavior.AllowGet);
            }
            [HttpGet]
            public JsonResult ListarProgramasClientesFtec(string cliente9,string cliente4,string cliente2)
            {
                return Json(objProgramaCliFtecM.Listar(cliente9,cliente4,cliente2),JsonRequestBehavior.AllowGet);
            }

            //CAMBIO TALLER
            [HttpGet]
            public JsonResult ListarRegistrosCTaller()
            {
                return Json(objCTallerM.Listar(0),JsonRequestBehavior.AllowGet);
            }
            [HttpGet]
            public JsonResult ListarTallerDestino()
            {
                return Json(objTallerDestinoM.Listar(), JsonRequestBehavior.AllowGet);
            }
            [HttpGet]
            public JsonResult ListarMotivo()
            {
                return Json(objMotivoM.Listar(), JsonRequestBehavior.AllowGet);
            }
            [HttpPost]
            public JsonResult SetCambioTaller(CambioTallerEntidad objFile)
            {
                bool resul = false;
                int rint = 0;
                int rver = objCTallerM.ExisteFicha(objFile);
                if (rver == 0)
                {
                    rint = 2;
                    return Json(rint);
                }
                int resp = objCTallerM.ListaFicha(objFile);
                if (resp > 0)
                {
                    resul = false;
                    return Json(resul);
                }
                
                objFile.Responsable = Session["Usuario"].ToString();

                resul = objCTallerM.GetCambioTaller(objFile);
                var t = Task.Run(() =>
                {
                    objCTallerM.EnviarCorreo(objFile.Codigo_Ficha, "Estimado se ha enviado solicitud aprobación para el cambio de taller" + "\n" + "de la ficha N°: " + objFile.Codigo_Ficha + " \n" + "para ser enviado al taller: " + objFile.Nombre_Taller_Destino);
                });
                return Json(resul);
            }
            [HttpGet]
            public JsonResult EliminaSolicitudCT(int id)
            {
                return Json(new { result = objCTallerM.EliminarSolicitud(id)},JsonRequestBehavior.AllowGet);
            }

            //DESBLOQUEAR ORDEN SERVICIO
            [HttpPost]
            public JsonResult GetValidaOrden(OrdenServicioEntidad objFile)
            {
                int resul = 0;
                int situ = 0;
                int EstadoAnterior = 0;
                string numorden = "";
                string[] NumeroOrden = objFile.Numero_Orden_String.Trim().Split(',');

                if (NumeroOrden.Length > 0)
                {
                    for (int x = 0; x < NumeroOrden.Length; x++)
                    {

                        if (x == NumeroOrden.Length - 1)
                        {
                            numorden += NumeroOrden[x];
                        }
                        else
                        {
                            numorden += NumeroOrden[x] + ",";
                        }
                    }
                }
                else
                {
                    numorden = objFile.Numero_Orden_String.Trim();
                }
                objFile.Numero_Orden_String = numorden;
                List<OrdenServicioEntidad> listaVer = objOrdenM.ListaOrdenServicio(objFile);
                if (listaVer.Count == 0)
                {
                    resul = 1;
                    return Json(resul);
                }
                else
                {
                    for (int j = 0; j <= listaVer.Count - 1; j++)
                    {
                        EstadoAnterior = listaVer[j].Situacion_Orden;

                        var orden_situacion = listaVer.Where(x => Convert.ToInt64(x.Situacion_Orden) == Utilitarios.ValorEstado.Bloqueado).ToList();
                        if (orden_situacion.Count > 0)
                        {
                            resul = 2;
                            return Json(resul);
                        }
                        situ = Utilitarios.ValorEstado.Bloqueado;
                        resul = objOrdenM.GetCambioOrdenServicio(listaVer[j].Numero_Orden, situ);
                        IPHostEntry host;
                        host = Dns.GetHostEntry(Dns.GetHostName());
                        LogOrdenServicioEntidad objLogdato = (new LogOrdenServicioEntidad()
                        {
                            Opcion = "I",
                            Proceso = Utilitarios.Metodos.NombreClase,
                            Numero_Orden = listaVer[j].Numero_Orden,
                            Estado_Inicial = listaVer[j].Situacion_Orden,
                            Responsable = Session["Usuario"].ToString(),
                            Estado_cambio = situ,
                            Estado_final = listaVer[j].Situacion_Orden,
                            Fecha_final = DateTime.Today,
                            Flag_regresa = objFile.Indicador_Inicial,
                            Pc_usuario = host.HostName,

                        });
                        var reglog = objOrdenM.GetRegistroLog(objLogdato);
                    }
                    if (objFile.Indicador_Inicial == 1)
                    {
                        var t = Task.Run(() =>
                        {
                            situ = EstadoAnterior;
                            List<OrdenServicioEntidad> listaVerHilo = objOrdenM.ListaOrdenServicio(objFile);
                            int hor = 10 * 60000;
                            Thread.Sleep(hor);
                            for (int i = 0; i <= listaVerHilo.Count - 1; i++)
                            {
                                resul = objOrdenM.GetCambioOrdenServicio(listaVerHilo[i].Numero_Orden, situ);
                            }
                        });

                    }
                }
                return Json(resul);
            }
        
            //APERTURA GUIA ENTRADA
            [HttpGet]
            public ActionResult GetVerDatosApertura(GuiaEntradaEntidad objFile)
            {
                List<GuiaEntradaEntidad> listaVer = objGEntradaM.GetListaAperturaGuia(objFile.Documento, objFile.Serie, objFile.Numero_Ruc);
                return Json(listaVer, JsonRequestBehavior.AllowGet);
            }
            [HttpPost]
            public JsonResult SetAperturaGuia(GuiaEntradaEntidad objFile)
            {
                bool resul = false;
                int resv = 0;
                int rval = 0;
                int resp = objGEntradaM.BuscaGuiaEntrada(objFile.Documento, objFile.Serie, objFile.Numero_Ruc);
                if (resp == 0)
                {
                    resv = 0;
                    return Json(resv);
                }
                else if (resp == 1)
                {
                    resv = 2;
                    return Json(resv);
                }else if (resp == 3){
                    resv = 4;
                    return Json(resv);
                }
                objFile.Responsable = Session["Usuario"].ToString();
                int respvalida = objGEntradaM.ValidaPermiso(objFile.Documento, objFile.Serie, objFile.Numero_Ruc, objFile.Responsable);
                if (respvalida == 0)
                {
                    rval = 3;
                    return Json(rval);
                }
                resul = objGEntradaM.GetAperturaGuia(objFile.Documento, objFile.Serie, objFile.Numero_Ruc, objFile.Responsable);
                return Json(resul);
            }
            
            //APERTURA GUIA SALIDA
            [HttpGet]
            public ActionResult GetVerDatosAperturaSalida(GuiaSalidaEntidad objFile)
            {
                List<GuiaSalidaEntidad> listaVer = objGSalidaM.GetListaAperturaSalida(objFile.Empresa, objFile.NumeroDocumento, objFile.Serie);
                return Json(listaVer, JsonRequestBehavior.AllowGet);
            }
            [HttpPost]
            public JsonResult SetAperturaGuiaSalida(GuiaSalidaEntidad objFile)
            {
                bool resul = false;
                int resv = 0;
                int resp = objGSalidaM.BuscaGuiaSalida(objFile.NumeroDocumento, objFile.Serie);
                if (resp == 0)
                {
                    resv = 0;
                    return Json(resv);
                }
                else if (resp == 1)
                {
                    resv = 2;
                    return Json(resv);
                }
                else if (resp == 3)
                {
                    resv = 3;
                    return Json(resv);
                }
                objFile.Responsable = Session["Usuario"].ToString();
                resul = objGSalidaM.GetAperturaGuiaSalida(objFile.Empresa, objFile.NumeroDocumento, objFile.Serie);
                return Json(resul);
            }
            

            //DESASOCIAR FICHA
            [HttpGet]
            public JsonResult ListarDespachosDevoluciones(char op,int ficha,int os)
            {
                return Json(objDesasociarFichaM.ListarDespachosDevoluciones(op,ficha,os),JsonRequestBehavior.AllowGet);
            }


        #endregion

    }
}