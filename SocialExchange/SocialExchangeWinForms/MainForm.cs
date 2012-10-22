using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SocialExchange2;

namespace SocialExchangeWinForms
{
    public partial class SocialExchangeForm : Form
    {
        public static LogicEngine LogicEngine = new LogicEngine();

        public List<RecognitionUserControl> ImplicitRecognitionControls { get; protected set; }
        public List<RecognitionUserControl> ExplicitRecognitionControls { get; protected set; }

        public const string SCORE_TEXT = "You have {0} points.";

        public const string STATUS_TEXT__GIVE_1_OR_2_POINTS_TO_PLAYER2 = "Give 1 or 2 Points to Player2?";
        public const string STATUS_TEXT__SENDING_PLAYER2_X_POINTS = "Sending Player2 {0} points...";
        public const string STATUS_TEXT__WAITING_ON_PLAYER2_RESPONSE = "Waiting on Player2 response...";
        public const string STATUS_TEXT__PLAYER2_RESPONDED_WITH_X_POINTS = "Player2 responded with {0} points.";
        public const string STATUS_TEXT__YOUR_POINTS_X = "Your points {0}.";
        public const string STATUS_TEXT__ADVANCING_TO_NEXT_PLAYER = "Advancing to next player...";

        public SocialExchangeForm()
        {
            InitializeComponent();

            InitializeProgressBar();

            InitializeImplicitRecognitionControlList();
            InitializeExplicitRecognitionControlList();

            AdvanceToTrustExchangeTask();
        }

        private void InitializeProgressBar()
        {
            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = 100;
            ProgressBar.Step = 2;
            ProgressBar.Style = ProgressBarStyle.Blocks;
        }

        private void SocialExchangeForm_SizeChanged(object sender, EventArgs e)
        {
            ImageAndPointsButtonsPanel.CenterWithinParent();
        }

        private void InitializeImplicitRecognitionControlList()
        {
            ImplicitRecognitionControls = new List<RecognitionUserControl>();
            ImplicitRecognitionControls.AddRange(
                new RecognitionUserControl[] {
                    ImpRecogImgA1,
                    ImpRecogImgA2,
                    ImpRecogImgA3,
                    ImpRecogImgA4,
                    ImpRecogImgA5,
                    ImpRecogImgA6,
                    ImpRecogImgA7,
                    ImpRecogImgA8,
                    ImpRecogImgB1,
                    ImpRecogImgB2,
                    ImpRecogImgB3,
                    ImpRecogImgB4,
                    ImpRecogImgB5,
                    ImpRecogImgB6,
                    ImpRecogImgB7,
                    ImpRecogImgB8,
                    ImpRecogImgC1,
                    ImpRecogImgC2,
                    ImpRecogImgC3,
                    ImpRecogImgC4,
                    ImpRecogImgC5,
                    ImpRecogImgC6,
                    ImpRecogImgC7,
                    ImpRecogImgC8
                }
            );
        }

        private void InitializeExplicitRecognitionControlList()
        {
            ExplicitRecognitionControls = new List<RecognitionUserControl>();
            ExplicitRecognitionControls.AddRange(
                new RecognitionUserControl[] {
                    ExpRecogImgA1,
                    ExpRecogImgA2,
                    ExpRecogImgA3,
                    ExpRecogImgA4,
                    ExpRecogImgA5,
                    ExpRecogImgA6,
                    ExpRecogImgA7,
                    ExpRecogImgA8,
                    ExpRecogImgB1,
                    ExpRecogImgB2,
                    ExpRecogImgB3,
                    ExpRecogImgB4,
                    ExpRecogImgB5,
                    ExpRecogImgB6,
                    ExpRecogImgB7,
                    ExpRecogImgB8,
                    ExpRecogImgC1,
                    ExpRecogImgC2,
                    ExpRecogImgC3,
                    ExpRecogImgC4,
                    ExpRecogImgC5,
                    ExpRecogImgC6,
                    ExpRecogImgC7,
                    ExpRecogImgC8
                }
            );
        }

        private void AdvanceToTrustExchangeTask()
        {
            SetTrustExchangePictureBoxImageToCurrentRoundPersona();

            ShowTab(TrustExchangeTaskTab);
        }

        private void SetTrustExchangePictureBoxImageToCurrentRoundPersona()
        {
            TrustExchangePictureBox.Image = LogicEngine.TrustExchangeTask.CurrentRound.Persona.Image;
        }

        private void AdvanceToDemographicsTask()
        {
            throw new NotImplementedException();
        }

        private void AdvanceToImplicitRecognitionTask()
        {
            LogicEngine.InitializeImplicitRecognitionTask();

            throw new NotImplementedException();
        }

        private void AdvanceToExplicitRecognitionTask()
        {
            LogicEngine.InitializeExplicitRecognitionTask();

            throw new NotImplementedException();
        }

        private void Give1PointButton_Click(object sender, EventArgs e)
        {
            GivePoints(1);
        }

        private void Give2PointsButton_Click(object sender, EventArgs e)
        {
            GivePoints(2);
        }

        private void GivePoints(int points)
        {            
            SetTrustExchangeControlEnabledState(false);
            
            SetTextBoxText
            (
                TrustExchangeTaskStatusTextBox,
                string.Format(STATUS_TEXT__SENDING_PLAYER2_X_POINTS, points)
            );

            int priorScore = LogicEngine.TrustExchangeTask.PlayerScore;
            LogicEngine.TrustExchangeTask.ProcessPlayerInput(points);

            StepProgressBar(millisMin: 20, millisMax: 20);

            SetTextBoxText
            (
                TrustExchangeTaskStatusTextBox,
                STATUS_TEXT__WAITING_ON_PLAYER2_RESPONSE
            );

            StepProgressBar();
            
            SetTextBoxText
            (
                TrustExchangeTaskStatusTextBox,
                string.Format
                (
                    STATUS_TEXT__PLAYER2_RESPONDED_WITH_X_POINTS, 
                    LogicEngine.TrustExchangeTask.CurrentRound.MultipliedPersonaPointsOut
                )
            );

            SetTextBoxText
            (
                TrustExchangeTaskScoreTextBox,
                string.Format(SCORE_TEXT, LogicEngine.TrustExchangeTask.PlayerScore)
            );

            Thread.Sleep(3000);
            
            string contextualScoringMessage =
                priorScore > LogicEngine.TrustExchangeTask.PlayerScore ?
                string.Format("increased from {0} to {1}", priorScore, LogicEngine.TrustExchangeTask.PlayerScore) :
                    priorScore < LogicEngine.TrustExchangeTask.PlayerScore ?
                    string.Format("decreased from {0} to {1}", priorScore, LogicEngine.TrustExchangeTask.PlayerScore) :
                        string.Format("remains at {0}",LogicEngine.TrustExchangeTask.PlayerScore);

            SetTextBoxText
            (
                TrustExchangeTaskStatusTextBox,
                string.Format(STATUS_TEXT__YOUR_POINTS_X, contextualScoringMessage)
            );
            
            if
            (
                LogicEngine.TrustExchangeTask.CurrentRoundIndex < LogicEngine.TrustExchangeTask.Rounds.Count &&
                LogicEngine.TrustExchangeTask.CurrentRoundIndex != LogicEngine.TrustExchangeTask.Rounds.Count - 1
            )
            {
                SetTextBoxText
                (
                    TrustExchangeTaskStatusTextBox,
                    STATUS_TEXT__ADVANCING_TO_NEXT_PLAYER
                );
                Thread.Sleep(3000);

                LogicEngine.TrustExchangeTask.AdvanceToNextRound();
                SetTrustExchangePictureBoxImageToCurrentRoundPersona();
                SetTrustExchangeControlEnabledState(true);

                SetTextBoxText
                (
                    TrustExchangeTaskStatusTextBox,
                    STATUS_TEXT__GIVE_1_OR_2_POINTS_TO_PLAYER2
                );
            }
            else
            {
                MessageBox.Show("Advancing to Q&A Task.");
            }
        }
        
        public void SetTextBoxText(TextBox textBox, string text)
        {
            textBox.Text = text;
            textBox.Invalidate();
            textBox.Visible = true;
        }

        private void ShowTab(TabPage tab)
        {
            Tabs.TabPages.Cast<TabPage>().ToList().ForEach(t => t.Hide());
            tab.Show();
            Tabs.SelectedTab = tab;
        }

        private void SetTrustExchangeControlEnabledState(bool state)
        {
            Give1PointButton.Enabled = state;
            Give2PointsButton.Enabled = state;
            TrustExchangePictureBox.Enabled = state;
        }

        private void LoadTrustExchangeCurrentRoundPersonaImage(string p)
        {
            this.TrustExchangePictureBox.ImageLocation = LogicEngine.TrustExchangeTask.CurrentRound.Persona.Filename;
        }

        private void StepProgressBar(int fromPercentage = -1, int toPercentage = 100, int millisMin = 50, int millisMax = 200)
        {
            ProgressBar.Visible = true;

            ProgressBar.Value = fromPercentage == -1 ? ProgressBar.Value : fromPercentage;

            while (ProgressBar.Value < toPercentage)
            {
                Thread.Sleep(new Random().Next(millisMin, millisMax));
                ProgressBar.PerformStep();
            }

            ProgressBar.Visible = false;

            ProgressBar.Value = toPercentage == 100 ? 0 : fromPercentage;
        }
    }
}
