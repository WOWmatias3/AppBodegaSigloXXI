using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;

namespace BLL
{
    public class bebestibleBLL
    {
        public int id_bebestible { get; set; }
        public string nombre_ins { get; set; }
        public Double stock { get; set; }



        public static ingredienteBLL instance = null;


        public bebestibleBLL() { }


        public bebestibleBLL(int id_bebestible,
       string nombre_bebestible,
       Double stock)

        {
            this.id_bebestible = id_bebestible;
            this.nombre_ins = nombre_bebestible;
            this.stock = stock;
        }
        
        public bool btnAceptar(bebestibleBLL objaux)
        {
            bebestibleDAL bbd = new bebestibleDAL();
            bbd.id_bebestible = this.id_bebestible;
            bbd.nombre_ins = this.nombre_ins;
            bbd.stock = this.stock;
            return bbd.SolicitarBebestible(bbd);

        }

        public DataTable getBebestible()
        {
            bebestibleDAL li = new bebestibleDAL();
            DataTable dt = li.getBebestible();
            return dt;
        }



        public DataTable GetBebHabilitado()
        {
            bebestibleDAL bebDAL = new bebestibleDAL();
            DataTable dt = bebDAL.ListAllBebestiblesHab();
            return dt;
        }

        public double GetStock(int id)
        {
            bebestibleDAL bebDAL = new bebestibleDAL();
            return bebDAL.GetStockBebestible(id);
        }


        public bool UpdateOnlyStockBeb(int id, int cantidad)
        {
            bebestibleDAL bebDAL = new bebestibleDAL();
            return bebDAL.SetOnlyBebestibleStock(id, cantidad);
        }
        public bool UpdateStockAddBeb(int id, int cantidad)
        {
            bebestibleDAL bebDAL = new bebestibleDAL();
            return bebDAL.AgregaBebestibleStock(id, cantidad);
        }

        public DataTable Get_BebestibleByName(string nombre_bebestible)
        {
            bebestibleDAL bd = new bebestibleDAL();
            DataTable dt = bd.Get_bebestibleByName(nombre_bebestible);
            return dt;
        }

        public DataTable Get_allbebest()
        {
            bebestibleDAL bd = new bebestibleDAL();
            DataTable dt = bd.GetallBebest();
            return dt;
        }

        public DataTable Get_bebCritico()
        {
            bebestibleDAL bd = new bebestibleDAL();
            return bd.Get_beb_critico();
        }

        public int Get_bebyid(string nom)
        {
            bebestibleDAL pd = new bebestibleDAL();
            return pd.Get_bebyid(nom);
        }
    }
}
