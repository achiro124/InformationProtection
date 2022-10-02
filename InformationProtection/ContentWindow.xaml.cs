﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private string path = "E:\\C#\\InformationProtection\\InformationProtection\\baseDate.txt";
        public ContentWindow(List<User> listUser, User user)
        {
            InitializeComponent();
            User = user;
            ListUsers = new ObservableCollection<User>(listUser);
            ListUsers.Remove(user);

            if(User.Role == Role.User)
                UserTabControl.Items.RemoveAt(1);
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            UserWindow UserWindow = new UserWindow(new User(), ListUsers);
            UserWindow.Owner = this;
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
            }, ListUsers);
            UserWindow.Owner = this;

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

        private void DataContext_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl? tabControl = sender as TabControl;
            if(tabControl?.SelectedIndex == 1)
            {
                DataContext = ListUsers;
               
            }
            else
            {
                DataContext = User;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            int enable = User.Enable ? 1 : 0;
            File.WriteAllText(path, $"{User.Login}/{User.Password}/{enable}\n");
            foreach (var user in ListUsers)
            {
                File.AppendAllText(path, $"{user.Login}/{user.Password}/{enable}\n");
            }

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }


        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(User);
            changePasswordWindow.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            int enable = User.Enable ? 1 : 0;
            File.WriteAllText(path, $"{User.Login}/{User.Password}/{enable}\n");
            foreach (var user in ListUsers)
            {
                File.AppendAllText(path,$"{user.Login}/{user.Password}/{enable}\n");
            }
        }
    }
}
