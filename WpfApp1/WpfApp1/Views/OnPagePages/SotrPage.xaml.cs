using System;
using System.Collections.Generic;
using System.Data;
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
using WpfApp1.ViewModel;

namespace WpfApp1.Views.OnPagePages
{
    /// <summary>
    /// Логика взаимодействия для SotrPage1.xaml
    /// </summary>
    public partial class SotrPage1 : Page
    {
        InventoryDbContext db;
        public SotrPage1()
        {
            InitializeComponent();
        }

        private void Load() 
        {
            db = new InventoryDbContext();
            var users = db.Users
                .Where(x => x.Id > 1)
                .ToList()
                .Select(x => new UserViewModel
                {
                    UserId = x.Id,
                    UserName = x.Name,
                    UserAcess = x.Acess,
                    UserLogin = "*****",
                    UserPas = "*****",
                    UserPost = x.Documents != null ?
                                JsonConvert.DeserializeObject<dynamic>(x.Documents)?.Post :
                                null,
                    DateOfReceipt = x.Documents != null ?
                                JsonConvert.DeserializeObject<dynamic>(x.Documents)?.DateOfReceipt :
                                null
                })
                .ToList();
            UsersGrid.ItemsSource = users;

        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new UsersAdPage());
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = (dynamic)UsersGrid.SelectedItem;
            if (MessageBox.Show($"Вы точно хотите удалить пользователя {selectedUser.UserName}?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    int userId = selectedUser.UserId;
                    var user = db.Users.FirstOrDefault(u => u.Id == userId);
                    if (user != null)
                    {
                        db.Users.Remove(user);
                        db.SaveChanges();
                        Load();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при удалении пользователя: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = UsersGrid.SelectedItem as UserViewModel;
            if (selectedUser != null)
            {
                int userId = selectedUser.UserId;
                var editUserPage = new EditUserPage(userId);
                NavigationService.Navigate(editUserPage);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
               Load();
        }
    }
}
