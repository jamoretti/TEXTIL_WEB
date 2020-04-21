using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Seguridad;

namespace TSC_WEB.Models.Modelos.Seguridad
{
    public class CambioFechaModelo
    {
        DBAccess conexion = new DBAccess();

        public List<CambioFechaEntidad> Listar()
        {
            List<CambioFechaEntidad> lista = new List<CambioFechaEntidad>();

            OracleCommand comando = new OracleCommand("spu_getfechasegu_006",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while(registros.Read()){

                lista.Add(
                    new CambioFechaEntidad
                    {
                        usuario = registros["usuario"].ToString(),
                        fechactual = registros["fechaactual"].ToString(),
                        fechasistema = registros["fechasistema"].ToString(),
                        sistema = registros["sistema"].ToString(),
                        nombreusuario = registros["nombreusuario"].ToString(),
                        centrocosto = registros["centrocosto"].ToString(),

                    }
                );

            }

            conexion.Desconectar();

            return lista;
        }
    }
}