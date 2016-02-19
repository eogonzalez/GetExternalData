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
        public bool SaveDataIndicators(DataSet indicator_list, string idioma)
        {
            return objBM.SaveDataIndicators(indicator_list, idioma);
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

        //Funcion que obtiene cantidad de indicadores
        public int CantidadIndicadores()
        {
            return objBM.CantidadIndicadores();
        }

        //Funcion que obtiene cantidad de paises
        public int CantidadPaises()
        {
            return objBM.CantidadPaises();
        }

        //Funcion que obtiene cantidad de metadata
        public int CantidadMetaData()
        {
            return objBM.CantidadMetaData();
        }

        //Funcion que obtiene listado de paises
        public DataTable SelectPaises()
        {
            return objBM.SelectPaises();
        }

        //Funcion que obtiene listado de temas
        public DataTable SelectTemas()
        {
            return objBM.SelectTemas();
        }

        //Funcion que obtiene listado de indicadores por tema
        public DataTable SelectIndicadores(int id_tema)
        {
            return objBM.SelectIndicadores(id_tema);
        }
    }
}
