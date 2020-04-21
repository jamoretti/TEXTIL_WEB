using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Logistica.AprobacionExcedentes;
using TSC_WEB.Models.Entidades.Sistema;

namespace TSC_WEB.Models.Modelos.Logistica.AprobacionExcedentes
{
    public class AprobExcOcModelo
    {
        NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
        DBAccess conexion = new DBAccess();

        public List<ListaPendiente> ListarOcExcentePendientes(int codgerencia, int codperiodo, string usuario)
        {
            List<ListaPendiente> objListar1 = new List<ListaPendiente>();

            OracleCommand comando = new OracleCommand("SPU_GET_OC_EXCEDENTE", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("IP_OPCION", 1));
            comando.Parameters.Add(new OracleParameter("P_I_CODGERENCIA", codgerencia));
            comando.Parameters.Add(new OracleParameter("P_I_CODPERIODO", codperiodo));
            comando.Parameters.Add(new OracleParameter("P_I_CODCC", null));
            comando.Parameters.Add(new OracleParameter("P_I_PEDIDOCOMPRA", null));
            comando.Parameters.Add(new OracleParameter("P_CH_USUARIO", usuario));

            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objListar1.Add(
                            new ListaPendiente
                            {
                                CODAUTORIZA = registros["CODAUTORIZA"].ToString(),
                                DISPONIBLE = Convert.ToDecimal(registros["DISPONIBLE"].ToString()).ToString("N", nfi),
                                PEDIDO = Convert.ToInt32(registros["PEDIDO"].ToString()),
                                FECHA = Convert.ToDateTime(registros["FECHA"].ToString()).ToShortDateString(),
                                DESCRIPCIONPAGO = registros["DESCRIPCIONPAGO"].ToString(),
                                TOTAL_PEDIDO = Convert.ToDecimal(registros["TOTAL_PEDIDO"].ToString()).ToString("N", nfi),
                                PROVEEDOR = registros["PROVEEDOR"].ToString(),
                                COMPRADOR = registros["COMPRADOR"].ToString(),
                                CENTROCOSTO = registros["CENTROCOSTO"].ToString(),
                                SIMBOLO_MOEDA = registros["SIMBOLO_MOEDA"].ToString(),
                                CH_MOTIVO = registros["CH_MOTIVO"].ToString()
                            }
                        );
            }

            conexion.Desconectar();
            return objListar1;
        }
        public RespuestaOperacion AprobacionRechazo(int periodo, int ordencompra, string usuario, int estado, string observacion)
        {
            RespuestaOperacion respuesta = new RespuestaOperacion();
            try
            {
                OracleCommand comando = new OracleCommand("SPU_SET_OC_EXCEDENTE_APROB", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("P_I_CODPERIODO", periodo));
                comando.Parameters.Add(new OracleParameter("P_I_ORDENCOMPRA", ordencompra));
                comando.Parameters.Add(new OracleParameter("P_CH_USUARIO", usuario));
                comando.Parameters.Add(new OracleParameter("P_I_ESTADO", estado));
                comando.Parameters.Add(new OracleParameter("P_CH_OBS_APROB", observacion));

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
        public List<ListaAprobados> ListarAprobados(int codperiodo, string usuario)
        {
            List<ListaAprobados> objListar1 = new List<ListaAprobados>();
            decimal total = 0;

            OracleCommand comando = new OracleCommand("SPU_GET_OC_EXCEDENTE", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("IP_OPCION", 2));
            comando.Parameters.Add(new OracleParameter("P_I_CODGERENCIA", 1));
            comando.Parameters.Add(new OracleParameter("P_I_CODPERIODO", codperiodo));
            comando.Parameters.Add(new OracleParameter("P_I_CODCC", null));
            comando.Parameters.Add(new OracleParameter("P_I_PEDIDOCOMPRA", null));
            comando.Parameters.Add(new OracleParameter("P_CH_USUARIO", usuario));

            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objListar1.Add(
                            new ListaAprobados
                            {
                                CODAUTORIZA = registros["CODAUTORIZA"].ToString(),
                                DISPONIBLE = Convert.ToDecimal(registros["DISPONIBLE"].ToString()).ToString("N", nfi),
                                PEDIDO = Convert.ToInt32(registros["PEDIDO"].ToString()),
                                FECHA = Convert.ToDateTime(registros["FECHA"].ToString()).ToShortDateString(),
                                DESCRIPCIONPAGO = registros["DESCRIPCIONPAGO"].ToString(),
                                TOTAL_PEDIDO = Convert.ToDecimal(registros["TOTAL_PEDIDO"].ToString()).ToString("N", nfi),
                                PROVEEDOR = registros["PROVEEDOR"].ToString(),
                                COMPRADOR = registros["COMPRADOR"].ToString(),
                                CENTROCOSTO = registros["CENTROCOSTO"].ToString(),
                                SIMBOLO_MOEDA = registros["SIMBOLO_MOEDA"].ToString(),

                                FECHA_APROB = Convert.ToDateTime(registros["FECHA_APROB"].ToString()).ToShortDateString(),
                                USUARIO_APROB = registros["USUARIO_APROB"].ToString(),
                                OBS_APROB = registros["OBS_APROB"].ToString(),

                                VALOR_DOLAR = Convert.ToDecimal(registros["VALOR_DOLAR"]),
                                CODCANCEL = Convert.ToInt32(registros["CODCANCEL"].ToString())

                            });

                total += Convert.ToDecimal(registros["TOTAL_PEDIDO"].ToString());
            }

            foreach (var item in objListar1)
            {
                item.TOTAL_PEDIDO_V = total.ToString("N", nfi);
            }

            conexion.Desconectar();
            return objListar1;
        }
        public List<ListaRechazados> ListarRechazados(int codperiodo, string usuario)
        {
            List<ListaRechazados> objListar1 = new List<ListaRechazados>();
            decimal total = 0;

            OracleCommand comando = new OracleCommand("SPU_GET_OC_EXCEDENTE", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("IP_OPCION", 3));
            comando.Parameters.Add(new OracleParameter("P_I_CODGERENCIA", 1));
            comando.Parameters.Add(new OracleParameter("P_I_CODPERIODO", codperiodo));
            comando.Parameters.Add(new OracleParameter("P_I_CODCC", null));
            comando.Parameters.Add(new OracleParameter("P_I_PEDIDOCOMPRA", null));
            comando.Parameters.Add(new OracleParameter("P_CH_USUARIO", usuario));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objListar1.Add(
                            new ListaRechazados
                            {
                                CODAUTORIZA = registros["CODAUTORIZA"].ToString(),
                                DISPONIBLE = Convert.ToDecimal(registros["DISPONIBLE"].ToString()).ToString("N", nfi),
                                PEDIDO = Convert.ToInt32(registros["PEDIDO"].ToString()),
                                FECHA = Convert.ToDateTime(registros["FECHA"].ToString()).ToShortDateString(),
                                DESCRIPCIONPAGO = registros["DESCRIPCIONPAGO"].ToString(),
                                TOTAL_PEDIDO = Convert.ToDecimal(registros["TOTAL_PEDIDO"].ToString()).ToString("N", nfi),
                                PROVEEDOR = registros["PROVEEDOR"].ToString(),
                                COMPRADOR = registros["COMPRADOR"].ToString(),
                                CENTROCOSTO = registros["CENTROCOSTO"].ToString(),
                                SIMBOLO_MOEDA = registros["SIMBOLO_MOEDA"].ToString(),

                                FECHA_RECH = Convert.ToDateTime(registros["FECHA_RECH"].ToString()).ToShortDateString(),
                                USUARIO_RECH = registros["USUARIO_RECH"].ToString(),
                                OBS_RECH = registros["OBS_RECH"].ToString(),

                                VALOR_DOLAR = Convert.ToDecimal(registros["VALOR_DOLAR"]),
                                CODCANCEL = Convert.ToInt32(registros["CODCANCEL"].ToString())
                            });
                total += Convert.ToDecimal(registros["TOTAL_PEDIDO"].ToString());
            }


            foreach (var item in objListar1)
            {
                item.TOTAL_PEDIDO_V = total.ToString("N", nfi);
            }

            conexion.Desconectar();
            return objListar1;
        }

        public List<Informacion> InformacionExcedente(int periodo, int codgerencia, int codcc, int pedido)
        {
            List<Informacion> lista = new List<Informacion>();
            DBAccess conexion = new DBAccess();

            OracleCommand comando = new OracleCommand("SPU_GET_OC_EXCEDENTE", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("IP_OPCION", 4));
            comando.Parameters.Add(new OracleParameter("P_I_CODGERENCIA", codgerencia));
            comando.Parameters.Add(new OracleParameter("P_I_CODPERIODO", periodo));
            comando.Parameters.Add(new OracleParameter("P_I_CODCC", codcc));
            comando.Parameters.Add(new OracleParameter("P_I_PEDIDOCOMPRA", pedido));
            comando.Parameters.Add(new OracleParameter("P_CH_USUARIO", null));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader(CommandBehavior.SingleResult);

            if (registros.HasRows)
            {
                while (registros.Read())
                {
                    lista.Add(new Informacion
                    {
                        CUENTA = registros["CUENTA"].ToString(),
                        PRESUPUESTO = Convert.ToDecimal(registros["PRESUPUESTO"].ToString()).ToString("N", nfi),
                        CONSUMIDO = Convert.ToDecimal(registros["CONSUMIDO"].ToString()).ToString("N", nfi),

                        DISPONIBLE = Convert.ToDecimal(registros["DISPONIBLE"].ToString()).ToString("N", nfi),
                        VALOR_OC = Convert.ToDecimal(registros["VALOR_OC_C"].ToString()).ToString("N", nfi),

                        SIM_PRE = registros["SIM_PRE"].ToString(),
                        SIM_OC = registros["SIM_OC"].ToString(),

                        V_DISPONIBLE = Convert.ToDecimal(registros["DISPONIBLE"]),
                        V_VALOR_OC = Convert.ToDecimal(registros["VALOR_OC_C"])
                    });

                }
            }
            conexion.Desconectar();
            return lista;
        }

        public List<string> UsuariosPermitidos()
        {
            List<string> objListar1 = new List<string>();

            OracleCommand comando = new OracleCommand("SPU_GET_OC_EXCEDENTE", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("IP_OPCION", 5));
            comando.Parameters.Add(new OracleParameter("P_I_CODGERENCIA", null));
            comando.Parameters.Add(new OracleParameter("P_I_CODPERIODO", null));
            comando.Parameters.Add(new OracleParameter("P_I_CODCC", null));
            comando.Parameters.Add(new OracleParameter("P_I_PEDIDOCOMPRA", null));
            comando.Parameters.Add(new OracleParameter("P_CH_USUARIO", null));

            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objListar1.Add(registros["USUARIO"].ToString());
            }

            conexion.Desconectar();
            return objListar1;
        }

    }
}