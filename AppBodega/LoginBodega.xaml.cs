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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using BLL;
using Tulpep.NotificationWindow;

namespace AppBodega
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class LoginBodega : MetroWindow
    {
        public string username = null;

        public LoginBodega()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {

            usuarioBLL usrbll = new usuarioBLL();
            bool check = usrbll.getLogin(txtUsuario.Text, pbContrasena.Password);
            if (check)
            {
                int rut = usrbll.Getrut(txtUsuario.Text, pbContrasena.Password);
                string nombre = usrbll.Get_nombrecompleto(rut);

                PopupNotifier popup = new PopupNotifier();
                popup.TitleText = "Aviso";
                popup.Image = Properties.Resources.add;
                popup.ContentText = "Bienvenido" + nombre;
                popup.AnimationDuration = 500;
                popup.Delay = 3500;
                popup.Popup();
                Bodega adm = new Bodega();
                adm.lblUser.Content = nombre;
                Close();
                adm.ShowDialog();


            }
            else
            {
                MessageBox.Show("Credenciales o Rol Incorrectos \n Intente nuevamente");

            }
        }
    }          
}

