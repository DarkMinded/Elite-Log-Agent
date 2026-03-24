using System;

namespace DW.ELA.Interfaces
{
    public interface ISettingsProvider
    {
        GlobalSettings Settings { get; }
        void Save(GlobalSettings settings);
        event EventHandler SettingsChanged;
    }
}