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

namespace WpfApp1.Views.OnPagePages
{
    /// <summary>
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        InventoryDbContext db;
        public ClientsPage()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            db = new InventoryDbContext();

            var clients = db.Clients
                .Where(c => c.Id > 0)
                .GroupJoin(db.Orders1,
                    c => c.Id,
                    o => o.Clientid,
                    (c, o) => new { Client = c, Orders = o })
                .Select(x => new
                {
                    IdClient = x.Client.Id,
                    NameClient = x.Client.Name,
                    CountOrders = x.Orders.Count()+9,
                    Ransom = x.Client.Ransom,
                    Discount = x.Client.Discount
                })
                .OrderByDescending(x => x.CountOrders)
                .ToList();

            this.ClientsGrid.ItemsSource = clients;
            this.ClientsGrid.Visibility=Visibility.Visible;
        }
    }
}
