using ControlzEx.Standard;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WorkAppWFP
{
    /// <summary>
    /// Логика взаимодействия для PC.xaml
    /// </summary>
    public partial class PC : Window
    {

        PerformanceCounter cpuCounter;
        PerformanceCounter ramCounter;
        public PC()
        {
            InitializeComponent();
            stop.IsEnabled= false;
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            cp.Content = Convert.ToInt32(cpuCounter.NextValue()) + "%";
            cpy.Content = ramCounter.NextValue() + "MB";

        }

        bool zagg = false;

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            this.Close();
        }

        [DllImport("kernel32.dll")]
        static extern UIntPtr SetThreadAffinityMask(IntPtr hThread, UIntPtr dwThreadAffinityMask);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentThread();
        private void testing(object obj)
        {
            
                var token = (CancellationToken)obj;
                while (!token.IsCancellationRequested) { }
           

        }

        private void test(object proc)
        {
                        while (zagg)
                        {
                            var source = new CancellationTokenSource();
                            for (int i = 0; i < Environment.ProcessorCount; i++)
                                new Thread(testing).Start(source.Token);
                            source.Cancel();
                        }
              
        }

        private void Button_Start(object sender, RoutedEventArgs e)
        {

            if (textbo.Text == "")
            {
                MessageBox.Show("Введите значение нагрузки RAM", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            stop.IsEnabled = true;
            start.IsEnabled = false;
            status.Content = "Запущен";
            zagg = true;
            new Thread(test).Start(60);
        

        }

        private void TextValidationNumber(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[a-zA-Z]+$");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            zagg = false;
            status.Content = "Не запущен";
            stop.IsEnabled = false;
            start.IsEnabled = true;
        }

        private void Button_Refresh(object sender, RoutedEventArgs e)
        {
            
            cp.Content = cpuCounter.NextValue() + "%";
            cpy.Content = ramCounter.NextValue() + "MB";
        }
    }
}
