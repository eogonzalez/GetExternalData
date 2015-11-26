using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.COMTRADE
{
    public class NUN
    {
        Datos.COMTRADE.UN objUN = new Datos.COMTRADE.UN();

        //Funcion que almacena los incisos arancelarios de la COMTRADE
        public bool SaveCommodity(DataSet commodities_list)
        {
            return objUN.SaveCommodity(commodities_list);
        }

        //Funcion que almacena los paises de la COMTRADE
        public bool SaveCountry(DataSet countries_list)
        {
            return objUN.SaveCountry(countries_list);
        }

    }
}
