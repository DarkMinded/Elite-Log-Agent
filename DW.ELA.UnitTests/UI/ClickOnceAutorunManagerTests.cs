using EliteLogAgent.Autorun;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace DW.ELA.UnitTests.UI
{
    public class ClickOnceAutorunManagerTests
    {
        [Test]
        public void ShouldEnableThenDisableClickOnceAutorun()
        {
            var manager = new ClickOnceAutorunManager
            {
                AutorunEnabled = true
            };
            ClassicAssert.IsTrue(manager.AutorunEnabled);
            manager.AutorunEnabled = false;
            ClassicAssert.IsFalse(manager.AutorunEnabled);
        }

        [Test]
        public void ShouldEnableThenDisablePortableAutorun()
        {
            var manager = new PortableAutorunManager
            {
                AutorunEnabled = true
            };
            ClassicAssert.IsTrue(manager.AutorunEnabled);
            manager.AutorunEnabled = false;
            ClassicAssert.IsFalse(manager.AutorunEnabled);
        }
    }
}
