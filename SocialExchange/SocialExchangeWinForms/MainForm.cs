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

        public List<RecognitionUserControl> ImpRecogControls { get; protected set; }
        public List<RecognitionUserControl> ExpRecogControls { get; protected set; }

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
            ImpRecogControls = new List<RecognitionUserControl>();
            ImpRecogControls.AddRange(
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
            ExpRecogControls = new List<RecognitionUserControl>();
            ExpRecogControls.AddRange(
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
            ShowTab(DemographicsTaskTab);

            MessageBox.Show("Insert demographics task here.", "Advancing to Next Task", MessageBoxButtons.OK);

            AdvanceToImplicitRecognitionTask();
        }

        private void AdvanceToImplicitRecognitionTask()
        {
            LogicEngine.InitializeImplicitRecognitionTask();

            ImpRecogControls
                .ForEach
                (
                    control => 
                    {
                        control.RecognitionRound = LogicEngine.ImplicitRecognitionTask.Rounds[ImpRecogControls.IndexOf(control)];

                        control.PictureBox.Image = control.RecognitionRound.Persona.Image;

                        control.Radio1.Visible = false;
                        control.Radio2.Text = "Choose";
                        control.Radio3.Text = "Discard";

                        control.Radio2.CheckedChanged += 
                            (sender, e) => 
                            {
                                int countChecked =
                                    ImpRecogControls.Count<RecognitionUserControl>(ruc => ruc.Radio2.Checked == true);
                                if (countChecked < 12)
                                {
                                    ImpRecogSubmitButton.Enabled = false;
                                }
                                else
                                    if (countChecked == 12)
                                    {
                                        ImpRecogSubmitButton.Enabled = true;
                                    }
                                    else
                                        if(countChecked > 12)
                                        {
                                            ImpRecogSubmitButton.Enabled = false;

                                            MessageBox.Show
                                            (
                                                string.Format("You have chosen {0} too many. ", countChecked - 12) +
                                                "Please discard those you would NOT want to play again until you reach twelve chosen players.",
                                                string.Format("Please Discard {0}", countChecked - 12),
                                                MessageBoxButtons.OK
                                            );
                                        }
                            };
                    }
                );

            ShowTab(ImpRecogTaskTab);
        }

        private void AdvanceToExplicitRecognitionTask()
        {
            LogicEngine.InitializeExplicitRecognitionTask();

            ExpRecogControls
                .ForEach
                (
                    control =>
                    {
                        control.RecognitionRound = LogicEngine.ExplicitRecognitionTask.Rounds[ExpRecogControls.IndexOf(control)];

                        control.PictureBox.Image = control.RecognitionRound.Persona.Image;

                        control.Radio1.Text = "YES, gave me points back";
                        control.Radio2.Text = "NO, gave no points back";
                        control.Radio3.Text = "I did not play this person";

                        EventHandler validationEventHandler =
                            (sender, e) =>
                            {
                                int countRadio1Checked =
                                    ExpRecogControls.Count<RecognitionUserControl>(ruc => ruc.Radio1.Checked == true);

                                int countRadio2Checked =
                                    ExpRecogControls.Count<RecognitionUserControl>(ruc => ruc.Radio2.Checked == true);

                                int countRadio3Checked =
                                    ExpRecogControls.Count<RecognitionUserControl>(ruc => ruc.Radio2.Checked == true);

                                if (countRadio1Checked < 6 && countRadio2Checked < 6)
                                {
                                    ExpRecogSubmitButton.Enabled = false;
                                }
                                else
                                    if (countRadio1Checked == 6 && countRadio2Checked == 6)
                                    {
                                        ExpRecogSubmitButton.Enabled = true;
                                    }
                                    else
                                        if (countRadio1Checked > 6 || countRadio2Checked > 6 || countRadio3Checked > 12)
                                        {
                                            ExpRecogSubmitButton.Enabled = false;

                                            MessageBox.Show
                                            (
                                                string.Format("There are incorrect amounts of certain selections. ", countRadio1Checked - 6) +
                                                "Please choose exactly 6 players that gave you points, exactly 6 that did not, and exactly 12 that you did not play.",
                                                string.Format("Choose Exactly 6 And 6", countRadio1Checked - 6),
                                                MessageBoxButtons.OK
                                            );
                                        }
                            };


                        control.Radio1.CheckedChanged += validationEventHandler;
                        control.Radio2.CheckedChanged += validationEventHandler;
                        control.Radio3.CheckedChanged += validationEventHandler;
                    }
                );

            ShowTab(ExpRecogTaskTab);
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
            SetTrustExchangeControlsEnabledStateTo(false);

            int scoreAtRoundStart = LogicEngine.TrustExchangeTask.PlayerScore;
            
            NotifyPlayerOfSendingPoints(points);
            NotifyPlayerOfReductionOfScoreBySentPoints(points);

            LogicEngine.TrustExchangeTask.ProcessPlayerInput(points);

            NotifyPlayerOfWaitingOnPersonaResponse();
            NotifyPlayerOfPersonaResponse();
            NotifyPlayerOfScoringThisRound(scoreAtRoundStart);
            
            if
            (
                LogicEngine.TrustExchangeTask.CurrentRoundIndex < LogicEngine.TrustExchangeTask.Rounds.Count &&
                LogicEngine.TrustExchangeTask.CurrentRoundIndex != LogicEngine.TrustExchangeTask.Rounds.Count - 1
            )
            {
                NextRoundButton.Visible = true;
            }
            else
            {
                MessageBox.Show("You have finished the Trust Game.", "Advancing to Q&A Task", MessageBoxButtons.OK);
                AdvanceToDemographicsTask();
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
            1500.EmulateTimeDelay();
        }

        private void NotifyPlayerOfWaitingOnPersonaResponse()
        {
            Status.SetTextInvoke(STATUS_TEXT__WAITING_ON_PLAYER2_RESPONSE);
            ProgressBar.StepInvoke(millisMin: 10, millisMax: 500);
        }

        private void NotifyPlayerOfPersonaResponse()
        {
            Score.SetTextInvoke(SCORE_TEXT, LogicEngine.TrustExchangeTask.PlayerScore);
            Status.SetTextInvoke(STATUS_TEXT__PLAYER2_RESPONDED_WITH_X_POINTS, LogicEngine.TrustExchangeTask.CurrentRound.MultipliedPersonaPointsOut);
            2500.EmulateTimeDelay();
        }

        private void NotifyPlayerOfScoringThisRound(int scoreAtRoundStart)
        {
            int finalScore = LogicEngine.TrustExchangeTask.PlayerScore;

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
            2500.EmulateTimeDelay();
        }

        private void ShowTab(TabPage tab)
        {
            Tabs.TabPages.Cast<TabPage>().ToList().ForEach(t => t.Hide());
            tab.Show();
            Tabs.SelectedTab = tab;
            Application.DoEvents();
        }

        private void SetTrustExchangeControlsEnabledStateTo(bool state)
        {
            Give1PointButton.Enabled = state;
            Give2PointsButton.Enabled = state;
            TrustExchangePictureBox.Enabled = state;
        }

        private void LoadTrustExchangeCurrentRoundPersonaImage(string p)
        {
            this.TrustExchangePictureBox.ImageLocation = LogicEngine.TrustExchangeTask.CurrentRound.Persona.FileName;
        }

        private void NextRoundButton_Click(object sender, EventArgs e)
        {
            LogicEngine.TrustExchangeTask.AdvanceToNextRound();
            SetTrustExchangePictureBoxImageToCurrentRoundPersona();
            SetTrustExchangeControlsEnabledStateTo(true);
            Status.SetTextInvoke(STATUS_TEXT__GIVE_1_OR_2_POINTS_TO_PLAYER2);
            NextRoundButton.Visible = false;
        }

        private void ImpRecogSubmitButton_Click(object sender, EventArgs e)
        {
            ImpRecogControls
                .ForEach
                (
                    control =>
                    {
                        control.Radio1.Enabled = false;
                        control.Radio2.Enabled = false;
                        control.Radio2.Enabled = false;

                        control.RecognitionRound.ProcessPlayerInput
                        (
                            control.Radio2.Checked ? 
                            PlayerInputClassifications.ImplicitlyChosePersona :
                            PlayerInputClassifications.ImplicitlyDiscardedPersona 
                        );
                    }
                );

            ImpRecogSubmitButton.Enabled = false;

            AdvanceToExplicitRecognitionTask();
        }

        private void ExpRecogSubmitButton_Click(object sender, EventArgs e)
        {
            ExpRecogControls
                .ForEach
                (
                    control =>
                    {
                        control.Radio1.Enabled = false;
                        control.Radio2.Enabled = false;
                        control.Radio2.Enabled = false;

                        control.RecognitionRound.ProcessPlayerInput
                        (
                            control.Radio1.Checked ?
                            PlayerInputClassifications.ExplicitlyChoseCooperatorPersona :
                            control.Radio2.Checked ?
                            PlayerInputClassifications.ExplicitlyChoseDefectorPersona :
                            PlayerInputClassifications.ExplicitlyDiscardedPersona

                        );
                    }
                );

            ExpRecogSubmitButton.Enabled = false;

            MessageBox.Show("You have completed all tasks. Please discuss your results with your proctor.", "CONGRATULATIONS!", MessageBoxButtons.OK); 
            
            LogicEngine.SaveResults
            (
                string.Join
                (
                    Environment.NewLine,
                    new object[] 
                    {
                        RoundExtensions.GetCommaDelimitedColumnNames(),
                        string.Join(Environment.NewLine, LogicEngine.TrustExchangeTask.Rounds.Select<TrustExchangeRound, object>(r => r.ToString()).ToArray()),
                        string.Join(Environment.NewLine, LogicEngine.ImplicitRecognitionTask.Rounds.Select<RecognitionRound, object>(r => r.ToString()).ToArray()),
                        string.Join(Environment.NewLine, LogicEngine.ExplicitRecognitionTask.Rounds.Select<RecognitionRound, object>(r => r.ToString()).ToArray())
                    }
                )
            );
        }
    }

    public static class SocialExchangeExtensions
    {
        public static bool IsEmulatingTimeDelay = true;

        public static void EmulateTimeDelay(this int @int)
        {
            if (IsEmulatingTimeDelay)
            {
                Thread.Sleep(@int);
                Application.DoEvents();
            }
        }

        public static void SetTextInvoke(this TextBox textBox, string text, params object[] @params)
        {
            textBox.Invoke(new Action(() => textBox.SetText(text, @params)));
            //Application.DoEvents();
            //Thread.Sleep(10);
        }

        public static void StepInvoke(this ProgressBar progressBar, int fromPercentage = -1, int toPercentage = 100, int millisMin = 50, int millisMax = 200)
        {
            progressBar.Invoke(new Action(() => progressBar.Step(fromPercentage, toPercentage, millisMin, millisMax)));
        }

        public static void SetText(this TextBox textBox, string text, params object[] @params)
        {
            textBox.Text = string.Format(text, @params ?? new object[] { text });
            Application.DoEvents();
        }


        public static void Step(this ProgressBar progressBar, int fromPercentage = -1, int toPercentage = 100, int millisMin = 50, int millisMax = 200)
        {
            if(IsEmulatingTimeDelay)
            {
                progressBar.Visible = true;

                progressBar.Value = fromPercentage == -1 ? progressBar.Value : fromPercentage;

                while (progressBar.Value < toPercentage)
                {
                    Thread.Sleep(new Random().Next(millisMin, millisMax));
                    Application.DoEvents();
                    progressBar.PerformStep();
                }

                progressBar.Visible = false;

                progressBar.Value = toPercentage == 100 ? 0 : fromPercentage;

                //Thread.Sleep(10);
                Application.DoEvents();
            }
        }
    }
}
