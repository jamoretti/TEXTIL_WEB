using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using TSC_WEB.Config;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.OperacionesProceso.DesbloquearOS;

namespace TSC_WEB.Models.Modelos.OperacionesProceso.DesbloquearOs
{
    public class OrdenServicioModelo
    {
        DBAccess conexion = new DBAccess();

        public List<OrdenServicioEntidad> ListaOrdenServicio(OrdenServicioEntidad obj_orden)
        {
            var List = new List<OrdenServicioEntidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.GETSYS_LISTAORDEN",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("COD_ORDEN", obj_orden.Numero_Orden_String));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            //System.Data.DataTable dt = Conexion.ExecuteDataTable("SYSTEXTILRPT.GETSYS_LISTAORDEN");
            while (registros.Read())
            {
                List.Add(new OrdenServicioEntidad()
                {
                    Numero_Orden = Convert.ToInt32(registros["NUMERO_ORDEM"].ToString()), 
                    Situacion_Orden = Convert.ToInt32(registros["SITUACAO_ORDEM"].ToString()),
                    Codigo_Cancela = Convert.ToInt32(registros["COD_CANC_ORDEM"].ToString()),
                });
            }
            conexion.Desconectar();
            return List;
        }

        public int GetCambioOrdenServicio(int orden_servicio, int sit)
        {
            OracleCommand comando = new OracleCommand("USYSTEX.ACTUALIZA_ORDEN",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("COD_ORDEN",orden_servicio));
            comando.Parameters.Add(new OracleParameter("SITUACION", sit));
            comando.ExecuteNonQuery();
            conexion.Desconectar();
            return 3;
        }

        public int GetRegistroLog(LogOrdenServicioEntidad objLog)
        {
            OracleCommand comando = new OracleCommand("USYSTEX.INSERTAR_LOG_HABILITA_ORDEN", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("P_OPCION", objLog.Opcion));
            comando.Parameters.Add(new OracleParameter("COD_PROCESO", objLog.Proceso));
            comando.Parameters.Add(new OracleParameter("ORDEN_SERVICIO", objLog.Numero_Orden));
            comando.Parameters.Add(new OracleParameter("ESTADO_INICIAL", objLog.Estado_Inicial));
            comando.Parameters.Add(new OracleParameter("RESPONSABLE", objLog.Responsable));
            comando.Parameters.Add(new OracleParameter("ESTADO_CAMBIO", objLog.Estado_cambio));
            comando.Parameters.Add(new OracleParameter("ESTADO_FINAL", objLog.Estado_final));
            comando.Parameters.Add(new OracleParameter("FECHA_FINAL", objLog.Fecha_final));
            comando.Parameters.Add(new OracleParameter("FLAG_REGRESA", objLog.Flag_regresa));
            comando.Parameters.Add(new OracleParameter("PC_USUARIO", objLog.Pc_usuario));
            comando.ExecuteNonQuery();
            conexion.Desconectar();
            return 3;
        }

    }
}