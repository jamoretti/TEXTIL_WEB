using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Config;
using System.Data;
using TSC_WEB.Models.Entidades.DesarrolloProducto.FichaTecnica;

namespace TSC_WEB.Models.Modelos.DesarrolloProducto.FichaTecnica
{
    public class EtapasModelo
    {
        DBAccess conexion = new DBAccess();

        public List<EtapasEntidad> Listar()
        {
            //LISTA
            List<EtapasEntidad> objEtapasLista = new List<EtapasEntidad>();

            OracleCommand comando = new OracleCommand("USYSTEX.SPGET_DESA_ETAPAS", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("PCURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objEtapasLista.Add(
                    new EtapasEntidad {
                        codigo_estagio = Convert.ToInt32(registros["codigo_estagio"].ToString()),
                        descricao = registros["descricao"].ToString(),
                    }
                );
            }

            conexion.Desconectar();

            return objEtapasLista;
        }
    }
}