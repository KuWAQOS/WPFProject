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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        InventoryDbContext db;
        public OrdersPage()
        {
            InitializeComponent();
            db = new InventoryDbContext();
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = (dynamic)OrdersGrid.SelectedItem;
            if (MessageBox.Show("Заказ сформирован?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    int orderId = selectedOrder.OrderId;

                    // Удаление записи из таблицы Orders
                    var order = db.Orders1.FirstOrDefault(o => o.Id == orderId);
                    if (order != null)
                    {
                        db.Orders1.Remove(order);
                        db.SaveChanges();
                    }

                    // Удаление связанной записи из таблицы Orders1
                    var order1 = db.Orders.FirstOrDefault(o1 => o1.Orderid == orderId);
                    if (order1 != null)
                    {
                        db.Orders.Remove(order1);
                        db.SaveChanges();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при попытке сохранения: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            Load();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }

        private void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load() 
        {
            var orderList = db.Orders
                .Where(o => o.Id > 0)
                .Join(db.Orders1,
                    o => o.Orderid,
                    o1 => o1.Id,
                    (o, o1) => new { Order = o, Order1 = o1 })
                .Join(db.Clients,
                    joined => joined.Order1.Clientid,
                    client => client.Id,
                    (joined, client) => new
                    {
                        OrderId = joined.Order.Id,
                        ClientName = client.Name,
                        ProdId = joined.Order.Productid,
                        ProdCount = joined.Order.Productcount,
                        DateOfCreate = joined.Order1.Dateofcreate,
                        OrderSum = joined.Order1.Sum
                    })
                .OrderByDescending(x => x.OrderId)
                .ToList();
            OrdersGrid.ItemsSource = orderList;
        }

        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = (dynamic)OrdersGrid.SelectedItem;
            if (MessageBox.Show("Заказ сформирован?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    int orderId = selectedOrder.OrderId;

                    // Удаление записи из таблицы Orders
                    var order = db.Orders1.FirstOrDefault(o => o.Id == orderId);
                    if (order != null)
                    {
                        db.Orders1.Remove(order);
                        db.SaveChanges();
                    }

                    // Удаление связанной записи из таблицы Orders1
                    var order1 = db.Orders.FirstOrDefault(o1 => o1.Orderid == orderId);
                    if (order1 != null)
                    {
                        db.Orders.Remove(order1);
                        db.SaveChanges();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при попытке сохранения: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            Load();
        }
    }
}
