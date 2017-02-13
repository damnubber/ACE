using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;


namespace ACEmuLauncher
{
    /// <summary>
    /// Interaction logic for Profiles.xaml
    /// </summary>
    public partial class Profiles : Window
    {
        public Profiles()
        {
            InitializeComponent();

            List<ProfileItem> pitems = new List<ProfileItem>(); //load profiles and populate comboBox
            pitems = MainWindow.profileLoadJson();

            try
            {
                for (int i = 0; i < pitems.Count; i++)
                {
                    profileComboBox.Items.Add(pitems[i].profileName);
                }
            }
            catch { }
        }

        private void button1_Click(object sender, RoutedEventArgs e) //cancel Button
        {
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e) //update
        {
            string path = Directory.GetCurrentDirectory();
            if (File.Exists(path + "\\profiles.json"))
            {
                List<ProfileItem> pitems = new List<ProfileItem>();
                pitems = MainWindow.profileLoadJson();
            
                for (int i = 0; i < pitems.Count; i++)
                {
                    if (profileNameTxtBox.Text == (pitems[i].profileName)) //does name match current record
                    {
                        MainWindow.updateJSOC(
                    profileNameTxtBox.Text,
                    CharacterNameTxtBox.Text,
                    passwordTxtBox.Text,
                    serverTxtBox.Text,
                    portTxtBox.Text,
                    true

                    );
                        this.Close();
                        return;
                        
                    }
                }
                //If name isn't found add line in file
                MainWindow.updateJSOC(
                profileNameTxtBox.Text,
                CharacterNameTxtBox.Text,
                passwordTxtBox.Text,
                serverTxtBox.Text,
                portTxtBox.Text,
                false
                );
                
            }
            else //if file doesn't exist 
            {
                MainWindow.updateJSOC(
                profileNameTxtBox.Text,
                CharacterNameTxtBox.Text,
                passwordTxtBox.Text,
                serverTxtBox.Text,
                portTxtBox.Text,
                false
                );
            }
            
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e) //delete button
        {
            MainWindow.deleteProfile(profileNameTxtBox.Text,
                    CharacterNameTxtBox.Text,
                    passwordTxtBox.Text,
                    serverTxtBox.Text,
                    portTxtBox.Text);
            this.Close();
        }

        private void profileComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) //when item is selected populate boxes
        {
            List<ProfileItem> pitems = new List<ProfileItem>();
            pitems = MainWindow.profileLoadJson();

            string selection = profileComboBox.SelectedItem.ToString(); 
            int index = 0;

            for (int i = 0; i < pitems.Count; i++) //check to see if the profile is listed if so append record for writing
            {

                if (pitems[index].profileName == profileComboBox.SelectedValue.ToString())
                {
                    profileNameTxtBox.Text = pitems[index].profileName;
                    serverTxtBox.Text = pitems[index].serverName;
                    portTxtBox.Text = pitems[index].serverPort;
                    CharacterNameTxtBox.Text = pitems[index].characterName;
                    passwordTxtBox.Text = pitems[index].characterPassword;
                }
                index++;

            }
        }
    }
}
