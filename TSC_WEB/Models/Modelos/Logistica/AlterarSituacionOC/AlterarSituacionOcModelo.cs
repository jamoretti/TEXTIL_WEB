using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.Logistica.AlterarSituacion;
using System.Data;
using TSC_WEB.Models.Entidades.Sistema;

namespace TSC_WEB.Models.Modelos.Logistica.AlterarSituacionOC
{
    public class AlterarSituacionOcModelo
    {
        NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

        DBAccess conexion = null;

        public AlterarSituacionOcModelo()
        {
            conexion = new DBAccess();
        }

        //LISTA TODAS LA OC QUE PUEDEN PARA SER ALTERADAS DE SITUACION
        public List<ListaAlterarSituacionCab> ListarOcAlterarSituacion(int codperiodo, int codgerencia)
        {
            List<ListaAlterarSituacionCab> lista = new List<ListaAlterarSituacionCab>();

            OracleCommand comando = new OracleCommand("SPU_GET_OC_ALTERSITUACION", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("I_OPCION", "1"));
            comando.Parameters.Add(new OracleParameter("I_CH_FILTRO_1", "0"));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_1", codperiodo));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_2", codgerencia));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_3", 1));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                lista.Add(
                    new ListaAlterarSituacionCab
                    {
                        CODAUTORIZA = registros["CODAUTORIZA"].ToString(),
                        DISPONIBLE = Convert.ToDecimal(registros["DISPONIBLE"].ToString()).ToString("N", nfi),
                        PEDIDO = Convert.ToInt32(registros["PEDIDO"].ToString()),
                        FECHA = Convert.ToDateTime(registros["FECHA"].ToString()).ToShortDateString(),
                        DESCRIPCIONPAGO = registros["DESCRIPCIONPAGO"].ToString(),
                        MONEDA = registros["MONEDA"].ToString(),
                        TOTAL_PEDIDO = Convert.ToDecimal(registros["TOTAL_PEDIDO"].ToString()).ToString("N", nfi),
                        PROVEEDOR = registros["PROVEEDOR"].ToString(),
                        COMPRADOR = registros["COMPRADOR"].ToString(),
                        CENTROCOSTO = registros["CENTROCOSTO"].ToString(),
                        SIMBOLO_MOEDA = registros["SIMBOLO_MOEDA"].ToString(),
                        vDisponible = Convert.ToDecimal(registros["DISPONIBLE"]),
                        vTotalPedido = Convert.ToDecimal(registros["TOTAL_PEDIDO_V"])
                    }
                );
            }
            conexion.Desconectar();
            return lista;
        }


        //LISTA DETALLE OC - ALTERAR SITUACION.
        public List<ListaAlterarSituacionDet> ListarDetalleOCAlter(int ordencompra)
        {
            List<ListaAlterarSituacionDet> lista = new List<ListaAlterarSituacionDet>();

            OracleCommand comando = new OracleCommand("SPU_GET_OC_ALTERSITUACION", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("I_OPCION", "2"));
            comando.Parameters.Add(new OracleParameter("I_CH_FILTRO_1", ""));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_1", ordencompra));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_2", 1));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_3", 1));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                lista.Add(
                      new ListaAlterarSituacionDet
                      {
                          seq_item_pedido = registros["seq_item_pedido"].ToString(),
                          ITEM_100_NIVEL99 = registros["ITEM_100_NIVEL99"].ToString(),
                          item_100_grupo = registros["item_100_grupo"].ToString(),
                          item_100_subgrupo = registros["item_100_subgrupo"].ToString(),
                          item_100_item = registros["item_100_item"].ToString(),
                          descricao_item = registros["descricao_item"].ToString(),
                          centro_custo = registros["centro_custo"].ToString(),
                          unidade_conv = registros["unidade_conv"].ToString(),
                          reservaplane = registros["reservaplane"].ToString(),
                          QTDE_PEDIDA_ITEM = Convert.ToDecimal(registros["QTDE_PEDIDA_ITEM"].ToString()).ToString("N", nfi),
                          VALOR_CONV = registros["VALOR_CONV"].ToString(),
                          total = Convert.ToDecimal(registros["total"].ToString()).ToString("N", nfi),
                          DESCRICAO = registros["DESCRICAO"].ToString(),
                      }
                );
            }
            conexion.Desconectar();
            return lista;
        }


        //  LISTAR OC COMBOBOX GERENCIAS
        public List<ListarOCGerencias> ListarCbxGerencias()
        {
            List<ListarOCGerencias> listarOCGerencias = new List<ListarOCGerencias>();

            OracleCommand comando = new OracleCommand("SPU_GET_OC_ALTERSITUACION", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("I_OPCION", "3"));
            comando.Parameters.Add(new OracleParameter("I_CH_FILTRO_1", "0"));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_1", 1));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_2", 1));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_3", 1));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            if (registros.HasRows)
            {
                while (registros.Read())
                {
                    listarOCGerencias.Add(
                          new ListarOCGerencias
                          {
                              CodGerencia = registros["COD_GER"].ToString(),
                              DescGerencia = registros["DESC_GER"].ToString(),
                          }
                    );
                }
            }
            conexion.Desconectar();
            return listarOCGerencias;
        }

        public List<ListaOCPeriodo> ListarCbxPeriodo()
        {
            List<ListaOCPeriodo> lista = new List<ListaOCPeriodo>();

            OracleCommand comando = new OracleCommand("SPU_GET_OC_ALTERSITUACION", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("I_OPCION", "5"));
            comando.Parameters.Add(new OracleParameter("I_CH_FILTRO_1", ""));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_1", 1));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_2", 1));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_3", 1));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            if (registros.HasRows)
            {
                while (registros.Read())
                {
                    lista.Add(
                          new ListaOCPeriodo
                          {
                              COD_PERIODO = Convert.ToInt32(registros["COD_PERIODO"].ToString()),
                              DESC_PERIODO = registros["DESC_PERIODO"].ToString(),
                          }
                    );
                }
            }
            conexion.Desconectar();
            return lista;
        }

        //  LISTAR RESUMEN PRESUPUSETO POR CENTRO DE COSTO.
        public ResumenPresupuesto ListarResumen(int codgerencia, int codperiodo)
        {
            ResumenPresupuesto resumen = new ResumenPresupuesto();
            DBAccess conexion = new DBAccess();

            OracleCommand comando = new OracleCommand("SPU_GET_OC_ALTERSITUACION", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("I_OPCION", "6"));
            comando.Parameters.Add(new OracleParameter("I_CH_FILTRO_1", ""));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_1", codgerencia));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_2", codperiodo));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_3", 1));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader(CommandBehavior.SingleResult);

            if (registros.HasRows)
            {
                if (registros.Read())
                {
                    resumen = new ResumenPresupuesto();
                    resumen.Presupuesto = Convert.ToDecimal(registros["PRESUPUESTO"].ToString()).ToString("N", nfi);
                    resumen.Consumido = Convert.ToDecimal(registros["CONSUMIDO"].ToString()).ToString("N", nfi);
                    resumen.Disponible = Convert.ToDecimal(registros["DISPONIBLE"].ToString()).ToString("N", nfi);
                    resumen.Simbolo = registros["SIMBOLO"].ToString();
                }
            }
            conexion.Desconectar();
            return resumen;
        }


        //ALTERERAR SITUACION OC 
        public RespuestaOperacion AlterarSituacionOc(int ordencompra, string usuario, int periodo)
        {
            RespuestaOperacion respuesta = new RespuestaOperacion();
            DBAccess con = new DBAccess();
            try
            {
                OracleCommand comando = new OracleCommand("SPU_SET_OC_ALTERSITUACION", con.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                con.Conectar();

                comando.Parameters.Add(new OracleParameter("P_I_ORDENCOMPRA", ordencompra));
                comando.Parameters.Add(new OracleParameter("P_CH_USUARIO", usuario));
                comando.Parameters.Add(new OracleParameter("P_I_CODPERIODO", periodo));
                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

                OracleDataReader registros = comando.ExecuteReader();

                if (registros.HasRows)
                {
                    while (registros.Read())
                    {
                        respuesta.id_status = Convert.ToInt32(registros["id_status"]);
                        respuesta.desc_status = registros["desc_status"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                respuesta.id_status = 0;
                respuesta.desc_status = e.Message;
            }

            con.Desconectar();
            return respuesta;
        }

        // 
        public RespuestaOperacion AlterSituacionValidacion(int ordencompra, int periodo)
        {
            RespuestaOperacion respuesta = new RespuestaOperacion();
            DBAccess con = new DBAccess();
            try
            {
                OracleCommand comando = new OracleCommand("SPU_GET_ALTERSITUACION_VALID", con.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                con.Conectar();

                comando.Parameters.Add(new OracleParameter("IP_PEDIDOCOMPRA", ordencompra));
                comando.Parameters.Add(new OracleParameter("IP_CODPERIODO", periodo));
                comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

                OracleDataReader registros = comando.ExecuteReader();

                if (registros.HasRows)
                {
                    while (registros.Read())
                    {
                        respuesta.id_status = Convert.ToInt32(registros["ID_RESPUESTA"]);
                        respuesta.desc_status = registros["DESC_RESPUESTA"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                respuesta.id_status = 0;
                respuesta.desc_status = e.Message;
            }

            con.Desconectar();
            return respuesta;
        }


        public RespuestaOperacion RegistrarSoliExc(int ordencompra, string usuario, string motivo)
        {
            RespuestaOperacion respuesta = new RespuestaOperacion();
            DBAccess conexion = new DBAccess();
            try
            {
                OracleCommand comando = new OracleCommand("SPU_SET_OC_EXCEDENTE", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("P_I_ORDENCOMPRA", ordencompra));
                comando.Parameters.Add(new OracleParameter("P_CH_USUARIO", usuario));
                comando.Parameters.Add(new OracleParameter("P_CH_MOTIVO", motivo));
                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

                OracleDataReader registros = comando.ExecuteReader();

                if (registros.HasRows)
                {
                    while (registros.Read())
                    {
                        respuesta.id_status = Convert.ToInt32(registros["id_status"]);
                        respuesta.desc_status = registros["desc_status"].ToString();
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


        public RespuestaOperacion OC_EnviarEmail(int ordencompra, string usuario, string motivo)
        {
            RespuestaOperacion respuesta = new RespuestaOperacion();
            DBAccess conexion = new DBAccess();
            try
            {
                OracleCommand comando = new OracleCommand("SPU_OC_EXCEDENTE_EMAIL", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("P_I_ORDENCOMPRA", ordencompra));
                comando.Parameters.Add(new OracleParameter("P_CH_USUARIO", usuario));
                comando.Parameters.Add(new OracleParameter("P_CH_MOTIVO", motivo));
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


        public List<ListaDispXCuenta> ListarPlanContDetalle(int codpedcompra,
                                                       int codperiodo,
                                                       int codcc)
        {
            List<ListaDispXCuenta> lista = new List<ListaDispXCuenta>();
            DBAccess conexion = new DBAccess();

            OracleCommand comando = new OracleCommand("SPU_GET_OC_ALTERSITUACION", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("I_OPCION", "8"));
            comando.Parameters.Add(new OracleParameter("I_CH_FILTRO_1", "0"));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_1", codpedcompra));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_2", codperiodo));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_3", codcc));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader(CommandBehavior.SingleResult);

            if (registros.HasRows)
            {
                while(registros.Read())
                {
                    lista.Add(new ListaDispXCuenta
                    {
                        CUENTA = registros["CUENTA"].ToString(),
                        PRESUPUESTO = Convert.ToDecimal(registros["PRESUPUESTO"].ToString()).ToString("N", nfi),
                        CONSUMIDO = Convert.ToDecimal(registros["CONSUMIDO"].ToString()).ToString("N", nfi),
                        DISPONIBLE = Convert.ToDecimal(registros["DISPONIBLE"].ToString()).ToString("N", nfi),
                        VALOR_OC = Convert.ToDecimal(registros["VALOR_OC_C"].ToString()).ToString("N", nfi),
                        SIM_PRE = registros["SIM_PRE"].ToString(),
                        SIM_OC = registros["SIM_OC"].ToString()
                    });

                }
            }
            conexion.Desconectar();
            return lista;
        }


    }
}