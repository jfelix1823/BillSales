using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Mvc.BillSales.Util
{
    public class Constantes
    {
        public class Nivel
        {
            public static int Administrador
            {
                get { return 1; }
            }
            public static int Usuario
            {
                get { return 2; }
            }
            public static int Visitante
            {
                get { return 3; }
            }
        }


        public class Tipo_Usuario
        {
            public static string Web
            {
                get { return "Web"; }
            }
            public static string Usuario_Final
            {
                get { return "Usuario Final"; }
            }
            public static string Cliente
            {
                get { return "Cliente"; }
            }
        }


        public class Tipo_Servicio
        {
            public static string Tsa
            {
                get { return "TSA"; }
            }
            public static string Tsl
            {
                get { return "TSL"; }
            }
            public static string Crl
            {
                get { return "CRL"; }
            }
        }

        public class Estilo
        {
            public static string Texto
            {
                get { return "1"; }
            }
            public static string Imagen
            {
                get { return "2"; }
            }
            public static string Texto_Imagen
            {
                get { return "3"; }
            }
        }




    }
}
