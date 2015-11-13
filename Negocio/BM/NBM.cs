using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.BM
{
    public class NBM
    {
        //Funcion que almacena informacion de los indicadores del banco mundial
        public bool SaveDataIndicators(DataSet indicator_list)
        {
            var objBM = new Datos.BM.BM();
            return objBM.SaveDataIndicators(indicator_list);
        }
    }
}
