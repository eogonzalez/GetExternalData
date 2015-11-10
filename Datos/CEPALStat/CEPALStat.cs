using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Entidad;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace Datos.CEPALStat
{
    public class CEPALStat
    {
        

        //Verifica si el archivo ya esta existe
        public bool ExisteArchivo(string filename)
        {
            return File.Exists(filename);
        }
        
        //Obtiene archivo del servicio web
        public XmlDocument ObtieneArchivo(string url)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                
                xmlDoc.Load(url);

            }
            catch (XmlException)
            {
                throw;
            }
            catch (Exception)
            {
                
                throw;
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
            catch (XmlException)
            {
                throw;
            }
            catch (Exception)
            {
                
                throw;
            }

            return estado;
        }

        //Lee archivo y llena listado
        public List<Entidad.CEPALStat.tema> ReadArchivo(string filename)
        {
            List<Entidad.CEPALStat.tema> temas_list = new List<Entidad.CEPALStat.tema>();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filename);

                
                //var ds = new DataSet();
                //ds.ReadXml(filename, XmlReadMode.InferSchema);

                XmlNodeList xItem = xmlDoc.GetElementsByTagName("item");

                
                Entidad.CEPALStat.tema tema_item = new Entidad.CEPALStat.tema();

                tema_item.name = ((XmlElement)xItem[0]).GetAttribute("name");
                tema_item.id_tema = Convert.ToInt16(((XmlElement)xItem[0]).GetAttribute("id_tema"));

                List<Entidad.CEPALStat.area> areas_list = new List<Entidad.CEPALStat.area>();

                XmlNodeList xListItems = ((XmlElement)xItem[0]).GetElementsByTagName("item");

                string name_area = null;
                int id_area = 0;

                foreach (XmlElement nodo in xListItems)
                {


                    if ((Convert.ToInt16(nodo.GetAttribute("id_area").Count()) > 0))
                    {
                        Entidad.CEPALStat.area area_item = new Entidad.CEPALStat.area();

                        area_item.name = nodo.GetAttribute("name");
                        area_item.id_area = Convert.ToInt16(nodo.GetAttribute("id_area"));

                        name_area = area_item.name;
                        id_area = area_item.id_area;

                        areas_list.Add(area_item);
                    }
                    else if ((Convert.ToInt16(nodo.GetAttribute("idIndicator").Count()) > 0))
                    {
                        List<Entidad.CEPALStat.indicador> indicador_list = new List<Entidad.CEPALStat.indicador>();
                        Entidad.CEPALStat.indicador indicador_item = new Entidad.CEPALStat.indicador();

                        indicador_item.name = nodo.GetAttribute("name");
                        indicador_item.idIndicator = Convert.ToInt16(nodo.GetAttribute("idIndicator"));
                        indicador_list.Add(indicador_item);

                        Entidad.CEPALStat.area area_item = new Entidad.CEPALStat.area();

                        area_item.name = name_area;
                        area_item.id_area = id_area;
                        area_item.indicadores = indicador_list;
                        areas_list.Add(area_item);

                    }
                    //area_item.indicadores = indicador_list;
                }

                tema_item.areas = areas_list;

                temas_list.Add(tema_item);
            }
            catch(XmlException)
            {
                throw;
            }
            catch (Exception)
            {
                
                throw;
            }
            return temas_list;
        }

        //Funcion que almacena la informacion en la base datos
        public bool SaveData(List<Entidad.CEPALStat.tema> temas_list)
        {
            var objConectar = new General.ConectarService();
 
            bool estado = true;
            string sql_query = null;
            try
            {
                foreach (Entidad.CEPALStat.tema item_temas in temas_list){
                    //Query para insertar en la tabla de temas
                    sql_query = " INSERT INTO Thematic_A_Tema " +
                        " ([name] "+
                        " ,[id_tema]) "+
                        " VALUES "+
                        " (@name "+
                        " ,@id_tema) ";

                    using ( var conexion = objConectar.Conectar())
                    {
                        var command = new SqlCommand(sql_query, conexion);
                        command.Parameters.AddWithValue("name", item_temas.name);
                        command.Parameters.AddWithValue("id_tema", item_temas.id_tema);
                        conexion.Open();
                       
                       if ( command.ExecuteNonQuery()  > 0)
                       {    
                           //Si se inserta tema
                           estado = true;
                           if (item_temas.areas != null)
                           {
                               foreach (var item_area in item_temas.areas)
                               {//Recorro las areas del tema
                                   sql_query = " INSERT INTO Thematic_B_Area " +
                                       " (id_tema " +
                                       " , name " +
                                       " , id_area) " +
                                       " VALUES " +
                                       " (@id_tema, @name, @id_area) ";
                                   using (var con = objConectar.Conectar())
                                   {
                                       var cm = new SqlCommand(sql_query, con);
                                       cm.Parameters.AddWithValue("id_tema", item_temas.id_tema);
                                       cm.Parameters.AddWithValue("name", item_area.name);
                                       cm.Parameters.AddWithValue("id_area", item_area.id_area);
                                       con.Open();
                                       cm.ExecuteNonQuery();

                                   }

                                   if (item_area.indicadores != null)
                                   {//Recorro los indicadores de cada tema
                                       
                                        foreach (var item_indicador in item_area.indicadores)
	                                    {
                                            sql_query = " INSERT INTO Thematic_C_Indicator "+
                                                " ([id_tema] ,[id_area] "+
                                                " ,[name] ,[idIndicator]) "+
                                                " VALUES "+
                                                " (@id_tema, @id_area "+
                                                " ,@name, @idIndicator) ";

                                            using (var con2 = objConectar.Conectar())
                                            {
                                                var cm2 = new SqlCommand(sql_query, con2);
                                                cm2.Parameters.AddWithValue("id_tema", item_temas.id_tema);
                                                cm2.Parameters.AddWithValue("id_area", item_area.id_area);
                                                cm2.Parameters.AddWithValue("name", item_indicador.name);
                                                cm2.Parameters.AddWithValue("idIndicator", item_indicador.idIndicator);
                                                con2.Open();
                                                cm2.ExecuteNonQuery();
                                            }
                                    	}
                                   }
                           }
                           }
                       }
                       else
                       {
                           estado = false;
                       }


                    }

                }
            }
            catch (Exception)
            {
                estado = false;   
                throw;
            }
            return estado;
        }

        //public bool GetThematicTree()
        //{
        //    bool estado = true;
        //    string Url = @"http://interwp.cepal.org/sisgen/ws/cepalstat/getThematicTree.asp?language=spanish&password=87654321";
        //    string filename = "C:/getThematicTree_"+Convert.ToString(DateTime.Today.Year)+"_"+Convert.ToString(DateTime.Today.Month)+"_"+Convert.ToString(DateTime.Today.Day)+".xml";

        //    try
        //    {
        //        XmlDocument xmlDoc = new XmlDocument();

        //        //Consulto Catalogo
        //        xmlDoc.Load(Url);

        //        //Almaceno Archivo
        //        xmlDoc.Save(filename);

        //        //Leo Archivo Fisico
        //        xmlDoc.Load(filename);

        //        XmlNodeList xItem = xmlDoc.GetElementsByTagName("item");
                
        //        List<Entidad.CEPALStat.tema> temas_list = new List<Entidad.CEPALStat.tema>();
        //        Entidad.CEPALStat.tema tema_item = new Entidad.CEPALStat.tema();

        //        tema_item.name = ((XmlElement)xItem[0]).GetAttribute("name");
        //        tema_item.id_tema = Convert.ToInt16(((XmlElement)xItem[0]).GetAttribute("id_tema"));
                
        //        List<Entidad.CEPALStat.area> areas_list = new List<Entidad.CEPALStat.area>();

        //        XmlNodeList xListItems = ((XmlElement)xItem[0]).GetElementsByTagName("item");

        //        string name_area = null;
        //        int id_area = 0;

        //        foreach (XmlElement nodo in xListItems)
        //        {
                    

        //            if ( (Convert.ToInt16(nodo.GetAttribute("id_area").Count()) > 0) ) {
        //                Entidad.CEPALStat.area area_item = new Entidad.CEPALStat.area();

        //                area_item.name = nodo.GetAttribute("name");
        //                area_item.id_area = Convert.ToInt16(nodo.GetAttribute("id_area"));
                        
        //                name_area = area_item.name;
        //                id_area = area_item.id_area;

        //                areas_list.Add(area_item);
        //            }
        //            else if ( (Convert.ToInt16(nodo.GetAttribute("idIndicator").Count()) > 0 )){
        //                List<Entidad.CEPALStat.indicador> indicador_list = new List<Entidad.CEPALStat.indicador>();
        //                Entidad.CEPALStat.indicador indicador_item = new Entidad.CEPALStat.indicador();

        //                indicador_item.name = nodo.GetAttribute("name");
        //                indicador_item.idIndicator = Convert.ToInt16(nodo.GetAttribute("idIndicator"));
        //                indicador_list.Add(indicador_item);

        //                Entidad.CEPALStat.area area_item = new Entidad.CEPALStat.area();

        //                area_item.name = name_area;
        //                area_item.id_area = id_area;
        //                area_item.indicadores = indicador_list;
        //                areas_list.Add(area_item);

        //            }
        //            //area_item.indicadores = indicador_list;
        //        }

        //        tema_item.areas = areas_list;

        //        temas_list.Add(tema_item);


        //    }
        //    catch (XmlException)
        //    {
        //        estado = false;
        //        throw;
        //    }
        //    catch (Exception)
        //    {
        //        estado = false;
                
        //        throw;
        //    }
            
        //    return estado;
        //}


    }
}
