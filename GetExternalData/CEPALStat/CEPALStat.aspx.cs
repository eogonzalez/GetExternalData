using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Data;

namespace GetExternalData.CEPALStat
{
    public partial class CEPALStat : System.Web.UI.Page
    {
        Negocio.CEPALStat.NCEPALStat objCepal = new Negocio.CEPALStat.NCEPALStat();
        Negocio.General.NGeneral objGeneral = new Negocio.General.NGeneral();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            SetTheProgress(barCepal, "5%");
            //Verifica si archivo ya ha sido descargado hoy
            string contenido = null;
            

            string filename = "C:/Getdata/CEPAL/getThematicTree_" + Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + ".xml";
            string url = @"http://interwp.cepal.org/sisgen/ws/cepalstat/getThematicTree.asp?language=spanish&password=87654321";
            contenido = DateTime.Today.ToString() + " - Verificando si el archivo " +filename+ " ya ha sido cargado.";

            txt_log_theme.Text = contenido;

            DataSet temas_list = new DataSet();

            if (objGeneral.ExisteArchivo(filename))
            {
                //Si archivo existe
                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ya existe.";
                txt_log_theme.Text = contenido;

                temas_list = objGeneral.ReadArchivo(filename);

                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ha sido cargado.";
                txt_log_theme.Text = contenido;
                
            }
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc = objGeneral.ObtieneArchivo(url);
                //Si archivo no existe
                SetTheProgress(barCepal, "15%");
                
                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo no existe.";
                txt_log_theme.Text = contenido;

                objGeneral.SaveArchivo(xmlDoc, filename);

                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo se ha almacenado.";
                txt_log_theme.Text = contenido;

                SetTheProgress(barCepal, "25%");

                temas_list = objGeneral.ReadArchivo(filename);

                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ha sido cargado.";
                txt_log_theme.Text = contenido;
            }
            
            SetTheProgress(barCepal, "50%");
            objCepal.SaveDataTheme(temas_list);

            contenido = contenido + "\n" + DateTime.Today.ToString() + " - La estructura del archivo ha sido almacenado en la Base de Datos.";
            txt_log_theme.Text = contenido;

            //objCepal.GetThematicTree();
            SetTheProgress(barCepal, "100%");
        }

        protected void btnGetDim_Click(object sender, EventArgs e)
        {
            SetTheProgress(barCepalDim, "5%");
            //Verifica si archivo ya ha sido descargado hoy
            string contenido = null;
            string indicador = null;
            indicador = txtIdIndicador.Text;

            if (indicador.Length > 0)
	        {
                string filename = "C:/Getdata/CEPAL/Dimensiones/getDimension_" + indicador + "_"+Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + ".xml";
                string url = @"http://interwp.cepal.org/sisgen/ws/cepalstat/getDimensions.asp?idIndicator=" + indicador + "&language=spanish&password=87654321";
                contenido = DateTime.Today.ToString() + " - Verificando si el archivo " + filename + " ya ha sido cargado.";

                txt_log_dim.Text = contenido;

                DataSet dimension_list = new DataSet();

                if (objGeneral.ExisteArchivo(filename))
                {
                    //Si archivo existe
                    contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ya existe.";
                    txt_log_dim.Text = contenido;

                    dimension_list = objGeneral.ReadArchivo(filename);

                    contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ha sido cargado.";
                    txt_log_dim.Text = contenido;

                }
                else
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc = objGeneral.ObtieneArchivo(url);
                    //Si archivo no existe
                    SetTheProgress(barCepalDim, "15%");

                    contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo no existe.";
                    txt_log_dim.Text = contenido;

                    objGeneral.SaveArchivo(xmlDoc, filename);

                    contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo se ha almacenado.";
                    txt_log_dim.Text = contenido;

                    SetTheProgress(barCepalDim, "25%");

                    dimension_list = objGeneral.ReadArchivo(filename);

                    contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ha sido cargado.";
                    txt_log_dim.Text = contenido;
                }

                SetTheProgress(barCepalDim, "50%");
                objCepal.SaveDataDimensions(dimension_list);


                contenido = contenido + "\n" + DateTime.Today.ToString() + " - La estructura del archivo ha sido almacenado en la Base de Datos.";
                txt_log_dim.Text = contenido;

                //objCepal.GetThematicTree();
                SetTheProgress(barCepalDim, "100%");
	        }

        }

        protected void BtnGetMetaData_Click(object sender, EventArgs e)
        {
            SetTheProgress(barCepalMeta, "5%");
            //Verifica si archivo ya ha sido descargado hoy
            string contenido = null;
            string indicador = null;
            indicador = txtMetaDataIndicator.Text;

            if (indicador.Length > 0)
            {
                string filename = "C:/Getdata/CEPAL/Metadata/getMetaData_" + indicador + "_" + Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + ".xml";
                string url = @"http://interwp.cepal.org/sisgen/ws/cepalstat/getDataMeta.asp?idIndicator=" + indicador + "&language=spanish&password=87654321";
                contenido = DateTime.Today.ToString() + " - Verificando si el archivo " + filename + " ya ha sido cargado.";
            
                txt_log_metadata.Text = contenido;

                DataSet metadata_list = new DataSet();

                if (objGeneral.ExisteArchivo(filename))
                {
                    //Si archivo existe
                    contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ya existe.";
                    txt_log_metadata.Text = contenido;

                    metadata_list = objGeneral.ReadArchivo(filename);

                    contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ha sido cargado.";
                    txt_log_metadata.Text = contenido;

                }
                else
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc = objGeneral.ObtieneArchivo(url);
                    //Si archivo no existe
                    SetTheProgress(barCepalMeta, "15%");

                    contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo no existe.";
                    txt_log_metadata.Text = contenido;

                    objGeneral.SaveArchivo(xmlDoc, filename);

                    contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo se ha almacenado.";
                    txt_log_metadata.Text = contenido;

                    SetTheProgress(barCepalMeta, "25%");

                    metadata_list = objGeneral.ReadArchivo(filename);

                    contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ha sido cargado.";
                    txt_log_metadata.Text = contenido;
                }

                SetTheProgress(barCepalMeta, "50%");
                objCepal.SaveMetaData(metadata_list);


                contenido = contenido + "\n" + DateTime.Today.ToString() + " - La estructura del archivo ha sido almacenado en la Base de Datos.";
                txt_log_metadata.Text = contenido;

                //objCepal.GetThematicTree();
                SetTheProgress(barCepalMeta, "100%");
            }
        }

        void SetTheProgress(HtmlGenericControl bar, string value)
        {
            bar.Attributes.Add("style", string.Format("width:{0};", value));
            //bar.Attributes.Add("value", string.Format(value));
        }

    }
}