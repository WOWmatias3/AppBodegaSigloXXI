using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;


namespace BLL
{
    public class ingredienteBLL
    {
        public int id_ingrediente { get; set; }
        public string nombre_ins { get; set; }
        public double stock { get; set; }


        public static ingredienteDAL instance = null;


        public ingredienteBLL() { }


        public ingredienteBLL(int id_ingrediente,
                        string nombre_ins,
                        double stock)
        {
            this.id_ingrediente = id_ingrediente;
            this.nombre_ins = nombre_ins;       
            this.stock = stock;

        }

        public bool btnAceptar(ingredienteBLL objaux)
        {
            ingredienteDAL au = new ingredienteDAL();
            au.id_ingrediente = this.id_ingrediente;
            au.nombre_ins = this.nombre_ins;
            au.stock = this.stock;
            return au.SolicitarInsumo(au);
        }

        public DataTable AllingredientesList()
        {
            ingredienteDAL li = new ingredienteDAL();
            DataTable dt = li.getIngredientes();
            return dt;
        }

        public DataTable getIngrediente(string nombre_insumo)
        {
           ingredienteDAL aiu = new ingredienteDAL();
            DataTable dt = aiu.BuscaringNomb(nombre_insumo);
            return dt;
        }

        public DataTable GetIngHab()
        {
            ingredienteDAL li = new ingredienteDAL();
            DataTable dt = li.getIngredientesHab();
            return dt;
        }


        public DataTable Get_ingCritico()
        {
            ingredienteDAL li = new ingredienteDAL();
            return li.Get_ing_critico();
        }



        public bool UpdateStockIngrediente(int id, double cantidad)
        {
            ingredienteDAL ingDAL = new ingredienteDAL();
            return ingDAL.UpdateIngredienteStock(id,cantidad);
        }


        //update2


        public double GetStock(int id)
        {
            ingredienteDAL ingDAL = new ingredienteDAL();
            return ingDAL.GetStockIngrediente(id);
        }

        public bool UpdateOnlyStockingrediente(int id, double cantidad)
        {
            ingredienteDAL ingDAL = new ingredienteDAL();
            return ingDAL.SetOnlyIngredienteStock(id,cantidad);
        }
        public bool UpdateStockAddIngrediente(int id, double cantidad)
        {
            ingredienteDAL ingDAL = new ingredienteDAL();
            return ingDAL.AgregaIngredienteStock(id, cantidad);
        }

       

        public DataTable GetAllIngredientes()
        {
            ingredienteDAL li = new ingredienteDAL();
            DataTable dt = li.GetAllIngredientes();
            return dt;
        }
        

        public int Get_idbynom(string nom)
        {
            ingredienteDAL pd = new ingredienteDAL();
            return pd.Get_iding_nomb(nom);
        }

    }
}
