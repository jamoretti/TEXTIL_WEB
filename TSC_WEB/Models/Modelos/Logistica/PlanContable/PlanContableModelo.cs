using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Logistica.PlanContable;
using TSC_WEB.Models.Entidades.Sistema;

namespace TSC_WEB.Models.Modelos.Logistica.PlanContable
{
    public class PlanContableModelo
    {
        NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
        DBAccess conexion = new DBAccess();


        //LISTA TODAS LA OC QUE PUEDEN PARA SER ALTERADAS DE SITUACION
        public List<ListaPlanContable> ListaPlanContable(int codperiodo, int codgerencia)
        {
            List<ListaPlanContable> lista = new List<ListaPlanContable>();
            OracleCommand comando = new OracleCommand("SPU_GET_PLANCONTABLE", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("IP_PERIODO", codperiodo));
            comando.Parameters.Add(new OracleParameter("IP_GERENCIA", codgerencia));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                lista.Add(new ListaPlanContable
                {
                    CODPERIODO = registros["CODPERIODO"].ToString(),
                    PERIODO = registros["PERIODO"].ToString(),
                    CODCC = registros["CODCC"].ToString(),
                    CC = registros["CC"].ToString(),
                    CODPLAN = registros["CODPLAN"].ToString(),
                    PLANCONT = registros["PLANCONT"].ToString(),
                    SIMBOLO = registros["SIMBOLO"].ToString(),
                    PRESUPUESTO = Convert.ToDecimal(registros["PRESUPUESTO"].ToString()).ToString("N", nfi),
                    CONSUMIDO = Convert.ToDecimal(registros["CONSUMIDO"].ToString()).ToString("N", nfi)
                });
            }


            conexion.Desconectar();
            return lista;
        }


        public RespuestaOperacion RegistrarConsumo( int periodo, 
                                                    int codcc, 
                                                    int codplan, 
                                                    decimal consumo, 
                                                    string usuario)
        {
            RespuestaOperacion respuesta = new RespuestaOperacion();
            try
            {
                OracleCommand comando = new OracleCommand("SPU_SET_PLANCONTABLE", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("IP_PERIODO", periodo));
                comando.Parameters.Add(new OracleParameter("IP_CC", codcc));
                comando.Parameters.Add(new OracleParameter("IP_CODPLAN  ", codplan));
                comando.Parameters.Add(new OracleParameter("IP_CONSUMO", consumo));
                comando.Parameters.Add(new OracleParameter("CHP_USUARIO", usuario));

                comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

                OracleDataReader registros = comando.ExecuteReader();

                if (registros.HasRows)
                {
                    while (registros.Read())
                    {
                        respuesta.id_status = Convert.ToInt32(registros["ID_STATUS"]);
                        respuesta.desc_status = registros["DESC_STATUS"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                respuesta.id_status = 0;
                respuesta.desc_status = e.Message;
            }

            conexion.Desconectar();
            return respuesta;
        }



    }
}