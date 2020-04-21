using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using TSC_WEB.Config;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.OperacionesProceso.GuiaSalida;

namespace TSC_WEB.Models.Modelos.OperacionesProceso.GuiaSalida
{
    public class GuiaSalidaModelo
    {
        DBAccess conexion = new DBAccess();
        public List<GuiaSalidaEntidad> GetListaAperturaSalida(int empresa, string numero, string serie)
        {

            var List = new List<GuiaSalidaEntidad>();
            OracleCommand comando = new OracleCommand("USYSTEX.SPGET_BUSCA_GUIASALIDA",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("P_EMPRESA",empresa));
            comando.Parameters.Add(new OracleParameter("P_NUM_GUIA", numero));
            comando.Parameters.Add(new OracleParameter("P_SERIE_GUIA", serie));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros =  comando.ExecuteReader();
            while (registros.Read())
            {
                List.Add(new GuiaSalidaEntidad()
                {
                    NumeroDocumento =  registros["NUM_DOC"].ToString(),
                    Serie = registros["SERIE_DOC"].ToString(),
                    CodigoDestinatario = registros["COD_SERVICIO"].ToString(),
                    NombreDestinatario = registros["NOMBRE_SERVICIO"].ToString(),
                    TipoOperacion = registros["OPERACION"].ToString(),
                    NombreTipoOperacion = registros["DESCRIP_OPERACION"].ToString(),
                    FechaRecepcion = registros["FECHA_DOC"].ToString(),  
                    Especie = registros["ESPECIE"].ToString(), 
                    NombreSitDF = registros["SITUACION"].ToString(),
                });
            }
            conexion.Desconectar();
            return List;
        }


        public int BuscaGuiaSalida(string numero, string serie)
        {
            DataTable dt = new DataTable();
            OracleCommand comando = new OracleCommand("USYSTEX.SPGET_VALIDA_PERIODO_GUIA_S", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("P_NUMERO_GUIA",numero));
            comando.Parameters.Add(new OracleParameter("P_SERIE_GUIA", serie));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            dt.Load(comando.ExecuteReader());
            conexion.Desconectar();

            string respuesta = dt.Rows[0][0].ToString();
            if(respuesta == string.Empty || respuesta == null){
                respuesta = "3";
            }
            return Convert.ToInt16(respuesta);
        }

        public bool GetAperturaGuiaSalida(int empresa, string numero, string serie)
        {
            OracleCommand comando = new OracleCommand("USYSTEX.SPGET_APERTURA_GUIASALIDA", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("P_EMPRESA", empresa));
                comando.Parameters.Add(new OracleParameter("P_NUM_GUIA", numero));
                comando.Parameters.Add(new OracleParameter("P_SERIE_GUIA", serie));
                comando.ExecuteNonQuery();
                return true;
            }catch{
                return false;
            }
            finally
            {
                conexion.Desconectar();
            }

        }
    }
}