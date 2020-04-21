using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using TSC_WEB.Config;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.OperacionesProceso.SubirFichaTecnica;

namespace TSC_WEB.Models.Modelos.OperacionesProceso.SubirFichaTecnica
{
    public class ProgramasClientesModelo
    {
        DBAccess conexion = new DBAccess();

        public List<ProgramasClientesEntidad> Listar(string cliente9, string cliente4, string cliente2)
        {
            List<ProgramasClientesEntidad> ProgramaLista = new List<ProgramasClientesEntidad>();
            OracleCommand comando = new OracleCommand("systextilrpt.programasftec_listar", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("i_cliente9", cliente9));
            comando.Parameters.Add(new OracleParameter("i_cliente4", cliente4));
            comando.Parameters.Add(new OracleParameter("i_cliente2", cliente2));
            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ProgramaLista.Add(
                    new ProgramasClientesEntidad
                    {
                        programa = registros["GRUPO_PLANEJAMENTO"].ToString(),
                    }
                );
            }

            conexion.Desconectar();
            return ProgramaLista;
        }
    }
}