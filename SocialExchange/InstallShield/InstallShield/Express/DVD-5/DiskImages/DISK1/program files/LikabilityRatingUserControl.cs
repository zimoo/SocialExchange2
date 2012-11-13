using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InvestmentGame
{
    public partial class LikabilityRatingUserControl : UserControl
    {
        public int LikabilityRatingIndex { get; protected set; }

        public Action<object, EventArgs> SubmitButtonClickEventAction { get; set; }

        public List<RadioButton> RadioButtons { get; protected set; }

        public LikabilityRatingUserControl()
        {
            InitializeComponent();
            InitializeRadioButtonsList();
        }

        private void InitializeRadioButtonsList()
        {
            RadioButtons = new List<RadioButton>();
            RadioButtons.Add(LikabilityRadioButton1);
            RadioButtons.Add(LikabilityRadioButton2);
            RadioButtons.Add(LikabilityRadioButton3);
            RadioButtons.Add(LikabilityRadioButton4);
            RadioButtons.Add(LikabilityRadioButton5);
        }

        private void ReadySubmission(int likabilityRatingIndex)
        {
            LikabilityRatingIndex = likabilityRatingIndex;
            SubmitButton.Enabled = true;
        }

        private void LikabilityRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ReadySubmission(1);
        }

        private void LikabilityRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ReadySubmission(2);
        }

        private void LikabilityRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            ReadySubmission(3);
        }

        private void LikabilityRadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            ReadySubmission(4);
        }

        private void LikabilityRadioButton5_CheckedChanged(object sender, EventArgs e)
        {
            ReadySubmission(5);
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            SubmitButtonClickEventAction(sender, e);
            ResetControl();
        }

        private void ResetControl()
        {
            SubmitButton.Enabled = false;
            RadioButtons.ForEach(rb => rb.Checked = false);
        }

    }
}
