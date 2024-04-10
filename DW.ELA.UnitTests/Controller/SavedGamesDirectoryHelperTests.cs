using DW.ELA.Controller;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace DW.ELA.UnitTests.Controller
{
    public class SavedGamesDirectoryHelperTests
    {
        [Test]
        public void ShouldFindSavesDirectory() => ClassicAssert.IsNotEmpty(new SavedGamesDirectoryHelper().Directory);
    }
}
