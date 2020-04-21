using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using System.Data;
using TSC_WEB.Models.Entidades.OperacionesProceso.GuiaEntrada;
using Oracle.ManagedDataAccess.Client;

namespace TSC_WEB.Models.Modelos.OperacionesProceso.GuiaEntrada
{
    public class GuiaEntradaModelo
    {
        DBAccess conexion = new DBAccess();
        public List<GuiaEntradaEntidad> GetListaAperturaGuia(int numero, string serie, string ruc)
        {
            var List = new List<GuiaEntradaEntidad>();
            OracleCommand comando = new OracleCommand("USYSTEX.SPGET_BUSCA_GUIA_ENTRADA",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("P_NUMERO_GUIA",numero));
            comando.Parameters.Add(new OracleParameter("P_SERIE_GUIA", serie));
            comando.Parameters.Add(new OracleParameter("P_RUC_GUIA", ruc));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {
                List.Add(new GuiaEntradaEntidad()
                {
                    Codigo_Operacion = Convert.ToInt32(registros["NATOPER_NAT_OPER"].ToString()),
                    Descripcion_Operacion = registros["DESCR_NAT_OPER"].ToString(),
                    C_Codigo_Prov9 =  Convert.ToInt32(registros["CGC_CLI_FOR_9"].ToString()),
                    Descripcion_Emisor = registros["NOME_FORNECEDOR"].ToString(),
                    Codigo_Transaccion =  Convert.ToInt32(registros["CODIGO_TRANSACAO"].ToString()),
                    Fecha_Transaccion = registros["DATA_TRANSACAO"].ToString(),
                    Fecha_Emision = registros["DATA_EMISSAO"].ToString(),
                    Estado_Entrada =  Convert.ToInt32(registros["SITUACAO_ENTRADA"].ToString()),
                    Descripcion_Estado_Entrada = registros["SITUACION"].ToString(),
                   
                });
            }
            conexion.Desconectar();
            return List;
        }

        public int BuscaGuiaEntrada(int numero, string serie, string ruc)
        {
            DataTable dt = new DataTable();
            OracleCommand comando = new OracleCommand("USYSTEX.SPGET_VALIDA_PERIODO_GUIA", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("P_NUMERO_GUIA",numero));
            comando.Parameters.Add(new OracleParameter("P_SERIE_GUIA", serie));
            comando.Parameters.Add(new OracleParameter("P_RUC_GUIA", ruc));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            dt.Load(comando.ExecuteReader());
            string respuesta = dt.Rows[0][0].ToString();

            if(respuesta == string.Empty || respuesta == null ){
                respuesta = "3";
            }
            conexion.Desconectar();
            return  Convert.ToInt16(respuesta);
        }

        public int ValidaPermiso(int numero, string serie, string ruc, string usuario)
        {
            DataTable dt = new DataTable();
            OracleCommand comando = new OracleCommand("USYSTEX.SPGET_VALIDA_PERMISOS", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("P_NUMERO_GUIA_V",numero));
            comando.Parameters.Add(new OracleParameter("P_SERIE_GUIA_V", serie));
            comando.Parameters.Add(new OracleParameter("P_RUC_GUIA_V", ruc));
            comando.Parameters.Add(new OracleParameter("P_USUARIO_ERP_V", usuario));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            dt.Load(comando.ExecuteReader());
            int respuesta = Convert.ToInt32(dt.Rows[0][0]);
            conexion.Desconectar();
            return respuesta;
        }

        //APERTURA
        public bool GetAperturaGuia(int numero, string serie, string ruc, string usuario)
        {
            OracleCommand comando = new OracleCommand("USYSTEX.UPDATE_APERTURA_GUIA_UP", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("V_DOCUMENTO",numero));
            comando.Parameters.Add(new OracleParameter("V_SERIE", serie));
            comando.Parameters.Add(new OracleParameter("V_RUC", ruc));
            comando.Parameters.Add(new OracleParameter("V_USUARIO_MODIFICA", usuario));
            comando.ExecuteNonQuery();
            conexion.Desconectar();
            return true;
        }
    }
}