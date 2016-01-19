using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Datos.General
{
    public class General
    {
        //Verifica si el archivo ya esta existe
        public bool ExisteArchivo(string filename)
        {
            return File.Exists(filename);
        }

        //Lee archivo y llena listado
        public DataSet ReadArchivo(string filename)
        {
            var ds = new DataSet();
            try
            {
                ds.ReadXml(filename, XmlReadMode.InferSchema);
            }
            catch (XmlException e)
            {
                throw e;
            }
            catch (Exception e)
            {

                throw e;
            }
            return ds;
        }

        //Obtiene archivo del servicio web
        public XmlDocument ObtieneArchivo(string url)
        {
            var xmlDoc = new XmlDocument();
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Timeout = 1000 * 60 * 5; //milisegundos
                WebResponse res = req.GetResponse();
                Stream responseStream = res.GetResponseStream();
                xmlDoc.Load(responseStream);
                responseStream.Close();
                //xmlDoc.Load(url);
            }
            catch (XmlException e)
            {
                throw e;
            }
            catch  (WebException e)
            {
                throw e;
            }
            catch (Exception e)
            {

                throw e;
            }

            return xmlDoc;
        }

        //Almacena Archivo Obtenido del servicio web
        public bool SaveArchivo(XmlDocument xmlDoc, string filename)
        {
            bool estado = true;

            try
            {
                xmlDoc.Save(filename);
            }
            catch (XmlException e)
            {
                throw e;
            }
            catch (Exception e)
            {

                throw e;
            }

            return estado;
        }

    }
}
