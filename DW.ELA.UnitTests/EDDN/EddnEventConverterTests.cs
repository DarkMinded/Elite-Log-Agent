using System;
using System.Linq;
using DW.ELA.Interfaces;
using DW.ELA.Plugin.EDDN;
using DW.ELA.Plugin.EDDN.Model;
using DW.ELA.UnitTests.Utility;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace DW.ELA.UnitTests.EDDN
{
    [TestFixture]
    public class EddnEventConverterTests
    {
        //[Test]
        //[Parallelizable]
        //[TestCaseSource(typeof(TestEventSource), nameof(TestEventSource.CannedEvents))]
        //public void EddnConverterShouldConvertAndValidate(JournalEvent e)
        //{
        //    var recorderMock = GetRecorderMock();

        //    var eventConverter = new EddnEventConverter(recorderMock) { MaxAge = TimeSpan.FromDays(5000) };
        //    var result = eventConverter.Convert(e, TestCredentials.UserName).ToList();
        //    Assert.NotNull(result);
        //    CollectionAssert.AllItemsAreInstancesOfType(result, typeof(EddnEvent));
        //    foreach (var @event in result)
        //        Assert.IsTrue(validator.ValidateSchema(@event), "Event {0} should have validated", e.Event);
        //}

        [Test]
        [Parallelizable]
        public void ShouldEnrichJournalEventsWithStarSystemFields()
        {
            var recorderMock = GetRecorderMock();

            var eventConverter = new EddnEventConverter(recorderMock) { MaxAge = TimeSpan.FromDays(5000) };

            var convertedEvents = TestEventSource.CannedEvents
                .SelectMany(e => eventConverter.Convert(e, TestCredentials.UserName))
                .OfType<EddnJournalEvent>()
                .ToList();

            CollectionAssert.IsNotEmpty(convertedEvents);
            CollectionAssert.AllItemsAreNotNull(convertedEvents);

            foreach (var e in convertedEvents.OfType<EddnJournalEvent>())
            {
                ClassicAssert.NotNull(e.Message.Property("SystemAddress"));
                ClassicAssert.NotNull(e.Message.Property("StarPos"));
                ClassicAssert.NotNull(e.Message.Property("StarSystem"));
            }
        }

        [Test]
        [Parallelizable]
        public void JournalEventsShouldNotContainPersonalInfo()
        {
            var recorderMock = GetRecorderMock();

            var eventConverter = new EddnEventConverter(recorderMock) { MaxAge = TimeSpan.FromDays(5000) };

            var convertedEvents = TestEventSource.CannedEvents
                .SelectMany(e => eventConverter.Convert(e, TestCredentials.UserName))
                .OfType<EddnJournalEvent>()
                .ToList();

            CollectionAssert.IsNotEmpty(convertedEvents);
            CollectionAssert.AllItemsAreNotNull(convertedEvents);

            foreach (var e in convertedEvents.OfType<EddnJournalEvent>())
            {
                ClassicAssert.Null(e.Message.Property("ActiveFine"));
                ClassicAssert.Null(e.Message.Property("BoostUsed"));
                ClassicAssert.Null(e.Message.Property("CockpitBreach"));
                ClassicAssert.Null(e.Message.Property("FuelLevel"));
                ClassicAssert.Null(e.Message.Property("FuelUsed"));
                ClassicAssert.Null(e.Message.Property("JumpDist"));
                ClassicAssert.Null(e.Message.Property("Latitude"));
                ClassicAssert.Null(e.Message.Property("Longitude"));
                ClassicAssert.Null(e.Message.Property("Wanted"));
            }
        }

        private IPlayerStateHistoryRecorder GetRecorderMock()
        {
            var recorderMock = new Mock<IPlayerStateHistoryRecorder>();
            recorderMock.Setup(r => r.GetStarPos(It.IsAny<string>())).Returns(new[] { 0.0, 1.1, 2.2 });
            recorderMock.Setup(r => r.GetPlayerSystem(It.IsAny<DateTime>())).Returns("SomeSystem");
            recorderMock.Setup(r => r.GetSystemAddress(It.IsAny<string>())).Returns(123456789456);
            return recorderMock.Object;
        }
    }
}