using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.BM
{
    public class BM
    {
        //Funcion que almacena informacion de los indicadores del banco mundial
        public bool SaveDataIndicators(DataSet indicator_list)
        {
            var objConectar = new General.ConectarService();
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

        
    }
}
