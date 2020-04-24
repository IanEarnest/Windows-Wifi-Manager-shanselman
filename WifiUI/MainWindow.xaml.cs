using NetSh;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

/*
 * Windows Wifi Manager

Left click 
	- shows Wifi - name, security, auto?
	SCAPPA		WPA		Auto
Right click 
	- exit
Rick click SCAPPA 
	- forget this network, forget all networks


Code
Uses NetSh/ Linq


Questions
NetSh/ Linq - what is this?


Fixes
Rick click - exit opens in wrong location
1. Display current wifi password (where?) (and past passwords)
    /// Control Panel\Network and Internet\Network Connections
    /// Status - Wireless properties
    /// Security - Network security key(show characters)
 * 
 * 
 * 
 * 
 * 
 */


namespace WifiUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Get current wifi password
    /// Control Panel\Network and Internet\Network Connections
    /// Status - Wireless properties
    /// Security - Network security key(show characters)
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors

        /// <summary>
        /// Constructs the window.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            trayIcon.TrayLeftMouseDown += trayIcon_TrayLeftMouseDown;
            WifiProfiles = new ObservableCollection<WifiProfile>();
            listView.ItemsSource = WifiProfiles;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Represents the list of Wifi profiles to display.
        /// </summary>
        public ObservableCollection<WifiProfile> WifiProfiles { get; set; }

        #endregion

        #region Private Methods

        private void RefreshItemsSource()
        {
            WifiProfiles.Clear();

            List<WifiProfile> wifiProfiles = NetShWrapper.GetWifiProfiles();

            /*
             * Iterate over NetSh results to display them in the view.
             */
            foreach (WifiProfile wifiProfile in wifiProfiles)
            {
                WifiProfiles.Add(wifiProfile);
            }

            listView.Items.Refresh();
        }

        #endregion

        #region Events

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void HelloWorld(object sender, RoutedEventArgs e)
        {
            // Hello World
            
        }

        void trayIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
        {
            RefreshItemsSource();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            NetShWrapper.DeleteWifiProfile((listView.SelectedItem as WifiProfile).Name);
        }

        private void DeleteAutoOpen_Click(object sender, RoutedEventArgs e)
        {
            List<WifiProfile> wifiProfiles = NetShWrapper.GetWifiProfiles();

            /*
             * Iterate over NetSh results to remove bad profiles.
             */
            foreach (WifiProfile wifiProfile in wifiProfiles.Where(NetShWrapper.IsOpenAndAutoWifiProfile))
            {
                NetShWrapper.DeleteWifiProfile(wifiProfile.Name);
            }
        }

        #endregion
    }
}
