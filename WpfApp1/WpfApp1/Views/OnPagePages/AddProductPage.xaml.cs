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
    /// Логика взаимодействия для AddProductPage.xaml
    /// </summary>
    public partial class AddProductPage : Page
    {
        Product newProd;
        InventoryDbContext db;
        public AddProductPage()
        {
            InitializeComponent();
            db = new InventoryDbContext();

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            data_check();
        }

        private void data_check()
        {
            StringBuilder errors = new StringBuilder();
            if (pType.SelectedItem == null)
            {
                errors.AppendLine("Тип не указан");
            }
            if (string.IsNullOrEmpty(pNameProduct.Text))
                errors.AppendLine("Наименование не заполнено");
            if (string.IsNullOrEmpty(pPrice.Text))
                errors.AppendLine("Цена не заполнена");
            if (string.IsNullOrEmpty(pDescription.Text))
                errors.AppendLine("Описание не заполнено");
            if (string.IsNullOrEmpty(pCount.Text))
                errors.AppendLine("Количество не заполнено");
            if (string.IsNullOrEmpty(pProducer.Text))
                errors.AppendLine("Производитель не заполнен");
            if (string.IsNullOrEmpty(pModel.Text))
                errors.AppendLine("Модель не заполнена");


            if (errors.Length > 0)
            {
                if (MessageBox.Show(errors.ToString(), "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Error) == MessageBoxResult.Cancel)
                    return;
            }
            else
            {
                int price = int.Parse(pPrice.Text);
                int count = int.Parse(pCount.Text);
                newProd = new Product
                {
                    Nameproduct = pNameProduct.Text,
                    Producttypeid = 3,
                    Producername = pProducer.Text,
                    Model = pModel.Text,
                    Price = price,
                    Description = pDescription.Text,
                    Countofprod = count
                };

                db.Products.Add(newProd);
                db.SaveChanges();
                NavigationService.GoBack();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> type = new List<string>
            {
                "1",
                "2",
                "3"
            };
            // Привязка коллекции к ComboBox
            pType.ItemsSource = type;
        }
    }
}
