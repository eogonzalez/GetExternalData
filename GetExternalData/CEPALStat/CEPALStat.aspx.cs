using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace GetExternalData.CEPALStat
{
    public partial class CEPALStat : System.Web.UI.Page
    {
        Negocio.CEPALStat.NCEPALStat objCepal = new Negocio.CEPALStat.NCEPALStat();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            SetTheProgress(barCepal, "5%");
            //Verifica si archivo ya ha sido descargado hoy
            string filename = "C:/getThematicTree_" + Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + ".xml";
            string url = @"http://interwp.cepal.org/sisgen/ws/cepalstat/getThematicTree.asp?language=spanish&password=87654321";
            
            List<Entidad.CEPALStat.tema> temas_list = new List<Entidad.CEPALStat.tema>();

            if (objCepal.ExisteArchivo(filename))
            {
                //Si archivo existe
                temas_list = objCepal.ReadArchivo(filename);
                
            }
            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc = objCepal.ObtieneArchivo(url);
                //Si archivo no existe
                SetTheProgress(barCepal, "15%");

                objCepal.SaveArchivo(xmlDoc, filename);
                SetTheProgress(barCepal, "25%");

                temas_list = objCepal.ReadArchivo(filename);
                
            }
            
            SetTheProgress(barCepal, "50%");
            objCepal.SaveData(temas_list);
            //objCepal.GetThematicTree();
            SetTheProgress(barCepal, "100%");
        }

        void SetTheProgress(HtmlGenericControl bar, string value)
        {
            bar.Attributes.Add("style", string.Format("width:{0};", value));
            //bar.Attributes.Add("value", string.Format(value));
        }
    }
}