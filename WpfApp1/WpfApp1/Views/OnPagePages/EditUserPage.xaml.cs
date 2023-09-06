using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.ViewModel;
using Newtonsoft.Json ;
using System.Text;

namespace WpfApp1.Views.OnPagePages
{
    public partial class EditUserPage : Page
    {
        InventoryDbContext db;
        private int UserId;
        private string dateOfRecepit;

        public EditUserPage(int id)
        {
            InitializeComponent();
            this.UserId = id;
            db = new InventoryDbContext();
            LoadData();
        }

        private void LoadData()
        {

            var selectedUser = db.Users.FirstOrDefault(u => u.Id == UserId);

            if (selectedUser != null)
            {
                string payment = JsonConvert.DeserializeObject<dynamic>(selectedUser.Documents).HPU;
                string passport = JsonConvert.DeserializeObject<dynamic>(selectedUser.Documents).Passport;
                string post = JsonConvert.DeserializeObject<dynamic>(selectedUser.Documents).Post;

                uName.Text = selectedUser.Name;
                uPassp.Text = passport;
                uAddress.Text = selectedUser.Address;
                uPayment.SelectedValue = payment;
                uPost.SelectedValue = post;

                uAccess.SelectedValue = selectedUser.Acess.ToString();
                uLogin.Text = selectedUser.Login;
                uPassword.Text = selectedUser.Password;
                dateOfRecepit = selectedUser.Documents != null ?
                                JsonConvert.DeserializeObject<dynamic>(selectedUser.Documents)?.DateOfReceipt :
                                null;
            }
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Сохранение изменений
            data_check();

            NavigationService.GoBack();
        }

        private void data_check()
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(uName.Text))
                errors.AppendLine("ФИО не заполнено");
            if (string.IsNullOrEmpty(uPassp.Text))
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

            if (errors.Length > 0)
            {
                if (MessageBox.Show(errors.ToString(), "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Error) == MessageBoxResult.Cancel)
                    return;
            }
            else
            {
                int access = int.Parse(uAccess.Text);

                var data = new
                {
                    Passport = uPassp.Text,
                    Password = uPassword.Text,
                    Key = "123",
                    Post = uPost.Text,
                    HPU = uPayment.Text,
                    OPu = "249",
                    Payment ="120",
                    DateOfReceipt = dateOfRecepit
                    
                };

                string json = JsonConvert.SerializeObject(data);

                var selectedUser = db.Users.FirstOrDefault(u => u.Id == UserId);

                if (selectedUser != null) 
                {
                    selectedUser.Name = uName.Text;
                    selectedUser.Acess = access;
                    selectedUser.Login = uLogin.Text;
                    selectedUser.Address = uAddress.Text;
                    selectedUser.Password = uPassword.Text;
                    selectedUser.Documents = json;
                }

                db.SaveChanges();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
