using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.COMTRADE
{
    public class CD_BitacoraUN
    {
        General.ConectarService objConectar = new General.ConectarService();
        Entidad.COMTRADE.CE_BitacoraUN objBitacora = new Entidad.COMTRADE.CE_BitacoraUN();

        //Funcion que obtiene el detalle del registro seleccionado de la bitacora
        public DataTable SelectDetBitacora(Entidad.COMTRADE.CE_BitacoraUN objBitacora)
        {
            DataTable det_list = new DataTable();
            string sql_query = null;
            try
            {
                sql_query = " SELECT [codigo_inciso] "+
                    " ,[hora_inicio],[hora_fin] "+
                    " ,[tiempo_total],[estado] "+
                    " FROM "+
                    " [UNDB].[dbo].[Control_carga_det] "+
                    " WHERE "+
                    " codigo_pais = @codigo_pais AND "+
                    " anio_carga = @anio_carga AND "+
                    " codigo_sistem_harmony = @codigo_sistem_harmony ";

                using (var con = objConectar.Conectar("un"))
                {
                    var command = new SqlCommand(sql_query, con);
                    command.Parameters.AddWithValue("codigo_pais", objBitacora.codigo_pais);
                    command.Parameters.AddWithValue("anio_carga", objBitacora.año);
                    command.Parameters.AddWithValue("codigo_sistem_harmony", objBitacora.codigo_hs);
                    var data_adapter = new SqlDataAdapter(command);
                    data_adapter.Fill(det_list);
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
            return det_list;
        }

        //Funcion que obtiene el encabezado de bitacora
        public DataTable SelectEncBitacora()
        {
            DataTable encabezado_list = new DataTable();
            string sql_query = null;

            try
            {
                //Query que obtiene listado de registros consultados
                sql_query = " SELECT [codigo_pais], coun.name " +
                    " ,[anio_carga], [codigo_sistem_harmony] " +
                    " ,[fecha_inicio],[fecha_fin],[estado] " +
                    " FROM [UNDB].[dbo].[Control_carga] cc, " +
                    " Countries coun " +
                    " WHERE " +
                    " coun.code = cc.codigo_pais "+
                    " order by cc.estado DESC, cc.fecha_inicio";

                using (var con = objConectar.Conectar("un"))
                {
                    var command = new SqlCommand(sql_query, con);
                    var dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(encabezado_list);
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


            return encabezado_list;
        }
    }
}
