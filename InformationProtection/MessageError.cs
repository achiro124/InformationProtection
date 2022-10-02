using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InformationProtection
{
    public class MessageError : INotifyPropertyChanged
    {
        public string? Text { get; private set; }
        public Type TypeError {
            set
            {
                switch (value)
                {
                    case Type.CopyPasswordErr: 
                    {
                            Text = "Пароли не совпадают";
                            break;
                    }
                    case Type.OldPasswordErr:
                    {
                            Text = "Старый пароль не верен";
                            break;
                    }
                    case Type.LoginErr:
                    {
                            Text = "Данного логина не существует";
                            break;
                    }
                    case Type.PasswordErr:
                    {
                            Text = "Неправильный пароль";
                            break;
                    }
                    case Type.OldAndNewErr:
                    {
                           Text = "Старый пароль совпадает с новым";
                           break;
                    }
                    case Type.UserBlockErr:
                    {
                           Text = "Аккаунт заблокирован";
                           break;
                    }
                    case Type.SimilarLoginsErr:
                    {
                        Text = "Аккаунт с таким логином уже существует";
                        break;
                    }
                }
                OnPropertyChanged("Text");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public enum Type
    {
        OldPasswordErr,
        CopyPasswordErr,
        LoginErr,
        PasswordErr,
        OldAndNewErr,
        UserBlockErr,
        SimilarLoginsErr
    }
}
