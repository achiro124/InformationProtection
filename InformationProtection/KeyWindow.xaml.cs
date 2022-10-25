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
using System.Windows.Shapes;

namespace InformationProtection
{
    /// <summary>
    /// Логика взаимодействия для KeyWindow.xaml
    /// </summary>
    public partial class KeyWindow : Window
    {
        private string copyPath = "E:\\C#\\InformationProtection\\InformationProtection\\baseDateCopy.txt";
        private string path = "E:\\C#\\InformationProtection\\InformationProtection\\baseDate.txt";
        private MessageError msgErr = new MessageError();
        public string Key { get; private set; } = string.Empty;
        public KeyWindow()
        {
            InitializeComponent();
            txtBlock.DataContext = msgErr;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            FileInfo baseDate = new FileInfo(path);
            Key = txtBoxKey.Password;
            if (baseDate.Exists)
            {
                string fileContent = string.Empty;
                try
                {
                    fileContent = Crypt.ReadFile(path, Key);
                }
                catch
                {
                    msgErr.TypeError = Type.KeyErr;
                }
                if (fileContent.Contains("ADMIN"))
                {
                    File.AppendAllText(copyPath, fileContent);
                    MainWindow mainWindow = new MainWindow(Key);
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    msgErr.TypeError = Type.KeyErr;
                }

            }
            else
            {
                await File.AppendAllLinesAsync(copyPath, new string[] { "ADMIN  1 0" });
                MainWindow mainWindow = new MainWindow(Key);
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
