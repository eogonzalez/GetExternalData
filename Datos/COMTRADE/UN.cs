using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.COMTRADE
{
    public class UN
    {
        General.ConectarService objConectar = new General.ConectarService();

        //Funcion que almacena los dotos de los productos obtenidos de la COMTRADE
        public bool SaveCommodity(DataSet commodities_list)
        {
            bool estado = true;
            string sql_query = null;
            try
            {
                DataTable dt_commodity = commodities_list.Tables["r"];

                foreach (DataRow row_comodity in dt_commodity.Rows)
                {
                    //Query para insertar en la tabla country
                    sql_query = " INSERT INTO Commodities " +
                        " (code, descE, isBasicCode, level, parentCode, class)" +
                        " VALUES " +
                        " (@code, @descE, @isBasicCode, @level, @parentCode, @class) ";

                    using (var conexion = objConectar.Conectar("un"))
                    {
                        var command = new SqlCommand(sql_query, conexion);
                        command.Parameters.AddWithValue("code", row_comodity["code"]);
                        command.Parameters.AddWithValue("descE", row_comodity["descE"]);
                        command.Parameters.AddWithValue("isBasicCode", row_comodity["isBasicCode"]);
                        command.Parameters.AddWithValue("level", row_comodity["level"]);
                        command.Parameters.AddWithValue("parentCode", row_comodity["parentCode"]);
                        command.Parameters.AddWithValue("class", row_comodity["class"]);
                        conexion.Open();
                        if (command.ExecuteNonQuery() > 0)
                        {
                            estado = true;
                        }
                        else
                        {
                            estado = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                estado = false;
                throw;
            }
            return estado;
        }

        //Funcion que almacena los paises de la COMTRADE
        public bool SaveCountry(DataSet countries_list)
        {
            bool estado = true;
            string sql_query = null;
            try
            {
                DataTable dt_country = countries_list.Tables["r"];

                foreach (DataRow row_country in dt_country.Rows)
                {
                    //Query para insertar en la tabla country
                    sql_query = " INSERT INTO Countries " +
                        " (code, name, iso2, iso3)" +
                        " VALUES " +
                        " (@code, @name, @iso2, @iso3) ";

                    using (var conexion = objConectar.Conectar("un"))
                    {
                        var command = new SqlCommand(sql_query, conexion);
                        command.Parameters.AddWithValue("code", row_country["code"]);
                        command.Parameters.AddWithValue("name", row_country["name"]);
                        command.Parameters.AddWithValue("iso2", row_country["iso2"]);
                        command.Parameters.AddWithValue("iso3", row_country["iso3"]);
                        
                        conexion.Open();
                        if (command.ExecuteNonQuery() > 0)
                        {
                            estado = true;
                        }
                        else
                        {
                            estado = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                estado = false;
                throw;
            }
            return estado;
        }
    }
}
