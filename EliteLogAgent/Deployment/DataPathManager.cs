using DW.ELA.Interfaces;
using DW.ELA.Utility.App;
using System;
using System.IO;

namespace EliteLogAgent.Deployment
{
    public class DataPathManager : IPathManager
    {
        public string SettingsDirectory => AppInfo.IsNetworkDeployed ? AppDataDirectory : LocalDirectory;

        public string LogDirectory => AppInfo.IsNetworkDeployed ? Path.Combine(AppDataDirectory, "Log") : Path.Combine(LocalDirectory, "Log");

        private string AppDataDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EliteLogAgent");

        private string LocalDirectory => Path.GetDirectoryName(new Uri(typeof(Program).Assembly.Location).LocalPath);
    }
}
