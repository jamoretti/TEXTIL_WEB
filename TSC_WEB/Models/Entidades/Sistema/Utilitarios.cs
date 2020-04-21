using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSC_WEB.Models.Entidades.Sistema
{
    public static class Utilitarios
    {
        public struct EstadoAprobacion
        {
            public const string Pendiente = "1";
            public const string Aprobado = "2";
            public const string Rechazado = "3";

            public const string MensajeAprobado = "Aprobado";
            public const string MensajeRechazado = "Rechazado";
            public const string MensajePendiente = "Pendiente";
        }
        public class ValorCorreos
        {

        }
        public struct ValorEstado
        {
            public const int Bloqueado = 9;
        }
        public struct Metodos
        {
            public const string NombreClase = "OrdenServicio";
        }
    }
}