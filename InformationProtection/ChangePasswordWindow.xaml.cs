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
    /// Логика взаимодействия для ChangePasswordWindow.xaml
    /// </summary>
    ///
    public partial class ChangePasswordWindow : Window
    {
        private User User { get; set; }
        private MessageError msgErr = new();
        public ChangePasswordWindow(User user)
        {
            InitializeComponent();
            User = user;
            DataContext = msgErr;
        }

        public void Accept_Click(object sender, EventArgs e)
        {
            if(User.Password == oldPasswordBox.Password)
            {
                if(newPasswordBox.Password == copyPasswordBox.Password && newPasswordBox.Password != "")
                {
                    if(newPasswordBox.Password != oldPasswordBox.Password && !User.Criterion) 
                    {
                        User.Password = newPasswordBox.Password;
                        DialogResult = true;
                    }
                    else
                    {
                        msgErr.TypeError = Type.OldAndNewErr;
                        if (User.Criterion)
                        {
                            if (User.CheckParametrsCriterion(newPasswordBox.Password) && newPasswordBox.Password != oldPasswordBox.Password)
                            {
                                User.Password = newPasswordBox.Password;
                                DialogResult = true;
                            }
                            else
                            {
                                msgErr.TypeError = newPasswordBox.Password == oldPasswordBox.Password ? Type.OldAndNewErr : Type.CriterionErr;
                            }
                        }
                    }
                }
                else
                {
                    msgErr.TypeError = Type.CopyPasswordErr;
                }
            }
            else
            {
                msgErr.TypeError = Type.OldPasswordErr;
            }
        }
    }
}
