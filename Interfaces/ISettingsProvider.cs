using System;

namespace DW.ELA.Interfaces
{
    public interface ISettingsProvider
    {
        event EventHandler SettingsChanged;

        GlobalSettings Settings { get; set; }
    }
}