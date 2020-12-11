using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace AppBodega
{
    /// <summary>
    /// Lógica de interacción para ActualizarStock.xaml
    /// </summary>
    public partial class Bodega : MetroWindow
    {
        public string  username = null;
 
        public Bodega()
        {
            InitializeComponent();


        }
        private void lblUser_Loaded(object sender, RoutedEventArgs e)
        {
            lblUser.Content = username;
        }

      

        private void btnListado_Click(object sender, RoutedEventArgs e)
        {
            ListadodeInsumos listIns = new ListadodeInsumos();
            Close();
            listIns.ShowDialog();
        }

        private void btnEnvio_Click(object sender, RoutedEventArgs e)
        {
            AbastecimientodeCocina abcoc = new AbastecimientodeCocina();
            Close();
            abcoc.ShowDialog();
        }
       
        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            LoginBodega lb = new LoginBodega();
            Close();
            lb.ShowDialog();
        }

      
        

        private void Btn_act_ins_Click(object sender, RoutedEventArgs e)
        {
            ActualizacionStockInsumo ai = new ActualizacionStockInsumo();
            ai.Owner = this;
            ai.ShowDialog();
        }
    }
}
