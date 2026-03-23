using NLog;
using System;
using System.Diagnostics;

namespace DW.ELA.Utility.Logging
{
    public class LoggingTimer : IDisposable
    {
        private static readonly Logger DefaultLogger = LogManager.GetCurrentClassLogger();
        private static readonly LogLevel DefaultLogLevel = LogLevel.Info;

        private readonly Stopwatch stopwatch = new();
        private readonly Logger logger;
        private readonly LogLevel logLevel;
        private readonly string context;

        public LoggingTimer(string context) : this(context, LogLevel.Info, DefaultLogger) { }

        public LoggingTimer(string context, LogLevel logLevel) : this(context, logLevel, DefaultLogger) { }

        public LoggingTimer(string context, Logger logger) : this(context, DefaultLogLevel, logger) { }

        public LoggingTimer(string context, LogLevel logLevel, Logger logger)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.logger = logger;
            this.logLevel = logLevel ?? throw new ArgumentNullException(nameof(logLevel));
        }

        public TimeSpan Elapsed => stopwatch.Elapsed;

        public void Dispose()
        {
            stopwatch.Stop();

            logger.WithProperty("duration", stopwatch.ElapsedMilliseconds).Log(logLevel, context);


        }
    }
}
