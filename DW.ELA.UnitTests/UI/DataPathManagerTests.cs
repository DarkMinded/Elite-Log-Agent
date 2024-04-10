using EliteLogAgent.Deployment;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace DW.ELA.UnitTests.UI
{
    public class DataPathManagerTests
    {
        [Test]
        public void ShouldProvideDirectories()
        {
            var manager = new DataPathManager();
            ClassicAssert.IsNotEmpty(manager.LogDirectory);
            ClassicAssert.IsNotEmpty(manager.SettingsDirectory);
        }
    }
}
