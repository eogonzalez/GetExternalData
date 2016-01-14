using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.COMTRADE
{
    public class NUN
    {
        Datos.COMTRADE.UN objUN = new Datos.COMTRADE.UN();

        //Funcion que almacena los incisos arancelarios de la COMTRADE
        public bool SaveCommodity(DataSet commodities_list)
        {
            return objUN.SaveCommodity(commodities_list);
        }

        //Funcion que obtiene cantidad de incisos
        public int CantidadCommodity()
        {
            return objUN.CantidadCommodity();
        }

        //Funcion que almacena los paises de la COMTRADE
        public bool SaveCountry(DataSet countries_list)
        {
            return objUN.SaveCountry(countries_list);
        }

        //Funcion que obtiene cantidad de paises descargdos
        public int CantidadPaises()
        {
            return objUN.CantidadPaises();
        }

        //Funcion que obtiene listado de codigos de incisos de la data de la comtrade
        public DataTable IncisosPendientesList(string sistem_harmony_type, int codigo_pais, int anio_carga)
        {
            return objUN.IncisosPendientesList(sistem_harmony_type, codigo_pais, anio_carga);
        }

        //Funcion que obtiene la cantidad de registros en la metadata
        public int CantidadMetaData()
        {
            return objUN.CantidadMetaData();
        }

        //Funcion que almacena la metadata de la COMTRADE
        public bool SaveMetaData(DataSet metadata_list)
        {
            return objUN.SaveMetaData(metadata_list);
        }

        //Funcion que verifica si es carga nueva
        public bool ExisteCarga(int codigo_pais, int anio_carga, string codigo_sh)
        {
            return objUN.ExisteCarga(codigo_pais, anio_carga, codigo_sh);
        }

        //Funcion que inserta registro de nueva carga de datos
        public bool SaveNuevaCarga(int codigo_pais, int anio_carga, string codigo_sh)
        {
            return objUN.SaveNuevaCarga(codigo_pais, anio_carga, codigo_sh);
        }

        //Funcion que verifica si el inciso a operar esta pendiente o no
        public bool ExisteIncisoDetalle(int codigo_pais, int anio_carga, string codigo_sh, string codigo_inciso)
        {
            return objUN.ExisteIncisoDetalle(codigo_pais, anio_carga, codigo_sh, codigo_inciso);
        }

        //Funcion que almacena en bitacora inciso a obtener y almacenar
        public bool SaveIncisoDetalle(int codigo_pais, int anio_carga, string codigo_sh, string codigo_inciso)
        {
            return objUN.SaveIncisoDetalle(codigo_pais, anio_carga, codigo_sh, codigo_inciso);
        }

        //Funcion que actuliza el estado de la transaccion
        public bool UpdateIncisoDetalle(int codigo_pais, int anio_carga, string codigo_sh, string codigo_inciso)
        {
            return objUN.UpdateIncisoDetalle(codigo_pais, anio_carga, codigo_sh, codigo_inciso);
        }

        //Actualiza estado de carga
        public bool UpdateCarga(int codigo_pais, int anio_carga, string codigo_sh)
        {
            return objUN.UpdateCarga(codigo_pais, anio_carga, codigo_sh);
        }

        //Verifica si carga ha finalizado
        public bool FinalizaCarga(int codigo_pais, int anio_carga, string codigo_sh)
        {
            return objUN.FinalizaCarga(codigo_pais, anio_carga, codigo_sh);
        }
    }
}
