using DW.ELA.Interfaces;
using DW.ELA.Utility.App;
using EliteLogAgent.Controls;
using EliteLogAgent.Properties;
using EliteLogAgent.Settings;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace EliteLogAgent
{
    public partial class SettingsForm : Form
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        private GlobalSettings currentSettings;

        private readonly IDictionary<string, ISettingsControl> settingsControls = new Dictionary<string, ISettingsControl>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GlobalSettings GlobalSettings { get; internal set; }

        public SettingsForm()
        {
            InitializeComponent();
            Icon = Resources.EliteIcon;
            string versionLabel = "Version: " + AppInfo.Version;
            Text += ". " + versionLabel;
            Load += SettingsForm_Load;
        }

        // These have to be properties because Form designer does not allow arguments in constructor
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal ISettingsProvider Provider { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal IPlayerStateHistoryRecorder PlayerStateRecorder { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal List<IPlugin> Plugins { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal IAutorunManager AutorunManager { get; set; }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            currentSettings = Provider.Settings.Clone();

            // Will check passed properties for null here as they are not yet set in constructor
            var generalSettingsControl = new GeneralSettingsControl()
            {
                PlayerStateRecorder = PlayerStateRecorder ?? throw new ArgumentNullException("MessageBroker"),
                GlobalSettings = currentSettings ?? throw new ArgumentNullException("Settings"),
                Plugins = Plugins ?? throw new ArgumentNullException("Plugins"),
                AutorunManager = AutorunManager ?? throw new ArgumentNullException("AutorunManager"),
                Dock = DockStyle.Fill
            };
            settingsControls.Add("General", generalSettingsControl);
            settingsCategoriesListView.Items.Add("General");

            //foreach (var plugin in Plugins)
            //{
            //    try
            //    {
            //        var control = plugin.GetSettingsPageProvider(currentSettings);
            //        if (control == null)
            //            continue;
            //        control.Dock = DockStyle.Fill;
            //        control.PerformLayout();
            //        settingsControls.Add(plugin.PluginName, control);
            //    }
            //    catch (Exception ex)
            //    {
            //        Log.Error(ex, "Error loading plugin {0}", plugin.PluginId);
            //    }
            //}

            foreach (string category in settingsControls.Keys.OrderBy(x => x))
            {
                if (category != "General")
                    settingsCategoriesListView.Items.Add(category);
            }

            settingsCategoriesListView.SelectedIndices.Add(0);
        }

        private void SettingsCategoriesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (settingsCategoriesListView.SelectedIndices.Count > 0)
            {
                int selectedIndex = settingsCategoriesListView.SelectedIndices.Cast<int>().Single();
                settingsControlContainer.Controls.OfType<ISettingsControl>().SingleOrDefault()?.SaveSettings();
                settingsControlContainer.Controls.Clear();
                settingsControlContainer.Controls.Add((Control)settingsControls[settingsCategoriesListView.Items[selectedIndex].Text]);
                settingsControlContainer.PerformLayout();
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            ApplySettings();
            Close();
        }

        private void ApplyButton_Click(object sender, EventArgs e) => ApplySettings();

        private void ApplySettings()
        {
            settingsControlContainer.Controls.OfType<ISettingsControl>().SingleOrDefault()?.SaveSettings();
            Provider.Save (currentSettings);
        }

        private void CancelButton_Click(object sender, EventArgs e) => Close();
    }
}
