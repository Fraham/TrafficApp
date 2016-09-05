using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TrafficApp.Model.Traffic;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace TrafficApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadTraffic();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            //LoadTraffic();
        }

        private async Task LoadTraffic()
        {
            Traffic traffic = new Traffic();
            await traffic.Process();
            textBlock.Text = traffic.ToString;
        }

        private async void btnRefreshClick(object sender, RoutedEventArgs e)
        {
            await LoadTraffic();
        }
    }
}
