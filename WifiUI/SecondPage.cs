using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            QuitWifiUI(); // Quit both at same time

            /*
            if (e.CloseReason == CloseReason.WindowsShutDown) 
            {
                QuitWifiUI(); // Quit both at same time
                return;
            }
            */
                // Confirm user wants to close
                /*
                switch (MessageBox.Show(this, "Are you sure you want to close?", "Closing", MessageBoxButtons.YesNo))
                {
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                    default: 
                        QuitWifiUI(); // Quit both at same time
                        break;
                }
                */
            }

        // Other - set window position

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
            // Quit both/ full process
            QuitWifiUI(); //this.Close();

            //trayIcon_TrayLeftMouseDown //RefreshItemsSource();
        }

        public static void QuitWifiUI()
        {
            // myApp[WifiUI1, WifiUI2] . kill
            //Process.GetProcessesByName("wmenc.exe")[0].CloseMainWindow();
            Process[] myApp = Process.GetProcessesByName("WifiUI");
            foreach (Process process in myApp)
                process.Kill();
        }
        // Not used, example
        private void Processes()
        {
            // Get all processes (each process has a module)
            // loop through to find "my process"
            // Stop "my process"
            Process[] runningProcesses = Process.GetProcesses();

            // runningProcesses[] has a list of processes
            // sort these
            Array.Sort(runningProcesses, (x, y) => String.Compare(x.ProcessName, y.ProcessName));


            foreach (Process process in runningProcesses)
            {
                try
                {
                    richTextBox1.Text += process.ProcessName + ". " + process.MainModule.FileName + "\n";
                    //if (process.Modules[0].FileName.Equals("WifiUI.exe"))
                    //if (Process.GetProcessesByName("WifiUI.exe"))
                    if (process.ProcessName == "WifiUI")
                    {
                        richTextBox1.Text += process.Modules[0].FileName + " DELETED, ";
                        process.Kill();
                        //proc.CloseMainWindow();
                        //proc.WaitForExit();
                    }
                }
                catch
                {
                    richTextBox1.Text += process.ProcessName + ". " + "error" + "\n";
                }
            }
            //listView1_SelectedIndexChanged
            //listView1.Items.Add
            //itemAdd = ListView1.Items.Add(proc.MainWindowTitle);
            //itemAdd.SubItems.Add(proc.Id.ToString());
            // now check the modules of the process
            //? example
            /*
            foreach (ProcessModule module in process.Modules)
            {
                if (module.FileName.Equals("MyProcessaaaaaaaa.exe"))
                {
                    richTextBox1.Text += module.FileName + " DELETED, ";
                    process.Kill();
                    //proc.CloseMainWindow();
                    //proc.WaitForExit();
                }
                else
                {
                    richTextBox1.Text += "Nothing , ";
                    //MessageBox.Show("No instances of Notepad running.");
                    //enter code here if process not found
                }
            }*/

            // Get button by name in another app and activate
            // only works with your own created apps? not external?
            /*Application app = Application.Launch("myapp.exe");
            Window win = app.GetWindow("name", InitializeOption.NoCache);

            Button btn1 = win.Get<Button>("quit");
            btn1.Click();
                 */
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

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
