using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using TSC_WEB.Config;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.Sistema;

namespace TSC_WEB.Models.Modelos.OperacionesProceso.SubirFichaTecnica
{
    public class ClientesModelo
    {
        DBAccess conexion = new DBAccess();
        public List<ClientesEntidad> Listar()
        {
            List<ClientesEntidad> ClientesLista = new List<ClientesEntidad>();
            OracleCommand comando = new OracleCommand("systextilrpt.clientesftec_listar",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while(registros.Read()){
                ClientesLista.Add(
                    new ClientesEntidad
                    {
                        cliente9        = registros["CGC_CLIENTE9"].ToString(),
                        cliente4        = registros["CGC_CLIENTE4"].ToString(),
                        cliente2        = registros["CGC_CLIENTE2"].ToString(),
                        nombre_cliente  = registros["NOME_CLIENTE"].ToString(),

                    }
                );
            }

            conexion.Desconectar();
            return ClientesLista;
        }
    }
}