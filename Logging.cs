using System;
using System.IO;

namespace MailMaster
{
    public class Logging
    {
        private string _logDirectory;
        private string _logFullPath;
        private string _date = DateTime.Now.ToString("yyyy-MM-dd");
        private string _time = DateTime.Now.ToString("HH:mm:ss");
        public string LogDirectory
        {
            get => this._logDirectory;
            set => this._logDirectory = value;
        }

        public void InitLogging()
        {
            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(this._logDirectory))
                {
                    Console.WriteLine("That path exists already.");
                    return;
                }
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(this._logDirectory);
                Console.WriteLine("The directory was created successfully at {0}.",
                    Directory.GetCreationTime(this._logDirectory));
            }
            catch (Exception e)
            {
                Console.WriteLine("Fatal Error: The process failed: {0}", e);
                Environment.Exit(1);
            }
            
        }

        private void FileWriter(string message)
        {
            GlobalAppInfo appInfo = new GlobalAppInfo();
            _logFullPath = String.Join(this._logDirectory, $"{appInfo.AppName}-{_date}.log");
            if(!File.Exists(this._logFullPath))
            {
                File.Create(this._logFullPath).Dispose();

                using( TextWriter tw = new StreamWriter(this._logFullPath))
                {
                    tw.WriteLine(message);
                }

            }    
            else if (File.Exists(this._logFullPath))
            {
                using(TextWriter tw = new StreamWriter(this._logFullPath, append: true))
                {
                    tw.WriteLine(message);
                }
            }
        }
        
        public void LogInfo(string message)
        {
            var messageBuffer = $"\n{_time} {_date} - [INFO]: {message}";
            Console.WriteLine(messageBuffer);
            FileWriter(messageBuffer);
        }
        public void LogWarning(string message)
        {
            var messageBuffer = $"\n{_time} {_date} - [WARNING]: {message}";
            Console.WriteLine(messageBuffer);
            FileWriter(messageBuffer);
        }
        public void LogError(string message)
        {
            var messageBuffer = $"\n{_time} {_date} - [ERROR]: {message}";
            Console.WriteLine(messageBuffer);
            FileWriter(messageBuffer);
        }
    }
}