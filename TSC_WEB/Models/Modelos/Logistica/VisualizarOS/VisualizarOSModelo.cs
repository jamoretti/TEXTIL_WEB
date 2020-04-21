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
using TSC_WEB.Models.Entidades.Logistica.VisualizarOS;
using TSC_WEB.Models.Entidades.Sistema;

namespace TSC_WEB.Models.Modelos.Logistica.VisualizarOS
{
    public class VisualizarOSModelo
    {
        NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
        DBAccess conexion = new DBAccess();


        //LISTA TODAS LA OC QUE PUEDEN PARA SER ALTERADAS DE SITUACION
        public List<ListaReporte1> ListarReporteOS_1(int codperiodo)
        {
            List<ListaReporte1> objListar1 = new List<ListaReporte1>();
            decimal presupuesto_total = 0;
            decimal consumido_total = 0;

            OracleCommand comando = new OracleCommand("SPU_GET_VISUALIZAR_OS", conexion.Acceder());
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



        public List<ListaReporte2> ListarReporteOS_2(int codperiodo, int codgerencia)
        {
            List<ListaReporte2> objListar2 = new List<ListaReporte2>();
            decimal presupuesto_total = 0;
            decimal consumido_total = 0;

            OracleCommand comando = new OracleCommand("SPU_GET_VISUALIZAR_OS", conexion.Acceder());
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


        public List<ListaReporte3> ListarReporteOS_3(int codperiodo,
                                                     int codgerencia,
                                                     int codcc)
        {
            List<ListaReporte3> objListar3 = new List<ListaReporte3>();
            decimal presupuesto_total = 0;
            decimal consumido_total = 0;

            OracleCommand comando = new OracleCommand("SPU_GET_VISUALIZAR_OS", conexion.Acceder());
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



        public List<ListaOSCentroCosto> ListarCbxCentroCostos(int codigoperiodo, int codigogerencia)
        {
            List<ListaOSCentroCosto> lista = new List<ListaOSCentroCosto>();

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
                          new ListaOSCentroCosto
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


        public List<ListaReporteOS3_Detalle> ListarReporteOS_3_Detalle(int codperiodo,
                                                             int codcc,
                                                             int codplancont)
        {
            List<ListaReporteOS3_Detalle> objListar3 = new List<ListaReporteOS3_Detalle>();

            OracleCommand comando = new OracleCommand("SPU_GET_VISUALIZAR_OS", conexion.Acceder());
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
                            new ListaReporteOS3_Detalle
                            {

                                NUMERO_ORDEM = registros["NUMERO_ORDEM"].ToString(),
                                PROVEEDOR = registros["PROVEEDOR"].ToString(),
                                SERVICIO = registros["SERVICIO"].ToString(),
                                SIMBOLO = registros["SIMBOLO"].ToString(),
                                TIPO_PAGO = registros["TIPO_PAGO"].ToString(),
                                FECHA = Convert.ToDateTime(registros["FECHA"].ToString()).ToShortDateString(),
                                CANTIDAD = registros["CANTIDAD"].ToString(),
                                TOTAL = Convert.ToDecimal(registros["TOTAL"].ToString()).ToString("N", nfi),
                                ESTADO = registros["ESTADO"].ToString(),

                            }
                        );
            }


            conexion.Desconectar();
            return objListar3;
        }




        public List<ListaReporteExcel> ListarRepExcel(int codperiodo)
        {
            List<ListaReporteExcel> objListar3 = new List<ListaReporteExcel>();

            OracleCommand comando = new OracleCommand("SPU_GET_VISUALIZAR_OS", conexion.Acceder());
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
                objListar3.Add(
                            new ListaReporteExcel
                            {

                                NUMERO_ORDEM = registros["NUMERO_ORDEM"].ToString(),
                                COD_CC = registros["COD_CC"].ToString(),
                                PLAN_CONT = registros["PLAN_CONT"].ToString(),
                                PROVEEDOR = registros["PROVEEDOR"].ToString(),
                                SERVICIO = registros["SERVICIO"].ToString(),
                                SIMBOLO = registros["SIMBOLO"].ToString(),
                                TIPO_PAGO = registros["TIPO_PAGO"].ToString(),
                                FECHA = Convert.ToDateTime(registros["FECHA"].ToString()).ToShortDateString(),
                                CANTIDAD = Convert.ToInt32(registros["CANTIDAD"]),
                                TOTAL = Convert.ToDecimal(registros["TOTAL"]),
                                ESTADO = registros["ESTADO"].ToString(),

                            }
                        );
            }


            conexion.Desconectar();
            return objListar3;
        }


        public MemoryStream ExportarExcel(List<ListaReporteExcel> listado)
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

                workSheet.Cells["A1"].Value = "NUMERO ORDEN";
                workSheet.Cells["B1"].Value = "FECHA";
                workSheet.Cells["C1"].Value = "ESTADO";
                workSheet.Cells["D1"].Value = "CENTRO DE COSTO";
                workSheet.Cells["E1"].Value = "PLAN CONTABLE";
                workSheet.Cells["F1"].Value = "PROVEEDOR";
                workSheet.Cells["G1"].Value = "SERVICIO";
                workSheet.Cells["H1"].Value = "TIPO PAGO";
                workSheet.Cells["I1"].Value = "CANTIDAD";
                workSheet.Cells["J1"].Value = "SIMBOLO";
                workSheet.Cells["K1"].Value = "TOTAL";
                

                workSheet.Column(1).Width = 10;      // NUMERO ORDEN"
                workSheet.Column(2).Width = 13;     // FECHA
                workSheet.Column(3).Width = 15;     // ESTADO
                workSheet.Column(4).Width = 15;     // CENTRO DE COSTO"
                workSheet.Column(5).Width = 25;     // PLAN CONTABLE";
                workSheet.Column(6).Width = 30;     // PROVEEDOR";
                workSheet.Column(7).Width = 30;     // SERVICIO";
                workSheet.Column(8).Width = 20;     // TIPO PAGO";
                workSheet.Column(9).Width = 10;     // CANTIDAD";
                workSheet.Column(10).Width = 10;    // SIMBOLO";
                workSheet.Column(11).Width = 10;    // TOTAL";

                int i = 2;

                foreach (var item in listado)
                {

                    workSheet.Row(i).Style.Font.Size = 8;

                    workSheet.Cells[i, 1].Value = item.NUMERO_ORDEM;
                    workSheet.Cells[i, 2].Value = item.FECHA;
                    workSheet.Cells[i, 3].Value = item.ESTADO;
                    workSheet.Cells[i, 4].Value = item.COD_CC;
                    workSheet.Cells[i, 5].Value = item.PLAN_CONT;
                    workSheet.Cells[i, 6].Value = item.PROVEEDOR;
                    workSheet.Cells[i, 7].Value = item.SERVICIO;
                    workSheet.Cells[i, 8].Value = item.TIPO_PAGO;
                    workSheet.Cells[i, 9].Value = item.CANTIDAD;
                    workSheet.Cells[i, 10].Value = item.SIMBOLO;
                    workSheet.Cells[i, 11].Value = item.TOTAL;

                    i++;
                }

                package.Save();
            }



            stream.Position = 0;
            return stream;

        }

        
        public RespuestaOperacion ActualizarOS()
        {
            RespuestaOperacion respuesta = new RespuestaOperacion();
            DBAccess conexion = new DBAccess();
            try
            {
                OracleCommand comando = new OracleCommand("SPU_SET_ACTUALIZAR_OS", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();

                comando.Parameters.Add(new OracleParameter("o_cursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

                OracleDataReader registros = comando.ExecuteReader();

                if (registros.HasRows)
                {
                    while (registros.Read())
                    {
                        respuesta.id_status = Convert.ToInt32(registros["id_status"]);
                        respuesta.desc_status = registros["desc_status"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                respuesta.id_status = 0;
                respuesta.desc_status = e.Message;
            }

            conexion.Desconectar();
            return respuesta;
        }



        


    }
}