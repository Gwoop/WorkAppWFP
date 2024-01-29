using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
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
using static System.Net.WebRequestMethods;

namespace WorkAppWFP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Setting setting = new Setting();
            setting.Get_Ip();
        }

        public class Answer
        {
            public bool answerclass { get; set; }
        }

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            
            
            string URL = Const.PathServer + "/admin/authorization";
            Const.Login = login.Text;
            Const.Password = password.Password;

            
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = "GET";
                request.ContentType = "application/json";
            
            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                           .GetBytes(Const.Login + ":" + Const.Password));

            request.Headers.Add("Authorization", "Basic " + encoded);
           
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Menu menu = new Menu();
                    menu.Show();
                }
                response.Close();
                this.Close();
            }
            catch {
                MessageBox.Show("Нету подключения к серверу, проверьте данные", "Error",MessageBoxButton.OK);
            }
            
            
        }

        private void TextValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^\w\.@-]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Setting(object sender, RoutedEventArgs e)
        {
            Setting setting = new Setting();
            setting.Show();
        }
    }
}
