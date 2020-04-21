using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Config;
using System.Data;
using TSC_WEB.Models.Entidades.Corte;
using TSC_WEB.Models.Entidades.Corte.LiquidacionTela;
// EXPORTAR
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;


namespace TSC_WEB.Models.Modelos.Corte.LiquidacionTela
{
    public class FichasModelo
    {
        DBAccess conexion = new DBAccess();

        #region BUSCAR LIQUIDACION
        public List<Corte_001Entidad> ListarVersiones(string ficha, string version, string tipotela, string op)
        {
            //listar versiones
            List<Corte_001Entidad> Objx = new List<Corte_001Entidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                Objx.Add(new Corte_001Entidad()
                {
                    PARTIDA = Convert.ToString(registros["PARTIDA"].ToString()),
                    VERSIONES = Convert.ToInt32(registros["VERSIONES"].ToString()),
                   
                    F_REGISTRO = Convert.ToString(registros["F_REGISTRO"].ToString()),
                    U_REGISTRO = Convert.ToString(registros["U_REGISTRO"].ToString()),
                    TIPO_TELA = Convert.ToString(registros["TIPO_TELA"].ToString()),
                    //AGREGADO
                    contenido = Convert.ToInt32(registros["contenido"].ToString()),
                });
            }
            conexion.Desconectar();
            return Objx;
        }
        public Corte_001Entidad ListarCabeceraCort001(string ficha, string version, string tipotela, string op)
        {
            //LISTA
            Corte_001Entidad ObjFichasLista = new Corte_001Entidad();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.PARTIDA = Convert.ToString(registros["PARTIDA"].ToString());
                ObjFichasLista.CLIENTES = Convert.ToString(registros["CLIENTES"].ToString());
                ObjFichasLista.PROGRAMAS = Convert.ToString(registros["PROGRAMAS"].ToString());
                ObjFichasLista.PEDIDOS = Convert.ToString(registros["PEDIDOS"].ToString());
                ObjFichasLista.ESTILOS = Convert.ToString(registros["ESTILOS"].ToString());
                ObjFichasLista.ENV_TELA_MTS = Convert.ToDecimal(registros["ENV_TELA_MTS"].ToString());
                ObjFichasLista.ENV_TELA_KG = Convert.ToDecimal(registros["ENV_TELA_KG"].ToString());
                ObjFichasLista.TOTAL_RESULT = Convert.ToInt32(registros["TOTAL_RESULT"].ToString());
                ObjFichasLista.RESULTADO = Convert.ToInt32(registros["RESULTADO"].ToString());
                ObjFichasLista.TOTAL_TALLXCONSU = Convert.ToDecimal(registros["TOTAL_TALLXCONSU"].ToString());
                ObjFichasLista.VARIACION = Convert.ToDecimal(registros["VARIACION"].ToString());
                ObjFichasLista.KG_ASIGNADO = Convert.ToDecimal(registros["KG_ASIGNADO"].ToString());
                ObjFichasLista.KG_SEGUN_TIZADO = Convert.ToDecimal(registros["KG_SEGUN_TIZADO"].ToString());
                ObjFichasLista.RESUL_KG = Convert.ToDecimal(registros["RESUL_KG"].ToString());
                ObjFichasLista.LETRA_KG = Convert.ToString(registros["LETRA_KG"].ToString());
                ObjFichasLista.F_REGISTRO = Convert.ToString(registros["F_REGISTRO"].ToString());
                ObjFichasLista.U_REGISTRO = Convert.ToString(registros["U_REGISTRO"].ToString());
                ObjFichasLista.CONSU_MTS = Convert.ToDecimal(registros["CONSU_MTS"].ToString());
                ObjFichasLista.CONSU_KG = Convert.ToDecimal(registros["CONSU_KG"].ToString());
                ObjFichasLista.UTILIDAD = Convert.ToDecimal(registros["UTILIDAD"].ToString());
                ObjFichasLista.comentario = registros["comentario"].ToString();

            }
            conexion.Desconectar();
            return ObjFichasLista;
        }
        public ReportePivot ObjCant(string ficha, string version, string tipotela, string op)
        {
            //LISTA
            ReportePivot ObjFichasLista = new ReportePivot();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.CANTPREND = Convert.ToInt32(registros["CANTPRND"].ToString());
                ObjFichasLista.CANTPANOS = Convert.ToInt32(registros["CANTPANO"].ToString());
                ObjFichasLista.TELA = Convert.ToString(registros["TELA"].ToString());
            }
            conexion.Desconectar();
            return ObjFichasLista;
        }
        public List<DatosTelaEntidad> ListaDatosTela(string ficha, string version, string tipotela, string op)
        {
            //LISTA
            List<DatosTelaEntidad> ObjFichasLista = new List<DatosTelaEntidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.Add(new DatosTelaEntidad()
                {
                    columna1 = Convert.ToString(registros["Datos Tela"].ToString()),
                    columna2 = Convert.ToDecimal(registros["Ancho Tela"].ToString()),
                    columna3 = Convert.ToDecimal(registros["Ancho Util"].ToString()),

                });
            }
            conexion.Desconectar();
            return ObjFichasLista;
        }
        public DataTable ListaConsumos(string ficha, string version, string tipotela, string op)
        {
            //LISTA
            DataTable dt = new DataTable();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            dt.Load(comando.ExecuteReader());
            conexion.Desconectar();
            return dt;
        }
        public List<Corte_005Entidad> ListaTotal(string ficha, string version, string tipotela, string op)
        {
            //LISTA
            List<Corte_005Entidad> ObjFichasLista = new List<Corte_005Entidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.Add(new Corte_005Entidad()
                {

                    TALLAXPRENDA = Convert.ToInt32(registros["0"].ToString()),
                    LARGO_TIZADO = Convert.ToDecimal(registros["0_1"].ToString()),
                    CONSUMO_PRENDA = Convert.ToDecimal(registros["0_2"].ToString()),
                    PESO_PANO = Convert.ToDecimal(registros["0_3"].ToString()),
                    EFICIE_TIZADO = Convert.ToDecimal(registros["0_4"].ToString()),
                });
            }
            conexion.Desconectar();
            return ObjFichasLista;
        }
        public DataTable ListaProporciones(string ficha, string version, string tipotela, string op)
        {

            DataTable dt = new DataTable();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            dt.Load(comando.ExecuteReader());
            conexion.Desconectar();
            return dt;
        }
        public DataTable FichasxCantidades(string ficha, string version, string tipotela, string op)
        {
            DataTable dt = new DataTable();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            dt.Load(comando.ExecuteReader());
            conexion.Desconectar();
            return dt;
        }

        #endregion

        #region REGISTRAR TENDIDO
        public List<FichasEntidad> Listarfichas(string ficha,  string version, string tipotela , string op)
        {
            //LISTA FICHAS ASOCIADAS A LA PARTIDA
            List<FichasEntidad> ObjFichasLista = new List<FichasEntidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.Add(new FichasEntidad()
                {
                    FICHA = Convert.ToString(registros["ORDEM_PRODUCAO"].ToString()),
                    CANTIDAD = Convert.ToInt32(registros["QTDE_PROGRAMADA"].ToString()),
                });
            }
            conexion.Desconectar();
            return ObjFichasLista;
        }

        public bool RegistrarCorte007(string PARTIDA, string ETAPAS, decimal NUM_PANOS, decimal LARGO_PANOS, decimal PESO_PANOS, decimal ANCHO_TELA_REAL, decimal KXETAPAS, string U_REGISTRO, string VERSION, string TELA)
        {
            //GRABAR
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.CORT_007_GRABAR", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("p_PARTIDA", PARTIDA));
                comando.Parameters.Add(new OracleParameter("p_ETAPAS", ETAPAS));
                comando.Parameters.Add(new OracleParameter("p_NUM_PANOS", NUM_PANOS));
                comando.Parameters.Add(new OracleParameter("p_LARGO_PANOS", LARGO_PANOS));
                comando.Parameters.Add(new OracleParameter("p_PESO_PANOS", PESO_PANOS));
                comando.Parameters.Add(new OracleParameter("p_ANCHO_TELA_REAL", ANCHO_TELA_REAL));
                comando.Parameters.Add(new OracleParameter("p_KXETAPAS", KXETAPAS));
                comando.Parameters.Add(new OracleParameter("p_U_REGISTRO", U_REGISTRO));
                comando.Parameters.Add(new OracleParameter("p_VERSIONES", VERSION));
                comando.Parameters.Add(new OracleParameter("p_TELA", TELA));
                comando.ExecuteNonQuery();
                return true;
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
        public List<Corte_007Entidad> ListarCorte(string ficha, string version, string tipotela, string op)
        {
            //LISTA
            List<Corte_007Entidad> ObjFichasLista = new List<Corte_007Entidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.Add(new Corte_007Entidad()
                {
                    ETAPAS = Convert.ToString(registros["ETAPAS"].ToString()),
                    NUM_PANOS = Convert.ToDecimal(registros["NUM_PANOS"].ToString()),
                    LARGO_PANOS = Convert.ToDecimal(registros["LARGO_PANOS"].ToString()),
                    PESO_PANOS = Convert.ToDecimal(registros["PESO_PANOS"].ToString()),
                    ANCHO_TELA_REAL = Convert.ToDecimal(registros["ANCHO_TELA_REAL"].ToString()),
                    KXETAPAS = Convert.ToDecimal(registros["KXETAPAS"].ToString()),
                    id = Convert.ToInt32(registros["id"].ToString()),
                    u_registro =  Convert.ToString(registros["u_registro"].ToString()),

                });
            }
            conexion.Desconectar();
            return ObjFichasLista;
        }

        public bool RegistrarCorte006(string PARTIDA, decimal MER_PUNTAS, decimal MER_RETAZOSMAS, decimal MER_RETAZOSMEN, decimal EMPALMES, decimal DEVO_PRIMERA, decimal DEVO_SEGUNDA, decimal CONOS, decimal PLASTICO, string U_REGISTRO, string VERSION, string TELA,decimal kgadicionales,int motivoadicional)
        {
            //GRABAR
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.CORT_006_GRABAR", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("p_PARTIDA", PARTIDA));
                comando.Parameters.Add(new OracleParameter("p_MER_PUNTAS", MER_PUNTAS));
                comando.Parameters.Add(new OracleParameter("p_MER_RETAZOSMAS", MER_RETAZOSMAS));
                comando.Parameters.Add(new OracleParameter("p_MER_RETAZOSMEN", MER_RETAZOSMEN));
                comando.Parameters.Add(new OracleParameter("p_EMPALMES", EMPALMES));
                comando.Parameters.Add(new OracleParameter("p_DEVO_PRIMERA", DEVO_PRIMERA));
                comando.Parameters.Add(new OracleParameter("p_DEVO_SEGUNDA", DEVO_SEGUNDA));
                comando.Parameters.Add(new OracleParameter("p_CONOS", CONOS));
                comando.Parameters.Add(new OracleParameter("p_PLASTICO", PLASTICO));
                comando.Parameters.Add(new OracleParameter("p_U_REGISTRO", U_REGISTRO));
                comando.Parameters.Add(new OracleParameter("p_VERSIONES", VERSION));
                comando.Parameters.Add(new OracleParameter("p_TELA", TELA));
                //AGREGADO
                comando.Parameters.Add(new OracleParameter("p_kgextras", kgadicionales));
                if(motivoadicional == 0){
                    comando.Parameters.Add(new OracleParameter("p_motadicional", null));

                }
                else
                {
                    comando.Parameters.Add(new OracleParameter("p_motadicional", motivoadicional));

                }

                comando.ExecuteNonQuery();
                return true;
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

        public Corte_006Entidad BuscarCabe(string ficha, string version, string tipotela, string op)
        {
            //LISTA
            Corte_006Entidad ObjFichasLista = new Corte_006Entidad();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tipotela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.MER_PUNTAS = Convert.ToDecimal(registros["MER_PUNTAS"].ToString());
                ObjFichasLista.MER_RETAZOSMAS = Convert.ToDecimal(registros["MER_RETAZOSMAS"].ToString());
                ObjFichasLista.MER_RETAZOSMEN = Convert.ToDecimal(registros["MER_RETAZOSMEN"].ToString());
                ObjFichasLista.EMPALMES = Convert.ToDecimal(registros["EMPALMES"].ToString());
                ObjFichasLista.DEVO_PRIMERA = Convert.ToDecimal(registros["DEVO_PRIMERA"].ToString());
                ObjFichasLista.DEVO_SEGUNDA = Convert.ToDecimal(registros["DEVO_SEGUNDA"].ToString());
                ObjFichasLista.CONOS = Convert.ToDecimal(registros["CONOS"].ToString());
                ObjFichasLista.PLASTICO = Convert.ToDecimal(registros["PLASTICO"].ToString());
                ObjFichasLista.KG_ADICIONALES = registros["KG_ADICIONALES"].ToString();
                ObjFichasLista.MOT_KGADICIONAL = registros["MOT_KGADICIONAL"].ToString();



            }
            conexion.Desconectar();
            return ObjFichasLista;
        }



        #endregion

        
        public ReportePivot BuscarExiste(string ficha, string version, string tela, string op)
        {
            //LISTA
            ReportePivot ObjFichasLista = new ReportePivot();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.EXISTE = Convert.ToInt32(registros["EXISTE"].ToString());            
            }
            conexion.Desconectar();
            return ObjFichasLista;
        }
       
        
        #region REGISTRAR CORTE
        public ReportePivot BuscarExisteCort007(string ficha, string version, string tela, string op)
        {
            //LISTA
            ReportePivot ObjFichasLista = new ReportePivot();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.EXISTE = Convert.ToInt32(registros["EXISTE"].ToString());
            }
            conexion.Desconectar();
            return ObjFichasLista;
        }

        public bool RegistrarCorte008(string PARTIDA, decimal ORILLOS, decimal EXTREMOS, decimal ENTRECORTE, string U_REGISTRO, string VERSION, string TELA)
        {
            //GRABAR
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.CORT_008_GRABAR", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("p_PARTIDA", PARTIDA));
                comando.Parameters.Add(new OracleParameter("p_ORILLOS", ORILLOS));
                comando.Parameters.Add(new OracleParameter("p_EXTREMOS", EXTREMOS));
                comando.Parameters.Add(new OracleParameter("p_ENTRECORTE", ENTRECORTE));
                comando.Parameters.Add(new OracleParameter("p_U_REGISTRO", U_REGISTRO));
                comando.Parameters.Add(new OracleParameter("p_VERSIONES", VERSION));
                comando.Parameters.Add(new OracleParameter("p_TELA", TELA));

                comando.ExecuteNonQuery();
                return true;
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


        #endregion



        public bool ModificarCort007(int id,decimal num_panos,decimal largo_panos,decimal peso_panos,decimal ancho_tela_real,decimal kxetapas)
        {
            //GRABAR
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.CORT_007_MODIFICAR", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            try
            {
                comando.Parameters.Add(new OracleParameter("p_id", id));
                //comando.Parameters.Add(new OracleParameter("p_etapas", etapas));
                comando.Parameters.Add(new OracleParameter("p_num_panos", num_panos));
                comando.Parameters.Add(new OracleParameter("p_largo_panos", largo_panos));
                comando.Parameters.Add(new OracleParameter("p_peso_panos", peso_panos));
                comando.Parameters.Add(new OracleParameter("p_ancho_tela_real", ancho_tela_real));
                comando.Parameters.Add(new OracleParameter("p_kxetapas", kxetapas));

                comando.ExecuteNonQuery();
                return true;
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


        public Corte_008Entidad BuscarRegistro008(string ficha, string version, string tela, string op)
        {
            //LISTA
            Corte_008Entidad ObjFichasLista = new Corte_008Entidad();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.PCP_RPTLIQTELASWEB", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("p_partida", ficha));
            comando.Parameters.Add(new OracleParameter("p_version", version));
            comando.Parameters.Add(new OracleParameter("p_tela", tela));
            comando.Parameters.Add(new OracleParameter("p_opcion", op));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                ObjFichasLista.PARTIDA = Convert.ToString(registros["PARTIDA"].ToString());
                ObjFichasLista.ORILLOS = Convert.ToDecimal(registros["ORILLOS"].ToString());
                ObjFichasLista.EXTREMOS = Convert.ToDecimal(registros["EXTREMOS"].ToString());
                ObjFichasLista.ENTRECORTE = Convert.ToDecimal(registros["ENTRECORTE"].ToString());
                ObjFichasLista.F_REGISTRO = Convert.ToString(registros["F_REGISTRO"].ToString());
                ObjFichasLista.U_REGISTRO = Convert.ToString(registros["U_REGISTRO"].ToString());
            }
            conexion.Desconectar();
            return ObjFichasLista;
        }

      
        //RETORNA COMPARACION KILOS ASIGNADOS - KILOS SEGUN TIZADO
        public List<DatosTelaEntidad> ComparacionKilos(int ficha, int version,string tela)
        {
            //LISTA
            List<DatosTelaEntidad> objLista = new List<DatosTelaEntidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.spu_comparacionkiloscorteweb", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("i_ficha", ficha));
            comando.Parameters.Add(new OracleParameter("i_version", version));
            comando.Parameters.Add(new OracleParameter("i_teña", tela));

            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objLista.Add(
                    new DatosTelaEntidad { columna4 = registros["kg1"].ToString(), columna5 = registros["kg2"].ToString() ,columna1 = registros["kg3"].ToString()}
                );
            }
            conexion.Desconectar();
            return objLista;
        }


        // LIQUIDACION POR FICHA
        public List<ReporteLiquidacionEntidad> GenerarReporteLiquidacion(string partida)
        {
            List<ReporteLiquidacionEntidad> objLista = new List<ReporteLiquidacionEntidad>();
            OracleCommand comando = new OracleCommand("SYSTEXTILRPT.spu_getreporteliquidacionweb", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();
            comando.Parameters.Add(new OracleParameter("i_partida", partida));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objLista.Add(
                    new ReporteLiquidacionEntidad { 
                            fechaliquidacion    = registros["fechaliquidacion"].ToString(),
                            fichas              = registros["fichas"].ToString(),
                            pedido              = registros["pedido"].ToString(),
                            partida             = registros["partida"].ToString(),
                            programa            = registros["programa"].ToString(),
                            estilo              = registros["estilo"].ToString(),
                            color               = registros["color"].ToString(),
                            descripciontela     = registros["descripciontela"].ToString(),
                            prendasprogramadas  = registros["prendasprogramadas"].ToString(),
                            prendasreales       = registros["prendasreales"].ToString(),
                            diferencia0         = registros["diferencia0"].ToString(),
                            kilosprogramados = registros["kilosprogramados"].ToString(),
                            kilostizados = registros["kilostizados"].ToString(),
                            kilosreales = registros["kilosreales"].ToString(),
                            largotizado = registros["largotizado"].ToString(),
                            largopanoreal = registros["largopanoreal"].ToString(),
                            diferenciametros = registros["diferenciametros"].ToString(),
                            porcentajes = registros["porcentajes"].ToString(),
                            tallas = registros["tallas"].ToString(),
                            panos = registros["panos"].ToString(),
                            pesopanoprogramado = registros["pesopanoprogramado"].ToString(),
                            pesopanoreal = registros["pesopanoreal"].ToString(),
                            consnetoprogramado = registros["consnetoprogramado"].ToString(),
                            consnetoreal = registros["consnetoreal"].ToString(),
                            mermatendprog = registros["mermatendprog"].ToString(),
                            mermatendreal = registros["mermatendreal"].ToString(),
                            conscotiz = registros["conscotiz"].ToString(),
                            consbrutprog = registros["consbrutprog"].ToString(),
                            consbrutoreal = registros["consbrutoreal"].ToString(),
                            diferencia = registros["diferencia"].ToString(),
                            eficprog = registros["eficprog"].ToString(),
                            eficreal = registros["eficreal"].ToString(),
                            diferencia2 = registros["diferencia2"].ToString(),
                            puntas = registros["puntas"].ToString(),
                            retazos = registros["retazos"].ToString(),
                            empalmes = registros["empalmes"].ToString(),
                            mermaprog = registros["mermaprog"].ToString(),
                            mermareal = registros["mermareal"].ToString(),
                            entrecorte = registros["entrecorte"].ToString(),
                            conos = registros["conos"].ToString(),
                            bolsas = registros["bolsas"].ToString(),
                            dev1ra = registros["dev1ra"].ToString(),
                            dev2da = registros["dev2da"].ToString(),
                            anchprog = registros["anchprog"].ToString(),
                            anchreal = registros["anchreal"].ToString(),
                            direfencia3 = registros["direfencia3"].ToString(),
                            anchotizadoutil = registros["anchotizadoutil"].ToString(),
                            difmts = registros["difmts"].ToString(),
                            porcentaje2 = registros["porcentaje2"].ToString(),
                            densprog = registros["densprog"].ToString(),
                            densreal = registros["densreal"].ToString(),
                            diferencia4 = registros["diferencia4"].ToString(),
                            cuadretela = registros["cuadretela"].ToString(),
                            diferencia5 = registros["diferencia5"].ToString(),
                    
                    }
                );
            }
            conexion.Desconectar();
            return objLista;

        }

        //EXPORTAR REPORTE DE LIQUIDACION
        public MemoryStream ExportarReporteLiquidacion(List<ReporteLiquidacionEntidad> listado)
        {
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Hoja1");

                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.Font.Size = 8;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;
                workSheet.Row(1).Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Row(1).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Row(1).Style.Fill.BackgroundColor.SetColor(Color.White);

                workSheet.Cells["A1"].Value = "MODULO";
                workSheet.Cells["B1"].Value = "CODIGO AUTORIZA";
                workSheet.Cells["C1"].Value = "CODIGO";
                workSheet.Cells["D1"].Value = "PROVEEDOR";
                workSheet.Cells["E1"].Value = "FECHA";
                workSheet.Cells["F1"].Value = "CENTRO DE COSTO";
                workSheet.Cells["G1"].Value = "TIPO PAGO";
                workSheet.Cells["H1"].Value = "VALOR SOLES";
                workSheet.Cells["I1"].Value = "VALOR DOLAR";
                workSheet.Cells["J1"].Value = "VALOR DOLAR - CONSUMIDO";

                workSheet.Column(1).Width = 10;     // MODULO
                workSheet.Column(2).Width = 10;     // CODIGO AUTORIZA
                workSheet.Column(3).Width = 10;     // CODIGO
                workSheet.Column(4).Width = 25;     // PROVEEDOR
                workSheet.Column(5).Width = 15;     // FECHA
                workSheet.Column(6).Width = 30;     // CENTRO DE COSTO
                workSheet.Column(7).Width = 25;     // TIPO PAGO
                workSheet.Column(8).Width = 20;     // VALOR SOLES
                workSheet.Column(9).Width = 20;     // VALOR DOLAR
                workSheet.Column(10).Width = 20;     // VALOR DOLAR

                int i = 2;
                /*
                foreach (var item in listado)
                {

                    workSheet.Row(i).Style.Font.Size = 8;

                    workSheet.Cells[i, 1].Value = item.MODULO;
                    workSheet.Cells[i, 2].Value = item.CODAUTORIZA;
                    workSheet.Cells[i, 3].Value = item.CODIGO;
                    workSheet.Cells[i, 4].Value = item.PROVEEDOR;
                    workSheet.Cells[i, 5].Value = item.FECHA;
                    workSheet.Cells[i, 6].Value = item.CC;
                    workSheet.Cells[i, 7].Value = item.TIPO_PAGO;
                    workSheet.Cells[i, 8].Value = item.V_SOLES;
                    workSheet.Cells[i, 9].Value = item.V_DOLAR;
                    workSheet.Cells[i, 10].Value = item.V_CONSUMIDO;

                    i++;
                }*/

                package.Save();
            }

            stream.Position = 0;
            return stream;

        }


    }
}