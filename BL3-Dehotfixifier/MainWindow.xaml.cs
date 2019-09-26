using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        }

        string hostsEnabled = "127.0.0.1    discovery.services.gearboxsoftware.com";
        string hostsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"drivers\etc\hosts");
        private void Disable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StreamWriter w = File.AppendText(hostsPath))
                {
                    w.WriteLine(hostsEnabled);
                }
                flushDNSCache();
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred! Try running in administrator mode. If this doesn't work, contact FromDarkHell!");
            }
        }
        private void Enable_Click(object sender, RoutedEventArgs e)
        {
            var linesToKeep = File.ReadAllLines(hostsPath).Where(l => l != hostsEnabled);
            File.WriteAllLines(hostsPath, linesToKeep);
            flushDNSCache();

        }

        private void flushDNSCache()
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c ipconfig /flushdns";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo = startInfo;
            process.Start();

            process.WaitForExit(1000);
            process.Close();
            MessageBox.Show("Complete!");
        }
    }
}
