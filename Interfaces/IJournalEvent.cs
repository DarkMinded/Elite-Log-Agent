using Newtonsoft.Json.Linq;
using System;

namespace DW.ELA.Interfaces
{
    public interface IJournalEvent
    {
        string Event { get; }
        JObject Raw { get; }
        DateTime Timestamp { get; }
    }
}