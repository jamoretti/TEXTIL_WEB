using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using TSC_WEB.Config;
using Oracle.ManagedDataAccess.Client;
using TSC_WEB.Models.Entidades.OperacionesProceso.DesasociarFicha;

namespace TSC_WEB.Models.Modelos.OperacionesProceso.DesasociarFicha
{
    public class DesasociarFichaModelo
    {
        DBAccess conexion = new DBAccess();


        //LISTA DESPACHOS Y DEVOLUCIONES
        public List<DesasociarFichaEntidad> ListarDespachosDevoluciones(char op,int ficha,int os)
        {
            //LISTA
            List<DesasociarFichaEntidad> DesasociarLista = new List<DesasociarFichaEntidad>();


            OracleCommand comando = new OracleCommand("spu_desasociar1",conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("op",op));
            comando.Parameters.Add(new OracleParameter("os", os));
            comando.Parameters.Add(new OracleParameter("ficha", ficha));
            comando.Parameters.Add(new OracleParameter("cur", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;


            OracleDataReader registros = comando.ExecuteReader();

            while(registros.Read()){

                DesasociarLista.Add(
                    new DesasociarFichaEntidad
                    {
                         operacion              =   Convert.ToInt32(registros["operacion"].ToString()),
                         descripcionoperacion   =   registros["descripcionoperacion"].ToString(),
                         fechaemision           =   Convert.ToDateTime(registros["fechaemision"].ToString()).ToShortDateString(),
                         guia                   =   registros["guia"].ToString(),
                         serieguia              =   registros["serieguia"].ToString(),
                         almacen                =   Convert.ToInt32(registros["almacen"].ToString()),
                         cantidad               =   registros["cantidad"].ToString(),
                    }
                );
            }

            conexion.Desconectar();

            return DesasociarLista;

        }

    }
}