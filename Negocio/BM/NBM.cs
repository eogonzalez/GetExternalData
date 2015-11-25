using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Negocio.BM
{
    public class NBM
    {
        Datos.BM.BM objBM = new Datos.BM.BM();

        //Funcion que almacena informacion de los indicadores del banco mundial
        public bool SaveDataIndicators(DataSet indicator_list)
        {
            return objBM.SaveDataIndicators(indicator_list);
        }

        //Funcion que obtiene archivo y descoprime
        public XmlDocument ObtenerArchivoGzip(string url)
        {   
            return objBM.ObtenerArchivoGzip(url);
        }

        //Funcion que almacena informacion de los paises del banco mundial
        public bool SaveDataCountries(DataSet country_list)
        {
            return objBM.SaveDataCountries(country_list);
        }

        //Funcion que almacena metadata de indicador
        public bool SaveMetaData(DataSet metadata_list)
        {
            return objBM.SaveMetaData(metadata_list);
        }
    }
}
