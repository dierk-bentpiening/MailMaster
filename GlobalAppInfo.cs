using System;

namespace MailMaster
{
    public class GlobalMessages
    {
        public string AppName = " MailMaster";
        public double Version = 1.0;
        public string DistributorName = "Dierk-Bent Piening";
        public int StartDevYear = 1996;
        public string LicenseName = "MIT License";
        private string _argStructure;
        public string ArgStructure
        {
            get => this._argStructure;
            set => this._argStructure = value;
        }
        public int ActualYear => DateTime.Now.Year;
        public int ActualMonth => DateTime.Now.Month;
        public int ActualDay => DateTime.Now.Day;

        public string GetAppInfoMessag()
        {
            return $"{AppName} Version {Version}\n(C) {StartDevYear} - {ActualYear} {DistributorName}\n All rights reserved.\n Licensed under {LicenseName}";
        }
        
        public string GetArgMissmatchErrorMessag(int counter, int counterExpected)
        {
            return $"{AppName} Error: You entered {counter} Arguments, but {counterExpected} are required\n";
        }
    }
}