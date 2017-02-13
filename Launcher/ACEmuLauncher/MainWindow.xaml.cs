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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Newtonsoft.Json;
using System.Collections;


namespace ACEmuLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public class ProfileItem //for Json file
    {
        public string profileName;
        public string characterName;
        public string characterPassword;
        public string serverName;
        public string serverPort;
    }



    public partial class MainWindow : Window
    {


        public MainWindow() //Main Function
        {
            InitializeComponent();
            string path = Directory.GetCurrentDirectory(); 
            string client = "\\acclient.exe";
                
                if (!File.Exists(path + client)) //Check to see if in the correct path
                {
                    MessageBox.Show("Please move launcher to the acclient folder");
                }


            List<ProfileItem> pitems = new List<ProfileItem>();

            pitems = profileLoadJson(); //grab JSON file

            for (int i = 0; i < pitems.Count; i++) //loop through the whole array and add profiles to drop down
            {
                profileComboBox.Items.Add(pitems[i].profileName);
            }
        }

        public void updateComboBox() //updates comboBox
        {
            List<ProfileItem> pitems = new List<ProfileItem>();
            pitems = profileLoadJson(); //grab JSON file

            serverTxtBox.Text = pitems[0].serverName; //update main menu from the first profile
            portTxtBox.Text = pitems[0].serverPort;
            usernameTxtBox.Text = pitems[0].characterName;
            passwordTxtBox.Text = pitems[0].characterPassword;


            for (int i = 0; i < pitems.Count; i++) //loop through the whole array and add profiles to drop down
            {
                profileComboBox_Copy.Items.Add(pitems[i].profileName);
            }

           // this.InvalidateVisual();
          //  this.UpdateLayout();
           // profileComboBox.Items.Refresh();
        }


        private void launchBtn_Click(object sender, RoutedEventArgs e) //Launch Client
        {
            LaunchGame.launchGame(serverTxtBox.Text, portTxtBox.Text, usernameTxtBox.Text, passwordTxtBox.Text);
            Environment.Exit(0);
        }

        private void returnBtn_Click(object sender, RoutedEventArgs e) //reload webpage
        {
            webAC.Navigate("http://acemulator.org/");
        }



        public void LoadJson() //load JSON file
        {
            string path = Directory.GetCurrentDirectory();

            if (!File.Exists(path + "\\profiles.json")) //check to see if it exists
            {
                using (StreamWriter file = File.CreateText(path + "\\profiles.json")) ; 


            }
            else
            {
                using (StreamReader r = new StreamReader("profiles.json")) // if exists update items.
                {
                    string json = r.ReadToEnd();
                    List<ProfileItem> items = JsonConvert.DeserializeObject<List<ProfileItem>>(json);

                    serverTxtBox.Text = items[0].serverName;
                    portTxtBox.Text = items[0].serverPort;
                    usernameTxtBox.Text = items[0].characterName;
                    passwordTxtBox.Text = items[0].characterPassword;
                    r.Close();
                    r.Dispose();


                }
            }
            try { updateComboBox(); } catch { }

        }

      
        
        public static System.Collections.Generic.List<ProfileItem> profileLoadJson()
        {
            string path = Directory.GetCurrentDirectory();

            if (!File.Exists(path + "\\profiles.json"))
            {
                // using (StreamWriter file = File.CreateText(path + "\\profiles.json")) ; 
                return null;

            }
            else
            {
                using (StreamReader r = new StreamReader("profiles.json"))
                {
                    string json = r.ReadToEnd();
                    List<ProfileItem> items = JsonConvert.DeserializeObject<List<ProfileItem>>(json);
                    r.Close();
                    r.Dispose();

                    return items;
                }
            }


        }
        private void profileBtn_Click(object sender, RoutedEventArgs e)
        {
            // LoadJson();
            Profiles prof = new Profiles();
            prof.Show();
            updateComboBox();

        }

        public static void updateJSOC(string pName, string cName, string cPass, string sName, string sPort, bool update) //update Jsoc file
        {
            string path = Directory.GetCurrentDirectory();

            ProfileItem updateProfile = new ProfileItem
            {
                profileName = pName,
                characterName = cName,
                characterPassword = cPass,
                serverName = sName,
                serverPort = sPort
            };

            if (File.Exists(path + "\\profiles.json"))//check to see if there is a profile.json
            {
                using (StreamReader r = new StreamReader("profiles.json"))
                {
                    string json = r.ReadToEnd();
                    List<ProfileItem> items = JsonConvert.DeserializeObject<List<ProfileItem>>(json);
                    r.Close();
                    r.Dispose();

                    if (update == true) //update the current record
                    {
                        for (int i = 0; i < items.Count; i++)
                        {
                            if (pName == items[i].profileName)
                            {
                                items[i].characterName = cName;
                                items[i].serverName = sName;
                                items[i].characterPassword = cPass;
                                items[i].serverPort = sPort;
                            }

                        }

                        using (StreamWriter file = File.CreateText(path + "\\profiles.json")) //write updated JSON
                        {
                            JsonSerializer serial = new JsonSerializer();
                            serial.Serialize(file, items);
                            file.Close();
                            file.Dispose();
                        }

                    }
                    else //add new record
                    {
                        using (StreamWriter file = File.CreateText(path + "\\profiles.json"))
                        {
                            JsonSerializer serial = new JsonSerializer();
                            items.Add(updateProfile);
                            serial.Serialize(file, items);
                            file.Close();
                            file.Dispose();
                        }

                    }
                }

            }
            else //write file if it doesn't exist
            {

                JsonSerializer serial = new JsonSerializer();
                string updates = "[" + JsonConvert.SerializeObject(updateProfile, Formatting.None) + "]";
                File.WriteAllText(path + "\\profiles.json", updates);
                //serial.Serialize(file, updates);
            }
            
        }

        public static void deleteProfile(string pName, string cName, string cPass, string sName, string sPort)
        {

            string path = Directory.GetCurrentDirectory();

            ProfileItem updateProfile = new ProfileItem
            {
                profileName = pName,
                characterName = cName,
                characterPassword = cPass,
                serverName = sName,
                serverPort = sPort
            };

            if (File.Exists(path + "\\profiles.json"))//check to see if there is a profile.json
            {
                using (StreamReader r = new StreamReader("profiles.json"))
                {
                    string json = r.ReadToEnd();
                    List<ProfileItem> items = JsonConvert.DeserializeObject<List<ProfileItem>>(json);
                    r.Close();
                    r.Dispose();

                    for (int i = 0; i < items.Count; i++) //loop through
                    {
                        if (pName == items[i].profileName)
                        {
                            items.RemoveAt(i);  //remove row
                        }

                    }

                    using (StreamWriter file = File.CreateText(path + "\\profiles.json")) //write updated JSON
                    {
                        JsonSerializer serial = new JsonSerializer();
                        serial.Serialize(file, items);
                        file.Close();
                        file.Dispose();
                    }

                }
            }



                }

        private void profileComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) // When comboBox selection changes update boxes
        {
            List<ProfileItem> pitems = new List<ProfileItem>();
            pitems = profileLoadJson();
            string selection = "";
            try { selection = profileComboBox.SelectedItem.ToString(); } catch { }
            //selection = profileComboBox.SelectedItem.ToString(); 
            int index = 0;
            for (int i = 0; i < pitems.Count; i++)
            {
                
                if (pitems[index].profileName == selection)
                {
                    serverTxtBox.Text = pitems[index].serverName;
                    portTxtBox.Text = pitems[index].serverPort;
                    usernameTxtBox.Text = pitems[index].characterName;
                    passwordTxtBox.Text = pitems[index].characterPassword;
                }
                index++;

            }

        }

        private void MainWindow1_GotFocus(object sender, RoutedEventArgs e)
        {
           // profileComboBox.Items.Refresh();
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            List<ProfileItem> pitems = new List<ProfileItem>();

            pitems = profileLoadJson(); //grab JSON file
            profileComboBox.Items.Clear();
            for (int i = 0; i < pitems.Count; i++) //loop through the whole array and add profiles to drop down
            {
                
                profileComboBox.Items.Add(pitems[i].profileName);
            }

        }
    }
}
