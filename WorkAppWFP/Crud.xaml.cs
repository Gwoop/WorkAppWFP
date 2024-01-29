using ControlzEx.Standard;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static WorkAppWFP.Crud;

namespace WorkAppWFP
{
    /// <summary>
    /// Логика взаимодействия для Crud.xaml
    /// </summary>
    public partial class Crud : Window
    {
        public Crud()
        {
            InitializeComponent();
            GetDataPatern();
            GetDatatext();
            GetUser();
            com.SelectedIndex = 0;
            drg.SelectedValuePath = "id";
            drg2.SelectedValuePath = "id";
            drg3.SelectedValuePath = "id";
            testuser.Visibility = Visibility.Hidden;
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Hide();
        }


        private void GetDataPatern()
        {
            string URL = Const.PathServer + "/admin/getdocpattern";
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
                    var deptList = System.Text.Json.JsonSerializer.Deserialize<IList<Response>>(response);
                    foreach (var dept in deptList)
                    {
                        Response item = new Response();
                        item.id = dept.id;
                        item.name = dept.name;
                        item.description = dept.description;
                        item.uuid = dept.uuid;
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
        private void GetDatatext()
        {
            Const @const = new Const();
            string URL = Const.PathServer + "/admin/getdockstext";
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
                    var deptList = System.Text.Json.JsonSerializer.Deserialize<IList<ResponsesDockstext>>(response);
                    foreach (var dept in deptList)
                    {
                        ResponsesDockstext item = new ResponsesDockstext();
                        item.id = dept.id;
                        item.id_doc = dept.id_doc;
                        item.text = dept.text;
                        item.create_date = dept.create_date;
                        item.lang = dept.lang;
                        item.uuid = dept.uuid;
                        drg2.Items.Add(item);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }
        private void GetUser()
        {
            string URL = Const.PathServer + "/admin/getuser";
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
                    var deptList = System.Text.Json.JsonSerializer.Deserialize<IList<ResponsesUser>>(response);
                    foreach (var dept in deptList)
                    {
                        ResponsesUser item = new ResponsesUser();
                        item.id = dept.id;
                        item.name = dept.name;
                        item.lastname = dept.lastname;
                        item.sex = dept.sex;
                        item.birdh = dept.birdh;
                        item.tel = dept.tel;
                        item.chatid = dept.chatid;
                        item.email = dept.email;
                        item.password = dept.password;
                        drg3.Items.Add(item);

    }
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }
        public class Response
        {
            public long id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string uuid { get; set; }
            public string create_date { get; set; }
        }
        public class RequestDockpattern
        {
            public string name { get; set; }
            public string description { get; set; }
        }
        public class ResponsesDockstext
        {
            public long id { get; set; }
            public long id_doc { get; set; }
            public string text { get; set; }
            public string create_date { get; set; }
            public string lang { get; set; }
            public string uuid { get; set; }
        }

        public class ResponsesUser
        {
            public long id { get; set; }
            public string name { get; set; }
            public string lastname { get; set; }
            public int sex { get; set; }
            public string birdh { get; set; }
            public string tel { get; set; }
            public int chatid { get; set; }
            public string email { get; set; }
            public string password { get; set; }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
        }

        private void ComboBox_SelectionDate(object sender, SelectionChangedEventArgs e)
        {
            if (com.SelectedIndex == 0)
            {
                
                btnins.Visibility = Visibility.Visible;
                btnup.Visibility = Visibility.Visible;
                btndel.Visibility = Visibility.Visible;
                text1.Visibility = Visibility.Visible;
                text2.Visibility = Visibility.Visible;
                text3.Visibility = Visibility.Visible;
                text4.Visibility = Visibility.Visible;

                drg.Visibility= Visibility.Visible;
                drg2.Visibility = Visibility.Collapsed;
                drg3.Visibility = Visibility.Hidden;


                one.Content = "ID";
                two.Content = "Название";
                the.Content = "Описание";
                forr.Content = "uuid";
                five.Content = "Дата создания";
                six.Visibility = Visibility.Hidden;
                textsix.Visibility= Visibility.Hidden;
                testuser.Visibility = Visibility.Hidden;
            }
            if (com.SelectedIndex == 1)
            {
                
                btnins.Visibility = Visibility.Visible;
                btnup.Visibility = Visibility.Hidden;
                btndel.Visibility = Visibility.Hidden;


                drg.Visibility = Visibility.Hidden;
                drg2.Visibility = Visibility.Visible;
                drg3.Visibility = Visibility.Hidden;
                one.Content = "ID";
                two.Content = "Шаблон";
                the.Content = "Содержание";
                forr.Content = "Дата создания";
                five.Content = "Язык";
                six.Visibility = Visibility.Visible;
                textsix.Visibility = Visibility.Visible;
                six.Content = "uuid";
                testuser.Visibility = Visibility.Hidden;

            }
            if (com.SelectedIndex == 2)
            {

                text1.Visibility = Visibility.Hidden;
                text2.Visibility = Visibility.Hidden;
                text3.Visibility = Visibility.Hidden;
                text4.Visibility = Visibility.Hidden;


                
                btnins.Visibility = Visibility.Hidden;
                btnup.Visibility = Visibility.Hidden;
                btndel.Visibility = Visibility.Hidden;
                drg.Visibility = Visibility.Hidden;
                drg2.Visibility = Visibility.Hidden;
                drg3.Visibility = Visibility.Visible;
                one.Content = "ID";
                two.Content = "Шаблон";
                the.Content = "Содержание";
                forr.Content = "Дата создания";
                five.Content = "Язык";
                six.Visibility = Visibility.Hidden;
                
                textsix.Visibility = Visibility.Hidden;
                six.Content = "uuid";
                testuser.Visibility = Visibility.Visible;
            }
        }

        private void Button_Insert(object sender, RoutedEventArgs e)
        {
            int comidex = com.SelectedIndex;

            if (text1.Text == "" && text2.Text == "")
            {
                MessageBox.Show("Вы ввели не все данные для добавления", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            switch (comidex)
            {
                case 0:
                    var response = new RequestDockpattern
                    {
                        name = text1.Text,
                        description = text2.Text,
                    };
                    string jsonString = System.Text.Json.JsonSerializer.Serialize(response);
                    string URL = Const.PathServer + "/admin/adddocpattern";

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = jsonString.Length;
                    request.ContentType = "application/json";
                    string encoded = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                                   .GetBytes(Const.Login + ":" + Const.Password));

                    request.Headers.Add("Authorization", "Basic " + encoded);
                    StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
                    requestWriter.Write(jsonString);
                    requestWriter.Close();

                    try
                    {
                        WebResponse webResponse = request.GetResponse();
                        Stream webStream = webResponse.GetResponseStream();
                        StreamReader responseReader = new StreamReader(webStream);
                        string responses = responseReader.ReadToEnd();
                        responseReader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                    break;
                case 1:

                    var response1 = new ResponsesDockstext
                    {
                        id_doc = Convert.ToInt64(text1.Text),
                        text = text2.Text,
                        lang = text4.Text,
                    };
                    string jsonString1 = System.Text.Json.JsonSerializer.Serialize(response1);
                    string URL1 = Const.PathServer + "/admin/adddockstextactyality/" + Convert.ToString(response1.id_doc) + "/";

                    HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(URL1);
                    request1.Method = "POST";
                    request1.ContentType = "application/json";
                    request1.ContentLength = jsonString1.Length;
                    request1.ContentType = "application/json";
                    string encoded1 = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                                   .GetBytes(Const.Login + ":" + Const.Password));

                    request1.Headers.Add("Authorization", "Basic " + encoded1);
                    StreamWriter requestWriter1 = new StreamWriter(request1.GetRequestStream(), System.Text.Encoding.ASCII);
                    requestWriter1.Write(jsonString1);
                    requestWriter1.Close();

                    try
                    {
                        WebResponse webResponse = request1.GetResponse();
                        Stream webStream = webResponse.GetResponseStream();
                        StreamReader responseReader = new StreamReader(webStream);
                        string responses = responseReader.ReadToEnd();
                        responseReader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }

                    break;
                case 2:

                    break;

            }

            drg.Items.Clear();
            drg2.Items.Clear();
            drg3.Items.Clear();

            GetDataPatern();
            GetDatatext();
            GetUser();
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            if (drg.SelectedItem == null)
            {
                MessageBox.Show("Чтобы удалить данные выберите её в списке", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (drg.SelectedItem != null)
            {
                Response response = new Response()
                {
                    id = (drg.SelectedItem as Response).id
                };

                string URL1 = Const.PathServer + "/admin/deletedocpattern/" + Convert.ToString(response.id) + "/";

                HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(URL1);
                request1.Method = "DELETE";
                request1.ContentType = "application/json";
                request1.ContentType = "application/json";
                string encoded1 = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                               .GetBytes(Const.Login + ":" + Const.Password));

                request1.Headers.Add("Authorization", "Basic " + encoded1);

                try
                {
                    WebResponse webResponse = request1.GetResponse();
                    Stream webStream = webResponse.GetResponseStream();
                    StreamReader responseReader = new StreamReader(webStream);
                    string responses = responseReader.ReadToEnd();
                    responseReader.Close();
                }
                catch
                {
                    return;
                }
            }
            if (drg2.SelectedItem != null)
            {
                return;
            }
            if (drg3.SelectedItem != null)
            {
                return;
            }
            drg.Items.Clear();
            drg2.Items.Clear();
            drg3.Items.Clear();

            GetDataPatern();
            GetDatatext();
            GetUser();
        }

        private void drg2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (drg2.SelectedItem != null)
            {
                ResponsesDockstext response = new ResponsesDockstext()
                {
                    id = (drg2.SelectedItem as ResponsesDockstext).id,
                    id_doc = (drg2.SelectedItem as ResponsesDockstext).id_doc,
                    text = (drg2.SelectedItem as ResponsesDockstext).text,
                    create_date = (drg2.SelectedItem as ResponsesDockstext).create_date,
                    lang = (drg2.SelectedItem as ResponsesDockstext).lang,
                    uuid = (drg2.SelectedItem as ResponsesDockstext).uuid,
                };

                text1.Text = Convert.ToString(response.id_doc);
                text2.Text = response.text;
                text3.Text = response.create_date;
                text4.Text = response.lang;
                textsix.Text = response.uuid;

            }
        }

        private void drg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (drg.SelectedItem != null)
            {
                Response response = new Response() { id = (drg.SelectedItem as Response).id,
                name = (drg.SelectedItem as Response).name,
                description = (drg.SelectedItem as Response).description,
                uuid = (drg.SelectedItem as Response).uuid,
                create_date = (drg.SelectedItem as Response).create_date,
                };

            
                text1.Text = response.name;
                text2.Text = response.description;
                text3.Text = response.uuid;
                text4.Text = response.create_date;

            }
        }

        private void drg3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (drg3.SelectedItem != null)
            {
                ResponsesUser response = new ResponsesUser()
                {
                    id = (drg3.SelectedItem as ResponsesUser).id,
                    name = (drg3.SelectedItem as ResponsesUser).name,
                    lastname = (drg3.SelectedItem as ResponsesUser).lastname,
                    sex = (drg3.SelectedItem as ResponsesUser).sex,
                    birdh = (drg3.SelectedItem as ResponsesUser).birdh,
                    tel = (drg3.SelectedItem as ResponsesUser).tel,
                    chatid = (drg3.SelectedItem as ResponsesUser).chatid,
                    email = (drg3.SelectedItem as ResponsesUser).email,
                    password = (drg3.SelectedItem as ResponsesUser).password,
                };


                text1.Text = response.name;
                text2.Text = response.lastname;
                text3.Text = Convert.ToString(response.sex);
                text4.Text = response.birdh;
                textsix.Text = response.tel;

            }
        }

        private void testuser_Click(object sender, RoutedEventArgs e)
        {
            string URL1 = Const.PathServer + "/admin/adduser";

            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(URL1);
            request1.Method = "GET";
            request1.ContentType = "application/json";
            request1.ContentType = "application/json";
            string encoded1 = System.Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                           .GetBytes(Const.Login + ":" + Const.Password));

            request1.Headers.Add("Authorization", "Basic " + encoded1);

            try
            {
                WebResponse webResponse = request1.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string responses = responseReader.ReadToEnd();
                responseReader.Close();
            }
            catch
            {
                return;
            }
            GetUser();
        }
    }
}
