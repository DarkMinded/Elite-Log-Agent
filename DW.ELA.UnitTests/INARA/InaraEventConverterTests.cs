using DW.ELA.Controller;
using DW.ELA.Interfaces;
using DW.ELA.Plugin.Inara;
using DW.ELA.Plugin.Inara.Model;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace DW.ELA.UnitTests.INARA
{
    [TestFixture]
    [Parallelizable]
    public class InaraEventConverterTests
    {
        private readonly IPlayerStateHistoryRecorder stateRecorder = new PlayerStateRecorder();
        private readonly InaraEventConverter eventConverter;

        public InaraEventConverterTests()
        {
            eventConverter = new InaraEventConverter(stateRecorder);
        }

        [Test]
        [Parallelizable]
        [TestCaseSource(typeof(TestEventSource), nameof(TestEventSource.CannedEvents))]
        public void InaraConverterShouldNotFailOnEvents(JournalEvent e)
        {
            var result = eventConverter.Convert(e);
            ClassicAssert.NotNull(result);
            CollectionAssert.AllItemsAreInstancesOfType(result, typeof(ApiInputEvent));
        }
    }
}
