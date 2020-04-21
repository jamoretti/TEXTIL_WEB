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
using TSC_WEB.Models.Entidades.Logistica.AcumuladoXCuenta;

namespace TSC_WEB.Models.Modelos.Logistica.AcumuladoXCuenta
{
    public class AcumuladoXCuentaModelo
    {
        NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;


        public List<ListaSeccion> ListarSeccionMult(int codperiodo)
        {
            List<ListaSeccion> lista = new List<ListaSeccion>();
            DBAccess conexion = new DBAccess();

            OracleCommand comando = new OracleCommand("SPU_GET_AcumuladoXCuenta", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("IP_OPCION", 2));
            comando.Parameters.Add(new OracleParameter("IP_CODPERIODO", codperiodo));
            comando.Parameters.Add(new OracleParameter("CH_CODSECCIONES", null));
            comando.Parameters.Add(new OracleParameter("CH_CODFAMILIAS", null));
            comando.Parameters.Add(new OracleParameter("CH_CODCUENTAS", null));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader(CommandBehavior.SingleResult);

            if (registros.HasRows)
            {
                while (registros.Read())
                {
                    lista.Add(new ListaSeccion
                    {
                        CODSECCION = Convert.ToInt32(registros["CODSECCION"].ToString()),
                        SECCION = registros["SECCION"].ToString()
                    });
                }
            }
            conexion.Desconectar();
            return lista;
        }

        public List<ListaFamilia> ListarFamiliaMult(int codperiodo, string codseccion)
        {
            List<ListaFamilia> lista = new List<ListaFamilia>();
            DBAccess conexion = new DBAccess();

            OracleCommand comando = new OracleCommand("SPU_GET_AcumuladoXCuenta", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("IP_OPCION", 3));
            comando.Parameters.Add(new OracleParameter("IP_CODPERIODO", codperiodo));
            comando.Parameters.Add(new OracleParameter("CH_CODSECCIONES", codseccion));
            comando.Parameters.Add(new OracleParameter("CH_CODFAMILIAS", null));
            comando.Parameters.Add(new OracleParameter("CH_CODCUENTAS", null));
            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;


            OracleDataReader registros = comando.ExecuteReader(CommandBehavior.SingleResult);

            if (registros.HasRows)
            {
                while (registros.Read())
                {
                    lista.Add(new ListaFamilia
                    {
                        CODFAMILIA = Convert.ToInt32(registros["CODFAMILIA"].ToString()),
                        FAMILIA = registros["FAMILIA"].ToString()
                    });
                }
            }
            conexion.Desconectar();
            return lista;
        }

        public List<ListaCuenta> ListarCuentaMult(int codperiodo, string codseccion, string codfamilia)
        {
            List<ListaCuenta> lista = new List<ListaCuenta>();
            DBAccess conexion = new DBAccess();

            OracleCommand comando = new OracleCommand("SPU_GET_AcumuladoXCuenta", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("IP_OPCION", 4));

            comando.Parameters.Add(new OracleParameter("IP_CODPERIODO", codperiodo));
            comando.Parameters.Add(new OracleParameter("CH_CODSECCIONES", codseccion));
            comando.Parameters.Add(new OracleParameter("CH_CODFAMILIAS", codfamilia));
            comando.Parameters.Add(new OracleParameter("CH_CODCUENTAS", null));


            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader(CommandBehavior.SingleResult);

            if (registros.HasRows)
            {
                while (registros.Read())
                {
                    lista.Add(new ListaCuenta
                    {
                        CODPLACONT = Convert.ToInt32(registros["CODPLACONT"].ToString()),
                        PLANCONT = registros["PLANCONT"].ToString()
                    });

                }
            }
            conexion.Desconectar();
            return lista;
        }

        public IndicadoresPresupuesto ListarIndicadoresMult(int codperiodo, string codseccion, string codfamilia, string codcuenta)
        {

            IndicadoresPresupuesto entidad = new IndicadoresPresupuesto();
            DBAccess conexion = new DBAccess();

            OracleCommand comando = new OracleCommand("SPU_GET_AcumuladoXCuenta", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("IP_OPCION", 5));

            comando.Parameters.Add(new OracleParameter("IP_CODPERIODO", codperiodo));
            comando.Parameters.Add(new OracleParameter("CH_CODSECCIONES", codseccion));
            comando.Parameters.Add(new OracleParameter("CH_CODFAMILIAS", codfamilia));
            comando.Parameters.Add(new OracleParameter("CH_CODCUENTAS", codcuenta));

            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader(CommandBehavior.SingleResult);

            if (registros.HasRows)
            {
                if (registros.Read())
                {
                    entidad = new IndicadoresPresupuesto();
                    entidad.PRESUPUESTO = Convert.ToDecimal(registros["PRESUPUESTO"]);
                    entidad.CONSUMIDO = Convert.ToDecimal(registros["CONSUMIDO"]);
                    entidad.DISPONIBLE = Convert.ToDecimal(registros["DISPONIBLE"]);
                    entidad.SIMBOLO = registros["SIMBOLO"].ToString();
                }
            }


            conexion.Desconectar();
            return entidad;
        }

        public List<ListaAcumulado> ListarAcumuladoMultiple(int codperiodos, string codsecciones, string codfamilias, string codcuentas)
        {
            List<ListaAcumulado> lista = new List<ListaAcumulado>();
            DBAccess conexion = new DBAccess();

            OracleCommand comando = new OracleCommand("SPU_GET_AcumuladoXCuenta", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("IP_OPCION", 1));

            comando.Parameters.Add(new OracleParameter("IP_CODPERIODO", codperiodos));
            comando.Parameters.Add(new OracleParameter("CH_CODSECCIONES", codsecciones));
            comando.Parameters.Add(new OracleParameter("CH_CODFAMILIAS", codfamilias));
            comando.Parameters.Add(new OracleParameter("CH_CODCUENTAS", codcuentas));

            comando.Parameters.Add(new OracleParameter("O_CURSOR", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            OracleDataReader registros = comando.ExecuteReader(CommandBehavior.SingleResult);

            if (registros.HasRows)
            {
                while (registros.Read())
                {
                    lista.Add(new ListaAcumulado
                    {
                        MODULO = registros["MODULO"].ToString(),
                        CODAUTORIZA = registros["CODAUTORIZA"].ToString(),
                        CODIGO = registros["CODIGO"].ToString(),
                        PROVEEDOR = registros["PROVEEDOR"].ToString(),
                        FECHA = registros["FECHA"].ToString(),
                        SIMBOLO = registros["SIMBOLO"].ToString(),
                        SIMBOLO_S = registros["SIMBOLO_S"].ToString(),
                        SIMBOLO_D = registros["SIMBOLO_D"].ToString(),
                        CC = registros["CC"].ToString(),
                        TIPO_PAGO = registros["TIPO_PAGO"].ToString(),
                        VALOR_SOLES = Convert.ToDecimal(registros["VALOR_SOLES"].ToString()).ToString("N", nfi),
                        VALOR_DOLAR = Convert.ToDecimal(registros["VALOR_DOLAR"].ToString()).ToString("N", nfi),
                        VALOR_CONSUMIDO = Convert.ToDecimal(registros["VALOR_CONSUMIDO"].ToString()).ToString("N", nfi),

                        V_SOLES = Convert.ToDecimal(registros["VALOR_SOLES"]),
                        V_DOLAR = Convert.ToDecimal(registros["VALOR_DOLAR"]),
                        V_CONSUMIDO = Convert.ToDecimal(registros["VALOR_CONSUMIDO"]),

                    });

                }
            }
            conexion.Desconectar();
            return lista;
        }

        public MemoryStream ExpExcelAcumuladoMult(List<ListaAcumulado> listado)
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
                workSheet.Column(10).Width = 20;    // VALOR DOLAR

                int i = 2;

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
                }

                package.Save();
            }

            stream.Position = 0;
            return stream;

        }

        public bool ActualizasOS()
        {
            try
            {
                DBAccess conexion = new DBAccess();
                OracleCommand comando = new OracleCommand("SPU_SET_ACTUALIZAR_OS", conexion.Acceder());
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Conectar();
                comando.ExecuteNonQuery();
                conexion.Desconectar();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}







