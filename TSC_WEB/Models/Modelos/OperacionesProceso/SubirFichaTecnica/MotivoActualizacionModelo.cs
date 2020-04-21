using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.OperacionesProceso.SubirFichaTecnica;
using System.Data;


namespace TSC_WEB.Models.Modelos.OperacionesProceso.SubirFichaTecnica
{
    public class MotivoActualizacionModelo
    {
        DBAccess conexion = new DBAccess();

        public List<MotivoActualizacionEntidad> Listar()
        {
            List<MotivoActualizacionEntidad> MotivoLista = new List<MotivoActualizacionEntidad>();

            OracleCommand comando = new OracleCommand("systextilrpt.spu_mactualizacionftecnica_lis", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                MotivoActualizacionEntidad objMotivo = new MotivoActualizacionEntidad();
                objMotivo.idmotivoactualizacion = Convert.ToInt16(registros["IDMOTIVOACTUALIZACION"].ToString());
                objMotivo.descripcion = registros["DESCRIPCION"].ToString();
                MotivoLista.Add(objMotivo);

            }
            conexion.Desconectar();
            return MotivoLista;
        }
    }
}