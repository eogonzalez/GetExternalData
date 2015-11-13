using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Negocio.General
{
    public class NGeneral
    {
        Datos.General.General objGeneral = new Datos.General.General();

        //Verifica si el archivo ya esta existe
        public bool ExisteArchivo(string filename)
        {
            return objGeneral.ExisteArchivo(filename);
        }

        //Lee archivo y llena listado
        public DataSet ReadArchivo(string filename)
        {
            return objGeneral.ReadArchivo(filename);
        }

        //Obtiene archivo del servicio web
        public XmlDocument ObtieneArchivo(string url)
        {
            return objGeneral.ObtieneArchivo(url);
        }

        //Almacena Archivo Obtenido del servicio web
        public bool SaveArchivo(XmlDocument xmlDoc, string filename)
        {
            return objGeneral.SaveArchivo(xmlDoc, filename);
        }
    }
}
