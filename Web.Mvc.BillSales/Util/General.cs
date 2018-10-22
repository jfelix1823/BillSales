using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Web.Mvc.BillSales.Util
{
    //public static class HttpSessionExtension
    //{
    //    public static void Set<T>(this ISession session, string key, T value)
    //    {
    //        session.SetString(key, JsonConvert.SerializeObject(value));
    //    }

    //    public static T Get<T>(this ISession session, string key)
    //    {
    //        var value = session.GetString(key);
    //        return value == null ? default(T) :
    //                              JsonConvert.DeserializeObject<T>(value);
    //    }
    //}

    public static class General
    {
        public static List<Nivel> Listado_Nivel()
        {
            List<Nivel> Niveles = new List<Nivel>();
            Niveles.Add(new Nivel() { nivel_id = Constantes.Nivel.Administrador, nivel_nombre = "Administrador" });
            Niveles.Add(new Nivel() { nivel_id = Constantes.Nivel.Usuario, nivel_nombre = "Usuario" });
            Niveles.Add(new Nivel() { nivel_id = Constantes.Nivel.Visitante, nivel_nombre = "Visitante" });
            return Niveles;
        }


        public static List<Tipo_Usuario> Listado_Tipo_Usuario()
        {
            List<Tipo_Usuario> Tipo_Usuarios = new List<Tipo_Usuario>();
            Tipo_Usuarios.Add(new Tipo_Usuario() { tipo_id = Constantes.Tipo_Usuario.Cliente, tipo_nombre = "Cliente" });
            Tipo_Usuarios.Add(new Tipo_Usuario() { tipo_id = Constantes.Tipo_Usuario.Web, tipo_nombre = "Web" });
            Tipo_Usuarios.Add(new Tipo_Usuario() { tipo_id = Constantes.Tipo_Usuario.Usuario_Final, tipo_nombre = "Usuario Final" });
            return Tipo_Usuarios;
        }


        public static List<Tipo_Servicio> Listado_Tipo_Servicio()
        {
            List<Tipo_Servicio> Tipo_Servicios = new List<Tipo_Servicio>();
            Tipo_Servicios.Add(new Tipo_Servicio() { servicio_tipo = Constantes.Tipo_Servicio.Crl, servicio_tipo_nombre = "CRL" });
            Tipo_Servicios.Add(new Tipo_Servicio() { servicio_tipo = Constantes.Tipo_Servicio.Tsa, servicio_tipo_nombre = "TSA" });
            Tipo_Servicios.Add(new Tipo_Servicio() { servicio_tipo = Constantes.Tipo_Servicio.Tsl, servicio_tipo_nombre = "TSL" });
            return Tipo_Servicios;
        }

        public static List<Estilo> Listado_Tipo_Estilo()
        {
            List<Estilo> Tipo_Estilos = new List<Estilo>();
            Tipo_Estilos.Add(new Estilo() { Codigo = Constantes.Estilo.Texto, Descripcion = "Texto" });
            Tipo_Estilos.Add(new Estilo() { Codigo = Constantes.Estilo.Imagen, Descripcion = "Imagen" });
            Tipo_Estilos.Add(new Estilo() { Codigo = Constantes.Estilo.Texto_Imagen, Descripcion = "Texto e Imagen" });
            return Tipo_Estilos;
        }
        


    }
}
