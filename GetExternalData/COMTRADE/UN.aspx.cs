using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
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
            if (!IsPostBack)
            {

                ActualizarContadores();
                LlenarComboPaisMetaData();
                LlenarComboHSClass();
            }   
        }

        void SetTheProgress(HtmlGenericControl bar, string value)
        {
            bar.Attributes.Add("style", string.Format("width:{0};", value));
        }

        protected void btnGetCommodity_Click(object sender, EventArgs e)
        {          
            SetTheProgress(bar_UN_Commodity, "5%");
            //Verifica si archivo ya ha sido descargado hoy
            string contenido = null;
            if (string.IsNullOrEmpty(ddl_hs.SelectedValue))
            {
                return;
            }
            else
            {
                string str_hs = ddl_hs.SelectedValue;
                Session.Add("str_hs", str_hs);

                string filename = "C:/Getdata/UN/getCommodity_"+str_hs+"_" + Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + ".xml";
                string url = @"http://comtrade.un.org/ws/refs/getCommodityList.aspx?px="+str_hs;
                contenido = DateTime.Today.ToString() + " - Verificando si el archivo " + filename + " ya ha sido cargado.";

                txt_log_Commodity.Text = contenido;

                ManageFile(filename, url, "SAC", txt_log_Commodity);

                ActualizarContadores();

                contenido = contenido + "\n" + DateTime.Now.ToString() + " - La estructura del archivo ha sido almacenado en la Base de Datos.";
                txt_log_Commodity.Text = contenido;

                SetTheProgress(bar_UN_Commodity, "100%");
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

        protected void btn_get_metadata_un_Click(object sender, EventArgs e)
        {
            SetTheProgress(bar_get_metadata_un, "5%");
            
            string contenido = null;
            string str_sistem_harmony = null;
            string str_codigo_pais = null;
            int year = 0;

            
            RealizaCargaMetadata(contenido, str_sistem_harmony, str_codigo_pais, year);
            ActualizarContadores();

            SetTheProgress(bar_get_metadata_un, "100%");

        }

        protected void RealizaCargaMetadata(string contenido, string str_sistem_harmony, string str_codigo_pais, int year)
        {
            if (!string.IsNullOrEmpty(ddl_hsMetaData.SelectedValue) && !string.IsNullOrEmpty(ddl_paisMetaData.SelectedValue) && !string.IsNullOrEmpty(txtAnio.Text))
            {//Si las variables no estan vacias

                str_sistem_harmony = ddl_hsMetaData.SelectedValue;
                str_codigo_pais = ddl_paisMetaData.SelectedValue;
                year = Convert.ToInt32(txtAnio.Text);

                if (!objUN.ExisteCarga(Convert.ToInt32(str_codigo_pais), year, str_sistem_harmony))
                {//Si no existe 
                    objUN.SaveNuevaCarga(Convert.ToInt32(str_codigo_pais), year, str_sistem_harmony);
                }

                ProcesaSolicitud(str_sistem_harmony, str_codigo_pais, year);
            }
            else
            {
                contenido = DateTime.Now.ToString() + " -ERROR- Las variables no deben de quedar vacias.";
                txt_log_metadata.Text = contenido;
                return;
            }
        }

        protected bool ProcesaSolicitud(string sistema_harmony, string codigo_pais, int año)
        {
            bool estado = true;
            bool finaliza = true;
            string contenido = null;
            var dt_incisos = new DataTable();
            string filename = null;
            string url = null;
            string commodity_code = null;
            dt_incisos = objUN.IncisosPendientesList(sistema_harmony, Convert.ToInt32(codigo_pais), año);

            contenido = DateTime.Now.ToString() + " - Inicia proceso de carga.";
            txt_log_metadata.Text = contenido;

            foreach (DataRow item_inciso in dt_incisos.Rows)
            {
                finaliza = false;
                commodity_code = item_inciso["code"].ToString();

                if (!objUN.ExisteIncisoDetalle(Convert.ToInt32(codigo_pais), año, sistema_harmony, commodity_code))
                {//Si no existe
                    //Agrego Inciso Detalle
                    objUN.SaveIncisoDetalle(Convert.ToInt32(codigo_pais), año, sistema_harmony, commodity_code);
                }

                filename = "C:/Getdata/UN/metadata/getMetaData_" + commodity_code + "_" + codigo_pais + "_" + año.ToString()+"_" + Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + ".xml";
                url = @"http://comtrade.un.org/ws/get.aspx?cc=" + commodity_code + "??&px=" + sistema_harmony + "&r=" + codigo_pais + "&y=" + año.ToString() + "&qt=n&detail=true&comp=false&code=jjgdN75QdqmVL+fIhYRv3iZPQMJzhIUGBoyISS79Dzw8pKPHiads2eor2IFmGpHV9v3t7uTrB/MW8aaJWj0HaSKoebDL0VNJ9PAcdlAvVNS1YcsTNzzFZmBxp4yH5H4vSLTB1A6SwKmO9Kc+edweEP1YSf0MW8p+nNruPQkVaXg=";

                if (ManageFile(filename, url, "META", txt_log_metadata))
                {
                    estado = true;
                }
                else
                {
                    estado = false;
                    break;
                }

                //Actualizo estado de inciso detalle
                objUN.UpdateIncisoDetalle(Convert.ToInt32(codigo_pais), año, sistema_harmony, commodity_code);

                break;
            }

            if (finaliza)
            {//Si ya no existen archivos a descargar
                if (!objUN.FinalizaCarga(Convert.ToInt32(codigo_pais), año, sistema_harmony))
                {//Si no ha finalizado carga
                    //Actualizo encabezado
                    objUN.UpdateCarga(Convert.ToInt16(codigo_pais), año, sistema_harmony);    
                }
            }

            contenido = contenido + "\n" + DateTime.Now.ToString() + " - El proceso ha terminado.";
            txt_log_metadata.Text = contenido;

            return estado;
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

        protected void LlenarComboPaisMetaData()
        {
            var dt_paises = objUN.SelectPaises();

            ddl_paisMetaData.DataSource = dt_paises;
            ddl_paisMetaData.DataTextField = "name";
            ddl_paisMetaData.DataValueField = "code";
            ddl_paisMetaData.DataBind();
        }

        protected void LlenarComboHSClass()
        {
            var dt_hsClass = objUN.SelectHSClass();

            ddl_hsMetaData.DataSource = dt_hsClass;
            ddl_hsMetaData.DataTextField = "class";
            ddl_hsMetaData.DataValueField = "class";
            ddl_hsMetaData.DataBind();
        }

        protected void ActualizarContadores()
        {
            lbl_cant_paises.Text = objUN.CantidadPaises().ToString();
            lbl_cant_commodities.Text = objUN.CantidadCommodity().ToString();
            lbl_cant_metadata.Text = objUN.CantidadMetaData().ToString();
        }

    }
}