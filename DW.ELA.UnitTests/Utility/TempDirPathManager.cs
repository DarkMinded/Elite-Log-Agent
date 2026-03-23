using DW.ELA.Interfaces;
using System.IO;

namespace DW.ELA.UnitTests.Utility
{
    internal class TempDirPathManager : IPathManager
    {
        public string SettingsDirectory => Path.GetTempPath();

        public string LogDirectory => Path.GetTempPath();
    }
}
