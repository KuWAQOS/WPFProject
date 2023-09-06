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
using WpfApp1.Commands;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {

            // получение данных
            using (InventoryDbContext db = new InventoryDbContext())
            {
                var currentuser = db.Users.FirstOrDefault(u=>u.Login==LogTB.Text && u.Password==PasTB.Password);

                switch (currentuser.Acess)
                {
                    case 1:
                        NavigationService.Navigate(new MainMenuPage());
                        break;
                        case 2:
                        NavigationService.Navigate(new MainMenuPageManag());
                        break;
                    case 3:
                        NavigationService.Navigate(new MainMenuPageKlad());
                        break;
                    default:
                        MessageBox.Show("Такого пользователя нет в системе");
                        break;
                }
            }

        }

        private void PasTB_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordLabel.Visibility = Visibility.Collapsed;
        }

        private void PasTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PasTB.Password))
            {
                PasswordLabel.Visibility = Visibility.Visible;
            }
        }

    }
}
