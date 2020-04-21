using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
//LIBRERIA CONEXION
using Oracle.ManagedDataAccess.Client;
//CONEXION
using TSC_WEB.Config;
//ENTIDADES
using TSC_WEB.Models.Entidades.Sistema;

namespace TSC_WEB.Models.Modelos.Sistema
{
    public class UsuariosModelo 
    {
        //INSTANCIA CONEXION
        DBAccess conexion = new DBAccess();

        //LOGIN
        public UsuariosEntidad Login(string usuario, string clave,bool op)
        {
            //INSTANCIA DE LA ENTIDAD A DEVOLVER
            UsuariosEntidad objUsuariosE = new UsuariosEntidad();

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SPU_USUARIOS_LOGIN",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("I_USUARIO",usuario));
                comando.Parameters.Add(new OracleParameter("I_PASS",  op ? clave : null ));
                comando.Parameters.Add(new OracleParameter("CUR",OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                //DATA READER 
                OracleDataReader registros = comando.ExecuteReader();
                while (registros.Read())
                {
                    objUsuariosE.usuario = registros["usuario"].ToString();
                    objUsuariosE.nombre = registros["nombre"].ToString();
                    objUsuariosE.codigo_cargo = Convert.ToInt32(registros["codigo_cargo"].ToString());
                }

            }
            catch (Exception e)
            {
                objUsuariosE.error = e.Message.ToString();
            }
            finally
            {
                conexion.Desconectar();
            }

            return objUsuariosE;
            
        }

        //LISTA USUARIOS
        public List<UsuariosEntidad> Listar()
        {
            List<UsuariosEntidad> UsuariosLista = new List<UsuariosEntidad>();
            OracleCommand comando = new OracleCommand("systextilrpt.spu_listar_usuarios_web",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("cur",OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                UsuariosEntidad objUsuariosE = new UsuariosEntidad();
                objUsuariosE.usuario = registros["usuario"].ToString();
                objUsuariosE.nombre = registros["nombre"].ToString();

                UsuariosLista.Add(objUsuariosE);
            }

            conexion.Desconectar();

            return UsuariosLista;
        }
    }
}