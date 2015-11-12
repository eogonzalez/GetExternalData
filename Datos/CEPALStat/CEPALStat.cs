using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Entidad;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace Datos.CEPALStat
{
    public class CEPALStat
    {
        
        //Funcion que almacena la informacion de los temas en la base datos
        public bool SaveDataTheme(DataSet temas_list)
        {
            var objConectar = new General.ConectarService();
 
            bool estado = true;
            string sql_query = null;
            try
            {
                DataTable dt_temas = temas_list.Tables["item"];

                foreach (DataRow temas_row in dt_temas.Rows){
                    //Query para insertar en la tabla de temas
                    sql_query = " INSERT INTO A_Thematic " +
                        " (item_Id, name, idIndicator, id_area, "+
                        " item_Id_0, id_tema) " +
                        " VALUES "+
                        " (@item_Id, @name, @idIndicator, @id_area, "+
                        " @item_Id_0, @id_tema) ";

                    using ( var conexion = objConectar.Conectar())
                    {
                        var command = new SqlCommand(sql_query, conexion);
                        command.Parameters.AddWithValue("item_Id", temas_row["item_Id"]);
                        command.Parameters.AddWithValue("name", temas_row["name"]);
                        command.Parameters.AddWithValue("idIndicator", temas_row["idIndicator"]);    
                        command.Parameters.AddWithValue("id_area", temas_row["id_area"]);    
                        command.Parameters.AddWithValue("item_Id_0", temas_row["item_Id_0"]);    
                        command.Parameters.AddWithValue("id_tema", temas_row["id_tema"]);    
                        conexion.Open();
                       
                       if ( command.ExecuteNonQuery()  > 0)
                       {    
                           //Si se inserta tema
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

            }
            catch (Exception)
            {
                estado = false;   
                throw;
            }
            return estado;
        }

        //Funcion que almacena la informacion de las dimensiones en la base datos
        public bool SaveDataDimensions(DataSet list_dim)
        {
            var objConectar = new General.ConectarService();

            bool estado = true;
            string sql_query = null;
            try
            {
                DataTable dt_dimensions = list_dim.Tables["dimensions"];
                DataTable dt_dim = list_dim.Tables["dim"];
                DataTable dt_des = list_dim.Tables["des"];

                foreach (DataRow dimensions_row in dt_dimensions.Rows)
                {
                    //Query para insertar en la tabla de dimensions
                    sql_query = " INSERT INTO B_1_dimensions " +
                        " (dimensions_Id, idIndicator, unidad) " +
                        " VALUES " +
                        " (@dimensions_Id, @idIndicator, @unidad) ";
                        

                    using (var conexion = objConectar.Conectar())
                    {
                        var command = new SqlCommand(sql_query, conexion);
                        command.Parameters.AddWithValue("dimensions_Id", dimensions_row["dimensions_Id"]);
                        command.Parameters.AddWithValue("idIndicator", dimensions_row["idIndicator"]);
                        command.Parameters.AddWithValue("unidad", dimensions_row["unidad"]);
                        
                        conexion.Open();

                        if (command.ExecuteNonQuery() > 0)
                        {
                            //Si se inserta tema
                            estado = true;
                        }
                        else
                        {
                            estado = false;
                        }
                    }
                }

                foreach (DataRow dim_row in dt_dim.Rows)
                {
                    //Query para insertar en la tabla de dimensions
                    sql_query = " INSERT INTO B_2_dim " +
                        " (dim_id, name, id, dimensions_Id) " +
                        " VALUES " +
                        " (@dim_id, @name, @id, @dimensions_Id) ";
                        

                    using (var conexion = objConectar.Conectar())
                    {
                        var command = new SqlCommand(sql_query, conexion);
                        command.Parameters.AddWithValue("dim_id", dim_row["dim_id"]);
                        command.Parameters.AddWithValue("name", dim_row["name"]);
                        command.Parameters.AddWithValue("id", dim_row["id"]);
                        command.Parameters.AddWithValue("dimensions_Id", dim_row["dimensions_Id"]);
                        
                        conexion.Open();

                        if (command.ExecuteNonQuery() > 0)
                        {
                            //Si se inserta tema
                            estado = true;
                        }
                        else
                        {
                            estado = false;
                        }
                    }
                }

                foreach (DataRow des_row in dt_des.Rows)
                {
                    //Query para insertar en la tabla de dimensions
                    sql_query = " INSERT INTO B_3_des "+
                        " ([name] "+
                        " ,[id] "+
                        " ,[order]  "+
                        " ,[in] "+
                        " ,[dim_Id]) "+
                        " VALUES "+
                        " (@name,@id,@order,@in,@dim_Id)";


                    using (var conexion = objConectar.Conectar())
                    {
                        var command = new SqlCommand(sql_query, conexion);
                        command.Parameters.AddWithValue("name", des_row["name"]);
                        command.Parameters.AddWithValue("id", des_row["id"]);
                        command.Parameters.AddWithValue("order", des_row["order"]);
                        command.Parameters.AddWithValue("in", des_row["in"]);
                        command.Parameters.AddWithValue("dim_Id", des_row["dim_Id"]);

                        conexion.Open();

                        if (command.ExecuteNonQuery() > 0)
                        {
                            //Si se inserta tema
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

        //Funcion que almacena la informacion de la metadata en la base datos
        public bool SaveMetaData(DataSet metad_list)
        {
            var objConectar = new General.ConectarService();

            bool estado = true;
            string sql_query = null;
            try
            {
                DataTable dt_metadatos = metad_list.Tables["metadatos"];
                DataTable dt_dato = metad_list.Tables["dato"];
                DataTable dt_fuente = metad_list.Tables["fuente"];

                int idIndicator = Convert.ToInt16(dt_metadatos.Rows[0]["idIndicator"].ToString());
                
                foreach (DataRow metadatos_row in dt_metadatos.Rows)
                {
                    //Query para insertar en la tabla de dimensions
                    sql_query = " INSERT INTO C_metadatos " +
                        " ([idIndicator],[indicador],[tema],[area],[nota] " +
                        " ,[unidad],[definicion],[caracteristicas],[metodologia] " +
                        " ,[comentarios]) " +
                        " VALUES " +
                        " (@idIndicator,@indicador,@tema,@area,@nota,@unidad,@definicion " +
                        " ,@caracteristicas,@metodologia,@comentarios) ";



                    using (var conexion = objConectar.Conectar())
                    {
                        var command = new SqlCommand(sql_query, conexion);
                        command.Parameters.AddWithValue("idIndicator", metadatos_row["idIndicator"]);
                        command.Parameters.AddWithValue("indicador", metadatos_row["indicador"]);
                        command.Parameters.AddWithValue("tema", metadatos_row["tema"]);
                        command.Parameters.AddWithValue("area", metadatos_row["area"]);
                        command.Parameters.AddWithValue("nota", metadatos_row["nota"]);
                        command.Parameters.AddWithValue("unidad", metadatos_row["unidad"]);
                        command.Parameters.AddWithValue("definicion", metadatos_row["definicion"]);
                        command.Parameters.AddWithValue("caracteristicas", metadatos_row["caracteristicas_dato"]);
                        command.Parameters.AddWithValue("metodologia", metadatos_row["metodologia_calculo"]);
                        command.Parameters.AddWithValue("comentarios", metadatos_row["comentarios"]);

                        conexion.Open();

                        if (command.ExecuteNonQuery() > 0)
                        {
                            //Si se inserta tema
                            estado = true;
                        }
                        else
                        {
                            estado = false;
                        }
                    }
                }

                int cont_item = 0;
                int correlativo = GetCorrMetaData(idIndicator)+1;

                foreach (DataRow dato_row in dt_dato.Rows)
                {
                    cont_item++;
                    foreach (DataColumn column in dt_dato.Columns)
                    {
                        //Query para insertar en la tabla de dimensions
                        sql_query = " INSERT INTO C_dato " +
                            " (correlativo, corr_item, idIndicator, columna, valor) " +
                            " VALUES " +
                            " (@correlativo, @corr_item, @idIndicator, @columna, @valor) ";


                        using (var conexion = objConectar.Conectar())
                        {
                            var command = new SqlCommand(sql_query, conexion);
                            command.Parameters.AddWithValue("correlativo", correlativo);
                            command.Parameters.AddWithValue("corr_item", cont_item);
                            command.Parameters.AddWithValue("idIndicator", idIndicator);
                            command.Parameters.AddWithValue("columna", column.ColumnName);
                            command.Parameters.AddWithValue("valor", dato_row[column].ToString());

                            conexion.Open();

                            if (command.ExecuteNonQuery() > 0)
                            {
                                //Si se inserta tema
                                estado = true;
                            }
                            else
                            {
                                estado = false;
                            }
                        }
                    }
                }

                foreach (DataRow fuente_row in dt_fuente.Rows)
                {
                    //Query para insertar en la tabla de dimensions
                    sql_query = " INSERT INTO C_fuente " +
                            " ([id],[descripcion],[url_publicacion],[sigla_organismo] " +
                            " ,[nombre_organismo],[url_organizacion],[fuentes_Id]) " +
                            " VALUES " +
                            " (@id, @descripcion, @url_publicacion, @sigla_organismo, @nombre_organismo, @url_organizacion ,@fuentes_Id)";


                    using (var conexion = objConectar.Conectar())
                    {
                        var command = new SqlCommand(sql_query, conexion);
                        command.Parameters.AddWithValue("id", fuente_row["id"]);
                        command.Parameters.AddWithValue("descripcion", fuente_row["descripcion"]);
                        command.Parameters.AddWithValue("url_publicacion", fuente_row["url_publicacion"]);
                        command.Parameters.AddWithValue("sigla_organismo", fuente_row["sigla_organismo"]);
                        command.Parameters.AddWithValue("nombre_organismo", fuente_row["nombre_organismo"]);
                        command.Parameters.AddWithValue("url_organizacion", fuente_row["url_organizacion"]);
                        command.Parameters.AddWithValue("fuentes_id", fuente_row["fuentes_id"]);

                        conexion.Open();

                        if (command.ExecuteNonQuery() > 0)
                        {
                            //Si se inserta tema
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

        //Funcion que obtiene el ultimo correlativo
        int GetCorrMetaData(int idIndicator)
        {
            var objConectar = new General.ConectarService();
            int correlativo = 0;
            string sql_query = null;
            var dt_corr = new DataTable();
            try
            {
                sql_query = " SELECT COALESCE(( " +
                " SELECT MAX(correlativo)  " +
                " FROM C_dato " +
                " WHERE idIndicator = @idIndicator ),0) correlativo ";

                using (var conn = objConectar.Conectar())
                {
                    var command = new SqlCommand(sql_query, conn);
                    command.Parameters.AddWithValue("idIndicator", idIndicator);
                    var da = new SqlDataAdapter(command);
                    da.Fill(dt_corr);
                }
                correlativo = Convert.ToInt32(dt_corr.Rows[0]["correlativo"].ToString());
            }
            catch(SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                
                throw;
            }
            return correlativo;
        }

    }
}
