using Newtonsoft.Json;
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
    /// Логика взаимодействия для UsersAdPage.xaml
    /// </summary>
    public partial class UsersAdPage : Page
    {
        InventoryDbContext db;
        public UsersAdPage()
        {
            InitializeComponent();
            db = new InventoryDbContext();
        }

        private void data_check()
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(uName.Text))
                errors.AppendLine("ФИО не заполнено");
            if (string.IsNullOrEmpty(uPassport.Text))
                errors.AppendLine("Пасспорт не заполнен");
            if (string.IsNullOrEmpty(uAddress.Text))
                errors.AppendLine("Адрес не заполнен");
            if (uPayment.SelectedItem == null)
            {
                errors.AppendLine("Почасовая ставка не указана");
            }
            if (uPost.SelectedItem == null)
            {
                errors.AppendLine("Должность не указана");
            }
            if (uAccess.SelectedItem == null)
            {
                errors.AppendLine("Доступ не указан");
            }
            if (string.IsNullOrEmpty(uLogin.Text))
                errors.AppendLine("Логин не заполнен");
            if (string.IsNullOrEmpty(uPassword.Text))
                errors.AppendLine("Пароль заполнен");

            if (errors.Length>0)
            {
                if (MessageBox.Show( errors.ToString(),"Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Error) == MessageBoxResult.Cancel)
                    return;
            }
            else 
            {
                int access = int.Parse(uAccess.Text);
                DateTime date = DateTime.Now;
                //string json = JsonConvert.SerializeObject(new { DateOfReceipt = date.ToString("dd.MM.yyyy") });

                var data = new
                {
                    DateOfReceipt = date.ToString("dd.MM.yyyy"),
                    HPU = uPayment.Text,
                    Passport = uPassport.Text,
                    Password = uPassword.Text,
                    Post = uPost.Text
                };

                string json=JsonConvert.SerializeObject(data);

                User newUser = new User
                {
                    Name = uName.Text,
                    Acess = access,
                    Login = uLogin.Text,
                    Address = uAddress.Text,
                    Password = uPassword.Text,
                    Documents = json
                };

                db.Users.Add(newUser);
                db.SaveChanges();
           }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            data_check();
            NavigationService.GoBack();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> payment = new List<string>
            {
                "110",
                "120",
                "135",
                "155"
            };
            // Привязка коллекции к ComboBox
            uPayment.ItemsSource = payment;
            List<string> post = new List<string>
            {
                "Кладовщик",
                "Упаковщик",
                "Менеджер"
            };
            // Привязка коллекции к ComboBox
            uPost.ItemsSource = post;
            List<string> access = new List<string>
            {
                "1",
                "2",
                "3"
            };
            // Привязка коллекции к ComboBox
            uAccess.ItemsSource = access;
        }
    }
}
