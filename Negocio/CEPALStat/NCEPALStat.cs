using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


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
        public List<Entidad.CEPALStat.tema> ReadArchivo(string filename)
        {
            return objDCepal.ReadArchivo(filename);
        }

        //Funcion que almacena la informacion en la base datos
        public bool SaveData(List<Entidad.CEPALStat.tema> temas_list)
        {
            return objDCepal.SaveData(temas_list);
        }

        //public bool GetThematicTree()
        //{
        //    return objDCepal.GetThematicTree();
        //}
    }
}
