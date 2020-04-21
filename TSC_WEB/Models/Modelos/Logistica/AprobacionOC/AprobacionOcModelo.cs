using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Globalization;
using TSC_WEB.Models.Entidades.Sistema;
using TSC_WEB.Models.Entidades.Logistica.AprobacionOC;

namespace TSC_WEB.Models.Modelos.Logistica.AprobacionOC
{
    public class AprobacionOcModelo
    {

        NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

        DBAccess conexion = new DBAccess();
        SistemaEntidad objSistemaE = new SistemaEntidad();
        OrdenCompraCabeceraEntidad  objOrdenCompraE = new OrdenCompraCabeceraEntidad();
        List<OrdenCompraEntidad> objOrdenCompraeLista = new List<OrdenCompraEntidad>();
        List<OrdenCompraDetalleEntidad> objOrdenCompraDetalleL = new List<OrdenCompraDetalleEntidad>();

        //CAMBIA EL ESTADO DE TODOS LOS DETALLES DE LA OC
        public SistemaEntidad CambiarEstadoDetalle(int ordencompra,int empresa,string usuario)
        {


            OracleCommand comando = new OracleCommand("spu_validarliberacionoc", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("i_ordencompra",ordencompra));
                comando.Parameters.Add(new OracleParameter("i_empresa", empresa));
                comando.Parameters.Add(new OracleParameter("i_usuario", usuario));
                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

                OracleDataReader objDevolver = comando.ExecuteReader();

                while(objDevolver.Read()){
                    objSistemaE.mensajesistema = objDevolver["mensaje"].ToString();
                    objSistemaE.respuestabool = objDevolver["validador"].ToString() == "1" ? true : false;
                }

            }catch(Exception e){
                objSistemaE.mensajesistema = e.Message;
                objSistemaE.respuestabool = false;
            }
            conexion.Desconectar();
            return objSistemaE;
        }

        //LISTA CABECERA DE OC
        public OrdenCompraCabeceraEntidad ListarCabecera(int ordencompra)
        {
            OracleCommand comando = new OracleCommand("spu_getinfocabeceraoc", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_oc",ordencompra));
            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while(registros.Read()){
                objOrdenCompraE.codigo_empresa  = Convert.ToInt32(registros["CODIGO_EMPRESA"].ToString());
                objOrdenCompraE.pedido_compra =  Convert.ToInt32(registros["pedido_compra"].ToString());
                objOrdenCompraE.data_prev_entr = Convert.ToDateTime(registros["data_prev_entr"].ToString()).ToShortDateString();
                objOrdenCompraE.datetime_pedido = Convert.ToDateTime(registros["datetime_pedido"].ToString()).ToShortDateString();
                objOrdenCompraE.FORN_PED_FORNE9 = registros["FORN_PED_FORNE9"].ToString();
                objOrdenCompraE.FORN_PED_FORNE4 = registros["FORN_PED_FORNE4"].ToString();
                objOrdenCompraE.FORN_PED_FORNE2 = registros["FORN_PED_FORNE2"].ToString();
                objOrdenCompraE.COND_PGTO_COMPRA = registros["COND_PGTO_COMPRA"].ToString();
                objOrdenCompraE.DESCR_COND_PGTO = registros["DESCR_COND_PGTO"].ToString();
                objOrdenCompraE.COD_MOEDA = Convert.ToInt32(registros["COD_MOEDA"].ToString());
                objOrdenCompraE.DESCRICAO = registros["DESCRICAO"].ToString();
                objOrdenCompraE.SIMBOLO_MOEDA = registros["SIMBOLO_MOEDA"].ToString();

                objOrdenCompraE.TOTAL_PEDIDO = registros["TOTAL_PEDIDO"].ToString();
                objOrdenCompraE.SITUACAO_PEDIDO = registros["SITUACAO_PEDIDO"].ToString();
                objOrdenCompraE.NOME_FORNECEDOR = registros["NOME_FORNECEDOR"].ToString();
                objOrdenCompraE.CODIGO_COMPRADOR = registros["CODIGO_COMPRADOR"].ToString();
                objOrdenCompraE.NOME_COMPRADOR = registros["NOME_COMPRADOR"].ToString();
            }

            conexion.Desconectar();

            return objOrdenCompraE;

        }

        //LISTA DETALLE DE OC
        public List<OrdenCompraDetalleEntidad> ListarDetalle(int ordencompra)
        {
            OracleCommand comando = new OracleCommand("spu_getinfodetalleoc", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_oc", ordencompra));
            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objOrdenCompraDetalleL.Add(
                      new OrdenCompraDetalleEntidad
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
                                    QTDE_PEDIDA_ITEM = Convert.ToDecimal(registros["QTDE_PEDIDA_ITEM"].ToString()).ToString("N", nfi),// registros["QTDE_PEDIDA_ITEM"].ToString(),
                                    VALOR_CONV = registros["VALOR_CONV"].ToString(),
                                    //total = Convert.ToDecimal(registros["total"].ToString()).ToString("N0"),
                                    total = Convert.ToDecimal(registros["total"].ToString()).ToString("N", nfi),
                                    DESCRICAO = registros["DESCRICAO"].ToString(),

                      }
                );
            }

            conexion.Desconectar();

            return objOrdenCompraDetalleL;
        }

        //TRAE PERIODO ACTUAL
        public OrdenCompraCabeceraEntidad DetalleCabecera(int ordencompra)
        {
            OracleCommand comando = new OracleCommand("spu_getperiodoactual", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_ordencompra", ordencompra));
            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while(registros.Read()){
                objOrdenCompraE.centrocosto = registros["centrocosto"].ToString();
                objOrdenCompraE.periodo = registros["periodo"].ToString();
                objOrdenCompraE.presupuesto = Convert.ToDecimal(registros["presupuesto"].ToString());
                objOrdenCompraE.valorusado = Convert.ToDecimal(registros["valorusado"].ToString());
                objOrdenCompraE.disponible = Convert.ToDecimal(registros["disponible"].ToString());
                objOrdenCompraE.aprobado = Convert.ToDecimal(registros["aprobado"].ToString());

            }

            conexion.Desconectar();

            return objOrdenCompraE;
        }

        //LIBERA OC
        public SistemaEntidad LiberarOc(int ordencompra,string usuario,int empresa)
        {
            OracleCommand comando = new OracleCommand("spu_liberarocgeneral",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_ordencompra",ordencompra));
            comando.Parameters.Add(new OracleParameter("i_usuario", usuario));
            comando.Parameters.Add(new OracleParameter("i_empresa", empresa));
            comando.Parameters.Add(new OracleParameter("nivel", 3));

            try
            {
                comando.ExecuteNonQuery();
                objSistemaE.respuestabool = true;
                objSistemaE.mensajesistema = "Orden de compra liberada";  

            }catch(Exception e){
                objSistemaE.respuestabool = false;
                objSistemaE.mensajesistema = e.Message;
            }

            conexion.Desconectar();
            return objSistemaE;
        }

        //LISTA TODAS LA OC QUE PUEDEN SER LIBERADAS
        public List<OrdenCompraEntidad> ListarOcLiberar(string usuario,int empresa)
        {
            OracleCommand comando = new OracleCommand("spu_getocliberar",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_usuario", usuario));
            comando.Parameters.Add(new OracleParameter("i_empresa",empresa));
            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while(registros.Read()){
                objOrdenCompraeLista.Add(
                    new OrdenCompraEntidad
                    {
                        pedido              = Convert.ToInt32(registros["pedido"].ToString()),
                        fecha               = Convert.ToDateTime(registros["fecha"].ToString()).ToShortDateString(),
                        descripcionpago     = registros["descripcionpago"].ToString(),
                        moneda              = registros["moneda"].ToString(),
                        //total_pedido        = Convert.ToDecimal(registros["total_pedido"].ToString()).ToString("N0"),
                        total_pedido        = Convert.ToDecimal(registros["total_pedido"].ToString()).ToString("N",nfi),

                        proveedor           = registros["proveedor"].ToString(),
                        comprador           = registros["comprador"].ToString(),
                        niveles             = Convert.ToInt32(registros["niveles"].ToString()),
                        centrocosto         = registros["centrocosto"].ToString(),
                        accesocentrocosto   = Convert.ToInt32(registros["accesocentrocosto"].ToString())
                    }
                );
            }

            conexion.Desconectar();

            return objOrdenCompraeLista;
        }

        //RECHAZA OC
        public SistemaEntidad RechazarOc(int ordencompra)
        {
            OracleCommand comando = new OracleCommand("systextilrpt.spu_rechazarocweb", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_oc", ordencompra));

            try
            {
                comando.ExecuteNonQuery();
                objSistemaE.respuestabool = true;
                objSistemaE.mensajesistema = "Orden de compra Rechazada";

            }
            catch (Exception e)
            {
                objSistemaE.respuestabool = false;
                objSistemaE.mensajesistema = e.Message;
            }

            conexion.Desconectar();
            return objSistemaE;
        }

        //DEVUELVE PRESUPUESTO OC
        public void DevuelvePresupuesto(int oc,int secuencia,string usuario)
        {
            OracleCommand comando = new OracleCommand("USYSTEX.SPU_SET_DEVOLVER_PPTO_OC", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("IP_PEDIDOCOMPRA", oc));
                comando.Parameters.Add(new OracleParameter("IP_SECUENCIA", secuencia));
                comando.Parameters.Add(new OracleParameter("CH_USUARIO", usuario));
                //comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

                comando.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {
                conexion.Desconectar();
            }
            
            
        }
    }
}