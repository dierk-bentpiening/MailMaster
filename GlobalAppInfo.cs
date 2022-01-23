using System;

namespace MailMaster
{
    public class GlobalAppInfo
    {
        public string AppName = "MailMaster";
        public double Version = 1.1;
        public string DistributorName = "Dierk-Bent Piening";
        public int StartDevYear = 2021;
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

        public string GetAppInfoMessage()
        {
            return $"{AppName} Version {Version}\n(C) {StartDevYear} - {ActualYear} {DistributorName}\nAll rights reserved.\nLicensed under {LicenseName}";
        }
        
        public string GetArgMissmatchErrorMessage(int counter, int counterExpected)
        {
            return $"{AppName} Error: You entered {counter} Arguments, but {counterExpected} are required\nArguments: \n{this._argStructure}";
        }
    }
}