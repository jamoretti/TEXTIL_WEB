using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Sistema;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace TSC_WEB.Models.Modelos.Sistema
{
    public class SisTareasModelo
    {
        DBAccess conexion = new DBAccess();

        public List<SisTareasEntidad> ListarTareas(int idtarea)
        {
            //CREAMOS LISTA
            List<SisTareasEntidad> objLista = new List<SisTareasEntidad>();
            OracleCommand comando = new OracleCommand("systextilrpt.spu_gettareas", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_idtarea", idtarea));
            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while(registros.Read()){
                objLista.Add(
                    new SisTareasEntidad { iddescripciontarea = Convert.ToInt32(registros["iddescripciontarea"].ToString()) , descripcion = registros["descripcion"].ToString()}
                );
            }
            conexion.Desconectar();

            return objLista;
        }
    }
}