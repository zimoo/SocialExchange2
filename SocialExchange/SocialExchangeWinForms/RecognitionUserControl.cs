using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SocialExchangeWinForms
{
    public partial class RecognitionUserControl : UserControl
    {
        public RecognitionUserControl()
        {
            InitializeComponent();
        }

        private void RecognitionUserControl_Resize(object sender, EventArgs e)
        {
            RadioButton1.CenterWithinParent();
            RadioButton2.CenterWithinParent();
            RadioButton3.CenterWithinParent();
        }
    }
}
