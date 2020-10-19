using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public partial class Service1 : ServiceBase
    {
        private string fwPath = "C:\\Users\\Faba Binch\\Documents\\Cours M2 IPI\\C#\\Test_Watcher";
        private string fileName = "Log.csv";
        private string filePath = "";

        private string date;
        private DateTime dateTime;

        private StreamWriter sw;
        

        public Service1()
        {
            InitializeComponent();
            filePath = fwPath + "\\" + fileName;

            date = dateTime.ToString("dd/MM/yyyy");

            if (!File.Exists(filePath))
            {
                string entete = "Date;Category;Code;Message";
                writeInFile(entete);
               
            }

            fileSystemWatcher1.Path = fwPath;
            fileSystemWatcher1.Renamed += FileSystemWatcher1_Renamed;
            fileSystemWatcher1.Changed += FileSystemWatcher1_Changed;
            fileSystemWatcher1.Created += FileSystemWatcher1_Created;
            fileSystemWatcher1.Deleted += FileSystemWatcher1_Deleted;
        }

        private void FileSystemWatcher1_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {
           string textDeleted = date + ";Deleted;111;Fichier " + e.Name + " supprimé";
            writeInFile(textDeleted);
        }

        private void FileSystemWatcher1_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            string textCreated = date + ";Created;222;Fichier " + e.Name + "créé";
            writeInFile(textCreated);
        }

        private void FileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            string textChanged = date + ";Changed;333;Fichier " + e.Name +  " modifié (" + e.ChangeType + ")";
            writeInFile(textChanged);
        }

        private void FileSystemWatcher1_Renamed(object sender, System.IO.RenamedEventArgs e)
        {
            string textRenamed = date + ";Renamed;444;Fichier " + e.OldName + " renomé " + e.Name;
            writeInFile(textRenamed);
        }

        private void writeInFile(string text)
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine(text);
               
            }
        }
    }
}
