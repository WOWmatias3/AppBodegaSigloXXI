using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class PlatoBLL
    {
        public int id_plato { get; set; }
        public string nombre_plato { get; set; }
        public int precio { get; set; }
        public int stock_preparado { get; set; }
        public int stock_a_preparar { get; set; }
        public string habilitado { get; set; }

        public PlatoBLL()
        {
        }

        public PlatoBLL(int id_plato, string nombre_plato, int precio, int stock_preparado, int stock_a_preparar, string habilitado)
        {
            this.id_plato = id_plato;
            this.nombre_plato = nombre_plato;
            this.precio = precio;
            this.stock_preparado = stock_preparado;
            this.stock_a_preparar = stock_a_preparar;
            this.habilitado = habilitado;
        }

        public DataTable GetPlatosHabilitados()
        {

            PlatoDAL plDAL = new PlatoDAL();
            return plDAL.getPlatosHabilitados();
        }

        public DataTable GetIngredientesPlato(int id_plat)
        {
            PlatoDAL plDAL = new PlatoDAL();
            return plDAL.getIngredientesFromPlato(id_plat);
        }

        public bool SetStockPlato(int id,int cantidad)
        {
            PlatoDAL plDAL = new PlatoDAL();
            return plDAL.UpdatePlatoStock(id,cantidad);
        }
    }
}
