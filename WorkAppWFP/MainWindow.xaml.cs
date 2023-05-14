using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
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
        }

        public class Answer
        {
            public bool answerclass { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {   
            string URL = Const.PathServer + "/admin/authorization";


            string username = login.Text;
            string passwordstring = password.Password;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";
            request.ContentType = "application/json";
            

            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                           .GetBytes(username + ":" + passwordstring));

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
                MessageBox.Show("Неверный Логин или пароль");
            }
            
            
        }
    }
}
