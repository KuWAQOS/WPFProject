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
using WpfApp1.Views.OnPagePages;

namespace WpfApp1.Views
{
    /// <summary>   
    /// Логика взаимодействия для MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        private void InfoBtn_Click(object sender, RoutedEventArgs e)
        {
            MainMenuFrame.Navigate(new InformPage());
        }

        private void StatBtn_Click(object sender, RoutedEventArgs e)
        {
            MainMenuFrame.Navigate(new StatisticPage());
        }

        private void ClnBtn_Click(object sender, RoutedEventArgs e)
        {
            MainMenuFrame.Navigate(new ClientsPage());
        }

        private void SotrBtn_Click(object sender, RoutedEventArgs e)
        {
            MainMenuFrame.Navigate(new SotrPage1());
        }

        private void ProdBtn_Click(object sender, RoutedEventArgs e)
        {
            MainMenuFrame.Navigate(new ProdPage1());
        }

        private void OrdsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainMenuFrame.Navigate(new OrdersPage());
        }
    }
}
