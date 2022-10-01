using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace InformationProtection
{
    /// <summary>
    /// Логика взаимодействия для ContentWindow.xaml
    /// </summary>
    public partial class ContentWindow : Window
    {
        private ObservableCollection<User> ListUsers { get; set; }
        private User User { get; set; }
        public ContentWindow(User user)
        {
            InitializeComponent();
            ListUsers = new ObservableCollection<User>();
            User = user;
        }

        public ContentWindow(List<User> listUser, User user)
        {
            InitializeComponent();
            User = user;
            ListUsers = new ObservableCollection<User>(listUser);
            ListUsers.Remove(user);
            DataContext = ListUsers;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            UserWindow UserWindow = new UserWindow(new User());
            //UserWindow.Owner = this;
            if (UserWindow.ShowDialog() == true)
            {
                User User = UserWindow.User;
                ListUsers?.Add(User);
            }
        }
        // редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            User? user = usersList.SelectedItem as User;
            // если ни одного объекта не выделено, выходим
            if (user is null) return;
      
            UserWindow UserWindow = new UserWindow(new User
            {
                Login = user.Login,
                Password = ""
            });

            if (UserWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                foreach (var user1 in ListUsers)
                {
                    if(user.Id == user1.Id)
                    {
                        user = user1;
                        break;
                    }
                }
                if (user != null)
                {
                    user.Login = UserWindow.User.Login;
                    user.Password = UserWindow.User.Password;
                }
            }
        }
        // удаление
     //   private void Delete_Click(object sender, RoutedEventArgs e)
     //   {
     //       // получаем выделенный объект
     //       User? user = usersList.SelectedItem as User;
     //       // если ни одного объекта не выделено, выходим
     //       if (user is null) return;
     //       db.Users.Remove(user);
     //       db.SaveChanges();
     //   }

    }
}
