using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace InformationProtection
{
    public class User : INotifyPropertyChanged
    {
        private bool enable = true;

        private bool criterion = false;
        public string? Login { get; set; }
        public string? Password { get; set; }
        public Role Role { get; set; }
        public bool Enable 
        {
            get => enable; 
            set
            {
                enable = value;
                OnPropertyChanged("enable");
            }
        }
        public bool Criterion
        {
            get => criterion;
            set
            {
                criterion = value;
                OnPropertyChanged("criterion");
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public static bool CheckParametrsCriterion(string password)
        {
            if (password != null)
            {
                string pattern = @"[0-9]+[0-9]+[\.\,\:\;\?]+[\.\,\:\;\?]+[0-9]+[0-9]+";
                return Regex.IsMatch(password, pattern);
            }
            else
                return false;
        }
    }
    public enum Role
    {
        Admin,
        User
    }


}
