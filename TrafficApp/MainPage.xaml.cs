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
        private bool showMotorways = true;
        private bool showARoads = true;

        static Traffic traffic;

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
            await Traffic.Process(showMotorways, showARoads);

            var frame = splitViewFrame as Frame;
            Page page = frame?.Content as Page;
            if (page?.GetType() != typeof(Views.Traffic.All))
            {
                frame.Navigate(typeof(Views.Traffic.All));
            }
        }
        /*
        private void FilterTraffic()
        {
            textBlock.Text = Traffic.Filter(showMotorways, showARoads);
        }

        private async void btnRefreshClick(object sender, RoutedEventArgs e)
        {
            textBlock.Text = "";
            await LoadTraffic();         
        }*/

        public static Traffic Traffic
        {
            get
            {
                if (traffic == null)
                {
                    traffic = new Traffic();
                }

                return traffic;
            }

            set
            {
                traffic = value;
            }
        }

        

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private void MotorwayMenuClick(object sender, RoutedEventArgs e)
        {
            var frame = splitViewFrame as Frame;
            Page page = frame?.Content as Page;
            if (page?.GetType() != typeof(Views.Traffic.Motorway))
            {
                frame.Navigate(typeof(Views.Traffic.Motorway));
            }
        }

        private void ARoadMenuClick(object sender, RoutedEventArgs e)
        {
            var frame = splitViewFrame as Frame;
            Page page = frame?.Content as Page;
            if (page?.GetType() != typeof(Views.Traffic.ARoad))
            {
                frame.Navigate(typeof(Views.Traffic.ARoad));
            }
            
        }

        private void AllMenuClick(object sender, RoutedEventArgs e)
        {
            var frame = splitViewFrame as Frame;
            Page page = frame?.Content as Page;
            if (page?.GetType() != typeof(Views.Traffic.All))
            {
                frame.Navigate(typeof(Views.Traffic.All));
            }
        }
    }
}
