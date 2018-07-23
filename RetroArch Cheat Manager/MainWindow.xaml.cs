using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace RetroArch_Cheat_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int cheatCount = 0;
        int currentCheatID = -1;
        String filepath;
        String fileContents;
        List<Cheat> cheats;

        public MainWindow()
        {
            InitializeComponent();
            cheats = new List<Cheat>();
            UpdateCheatCount(cheatCount);
        }

        private void UpdateCheatCount(int n)
        {
            cheatCount = n;
            label_CurrentCheats.Content = "Current Cheats (" + cheatCount + "):";
            PopulateListBox();
        }

        private void PopulateListBox()
        {
            listbox_CheatsList.Items.Clear();
            for (int i = 0; i < cheatCount; i++)
            {
                cheats.ElementAt(i).Id = i;
                listbox_CheatsList.Items.Add(cheats.ElementAt(i).Name);
            }
        }

        private void NewFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "RetroArch Cheat Files (*.cht)|*.cht";
            sfd.Title = "Save a New RetroArch Cheat File";
            if (sfd.ShowDialog() == true)
            {
                filepath = sfd.FileName;
            }
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            cheats = new List<Cheat>();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "RetroArch Cheat Files (*.cht)|*.cht";
            ofd.Title = "Open a RetroArch Cheat File";

            if (ofd.ShowDialog() == true)
            {
                filepath = ofd.FileName;
                fileContents = File.ReadAllText(filepath);
            }

            string[] lines = fileContents.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                if (line.Contains("cheats = "))
                {
                    line = line.Remove(0, 8);
                    line.Trim();
                    cheatCount = Int32.Parse(line);

                    for (int j = 0; j < cheatCount; j++)
                    {
                        cheats.Add(new Cheat());
                        //cheats.Insert(i, new Cheat());
                    }
                    UpdateCheatCount(cheatCount);
                }

                if (line.Contains("_desc"))
                {
                    line = line.Remove(0, 5);
                    int id = Int32.Parse( line.Remove( line.IndexOf("_") ) );
                    string name = line.Substring(line.IndexOf("\"") + 1, line.Length - line.IndexOf("\"") - 2);
                    cheats.ElementAt(id).Name = name;
                    //cheats.ElementAt(id].Id = id;
                }

                if (line.Contains("_code"))
                {
                    line = line.Remove(0, 5);
                    int id = Int32.Parse(line.Remove(line.IndexOf("_")));
                    string code = line.Substring(line.IndexOf("\"") + 1, line.Length - line.IndexOf("\"") - 2);
                    cheats.ElementAt(id).Code = code;
                }

                if (line.Contains("_enable"))
                {
                    line = line.Remove(0, 5);
                    int id = Int32.Parse(line.Remove(line.IndexOf("_")));
                    string enable = line.Substring(line.IndexOf("=") + 2).ToLower();
                    if (enable == "false")
                    {
                        cheats.ElementAt(id).Enable = false;
                    }
                    else if (enable == "true")
                    {
                        cheats.ElementAt(id).Enable = true;
                    }
                }
            }
            PopulateListBox();
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            StringBuilder cheatFileContents = new StringBuilder();

            cheatFileContents.Append("cheats = " + cheatCount + "\n\n");

            for (int i = 0; i < cheats.Count; i++)
            {
                cheatFileContents.Append(cheats.ElementAt(i).ToString());
            }

            File.WriteAllText(filepath, cheatFileContents.ToString());
        }

        private void DeleteCheat(object sender, RoutedEventArgs e)
        {
            int index = listbox_CheatsList.SelectedIndex;
            if (index != -1)
            {
                cheats.RemoveAt(index);
                cheatCount--;
                UpdateCheatCount(cheatCount);
            }
        }

        private void EditCheat(object sender, RoutedEventArgs e)
        {
            int listBoxIndex = listbox_CheatsList.SelectedIndex;
            if (listBoxIndex != -1)
            {
                textbox_CheatName.Text = cheats.ElementAt(listBoxIndex).Name;
                textbox_CheatCodes.Text = cheats.ElementAt(listBoxIndex).Code;
                currentCheatID = listBoxIndex;
            }
        }

        private void SaveCheat(object sender, RoutedEventArgs e)
        {
            if (currentCheatID != -1)
            {
                cheats.ElementAt(currentCheatID).Name = textbox_CheatName.Text;
                cheats.ElementAt(currentCheatID).Code = textbox_CheatCodes.Text;
                PopulateListBox();
            }
            else if (currentCheatID == -1)
            {
                AddCheat(sender, e);
            }
        }

        private void AddCheat(object sender, RoutedEventArgs e)
        {
            string name = textbox_CheatName.Text;
            string code = textbox_CheatCodes.Text;
            cheats.Add(new Cheat(name, code, false, cheatCount));
            cheatCount++;
            UpdateCheatCount(cheatCount);
        }
    }
}
