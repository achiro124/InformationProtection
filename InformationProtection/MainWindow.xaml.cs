using System;
using System.Collections.Generic;
using System.IO;
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

namespace InformationProtection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///


    public partial class MainWindow : Window
    {
        private string path = "E:\\C#\\InformationProtection\\InformationProtection\\baseDate.txt";
        List<User> listUsers = new List<User>();
        public MainWindow()
        {
            InitializeComponent();
            
        }


        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            foreach(var user in listUsers)
            {
                if(user.Login == txtBoxLogin.Text && user.Password == passwordBox.Password)
                {
                    if(user.Password == "")
                    {
                        PasswordRegistrationWindow passwordRegistration = new PasswordRegistrationWindow(user);
                        passwordRegistration.Owner = this;
                        passwordRegistration.ShowDialog();
                        return;
                    }
                    else
                    {
                        ContentWindow contentWindow = new ContentWindow();
                        contentWindow.Show();
                        this.Close();
                        break;
                    }

                }
            }
            txtBoxError.Visibility = Visibility.Visible;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FileInfo baseDate = new FileInfo(path);
            if (baseDate.Exists)
            {
                string[] lines = File.ReadAllLines(path);
                foreach(var line in lines)
                {
                    string[] fields = line.Split('/');
                    listUsers.Add(new User
                    {
                        Login = fields[0],
                        Password = fields[1],
                        Role = fields[0].Equals("ADMIN") ? Role.Admin : Role.User
                        
                    });
                }
            }
            else
            {
                await File.AppendAllLinesAsync(path,new string[] { "ADMIN/" });
                listUsers.Add(new User
                {
                    Login = "ADMIN",
                    Password = "",
                    Role = Role.Admin
                });
            }
        }
    }
}
