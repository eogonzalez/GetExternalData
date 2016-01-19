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
    public partial class PaisesUN : System.Web.UI.Page
    {
        Negocio.General.NGeneral objGeneral = new Negocio.General.NGeneral();
        Negocio.COMTRADE.NUN objUN = new Negocio.COMTRADE.NUN();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActualizarContadores();
            }
        }

        protected void btn_get_paises_un_Click(object sender, EventArgs e)
        {
            SetTheProgress(bar_get_paises_un, "5%");
            //Verifica si archivo ya ha sido descargado hoy
            string contenido = null;

            string filename = "C:/Getdata/UN/getCountries_" + Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + ".xml";
            string url = @"http://comtrade.un.org/ws/refs/getCountryList.aspx";
            contenido = DateTime.Now.ToString() + " - Verificando si el archivo " + filename + " ya ha sido cargado.";

            txt_log_paises_un.Text = contenido;

            ManageFile(filename, url, "PAISES", txt_log_paises_un);
            ActualizarContadores();

            contenido = contenido + "\n" + DateTime.Now.ToString() + " - La estructura del archivo ha sido almacenado en la Base de Datos.";
            txt_log_paises_un.Text = contenido;

            SetTheProgress(bar_get_paises_un, "100%");
        }

        protected bool ManageFile(string filename, string url, string dimension, TextBox control_log)
        {// Verifica si archivo ha sido descargado. Si no ha sido descargado lo descarga del url especificado.
            HtmlGenericControl bar = new HtmlGenericControl();


            string contenido = null;
            bool estado = true;


            // http://comtrade.un.org/ws/get.aspx?cc=??????&px=H2&r=156&y=2007&p=0&rg=2&tv1=0&tv2=500000000&so=1001&qt=n&detail=true&comp=false&max=10&code=YourCode
            // http://comtrade.un.org/ws/get.aspx?cc=??????&px=H2&r=156&y=2007&p=0&rg=2&tv1=0&tv2=500000000&so=1001&qt=n&detail=true&comp=false&max=10
            // http://comtrade.un.org/ws/get.aspx?cc=010190&px=H2&r=156&y=2007&qt=n&detail=true&comp=false

            DataSet metadata_list = new DataSet();

            if (objGeneral.ExisteArchivo(filename))
            {
                //Si archivo existe
                contenido = contenido + "\n" + DateTime.Now.ToString() + " - El archivo ya existe.";
                control_log.Text = contenido;


                metadata_list = objGeneral.ReadArchivo(filename);

                contenido = contenido + "\n" + DateTime.Now.ToString() + " - El archivo ha sido cargado.";
                control_log.Text = contenido;

            }
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                //xmlDoc = objBM.ObtenerArchivoGzip(url);
                xmlDoc = objGeneral.ObtieneArchivo(url);
                //Si archivo no existe
                SetTheProgress(bar, "15%");

                contenido = contenido + "\n" + DateTime.Now.ToString() + " - El archivo no existe.";
                control_log.Text = contenido;

                objGeneral.SaveArchivo(xmlDoc, filename);

                contenido = contenido + "\n" + DateTime.Now.ToString() + " - El archivo se ha almacenado.";
                control_log.Text = contenido;

                SetTheProgress(bar, "25%");

                metadata_list = objGeneral.ReadArchivo(filename);

                contenido = contenido + "\n" + DateTime.Now.ToString() + " - El archivo ha sido cargado.";
                control_log.Text = contenido;
            }

            SetTheProgress(bar, "50%");


            if (metadata_list != null)
            {//Si no es nulo
                if (metadata_list.Tables.Count > 0)
                {//Si trae registros
                    if (dimension == "PAISES")
                    {
                        estado = objUN.SaveCountry(metadata_list);
                    }
                    else if (dimension == "SAC")
                    {
                        var newColumn = new System.Data.DataColumn("HS", typeof(System.String));

                        newColumn.DefaultValue = Session["str_hs"].ToString();
                        metadata_list.Tables[0].Columns.Add(newColumn);

                        estado = objUN.SaveCommodity(metadata_list);
                    }
                    else
                    {
                        estado = objUN.SaveMetaData(metadata_list);
                    }

                }

            }

            return estado;
        }

        void SetTheProgress(HtmlGenericControl bar, string value)
        {
            bar.Attributes.Add("style", string.Format("width:{0};", value));
        }

        protected void ActualizarContadores()
        {
            lbl_cant_paises.Text = objUN.CantidadPaises().ToString();
        }
    }
}