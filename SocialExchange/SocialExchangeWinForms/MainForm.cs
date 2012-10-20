using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SocialExchangeWinForms
{
    public partial class SocialExchangeForm : Form
    {
        public SocialExchangeForm()
        {
            InitializeComponent();
        }

        private void SocialExchangeForm_SizeChanged(object sender, EventArgs e)
        {
            ImageAndPointsButtonsPanel.CenterWithinParent();
        }
    }
}
