using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.OperacionesProceso.CambioTaller;
using TSC_WEB.Models.Modelos.Sistema;
using System.Data;
using System.Net.Mail;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace TSC_WEB.Models.Modelos.OperacionesProceso.CambioTaller
{
    public class CambioTallerModelo
    {
        DBAccess conexion = new DBAccess();
        CorreosModelo objCorreosM = new CorreosModelo();
        //LISTA 
        public List<CambioTallerEntidad> Listar(int cod_ficha)
        {
            //LISTA
            List<CambioTallerEntidad> CambioTallerLista = new List<CambioTallerEntidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.GETSYS_CAMBIOTALLER",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("COD_FICHA",cod_ficha));
            comando.Parameters.Add(new OracleParameter("PCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                CambioTallerEntidad objTallerE = new CambioTallerEntidad();
                objTallerE.Codigo_Cambio_Taller = Convert.ToInt32(registros["P_COD_CAMB_TALLER"].ToString());
                objTallerE.Codigo_Ficha = Convert.ToInt32(registros["P_COD_FICHA"].ToString());
                objTallerE.Cantidad_Ficha = registros["P_CANT_FICHA"].ToString();
                objTallerE.Codigo_Taller_Original = registros["P_COD_TALLER_ORG"].ToString();
                objTallerE.Codigo_Taller_Destino = registros["P_COD_TALLER_DES"].ToString();
                objTallerE.Codigo_Motivo = registros["P_COD_MOTIVO"].ToString();
                objTallerE.Estado_Aprobacion = registros["P_EST_APROB"].ToString();
                objTallerE.Fecha_Creacion = registros["P_FEC_CREACION"].ToString();
                objTallerE.Nombre_Motivo_Rechazo = registros["MOTIVO_RECHAZO"].ToString();
                objTallerE.Fecha_Modificacion = registros["P_FEC_MODIFICA"].ToString();

                CambioTallerLista.Add(objTallerE);
            }
            conexion.Desconectar();

            return CambioTallerLista;
        }
        //VALIDA SI EXISTE FICHA
        public int ExisteFicha(CambioTallerEntidad objFile)
        {
            DataTable dt = new DataTable();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.GET_VALIDA_EXISTE_FICHA",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("cod_ficha", objFile.Codigo_Ficha));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            dt.Load(comando.ExecuteReader());
            conexion.Desconectar();
            int respuesta = dt.Rows.Count;
            return respuesta;
        }
        //VALIDA FICHA - TALLER DESTINO
        public int ListaFicha(CambioTallerEntidad objFile)
        {
            DataTable dt = new DataTable();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.GETSYS_VERIFICA_CAMBIOTALLER", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("cod_ficha", objFile.Codigo_Ficha));
            comando.Parameters.Add(new OracleParameter("cod_taller_des", objFile.Codigo_Taller_Destino));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            dt.Load(comando.ExecuteReader());
            conexion.Desconectar();
            int respuesta = dt.Rows.Count;
            return respuesta;
        }
        //ENVIAR CORREO
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
                mail.Subject = "Solicitud Aprobación Cambio de Taller";
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
        //REGISTRA LA SOLICITUD DE CAMBIO DE TALLER
        public bool GetCambioTaller(CambioTallerEntidad obj)
        {
            OracleCommand comando = new OracleCommand("USYSTEX.INSERTAR_CAMBIO_TALLER",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("p_cod_ficha", obj.Codigo_Ficha));
                comando.Parameters.Add(new OracleParameter("p_cod_taller_des", obj.Codigo_Taller_Destino));
                comando.Parameters.Add(new OracleParameter("p_cod_motivo", obj.Codigo_Motivo));
                comando.Parameters.Add(new OracleParameter("p_responsable", obj.Responsable));
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
        //ELIMINA SOLICITUD
        public bool EliminarSolicitud(int id)
        {
            OracleCommand comando = new OracleCommand("USYSTEX.ELIMINA_CAMBIO_TALLER",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("COD_CAMB_TALLER",id));
                comando.ExecuteNonQuery();
                return true;
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
    }
}