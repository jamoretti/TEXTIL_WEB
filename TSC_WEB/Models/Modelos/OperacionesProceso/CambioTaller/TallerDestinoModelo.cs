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
    public class TallerDestinoModelo
    {
        DBAccess conexion = new DBAccess();

        public List<TallerDestinoEntidad> Listar()
        {
            List<TallerDestinoEntidad> TallerDestinoLista = new List<TallerDestinoEntidad>();
            OracleCommand comando = new OracleCommand("systextilrpt.getsys_talleres",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();
            while(registros.Read()){
                TallerDestinoEntidad objTallerE = new TallerDestinoEntidad();
                objTallerE.CodigoTaller = registros["CODIGOTALLER"].ToString();
                objTallerE.NombreTaller = registros["NOMBRETALLER"].ToString();

                TallerDestinoLista.Add(objTallerE);
            }
            conexion.Desconectar();

            return TallerDestinoLista;
        }
    }
}