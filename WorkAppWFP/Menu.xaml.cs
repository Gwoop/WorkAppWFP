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

namespace WorkAppWFP
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Button_Logout(object sender, RoutedEventArgs e)
        {
            var confirmResult = MessageBox.Show("Вы уверенны что хотите выйти",
                                     "Выход",
                                     MessageBoxButton.YesNo);
            if (confirmResult == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
            else
            {
                return;
            }
            
        }

        private void Button_Server(object sender, RoutedEventArgs e)
        {
            Server server   = new Server();
            server.Show();
            this.Close();
        }

        private void Button_PC(object sender, RoutedEventArgs e)
        {
            PC pC = new PC();
            pC.Show();
            this.Close();
        }

        private void Button_Crud(object sender, RoutedEventArgs e)
        {
            Crud crud = new Crud();
            crud.Show();
            this.Close();
        }

        private void Button_Setting(object sender, RoutedEventArgs e)
        {
            Setting setting = new Setting();
            setting.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            History history = new History();
            history.Show();
            this.Close();
        }
    }
}
