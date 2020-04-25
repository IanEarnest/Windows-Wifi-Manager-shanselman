using NetSh;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Interop;

/*
 * Windows Wifi Manager

Left click 
	- shows Wifi - name, security, auto?
	SCAPPA		WPA		Auto
Right click 
	- exit
Rick click SCAPPA 
	- forget this network, forget all networks
 */

/*
 * 
 * https://support.microsoft.com/en-us/help/305603/how-to-use-visual-c-to-close-another-application
 * 
 private Process[] processes;
private string procName = "Notepad";
private string specialFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.System);
    


    [STAThread]
static void Main() 
{
Application.Run(new Form1());
}
  
private void buildList()
{
ListViewItem itemAdd ; 
ListView1.Items.Clear();
processes = Process.GetProcessesByName(procName);
foreach (Process proc in processes)
{
itemAdd = ListView1.Items.Add(proc.MainWindowTitle);
itemAdd.SubItems.Add(proc.Id.ToString());
}
}

private void btnLaunch1_Click(object sender, System.EventArgs e)
{
ProcessStartInfo p = new ProcessStartInfo();
p.FileName = specialFolder + @"\eula.txt";
p.WindowStyle = ProcessWindowStyle.Minimized ;
Process proc =Process.Start(p);
proc.WaitForInputIdle();
buildList();
}
private void Form1_Load(object sender, System.EventArgs e)
{
      
}

private void btnClose1_Click(object sender, System.EventArgs e)
{
try
{         
int procID=System.Convert.ToInt32(ListView1.SelectedItems[0].SubItems[1].Text);
Process tempProc=Process.GetProcessById(procID);
tempProc.CloseMainWindow();
tempProc.WaitForExit();
buildList();
}
catch
{
MessageBox.Show("Please select a process in the ListView before clicking this button." +
" Or the Process may have been closed by somebody." );
buildList();
}
}

private void btnCloseAll_Click(object sender, System.EventArgs e)
{
try
{
foreach (Process proc in processes)
{
proc.CloseMainWindow();
proc.WaitForExit();
}
buildList();
}
catch (System.NullReferenceException)
{
MessageBox.Show("No instances of Notepad running.");
}
}

private void closing(object sender, System.ComponentModel.CancelEventArgs e)
{
//Make sure that you do not leave any instances running.
if (processes != null && processes.Length!=0)
this.btnCloseAll_Click(this,e);
}
}
}
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

            LaunchSecondPage();
        }

        public static void LaunchSecondPage()
        {
            //Application.Run(new Form1());
            SecondPage form = new SecondPage();
            //WindowInteropHelper wih = new WindowInteropHelper(this);
            //wih.Owner = form.Handle;
            form.ShowDialog();
        }

        public static void ExitMainWindow()
        {
            //this.Close();
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
            MessageBox.Show("Hello World");
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
