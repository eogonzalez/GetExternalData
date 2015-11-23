using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Datos.BM
{
    public class BM
    {
        General.ConectarService objConectar = new General.ConectarService();

        //Funcion que almacena informacion de los indicadores del banco mundial
        public bool SaveDataIndicators(DataSet indicator_list)
        {
            bool estado = true;
            string sql_query = null;
            try
            {
                DataTable dt_indicators = indicator_list.Tables["indicators"];
                DataTable dt_indicator = indicator_list.Tables["indicator"];
                DataTable dt_source = indicator_list.Tables["source"];
                DataTable dt_topics = indicator_list.Tables["topics"];
                DataTable dt_topic = indicator_list.Tables["topic"];

                foreach (DataRow indicators_row in dt_indicators.Rows)
                {
                    //Query para insertar en la tabla indicators
                    sql_query = " INSERT INTO I_indicators "+
                        " (indicators_id, page, pages, per_page, total)"+
                        " VALUES "+
                        " (@indicators_id, @page, @pages, @per_page, @total) ";

                    using (var conexion = objConectar.Conectar("bm"))
                    {
                        var command = new SqlCommand(sql_query, conexion);
                        command.Parameters.AddWithValue("indicators_id", indicators_row["indicators_id"]);
                        command.Parameters.AddWithValue("page", indicators_row["page"]);
                        command.Parameters.AddWithValue("pages", indicators_row["pages"]);
                        command.Parameters.AddWithValue("per_page", indicators_row["per_page"]);
                        command.Parameters.AddWithValue("total", indicators_row["total"]);
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

                foreach (DataRow indicator_row in dt_indicator.Rows)
                {
                    //Query para insertar en la tabla indicators
                    sql_query = " INSERT INTO I_indicator " +
                        " (name, indicator_id, sourceNote, sourceOrganization, id, indicators_id)" +
                        " VALUES " +
                        " (@name, @indicator_id, @sourceNote, @sourceOrganization, @id, @indicators_id) ";

                    using (var conexion = objConectar.Conectar("bm"))
                    {
                        var command = new SqlCommand(sql_query, conexion);
                        command.Parameters.AddWithValue("name", indicator_row["name"]);
                        command.Parameters.AddWithValue("indicator_id", indicator_row["indicator_id"]);
                        command.Parameters.AddWithValue("sourceNote", indicator_row["sourceNote"]);
                        command.Parameters.AddWithValue("sourceOrganization", indicator_row["sourceOrganization"]);
                        command.Parameters.AddWithValue("id", indicator_row["id"]);
                        command.Parameters.AddWithValue("indicators_id", indicator_row["indicators_id"]);
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

                foreach (DataRow source_row in dt_source.Rows)
                {
                    //Query para insertar en la tabla indicators
                    sql_query = " INSERT INTO I_source " +
                        " (id, source_text, indicator_id)" +
                        " VALUES " +
                        " (@id, @source_text, @indicator_id) ";

                    using (var conexion = objConectar.Conectar("bm"))
                    {
                        var command = new SqlCommand(sql_query, conexion);
                        command.Parameters.AddWithValue("id", source_row["id"]);
                        command.Parameters.AddWithValue("source_text", source_row["source_text"]);
                        command.Parameters.AddWithValue("indicator_id", source_row["indicator_id"]);
                        
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

                foreach (DataRow topics_row in dt_topics.Rows)
                {
                    //Query para insertar en la tabla indicators
                    sql_query = " INSERT INTO I_topics " +
                        " (topics_id, indicator_id)" +
                        " VALUES " +
                        " (@topics_id, @indicator_id) ";

                    using (var conexion = objConectar.Conectar("bm"))
                    {
                        var command = new SqlCommand(sql_query, conexion);
                        command.Parameters.AddWithValue("topics_id", topics_row["topics_id"]);
                        command.Parameters.AddWithValue("indicator_id", topics_row["indicator_id"]);
                        
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

                foreach (DataRow topic_row in dt_topic.Rows)
                {
                    //Query para insertar en la tabla indicators
                    sql_query = " INSERT INTO I_topic " +
                        " (id, topic_Text, topics_id)" +
                        " VALUES " +
                        " (@id, @topic_Text, @topics_id) ";

                    using (var conexion = objConectar.Conectar("bm"))
                    {
                        var command = new SqlCommand(sql_query, conexion);
                        command.Parameters.AddWithValue("id", topic_row["id"]);
                        command.Parameters.AddWithValue("topic_Text", topic_row["topic_Text"]);
                        command.Parameters.AddWithValue("topics_id", topic_row["topics_id"]);
                        
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
            catch (SqlException)
            {
                estado = false;
                throw;
            }
            catch (Exception)
            {
                estado = false;
                throw;
            }
            return estado;
        }

        //Funcion que obtiene archivo y descoprime
        public XmlDocument ObtenerArchivoGzip(string url)
        {
            var xmlDoc = new XmlDocument();
            try
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                //StreamReader reader = new StreamReader(dataStream);
                using (GZipStream gzip = new GZipStream(dataStream, CompressionMode.Decompress))
                {
                    using (XmlReader xmlwriter = XmlReader.Create(gzip, new XmlReaderSettings()))
                    {
                        xmlwriter.MoveToContent();

                        xmlDoc.Load(xmlwriter);
                    }
                }
            }
            catch (XmlException)
            {
                throw;
            }
            catch (Exception)
            {
                
                throw;
            }
            return xmlDoc;
        }

        //Funcion que almacena informacion de los paises del banco mundial
        public bool SaveDataCountries(DataSet country_list)
        {
            bool estado = true;
            string sql_query = null;
            try
            {
                DataTable dt_countries = country_list.Tables["countries"];
                DataTable dt_country = country_list.Tables["country"];
                DataTable dt_region = country_list.Tables["region"];
                DataTable dt_adminregion = country_list.Tables["adminregion"];
                DataTable dt_incomelevel = country_list.Tables["incomeLevel"];
                DataTable dt_lendingType = country_list.Tables["lendingType"];

                foreach (DataRow row_countries in dt_countries.Rows)
                {
                    //Query para insertar en la tabla indicators
                    sql_query = " INSERT INTO C_countries " +
                        " (countries_id, page, pages, per_page, total)" +
                        " VALUES " +
                        " (@countries_id, @page, @pages, @per_page, @total) ";

                    using (var conexion = objConectar.Conectar("bm"))
                    {
                        var command = new SqlCommand(sql_query, conexion);
                        command.Parameters.AddWithValue("countries_id", row_countries["countries_id"]);
                        command.Parameters.AddWithValue("page", row_countries["page"]);
                        command.Parameters.AddWithValue("pages", row_countries["pages"]);
                        command.Parameters.AddWithValue("per_page", row_countries["per_page"]);
                        command.Parameters.AddWithValue("total", row_countries["total"]);
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

                foreach (DataRow row_country in dt_country.Rows)
                {
                    //Query para insertar en la tabla indicators
                    sql_query = " INSERT INTO C_country " +
                        " (iso2Code, name, country_Id, capitalCity, longitude, latitude, id, countries_Id)" +
                        " VALUES " +
                        " (@iso2Code, @name, @country_Id, @capitalCity, @longitude, @latitude, @id, @countries_Id) ";

                    using (var conexion = objConectar.Conectar("bm"))
                    {
                        var command = new SqlCommand(sql_query, conexion);
                        command.Parameters.AddWithValue("iso2Code", row_country["iso2Code"]);
                        command.Parameters.AddWithValue("name", row_country["name"]);
                        command.Parameters.AddWithValue("country_Id", row_country["country_Id"]);
                        command.Parameters.AddWithValue("capitalCity", row_country["capitalCity"]);
                        command.Parameters.AddWithValue("longitude", row_country["longitude"]);
                        command.Parameters.AddWithValue("latitude", row_country["latitude"]);
                        command.Parameters.AddWithValue("id", row_country["id"]);
                        command.Parameters.AddWithValue("countries_Id", row_country["countries_Id"]);
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

                foreach (DataRow row_region in dt_region.Rows)
                {
                    
                }

                foreach (DataRow row_adminregion in dt_adminregion.Rows)
                {
                    
                }

                foreach (DataRow row_incomelevel in dt_incomelevel.Rows)
                {
                    
                }

                foreach (DataRow row_lendingType in dt_lendingType.Rows)
                {
                    
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return estado;
        }
    }
}
