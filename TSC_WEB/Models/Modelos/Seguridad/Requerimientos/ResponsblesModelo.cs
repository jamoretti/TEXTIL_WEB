using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.Seguridad.Requerimientos;

namespace TSC_WEB.Models.Modelos.Seguridad.Requerimientos
{
    public class ResponsblesModelo
    {
        DBAccess conexion = new DBAccess();

        public List<ResponsablesEntidad> ListarResponsables()
        {
            List<ResponsablesEntidad> objLista = new List<ResponsablesEntidad>();

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.GET_RESPONSABLE_REQ",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objLista.Add(
                        new ResponsablesEntidad
                        {
                            idresponsable =  Convert.ToInt32(registros["idresponsable"].ToString()),
                            responsable = registros["responsable"].ToString()
                        }
                );
            }

            conexion.Desconectar();
            return objLista;
        }
    }
}