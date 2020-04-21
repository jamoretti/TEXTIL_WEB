using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Planeamiento;

namespace TSC_WEB.Models.Modelos.Planeamiento
{
    public class PedidosModelo
    {
        DBAccess conexion = new DBAccess();

        public List<PedidosEntidad> BuscarPedidos(string flag)
        {
            //LISTA
            List<PedidosEntidad> objPedidosL = new List<PedidosEntidad>();

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.sp_getpedidoambu", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();


            //FLAG
            if (flag == null)
            {
                comando.Parameters.Add(new OracleParameter("i_flag", DBNull.Value));
            }
            else
            {
                comando.Parameters.Add(new OracleParameter("i_flag", flag));
            }

            comando.Parameters.Add(new OracleParameter("o_cursor",OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while(registros.Read()){
                objPedidosL.Add(
                    new PedidosEntidad
                    {
                        pedido = registros["pedido"].ToString(),
                        //cliente = registros["cliente"].ToString(),
                        //flag = registros["flag"].ToString(),
                        //usuario = registros["usuario"].ToString(),
                        //fecha = registros["fecha"].ToString(),
                    }
                );
            }

            conexion.Desconectar();

            return objPedidosL;

        }
    }
}