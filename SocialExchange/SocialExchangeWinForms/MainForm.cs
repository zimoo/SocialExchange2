using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        public const string STATUS_TEXT__GIVE_1_OR_2_POINTS_TO_PLAYER2 = "Give 1 or 2 Points?";
        public const string STATUS_TEXT__SENDING_PLAYER2_X_POINTS = "Sending {0} points...";
        public const string STATUS_TEXT__WAITING_ON_PLAYER2_RESPONSE = "Waiting on response...";
        public const string STATUS_TEXT__PLAYER2_RESPONDED_WITH_X_POINTS = "You received {0} points back.";
        public const string STATUS_TEXT__SCORE_REDUCED_TO_X = "Score reduced to {0}.";
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
            Status.SetText(STATUS_TEXT__GIVE_1_OR_2_POINTS_TO_PLAYER2);
            Score.SetText(SCORE_TEXT, LogicEngine.TrustExchangeTask.PlayerScore);

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

            int scoreAtRoundStart = LogicEngine.TrustExchangeTask.PlayerScore;
            
            NotifyPlayerOfSendingPoints(points);
            NotifyPlayerOfReductionOfScoreBySentPoints(points);

            LogicEngine.TrustExchangeTask.ProcessPlayerInput(points);

            NotifyPlayerOfWaitingOnPersonaResponse();
            NotifyPlayerOfPersonaResponse();
            NotifyPlayerOfScoringThisRound(scoreAtRoundStart);// - points);
            
            if
            (
                LogicEngine.TrustExchangeTask.CurrentRoundIndex < LogicEngine.TrustExchangeTask.Rounds.Count &&
                LogicEngine.TrustExchangeTask.CurrentRoundIndex != LogicEngine.TrustExchangeTask.Rounds.Count - 1
            )
            {
                //MessageBox.Show("Click OK when you are ready to advance to the next player.", "Advancing to next player...", MessageBoxButtons.OK);

                //StatusTextBox.SetTextAsync(STATUS_TEXT__ADVANCING_TO_NEXT_PLAYER);
                //Thread.Sleep(2000);

                NextRoundButton.Visible = true;
            }
            else
            {
                MessageBox.Show("Advancing to Q&A Task.");
            }
        }

        private void NotifyPlayerOfSendingPoints(int points)
        {
            Score.SetTextInvoke(SCORE_TEXT, LogicEngine.TrustExchangeTask.PlayerScore);
            Status.SetTextInvoke(STATUS_TEXT__SENDING_PLAYER2_X_POINTS, points);
            ProgressBar.StepInvoke(millisMin: 20, millisMax: 20);
        }

        private void NotifyPlayerOfReductionOfScoreBySentPoints(int points)
        {
            Score.SetTextInvoke(SCORE_TEXT, LogicEngine.TrustExchangeTask.PlayerScore - points);
            Status.SetTextInvoke(STATUS_TEXT__SCORE_REDUCED_TO_X, LogicEngine.TrustExchangeTask.PlayerScore - points);
            Thread.Sleep(1500);
        }

        private void NotifyPlayerOfWaitingOnPersonaResponse()
        {
            Status.SetTextInvoke(STATUS_TEXT__WAITING_ON_PLAYER2_RESPONSE);
            //ProgressBar.StepProgressBar(millisMin: 100, millisMax: 800);
            ProgressBar.StepInvoke(millisMin: 20, millisMax: 20);
        }

        private void NotifyPlayerOfPersonaResponse()
        {
            Score.SetTextInvoke(SCORE_TEXT, LogicEngine.TrustExchangeTask.PlayerScore);
            Status.SetTextInvoke(STATUS_TEXT__PLAYER2_RESPONDED_WITH_X_POINTS, LogicEngine.TrustExchangeTask.CurrentRound.MultipliedPersonaPointsOut);
            Thread.Sleep(2500);
        }

        private void NotifyPlayerOfScoringThisRound(int scoreAtRoundStart)//, int points, int scoreAfterSendingPoints)
        {
            int finalScore = LogicEngine.TrustExchangeTask.PlayerScore;

            //string contextualScoringMessage =
            //    scoreAtRoundStart > finalScore ?
            //    string.Format("decreased from {0} to {1}", scoreAtRoundStart, finalScore) :
            //    //scoreAtRoundStart < finalScore ?
            //        string.Format("increased from {0} to {1}", scoreAtRoundStart, finalScore);//:
            ////string.Format("remain at {0}", finalScore);

            int diff = Math.Abs(scoreAtRoundStart - finalScore);
            string contextualScoringMessage =
                string.Format
                (
                    "You {0} {1} point{2}!",
                    scoreAtRoundStart > finalScore ? "lost" : "gained",
                    diff,
                    diff > 1 ? "s" : ""
                );

            Status.SetTextInvoke(contextualScoringMessage);
            Thread.Sleep(2500);
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

        private void NextRoundButton_Click(object sender, EventArgs e)
        {
            LogicEngine.TrustExchangeTask.AdvanceToNextRound();
            SetTrustExchangePictureBoxImageToCurrentRoundPersona();
            SetTrustExchangeControlEnabledState(true);
            Status.SetTextInvoke(STATUS_TEXT__GIVE_1_OR_2_POINTS_TO_PLAYER2);
            NextRoundButton.Visible = false;
        }
    }

    public static class Extensions
    {
        //public async static void SetTextAsync(this TextBox textBox, string text, params object[] @params)
        //{
        //    await System.Threading.Tasks.Task.Run(() => textBox.SetText(text, @params));
        //}

        public static void SetTextInvoke(this TextBox textBox, string text, params object[] @params)
        {
            //BackgroundWorker worker = new BackgroundWorker();
            //worker.DoWork +=
            //    (sender, e) =>
            //    {
            textBox.Invoke(new Action(() => textBox.SetText(text, @params)));
            Application.DoEvents();
            Thread.Sleep(10);
            //    };

            //worker.RunWorkerAsync(new object[] { textBox, text }.Concat(@params));
        }

        public static void StepInvoke(this ProgressBar progressBar, int fromPercentage = -1, int toPercentage = 100, int millisMin = 50, int millisMax = 200)
        {
            progressBar.Invoke(new Action(() => progressBar.Step(fromPercentage, toPercentage, millisMin, millisMax)));
            Application.DoEvents();
        }

        public static void SetText(this TextBox textBox, string text, params object[] @params)
        {
            textBox.Text = string.Format(text, @params ?? new object[] { text });
        }


        public static void Step(this ProgressBar progressBar, int fromPercentage = -1, int toPercentage = 100, int millisMin = 50, int millisMax = 200)
        {
            progressBar.Visible = true;

            progressBar.Value = fromPercentage == -1 ? progressBar.Value : fromPercentage;

            while (progressBar.Value < toPercentage)
            {
                Thread.Sleep(new Random().Next(millisMin, millisMax));
                progressBar.PerformStep();
            }

            progressBar.Visible = false;

            progressBar.Value = toPercentage == 100 ? 0 : fromPercentage;

            Thread.Sleep(10);
        }
    }
}
