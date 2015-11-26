using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

namespace GetExternalData.COMTRADE
{
    public partial class UN : System.Web.UI.Page
    {
        Negocio.General.NGeneral objGeneral = new Negocio.General.NGeneral();
        Negocio.COMTRADE.NUN objUN = new Negocio.COMTRADE.NUN();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        void SetTheProgress(HtmlGenericControl bar, string value)
        {
            bar.Attributes.Add("style", string.Format("width:{0};", value));
        }

        protected void btnGetCommodity_Click(object sender, EventArgs e)
        {
            //System.Windows.Forms.Application.DoEvents();

            SetTheProgress(bar_UN_Commodity, "5%");
            //Verifica si archivo ya ha sido descargado hoy
            string contenido = null;
            if (string.IsNullOrEmpty(txtHs.Text))
            {
                return;
            }
            else
            {
                string str_hs = txtHs.Text;

                string filename = "C:/Getdata/UN/getCommodity_"+str_hs+"_" + Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + ".xml";
                string url = @"http://comtrade.un.org/ws/refs/getCommodityList.aspx?px="+str_hs;
                contenido = DateTime.Today.ToString() + " - Verificando si el archivo " + filename + " ya ha sido cargado.";

                txt_log_Commodity.Text = contenido;

                DataSet commodities_list = new DataSet();

                if (objGeneral.ExisteArchivo(filename))
                {
                    //Si archivo existe
                    contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ya existe.";
                    txt_log_Commodity.Text = contenido;

                    commodities_list = objGeneral.ReadArchivo(filename);

                    contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ha sido cargado.";
                    txt_log_Commodity.Text = contenido;

                }
                else
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    //xmlDoc = objBM.ObtenerArchivoGzip(url);
                    xmlDoc = objGeneral.ObtieneArchivo(url);
                    //Si archivo no existe
                    SetTheProgress(bar_UN_Commodity, "15%");

                    contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo no existe.";
                    txt_log_Commodity.Text = contenido;

                    objGeneral.SaveArchivo(xmlDoc, filename);

                    contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo se ha almacenado.";
                    txt_log_Commodity.Text = contenido;

                    SetTheProgress(bar_UN_Commodity, "25%");

                    commodities_list = objGeneral.ReadArchivo(filename);

                    contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ha sido cargado.";
                    txt_log_Commodity.Text = contenido;
                }

                SetTheProgress(bar_UN_Commodity, "50%");

                
                objUN.SaveCommodity(commodities_list);

                contenido = contenido + "\n" + DateTime.Today.ToString() + " - La estructura del archivo ha sido almacenado en la Base de Datos.";
                txt_log_Commodity.Text = contenido;

                //objCepal.GetThematicTree();
                SetTheProgress(bar_UN_Commodity, "100%");
            }
        }

        protected void btn_get_paises_un_Click(object sender, EventArgs e)
        {
            SetTheProgress(bar_get_paises_un, "5%");
            //Verifica si archivo ya ha sido descargado hoy
            string contenido = null;

            string filename = "C:/Getdata/UN/getCountries_" + Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + ".xml";
            string url = @"http://comtrade.un.org/ws/refs/getCountryList.aspx";
            contenido = DateTime.Today.ToString() + " - Verificando si el archivo " + filename + " ya ha sido cargado.";

            txt_log_paises_un.Text = contenido;

            DataSet country_list = new DataSet();

            if (objGeneral.ExisteArchivo(filename))
            {
                //Si archivo existe
                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ya existe.";
                txt_log_paises_un.Text = contenido;

                country_list = objGeneral.ReadArchivo(filename);

                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ha sido cargado.";
                txt_log_paises_un.Text = contenido;

            }
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                //xmlDoc = objBM.ObtenerArchivoGzip(url);
                xmlDoc = objGeneral.ObtieneArchivo(url);
                //Si archivo no existe
                SetTheProgress(bar_get_paises_un, "15%");

                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo no existe.";
                txt_log_paises_un.Text = contenido;

                objGeneral.SaveArchivo(xmlDoc, filename);

                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo se ha almacenado.";
                txt_log_paises_un.Text = contenido;

                SetTheProgress(bar_get_paises_un, "25%");

                country_list = objGeneral.ReadArchivo(filename);

                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ha sido cargado.";
                txt_log_paises_un.Text = contenido;
            }

            SetTheProgress(bar_get_paises_un, "50%");


            objUN.SaveCountry(country_list);

            contenido = contenido + "\n" + DateTime.Today.ToString() + " - La estructura del archivo ha sido almacenado en la Base de Datos.";
            txt_log_paises_un.Text = contenido;

            //objCepal.GetThematicTree();
            SetTheProgress(bar_get_paises_un, "100%");
        }
    }
}