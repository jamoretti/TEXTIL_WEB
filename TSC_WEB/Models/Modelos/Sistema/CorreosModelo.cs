using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Sistema;

namespace TSC_WEB.Models.Modelos.Sistema
{
    public class CorreosModelo
    {
        DBAccess conexion = new DBAccess();

        //OBTIENE CORREOS A ENVIAR
        public List<CorreosEntidad> ListCorreo(string tipo, string proceso)
        {
            try
            {
                List<CorreosEntidad> CorreosLista = new List<CorreosEntidad>();
                OracleCommand comando = new OracleCommand("systextilrpt.getsys_correos", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();
                comando.Parameters.Add(new OracleParameter("ptipo", tipo));
                comando.Parameters.Add(new OracleParameter("pproceso", proceso));
                comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                OracleDataReader registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    CorreosLista.Add(new CorreosEntidad()
                    {
                        idcorreo = Convert.ToInt32(registros["idcorreo"].ToString()),
                        correo = registros["correo"].ToString(),
                        tipo = registros["tipo"].ToString(),
                        proceso = registros["proceso"].ToString(),
                        Observacion = registros["Observacion"].ToString(),
                        dominio = registros["dominio"].ToString(),
                        usuario = registros["usuario"].ToString(),
                        password = registros["contrasena"].ToString(),
                    });
                }
                conexion.Desconectar();
                return CorreosLista;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}