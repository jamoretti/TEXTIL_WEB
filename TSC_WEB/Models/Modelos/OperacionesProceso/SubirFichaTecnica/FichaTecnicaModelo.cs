using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using TSC_WEB.Models.Entidades.OperacionesProceso.SubirFichaTecnica;

namespace TSC_WEB.Models.Modelos.OperacionesProceso.SubirFichaTecnica
{
    public class FichaTecnicaModelo
    {
        DBAccess conexion = new DBAccess();
        //REGISTRAR
        public FichaTecnicaEntidad Registrar(int idarea, string estilo, string proyecto,string observacion,string usuario,int empresa,string cliente, string programa,int? idmotivoactualizacion)
        {
            FichaTecnicaEntidad objFicha = new FichaTecnicaEntidad();

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.setsys_ftec_uploadfile2",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            try
            {
                comando.Parameters.Add(new OracleParameter("i_idarea",idarea));
                comando.Parameters.Add(new OracleParameter("i_estilo", estilo));
                comando.Parameters.Add(new OracleParameter("i_proyecto", proyecto));
                comando.Parameters.Add(new OracleParameter("i_observacion", observacion));
                comando.Parameters.Add(new OracleParameter("i_usuario", usuario ));
                comando.Parameters.Add(new OracleParameter("i_empresa", empresa));
                comando.Parameters.Add(new OracleParameter("i_cliente", cliente));
                comando.Parameters.Add(new OracleParameter("i_programa", programa));
                comando.Parameters.Add(new OracleParameter("i_idmotivoactualizacion", idmotivoactualizacion));

                //PARAMETRO DE SALIDA
                comando.Parameters.Add(new OracleParameter("o_archivo", OracleDbType.Varchar2,200)).Direction = ParameterDirection.Output;
                comando.Parameters.Add(new OracleParameter("o_idupload", OracleDbType.Int64)).Direction = ParameterDirection.Output;

                comando.ExecuteNonQuery();
                var archivo = comando.Parameters["o_archivo"].Value.ToString();
                var id = Convert.ToInt32(comando.Parameters["o_idupload"].Value.ToString());

                objFicha.archivo = archivo;
                objFicha.idupload = id;

            }catch( Exception e){
                objFicha.error = e.ToString();
                objFicha.errorbool = true;
            }finally{
                conexion.Desconectar();
            }

            return objFicha;

        }
        //LISTAR
        public List<FichaTecnicaEntidad> Listar(string op, string idarea, string estilo, string pedido)
        {
            List<FichaTecnicaEntidad> objFichaLista = new List<FichaTecnicaEntidad>();

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.getsys_ftec_UploadFile2",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("i_op",  op == string.Empty ? null : op ));
            comando.Parameters.Add(new OracleParameter("i_idarea", idarea == string.Empty ? null : idarea));
            comando.Parameters.Add(new OracleParameter("i_estilo", estilo == string.Empty ? null : estilo));
            comando.Parameters.Add(new OracleParameter("i_pedido", pedido == string.Empty ? null : pedido));

            comando.Parameters.Add(new OracleParameter("pcursor",OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while(registros.Read()){
                FichaTecnicaEntidad objFichaE = new FichaTecnicaEntidad();
                objFichaE.idupload =  Convert.ToInt32(registros["idupload"].ToString());
                objFichaE.nombreareas = registros["nombreareas"].ToString();
                objFichaE.estilo = registros["estiloupload"].ToString();
                objFichaE.alternativa = registros["alternativaupload"].ToString();
                objFichaE.proyecto = registros["proyectoupload"].ToString();
                objFichaE.version = Convert.ToInt16(registros["VERSIONUPLOAD"].ToString());
                objFichaE.aprobacion = Convert.ToInt16(registros["APORBACIONUPLOAD"].ToString());
                objFichaE.archivo = registros["ARCHIVOUPLOAD"].ToString();
                objFichaE.observacion = registros["observacion"].ToString();
                objFichaE.usuariocre = registros["usuariorcre"].ToString();
                objFichaE.cliente = registros["cliente"].ToString();
                objFichaE.programa = registros["programa"].ToString();
                objFichaE.motivoactualizacion = registros["motivoactualizacion"].ToString();

                //FECHA
                DateTime fecha = Convert.ToDateTime(registros["fechacre"].ToString());
                objFichaE.fechacre = fecha.ToString("dd/MM/yyyy");
                //HORA 
                objFichaE.horacre = fecha.ToString("hh:mm tt");
                objFichaE.rutaarchivo = registros["rutaarchivo"].ToString();


                objFichaLista.Add(objFichaE);
            }

            conexion.Desconectar();
            return objFichaLista;

        }
        //ACTULIZA RUTA DE ACCESO A ARCHIVO
        public bool ActualizarRuta(string ruta,int idupload)
        {
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.actualizarutafichatecnica", conexion.Acceder());
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
        //ACTUALIZA ESTADO APROBACION
        public bool CambiaEstadoAprobacion(int idupload)
        {
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.spu_cestadofichatecnica", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            try
            {
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
        //VERIFICA SI ESTA ACTULIZANDO UN REGISTRO
        public int ValidaExistencia(int idarea, string estilo, string proyecto)
        {
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.spu_vactualiazacion_ftecnica", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("i_idarea", idarea));
                comando.Parameters.Add(new OracleParameter("i_estilo", estilo));
                comando.Parameters.Add(new OracleParameter("i_proyecto", proyecto));
                //PARAMETRO DE SALIDA
                comando.Parameters.Add(new OracleParameter("o_actualizacion", OracleDbType.Int16)).Direction = ParameterDirection.Output;
                comando.ExecuteNonQuery();
                var resultado = comando.Parameters["o_actualizacion"].Value.ToString();
                return Convert.ToInt16(resultado);
            }
            catch
            {
                return 0;
            }
            finally
            {
                conexion.Desconectar();
            }



        }
    }
}