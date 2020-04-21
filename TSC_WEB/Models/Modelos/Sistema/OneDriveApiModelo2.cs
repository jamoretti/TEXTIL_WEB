using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KoenZomers.OneDrive.Api;
using KoenZomers.OneDrive.Api.Entities;
using KoenZomers.OneDrive.Api.Enums;
using System.Threading.Tasks;
using System.Web.Configuration;
using Newtonsoft.Json;
using TSC_WEB.Models.Entidades.Sistema;
using System.IO;

namespace TSC_WEB.Models.Modelos.Sistema
{
    public class OneDriveApiModelo2
    {
        #region INSTANCIAS
        //INSTANCIA ONE DRIVE API
        OneDriveApi OneDriveApi;
        //INSTACIA PARA CRENDECIALES DE ONE DRIVE
        string clienteid = string.Empty;
        string aplicacionid = string.Empty;
        string clientesecreto = string.Empty;
        string RefreshToken = string.Empty;

        #endregion

        #region METODOS

        //CONECTA CON EL API ONE DRIVE
        public async Task<bool> ConectarApiOneDrive()
        {
            clienteid = WebConfigurationManager.AppSettings["OneDriveForBusinessO365ApiClientID"].ToString();
            clientesecreto = WebConfigurationManager.AppSettings["OneDriveForBusinessO365ApiClientSecret"].ToString();
            RefreshToken = WebConfigurationManager.AppSettings["OneDriveApiRefreshToken"].ToString();
            aplicacionid = WebConfigurationManager.AppSettings["GraphApiApplicationId"].ToString();

            OneDriveApi = new OneDriveGraphApi(aplicacionid);
            try
            {
                await OneDriveApi.AuthenticateUsingRefreshToken(RefreshToken);
                return true;
            }
            catch (Exception e)
            {
                string mensaje = e.Message;
                return false;
            }

        }

        // SUBE Y COMPARTE ARCHIVO
        public async Task<SistemaEntidad> SubirCompartirArchivo(string rutaarchivosubir, string rutanube, string nombrearchivo, HttpPostedFileBase archivo)
        {
            SistemaEntidad objSistema = new SistemaEntidad();

            //CONECTAMOS
            var conexion = await this.ConectarApiOneDrive();

            if(conexion){

                //CREAMOS CARPETA SI NO EXISTE
                if (!Directory.Exists(rutaarchivosubir))
                {
                    Directory.CreateDirectory(rutaarchivosubir);
                }
                //GUARDAMOS ARCHIVO
                archivo.SaveAs(rutaarchivosubir + "/" + nombrearchivo);
                //SUBIMOS ARCHIVO
                var datasubida = await SubirArchivo(rutaarchivosubir + "/" + nombrearchivo, rutanube); //DECLARANDO CARPETA
                //COMPARTIMOS
                var datacompartida = await CompartirArchivo(rutanube, nombrearchivo);
                //PARSEAMOS A JSON
                var jsonParse = JsonConvert.DeserializeObject<OneDriveApiEntidadMaster>(datacompartida.mensajesistema);
                //ASIGNAMOS VALOR AL MENSAJE
                objSistema.mensajesistema = jsonParse.link.webUrl;
                objSistema.respuestabool = true;

                //BORRAMOS ARCHIVO
                if (System.IO.File.Exists(rutaarchivosubir + "/" + nombrearchivo))
                {
                    try
                    {
                        //System.IO.File.Delete(rutaarchivosubir + "/" + nombrearchivo);
                    }
                    catch
                    {
                        ;
                    }
                }

            }
            else
            {
                objSistema.mensajesistema = "No se pudo establecer las credenciales";
                objSistema.respuestabool = false;
            }

            return objSistema;
        }

        //SUBE ARCHIVO 
        public async Task<bool> SubirArchivo(string ruta,string rutanube){

            bool rpt = true;
            try
            {
                var datasubida = await OneDriveApi.UploadFile(ruta, rutanube); //DECLARANDO CARPETA
                rpt = true;
            }catch{
                rpt = false;
            }
            
            if(!rpt){
                rpt = await SubirArchivo(ruta,rutanube);
            }

            return rpt;
        }

        //COMPARTIMOS ARCHIVO
        public async Task<SistemaEntidad> CompartirArchivo(string rutanube, string nombrearchivo)
        {
            SistemaEntidad objSistema = new SistemaEntidad();
            try
            {
                var datacompartida = await OneDriveApi.ShareItem(rutanube + "/" + nombrearchivo, OneDriveLinkType.Edit);

                //OBTENEMOS JSON EN STRING
                objSistema.mensajesistema = datacompartida.OriginalJson;
                objSistema.respuestabool = true;
            }
            catch
            {
                objSistema.respuestabool = false;
            }

            if (!objSistema.respuestabool)
            {
                objSistema = await CompartirArchivo(rutanube, nombrearchivo);
            }

            return objSistema;
        }

        #endregion

    }
}