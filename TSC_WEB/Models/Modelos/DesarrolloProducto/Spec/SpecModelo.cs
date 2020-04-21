using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.DesarrolloProducto.Spec;
using TSC_WEB.Models.Entidades.Sistema;

namespace TSC_WEB.Models.Modelos.DesarrolloProducto.Spec
{
    public class SpecModelo
    {
        DBAccess conexion = new DBAccess();
        //REGISTRAR
        public SpecEntidad Registrar( string estilo, string observacion, string usuario, int empresa)
        {
            SpecEntidad objFicha = new SpecEntidad();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SETSYS_GRABAR_DESA001", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("i_estilo", estilo));
                comando.Parameters.Add(new OracleParameter("i_observacion", observacion));
                comando.Parameters.Add(new OracleParameter("i_usuario", usuario));
                comando.Parameters.Add(new OracleParameter("i_empresa", empresa));
                //PARAMETRO DE SALIDA
                comando.Parameters.Add(new OracleParameter("o_archivo", OracleDbType.Varchar2, 200)).Direction = ParameterDirection.Output;
                comando.Parameters.Add(new OracleParameter("o_idupload", OracleDbType.Int64)).Direction = ParameterDirection.Output;
                comando.ExecuteNonQuery();
                var archivo = comando.Parameters["o_archivo"].Value.ToString();
                var id = Convert.ToInt32(comando.Parameters["o_idupload"].Value.ToString());

                objFicha.archivo = archivo;
                objFicha.idupload = id;
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
        public bool ActualizarRutaSpec(string ruta, int idupload)
        {
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SETSYS_ACTU_DESA001", conexion.Acceder());
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
        public SpecClienteEntidad BuscxEstilo(string estilo)
        {            
            SpecClienteEntidad ObjFichasLista = new SpecClienteEntidad();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.GETSYS_DESA_001", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("p_estilo", estilo));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.cliente = Convert.ToString(registros["NOME_CLIENTE"].ToString());
                ObjFichasLista.existe = Convert.ToInt32(registros["CD_IT_PE_GRUPO"].ToString());
               
            }
            conexion.Desconectar();
            return ObjFichasLista;
        }

        // REPORTE PARA LA CONSULTA PRINCIPAL DE LA BUSQUEDA
        public List<SpecEntidad> Listar(string idarea, string estilo, string estilocliente)
        {
            List<SpecEntidad> objFichaLista = new List<SpecEntidad>();

            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.GETSYS_RPTDESA001", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("i_idcliente", idarea == string.Empty ? null : idarea));
            comando.Parameters.Add(new OracleParameter("i_estilo", estilo == string.Empty ? null : estilo));
            comando.Parameters.Add(new OracleParameter("i_escliente", estilocliente == string.Empty ? null : estilocliente));

            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                SpecEntidad objFichaE = new SpecEntidad();
                objFichaE.idupload = Convert.ToInt32(registros["idupload"].ToString());
                objFichaE.cliente = registros["Cliente"].ToString();
                objFichaE.estilo = registros["EstiloTSC"].ToString();

                objFichaE.proyecto = registros["EstiloCliente"].ToString();
                objFichaE.version = Convert.ToInt16(registros["NVersion"].ToString());

                objFichaE.archivo = registros["Archivo"].ToString();
                objFichaE.observacion = registros["observacion"].ToString();
                objFichaE.usuariocre = registros["usuariorcre"].ToString();
                //FECHA
                DateTime fecha = Convert.ToDateTime(registros["fechacre"].ToString());
                objFichaE.fechacre = fecha.ToString("dd/MM/yyyy");
                //HORA 
                objFichaE.horacre = fecha.ToString("hh:mm tt");
                objFichaE.rutaarchivo = registros["RUTAARCHIVO"].ToString();
                objFichaE.RUTAARCHIVOTELA = registros["RUTAARCHIVOTELA"].ToString();

                objFichaLista.Add(objFichaE);
            }

            conexion.Desconectar();
            return objFichaLista;

        }
    
        //LISTA DE CLIENTE
        public List<ClientesEntidad> ListarClientes()
        {
            List<ClientesEntidad> ClientesLista = new List<ClientesEntidad>();
            OracleCommand comando = new OracleCommand("systextilrpt.DESA_001CLIENTES", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ClientesLista.Add(
                    new ClientesEntidad
                    {
                        cliente9 = registros["CGC_CLIENTE9"].ToString(),
                        cliente4 = registros["CGC_CLIENTE4"].ToString(),
                        cliente2 = registros["CGC_CLIENTE2"].ToString(),
                        nombre_cliente = registros["NOME_CLIENTE"].ToString(),

                    }
                );
            }

            conexion.Desconectar();
            return ClientesLista;
        }

        //ACTULIZA RUTA DE LA TELA
        public bool ActualizarFTTELA(string ruta, string usuario, int idupload, string observacion)
        {
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.SETSYS_ACTUTELA_DESA001", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            try
            {
                comando.Parameters.Add(new OracleParameter("i_ruta", ruta));           
                comando.Parameters.Add(new OracleParameter("I_USER", usuario));
                comando.Parameters.Add(new OracleParameter("I_OBSER", observacion));
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
    }
}