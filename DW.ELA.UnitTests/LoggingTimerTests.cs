using DW.ELA.Utility.Logging;
using NUnit.Framework;
using System;

namespace DW.ELA.UnitTests
{
    public class LoggingTimerTests
    {
        [Test]
        public void ShouldShowReasonableTime()
        {
            using var timer = new LoggingTimer("Test");
            Assert.That(timer.Elapsed, Is.GreaterThanOrEqualTo(TimeSpan.Zero));
            Assert.That(timer.Elapsed, Is.LessThan(TimeSpan.FromSeconds(5)));
        }
    }
}
