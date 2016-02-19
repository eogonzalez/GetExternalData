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
    public partial class PaisesBM : System.Web.UI.Page
    {
        Negocio.General.NGeneral objGeneral = new Negocio.General.NGeneral();
        Negocio.BM.NBM objBM = new Negocio.BM.NBM();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActualizarContador();
            }
        }

        protected void btn_get_paises_bm_Click(object sender, EventArgs e)
        {
            //Verifica si archivo ya ha sido descargado hoy
            string contenido = null;
            string url = null;
            string filename = null;

            SetTheProgress(bar_get_paises_bm, "5%");

            filename = "C:/Getdata/BM/getPaises_" + Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + ".xml";
            url = @"http://api.worldbank.org/countries";
            contenido = DateTime.Now.ToString() + " - Verificando si el archivo " + filename + " ya ha sido cargado.";

            txt_log_paises_bm.Text = contenido;

            ManageFile(filename, url, ref contenido);

            ActualizarContador();

            contenido = contenido + "\n" + DateTime.Now.ToString() + " - La estructura del archivo ha sido almacenado en la Base de Datos.";
            txt_log_paises_bm.Text = contenido;

            //objCepal.GetThematicTree();
            SetTheProgress(bar_get_paises_bm, "100%");
        }

        //Funcion que maneja el archivo y almacena la informacion de paises
        protected bool ManageFile(string filename, string url, ref string contenido)
        {
            bool estado = true;

            DataSet paises_list = new DataSet();

            if (objGeneral.ExisteArchivo(filename))
            {
                //Si archivo existe
                contenido = contenido + "\n" + DateTime.Now.ToString() + " - El archivo ya existe.";
                txt_log_paises_bm.Text = contenido;

                paises_list = objGeneral.ReadArchivo(filename);

                contenido = contenido + "\n" + DateTime.Now.ToString() + " - El archivo ha sido cargado.";
                txt_log_paises_bm.Text = contenido;

            }
            else
            {
                var xmlDoc = new XmlDocument();
                xmlDoc = objBM.ObtenerArchivoGzip(url);


                //Si archivo no existe
                SetTheProgress(bar_get_paises_bm, "15%");

                contenido = contenido + "\n" + DateTime.Now.ToString() + " - El archivo no existe.";
                txt_log_paises_bm.Text = contenido;

                objGeneral.SaveArchivo(xmlDoc, filename);

                contenido = contenido + "\n" + DateTime.Now.ToString() + " - El archivo se ha almacenado.";
                txt_log_paises_bm.Text = contenido;

                SetTheProgress(bar_get_paises_bm, "25%");

                paises_list = objGeneral.ReadArchivo(filename);

                string total_registros = null;
                total_registros = paises_list.Tables["countries"].Rows[0]["total"].ToString();

                url = @"http://api.worldbank.org/es/countries?per_page=" + total_registros;
                //filename = "C:/Getdata/BM/getPaises_full_" + Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + ".xml";

                xmlDoc = objGeneral.ObtieneArchivo(url);
                objGeneral.SaveArchivo(xmlDoc, filename);
                paises_list = objGeneral.ReadArchivo(filename);

                contenido = contenido + "\n" + DateTime.Now.ToString() + " - El archivo ha sido cargado.";
                txt_log_paises_bm.Text = contenido;
            }

            SetTheProgress(bar_get_paises_bm, "50%");

            estado = objBM.SaveDataCountries(paises_list);

            return estado;
        }

        protected void ActualizarContador()
        {
            lbl_cant_paises.Text = objBM.CantidadPaises().ToString();
        }

        void SetTheProgress(HtmlGenericControl bar, string value)
        {
            bar.Attributes.Add("style", string.Format("width:{0};", value));
        }
    }
}