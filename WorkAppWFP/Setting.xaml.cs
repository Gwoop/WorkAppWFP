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

namespace WorkAppWFP
{
    /// <summary>
    /// Логика взаимодействия для Setting.xaml
    /// </summary>
    public partial class Setting : Window
    {
        public Setting()
        {
            InitializeComponent();
            Get_Ip();

            Path.Text = Const.PathServer;
        }

        private void Button_Setting_Save(object sender, RoutedEventArgs e)
        {
            Const @const = new Const();
            Const.PathServer = Path.Text;
            Return_Ip();
            MessageBox.Show("Данные сохранены","Успешно",MessageBoxButton.OK);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Return_Ip()
        {
            string ip = Const.PathServer;
            string path = "config/ipconfig.txt";
            // полная перезапись файла 
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                await writer.WriteLineAsync(ip);
            }
        }
        public void Get_Ip()
        {
            string path = "config/ipconfig.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string text = reader.ReadToEnd();

                text = text.Replace("\r\n", string.Empty);
                Const.PathServer = text;
            }
        }
    }
}
