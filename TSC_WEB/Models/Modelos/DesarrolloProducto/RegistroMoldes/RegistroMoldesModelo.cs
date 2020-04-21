using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using TSC_WEB.Models.Entidades.DesarrolloProducto.RegistroMoldes;


namespace TSC_WEB.Models.Modelos.DesarrolloProducto.RegistroMoldes
{
    public class RegistroMoldesModelo
    {
        DBAccess conexion = new DBAccess();

        //REGISTRA
        public bool Registrar(string estilo,string programa,string pedidos,string observacion,string usuario,string ruta)
        {
            try
            {
                OracleCommand comando = new OracleCommand("spu_desa_003_registrar", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("i_estilo", estilo));
                comando.Parameters.Add(new OracleParameter("i_programa", programa));
                comando.Parameters.Add(new OracleParameter("i_pedidos", pedidos));
                comando.Parameters.Add(new OracleParameter("i_observacion", observacion));
                comando.Parameters.Add(new OracleParameter("i_usuario", usuario));
                comando.Parameters.Add(new OracleParameter("i_rutacompartida", ruta));

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

        //LISTA
        public List<RegistroMoldesEntidad> Listar(int? pedido,string fechai,string fechaf,string estilo,string programa)
        {
            List<RegistroMoldesEntidad> objRegistroLista = new List<RegistroMoldesEntidad>();

            OracleCommand comando = new OracleCommand("spu_desa_003_listar",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("i_pedido",pedido == 0 ? null : pedido));
            comando.Parameters.Add(new OracleParameter("i_fechai", fechai == string.Empty ? null : fechai  ));
            comando.Parameters.Add(new OracleParameter("i_fechaf", fechaf == string.Empty ? null : fechaf));
            comando.Parameters.Add(new OracleParameter("i_estilo", estilo == string.Empty ? null : estilo));
            comando.Parameters.Add(new OracleParameter("i_programa", programa == string.Empty ? null : programa));

            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while(registros.Read()){
                objRegistroLista.Add(new RegistroMoldesEntidad{
                    
                    iddesa003       = Convert.ToInt32(registros["iddesa003"].ToString()),
                    pedidos         = registros["pedidos"].ToString(),
                    observacion     = registros["observacion"].ToString(),
                    usuario         = registros["usuario"].ToString(),
                    fecha           = registros["fecha"].ToString(),
                    rutacompartida  = registros["rutacompartida"].ToString(),
                    programa        = registros["programa"].ToString(),
                    estilo          = registros["estilo"].ToString(),

                });
            }

            conexion.Desconectar();
            return objRegistroLista;


        }

        //VERIFICA EXISTENCIA
        public bool VerificaExistenciaPedido(int pedido)
        {
            OracleCommand comando = new OracleCommand("spu_desa_003_buscarpedido", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_pedido",pedido));
            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            DataTable dt = new DataTable();
            dt.Load(comando.ExecuteReader());
            conexion.Desconectar();
            return dt.Rows.Count > 0 ? true : false;
        }

        //DEVUELVE PEDIDOS SEGUN PROGRAMA
        public List<RegistroMoldesEntidad> getPedidosPrograma(string programa,string estilo)
        {
            List<RegistroMoldesEntidad> objRegistroLista = new List<RegistroMoldesEntidad>();

            OracleCommand comando = new OracleCommand("spu_desa_003_getpedidos", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("i_programa", programa));
            comando.Parameters.Add(new OracleParameter("i_estilo", estilo));

            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objRegistroLista.Add(new RegistroMoldesEntidad
                {
                    pedidos = registros["pedidos"].ToString()
                });
            }

            conexion.Desconectar();
            return objRegistroLista;
        }

    }


}