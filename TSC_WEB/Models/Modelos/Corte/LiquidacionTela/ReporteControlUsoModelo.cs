using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.Corte.LiquidacionTela;

namespace TSC_WEB.Models.Modelos.Corte.LiquidacionTela
{
    public class ReporteControlUsoModelo
    {
        DBAccess conexion = new DBAccess();

        //LISTA REPORTE
        public List<ReporteControlUsoEntidad> Listar(string i_fechainicio,string i_fechafin,string i_partida,int? i_ficha)
        {
            List<ReporteControlUsoEntidad> objLista = new List<ReporteControlUsoEntidad>();

            OracleCommand comando = new OracleCommand("systextilrpt.spu_getcontrolusoliquidacion",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("i_fechainicio", i_fechainicio == string.Empty ? null : i_fechainicio));
            comando.Parameters.Add(new OracleParameter("i_fechafin", i_fechafin == string.Empty ? null : i_fechafin));
            comando.Parameters.Add(new OracleParameter("i_partida", i_partida == string.Empty ? null : i_partida));
            comando.Parameters.Add(new OracleParameter("i_ficha", i_ficha));
            comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objLista.Add(
                    new ReporteControlUsoEntidad
                    {
                        partida = registros["partida"].ToString(),
                        ficha = Convert.ToInt32(registros["ficha"].ToString()),
                        fecha = registros["fecha"].ToString(),
                        tizado = registros["tizado"].ToString(),
                        tendido = registros["tendido"].ToString(),
                        corte = registros["corte"].ToString(),
                        cantidadprogramada = Convert.ToInt32(registros["cantidadprogramada"].ToString()),
                        version = Convert.ToInt32(registros["versiones"].ToString()),

                    }
                );
            }

            conexion.Desconectar();

            return objLista;

        }

        //LISTA REPORTE COMSUMO 2
        public DataSet Listar2(int semana1, int semana2)
        {
            OracleCommand comando = new OracleCommand("systextilrpt.spu_getcontrolusoliquidacion2", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();


            comando.Parameters.Add(new OracleParameter("i_semanainicio",semana1));
            comando.Parameters.Add(new OracleParameter("i_semanafin", semana2));
            comando.Parameters.Add(new OracleParameter("o_cursor1", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            comando.Parameters.Add(new OracleParameter("o_cursor2", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            DataSet resultado = new DataSet();
            OracleDataAdapter adapter = new OracleDataAdapter(comando);
            adapter.Fill(resultado);
            conexion.Desconectar();

            return resultado;
        }

    }
}