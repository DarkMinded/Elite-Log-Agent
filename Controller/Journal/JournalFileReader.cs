using DW.ELA.Interfaces;
using DW.ELA.Utility.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DW.ELA.Controller.Journal
{
    public class JournalFileReader
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        private static readonly HashSet<string> SkippedEvents = new(new[] { "Outfitting", "Shipyard", "Market" });

        /// <summary>
        /// Reads the given Journal file from specified position and generates the events
        /// </summary>
        /// <param name="textReader">Stream reader for input data</param>
        /// <returns>Sequence of events read from input</returns>
        public IEnumerable<JournalEvent> ReadEventsFromStream(TextReader textReader)
        {
            if (textReader == null)
                throw new ArgumentNullException(nameof(textReader));
            using var jsonReader = new JsonTextReader(textReader) { SupportMultipleContent = true, CloseInput = false };
            while (jsonReader.Read())
            {
                var @object = Converter.Serializer.Deserialize<JObject>(jsonReader);
                JournalEvent @event = null;
                try
                {
                    @event = JournalEventConverter.Convert(@object);
                }
                catch (Exception e)
                {
                    Log.Error(e, "Error deserializing event from journal");
                }
                if (@event != null)
                    yield return @event;
            }
        }

        public JournalEvent ReadFileEvent(string file)
        {
            Log.Debug("Reading file event");
            using var fileReader = OpenForSharedRead(file);
            using var textReader = new StreamReader(fileReader);
            return ReadEventsFromStream(textReader).SingleOrDefault();
        }

        public IEnumerable<JournalEvent> ReadEventsFromJournal(string journalFile)
        {
            using var fileReader = OpenForSharedRead(journalFile);
            using var textReader = new StreamReader(fileReader);
            foreach (var @event in ReadEventsFromStream(textReader))
                yield return @event;
        }

        private Stream OpenForSharedRead(string file) => new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
    }
}
