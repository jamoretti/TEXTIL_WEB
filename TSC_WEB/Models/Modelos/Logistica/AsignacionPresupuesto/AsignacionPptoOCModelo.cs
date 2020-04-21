using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Sistema;

namespace TSC_WEB.Models.Modelos.Logistica.AsignacionPresupuesto
{
    public class AsignacionPptoOCModelo
    {
        NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

        DBAccess conexion = new DBAccess();
        SistemaEntidad objSistemaE = new SistemaEntidad();

        //public List<ListaOCCentroCosto> ListarCbxCentroCostos(int codigoperiodo, int codigogerencia)
        //{
        //    List<ListaOCCentroCosto> lista = new List<ListaOCCentroCosto>();

        //    OracleCommand comando = new OracleCommand("SPU_GET_OC_ALTERSITUACION", conexion.Acceder());
        //    comando.CommandType = CommandType.StoredProcedure;
        //    conexion.Conectar();

        //    comando.Parameters.Add(new OracleParameter("I_OPCION", "4"));
        //    comando.Parameters.Add(new OracleParameter("I_CH_FILTRO_1", ""));
        //    comando.Parameters.Add(new OracleParameter("I_I_FILTRO_1", codigoperiodo));
        //    comando.Parameters.Add(new OracleParameter("I_I_FILTRO_2", codigogerencia));
        //    comando.Parameters.Add(new OracleParameter("I_I_FILTRO_3", 1));
        //    comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

        //    OracleDataReader registros = comando.ExecuteReader();

        //    if (registros.HasRows)
        //    {
        //        while (registros.Read())
        //        {
        //            lista.Add(
        //                  new ListaOCCentroCosto
        //                  {
        //                      CodCC = registros["COD_CC"].ToString(),
        //                      DescCC = registros["DESC_CC"].ToString(),
        //                  }
        //            );
        //        }
        //    }

        //    conexion.Desconectar();
        //    return lista;
        //}


    }
}




































































































