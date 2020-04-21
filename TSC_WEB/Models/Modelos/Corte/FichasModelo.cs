using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Config;
using System.Data;
using TSC_WEB.Models.Entidades.Corte;

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
                });
            }
            conexion.Desconectar();
            return ObjFichasLista;
        }

        public bool RegistrarCorte006(string PARTIDA, decimal MER_PUNTAS, decimal MER_RETAZOSMAS, decimal MER_RETAZOSMEN, decimal EMPALMES, decimal DEVO_PRIMERA, decimal DEVO_SEGUNDA, decimal CONOS, decimal PLASTICO, string U_REGISTRO, string VERSION, string TELA)
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

      
    }
}