using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.COMTRADE
{
    public class CN_BitacoraUN
    {
        Datos.COMTRADE.CD_BitacoraUN obj_CDBitacora = new Datos.COMTRADE.CD_BitacoraUN();

        //Funcion que obtiene el detalle del registro seleccionado de la bitacora
        public DataTable SelectDetBitacora(Entidad.COMTRADE.CE_BitacoraUN objBitacora)
        {
            return obj_CDBitacora.SelectDetBitacora(objBitacora);
        }

        //Funcion que obtiene el encabezado de bitacora
        public DataTable SelectEncBitacora()
        {
            return obj_CDBitacora.SelectEncBitacora();
        }

    }
}
