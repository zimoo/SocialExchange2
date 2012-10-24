using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SocialExchange2;

namespace SocialExchangeWinForms
{
    public partial class RecognitionUserControl : UserControl
    {
        public PictureBox PictureBox { get { return this.Image; } }
        public RadioButton Radio1 { get { return this.RadioButton1; } }
        public RadioButton Radio2 { get { return this.RadioButton2; } }
        public RadioButton Radio3 { get { return this.RadioButton3; } }

        public RecognitionRound RecognitionRound { get; set; }

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
