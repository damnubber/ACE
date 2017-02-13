using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;

namespace ACEmuLauncher
{
    class LaunchGame
    {


        public static void launchGame(string server, string port, string userName, string password)
        {
            string fileName = " -h " + server + " -p " + port + " -a " + userName + ":" + password;
            try
            {
             
             // The commeneted code supports normal injection from Decal
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();

                cmd.StandardInput.WriteLine("acclient.exe -h " + server + " -p " + port + " -a " + userName + ":" + password);
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.Close();
             
             
             //   Process.Start("acclient.exe", fileName); 
            }
            catch
            {
                MessageBox.Show("Game failed to launch. Please check and make sure the launcher is in the same folder as acclient");
            }
        }



    }
}
