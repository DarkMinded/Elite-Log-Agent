using DW.ELA.Interfaces;
using System;
using static System.IO.Directory;
using static System.IO.Path;

namespace DW.ELA.UnitTests.Utility
{
    public class TestDirectoryProvider : ILogDirectoryNameProvider
    {
        public TestDirectoryProvider()
        {
            Directory = Combine(GetTempPath(), "ELA-TEST-" + Guid.NewGuid().ToString());
            CreateDirectory(Directory);
        }

        public string Directory { get; }
    }
}
