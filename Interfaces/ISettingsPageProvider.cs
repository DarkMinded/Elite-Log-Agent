using System;

namespace DW.ELA.Interfaces
{
    public interface ISettingsPageProvider
    {
        string SettingsPageName { get; }
        object GetSettingsData(GlobalSettings settings);
        void ApplySettings(GlobalSettings settings, object data);
    }
}