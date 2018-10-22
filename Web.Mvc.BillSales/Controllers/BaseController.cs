using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Web.Mvc.BillSales.Models;
using Web.Mvc.BillSales.Filters;

namespace Web.Mvc.BillSales.Controllers
{
    public class BaseController : Controller
    {
        //Inicio Implementacion session view model Usuario
        public UsuariosViewModel obj_UsuariosViewModel = null;

        public UsuariosViewModel ObjetoSessionUsuariosViewModel
        {
            get { return HttpContext.Session.GetComplexData<UsuariosViewModel>("UsuariosViewModel"); }
            set { HttpContext.Session.SetComplexData("UsuariosViewModel", obj_UsuariosViewModel); }
        }

        public void GetSessionUsuariosViewModel(ref UsuariosViewModel objUsuariosViewModel)
        {
            if (ObjetoSessionUsuariosViewModel == null)
            {
                objUsuariosViewModel = new UsuariosViewModel();
                //objUser.general.TipoResult = Constantes.Login.SesionExpirada;
                throw new Exception("Su sesión ha expirado");
            }
            objUsuariosViewModel = ObjetoSessionUsuariosViewModel;
        }

        public void GetSessionAllClear()
        {
            HttpContext.Session.Clear();
        }
        //Fin Implementacion session view model Usuario

        public IList<T> ConvertTo<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return ConvertTo<T>(rows);
        }
        public IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }

            return list;
        }
        public T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    try
                    {
                        object value = row[column.ColumnName];
                        prop.SetValue
                            (obj, value.ToString(), null);
                    }
                    catch
                    {
                        // You can log something here
                        throw;
                    }
                }
            }
            return obj;
        }
        public DataTable fileToDatatable(string ruta, int numberOfColumns,char separador)
        {
            DataTable tbl = new DataTable();

            for (int col = 0; col < numberOfColumns; col++)
                tbl.Columns.Add(new DataColumn("Column" + (col + 1).ToString()));


            string[] lines = System.IO.File.ReadAllLines(ruta);

            foreach (string line in lines)
            {
                var cols = line.Split(separador);

                DataRow dr = tbl.NewRow();
                for (int cIndex = 0; cIndex < numberOfColumns; cIndex++)
                {
                    dr[cIndex] = cols[cIndex];
                }

                tbl.Rows.Add(dr);
            }

            return tbl;
        }
        
        
    }

    public static class SessionExtensions
    {
        public static T GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetComplexData(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }


}