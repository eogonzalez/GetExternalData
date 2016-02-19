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

        //Funcion que verifica si existe registros en tabla I_indicator
        public bool ExisteIndicator(DataRow rowIndicator, string idioma)
        {
            bool estado = true;
            string sql_query = null;

            try
            {

                sql_query = " SELECT " +
                    " coalesce(count(*),0) " +
                    " FROM " +
                    " I_indicator " +
                    " where " +
                    " indicator_id = @indicator_id " +
                    " and idioma = @idioma ";

                using (var conexion = objConectar.Conectar("bm"))
                {
                    var command = new SqlCommand(sql_query, conexion);
                    command.Parameters.AddWithValue("indicator_id", rowIndicator["indicator_id"]);
                    command.Parameters.AddWithValue("idioma", idioma);

                    conexion.Open();
                    int result = int.Parse(command.ExecuteScalar().ToString());

                    if (result > 0)
                    {
                        estado = true;
                    }
                    else
                    {
                        estado = false;
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

        //Funcion que verifica si existe registros en tabla I_source
        public bool ExisteSource(DataRow rowSource, string idioma)
        {
            bool estado = true;
            string sql_query = null;
            try
            {
                sql_query = " SELECT "+
                    " coalesce(count(*), 0) "+
                    " FROM "+
                    " I_source "+
                    " where "+
                    " id = @id  "+
                    " AND indicator_id = @indicator_id "+
                    " AND idioma = @idioma ";

                using (var conexion = objConectar.Conectar("bm"))
                {
                    var command = new SqlCommand(sql_query, conexion);
                    command.Parameters.AddWithValue("id", rowSource["id"]);
                    command.Parameters.AddWithValue("indicator_id", rowSource["indicator_id"]);
                    command.Parameters.AddWithValue("idioma", idioma);

                    conexion.Open();
                    int result = int.Parse(command.ExecuteScalar().ToString());

                    if (result > 0)
                    {
                        estado = true;
                    }
                    else
                    {
                        estado = false;
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

        //Funcion que verifica si existe registros en tabla I_topics
        public bool ExisteTopics(DataRow rowTopics, string idioma)
        {
            bool estado = true;
            string sql_query = null;

            try
            {

                sql_query = " SELECT "+
                    " COALESCE(count(*), 0) "+
                    " FROM "+
                    " I_topics "+
                    " where "+
                    " topics_id = @topics_id "+
                    " AND idioma = @idioma ";

                using (var conexion = objConectar.Conectar("bm"))
                {
                    var command = new SqlCommand(sql_query, conexion);
                
                    command.Parameters.AddWithValue("topics_id", rowTopics["topics_id"]);
                    command.Parameters.AddWithValue("idioma", idioma);

                    conexion.Open();
                    int result = int.Parse(command.ExecuteScalar().ToString());

                    if (result > 0)
                    {
                        estado = true;
                    }
                    else
                    {
                        estado = false;
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

        //Funcion que verifica si existe registros en tabla I_topic
        public bool ExisteTopic(DataRow rowTopic, string idioma)
        {
            bool estado = true;
            string sql_query = null;

            sql_query = " select "+
                " COALESCE(count(*),0) "+
                " FROM "+
                " I_topic "+
                " where " +
                " id = @id "+
                " and topics_id = @topics_id "+
                " and idioma = @idioma ";

            using (var conexion = objConectar.Conectar("bm"))
            {
                var command = new SqlCommand(sql_query, conexion);

                command.Parameters.AddWithValue("id", rowTopic["id"]);
                command.Parameters.AddWithValue("topics_id", rowTopic["topics_id"]);
                command.Parameters.AddWithValue("idioma", idioma);

                conexion.Open();
                int result = int.Parse(command.ExecuteScalar().ToString());

                if (result > 0)
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                }

            }

            try
            {

            }
            catch (Exception)
            {
                estado = false;
                throw;
            }

            return estado;
        }

        //Funcion que almacena informacion de los indicadores del banco mundial
        public bool SaveDataIndicators(DataSet indicator_list, string idioma)
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
                        " (indicators_id, page, pages, per_page, total, idioma)"+
                        " VALUES "+
                        " (@indicators_id, @page, @pages, @per_page, @total, @idioma) ";

                    using (var conexion = objConectar.Conectar("bm"))
                    {
                        var command = new SqlCommand(sql_query, conexion);
                        command.Parameters.AddWithValue("indicators_id", indicators_row["indicators_id"]);
                        command.Parameters.AddWithValue("page", indicators_row["page"]);
                        command.Parameters.AddWithValue("pages", indicators_row["pages"]);
                        command.Parameters.AddWithValue("per_page", indicators_row["per_page"]);
                        command.Parameters.AddWithValue("total", indicators_row["total"]);
                        command.Parameters.AddWithValue("idioma", idioma);

                        conexion.Open();
                        if (command.ExecuteNonQuery() > 0)
                        {
                            estado = true;
                        }
                        else
                        {
                            estado = false;
                        }
                        conexion.Close();
                    }

                }

                foreach (DataRow indicator_row in dt_indicator.Rows)
                {
                    if (!ExisteIndicator(indicator_row, idioma))
                    {
                        //Query para insertar en la tabla indicators
                        sql_query = " INSERT INTO I_indicator " +
                            " (name, indicator_id, sourceNote, sourceOrganization, id, indicators_id, idioma)" +
                            " VALUES " +
                            " (@name, @indicator_id, @sourceNote, @sourceOrganization, @id, @indicators_id, @idioma) ";

                        using (var conexion = objConectar.Conectar("bm"))
                        {
                            var command = new SqlCommand(sql_query, conexion);
                            command.Parameters.AddWithValue("name", indicator_row["name"]);
                            command.Parameters.AddWithValue("indicator_id", indicator_row["indicator_id"]);
                            command.Parameters.AddWithValue("sourceNote", indicator_row["sourceNote"]);
                            command.Parameters.AddWithValue("sourceOrganization", indicator_row["sourceOrganization"]);
                            command.Parameters.AddWithValue("id", indicator_row["id"]);
                            command.Parameters.AddWithValue("indicators_id", indicator_row["indicators_id"]);
                            command.Parameters.AddWithValue("idioma", idioma);

                            conexion.Open();

                            if (command.ExecuteNonQuery() > 0)
                            {
                                estado = true;
                            }
                            else
                            {
                                estado = false;
                            }

                            conexion.Close();

                        }
                    }

                }

                foreach (DataRow source_row in dt_source.Rows)
                {
                    if (!ExisteSource(source_row, idioma))
                    {

                        //Query para insertar en la tabla indicators
                        sql_query = " INSERT INTO I_source " +
                            " (id, source_text, indicator_id, idioma)" +
                            " VALUES " +
                            " (@id, @source_text, @indicator_id, @idioma) ";

                        using (var conexion = objConectar.Conectar("bm"))
                        {
                            var command = new SqlCommand(sql_query, conexion);
                            command.Parameters.AddWithValue("id", source_row["id"]);
                            command.Parameters.AddWithValue("source_text", source_row["source_text"]);
                            command.Parameters.AddWithValue("indicator_id", source_row["indicator_id"]);
                            command.Parameters.AddWithValue("idioma", idioma);

                            conexion.Open();
                            if (command.ExecuteNonQuery() > 0)
                            {
                                estado = true;
                            }
                            else
                            {
                                estado = false;
                            }
                            conexion.Close();
                        }
                    }
                }

                foreach (DataRow topics_row in dt_topics.Rows)
                {
                    if (!ExisteTopics(topics_row, idioma))
                    {

                        //Query para insertar en la tabla indicators
                        sql_query = " INSERT INTO I_topics " +
                            " (topics_id, indicator_id, idioma)" +
                            " VALUES " +
                            " (@topics_id, @indicator_id, @idioma) ";

                        using (var conexion = objConectar.Conectar("bm"))
                        {
                            var command = new SqlCommand(sql_query, conexion);
                            command.Parameters.AddWithValue("topics_id", topics_row["topics_id"]);
                            command.Parameters.AddWithValue("indicator_id", topics_row["indicator_id"]);
                            command.Parameters.AddWithValue("idioma", idioma);

                            conexion.Open();
                            if (command.ExecuteNonQuery() > 0)
                            {
                                estado = true;
                            }
                            else
                            {
                                estado = false;
                            }
                            conexion.Close();
                        }
                    }
                }

                foreach (DataRow topic_row in dt_topic.Rows)
                {

                    if (!ExisteTopic(topic_row, idioma))
	                {
		 
                        //Query para insertar en la tabla indicators
                        sql_query = " INSERT INTO I_topic " +
                            " (id, topic_Text, topics_id, idioma)" +
                            " VALUES " +
                            " (@id, @topic_Text, @topics_id, @idioma) ";

                        using (var conexion = objConectar.Conectar("bm"))
                        {
                            var command = new SqlCommand(sql_query, conexion);
                            command.Parameters.AddWithValue("id", topic_row["id"]);
                            command.Parameters.AddWithValue("topic_Text", topic_row["topic_Text"]);
                            command.Parameters.AddWithValue("topics_id", topic_row["topics_id"]);
                            command.Parameters.AddWithValue("idioma", idioma);
                        
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

        //Funcion que verifica si existe registro en tabla C_Country
        public bool ExisteCountry(DataRow rowCountry)
        {
            bool estado = true;
            string sql_query = null;

            try
            {

                sql_query = " select "+
                    " COALESCE(count(*), 0) "+
                    " FROM "+
                    " C_country "+
                    " where "+
                    " country_Id = @country_id ";

                using (var conexion = objConectar.Conectar("bm"))
                {
                    var command = new SqlCommand(sql_query, conexion);

                
                    command.Parameters.AddWithValue("country_id", rowCountry["country_id"]);
                    //command.Parameters.AddWithValue("idioma", idioma);

                    conexion.Open();
                    int result = int.Parse(command.ExecuteScalar().ToString());

                    if (result > 0)
                    {
                        estado = true;
                    }
                    else
                    {
                        estado = false;
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

        //Funcion que verifica si existe registro en tabla C_Region
        public bool ExisteRegion(DataRow rowRegion)
        {
            bool estado = true;
            string sql_query = null;

            try
            {

                sql_query = " SELECT "+
                    " COALESCE(count(*), 0) "+
                    " FROM "+
                    " C_region "+
                    " where "+
                    " country_Id = @country_id ";

                using (var conexion = objConectar.Conectar("bm"))
                {
                    var command = new SqlCommand(sql_query, conexion);


                    command.Parameters.AddWithValue("country_id", rowRegion["country_id"]);
                    //command.Parameters.AddWithValue("idioma", idioma);

                    conexion.Open();
                    int result = int.Parse(command.ExecuteScalar().ToString());

                    if (result > 0)
                    {
                        estado = true;
                    }
                    else
                    {
                        estado = false;
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

        //Funcion que verifica si existe registro en tabla C_AdminRegion
        public bool ExisteAdminRegion(DataRow rowAdminRegion)
        {
            bool estado = true;
            string sql_query = null;

            try
            {

                sql_query = " SELECT " +
                    " COALESCE(count(*), 0) " +
                    " FROM " +
                    " C_adminregion " +
                    " where " +
                    " country_Id = @country_id ";

                using (var conexion = objConectar.Conectar("bm"))
                {
                    var command = new SqlCommand(sql_query, conexion);


                    command.Parameters.AddWithValue("country_id", rowAdminRegion["country_id"]);
                    //command.Parameters.AddWithValue("idioma", idioma);

                    conexion.Open();
                    int result = int.Parse(command.ExecuteScalar().ToString());

                    if (result > 0)
                    {
                        estado = true;
                    }
                    else
                    {
                        estado = false;
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

        //Funcion que verifica si existe registro en tabla C_incomeLevel
        public bool ExisteIncomeLevel(DataRow rowIncomeLevel)
        {
            bool estado = true;
            string sql_query = null;

            try
            {

                sql_query = " SELECT " +
                    " COALESCE(count(*), 0) " +
                    " FROM " +
                    " C_incomelevel " +
                    " where " +
                    " country_Id = @country_id ";

                using (var conexion = objConectar.Conectar("bm"))
                {
                    var command = new SqlCommand(sql_query, conexion);


                    command.Parameters.AddWithValue("country_id", rowIncomeLevel["country_id"]);
                    //command.Parameters.AddWithValue("idioma", idioma);

                    conexion.Open();
                    int result = int.Parse(command.ExecuteScalar().ToString());

                    if (result > 0)
                    {
                        estado = true;
                    }
                    else
                    {
                        estado = false;
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

        //Funcion que verifica si existe registro en tabla C_incomeLevel
        public bool ExisteLendingType(DataRow rowLendingType)
        {
            bool estado = true;
            string sql_query = null;

            try
            {

                sql_query = " SELECT " +
                    " COALESCE(count(*), 0) " +
                    " FROM " +
                    " C_lendingType " +
                    " where " +
                    " country_Id = @country_id ";

                using (var conexion = objConectar.Conectar("bm"))
                {
                    var command = new SqlCommand(sql_query, conexion);


                    command.Parameters.AddWithValue("country_id", rowLendingType["country_id"]);
                    //command.Parameters.AddWithValue("idioma", idioma);

                    conexion.Open();
                    int result = int.Parse(command.ExecuteScalar().ToString());

                    if (result > 0)
                    {
                        estado = true;
                    }
                    else
                    {
                        estado = false;
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

                    if (!ExisteCountry(row_country))
                    {
                        //Query para insertar en la tabla C_country
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
                }

                foreach (DataRow row_region in dt_region.Rows)
                {
                    if (!ExisteRegion(row_region))
                    {


                        //Query para insertar en la tabla C_Region
                        sql_query = " INSERT INTO C_region " +
                            " (id, region_Text, country_Id) " +
                            " VALUES " +
                            " (@id, @region_Text, @country_Id) ";

                        using (var conexion = objConectar.Conectar("bm"))
                        {
                            var command = new SqlCommand(sql_query, conexion);
                            command.Parameters.AddWithValue("id", row_region["id"]);
                            command.Parameters.AddWithValue("region_Text", row_region["region_Text"]);
                            command.Parameters.AddWithValue("country_Id", row_region["country_Id"]);
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

                foreach (DataRow row_adminregion in dt_adminregion.Rows)
                {
                    if (!ExisteAdminRegion(row_adminregion))
                    {


                        //Query para insertar en la tabla C_adminregion
                        sql_query = " INSERT INTO C_adminregion " +
                            " (id, adminregion_Text, country_Id) " +
                            " VALUES " +
                            " (@id, @adminregion_Text, @country_Id) ";

                        using (var conexion = objConectar.Conectar("bm"))
                        {
                            var command = new SqlCommand(sql_query, conexion);
                            command.Parameters.AddWithValue("id", row_adminregion["id"]);
                            command.Parameters.AddWithValue("adminregion_Text", row_adminregion["adminregion_Text"]);
                            command.Parameters.AddWithValue("country_Id", row_adminregion["country_Id"]);
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

                foreach (DataRow row_incomelevel in dt_incomelevel.Rows)
                {
                    if (!ExisteIncomeLevel(row_incomelevel))
                    {


                        //Query para insertar en la tabla C_incomeLevel
                        sql_query = " INSERT INTO C_incomeLevel " +
                            " (id, incomeLevel_Text, country_Id) " +
                            " VALUES " +
                            " (@id, @incomeLevel_Text, @country_Id) ";

                        using (var conexion = objConectar.Conectar("bm"))
                        {
                            var command = new SqlCommand(sql_query, conexion);
                            command.Parameters.AddWithValue("id", row_incomelevel["id"]);
                            command.Parameters.AddWithValue("incomeLevel_Text", row_incomelevel["incomeLevel_Text"]);
                            command.Parameters.AddWithValue("country_Id", row_incomelevel["country_Id"]);
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

                foreach (DataRow row_lendingType in dt_lendingType.Rows)
                {
                    if (!ExisteLendingType(row_lendingType))
                    {
                        //Query para insertar en la tabla C_lendingType
                        sql_query = " INSERT INTO C_lendingType " +
                            " (id, lendingType_Text, country_Id) " +
                            " VALUES " +
                            " (@id, @lendingType_Text, @country_Id) ";

                        using (var conexion = objConectar.Conectar("bm"))
                        {
                            var command = new SqlCommand(sql_query, conexion);
                            command.Parameters.AddWithValue("id", row_lendingType["id"]);
                            command.Parameters.AddWithValue("lendingType_Text", row_lendingType["lendingType_Text"]);
                            command.Parameters.AddWithValue("country_Id", row_lendingType["country_Id"]);
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
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                
                throw;
            }
            return estado;
        }

        //Funcion que almacena metadata de indicador
        public bool SaveMetaData(DataSet metadata_list)
        {
            bool estado = true;
            string sql_query = null;
            try
            {
                DataTable dt_data = metadata_list.Tables["data"];
                DataTable dt_indicator = metadata_list.Tables["indicator"];
                DataTable dt_country = metadata_list.Tables["country"];

                foreach (DataRow row_data in dt_data.Rows)
                {
                    //Query para insertar en la tabla data
                    sql_query = " INSERT INTO D_data " +
                        " (data_id, date, value, decimal, page, pages, per_page)" +
                        " VALUES " +
                        " (@data_id, @date, @value, @decimal, @page, @pages, @per_page) ";

                    using (var conexion = objConectar.Conectar("bm"))
                    {
                        var command = new SqlCommand(sql_query, conexion);
                        command.Parameters.AddWithValue("data_id", row_data["data_id"]);
                        command.Parameters.AddWithValue("date", row_data["date"]);
                        command.Parameters.AddWithValue("value", row_data["value"]);
                        command.Parameters.AddWithValue("decimal", row_data["decimal"]);
                        command.Parameters.AddWithValue("page", row_data["page"]);
                        command.Parameters.AddWithValue("pages", row_data["pages"]);
                        command.Parameters.AddWithValue("per_page", row_data["per_page"]);
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

                foreach (DataRow row_indicator in dt_indicator.Rows)
                {
                    //Query para insertar en la tabla indicator
                    sql_query = " INSERT INTO D_indicator " +
                        " (id, indicator_Text, data_Id)" +
                        " VALUES " +
                        " (@id, @indicator_Text, @data_Id) ";

                    using (var conexion = objConectar.Conectar("bm"))
                    {
                        var command = new SqlCommand(sql_query, conexion);
                        command.Parameters.AddWithValue("id", row_indicator["id"]);
                        command.Parameters.AddWithValue("indicator_Text", row_indicator["indicator_Text"]);
                        command.Parameters.AddWithValue("data_Id", row_indicator["data_id"]);
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
                    //Query para insertar en la tabla country
                    sql_query = " INSERT INTO D_country " +
                        " (id, country_Text, data_Id)" +
                        " VALUES " +
                        " (@id, @country_Text, @data_Id) ";

                    using (var conexion = objConectar.Conectar("bm"))
                    {
                        var command = new SqlCommand(sql_query, conexion);
                        command.Parameters.AddWithValue("id", row_country["id"]);
                        command.Parameters.AddWithValue("country_Text", row_country["country_Text"]);
                        command.Parameters.AddWithValue("data_id", row_country["data_id"]);
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
                
                throw;
            }
            return estado;
        }

        //Funcion que obtiene cantidad de indicadores
        public int CantidadIndicadores()
        {
            int cantidad_indicadores = 0;
            string sql_query = null;

            try
            {
                sql_query = "  select "+
                    " count(ii.cantidad_indicadores) "+
                    " from "+
                    " (SELECT  "+
                    " COUNT(*) as cantidad_indicadores  "+
                    " FROM  "+
                    " I_indicator  "+
                    " group	by indicator_id) as ii ";

                using (var conn = objConectar.Conectar("bm"))
                {
                    var command = new SqlCommand(sql_query, conn);

                    conn.Open();
                    cantidad_indicadores = int.Parse(command.ExecuteScalar().ToString());
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                
                throw;
            }

            return cantidad_indicadores;
        }

        //Funcion que obtiene cantidad de paises
        public int CantidadPaises()
        {
            int cantidad_paises = 0;
            string sql_query = null;

            try
            {
                sql_query = " SELECT " +
                    " COUNT(*) as cantidad_paises " +
                    " FROM " +
                    " C_Country ";

                using (var conn = objConectar.Conectar("bm"))
                {
                    var command = new SqlCommand(sql_query, conn);

                    conn.Open();
                    cantidad_paises = int.Parse(command.ExecuteScalar().ToString());
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }

            return cantidad_paises;
        }

        //Funcion que obtiene cantidad de metadata
        public int CantidadMetaData()
        {
            int cantidad_metadata = 0;
            string sql_query = null;

            try
            {
                sql_query = " select "+
                    " count(*) "+
                    " FROM "+
                    " D_data ";

                using (var conn = objConectar.Conectar("bm"))
                {
                    var command = new SqlCommand(sql_query, conn);

                    conn.Open();
                    cantidad_metadata = int.Parse(command.ExecuteScalar().ToString());
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }

            return cantidad_metadata;
        }

        //Funcion que obtiene listado de paises
        public DataTable SelectPaises()
        {
            var dataTablePaises = new DataTable();
            string sql_query = null;

            try
            {
                sql_query = " select "+
                    " iso2Code as code, name "+
                    " FROM "+
                    " C_country "+
                    " where len(name) > 0 "+
                    " order by name ";

                using (var con = objConectar.Conectar("bm"))
                {
                    var command = new SqlCommand(sql_query, con);
                    var dataAdapter = new SqlDataAdapter(command);

                    con.Open();
                    dataAdapter.Fill(dataTablePaises);
                    con.Close();
                }
            }
            catch  (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                
                throw;
            }
            return dataTablePaises;
        }

        //Funcion que obtiene listado de temas
        public DataTable SelectTemas()
        {
            var dataTableTemas = new DataTable();
            string sql_query = null;

            try
            {
                sql_query = "  select "+
                    " it.id, it.topic_Text + ' - '+ algo.topic_Text as topic_Text "+
                    " FROM "+
                    " I_topic it, "+
                    " (select itin.id, itin.topic_Text "+
                    " from "+
                    " I_topic itin "+
                    " where itin.idioma = 'es' "+
                    " group by itin.id, itin.topic_Text) as algo "+
                    " where "+
                    " it.idioma = 'en' "+
                    " and it.id = algo.id "+
                    " group by it.id, it.topic_Text, algo.topic_Text ";

                using (var con = objConectar.Conectar("bm"))
                {
                    var command = new SqlCommand(sql_query, con);
                    var dataAdapter = new SqlDataAdapter(command);

                    con.Open();
                    dataAdapter.Fill(dataTableTemas);
                    con.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }
            return dataTableTemas;
        }

        //Funcion que obtiene listado de indicadores por tema
        public DataTable SelectIndicadores(int id_tema)
        {
            var dataTableIndicadores = new DataTable();
            string sql_query = null;

            try
            {
                sql_query = " select "+
                    " ii.indicator_id as id, ii.name as name "+
                    " FROM "+
                    " I_topic it "+
                    " left join  "+
                    " I_topics its on "+
                    " its.topics_id = it.topics_id "+
                    " left join  "+
                    " I_indicator ii ON "+
                    " its.indicator_id = ii.indicator_id "+
                    " where "+
                    " it.id = @id_tema ";

                using (var con = objConectar.Conectar("bm"))
                {
                    var command = new SqlCommand(sql_query, con);
                    command.Parameters.AddWithValue("id_tema", id_tema);

                    var dataAdapter = new SqlDataAdapter(command);

                    con.Open();
                    dataAdapter.Fill(dataTableIndicadores);
                    con.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }
            return dataTableIndicadores;
        }
    }
}
