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

using XenUpdaterAPI;

namespace XenUpdater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Run();
        }

        private async void Run()
        {
            Network xuAPI = new Network("https://xenupdater.projectge.com/playclient");

            bool serviceStatus = await xuAPI.TestConnection();
            if (serviceStatus)
            {
                MessageBox.Show("Service is live! (called async)");
            }
            else
            {
                MessageBox.Show("Service is not live! (called async)");

            }
        }
    }
}
