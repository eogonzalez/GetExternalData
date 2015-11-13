using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Datos.General
{
    
    public class ConectarService
    {
        SqlConnection sql_coneccion;

        public SqlConnection Conectar(string db)
        {
            if (db == "cepal")
            {
                sql_coneccion = new SqlConnection(ConfigurationManager.ConnectionStrings["cn_cepal"].ConnectionString);    
            }
            else if (db == "bm")
            {
                sql_coneccion = new SqlConnection(ConfigurationManager.ConnectionStrings["cn_bm"].ConnectionString);    
            }

            return sql_coneccion;
        }
    }
}
