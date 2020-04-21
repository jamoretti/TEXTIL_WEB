using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using TSC_WEB.Config;
using TSC_WEB.Models.Entidades.DesarrolloProducto.FichaTecnica;

namespace TSC_WEB.Models.Modelos.DesarrolloProducto.FichaTecnica
{
    public class ReporteImprimirModelo
    {
        DBAccess conexion = new DBAccess();

        public ReporteEntidad BuscarReporte(int pedido,int etapa)
        {
            OracleCommand comando = new OracleCommand("systextilrpt.SPGET_DESA_FTPRENDAPEDIDO_1", conexion.Acceder());
            comando.CommandType = CommandType.StoredProcedure;
            conexion.Conectar();

            comando.Parameters.Add(new OracleParameter("ppedido", pedido));
            comando.Parameters.Add(new OracleParameter("prutacompleta",""));
            comando.Parameters.Add(new OracleParameter("PETAPA", etapa));
            comando.Parameters.Add(new OracleParameter("PPEDIDOS", ""));
            comando.Parameters.Add(new OracleParameter("pcursor", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            comando.Parameters.Add(new OracleParameter("pcursor_rutas", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            comando.Parameters.Add(new OracleParameter("pcursor_combinaciones", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            comando.Parameters.Add(new OracleParameter("pcursor_avios", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            comando.Parameters.Add(new OracleParameter("pcursor_secoperaciones", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            comando.Parameters.Add(new OracleParameter("pcursor_bservacion", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            comando.Parameters.Add(new OracleParameter("pcursor_rutastela", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            comando.Parameters.Add(new OracleParameter("pcursor_rapport", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
            comando.Parameters.Add(new OracleParameter("pcursor_tipoTela", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;

            DataSet resultado = new DataSet();
            OracleDataAdapter adapter = new OracleDataAdapter(comando);
            adapter.Fill(resultado);
            conexion.Desconectar();

            //INSTANCIA QUE DEVOLVEREMOS
            ReporteEntidad objReporteEntidad = new ReporteEntidad();

            //CREAMOS ENTIDADDES 
            FichaTecnicaEntidad objFichaTecnicaE = new FichaTecnicaEntidad();
            List<RutasEntidad> objRutasELista = new List<RutasEntidad>();
            List<RutasTelaPrincipalEntidad> objRutasTelaPLista = new List<RutasTelaPrincipalEntidad>();
            List<ObservacionesEntidad> objObservacionesLista = new List<ObservacionesEntidad>();
            List<CombinacionesEntidad> objCombinacionesLista = new List<CombinacionesEntidad>();
            List<AviosEntidad> objAviosLista = new List<AviosEntidad>();
            List<SecuenciaOperacionesEntidad> objSecuenciaOpLista = new List<SecuenciaOperacionesEntidad>();
            //CONVIRTIENDO PRIMERA LISTA
            for (int x = 0; x < resultado.Tables[0].Rows.Count; x++)
            {
                objFichaTecnicaE = new FichaTecnicaEntidad
                {
                    fecha_generacion = Convert.ToDateTime(resultado.Tables[0].Rows[x]["fecha_generacion"].ToString()).ToShortDateString(),
                    estilo_cliente = resultado.Tables[0].Rows[x]["estilo_cliente"].ToString(),
                    descripcion_prenda = resultado.Tables[0].Rows[x]["descripcion_prenda"].ToString(),
                    cliente = resultado.Tables[0].Rows[x]["cliente"].ToString(),
                    estilo_propio = resultado.Tables[0].Rows[x]["estilo_propio"].ToString(),
                    n_solicitud = Convert.ToInt32(resultado.Tables[0].Rows[x]["n_solicitud"].ToString()),
                    temporada = resultado.Tables[0].Rows[x]["temporada"].ToString(),
                    tipo_muestra = resultado.Tables[0].Rows[x]["tipo_muestra"].ToString(),
                    alternativa = Convert.ToInt32(resultado.Tables[0].Rows[x]["alternativa"].ToString()),
                    n_ruta = Convert.ToInt32(resultado.Tables[0].Rows[x]["n_ruta"].ToString()),
                    estampado = resultado.Tables[0].Rows[x]["estampado"].ToString(),
                    bordado = resultado.Tables[0].Rows[x]["bordado"].ToString(),
                    lavado = resultado.Tables[0].Rows[x]["lavado"].ToString(),
                    finura = resultado.Tables[0].Rows[x]["finura"].ToString(),
                    pedido = resultado.Tables[0].Rows[x]["pedido"].ToString(),
                    tallas = resultado.Tables[0].Rows[x]["tallas"].ToString(),
                    po = resultado.Tables[0].Rows[x]["po"].ToString(),
                    grafico_prenda = resultado.Tables[0].Rows[x]["grafico_prenda"].ToString() == string.Empty ? "/Public/ImprimirFichaTecnica/images.jpg" : @"/desarrolloproducto/getfile/?ruta=\\172.16.88.138\Archivos_Systextil\ESTILOS" + resultado.Tables[0].Rows[x]["grafico_prenda"].ToString(),
                    narrativa = resultado.Tables[0].Rows[x]["narrativa"].ToString(),
                    conteudo_atributo = resultado.Tables[0].Rows[x]["conteudo_atributo"].ToString(),
                    densidad = resultado.Tables[0].Rows[x]["densidad"].ToString(),
                    ancho_tela = resultado.Tables[0].Rows[x]["ancho_tela"].ToString()
                };
            }
            objReporteEntidad.objFichaEntidad = objFichaTecnicaE;
            //COVIRTIENDO SEGUNDA LISTA
            for (int x = 0; x < resultado.Tables[1].Rows.Count; x++)
            {
                objRutasELista.Add(
                    new RutasEntidad
                    {
                        DESCRICAO = resultado.Tables[1].Rows[x]["descricao"].ToString(),
                        SEQUENCIA_ESTAGIO = Convert.ToInt32(resultado.Tables[1].Rows[x]["sequencia_estagio"].ToString()),
                    }
                );
            }
            objReporteEntidad.objRutasEntidadLista = objRutasELista;

            //CONVIRTIENDO TERCERA LISTA
            for (int x = 0; x < resultado.Tables[6].Rows.Count; x++)
            {
                objRutasTelaPLista.Add(
                        new RutasTelaPrincipalEntidad
                        {
                            CODIGO_OPERACAO = resultado.Tables[6].Rows[x]["codigo_operacao"].ToString() != "" ? Convert.ToInt32(resultado.Tables[6].Rows[x]["codigo_operacao"].ToString()) : 0,
                            NOME_OPERACAO = resultado.Tables[6].Rows[x]["NOME_OPERACAO"].ToString(),
                            SEQ_OPERACAO = resultado.Tables[6].Rows[x]["SEQ_OPERACAO"].ToString() != "" ? Convert.ToInt32(resultado.Tables[6].Rows[x]["SEQ_OPERACAO"].ToString()) : 0,
                        }
                 );
            }
            objReporteEntidad.objRutasTelaPrincipalLista = objRutasTelaPLista;
            //CONVIRTIENDO CUARTA LISTA - 8
            for (int x = 0; x < resultado.Tables[8].Rows.Count; x++)
            {
                objObservacionesLista.Add(
                    new ObservacionesEntidad
                    {
                        NARRATIVA = resultado.Tables[8].Rows[x]["narrativa"].ToString(),
                        DENSIDAD = Convert.ToInt32(resultado.Tables[8].Rows[x]["densidad"].ToString()),
                        ANCHO_TELA = Convert.ToDecimal(resultado.Tables[8].Rows[x]["ANCHO_TELA"].ToString()),
                        INCLINACAO_TRAMA = resultado.Tables[8].Rows[x]["INCLINACAO_TRAMA"].ToString(),
                        TECIDO_PRINCIPAL = resultado.Tables[8].Rows[x]["TECIDO_PRINCIPAL"].ToString() == "1" ? "SI" : "NO",

                        ALTERNATIVA_ITEM = Convert.ToInt32(resultado.Tables[8].Rows[x]["ALTERNATIVA_ITEM"].ToString()),
                        ANCHO_LAVADA = resultado.Tables[8].Rows[x]["ANCHO_LAVADA"].ToString(),
                        CONTEUDO_ATRIBUTO = resultado.Tables[8].Rows[x]["CONTEUDO_ATRIBUTO"].ToString(),
                        FINURA = resultado.Tables[8].Rows[x]["FINURA"].ToString(),
                        GRUPO_COMP = resultado.Tables[8].Rows[x]["GRUPO_COMP"].ToString(),
                        LARGO_LAVADA = resultado.Tables[8].Rows[x]["LARGO_LAVADA"].ToString(),
                        NIVEL_COMP = resultado.Tables[8].Rows[x]["NIVEL_COMP"].ToString(),
                    }
                );
            }
            objReporteEntidad.objObservacionesLista = objObservacionesLista;
            //CONVIERTE QUINTA TABLA - 2
            for (int x = 0; x < resultado.Tables[2].Rows.Count; x++)
            {
                objCombinacionesLista.Add(
                    new CombinacionesEntidad
                    {
                        AGRUPADOR = resultado.Tables[2].Rows[x]["AGRUPADOR"].ToString(),
                        CODIGO_TELA = resultado.Tables[2].Rows[x]["CODIGO_TELA"].ToString(),
                        COD_COLOR_TELA = resultado.Tables[2].Rows[x]["COD_COLOR_TELA"].ToString(),
                        COMBINACAO_ITEM = resultado.Tables[2].Rows[x]["COMBINACAO_ITEM"].ToString(),
                        CONSUMO = Convert.ToDecimal(resultado.Tables[2].Rows[x]["CONSUMO"].ToString()),
                        DESCR_COMBINACAO = resultado.Tables[2].Rows[x]["DESCR_COMBINACAO"].ToString(),
                        DESCR_TAM_REFER = resultado.Tables[2].Rows[x]["DESCR_TAM_REFER"].ToString(),
                        NARRATIVA = resultado.Tables[2].Rows[x]["NARRATIVA"].ToString(),
                        POR_EFICIENCIA = resultado.Tables[2].Rows[x]["POR_EFICIENCIA"].ToString(),
                        SEQUENCIA = Convert.ToInt32(resultado.Tables[2].Rows[x]["SEQUENCIA"].ToString()),
                        SEQ_COR = resultado.Tables[2].Rows[x]["SEQ_COR"].ToString(),
                        TECIDO_PRINCIPAL = Convert.ToInt32(resultado.Tables[2].Rows[x]["TECIDO_PRINCIPAL"].ToString()),
                        UBICACION = resultado.Tables[2].Rows[x]["UBICACION"].ToString(),
                        descricao_15 = resultado.Tables[2].Rows[x]["descricao_15"].ToString(),
                    }
                );
            }
            objReporteEntidad.objCombinacionesLista = objCombinacionesLista;
            //CONVIRTIENDO SEXTA TABLA - 3
            for (int x = 0; x < resultado.Tables[3].Rows.Count; x++)
            {
                if (resultado.Tables[3].Rows[x]["SEQUENCIA_ESTAGIO"].ToString() != string.Empty || resultado.Tables[3].Rows[x]["ESTAGIO"].ToString() == "38")
                {
                    objAviosLista.Add(
                        new AviosEntidad
                        {
                            CODIGO = resultado.Tables[3].Rows[x]["CODIGO"].ToString(),
                            CODIGO2 = resultado.Tables[3].Rows[x]["CODIGO2"].ToString(),
                            COMBINACAO_ITEM = resultado.Tables[3].Rows[x]["COMBINACAO_ITEM"].ToString(),
                            CONSUMO = Convert.ToDecimal(resultado.Tables[3].Rows[x]["CONSUMO"].ToString()),
                            DESCRICAO = resultado.Tables[3].Rows[x]["DESCRICAO"].ToString(),
                            DESCRICAO_01 = resultado.Tables[3].Rows[x]["DESCRICAO_01"].ToString(),
                            DESCRICAO_15 = resultado.Tables[3].Rows[x]["DESCRICAO_15"].ToString(),
                            DESCR_COMBINACAO = resultado.Tables[3].Rows[x]["DESCR_COMBINACAO"].ToString(),
                            ESTAGIO = resultado.Tables[3].Rows[x]["ESTAGIO"].ToString(),
                            GRAFICO_AVIO = resultado.Tables[3].Rows[x]["GRAFICO_AVIO"].ToString(),
                            NARRATIVA = resultado.Tables[3].Rows[x]["NARRATIVA"].ToString(),
                            OBSERVACION = resultado.Tables[3].Rows[x]["OBSERVACION"].ToString(),
                            SEQUENCIA = Convert.ToInt32(resultado.Tables[3].Rows[x]["SEQUENCIA"].ToString()),
                            SEQUENCIA_ESTAGIO = resultado.Tables[3].Rows[x]["SEQUENCIA_ESTAGIO"].ToString() == string.Empty ? 0 : Convert.ToInt32(resultado.Tables[3].Rows[x]["SEQUENCIA_ESTAGIO"].ToString()),
                            SUB_COMP = resultado.Tables[3].Rows[x]["SUB_COMP"].ToString(),
                            TALLA_A = resultado.Tables[3].Rows[x]["TALLA_A"].ToString(),
                            TECIDO_PRINCIPAL = Convert.ToInt32(resultado.Tables[3].Rows[x]["TECIDO_PRINCIPAL"].ToString()),
                            UNIDADE_MEDIDA = resultado.Tables[3].Rows[x]["UNIDADE_MEDIDA"].ToString(),


                        }
                    );
                }
               
                 
            }
            objReporteEntidad.objAviosLista = objAviosLista;

            //CONVIRTIENDO SEPTIMA TABLA - 4
            for (int x = 0; x < resultado.Tables[4].Rows.Count; x++)
            {
                objSecuenciaOpLista.Add(
                    new SecuenciaOperacionesEntidad
                    {
                        codigo_estagio = Convert.ToInt32(resultado.Tables[4].Rows[x]["codigo_estagio"].ToString()),
                        codigo_operacao = Convert.ToInt32(resultado.Tables[4].Rows[x]["codigo_operacao"].ToString()),
                        codigo_parte_peca = resultado.Tables[4].Rows[x]["codigo_parte_peca"].ToString(),
                        descricao = resultado.Tables[4].Rows[x]["descricao"].ToString(),
                        desc_parte_peca = resultado.Tables[4].Rows[x]["desc_parte_peca"].ToString(),
                        minutos = Convert.ToDecimal(resultado.Tables[4].Rows[x]["minutos"].ToString()),
                        nome_operacao = resultado.Tables[4].Rows[x]["nome_operacao"].ToString(),
                        observacao = resultado.Tables[4].Rows[x]["observacao"].ToString(),
                        observacao1 = resultado.Tables[4].Rows[x]["observacao1"].ToString(),
                        sequencia_estagio = Convert.ToInt32(resultado.Tables[4].Rows[x]["sequencia_estagio"].ToString()),
                        seq_operacao = Convert.ToInt32(resultado.Tables[4].Rows[x]["seq_operacao"].ToString()),

                    }
                );
            }
            objReporteEntidad.objSecuenciaOperacionesLista = objSecuenciaOpLista;

            return objReporteEntidad;
        }
    }
}