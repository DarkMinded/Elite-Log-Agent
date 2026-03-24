using DW.ELA.Controller;
using DW.ELA.Interfaces;
using DW.ELA.Utility;
using System;
using System.Collections.Generic;

namespace DW.ELA.Plugin.Inara
{
    public class InaraSettingsPageProvider : ISettingsPageProvider
    {
        private readonly IReadOnlyDictionary<string, string> initialApiKeys;
        private readonly GlobalSettings settings;
        private readonly IApiKeyValidator validator;
        private readonly string pluginId;

        public InaraSettingsPageProvider(
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

        public string SettingsPageName => "INARA";

        public object GetSettingsData(GlobalSettings settings) => new InaraSettingsData
        {
            ApiKeys = initialApiKeys
        };

        public void ApplySettings(GlobalSettings settings, object data)
        {
            if (data is InaraSettingsData inaraData)
                new PluginSettingsFacade<InaraSettings>(pluginId).SetPluginSettings(
                    settings, new InaraSettings { ApiKeys = (IDictionary<string, string>)inaraData.ApiKeys });
        }
    }

    public class InaraSettingsData
    {
        public IReadOnlyDictionary<string, string> ApiKeys { get; set; } = new Dictionary<string, string>();
    }
}
