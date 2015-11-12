using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data;

namespace Negocio.CEPALStat
{
    public class NCEPALStat
    {
        Datos.CEPALStat.CEPALStat objDCepal = new Datos.CEPALStat.CEPALStat();

        //Funcion que almacena la informacion de los temas en la base datos
        public bool SaveDataTheme(DataSet temas_list)
        {
            return objDCepal.SaveDataTheme(temas_list);
        }

        //Funcion que almacena la informacion de las dimensiones en la base datos
        public bool SaveDataDimensions(DataSet list_dim)
        {
            return objDCepal.SaveDataDimensions(list_dim);
        }

        //Funcion que almacena la informacion de la metadata en la base datos
        public bool SaveMetaData(DataSet metad_list)
        {
            return objDCepal.SaveMetaData(metad_list);
        }
    }
}
