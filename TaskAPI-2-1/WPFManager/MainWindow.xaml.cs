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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using WPFManager.Client;
using WPFManager.Request;
using WPFManager.Response;

namespace WPFManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Out.Text = "";
            var httpclient = new HttpClient();
            var client = new WPFClient(httpclient);
            var request = new WPFRequest();
            request.ToTime = ToTime.Text;
            request.FromTime = FromTime.Text;
            request.ClientBaseAddress = Address.Text;
            if (CPU.IsChecked == true)
            {
                var a = client.GetMetric(request);
                try
                {
                    foreach (var data in a.Metrics)
                    {
                        Out.Text = ($"{Out.Text}{"Значение"}-{data.Value} {"Дата"}-{data.Time.ToString("d")}\r\n");
                    }

                }
                catch
                {
                    MessageBox.Show("No metrics,please run agent service");
                }
            }
            
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
