﻿using System;
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
        List<User> listUsers = new List<User>();
        MessageError msgErr = new MessageError();
        int k = 0;
        public MainWindow()
        {
            InitializeComponent();     
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
                            PasswordRegistrationWindow passwordRegistration = new PasswordRegistrationWindow(user);
                            passwordRegistration.Owner = this;
                            passwordRegistration.ShowDialog();
                            return;
                        }
                        else
                        {
                            ContentWindow contentWindow;
                            contentWindow = new ContentWindow(listUsers, user);
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
            txtBoxError.DataContext = msgErr;
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
                        Role = fields[0].Equals("ADMIN") ? Role.Admin : Role.User,
                        Enable = fields[2] == "1",
                        Criterion = fields[3] == "1"

                    });
                }
            }
            else
            {
                await File.AppendAllLinesAsync(path,new string[] { "ADMIN//1/0" });
                listUsers.Add(new User
                {
                    Login = "ADMIN",
                    Password = "",
                    Role = Role.Admin,
                    Enable = true
                });
            }
        }
    }
}
