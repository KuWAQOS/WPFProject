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
using NpgsqlTypes;
using Newtonsoft.Json;
using System.Windows.Threading;

namespace WpfApp1.Views.OnPagePages
{
    /// <summary>
    /// Логика взаимодействия для StatisticPage.xaml
    /// </summary>
    public partial class StatisticPage : Page
    {
        InventoryDbContext inventoryDbContext;
        DateTime selectedMonth;
        public StatisticPage()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }

        private void ProductionButton_Click(object sender, RoutedEventArgs e)
        {

            InventoryDbContext inventoryDbContext = new InventoryDbContext();

            var usersprod = inventoryDbContext.Users
                .Where(x => x.Id > 0)
                .ToList()
                .Select(x => new
                {
                    IdUser = x.Id,
                    NameUser = x.Name,
                    PostUser = JsonConvert.DeserializeObject<dynamic>(x.Documents).Post,
                    HoursProdUser = (int?)JsonConvert.DeserializeObject<dynamic>(x.Documents).HPU,
                    OrderProdUser = (int?)JsonConvert.DeserializeObject<dynamic>(x.Documents).OPu,
                    PaymentUser = (int?)JsonConvert.DeserializeObject<dynamic>(x.Documents).Payment,
                    IncomeUser = (int?)JsonConvert.DeserializeObject<dynamic>(x.Documents).HPU *
                    (int?)JsonConvert.DeserializeObject<dynamic>(x.Documents).OPu
                }).ToList();

            Production_grid.ItemsSource = usersprod;
            this.Production_grid.Visibility = Visibility.Visible;
        }

        private void SoldBorder_Click(object sender, RoutedEventArgs e)
        {
          
            inventoryDbContext = new InventoryDbContext();

            var solds = inventoryDbContext.Products
                .Where(p => p.Id > 0)
                .GroupJoin(inventoryDbContext.Orders,
                    p => p.Id,
                    o => o.Productid,
                    (p, o) => new { Product = p, Orders = o })
                .Select(x => new
                {
                    Id = x.Product.Id,
                    Name = x.Product.Nameproduct,
                    Producer = x.Product.Producername,
                    Onware = x.Product.Countofprod,
                    Ordered = x.Orders.Sum(o => o.Productcount),
                    Revenue = x.Product.Price * x.Orders.Sum(o => o.Productcount)
                })
                .OrderByDescending(x => x.Ordered)
                .ToList();

            Sold_grid.ItemsSource = solds;

            this.Sold_grid.Visibility = Visibility.Visible;
        }

        private void MonthButton_Click(object sender, RoutedEventArgs e)
        {
            inventoryDbContext = new InventoryDbContext();

            
            this.Month_grid.Visibility = Visibility.Visible;
            this.MonthPicker.Visibility = Visibility.Visible;
        }

        private void MonthPicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            DateTime selectedDate = MonthPicker.SelectedDate ?? DateTime.MinValue;

            SoldShow(selectedDate);
        }

        private void SoldShow(DateTime selectedmonth) 
        {

            var selectedMonth = selectedmonth;

            var lastMonthSales = inventoryDbContext.Soldproducts
                .Where(s => s.Dateofsold.Month == selectedMonth.Month && s.Dateofsold.Year == selectedMonth.Year)
                .ToList()
                .GroupBy(s => new { Month = s.Dateofsold.ToString("yyyy-MM"), s.Productid })
                .Join(inventoryDbContext.Products,
                    s => s.Key.Productid,
                    p => p.Id,
                    (s, p) => new
                    {
                        s.Key.Productid,
                        SalesCount = s.Count(),
                        Month = s.Key.Month,
                        p.Countofprod
                    })
                .ToList();

            var salesSummary = inventoryDbContext.Soldproducts
                 .GroupBy(s => s.Productid)
                 .Select(g => new
                 {
                     ProductId = g.Key,
                     SalesCount = g.Count(),
                     MonthCount = g.Select(s => s.Dateofsold.Month).Distinct().Count(),
                     AverageSalesPerMonth = g.Count() / (double)g.Select(s => s.Dateofsold.Month).Distinct().Count()
                 })
                 .ToList();

            var result = lastMonthSales.Select(s => new
            {
                IdProd = s.Productid,
                Count = s.SalesCount,
                Month = s.Month,
                OnWare = s.Countofprod,
                AvgMonth = salesSummary.First(summary => summary.ProductId == s.Productid).AverageSalesPerMonth
            });


            Month_grid.ItemsSource = result;
        }
    }
}
