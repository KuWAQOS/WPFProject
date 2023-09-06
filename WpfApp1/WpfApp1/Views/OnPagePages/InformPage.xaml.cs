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
using Newtonsoft.Json;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для InformPage.xaml
    /// </summary>
    public partial class InformPage : Page
    {
        InventoryDbContext _dbContainer;
        public InformPage()
        {
            InitializeComponent();
        }

        private void DateBtn_Click(object sender, RoutedEventArgs e)
        {
            var addButton = sender as FrameworkElement;
            if (addButton != null)
            {
                addButton.ContextMenu.IsOpen = true;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _dbContainer = new InventoryDbContext();
            var users = _dbContainer.Users
                .Where(u => u.Id > 0)
                .ToList()
                .Select(u => new {
                    Name = u.Name,
                    Post = JsonConvert.DeserializeObject<dynamic>(u.Documents).Post,
                    Access = u.Acess
                })
                .ToList();

            // Привязка к DataGrid
            UsersDataGrid.ItemsSource = users;

            this.dateTB.Text = "Информация по предприятию\n" + DateTime.Now.ToString();

            int sum = (int)_dbContainer.Producttypes
                .Where(_onwar => _onwar.Id > 0)
                .Sum(_onwar => _onwar.Count);
            this.OnWare.Text=sum.ToString();

            int sumzakaz = (int)_dbContainer.Orders1
                .Count(x=>x.Id>0);
            this.Pending.Text = sumzakaz.ToString();

            int sort = (int)_dbContainer.Orders
                .Where(x => x.Id > 0)
                .Sum(x => x.Productcount);
            this.Sorted.Text =sort.ToString();

            int sold = (int)_dbContainer.Soldproducts
                .Count(x => x.Id>0);
            this.ReadyToGo.Text = sold.ToString();


            this.Lost.Text = "0";
        }

        private void MenuItemDay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemWeek_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemMonth_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemQuarter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }
    }
}
