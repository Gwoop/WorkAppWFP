using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using static WorkAppWFP.Crud;

namespace WorkAppWFP
{
    /// <summary>
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History : Window
    {
        public History()
        {
            InitializeComponent();
            GetDataPatern();
        }

        public class Log
        {
            public long id_log { get; set; }
            public string handler { get; set; }
            public string body { get; set; }
            public string header { get; set; }
            public string create_date { get; set; }
        }

        private void GetDataPatern()
        {
            string URL = Const.PathServer + "/admin/getlog";
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
                    var deptList = System.Text.Json.JsonSerializer.Deserialize<IList<Log>>(response);
                    foreach (var dept in deptList)
                    {
                        Log item = new Log();
                        item.id_log = dept.id_log;
                        item.handler = dept.handler;
                        item.body = dept.body;
                        item.header = dept.header;
                        item.create_date = dept.create_date;
                        drg.Items.Add(item);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void Button_back_Click(object sender, RoutedEventArgs e)
        {
            Menu mainWindow = new Menu();
            mainWindow.Show();
            this.Close();
        }
    }
}
