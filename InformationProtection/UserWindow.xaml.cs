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
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public User User { get; private set; }
        private ObservableCollection<User> ListUsers { get; set; }
        private MessageError msgErr = new MessageError();
        public UserWindow(User user, ObservableCollection<User> ListUsers, bool type)
        {
            InitializeComponent();
            User = user;
            this.ListUsers = ListUsers;
            DataContext = User;
            txtBoxLogin.IsEnabled = type;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            foreach(var user in ListUsers)
            {
                if (user.Login == txtBoxLogin.Text && txtBoxLogin.IsEnabled)
                {
                    msgErr.TypeError = Type.SimilarLoginsErr;
                    txtBoxError.DataContext = msgErr;
                    return;
                }
                    
            }
            DialogResult = true;
        }
    }
}
