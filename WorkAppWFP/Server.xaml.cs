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
using System.Windows.Shapes;
using static WorkAppWFP.Crud;

namespace WorkAppWFP
{
    /// <summary>
    /// Логика взаимодействия для Server.xaml
    /// </summary>
    public partial class Server : Window
    {
        public Server()
        {
            InitializeComponent();
            stop.IsEnabled = false;
        }


        public class GetInfoServer
        {
            public string usedpercent { get; set; }
            public int maxn { get; set; }
            public int count { get; set; }
            public int N { get; set; }
        }



        private void Button_Refrash(object sender, RoutedEventArgs e)
        {
            string URL = Const.PathServer + "/admin/statusBD";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "GET";
            request.ContentType = "application/json";
            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                           .GetBytes(Const.Login + ":" + Const.Password));

            request.Headers.Add("Authorization", "Basic " + encoded);
            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();
                    GetInfoServer deptObj = System.Text.Json.JsonSerializer.Deserialize<GetInfoServer>(response);
                    count.Content = deptObj.count;
                    maxth.Content = deptObj.maxn;
                    ram.Content = deptObj.usedpercent;
                    th.Content = deptObj.N;


                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }


        private void TextValidationNumber(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[a-zA-Z]+$");
            e.Handled = regex.IsMatch(e.Text);
        }



        private void Button_Start(object sender, RoutedEventArgs e)
        {
            if (textbo.Text == "")
            {
                MessageBox.Show("Введите значение нагрузки RAM", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            


            string URL = Const.PathServer + "/admin/testingbd/1/" + textbo.Text;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                           .GetBytes("admin" + ":" + "admin"));

            request.Headers.Add("Authorization", "Basic " + encoded);
            request.ContentType = "application/json";

     


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                status.Content = "Статус: Запущен";
                start.IsEnabled = false;
                stop.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Произошла ошибка на сервере");
            }
            response.Close();

        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Hide();
        }

        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            Const @const = new Const();
            string URL = Const.PathServer + "/admin/closetesting/0/";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                           .GetBytes(Const.Login + ":" + Const.Password));

            request.Headers.Add("Authorization", "Basic " + encoded);
            request.ContentType = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                status.Content = "Статус: Не запущен";
                start.IsEnabled = true;
                stop.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Произошла ошибка на сервере");
            }
            response.Close();
        }

        private void Button_Logs(object sender, RoutedEventArgs e)
        {
            string filePath = @"D:\GGWP\WorkApp\logs\";
            string argument = "/select, \"" + filePath + "\"";
            System.Diagnostics.Process.Start("explorer.exe", argument);
        }
    }
}
