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

        //Verifica si el archivo ya esta existe
        public bool ExisteArchivo(string filename)
        {
            return objDCepal.ExisteArchivo(filename);
        }

        //Obtiene archivo del servicio web
        public XmlDocument ObtieneArchivo(string url)
        {
            return objDCepal.ObtieneArchivo(url);
        }

        //Almacena Archivo Obtenido del servicio web
        public bool SaveArchivo(XmlDocument xmlDoc, string filename)
        {
            return objDCepal.SaveArchivo(xmlDoc, filename);
        }

        //Lee archivo y llena listado
        public DataSet ReadArchivo(string filename)
        {
            return objDCepal.ReadArchivo(filename);
        }

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
