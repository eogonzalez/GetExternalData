using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GetExternalData.COMTRADE
{
    public partial class BitacoraDetUN : System.Web.UI.Page
    {
        Negocio.COMTRADE.CN_BitacoraUN objBitacora = new Negocio.COMTRADE.CN_BitacoraUN();

        protected void Page_Load(object sender, EventArgs e)
        {
            string codigo_pais;
            int anio;
            string codigo_hs;

            if (!IsPostBack)
            {
                codigo_pais = Request.QueryString["cp"];
                anio = Convert.ToInt32(Request.QueryString["ca"]);
                codigo_hs = Request.QueryString["chs"];


                Session.Add("cp", codigo_pais);
                Session.Add("ca", anio);
                Session.Add("chs", codigo_hs);

                lbl_enc.Text = "Codigo Pais: "+codigo_pais+" Año: "+anio.ToString()+" Codigo Version SAC: "+codigo_hs;

                LlenarGridView(codigo_pais, anio, codigo_hs);
            }
        }

        protected void LlenarGridView(string codigo_pais, int anio, string codigo_hs)
        {
            Entidad.COMTRADE.CE_BitacoraUN obj_CEBitacora = new Entidad.COMTRADE.CE_BitacoraUN();
            var dt_bitacora = new DataTable();

            obj_CEBitacora.codigo_pais = codigo_pais;
            obj_CEBitacora.año = anio;
            obj_CEBitacora.codigo_hs = codigo_hs;

            dt_bitacora = objBitacora.SelectDetBitacora(obj_CEBitacora);
            
            lbl_cantidad.Text = dt_bitacora.Rows.Count.ToString();

            gvDetBitacora.DataSource = dt_bitacora;
            gvDetBitacora.DataBind();

        }

        protected void lkb_regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/COMTRADE/BitacoraUN.aspx");
        }

        protected void gvDetBitacora_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetBitacora.PageIndex = e.NewPageIndex;
            LlenarGridView(Session["cp"].ToString(), Convert.ToInt32(Session["ca"].ToString()), Session["chs"].ToString());
        }
    }
}