using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.CEPALStat
{
    public class indicador{
        public string name {get; set;}
        public int idIndicator {get;set;}
    }

    public class area{
        
        public string name {get; set;}
        public int id_area {get; set;}

        public IEnumerable<indicador> indicadores {get;set;}

        //public IEnumerable<area> areas {get;set;}
           
    }

    public class tema
    {
        public string name { get; set; }
        public int id_tema { get; set; }
        public IEnumerable<area> areas { get; set; }
    }

}
