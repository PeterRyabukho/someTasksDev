using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _5._1.BACKUP_SYSTEM
{
    public partial class Form1 : Form
    {
        //Observer observer;
        
        private string selectedFolder;
        private int count = 1;
        //private string beckupFolder = @"D:\Users\cam\mydocs\Files for Task-05\Beckup Files\";
        private string saveNewFileName;
        string newPathBackupFolder=Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+@"\Training Task-05\Beckup folder\";

        private void CheckFolder()
        {
            bool directoryExists = Directory.Exists(newPathBackupFolder);
            if (directoryExists)
            {
                MessageBox.Show("The directory exists!");
            }
            else
            {
                Directory.CreateDirectory(newPathBackupFolder);
            }
        }

        public Form1()
        {
            InitializeComponent();
            CheckFolder();
            string[] files = Directory.GetFiles(newPathBackupFolder);
            for (int i = 0; i < files.Length; i++)
            {
                DateTime beckupDate = File.GetLastWriteTime(files[i]);
                dateList.Items.Add($"{count}. {beckupDate}");

            }

            //observer = new Observer();

        }

        private void FileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs eventFile)
        {
            
            //fileSystemWatcher1.Path = selectedFolder;
            MessageBox.Show(newPathBackupFolder);
            File.Replace(eventFile.FullPath, newPathBackupFolder + eventFile.Name, newPathBackupFolder + $"{DateTime.Now:dd.mm.yyyy}. {eventFile.Name}");
            count++;
        }

        private void FileSystemWatcher1_Created(object sender, System.IO.FileSystemEventArgs eventFile)
        {
            //fileSystemWatcher1.Path = selectedFolder;
            File.Copy(eventFile.FullPath, newPathBackupFolder + eventFile.Name, true);
        }

        private void FolderButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = selectedFolder;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                selectedFolder = folderBrowserDialog1.SelectedPath;

                newPathBackupFolder = selectedFolder + @"\Backup files\";
                bool directoryExists = Directory.Exists(newPathBackupFolder);
                if (directoryExists)
                {
                    MessageBox.Show("The directory exists!");
                }
                else
                {
                    Directory.CreateDirectory(newPathBackupFolder);
                }

            }
            //MessageBox.Show(selectedFolder);
        }

        private void SaveAsButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(newText.Text))
            {
                MessageBox.Show("Plese type some text", "Unable to save this text", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            saveFileDialog1.InitialDirectory = selectedFolder;
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FileName = "";
            DialogResult result = saveFileDialog1.ShowDialog();
            
            if (result == DialogResult.OK)
            {
                saveNewFileName = selectedFolder + saveFileDialog1.FileName;
                using (StreamWriter writer = new StreamWriter(saveNewFileName))
                {
                    writer.WriteLine(newText.Text);
                }
                MessageBox.Show("New file written!", "Complite", MessageBoxButtons.OK);
                //MessageBox.Show(saveNewFileName);
            }
            //MessageBox.Show(newPathBackupFolder);
            //MessageBox.Show(observer.selectedFolder);
        }

        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            
            File.WriteAllText(saveNewFileName, newText.Text);
            
        }
    }
}
