using DW.ELA.Utility.App;
using EliteLogAgent.Properties;
using System;
using System.Windows.Forms;

namespace EliteLogAgent
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            Load += AboutForm_Load;
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            logoBox.Image = Resources.EliteIcon.ToBitmap();
            titleLabel.Text = AppInfo.Name;
        }

        private void About_Load(object sender, EventArgs e)
        {
        }
    }
}
