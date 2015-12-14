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

        //Funcion que obtiene listado de codigos de incisos de la data de la comtrade
        public DataTable IncisosPendientesList(string sistem_harmony_type, int codigo_pais, int anio_carga)
        {
            DataTable incisos_list = new DataTable();
            string sql_query = null;
            
            try
            {
                //Query que consulta el listado de incisos obtenidos de la comtrade
                sql_query = "   SELECT [code] "+
                    " FROM [UNDB].[dbo].[Commodities] "+
                    " where  "+
                    " level = 4 and class = @class AND "+
                    " code not in "+
                    " (SELECT "+
                    " codigo_inciso "+
                    " from "+
                    " Control_carga_det "+
                    " where "+
                    " codigo_pais = @codigo_pais AND "+
                    " anio_carga = @anio_carga AND "+
                    " codigo_sistem_harmony = @class AND "+
                    " estado = 'F') "+
                    " ORDER BY code ";
 

                using (var con = objConectar.Conectar("un"))
                {
                    var command = new SqlCommand(sql_query, con);
                    command.Parameters.AddWithValue("class", sistem_harmony_type);
                    command.Parameters.AddWithValue("codigo_pais", codigo_pais);
                    command.Parameters.AddWithValue("anio_carga", anio_carga);

                    var dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(incisos_list);
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

            return incisos_list;
        }

        bool ExisteMetadata(DataRow row_metadata)
        {
            bool estado = true;
            string sql_query = null;
            try
            {
                sql_query = " SELECT "+
                    " COALESCE(COUNT(1), 0) "+
                    " FROM MetaData " +
                    " where " +
                    " cmdCode = @cmdCode AND "+
                    " rt3ISO = @rt3ISO AND "+
                    " pt3ISO = @pt3ISO AND "+
                    " yr = @yr AND "+
                    " rgCode = @rgCode AND "+
                    " pfCode = @pfCode ";
                using (var conexion = objConectar.Conectar("un"))
                {
                    var command = new SqlCommand(sql_query, conexion);
                    command.Parameters.AddWithValue("cmdCode", row_metadata["cmdCode"]);
                    command.Parameters.AddWithValue("rt3ISO", row_metadata["rt3ISO"]);
                    command.Parameters.AddWithValue("pt3ISO", row_metadata["pt3ISO"]);
                    command.Parameters.AddWithValue("yr", row_metadata["yr"]);
                    command.Parameters.AddWithValue("rgCode", row_metadata["rgCode"]);
                    command.Parameters.AddWithValue("pfCode", row_metadata["pfCode"]);

                    conexion.Open();
                    int result = int.Parse(command.ExecuteScalar().ToString());
                    
                    if ( result > 0 )
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
                
                throw;
            }
            return estado;
        }

        //Funcion que almacena la metadata de la COMTRADE
        public bool SaveMetaData(DataSet metadata_list)
        {
            bool estado = true;
            string sql_query = null;
            try
            {
                DataTable dt_metadata = metadata_list.Tables["r"];

                foreach (DataRow row_metadata in dt_metadata.Rows)
                {
                    if (!ExisteMetadata(row_metadata))
                    {
                        //Query para insertar en la tabla country
                        sql_query = " INSERT INTO Metadata " +
                            " (pfCode, yr, aggrLevel, IsLeaf, rgCode, rgDesc, rtCode, rtTitle, rt3ISO, "+
                            " ptCode, ptTitle, pt3ISO, cmdCode, cmdDescE, qtCode, qtDesc, tradeQuantity, "+
                            " NetWeight, TradeValue, estCode)" +
                            " VALUES " +
                            " (@pfCode, @yr, @aggrLevel, @IsLeaf, @rgCode, @rgDesc, @rtCode, @rtTitle, @rt3ISO, "+
                            " @ptCode, @ptTitle, @pt3ISO, @cmdCode, @cmdDescE, @qtCode, @qtDesc, @tradeQuantity, "+
                            " @NetWeight, @TradeValue, @estCode) ";

                        using (var conexion = objConectar.Conectar("un"))
                        {
                            var command = new SqlCommand(sql_query, conexion);
                            command.Parameters.AddWithValue("pfCode", row_metadata["pfCode"]);
                            command.Parameters.AddWithValue("yr", row_metadata["yr"]);
                            command.Parameters.AddWithValue("aggrLevel", row_metadata["aggrLevel"]);
                            command.Parameters.AddWithValue("IsLeaf", row_metadata["IsLeaf"]);
                            command.Parameters.AddWithValue("rgCode", row_metadata["rgCode"]);
                            command.Parameters.AddWithValue("rgDesc", row_metadata["rgDesc"]);
                            command.Parameters.AddWithValue("rtCode", row_metadata["rtCode"]);
                            command.Parameters.AddWithValue("rtTitle", row_metadata["rtTitle"]);
                            command.Parameters.AddWithValue("rt3ISO", row_metadata["rt3ISO"]);
                            command.Parameters.AddWithValue("ptCode", row_metadata["ptCode"]);
                            command.Parameters.AddWithValue("ptTitle", row_metadata["ptTitle"]);
                            command.Parameters.AddWithValue("pt3ISO", row_metadata["pt3ISO"]);
                            command.Parameters.AddWithValue("cmdCode", row_metadata["cmdCode"]);
                            command.Parameters.AddWithValue("cmdDescE", row_metadata["cmdDescE"]);
                            command.Parameters.AddWithValue("qtCode", row_metadata["qtCode"]);
                            command.Parameters.AddWithValue("qtDesc", row_metadata["qtDesc"]);
                            command.Parameters.AddWithValue("tradeQuantity", row_metadata["tradeQuantity"]);
                            command.Parameters.AddWithValue("NetWeight", row_metadata["NetWeight"]);
                            command.Parameters.AddWithValue("TradeValue", row_metadata["TradeValue"]);
                            command.Parameters.AddWithValue("estCode", row_metadata["estcode"]);

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

        //Funcion que verifica si es carga nueva
        public bool ExisteCarga(int codigo_pais, int anio_carga, string codigo_sh)
        {
            bool estado = true;
            string sql_query = null;

            try
            {
                //Query que verifica si la carga es nueva
                sql_query = " SELECT " +
                    " coalesce(count(estado), 0) existe " +
                    " FROM " +
                    " Control_carga " +
                    " where " +
                    " codigo_pais = @codigo_pais AND " +
                    " anio_carga = @anio_carga AND " +
                    " codigo_sistem_harmony = @codigo_sistem_harmony ";

                using (var conexion = objConectar.Conectar("un"))
                {
                    var command = new SqlCommand(sql_query, conexion);
                    command.Parameters.AddWithValue("codigo_pais", codigo_pais);
                    command.Parameters.AddWithValue("anio_carga", anio_carga);
                    command.Parameters.AddWithValue("codigo_sistem_harmony", codigo_sh);

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
            catch  (SqlException)
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

        //Funcion que inserta registro de nueva carga de datos
        public bool SaveNuevaCarga(int codigo_pais, int anio_carga, string codigo_sh)
        {
            bool estado = true;
            string sql_query = null;

            try
            {
                //Query que inserta nueva carga
                sql_query = " INSERT INTO [UNDB].[dbo].[Control_carga] "+
                    " ([codigo_pais],[anio_carga] "+
                    " ,[codigo_sistem_harmony],[fecha_inicio] "+
                    " ,[estado]) "+
                    " VALUES "+
                    " (@codigo_pais,@anio_carga "+
                    " ,@codigo_sistem_harmony,@fecha_inicio "+
                    " ,@estado) ";

                using (var conexion = objConectar.Conectar("un"))
                {
                    var command = new SqlCommand(sql_query, conexion);
                    command.Parameters.AddWithValue("codigo_pais", codigo_pais);
                    command.Parameters.AddWithValue("anio_carga", anio_carga);
                    command.Parameters.AddWithValue("codigo_sistem_harmony", codigo_sh);
                    command.Parameters.AddWithValue("fecha_inicio", DateTime.Now);
                    command.Parameters.AddWithValue("estado", "P");
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

        //Funcion que verifica si el inciso a operar esta pendiente o no
        public bool ExisteIncisoDetalle(int codigo_pais, int anio_carga, string codigo_sh, string codigo_inciso)
        {
            bool estado = true;
            string sql_query = null;
            try
            {
                sql_query = " SELECT "+
                    " COALESCE(count(*), 0) "+
                    " FROM "+
                    " Control_carga_det "+
                    " where "+
                    " codigo_pais = @codigo_pais AND "+
                    " anio_carga = @anio_carga AND "+
                    " codigo_sistem_harmony = @codigo_sh AND "+
                    " codigo_inciso = @codigo_inciso AND "+
                    " estado = 'P' ";

                using (var conexion = objConectar.Conectar("un"))
                {
                    var command = new SqlCommand(sql_query, conexion);
                    command.Parameters.AddWithValue("codigo_pais", codigo_pais);
                    command.Parameters.AddWithValue("anio_carga", anio_carga);
                    command.Parameters.AddWithValue("codigo_sh", codigo_sh);
                    command.Parameters.AddWithValue("codigo_inciso", codigo_inciso);

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

        //Funcion que almacena en bitacora inciso a obtener y almacenar
        public bool SaveIncisoDetalle(int codigo_pais, int anio_carga, string codigo_sh, string codigo_inciso)
        {
            bool estado = true;
            string sql_query = null;
            try
            {
                sql_query = " INSERT INTO [UNDB].[dbo].[Control_carga_det] "+
                    " ([codigo_pais],[anio_carga] "+
                    " ,[codigo_sistem_harmony],[codigo_inciso] "+
                    " ,[hora_inicio], [estado]) "+
                    " VALUES "+
                    " (@codigo_pais,@anio_carga,@codigo_sistem_harmony "+
                    " ,@codigo_inciso,@hora_inicio "+
                    " ,@estado)";

                using (var conexion = objConectar.Conectar("un"))
                {
                    var command = new SqlCommand(sql_query, conexion);
                    command.Parameters.AddWithValue("codigo_pais", codigo_pais);
                    command.Parameters.AddWithValue("anio_carga", anio_carga);
                    command.Parameters.AddWithValue("codigo_sistem_harmony", codigo_sh);
                    command.Parameters.AddWithValue("codigo_inciso", codigo_inciso);
                    command.Parameters.AddWithValue("hora_inicio", DateTime.Now.ToString("h:mm:ss"));
                    command.Parameters.AddWithValue("estado", "P");
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

        //Funcion que actuliza el estado de la transaccion
        public bool UpdateIncisoDetalle(int codigo_pais, int anio_carga, string codigo_sh, string codigo_inciso)
        {
            bool estado = true;
            string sql_query = null;
            DateTime hora_fin;
            try
            {
                sql_query = " UPDATE Control_carga_det "+
                    " SET "+
                    " estado = 'F', "+
                    " hora_fin = @hora_fin, "+
                    " tiempo_total = convert(varchar(10), DATEDIFF(hour, cast(hora_inicio as DATETIME), cast(@hora_fin as DATETIME)))+':' "+
                    " +convert(varchar(10), DATEDIFF(minute, cast(hora_inicio as DATETIME), cast(@hora_fin as DATETIME))%60)+':' "+
                    " +convert(varchar(10), DATEDIFF(SECOND, cast(hora_inicio as DATETIME), cast(@hora_fin as DATETIME))%60) "+
                    " WHERE "+
                    " codigo_pais =  @codigo_pais AND "+
                    " anio_carga =  @anio_carga AND "+
                    " codigo_sistem_harmony = @codigo_sh AND "+
                    " codigo_inciso = @codigo_inciso ";

                hora_fin = DateTime.Now;
                using (var conexion = objConectar.Conectar("un"))
                {
                    var command = new SqlCommand(sql_query, conexion);

                    command.Parameters.AddWithValue("hora_fin", hora_fin.ToString("h:mm:ss"));
                    command.Parameters.AddWithValue("codigo_pais", codigo_pais);
                    command.Parameters.AddWithValue("anio_carga", anio_carga);
                    command.Parameters.AddWithValue("codigo_sh", codigo_sh);
                    command.Parameters.AddWithValue("codigo_inciso", codigo_inciso);
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

        //Actualiza estado de carga
        public bool UpdateCarga(int codigo_pais, int anio_carga, string codigo_sh)
        {
            bool estado = true;
            string sql_query = null;

            try
            {
                //Query que actualiza carga
                sql_query = " UPDATE [UNDB].[dbo].[Control_carga] " +
                    " SET " +
                    " fecha_fin = @fecha_fin, " +
                    " estado = @estado " +
                    " WHERE " +
                    " codigo_pais = @codigo_pais AND "+
                    " anio_carga = @anio_carga AND " +
                    " codigo_sistem_harmony = @codigo_sistem_harmony ";

                using (var conexion = objConectar.Conectar("un"))
                {
                    var command = new SqlCommand(sql_query, conexion);
                    command.Parameters.AddWithValue("codigo_pais", codigo_pais);
                    command.Parameters.AddWithValue("anio_carga", anio_carga);
                    command.Parameters.AddWithValue("codigo_sistem_harmony", codigo_sh);
                    command.Parameters.AddWithValue("fecha_fin", DateTime.Now);
                    command.Parameters.AddWithValue("estado", "F");
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

        //Verifica si carga ha finalizado
        public bool FinalizaCarga(int codigo_pais, int anio_carga, string codigo_sh)
        {
            bool estado = true;
            string sql_query = null;

            try
            {
                sql_query = "  SELECT " +
                    " COALESCE(count(*), 0) " +
                    " FROM " +
                    " Control_carga " +
                    " where " +
                    " codigo_pais = @codigo_pais AND " +
                    " anio_carga = @anio_carga AND " +
                    " codigo_sistem_harmony = @codigo_sh and " +
                    " estado = 'F' ";

                using (var conexion = objConectar.Conectar("un"))
                {
                    var command = new SqlCommand(sql_query, conexion);
                    command.Parameters.AddWithValue("codigo_pais", codigo_pais);
                    command.Parameters.AddWithValue("anio_carga", anio_carga);
                    command.Parameters.AddWithValue("codigo_sh", codigo_sh);
                    

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
