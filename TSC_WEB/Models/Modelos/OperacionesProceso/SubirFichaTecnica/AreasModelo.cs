using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.Sistema;
using System.Data;

namespace TSC_WEB.Models.Modelos.OperacionesProceso.SubirFichaTecnica
{
    public class AreasModelo
    {
        DBAccess conexion = new DBAccess();

        public List<AreasEntidad> ListarAreas()
        {
            List<AreasEntidad> AreasLista = new List<AreasEntidad>();

            OracleCommand comando = new OracleCommand("systextilrpt.getsys_ftec_areas",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while(registros.Read()){
                AreasEntidad objAreas = new AreasEntidad();
                objAreas.idarea = Convert.ToInt16(registros["idareas"].ToString());
                objAreas.nombre_area = registros["nombreareas"].ToString();

                AreasLista.Add(objAreas);
            }
            conexion.Desconectar();
            return AreasLista;
        }
    }
}