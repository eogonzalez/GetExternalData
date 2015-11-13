using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace GetExternalData.BM
{
    public partial class BM : System.Web.UI.Page
    {
        Negocio.General.NGeneral objGeneral = new Negocio.General.NGeneral();
        Negocio.BM.NBM objBM = new Negocio.BM.NBM();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        void SetTheProgress(HtmlGenericControl bar, string value)
        {
            bar.Attributes.Add("style", string.Format("width:{0};", value));
        }

        protected void btnGet_Click(object sender, EventArgs e)
        {

            //System.Windows.Forms.Application.DoEvents();

            SetTheProgress(barBMInicators, "5%");
            //Verifica si archivo ya ha sido descargado hoy
            string contenido = null;


            string filename = "C:/Getdata/BM/getIndicators_" + Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + ".xml";
            string url = @"http://api.worldbank.org/en/indicators";
            contenido = DateTime.Today.ToString() + " - Verificando si el archivo " + filename + " ya ha sido cargado.";

            txt_log_Indicators.Text = contenido;

            DataSet indicadores_list = new DataSet();

            if (objGeneral.ExisteArchivo(filename))
            {
                //Si archivo existe
                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ya existe.";
                txt_log_Indicators.Text = contenido;

                indicadores_list = objGeneral.ReadArchivo(filename);

                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ha sido cargado.";
                txt_log_Indicators.Text = contenido;

            }
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc = objGeneral.ObtieneArchivo(url);
                //Si archivo no existe
                SetTheProgress(barBMInicators, "15%");

                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo no existe.";
                txt_log_Indicators.Text = contenido;

                objGeneral.SaveArchivo(xmlDoc, filename);

                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo se ha almacenado.";
                txt_log_Indicators.Text = contenido;

                SetTheProgress(barBMInicators, "25%");

                indicadores_list = objGeneral.ReadArchivo(filename);

                string total_registros = null;
                total_registros = indicadores_list.Tables["indicators"].Rows[0]["total"].ToString();

                url = @"http://api.worldbank.org/en/indicators?per_page="+total_registros;
                //filename = "C:/Getdata/BM/getIndicators_full_" + Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + ".xml";

                xmlDoc = objGeneral.ObtieneArchivo(url);
                objGeneral.SaveArchivo(xmlDoc, filename);
                indicadores_list = objGeneral.ReadArchivo(filename);

                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ha sido cargado.";
                txt_log_Indicators.Text = contenido;
            }

            SetTheProgress(barBMInicators, "50%");
            
            objBM.SaveDataIndicators(indicadores_list);

            contenido = contenido + "\n" + DateTime.Today.ToString() + " - La estructura del archivo ha sido almacenado en la Base de Datos.";
            txt_log_Indicators.Text = contenido;

            //objCepal.GetThematicTree();
            SetTheProgress(barBMInicators, "100%");
        }

        protected void btn_get_paises_bm_Click(object sender, EventArgs e)
        {
            //System.Windows.Forms.Application.DoEvents();

            SetTheProgress(bar_get_paises_bm, "5%");
            //Verifica si archivo ya ha sido descargado hoy
            string contenido = null;

            

            string filename = "C:/Getdata/BM/getPaises_" + Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + ".xml";
            string url = @"http://api.worldbank.org/countries";
            contenido = DateTime.Today.ToString() + " - Verificando si el archivo " + filename + " ya ha sido cargado.";

            txt_log_paises_bm.Text = contenido;

            DataSet paises_list = new DataSet();

            if (objGeneral.ExisteArchivo(filename))
            {
                //Si archivo existe
                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ya existe.";
                txt_log_paises_bm.Text = contenido;

                paises_list = objGeneral.ReadArchivo(filename);

                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ha sido cargado.";
                txt_log_paises_bm.Text = contenido;

            }
            else
            {
                var xmlDoc = new XmlDocument();
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                //StreamReader reader = new StreamReader(dataStream);
                using (GZipStream gzip = new GZipStream(dataStream, CompressionMode.Decompress))
                {
                    using (XmlReader xmlwriter = XmlReader.Create(gzip, new XmlReaderSettings()))
                    {
                        xmlwriter.MoveToContent();
                        
                        xmlDoc.Load(xmlwriter);
                    }
                }
                
          
                //xmlDoc = objGeneral.ObtieneArchivo(url);

                //Si archivo no existe
                SetTheProgress(bar_get_paises_bm, "15%");

                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo no existe.";
                txt_log_paises_bm.Text = contenido;

                objGeneral.SaveArchivo(xmlDoc, filename);

                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo se ha almacenado.";
                txt_log_paises_bm.Text = contenido;

                SetTheProgress(bar_get_paises_bm, "25%");

                paises_list = objGeneral.ReadArchivo(filename);

                string total_registros = null;
                total_registros = paises_list.Tables["indicators"].Rows[0]["total"].ToString();

                url = @"http://api.worldbank.org/es/countries?per_page=" + total_registros;
                filename = "C:/Getdata/BM/getPaises_full_" + Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + ".xml";

                xmlDoc = objGeneral.ObtieneArchivo(url);
                objGeneral.SaveArchivo(xmlDoc, filename);
                paises_list = objGeneral.ReadArchivo(filename);

                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ha sido cargado.";
                txt_log_paises_bm.Text = contenido;
            }

            SetTheProgress(bar_get_paises_bm, "50%");

            //objBM.SaveDataIndicators(paises_list);

            contenido = contenido + "\n" + DateTime.Today.ToString() + " - La estructura del archivo ha sido almacenado en la Base de Datos.";
            txt_log_paises_bm.Text = contenido;

            //objCepal.GetThematicTree();
            SetTheProgress(bar_get_paises_bm, "100%");
        }
    }
}