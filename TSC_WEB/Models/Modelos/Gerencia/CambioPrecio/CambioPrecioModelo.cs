using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Gerencia.CambioPrecio;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace TSC_WEB.Models.Modelos.Gerencia.CambioPrecio
{
    public class CambioPrecioModelo
    {
        DBAccess conexion = new DBAccess();

        //LISTA
        public List<CambioPrecioEntidad> ListarPendientes()
        {
            //CREAMOS LISTA PARA DEVOLVER
            List<CambioPrecioEntidad> objPrecioList = new List<CambioPrecioEntidad>();

            OracleCommand comando = new OracleCommand("USYSTEX.GETSYS_COME_CAMBIO_PRECIO",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add("p_pedido",DBNull.Value);
            comando.Parameters.Add("p_cliente", DBNull.Value);
            comando.Parameters.Add("pcursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while(registros.Read()){
                objPrecioList.Add(
                        new CambioPrecioEntidad
                        {
                            cod_cliente = registros["COD_CLIENTE"].ToString(),
                            cod_clientenumero = registros["COD_CLIENTENUMERO"].ToString(),

                            cliente = registros["CLIENTE"].ToString(),
                            pedido = Convert.ToInt32(registros["PEDIDO"].ToString()),
                            antiguo_precio = Convert.ToDecimal(registros["ANTIGUO_PRECIO"].ToString()),
                            nuevo_precio = Convert.ToDecimal(registros["NUEVO_PRECIO"].ToString()),
                            estado = registros["ESTADO"].ToString(),
                            usuario = registros["USUARIO"].ToString(),
                            fecha_solicitud = registros["FECHA_SOLICITUD"].ToString(),

                        }
                    );
            }

            conexion.Desconectar();

            return objPrecioList;

        }

        //CAMBIA PRECIO
        public CambioPrecioEntidad CambiarEstado(int pedido, string codcodigo, string estado, string motivo, int? idmotivo) 
        {
            CambioPrecioEntidad objPrecioE = new CambioPrecioEntidad();

            OracleCommand comando = new OracleCommand("USYSTEX.spu_aprodescambioprecio",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            try
            {

                comando.Parameters.Add(new OracleParameter("i_pedido",pedido));
                comando.Parameters.Add(new OracleParameter("i_codcliente", codcodigo));
                comando.Parameters.Add(new OracleParameter("i_opcion", estado));

                if(idmotivo == null){
                    comando.Parameters.Add(new OracleParameter("i_motivo", DBNull.Value));
                }
                else
                {
                    comando.Parameters.Add(new OracleParameter("i_motivo",idmotivo));
                }

                comando.Parameters.Add(new OracleParameter("i_motivoc", motivo));


                comando.ExecuteNonQuery();

                objPrecioE.error        = "Proceso concluido con exito ";
                objPrecioE.errorbool    = false;
            }catch(Exception e)
            {
                objPrecioE.error = e.Message;
                objPrecioE.errorbool = true;
            }
            finally
            {
                conexion.Desconectar();
            }

            return objPrecioE;

        }

        //LISTA MOTIVOS DE RECHAZO
        public List<MotivoRechazosEntidad> ListarMotivos()
        {
            List<MotivoRechazosEntidad> objMotivoRE = new List<MotivoRechazosEntidad>();

            OracleCommand comando = new OracleCommand("select idmotivorechazo,motivo from mrechazos_cambioprecio",conexion.Acceder());
            comando.CommandType = CommandType.Text;
            conexion.Conectar();

            OracleDataReader registros = comando.ExecuteReader();

            while(registros.Read()){
                objMotivoRE.Add(
                    new MotivoRechazosEntidad
                    {
                        idmotivorechazo = Convert.ToInt32(registros["idmotivorechazo"].ToString()),
                        motivo          = registros["motivo"].ToString(),
                    }    
                );
            }

            conexion.Desconectar();

            return objMotivoRE;

        }
            

    }
}