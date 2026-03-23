using Newtonsoft.Json;
using System.Collections.Generic;

namespace DW.ELA.Plugin.Inara
{
    public class InaraSettings
    {
        /// <summary>
        /// Dictionary of CMDR name => API key pairs
        /// </summary>
        [JsonProperty("apiKeys")]
        public IDictionary<string, string> ApiKeys { get; internal set; } = new Dictionary<string, string>();
    }
}