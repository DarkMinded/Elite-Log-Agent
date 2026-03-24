using DW.ELA.Controller;
using DW.ELA.Interfaces;
using DW.ELA.Utility;
using System;
using System.Collections.Generic;

namespace DW.ELA.Plugin.EDSM
{
    public class EdsmSettingsPageProvider : ISettingsPageProvider
    {
        private readonly IReadOnlyDictionary<string, string> initialApiKeys;
        private readonly GlobalSettings settings;
        private readonly IApiKeyValidator validator;
        private readonly string pluginId;
        public EdsmSettingsPageProvider(
            string pluginId,
            IReadOnlyDictionary<string, string> initialApiKeys,
            GlobalSettings settings,
            IApiKeyValidator validator)
        {
            this.pluginId = pluginId;
            this.initialApiKeys = initialApiKeys;
            this.settings = settings;
            this.validator = validator;
        }

        public string SettingsPageName => "EDSM";

        public object GetSettingsData(GlobalSettings settings) => new EdsmSettingsData
        {
            ApiKeys = initialApiKeys
        };

        public void ApplySettings(GlobalSettings settings, object data)
        {
            if (data is EdsmSettingsData edsmData)
                new PluginSettingsFacade<EdsmSettings>(pluginId).SetPluginSettings(
                    settings, new EdsmSettings { ApiKeys = (IDictionary<string, string>)edsmData.ApiKeys });
        }
    }

    public class EdsmSettingsData
    {
        public IReadOnlyDictionary<string, string> ApiKeys { get; set; } = new Dictionary<string, string>();
    }
}
