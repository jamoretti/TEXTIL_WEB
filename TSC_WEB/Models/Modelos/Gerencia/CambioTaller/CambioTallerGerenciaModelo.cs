using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.OperacionesProceso.CambioTaller;
using TSC_WEB.Models.Entidades.Gerencia.CambioTaller;
using System.Data;
using System.Net.Mail;
using TSC_WEB.Models.Modelos.Sistema;
using Oracle.ManagedDataAccess.Client;


namespace TSC_WEB.Models.Modelos.Gerencia.CambioTaller
{
    public class CambioTallerGerenciaModelo
    {
        DBAccess conexion = new DBAccess();
        CorreosModelo objCorreosM = new CorreosModelo();

        public List<CambioTallerEntidad> Listar()
        {
            //LISTA
            List<CambioTallerEntidad> CambioTallerLista = new List<CambioTallerEntidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.GETSYS_LISTA_CAMBIO_TALLER", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("COD_FICHA", '0'));
            comando.Parameters.Add(new OracleParameter("PCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                CambioTallerEntidad objTallerE = new CambioTallerEntidad();
                objTallerE.Codigo_Cambio_Taller = Convert.ToInt32(registros["P_COD_CAMB_TALLER"].ToString());
                objTallerE.Codigo_Ficha = Convert.ToInt32(registros["P_COD_FICHA"].ToString());
                objTallerE.Codigo_Taller_Original = registros["P_COD_TALLER_ORG"].ToString();
                objTallerE.Codigo_Taller_Destino = registros["P_COD_TALLER_DES"].ToString();
                objTallerE.Nombre_Taller_Original = registros["P_NOM_TALLER_ORG"].ToString();
                objTallerE.Nombre_Taller_Destino = registros["P_NOM_TALLER_DES"].ToString();
                objTallerE.Codigo_Motivo = registros["P_COD_MOTIVO"].ToString();
                objTallerE.Responsable = registros["P_RESPONSABLE"].ToString();
                objTallerE.Estado_Aprobacion = registros["P_EST_APROB"].ToString();

                CambioTallerLista.Add(objTallerE);
            }
            conexion.Desconectar();

            return CambioTallerLista;
        }

        //MOTIVOS DE RECHAZOS
        public List<MotivoRechazoEntidad> ListarMotivoRechazo()
        {
            //LISTA
            List<MotivoRechazoEntidad> MotivoRechazoLista = new List<MotivoRechazoEntidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.GETSYS_MOTIVOS_RECHAZO", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("PCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                MotivoRechazoEntidad objMotivoE = new MotivoRechazoEntidad();
                objMotivoE.CODIGO_MOTIVO_RECHAZO = Convert.ToInt32(registros["CODIGO_MOTIVO_RECHAZO"].ToString());
                objMotivoE.NOMBRE_MOTIVO_RECHAZO = registros["NOMBRE_MOTIVO_RECHAZO"].ToString();

                MotivoRechazoLista.Add(objMotivoE);
            }
            conexion.Desconectar();

            return MotivoRechazoLista;
        }
        //EJECUTA LA APROBACION O DESAPROBACION DE CAMBIO DE TALLER
        public bool GetUpdateCambioTaller(CambioTallerEntidad obj)
        {
            OracleCommand comando = new OracleCommand("USYSTEX.UPDATE_CAMBIO_TALLER", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("p_cod_camb_taller", obj.Codigo_Cambio_Taller));
                comando.Parameters.Add(new OracleParameter("p_cod_motivo_r", obj.Codigo_Motivo_Rechazo));
                comando.Parameters.Add(new OracleParameter("p_est_aprob", obj.Estado_Aprobacion));
                comando.Parameters.Add(new OracleParameter("p_usuario_modifica", obj.Usuario_Modificacion));
                comando.ExecuteNonQuery();
                return true;
                //return comando.ExecuteNonQuery() > 0 ? true : false;
            }
            catch
            {
                return false;
            }
            finally
            {
                conexion.Desconectar();
            }
        }
        //ENVIA CORREO
        public void EnviarCorreo(int n_ficha, string mensaje)
        {
            try
            {
                var Listacorreo = objCorreosM.ListCorreo("E", "CAMBIOTALLER");
                var mail = new MailMessage();
                foreach (var item in Listacorreo)
                {
                    mail.To.Add(new MailAddress(item.correo + item.dominio, item.usuario));
                }
                mail.Subject = "Aprobación Cambio de Taller";
                mail.Body = mensaje;
                mail.IsBodyHtml = false;
                SmtpClient client = new SmtpClient();
                var listaemisor = objCorreosM.ListCorreo("S", "CAMBIOTALLER");
                foreach (var item in listaemisor)
                {
                    mail.From = new MailAddress(item.correo + item.dominio, " " + item.usuario);
                    client.Credentials = new System.Net.NetworkCredential(item.correo + item.dominio, item.password);
                }
                client.Port = 587;
                client.Host = "outlook.office365.com";
                client.EnableSsl = true;
                client.Send(mail);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}