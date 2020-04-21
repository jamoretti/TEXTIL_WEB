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
    public class EmpresasModelo
    {
        DBAccess conexion = new DBAccess();

        public  List<EmpresasEntidad> Listar()
        {
            //CREANDO LISTA PARA DEVOLVER 
            List<EmpresasEntidad> EmpresasLista = new List<EmpresasEntidad>();

            OracleCommand comando = new OracleCommand("systextilrpt.getsys_empresas", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("cur", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                EmpresasEntidad ObjEmpresasE = new EmpresasEntidad();
                ObjEmpresasE.codigo_empresa = Convert.ToInt32(registros["codigo_empresa"].ToString());
                ObjEmpresasE.nome_empresa = registros["nome_empresa"].ToString();

                //AGREGANDO DATOS A LA LISTA
                EmpresasLista.Add(ObjEmpresasE);
            }
            //CERRANDO CONEXION
            conexion.Desconectar();
            //RETORNANDO LISTA
            return EmpresasLista;
        }
    }
}