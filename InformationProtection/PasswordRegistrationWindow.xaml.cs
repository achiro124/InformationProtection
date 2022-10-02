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
using System.Windows.Shapes;

namespace InformationProtection
{
    /// <summary>
    /// Логика взаимодействия для PasswordRegistrationWindow.xaml
    /// </summary>
    public partial class PasswordRegistrationWindow : Window
    {
        public User User { get; set; }
        MessageError msErr = new MessageError();
        public PasswordRegistrationWindow(User user)
        {
            InitializeComponent();
            User = user;
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if(passwordBox.Password == copyPasswordBox.Password && passwordBox.Password != "")
            {
                User.Password = passwordBox.Password;
                this.DialogResult = true;
            }
            else
            {
                txtBoxError.DataContext = msErr;
                msErr.TypeError = Type.CopyPasswordErr;
                txtBoxError.Visibility = Visibility;
            }
        }
    }
}
