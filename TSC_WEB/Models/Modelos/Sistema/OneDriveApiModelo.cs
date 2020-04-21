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
    public class OneDriveApiModelo
    {
        //INSTANCIA ONE DRIVE API
        OneDriveApi OneDriveApi;
        //INSTACIA PARA CRENDECIALES DE ONE DRIVE
        string clienteid = string.Empty;
        string aplicacionid = string.Empty;
        string clientesecreto = string.Empty;

        string RefreshToken = string.Empty;

        //CONECTAR ONE
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
            }catch(Exception e){
                string mensaje = e.Message;
                return false;
            }

        }

        public async Task<OneDriveItem> SubirArchivo(string rutaarchivo,string area)
        { 
            //SUBIENDO ARCHIVO
            var data = await OneDriveApi.UploadFile(rutaarchivo, "ficha-tecnica/"+area); //DECLARANDO CARPETA
            return data;
        }
        public async Task<OneDriveItem> SubirArchivoSPEC(string rutaarchivo, string area)
        {
            //SUBIENDO ARCHIVO
            var data = await OneDriveApi.UploadFile(rutaarchivo, "SPEC/" + area); //DECLARANDO CARPETA
            return data;
        }
        public async Task<OneDriveItem> SubirArchivoPOS(string rutaarchivo, string area)
        {
            //SUBIENDO ARCHIVO
            var data = await OneDriveApi.UploadFile(rutaarchivo, "POs/" + area); //DECLARANDO CARPETA
            return data;
        }
        //COMPARTIR ARCHIVOS
        public async Task<OneDrivePermission> Compartir(string ruta)
        {
            var data = await OneDriveApi.ShareItem(ruta, OneDriveLinkType.Edit); 
            return data;
        }

        public OneDriveApi Acceder()
        {
            return OneDriveApi;
        }

        public async Task<string> Ejecutar(string ruta,string nombrearea,string archivo)
        {
            string rpt = string.Empty;
            try
            {
                var data = this.SubirArchivo(ruta, nombrearea);
                //RUTA Y NOMBRE DEL ARCHIVO CREADO 
                string rutanombrearchivo = "ficha-tecnica/" + nombrearea + "/" + archivo;
                var datac = await this.Compartir(rutanombrearchivo);

                string json = datac.OriginalJson;
                var jsonParse = JsonConvert.DeserializeObject<OneDriveApiEntidadMaster>(json);

                rpt = jsonParse.link.webUrl;
                
            }catch{
                rpt  = "error";
            }

            return rpt;
        }
        //MOLDES
        public async Task<OneDriveItem> SubirArchivoMoldes(string rutaarchivo)
        {
            //SUBIENDO ARCHIVO
            var data = await OneDriveApi.UploadFile(rutaarchivo, "moldes" ); //DECLARANDO CARPETA
            return data;
        }

        public async Task<string> EjecutarMoldes(string ruta, string archivo)
        {
            string rpt = string.Empty;
            try
            {
                var data = this.SubirArchivoMoldes(ruta);
                //RUTA Y NOMBRE DEL ARCHIVO CREADO 
                string rutanombrearchivo = "moldes/"  + archivo;
                var datac = await this.Compartir(rutanombrearchivo);

                string json = datac.OriginalJson;
                var jsonParse = JsonConvert.DeserializeObject<OneDriveApiEntidadMaster>(json);

                rpt = jsonParse.link.webUrl;

            }
            catch
            {
                rpt = "error";
            }

            return rpt;
        }

    }
}