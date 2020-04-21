using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.OperacionesProceso.CambioTaller;

namespace TSC_WEB.Models.Modelos.OperacionesProceso.CambioTaller
{
    public class MotivoModelo
    {
        DBAccess conexion = new DBAccess();

        public List<MotivoEntidad> Listar()
        {
            List<MotivoEntidad> MotivoLista = new List<MotivoEntidad>();
            OracleCommand comando = new OracleCommand("systextilrpt.getsys_motivos", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {
                MotivoEntidad objMotivoE = new MotivoEntidad();
                objMotivoE.CodigoMotivo = Convert.ToInt32(registros["CODIGO_MOTIVO"].ToString());
                objMotivoE.NombreMotivo = registros["NOMBRE_MOTIVO"].ToString();

                MotivoLista.Add(objMotivoE);
            }
            conexion.Desconectar();

            return MotivoLista;
        }
    }
}