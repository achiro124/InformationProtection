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
    public partial class MainWindow : Window
    {
        private string path = "E:\\C#\\InformationProtection\\InformationProtection\\baseDate.txt";
        private string copyPath = "E:\\C#\\InformationProtection\\InformationProtection\\baseDateCopy.txt";
        private string key = string.Empty;
        List<User> listUsers = new List<User>();
        MessageError msgErr = new MessageError();
        int k = 0;
        public MainWindow(string key)
        {
            InitializeComponent();
            DataContext = msgErr;
            this.key = key;
        }


        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            foreach(var user in listUsers)
            {
                if(user.Login == txtBoxLogin.Text)
                {
                    if (user.Password == passwordBox.Password && user.Enable)
                    {
                        if (user.Password == "" || user.Criterion && !User.CheckParametrsCriterion(user.Password))
                        {
                            if(user.Criterion && !User.CheckParametrsCriterion(user.Password))
                            {
                                ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(user);
                                changePasswordWindow.ShowDialog();
                            }
                            else
                            {
                                PasswordRegistrationWindow passwordRegistration = new PasswordRegistrationWindow(user);
                                passwordRegistration.Owner = this;
                                passwordRegistration.ShowDialog();
                            }
                            return;
                        }
                        else
                        {
                            ContentWindow contentWindow = new ContentWindow(listUsers, user,key);
                            contentWindow.Show();
                            this.Close();
                            break;
                        }
                    }
                    else
                    {
                        if (!user.Enable) 
                        {
                            msgErr.TypeError = Type.UserBlockErr;
                            break;
                        }
                        else
                        {
                            if (k == 3)
                                this.Close();
                            k++;
                            msgErr.TypeError = Type.PasswordErr;
                            break;
                        }
                    }
                }
                else
                {
                    msgErr.TypeError = Type.LoginErr;
                }
            }
            //txtBoxError.DataContext = msgErr;
            txtBoxError.Visibility = Visibility.Visible;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FileInfo baseDateCopy = new FileInfo(copyPath);
            if (baseDateCopy.Exists)
            {
                string[] lines = File.ReadAllLines(copyPath);
                foreach(var line in lines)
                {
                    string[] fields = line.Split(' ');
                    listUsers.Add(new User
                    {
                        Login = fields[0],
                        Password = fields[1],
                        Role = fields[0].Equals("ADMIN") ? Role.Admin : Role.User,
                        Enable = fields[2] == "1",
                        Criterion = fields[3] == "1"

                    });
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            File.Delete(copyPath);
        }
    }
}
