using DW.ELA.Utility.Json;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DW.ELA.Plugin.Inara.Model
{
    internal struct ApiInputBatch
    {
        [JsonProperty("header")]
        public Header Header;

        [JsonProperty("events")]
        public IList<ApiInputEvent> Events;

        public override string ToString() => Serialize.ToJson(this);
    }
}
