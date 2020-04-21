using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Logistica.AcumGerCecoCuenta;

namespace TSC_WEB.Models.Modelos.Logistica.AcumGerCecoCuenta
{
    public class AcumGerCecoCuentaModelo
    {
        NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;


        public List<ListaCbxPeriodo> ListarCbxPeriodo()
        {
            List<ListaCbxPeriodo> lista = new List<ListaCbxPeriodo>();
            DBAccess conexion = new DBAccess();

            OracleCommand comando = new OracleCommand("SPU_GET_ACUMGERCECOCUENTA", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("IP_OPCION", 3));
            comando.Parameters.Add(new OracleParameter("IP_PERIODO", 1));
            comando.Parameters.Add(new OracleParameter("IP_GERENCIA", 1));
            comando.Parameters.Add(new OracleParameter("IP_CODCC", 1));
            comando.Parameters.Add(new OracleParameter("IP_CODPLAN", 1));
            comando.Parameters.Add(new OracleParameter("IP_FAMILIA", 1));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            if (registros.HasRows)
            {
                while (registros.Read())
                {
                    lista.Add(
                          new ListaCbxPeriodo
                          {
                              COD_PERIODO = Convert.ToInt32(registros["COD_PERIODO"].ToString()),
                              DESC_PERIODO = registros["DESC_PERIODO"].ToString(),
                          }
                    );
                }
            }
            conexion.Desconectar();
            return lista;
        }

        public List<ListaAcumGerencia> ListarReporteGerencia(int codperiodo)
        {
            List<ListaAcumGerencia> objListar1 = new List<ListaAcumGerencia>();
            DBAccess conexion = new DBAccess();
            decimal presupuesto_total = 0;
            decimal consumido_total = 0;

            OracleCommand comando = new OracleCommand("SPU_GET_ACUMGERCECOCUENTA", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("IP_OPCION", 1));
            comando.Parameters.Add(new OracleParameter("IP_PERIODO", codperiodo));
            comando.Parameters.Add(new OracleParameter("IP_GERENCIA", 1));
            comando.Parameters.Add(new OracleParameter("IP_CODCC", 1));
            comando.Parameters.Add(new OracleParameter("IP_CODPLAN", 1));
            comando.Parameters.Add(new OracleParameter("IP_FAMILIA", 1));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objListar1.Add(
                            new ListaAcumGerencia
                            {
                                PERIODO = registros["PERIODO"].ToString(),
                                GERENCIA = registros["GERENCIA"].ToString(),
                                SIMBOLO = registros["SIMBOLO"].ToString(),
                                PRESUPUESTO = Convert.ToDecimal(registros["PRESUPUESTO"].ToString()).ToString("N", nfi),
                                CONSUMIDO = Convert.ToDecimal(registros["CONSUMIDO"].ToString()).ToString("N", nfi),
                                DISPONIBLE = Convert.ToDecimal(registros["DISPONIBLE"].ToString()).ToString("N", nfi)
                            }
                        );

                presupuesto_total = Math.Round(presupuesto_total, 2) + Convert.ToDecimal(registros["PRESUPUESTO"]);
                consumido_total = Math.Round(consumido_total, 2) + Convert.ToDecimal(registros["CONSUMIDO"]);
            }

            foreach (var item in objListar1)
            {
                item.PRESUPUESTO_TOTAL = presupuesto_total.ToString("N", nfi);
                item.CONSUMIDO_TOTAL = consumido_total.ToString("N", nfi);
                item.DISPONIBLE_TOTAL = (presupuesto_total - consumido_total).ToString("N", nfi);
            }

            conexion.Desconectar();
            return objListar1;
        }

        public List<ListaAcumCeCo> ListarReporteCeCo(int codperiodo, int codcc, string codfamilias)
        {
            List<ListaAcumCeCo> objListar2 = new List<ListaAcumCeCo>();
            DBAccess conexion = new DBAccess();
            decimal presupuesto_total = 0;
            decimal consumido_total = 0;

            OracleCommand comando = new OracleCommand("SPU_GET_ACUMGERCECOCUENTA", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("IP_OPCION", 2));
            comando.Parameters.Add(new OracleParameter("IP_PERIODO", codperiodo));
            comando.Parameters.Add(new OracleParameter("IP_GERENCIA", 1));
            comando.Parameters.Add(new OracleParameter("IP_CODCC", codcc));
            comando.Parameters.Add(new OracleParameter("IP_CODPLAN", 1));
            comando.Parameters.Add(new OracleParameter("IP_FAMILIA", codfamilias));

            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objListar2.Add(new ListaAcumCeCo
                {
                    PERIODO = registros["PERIODO"].ToString(),
                    GERENCIA = registros["GERENCIA"].ToString(),
                    SIMBOLO = registros["SIMBOLO"].ToString(),

                    CODCC = registros["CODCC"].ToString(),
                    CC = registros["CC"].ToString(),

                    COD_PLANCONT = registros["COD_PLANCONT"].ToString(),
                    DESC_PLANCONT = registros["DESC_PLANCONT"].ToString(),


                    PRESUPUESTO = Convert.ToDecimal(registros["PRESUPUESTO"].ToString()).ToString("N", nfi),
                    CONSUMIDO = Convert.ToDecimal(registros["CONSUMIDO"].ToString()).ToString("N", nfi),
                    DISPONIBLE = Convert.ToDecimal(registros["DISPONIBLE"].ToString()).ToString("N", nfi),

                    PRESUPUESTO_V = Math.Round(Convert.ToDecimal(registros["PRESUPUESTO"]), 2),
                    CONSUMIDO_V = Math.Round(Convert.ToDecimal(registros["CONSUMIDO"]), 2),
                    DISPONIBLE_V = Math.Round(Convert.ToDecimal(registros["DISPONIBLE"]), 2)
                });

                presupuesto_total = Math.Round(presupuesto_total, 2) + Convert.ToDecimal(registros["PRESUPUESTO"]);
                consumido_total = Math.Round(consumido_total, 2) + Convert.ToDecimal(registros["CONSUMIDO"]);
            }

            foreach (var item in objListar2)
            {
                item.PRESUPUESTO_TOTAL = presupuesto_total.ToString("N", nfi);
                item.CONSUMIDO_TOTAL = consumido_total.ToString("N", nfi);
                item.DISPONIBLE_TOTAL = (presupuesto_total - consumido_total).ToString("N", nfi);
            }

            conexion.Desconectar();
            return objListar2;
        }

        public List<ListaCbxGerencia> ListarCbxGerencias()
        {
            List<ListaCbxGerencia> listarOCGerencias = new List<ListaCbxGerencia>();
            DBAccess conexion = new DBAccess();

            OracleCommand comando = new OracleCommand("SPU_GET_ACUMGERCECOCUENTA", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("I_OPCION", 4));
            comando.Parameters.Add(new OracleParameter("IP_PERIODO", 1));
            comando.Parameters.Add(new OracleParameter("IP_GERENCIA", 1));
            comando.Parameters.Add(new OracleParameter("IP_CODCC", 1));
            comando.Parameters.Add(new OracleParameter("IP_CODPLAN", 1));
            comando.Parameters.Add(new OracleParameter("IP_FAMILIA", 1));

            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            if (registros.HasRows)
            {
                while (registros.Read())
                {
                    listarOCGerencias.Add(
                          new ListaCbxGerencia
                          {
                              CodGerencia = Convert.ToInt32(registros["COD_GER"]),
                              DescGerencia = registros["DESC_GER"].ToString(),
                          }
                    );
                }
            }
            conexion.Desconectar();
            return listarOCGerencias;
        }

        public List<ListaCbxCeCo> ListarCbxCentroCosto(int codperiodo, int codgerencia)
        {
            List<ListaCbxCeCo> listarOCCC = new List<ListaCbxCeCo>();
            DBAccess conexion = new DBAccess();

            OracleCommand comando = new OracleCommand("SPU_GET_ACUMGERCECOCUENTA", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("I_OPCION", 5));
            comando.Parameters.Add(new OracleParameter("IP_PERIODO", codperiodo));
            comando.Parameters.Add(new OracleParameter("IP_GERENCIA", codgerencia));
            comando.Parameters.Add(new OracleParameter("IP_CODCC", 1));
            comando.Parameters.Add(new OracleParameter("IP_CODPLAN", 1));
            comando.Parameters.Add(new OracleParameter("IP_FAMILIA", 1));

            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            if (registros.HasRows)
            {
                while (registros.Read())
                {
                    listarOCCC.Add(
                          new ListaCbxCeCo
                          {
                              COD_CC = Convert.ToInt32(registros["COD_CC"]),
                              DESC_CC = registros["DESC_CC"].ToString(),
                          }
                    );
                }
            }
            conexion.Desconectar();
            return listarOCCC;
        }

        public List<ListaCbxFamilia> ListarCbxFamilias(int codperiodo, int codcc)
        {
            List<ListaCbxFamilia> listado = new List<ListaCbxFamilia>();
            DBAccess conexion = new DBAccess();

            OracleCommand comando = new OracleCommand("SPU_GET_ACUMGERCECOCUENTA", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("I_OPCION", 6));
            comando.Parameters.Add(new OracleParameter("IP_PERIODO", codperiodo));
            comando.Parameters.Add(new OracleParameter("IP_GERENCIA", 1));
            comando.Parameters.Add(new OracleParameter("IP_CODCC", codcc));
            comando.Parameters.Add(new OracleParameter("IP_CODPLAN", 1));
            comando.Parameters.Add(new OracleParameter("IP_FAMILIA", 1));

            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            if (registros.HasRows)
            {
                while (registros.Read())
                {
                    listado.Add(new ListaCbxFamilia
                    {
                        CODFAMILIA = Convert.ToInt32(registros["CODFAMILIA"]),
                        FAMILIA = registros["FAMILIA"].ToString(),
                    });
                }
            }
            conexion.Desconectar();
            return listado;
        }

        public List<ListaRep2> ListarAcumuladoXCC(int codperiodo, int codgerencia)
        {
            List<ListaRep2> objListar2 = new List<ListaRep2>();
            DBAccess conexion = new DBAccess();

            decimal presupuesto_total = 0;
            decimal consumido_total = 0;

            OracleCommand comando = new OracleCommand("SPU_GET_ACUMGERCECOCUENTA", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("I_OPCION", 7));
            comando.Parameters.Add(new OracleParameter("IP_PERIODO", codperiodo));
            comando.Parameters.Add(new OracleParameter("IP_GERENCIA", codgerencia));
            comando.Parameters.Add(new OracleParameter("IP_CODCC", 1));
            comando.Parameters.Add(new OracleParameter("IP_CODPLAN", 1));
            comando.Parameters.Add(new OracleParameter("IP_FAMILIA", 1));

            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objListar2.Add(
                            new ListaRep2
                            {
                                PERIODO = registros["PERIODO"].ToString(),
                                GERENCIA = registros["GERENCIA"].ToString(),
                                SIMBOLO = registros["SIMBOLO"].ToString(),

                                CODCC = registros["CODCC"].ToString(),
                                CC = registros["CC"].ToString(),

                                PRESUPUESTO = Convert.ToDecimal(registros["PRESUPUESTO"].ToString()).ToString("N", nfi),
                                CONSUMIDO = Convert.ToDecimal(registros["CONSUMIDO"].ToString()).ToString("N", nfi),
                                DISPONIBLE = Convert.ToDecimal(registros["DISPONIBLE"].ToString()).ToString("N", nfi)
                            }
                        );

                presupuesto_total = Math.Round(presupuesto_total, 2) + Convert.ToDecimal(registros["PRESUPUESTO"]);
                consumido_total = Math.Round(consumido_total, 2) + Convert.ToDecimal(registros["CONSUMIDO"]);
            }

            foreach (var item in objListar2)
            {
                item.PRESUPUESTO_TOTAL = presupuesto_total.ToString("N", nfi);
                item.CONSUMIDO_TOTAL = consumido_total.ToString("N", nfi);
                item.DISPONIBLE_TOTAL = (presupuesto_total - consumido_total).ToString("N", nfi);
            }

            conexion.Desconectar();
            return objListar2;
        }


    }
}