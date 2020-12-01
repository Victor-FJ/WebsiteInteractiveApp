using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WebsiteInteractiveApp
{
    public class FileManager
    {
        public readonly string FilePath;

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                WriteAddress();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                WriteAddress();
            }
        }

        public FileManager(string filePath)
        {
            FilePath = filePath;
            ReadAddress();
        }

        private void ReadAddress()
        {
            StreamReader sr = new StreamReader(FilePath);
            _userName = sr.ReadLine();
            _password = sr.ReadLine();
            sr.Close();
        }

        private void WriteAddress()
        {
            StreamWriter sw = new StreamWriter(FilePath);
            sw.WriteLine(_userName);
            sw.WriteLine(_password);
            sw.Close();
        }
    }
}
