using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetSh;

namespace WifiUI
{
    public partial class SecondPage : Form
    {
        public SecondPage()
        {
            InitializeComponent();
        }

        private void SecondPage_Load(object sender, EventArgs e)
        {

        }

        public static void Exit()
        {
            // Quit both at same time
            Application.Exit();
            //mainwindow this.Close();
            MainWindow.ExitMainWindow();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            //main
            InitializeComponent();
            //trayIcon.TrayLeftMouseDown += trayIcon_TrayLeftMouseDown;
            WifiProfiles = new ObservableCollection<WifiProfile>();
            //listView.ItemsSource = WifiProfiles;
            label1.Text = "WifiProfiles.Count: " + WifiProfiles.Count();// + "WifiProfiles.Count: " + WifiProfiles[0];
        }

        public ObservableCollection<WifiProfile> WifiProfiles { get; set; }

        private void button2_Click(object sender, EventArgs e)
        {
            //private void RefreshItemsSource()
                WifiProfiles.Clear();

                List<WifiProfile> wifiProfiles = NetShWrapper.GetWifiProfiles();

                /*
                 * Iterate over NetSh results to display them in the view.
                 */
                foreach (WifiProfile wifiProfile in wifiProfiles)
                {
                    WifiProfiles.Add(wifiProfile);
                }

            //listView.Items.Refresh();
            label1.Text = "WifiProfiles.Count: " + WifiProfiles.Count() + "WifiProfiles.Count: " + WifiProfiles[0];
            textBox1.Text = "hi";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainWindow.LaunchSecondPage();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello World");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //trayIcon_TrayLeftMouseDown
            //RefreshItemsSource();


            // Quit
            this.Close();
        }
        //delete click
        //NetShWrapper.DeleteWifiProfile((listView.SelectedItem as WifiProfile).Name);

        //delete auto open
        /*
        List<WifiProfile> wifiProfiles = NetShWrapper.GetWifiProfiles();
        
        foreach (WifiProfile wifiProfile in wifiProfiles.Where(NetShWrapper.IsOpenAndAutoWifiProfile))
        {
            NetShWrapper.DeleteWifiProfile(wifiProfile.Name);
        }
        */

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
