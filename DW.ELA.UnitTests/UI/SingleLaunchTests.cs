using System.Diagnostics;
using System.Linq;
using EliteLogAgent;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace DW.ELA.UnitTests.UI
{
    public class SingleLaunchTests
    {
        [Test]
        public void AppShouldNotBeLaunched() => ClassicAssert.AreEqual(Process.GetProcessesByName("EliteLogAgent").Any(),
                                                                SingleLaunch.IsRunning,
                                                                "SingleLaunch.IsRunning should match actually running process");
    }
}
