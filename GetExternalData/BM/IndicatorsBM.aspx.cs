using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

namespace GetExternalData.BM
{
    public partial class IndicatorsBM : System.Web.UI.Page
    {
        Negocio.General.NGeneral objGeneral = new Negocio.General.NGeneral();
        Negocio.BM.NBM objBM = new Negocio.BM.NBM();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActualizarContador();
                cb_idioma.SelectedValue = "1";

                //cb_idioma.Attributes.Add("onclick", "return HandleOnCheck()");
                cb_idioma.Attributes.Add("onclick", "radioMe(event);");
                
            }
        }

        void SetTheProgress(HtmlGenericControl bar, string value)
        {
            bar.Attributes.Add("style", string.Format("width:{0};", value));
        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            string contenido = null;
            string url = null;
            string filename = null;
            string idioma = null;

            SetTheProgress(barBMInicators, "5%");
            
            if (cb_idioma.SelectedValue == "1")
            {//Si consulta ingles
                url = @"http://api.worldbank.org/en/indicators";
                filename = "C:/Getdata/BM/getIndicators_en_" + Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + ".xml";
                idioma = "en";
            }
            else
            {//Si consulta español
                url = @"http://api.worldbank.org/es/indicators";
                filename = "C:/Getdata/BM/getIndicators_es_" + Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + ".xml";
                idioma = "es";
            }

            
            contenido = DateTime.Now.ToString() + " - Verificando si el archivo " + filename + " ya ha sido cargado.";

            txt_log_Indicators.Text = contenido;

            //Metodo que manipula el archivo y almacena informacion
            ManageFile(filename, url, idioma, ref contenido);

            ActualizarContador();

            contenido = contenido + "\n" + DateTime.Now.ToString() + " - La estructura del archivo ha sido almacenado en la Base de Datos.";
            txt_log_Indicators.Text = contenido;

            //objCepal.GetThematicTree();
            SetTheProgress(barBMInicators, "100%");
        }

        protected void ActualizarContador()
        {
            lbl_cant_indicadores.Text = objBM.CantidadIndicadores().ToString();
        }

        //Funcion que maneja el archivo y almacena informacion de indicadores
        protected bool ManageFile(string filename, string url, string idioma, ref string contenido)
        {
            //string contenido = null;
            bool estado = true;

            DataSet indicadores_list = new DataSet();

            if (objGeneral.ExisteArchivo(filename))
            {
                //Si archivo existe
                contenido = contenido + "\n" + DateTime.Now.ToString() + " - El archivo ya existe.";
                txt_log_Indicators.Text = contenido;

                indicadores_list = objGeneral.ReadArchivo(filename);

                contenido = contenido + "\n" + DateTime.Now.ToString() + " - El archivo ha sido cargado.";
                txt_log_Indicators.Text = contenido;

            }
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc = objBM.ObtenerArchivoGzip(url);
                //Si archivo no existe
                SetTheProgress(barBMInicators, "15%");

                contenido = contenido + "\n" + DateTime.Now.ToString() + " - El archivo no existe.";
                txt_log_Indicators.Text = contenido;

                objGeneral.SaveArchivo(xmlDoc, filename);

                contenido = contenido + "\n" + DateTime.Now.ToString() + " - El archivo se ha almacenado.";
                txt_log_Indicators.Text = contenido;

                SetTheProgress(barBMInicators, "25%");

                indicadores_list = objGeneral.ReadArchivo(filename);

                string total_registros = null;
                total_registros = indicadores_list.Tables["indicators"].Rows[0]["total"].ToString();

                //url = @"http://api.worldbank.org/en/indicators?per_page=" + total_registros;
                url = url + "?per_page=" + total_registros;
                //filename = "C:/Getdata/BM/getIndicators_full_" + Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + ".xml";

                xmlDoc = objGeneral.ObtieneArchivo(url);
                objGeneral.SaveArchivo(xmlDoc, filename);
                indicadores_list = objGeneral.ReadArchivo(filename);

                contenido = contenido + "\n" + DateTime.Now.ToString() + " - El archivo ha sido cargado.";
                txt_log_Indicators.Text = contenido;
            }


            SetTheProgress(barBMInicators, "50%");

            //var newColumn = new System.Data.DataColumn("idioma", typeof(System.String));

            //newColumn.DefaultValue = idioma;
            //indicadores_list.Tables["indicators"].Columns.Add(new System.Data.DataColumn("idioma", typeof(System.String)));
            //newColumn.DefaultValue = idioma;
            //indicadores_list.Tables["indicator"].Columns.Add(new System.Data.DataColumn("idioma", typeof(System.String)));
            //newColumn.DefaultValue = idioma;
            //indicadores_list.Tables["source"].Columns.Add(new System.Data.DataColumn("idioma", typeof(System.String)));
            //newColumn.DefaultValue = idioma;
            //indicadores_list.Tables["topics"].Columns.Add(new System.Data.DataColumn("idioma", typeof(System.String)));

            estado = objBM.SaveDataIndicators(indicadores_list, idioma);

            return estado;
        }

    }
}