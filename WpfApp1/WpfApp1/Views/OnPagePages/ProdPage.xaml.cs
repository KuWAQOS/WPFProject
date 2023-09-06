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
    /// Логика взаимодействия для ProdPage1.xaml
    /// </summary>
    public partial class ProdPage1 : Page
    {
        InventoryDbContext db;
        public ProdPage1()
        {
            InitializeComponent();
            db = new InventoryDbContext();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProductPage());
        }

        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProd = (dynamic)ProductsGrid.SelectedItem;
            if (MessageBox.Show($"Вы точно хотите удалить позицию {selectedProd.ProdDesc}?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    int prodId = selectedProd.ProdId;
                    var prod = db.Products.FirstOrDefault(p => p.Id == prodId);
                    if (prod != null)
                    {
                        db.Products.Remove(prod);
                        db.SaveChanges();
                        Load();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при удалении позиции: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Load() 
        {
            var product = db.Products
                .Where(x => x.Id > 0)
                .ToList()
                .Select(x => new
                {
                    ProdId = x.Id,
                    ProdName = x.Nameproduct,
                    ProdProducer = x.Producername,
                    ProdMod = x.Model,
                    ProdPrice = x.Price,
                    ProdDesc = x.Description,
                    ProdOnWare = x.Countofprod
                }).ToList()
                .OrderBy(x => x.ProdId);

            ProductsGrid.ItemsSource = product;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Content=null;
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void EditProductButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditProductPage());
        }
    }
}
