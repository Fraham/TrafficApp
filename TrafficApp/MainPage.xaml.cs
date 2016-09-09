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

        Traffic traffic;

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
            textBlock.Text = await Traffic.Process(showMotorways, showARoads);
        }

        private void FilterTraffic()
        {
            textBlock.Text = Traffic.Filter(showMotorways, showARoads);
        }

        private async void btnRefreshClick(object sender, RoutedEventArgs e)
        {
            textBlock.Text = "";
            await LoadTraffic();         
        }

        public Traffic Traffic
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

        private void tbtnMotorwayClicked(object sender, RoutedEventArgs e)
        {
            showMotorways = (bool)((ToggleButton)sender).IsChecked;
            FilterTraffic();
        }

        private void tbtnARoadClicked(object sender, RoutedEventArgs e)
        {
            showARoads = (bool)((ToggleButton)sender).IsChecked;
            FilterTraffic();
        }
    }
}
