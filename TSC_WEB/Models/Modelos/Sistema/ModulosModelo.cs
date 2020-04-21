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
    public class ModulosModelo
    {
        DBAccess conexion = new DBAccess();

        public List<ModulosEntidad> ListarModulos(int idcargo)
        {
            //DECLARAMOS LISTA
            List<ModulosEntidad> ModulosLista = new List<ModulosEntidad>();

            OracleCommand comando = new OracleCommand("SPGET_SPU_LISTARTAGS", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("i_usuario", idcargo));
            comando.Parameters.Add(new OracleParameter("p_sistema", 2));
            comando.Parameters.Add(new OracleParameter("cur",OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

           
            while (registros.Read())
            {
                ModulosEntidad objModulosE = new ModulosEntidad();
                objModulosE.codcargo = Convert.ToInt32(registros["codcargo"].ToString());
                objModulosE.idtag = Convert.ToInt32(registros["idtag"].ToString());
                objModulosE.icono =  registros["icono"].ToString();
                objModulosE.nombretag = registros["nombretag"].ToString();
                objModulosE.detnombretag = registros["detnombretag"].ToString();
                objModulosE.ruta = registros["ruta"].ToString();
                objModulosE.codopcion = Convert.ToInt32(registros["codopcion"].ToString());
                var array = registros["ruta"].ToString().Split('/');
                objModulosE.idsubmodulo = array[1].ToString() +"-"+ array[2].ToString();

                ModulosLista.Add(objModulosE);

            }
            conexion.Desconectar();
            return ModulosLista;
        }
    }
}