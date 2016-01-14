using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GetExternalData.COMTRADE
{
    public partial class BitacoraUN : System.Web.UI.Page
    {
        Negocio.COMTRADE.CN_BitacoraUN objBitacora = new Negocio.COMTRADE.CN_BitacoraUN();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtieneEncabezado();    
            }
            
        }

        //Funcion que obtiene detalles de bitacora y llena gridview
        void ObtieneEncabezado()
        {
            var dt_bitacora = new DataTable();

            dt_bitacora = objBitacora.SelectEncBitacora();

            gvEncBitacora.DataSource = dt_bitacora;
            gvEncBitacora.DataBind();

        }

        protected void lkb_detalles_Click(object sender, EventArgs e)
        {
            if (getCodigoPaisGridView() != null)
            {
                Response.Redirect("~/COMTRADE/BitacoraDetUN.aspx?cp="+getCodigoPaisGridView()+"&ca="+getAnioGridView().ToString()+"&chs="+getCodigoHSGridView());
            }
            else
            {
                Mensaje("Seleccione un registro.");
            }
        }

        void Mensaje(string texto)
        {
            string jv = @"<script>alert('"+texto+"');</script>";

            ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", jv, false);
        }

        protected string getCodigoPaisGridView()
        {
            string codigo_pais = null;

            for (int i = 0; i < gvEncBitacora.Rows.Count - 1; i++)
            {

                RadioButton rb_check = gvEncBitacora.Rows[i].FindControl("rb_registro") as RadioButton;

                if (rb_check.Checked)
                {
                    codigo_pais = gvEncBitacora.Rows[i].Cells[1].Text;
                    break;
                }
                else
                {
                    codigo_pais = null;
                }
            }

            return codigo_pais;
        }

        protected int getAnioGridView()
        {
            int anio = 0;

            for (int i = 0; i < gvEncBitacora.Rows.Count - 1; i++)
            {

                RadioButton rb_check = gvEncBitacora.Rows[i].FindControl("rb_registro") as RadioButton;

                if (rb_check.Checked)
                {
                    
                    anio = Convert.ToInt32(gvEncBitacora.Rows[i].Cells[3].Text);
                    break;
                }
                else
                {
                    anio = 0;
                }
            }

            return anio;
        }

        protected string getCodigoHSGridView()
        {
            string codigo_hs = null;

            for (int i = 0; i < gvEncBitacora.Rows.Count - 1; i++)
            {

                RadioButton rb_check = gvEncBitacora.Rows[i].FindControl("rb_registro") as RadioButton;

                if (rb_check.Checked)
                {
                    codigo_hs = gvEncBitacora.Rows[i].Cells[4].Text;
                    break;
                }
                else
                {
                    codigo_hs = null;
                }
            }

            return codigo_hs;
        }

        protected void  gvEncBitacora_PageIndexChanging(Object sender, GridViewPageEventArgs e) 
        {
            gvEncBitacora.PageIndex = e.NewPageIndex;
            ObtieneEncabezado();
        }
    }
}