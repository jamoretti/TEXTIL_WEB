using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Comercial;

namespace TSC_WEB.Models.Modelos.Comercial
{
    public class Come002Modelo
    {
        DBAccess conexion = new DBAccess();
        public Come_002Entidad RegistrarCome002(string PO, string OBSERVACION, string USUARIORCRE, int IDEMPRESA)
        {
            Come_002Entidad objFicha = new Come_002Entidad();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SETSYS_GRABAR_COME002", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            //GRABAR
            try
            {
                comando.Parameters.Add(new OracleParameter("i_estilo", PO));
                comando.Parameters.Add(new OracleParameter("i_observacion", OBSERVACION));
                comando.Parameters.Add(new OracleParameter("i_usuario", USUARIORCRE));
                comando.Parameters.Add(new OracleParameter("i_empresa", IDEMPRESA));

                //PARAMETRO DE SALIDA
                comando.Parameters.Add(new OracleParameter("o_archivo", OracleDbType.Varchar2, 200)).Direction = ParameterDirection.Output;
                comando.Parameters.Add(new OracleParameter("o_idupload", OracleDbType.Int64)).Direction = ParameterDirection.Output;
                comando.ExecuteNonQuery();
                var archivo = comando.Parameters["o_archivo"].Value.ToString();
                var id = Convert.ToInt32(comando.Parameters["o_idupload"].Value.ToString());

                objFicha.ARCHIVO = archivo;
                objFicha.IDUPLOAD = id;
            }
            catch (Exception e)
            {
                objFicha.error = e.ToString();
                objFicha.errorbool = true;
            }
            finally
            {
                conexion.Desconectar();
            }
            return objFicha;

        }

        //ACTULIZA RUTA DE ACCESO A ARCHIVO
        public bool ActualizarPO(string ruta, int idupload)
        {
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SETSYS_ACTU_COME002", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            try
            {
                comando.Parameters.Add(new OracleParameter("i_ruta", ruta));
                comando.Parameters.Add(new OracleParameter("i_idupload", idupload));

                //EJECUTNADO
                return comando.ExecuteNonQuery() > 0 ? true : false;

            }
            catch
            {
                return false;
            }
            finally
            {
                conexion.Desconectar();
            }
        }

        //OBTENIENDO CLIENTE POR EL ESTILO TSC
        public Come_002Entidad  BuscarxPO(string po)
        {
            Come_002Entidad ObjFichasLista = new Come_002Entidad();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.GETSYS_COME_002", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("p_po", po));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.cliente = Convert.ToString(registros["NOME_CLIENTE"].ToString());

            }
            conexion.Desconectar();
            return ObjFichasLista;
        }

        // REPORTE PARA LA CONSULTA PRINCIPAL DE LA BUSQUEDA
        public List<Come_002Entidad> RptCome002(string po, string pedido,string programa)
        {
            List<Come_002Entidad> objFichaLista = new List<Come_002Entidad>();

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.GETSYS_RPTCOME002", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("i_po", po == string.Empty ? null : po));
            comando.Parameters.Add(new OracleParameter("i_pedido", pedido == string.Empty ? null : pedido));
            comando.Parameters.Add(new OracleParameter("i_programa", programa == string.Empty ? null : programa));

 

            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                Come_002Entidad objFichaE = new Come_002Entidad();
                objFichaE.IDUPLOAD = Convert.ToInt32(registros["IDUPLOAD"].ToString());
             
                objFichaE.PO = registros["PO"].ToString();
                objFichaE.VERSIONUPLOAD = Convert.ToInt32(registros["VERSIONUPLOAD"].ToString());
                objFichaE.ARCHIVO =  registros["ARCHIVOU"].ToString();
                objFichaE.OBSERVACION = registros["OBSERVACION"].ToString();
                objFichaE.USUARIORCRE = registros["USUARIORCRE"].ToString();
                objFichaE.RUTAARCHIVO = registros["RUTAARCHIVO"].ToString();
                objFichaE.cliente = registros["NOME_CLIENTE"].ToString();
                //FECHA
                DateTime FECHACREA = Convert.ToDateTime(registros["FECHACRE"].ToString());
                objFichaE.fechacre = FECHACREA.ToString("dd/MM/yyyy");
                //HORA 
                objFichaE.horacre = FECHACREA.ToString("hh:mm tt");                 
                objFichaLista.Add(objFichaE);    
            }

            conexion.Desconectar();
            return objFichaLista;

        }
    }
}