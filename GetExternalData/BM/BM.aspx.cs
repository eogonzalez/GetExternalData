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
            if (!IsPostBack)
            {
                ActualizarContador();
                LlenarComboPaises();
                LlenarComboTemas();
                LlenarComboIndicadores(Convert.ToInt32(ddl_tema.SelectedValue.ToString()));

            }
        }

        void SetTheProgress(HtmlGenericControl bar, string value)
        {
            bar.Attributes.Add("style", string.Format("width:{0};", value));
        }

        protected void btn_get_metadata_bm_Click(object sender, EventArgs e)
        {
            SetTheProgress(bar_get_metadata_bm, "5%");
            
            //Variable que contiene el log
            string contenido = null;
            
            //Obtiene variables de consulta
            string str_indicador = null;
            str_indicador = ddl_indicador.SelectedValue.ToString();
            
            int años = 0;
            if (!string.IsNullOrEmpty(txtAnios.Text))
            {
                años = Convert.ToInt32(txtAnios.Text);    
            }
            
            string pais = null;
            if (!string.IsNullOrEmpty(ddl_pais.SelectedValue.ToString()))
            {
                pais = ddl_pais.SelectedValue.ToString();    
            }
            
            int anio_inicial = 0;
            if (!string.IsNullOrEmpty(txt_anio_inicial.Text))
            {
                anio_inicial = Convert.ToInt32(txt_anio_inicial.Text);    
            }
            

            int anio_final = 0;
            if (!string.IsNullOrEmpty(txt_anio_final.Text))
            {
                anio_final = Convert.ToInt32(txt_anio_final.Text);    
            }
            

            string url = null;
            //http://api.worldbank.org/countries/indicators/SP.POP.TOTl?date=2008:2009
            //http://api.worldbank.org/countries/indicators/SP.POP.TOTl?MRV=5

            if (str_indicador.Length > 0)
            {
                if (anio_inicial > 0 && anio_final > 0)
                {
                    if (!string.IsNullOrEmpty(pais))
                    {
                        url = @"http://api.worldbank.org/countries/"+pais+"/indicators/" + str_indicador + "?date=" + anio_inicial + ":" + anio_final;
                    }
                    else
                    {
                        url = @"http://api.worldbank.org/countries/indicators/"+str_indicador+"?date="+anio_inicial+":"+anio_final;
                    }
                }
                else if (años > 0)
                {
                    if (!string.IsNullOrEmpty(pais))
                    {
                        url = @"http://api.worldbank.org/countries/"+pais+"/indicators/" + str_indicador + "?MRV=" + años;
                    }
                    else
                    {
                        url = @"http://api.worldbank.org/countries/indicators/" + str_indicador + "?MRV=" + años;
                    }
                }
                else
                {
                    contenido = contenido + "\n" + DateTime.Today.ToString() + " - ERROR - El campo de años y rango de años no deben quedar vacios.";
                    txt_log_metadata.Text = contenido;
                    return;
                }
            }
            else
            {
                contenido = contenido + "\n" + DateTime.Today.ToString() + " - ERROR - Campo indicador no puede quedar vacio.";
                txt_log_metadata.Text = contenido;
                return;
            }


            //Verifica si archivo ya ha sido descargado hoy
            string filename = "C:/Getdata/BM/getMetaData_"+str_indicador+"_" + Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + ".xml";
            
            contenido = contenido + "\n" + DateTime.Today.ToString() + " - Verificando si el archivo " + filename + " ya ha sido cargado.";

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
                var xmlDoc = new XmlDocument();
                xmlDoc = objGeneral.ObtieneArchivo(url);
                //xmlDoc = objBM.ObtenerArchivoGzip(url);


                //Si archivo no existe
                SetTheProgress(bar_get_metadata_bm, "15%");

                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo no existe.";
                txt_log_metadata.Text = contenido;

                objGeneral.SaveArchivo(xmlDoc, filename);

                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo se ha almacenado.";
                txt_log_metadata.Text = contenido;

                SetTheProgress(bar_get_metadata_bm, "25%");

                metadata_list = objGeneral.ReadArchivo(filename);

                string total_registros = null;
                total_registros = metadata_list.Tables["data"].Rows[0]["total"].ToString();

                url = url+"&per_page=" + total_registros;
                //filename = "C:/Getdata/BM/getPaises_full_" + Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + ".xml";

                //xmlDoc = objGeneral.ObtieneArchivo(url);
                xmlDoc = objBM.ObtenerArchivoGzip(url);

                objGeneral.SaveArchivo(xmlDoc, filename);
                metadata_list = objGeneral.ReadArchivo(filename);

                contenido = contenido + "\n" + DateTime.Today.ToString() + " - El archivo ha sido cargado.";
                txt_log_metadata.Text = contenido;
            }

            SetTheProgress(bar_get_metadata_bm, "50%");

            objBM.SaveMetaData(metadata_list);

            contenido = contenido + "\n" + DateTime.Today.ToString() + " - La estructura del archivo ha sido almacenado en la Base de Datos.";
            txt_log_metadata.Text = contenido;

            //objCepal.GetThematicTree();
            SetTheProgress(bar_get_metadata_bm, "100%");
        }

        protected void ActualizarContador()
        {
            lbl_cantidad_metadata.Text = objBM.CantidadMetaData().ToString();
        }

        protected void LlenarComboPaises()
        {
            var dt_paises = objBM.SelectPaises();

            ddl_pais.DataSource = dt_paises;
            ddl_pais.DataTextField = "name";
            ddl_pais.DataValueField = "code";
            ddl_pais.DataBind();
        }

        protected void LlenarComboTemas()
        {
            var dt_temas = objBM.SelectTemas();

            ddl_tema.DataSource = dt_temas;
            ddl_tema.DataTextField = "topic_Text";
            ddl_tema.DataValueField = "id";
            ddl_tema.DataBind();
        }

        protected void LlenarComboIndicadores(int id_Tema)
        {
            var dt_indicadores = objBM.SelectIndicadores(id_Tema);

            ddl_indicador.DataSource = dt_indicadores;
            ddl_indicador.DataTextField = "name";
            ddl_indicador.DataValueField = "id";
            ddl_indicador.DataBind();

        }

        protected void ddl_tema_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarComboIndicadores(Convert.ToInt32(ddl_tema.SelectedValue.ToString()));
        }
    }
}