/*using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Logistica.Visualizar;*/
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.Logistica.Visualizar;

namespace TSC_WEB.Models.Modelos.Logistica.Visualizar
{
    public class VisualizarOcModelo
    {
        NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
        DBAccess conexion = new DBAccess();


        //LISTA TODAS LA OC QUE PUEDEN PARA SER ALTERADAS DE SITUACION
        public List<ListaReporte1> ListarReporteOC_1(int codperiodo)
        {
            List<ListaReporte1> objListar1 = new List<ListaReporte1>();
            decimal presupuesto_total = 0;
            decimal consumido_total = 0;

            OracleCommand comando = new OracleCommand("SPU_GET_ALTERARSIT_REP", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("IP_OPCION", "1"));
            comando.Parameters.Add(new OracleParameter("IP_PERIODO", codperiodo));
            comando.Parameters.Add(new OracleParameter("IP_GERENCIA", 1));
            comando.Parameters.Add(new OracleParameter("IP_CODCC", 1));
            comando.Parameters.Add(new OracleParameter("IP_CODPLAN", 1));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objListar1.Add(
                            new ListaReporte1
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



        public List<ListaReporte2> ListarReporteOC_2(int codperiodo, int codgerencia)
        {
            List<ListaReporte2> objListar2 = new List<ListaReporte2>();
            decimal presupuesto_total = 0;
            decimal consumido_total = 0;

            OracleCommand comando = new OracleCommand("SPU_GET_ALTERARSIT_REP", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("IP_OPCION", "2"));
            comando.Parameters.Add(new OracleParameter("IP_PERIODO", codperiodo));
            comando.Parameters.Add(new OracleParameter("IP_GERENCIA", codgerencia));
            comando.Parameters.Add(new OracleParameter("IP_CODCC", 1));
            comando.Parameters.Add(new OracleParameter("IP_CODPLAN", 1));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objListar2.Add(
                            new ListaReporte2
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


        public List<ListaReporte3> ListarReporteOC_3(int codperiodo,
                                                     int codgerencia,
                                                     int codcc)
        {
            List<ListaReporte3> objListar3 = new List<ListaReporte3>();
            decimal presupuesto_total = 0;
            decimal consumido_total = 0;

            OracleCommand comando = new OracleCommand("SPU_GET_ALTERARSIT_REP", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("IP_OPCION", "3"));
            comando.Parameters.Add(new OracleParameter("IP_PERIODO", codperiodo));
            comando.Parameters.Add(new OracleParameter("IP_GERENCIA", codgerencia));
            comando.Parameters.Add(new OracleParameter("IP_CODCC", codcc));
            comando.Parameters.Add(new OracleParameter("IP_CODPLAN", 1));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objListar3.Add(
                            new ListaReporte3
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
                                DISPONIBLE = Convert.ToDecimal(registros["DISPONIBLE"].ToString()).ToString("N", nfi)
                            }
                        );

                presupuesto_total = Math.Round(presupuesto_total, 2) + Convert.ToDecimal(registros["PRESUPUESTO"]);
                consumido_total = Math.Round(consumido_total, 2) + Convert.ToDecimal(registros["CONSUMIDO"]);
            }

            foreach (var item in objListar3)
            {
                item.PRESUPUESTO_TOTAL = presupuesto_total.ToString("N", nfi);
                item.CONSUMIDO_TOTAL = consumido_total.ToString("N", nfi);
                item.DISPONIBLE_TOTAL = (presupuesto_total - consumido_total).ToString("N", nfi);
            }

            conexion.Desconectar();
            return objListar3;
        }



        public List<ListaOCCentroCosto> ListarCbxCentroCostos(int codigoperiodo, int codigogerencia)
        {
            List<ListaOCCentroCosto> lista = new List<ListaOCCentroCosto>();

            OracleCommand comando = new OracleCommand("SPU_GET_OC_ALTERSITUACION", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("I_OPCION", "4"));
            comando.Parameters.Add(new OracleParameter("I_CH_FILTRO_1", ""));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_1", codigoperiodo));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_2", codigogerencia));
            comando.Parameters.Add(new OracleParameter("I_I_FILTRO_3", 1));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            if (registros.HasRows)
            {
                while (registros.Read())
                {
                    lista.Add(
                          new ListaOCCentroCosto
                          {
                              CodCC = registros["COD_CC"].ToString(),
                              DescCC = registros["DESC_CC"].ToString(),
                          }
                    );
                }
            }

            conexion.Desconectar();
            return lista;
        }


        public List<ListaReporte3_Detalle> ListarReporteOC_3_Detalle(int codperiodo,
                                                             int codcc,
                                                             int codplancont)
        {
            List<ListaReporte3_Detalle> objListar3 = new List<ListaReporte3_Detalle>();

            OracleCommand comando = new OracleCommand("SPU_GET_ALTERARSIT_REP", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("IP_OPCION", "4"));
            comando.Parameters.Add(new OracleParameter("IP_PERIODO", codperiodo));
            comando.Parameters.Add(new OracleParameter("IP_GERENCIA", 1));
            comando.Parameters.Add(new OracleParameter("IP_CODCC", codcc));
            comando.Parameters.Add(new OracleParameter("IP_CODPLAN", codplancont));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objListar3.Add(
                            new ListaReporte3_Detalle
                            {
                                CODAUTORIZA = registros["CODAUTORIZA"].ToString(),
                                PEDIDO = registros["PEDIDO"].ToString(),
                                FECHA = Convert.ToDateTime(registros["FECHA"].ToString()).ToShortDateString(),
                                DESCRIPCIONPAGO = registros["DESCRIPCIONPAGO"].ToString(),
                                TOTAL_PEDIDO = Convert.ToDecimal(registros["TOTAL_PEDIDO"].ToString()).ToString("N", nfi),
                                PROVEEDOR = registros["PROVEEDOR"].ToString(),
                                COMPRADOR = registros["COMPRADOR"].ToString(),
                                CC = registros["CC"].ToString(),
                                SIMBOLO = registros["SIMBOLO"].ToString()
                            }
                        );
            }


            conexion.Desconectar();
            return objListar3;
        }

        public List<ListaReporte5> ListaExcel_OC_5(int codperiodo)
        {
            List<ListaReporte5> objListar5 = new List<ListaReporte5>();

            OracleCommand comando = new OracleCommand("SPU_GET_ALTERARSIT_REP", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("IP_OPCION", "5"));
            comando.Parameters.Add(new OracleParameter("IP_PERIODO", codperiodo));
            comando.Parameters.Add(new OracleParameter("IP_GERENCIA", 1));
            comando.Parameters.Add(new OracleParameter("IP_CODCC", 1));
            comando.Parameters.Add(new OracleParameter("IP_CODPLAN", 1));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader();

            while (registros.Read())
            {
                objListar5.Add(
                            new ListaReporte5
                            {
                                CODAUTORIZA = registros["CODAUTORIZA"].ToString(),
                                PEDIDO = registros["PEDIDO"].ToString(),
                                PLAN_CONT = registros["PLAN_CONT"].ToString(),
                                FECHA = Convert.ToDateTime(registros["FECHA"].ToString()).ToShortDateString(),
                                DESCRIPCIONPAGO = registros["DESCRIPCIONPAGO"].ToString(),
                                TOTAL_PEDIDO = Convert.ToDecimal(registros["TOTAL_PEDIDO"]),
                                PROVEEDOR = registros["PROVEEDOR"].ToString(),
                                COMPRADOR = registros["COMPRADOR"].ToString(),
                                CC = registros["CC"].ToString(),
                                SIMBOLO = registros["SIMBOLO"].ToString(),
                                VALOR_DOLAR = Convert.ToDecimal(registros["VALOR_DOLAR"]),
                                ESTADO = registros["ESTADO"].ToString()
                            }
                        );
            }


            conexion.Desconectar();
            return objListar5;
        }




        public MemoryStream ExportarExcelOC(List<ListaReporte5> listado)
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

                workSheet.Cells["A1"].Value = "CODIGO AUTORIZACION";
                workSheet.Cells["B1"].Value = "ESTADO OC";
                workSheet.Cells["C1"].Value = "PEDIDO COMPRA";
                workSheet.Cells["D1"].Value = "FECHA";
                workSheet.Cells["E1"].Value = "COD. PLAN CONTABLE";
                workSheet.Cells["F1"].Value = "CENTRO DE COSTO";
                workSheet.Cells["G1"].Value = "TIPO PAGO";
                workSheet.Cells["H1"].Value = "PROVEEDOR";
                workSheet.Cells["I1"].Value = "COMPRADOR";
                workSheet.Cells["J1"].Value = "SIMBOLO";
                workSheet.Cells["K1"].Value = "VALOR OC";
                workSheet.Cells["L1"].Value = "VALOR U$";

                workSheet.Column(1).Width = 10;      // CODAUTORIZA;
                workSheet.Column(2).Width = 10;     // ESTADO;
                workSheet.Column(3).Width = 10;     // PEDIDO;
                workSheet.Column(4).Width = 10;     // FECHA;
                workSheet.Column(5).Width = 20;     // PLAN_CONT;
                workSheet.Column(6).Width = 10;     // CC;
                workSheet.Column(7).Width = 25;     // DESCRIPCIONPAGO
                workSheet.Column(8).Width = 25;     // PROVEEDOR;
                workSheet.Column(9).Width = 25;     // COMPRADOR;
                workSheet.Column(10).Width = 5;    // SIMBOLO;
                workSheet.Column(11).Width = 10;    // TOTAL_PEDIDO;
                workSheet.Column(12).Width = 10;    // VALOR_DOLAR;

                int i = 2;

                foreach (var item in listado)
                {

                    workSheet.Row(i).Style.Font.Size = 8;

                    workSheet.Cells[i, 1].Value = item.CODAUTORIZA;
                    workSheet.Cells[i, 2].Value = item.ESTADO;
                    workSheet.Cells[i, 3].Value = item.PEDIDO;
                    workSheet.Cells[i, 4].Value = item.FECHA;
                    workSheet.Cells[i, 5].Value = item.PLAN_CONT;
                    workSheet.Cells[i, 6].Value = item.CC;
                    workSheet.Cells[i, 7].Value = item.DESCRIPCIONPAGO;
                    workSheet.Cells[i, 8].Value = item.PROVEEDOR;
                    workSheet.Cells[i, 9].Value = item.COMPRADOR;
                    workSheet.Cells[i, 10].Value = item.SIMBOLO;
                    workSheet.Cells[i, 11].Value = item.TOTAL_PEDIDO;
                    workSheet.Cells[i, 12].Value = item.VALOR_DOLAR;

                    i++;
                }

                package.Save();
            }



            stream.Position = 0;
            return stream;

        }


    }
}