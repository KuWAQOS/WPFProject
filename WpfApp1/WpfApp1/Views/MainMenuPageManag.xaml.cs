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
    /// Логика взаимодействия для MainMenuPageManag.xaml
    /// </summary>
    public partial class MainMenuPageManag : Page
    {
        public MainMenuPageManag()
        {
            InitializeComponent();

        }

        private void InfoBtn_Click(object sender, RoutedEventArgs e)
        {
            MainMenuFrame.Navigate(new InformPage());
        }

        private void SotrBtn_Click(object sender, RoutedEventArgs e)
        {
            MainMenuFrame.Navigate(new SotrPage1());
        }

        private void ProdBtn_Click(object sender, RoutedEventArgs e)
        {
            MainMenuFrame.Navigate(new ProdPage1());
        }
    }
}
