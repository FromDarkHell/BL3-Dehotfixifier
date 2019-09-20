using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace BL3_Dehotfixifier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LauncherTextBox.Text = Properties.Settings.Default.epicLauncherPath;
        }
        string cmdEnable = "Netsh advfirewall firewall add rule name=\"EGS - BL3 - DENY ACCESS\" program=\"{NAME}\" protocol=tcp dir=out enable=yes action = block profile=private,domain,public";

        string cmdDisable = "netsh advfirewall firewall set rule name=\"EGS - BL3 - DENY ACCESS\" new enable=no";
        private void Enable_Click(object sender, RoutedEventArgs e)
        {
            string newCmd = cmdDisable.Replace("{NAME}", LauncherTextBox.Text);
            startCMD(newCmd);
        }
        private void Disable_Click(object sender, RoutedEventArgs e)
        {
            string newCmd = cmdEnable.Replace("{NAME}", LauncherTextBox.Text);
            startCMD(newCmd);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default.epicLauncherPath = LauncherTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void startCMD(string cmd)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C " + cmd;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit(1000);
            process.Dispose();
            MessageBox.Show("Complete!");
        }

    }
}
