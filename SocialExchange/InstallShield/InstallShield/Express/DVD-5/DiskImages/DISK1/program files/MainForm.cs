using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
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

        private int Practice_PlayerScore = 0;
        private int Practice_CurrentRoundOrdinal = 1;
        private int Practice_MaximumRoundOrdinal = 3;
        private int Practice_CurrentRoundRawPointResponse = 0;

        private string adminPassword = "turnonadmin1027";

        public SocialExchangeForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormClosing += SocialExchangeForm_FormClosing;

            InitializeProgressBars();

            InitializeImplicitRecognitionControlList();
            InitializeExplicitRecognitionControlList();

            //MainFormExtensions.IsEmulatingTimeDelay = false;

            Practice_PlayerScore = LogicEngine.TrustExchangeTask.PlayerScore;
            UpdatePointsToolStripMenuItemWithPlayerScore();

            StartAtWelcomeTab();
        }

        void SocialExchangeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            PasswordForm closeForm = new PasswordForm();
            closeForm.ShowDialog();
            if(string.Equals(closeForm.PasswordTextBox.Text, adminPassword))
            {
                e.Cancel = false;
            }
            else
            {
                MessageBox.Show("Invalid password entered. Please try again. You will be returned to the application.", "Invalid Password", MessageBoxButtons.OK);
            }
        }

        private void StartAtWelcomeTab()
        {
            Application.DoEvents();
            Practice_PictureBox.Image = QuestionHeadBitmapMetaContainers.Red.Bitmap;
            ShowTab(WelcomeTab);
        }

        private void InitializeProgressBars()
        {
            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = 100;
            ProgressBar.Step = 2;
            ProgressBar.Style = ProgressBarStyle.Blocks;

            Practice_ProgressBar.Minimum = 0;
            Practice_ProgressBar.Maximum = 100;
            Practice_ProgressBar.Step = 2;
            Practice_ProgressBar.Style = ProgressBarStyle.Blocks;
        }

        private void SocialExchangeForm_SizeChanged(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized || this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }

            RecenterControls();
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

        private void AdvanceToPracticeTrustExchangeTask()
        {
            Practice_StatusButtonAsLabel.SetText(STATUS_TEXT__GIVE_1_OR_2_POINTS_TO_PLAYER2);
            Practice_ScoreButtonAsLabel.SetText(SCORE_TEXT, LogicEngine.TrustExchangeTask.PlayerScore);

            //SetTrustExchangePictureBoxImageToCurrentRoundPersona();

            ShowTab(Practice_TrustExchangeTaskTab);
        }

        private void AdvanceToTrustExchangeTask()
        {
            StatusButtonAsLabel.SetText(STATUS_TEXT__GIVE_1_OR_2_POINTS_TO_PLAYER2);
            ScoreButtonAsLabel.SetText(SCORE_TEXT, LogicEngine.TrustExchangeTask.PlayerScore);

            SetTrustExchangePictureBoxImageToCurrentRoundPersona();

            Practice_TrustExchangeTaskTab.RemoveFromAllowedTabs();
            ShowTab(TrustExchangeTaskTab);
        }

        private void SetTrustExchangePictureBoxImageToCurrentRoundPersona()
        {
            TrustExchangePictureBox.Image = LogicEngine.TrustExchangeTask.CurrentRound.Persona.Image;
        }

        private void AdvanceToDemographicsTask()
        {
            TrustExchangeTaskTab.RemoveFromAllowedTabs();
            ShowTab(DemographicsTaskTab);
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

            DemographicsTaskTab.RemoveFromAllowedTabs();
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

            ImpRecogTaskTab.RemoveFromAllowedTabs();
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

        private void Practice_GivePoints(int points)
        {
            Practice_SetTrustExchangeControlsEnabledStateTo(false);

            int scoreAtRoundStart = Practice_PlayerScore;

            Practice_NotifyPlayerOfSendingPoints(points);
            Practice_NotifyPlayerOfReductionOfScoreBySentPoints(points);

            Practice_CurrentRoundRawPointResponse = Practice_CurrentRoundOrdinal % 2 == 0 ? 0 : points;

            Practice_NotifyPlayerOfWaitingOnPersonaResponse();
            Practice_NotifyPlayerOfPersonaResponse();
            Practice_NotifyPlayerOfScoringThisRound(scoreAtRoundStart);

            if(Practice_CurrentRoundOrdinal < Practice_MaximumRoundOrdinal)
            {
                Practice_CurrentRoundOrdinal++;
                Practice_NextRoundButton.Visible = true;
            }
            else
            {
                MessageBox.Show(string.Format("You have finished the Practice {0}.", TrustExchangeTaskTab.Text), string.Format("Advancing to {0}", TrustExchangeTaskTab.Text), MessageBoxButtons.OK);
                Practice_TrustExchangeTaskTab.RemoveFromAllowedTabs();
                AdvanceToTrustExchangeTask();
            }
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

            UpdatePointsToolStripMenuItemWithPlayerScore();
            
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
                MessageBox.Show(string.Format("You have finished the {0}.", TrustExchangeTaskTab.Text), string.Format("Advancing to {0}", DemographicsTaskTab.Text), MessageBoxButtons.OK);
                AdvanceToDemographicsTask();
            }
        }

        private void UpdatePointsToolStripMenuItemWithPlayerScore()
        {
            PointsToolStripMenuItem.Text = LogicEngine.TrustExchangeTask.PlayerScore.ToString();
        }

        private void Practice_NotifyPlayerOfSendingPoints(int points)
        {
            Practice_ScoreButtonAsLabel.SetTextInvoke(SCORE_TEXT, Practice_PlayerScore);
            Practice_StatusButtonAsLabel.SetTextInvoke(STATUS_TEXT__SENDING_PLAYER2_X_POINTS, points);
            Practice_ProgressBar.StepInvoke(millisMin: 20, millisMax: 20, isEmulatingTimeDelay: !Admin_RealTimeResponse_ToolStripMenuItem.Checked);
        }

        private void NotifyPlayerOfSendingPoints(int points)
        {
            ScoreButtonAsLabel.SetTextInvoke(SCORE_TEXT, LogicEngine.TrustExchangeTask.PlayerScore);
            StatusButtonAsLabel.SetTextInvoke(STATUS_TEXT__SENDING_PLAYER2_X_POINTS, points);
            ProgressBar.StepInvoke(millisMin: 20, millisMax: 20, isEmulatingTimeDelay: !Admin_RealTimeResponse_ToolStripMenuItem.Checked);
        }

        private void Practice_NotifyPlayerOfReductionOfScoreBySentPoints(int points)
        {
            Practice_PlayerScore -= points;
            Practice_ScoreButtonAsLabel.SetTextInvoke(SCORE_TEXT, Practice_PlayerScore);
            Practice_StatusButtonAsLabel.SetTextInvoke(STATUS_TEXT__SCORE_REDUCED_TO_X, Practice_PlayerScore);
            1500.EmulateTimeDelay(!Admin_RealTimeResponse_ToolStripMenuItem.Checked);
        }

        private void NotifyPlayerOfReductionOfScoreBySentPoints(int points)
        {
            ScoreButtonAsLabel.SetTextInvoke(SCORE_TEXT, LogicEngine.TrustExchangeTask.PlayerScore - points);
            StatusButtonAsLabel.SetTextInvoke(STATUS_TEXT__SCORE_REDUCED_TO_X, LogicEngine.TrustExchangeTask.PlayerScore - points);
            1500.EmulateTimeDelay(!Admin_RealTimeResponse_ToolStripMenuItem.Checked);
        }

        private void Practice_NotifyPlayerOfWaitingOnPersonaResponse()
        {
            Practice_StatusButtonAsLabel.SetTextInvoke(STATUS_TEXT__WAITING_ON_PLAYER2_RESPONSE);
            Practice_ProgressBar.StepInvoke(millisMin: 10, millisMax: 350, isEmulatingTimeDelay: !Admin_RealTimeResponse_ToolStripMenuItem.Checked);
        }

        private void NotifyPlayerOfWaitingOnPersonaResponse()
        {
            StatusButtonAsLabel.SetTextInvoke(STATUS_TEXT__WAITING_ON_PLAYER2_RESPONSE);
            ProgressBar.StepInvoke(millisMin: 10, millisMax: 350, isEmulatingTimeDelay: !Admin_RealTimeResponse_ToolStripMenuItem.Checked);
        }

        private void Practice_NotifyPlayerOfPersonaResponse()
        {
            int pointsBack = LogicEngine.TrustExchangePointsMultiplier * Practice_CurrentRoundRawPointResponse;

            Practice_ScoreButtonAsLabel.SetTextInvoke(SCORE_TEXT, Practice_PlayerScore + pointsBack);
            Practice_StatusButtonAsLabel.SetTextInvoke(STATUS_TEXT__PLAYER2_RESPONDED_WITH_X_POINTS, pointsBack);
            2500.EmulateTimeDelay(!Admin_RealTimeResponse_ToolStripMenuItem.Checked);
        }

        private void NotifyPlayerOfPersonaResponse()
        {
            ScoreButtonAsLabel.SetTextInvoke(SCORE_TEXT, LogicEngine.TrustExchangeTask.PlayerScore);
            StatusButtonAsLabel.SetTextInvoke(STATUS_TEXT__PLAYER2_RESPONDED_WITH_X_POINTS, LogicEngine.TrustExchangeTask.CurrentRound.MultipliedPersonaPointsOut);
            2500.EmulateTimeDelay(!Admin_RealTimeResponse_ToolStripMenuItem.Checked);
        }

        private void Practice_NotifyPlayerOfScoringThisRound(int scoreAtRoundStart)
        {
            int pointsBack = LogicEngine.TrustExchangePointsMultiplier * Practice_CurrentRoundRawPointResponse;
            int finalScore = Practice_PlayerScore + pointsBack;

            int diff = Math.Abs(scoreAtRoundStart - finalScore);
            string contextualScoringMessage =
                string.Format
                (
                    "You {0} {1} point{2}.",
                    scoreAtRoundStart > finalScore ? "lost" : "gained",
                    diff,
                    diff > 1 ? "s" : ""
                );

            Practice_StatusButtonAsLabel.SetTextInvoke(contextualScoringMessage);
            2500.EmulateTimeDelay(!Admin_RealTimeResponse_ToolStripMenuItem.Checked);
        }

        private void NotifyPlayerOfScoringThisRound(int scoreAtRoundStart)
        {
            int finalScore = LogicEngine.TrustExchangeTask.PlayerScore;

            int diff = Math.Abs(scoreAtRoundStart - finalScore);
            string contextualScoringMessage =
                string.Format
                (
                    "You {0} {1} point{2}.",
                    scoreAtRoundStart > finalScore ? "lost" : "gained",
                    diff,
                    diff > 1 ? "s" : ""
                );

            StatusButtonAsLabel.SetTextInvoke(contextualScoringMessage);
            2500.EmulateTimeDelay(!Admin_RealTimeResponse_ToolStripMenuItem.Checked);
        }

        private void ShowTab(TabPage tab)
        {
            tab.AddToAllowedTabs();
            Tabs.TabPages.Cast<TabPage>().ToList().ForEach(t => t.Hide());
            tab.Show();
            Tabs.SelectedTab = tab;
            Application.DoEvents();
        }

        private void Practice_SetTrustExchangeControlsEnabledStateTo(bool state)
        {
            PracticeGive1PointButton.Enabled = state;
            PracticeGive2PointsButton.Enabled = state;
            Practice_PictureBox.Enabled = state;
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
            StatusButtonAsLabel.SetTextInvoke(STATUS_TEXT__GIVE_1_OR_2_POINTS_TO_PLAYER2);
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
                        control.Radio3.Enabled = false;

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
                        control.Radio3.Enabled = false;

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
            AdvanceToLikabilityRatingTask();
        }

        private void AdvanceToLikabilityRatingTask()
        {
            ReadyLikabilityUserControl();

            ExpRecogTaskTab.RemoveFromAllowedTabs();
            ShowTab(LikabilityRatingTab);
        }

        private void ReadyLikabilityUserControl()
        {
            LikabilityRatingUserControl.TrustExchangePictureBox.Image = LogicEngine.LikabilityRatingTask.GetCurrentRound().Persona.Image;
        }

        private void Tabs_Selecting(object sender, System.Windows.Forms.TabControlCancelEventArgs e)
        {
            if (!MainFormExtensions.AllowedTabs.Contains(e.TabPage) && !Admin_AllowTabSelection_ToolStripMenuItem.Checked)
            {
                if(MainFormExtensions.AllowedTabs.Count == 0)
                {
                    WelcomeTab.AddToAllowedTabs();
                    Practice_TrustExchangeTaskTab.AddToAllowedTabs();
                    //TrustExchangeTaskTab.AddToAllowedTabs();
                }

                Tabs.SelectedTab = MainFormExtensions.AllowedTabs.Last();
                MessageBox.Show
                (
                    "You cannot choose tabs directly." + Environment.NewLine + 
                    "You may proceed only through completing the on-screen instructions.", 
                    "Not Allowed", 
                    MessageBoxButtons.OK
                );
            }
        }

        private void Practice_Give1PointButton_Click(object sender, EventArgs e)
        {
            Practice_GivePoints(1);
        }

        private void Practice_Give2PointsButton_Click(object sender, EventArgs e)
        {
            Practice_GivePoints(2);
        }

        private void WelcomeTabStartPracticeButton_Click(object sender, EventArgs e)
        {
            AdvanceToPracticeTrustExchangeTask();
            WelcomeTab.RemoveFromAllowedTabs();
        }

        private void Practice_NextRoundButton_Click(object sender, EventArgs e)
        {
            if (Practice_CurrentRoundOrdinal == 2)
            {
                Practice_PictureBox.Image = QuestionHeadBitmapMetaContainers.Green.Bitmap;
            }
            else
                if (Practice_CurrentRoundOrdinal == 3)
                {
                    Practice_PictureBox.Image = QuestionHeadBitmapMetaContainers.Blue.Bitmap;
                }

            Practice_SetTrustExchangeControlsEnabledStateTo(true);
            Practice_StatusButtonAsLabel.SetTextInvoke(STATUS_TEXT__GIVE_1_OR_2_POINTS_TO_PLAYER2);
            Practice_NextRoundButton.Visible = false;
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DemographicsStartButton_Click(object sender, EventArgs e)
        {
            if
            (
                DemographicsTab_Q001_TextBox.Text == "" ||
                DemographicsTab_Q002_TextBox.Text == "" ||
                DemographicsTab_Q003_TextBox.Text == "" ||
                DemographicsTab_Q004_TextBox.Text == "" ||
                DemographicsTab_Q005_TextBox.Text == "" ||
                DemographicsTab_Q006_TextBox.Text == "" ||
                DemographicsTab_Q007_TextBox.Text == "" ||
                DemographicsTab_Q008_TextBox.Text == "" ||
                DemographicsTab_Q009_TextBox.Text == "" ||
                DemographicsTab_Q010_TextBox.Text == "" ||
                DemographicsTab_Q011_TextBox.Text == "" ||
                DemographicsTab_Q012_TextBox.Text == "" ||
                DemographicsTab_Q013_TextBox.Text == "" ||
                DemographicsTab_Q014_TextBox.Text == ""
                )
            {
                MessageBox.Show("Some answers are missing. Please answer all questions.", "Some Answers Missing", MessageBoxButtons.OK);
            }
            else
            {
                DemographicsTab_TaskContainer.Q001_Age_Label = DemographicsTab_Q001_Label.Text;
                DemographicsTab_TaskContainer.Q001_Age = DemographicsTab_Q001_TextBox.Text;

                DemographicsTab_TaskContainer.Q002_Gender_Label = DemographicsTab_Q002_Label.Text;
                DemographicsTab_TaskContainer.Q002_Gender = DemographicsTab_Q002_TextBox.Text;

                DemographicsTab_TaskContainer.Q003_CurrentCollegeLevel_Label = DemographicsTab_Q003_Label.Text;
                DemographicsTab_TaskContainer.Q003_CurrentCollegeLevel = DemographicsTab_Q003_TextBox.Text;

                DemographicsTab_TaskContainer.Q004_CollegeMajor_Label = DemographicsTab_Q004_Label.Text;
                DemographicsTab_TaskContainer.Q004_CollegeMajor = DemographicsTab_Q004_TextBox.Text;

                DemographicsTab_TaskContainer.Q005_StudentTimeStatus_Label = DemographicsTab_Q005_Label.Text;
                DemographicsTab_TaskContainer.Q005_StudentTimeStatus = DemographicsTab_Q005_TextBox.Text;

                DemographicsTab_TaskContainer.Q006_MaritalStatus_Label = DemographicsTab_Q006_Label.Text;
                DemographicsTab_TaskContainer.Q006_MaritalStatus = DemographicsTab_Q006_TextBox.Text;

                DemographicsTab_TaskContainer.Q007_WasSanDiegoCountyHighSchoolAttendant_Label = DemographicsTab_Q007_Label.Text;
                DemographicsTab_TaskContainer.Q007_WasSanDiegoCountyHighSchoolAttendant = DemographicsTab_Q007_TextBox.Text;

                DemographicsTab_TaskContainer.Q008_FreeTimeActivitiesHobbies_Label = DemographicsTab_Q008_Label.Text;
                DemographicsTab_TaskContainer.Q008_FreeTimeActivitiesHobbies = DemographicsTab_Q008_TextBox.Text;

                DemographicsTab_TaskContainer.Q009_FreeTimeActivitesHobbiesDislike_Label = DemographicsTab_Q009_Label.Text;
                DemographicsTab_TaskContainer.Q009_FreeTimeActivitesHobbiesDislike = DemographicsTab_Q009_TextBox.Text;

                DemographicsTab_TaskContainer.Q010_HasTraveledOutsideCalifornia_Label = DemographicsTab_Q010_Label.Text;
                DemographicsTab_TaskContainer.Q010_HasTraveledOutsideCalifornia = DemographicsTab_Q010_TextBox.Text;

                DemographicsTab_TaskContainer.Q011_ListUsStatesVisitedOutsideCalifornia_Label = DemographicsTab_Q011_Label.Text;
                DemographicsTab_TaskContainer.Q011_ListUsStatesVisitedOutsideCalifornia = DemographicsTab_Q011_TextBox.Text;

                DemographicsTab_TaskContainer.Q012_HasEverTraveledOutsideUnitedStatesBorder_Label = DemographicsTab_Q012_Label.Text;
                DemographicsTab_TaskContainer.Q012_HasEverTraveledOutsideUnitedStatesBorder = DemographicsTab_Q012_TextBox.Text;

                DemographicsTab_TaskContainer.Q013_ListCountriesVisitedOutsideUnitedStates_Label = DemographicsTab_Q013_Label.Text;
                DemographicsTab_TaskContainer.Q013_ListCountriesVisitedOutsideUnitedStates = DemographicsTab_Q013_TextBox.Text;

                DemographicsTab_TaskContainer.Q014_Ethnicity_Label = DemographicsTab_Q014_Label.Text;
                DemographicsTab_TaskContainer.Q014_Ethnicity = DemographicsTab_Q014_TextBox.Text;

                AdvanceToImplicitRecognitionTask();
            }
        }

        private void LikabilityRatingUserControl_Load(object sender, EventArgs e)
        {
            LikabilityRatingUserControl.SubmitButtonClickEventAction = LikabilityRatingUserControlSubmitButtonClickEventAction;
        }

        private void LikabilityRatingUserControlSubmitButtonClickEventAction(object obj, EventArgs e)
        {
            LogicEngine.LikabilityRatingTask.GetCurrentRound().LikabilityRatingIndex = LikabilityRatingUserControl.LikabilityRatingIndex;

            if(LogicEngine.LikabilityRatingTask.CanAdvanceToNextRound())
            {
                LogicEngine.LikabilityRatingTask.AdvanceToNextRound();
                ReadyLikabilityUserControl();
            }
            else
            {
                LikabilityRatingUserControl.Enabled = false;

                MessageBox.Show("You have completed all tasks. Please see your proctor.", "CONGRATULATIONS!", MessageBoxButtons.OK);

                LogicEngine.SaveResults(DemographicsTab_TaskContainer.ToString());
            }

        }

        private void Admin_Enabled_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.Admin_Enabled_ToolStripMenuItem.Checked == true)
            {
                this.Admin_Enabled_ToolStripMenuItem.Checked = false;

                Admin_AllowTabSelection_ToolStripMenuItem.Visible = false;
                Admin_AllowTabSelection_ToolStripMenuItem.Checked = false;

                Admin_RealTimeResponse_ToolStripMenuItem.Visible = false;
                Admin_RealTimeResponse_ToolStripMenuItem.Checked = false;
            }
            else
            {
                PasswordForm enableAdminForm = new PasswordForm();
                enableAdminForm.ShowDialog();

                if (string.Equals(enableAdminForm.PasswordTextBox.Text, adminPassword))
                {
                    this.Admin_Enabled_ToolStripMenuItem.Checked = true;

                    Admin_AllowTabSelection_ToolStripMenuItem.Visible = true;
                    Admin_AllowTabSelection_ToolStripMenuItem.Checked = true;

                    Admin_RealTimeResponse_ToolStripMenuItem.Visible = true;
                    Admin_RealTimeResponse_ToolStripMenuItem.Checked = true;
                }
            }

        }

        private void SocialExchangeForm_Resize(object sender, EventArgs e)
        {
            RecenterControls();
        }

        private void SocialExchangeForm_ResizeBegin(object sender, EventArgs e)
        {
            RecenterControls();
        }

        private void SocialExchangeForm_ResizeEnd(object sender, EventArgs e)
        {
            RecenterControls();
        }

        private void SocialExchangeForm_Shown(object sender, EventArgs e)
        {
            RecenterControls();
        }

        private void SocialExchangeForm_StyleChanged(object sender, EventArgs e)
        {
            RecenterControls();
        }

        private void RecenterControls()
        {
            ImageAndPointsButtonsPanel.CenterWithinParent();
            LikabilityRatingUserControlPanel.CenterWithinParent();
            Application.DoEvents();
        }
    }

    public static class DemographicsTab_TaskContainer
    {
        public static string Q001_Age_Label { get; set; }
        public static string Q001_Age { get; set; }
        public static string Q002_Gender_Label { get; set; }
        public static string Q002_Gender { get; set; }
        public static string Q003_CurrentCollegeLevel_Label { get; set; }
        public static string Q003_CurrentCollegeLevel { get; set; }
        public static string Q004_CollegeMajor_Label { get; set; }
        public static string Q004_CollegeMajor { get; set; }
        public static string Q005_StudentTimeStatus_Label { get; set; }
        public static string Q005_StudentTimeStatus { get; set; }
        public static string Q006_MaritalStatus_Label { get; set; }
        public static string Q006_MaritalStatus { get; set; }
        public static string Q007_WasSanDiegoCountyHighSchoolAttendant_Label { get; set; }
        public static string Q007_WasSanDiegoCountyHighSchoolAttendant { get; set; }
        public static string Q008_FreeTimeActivitiesHobbies_Label { get; set; }
        public static string Q008_FreeTimeActivitiesHobbies { get; set; }
        public static string Q009_FreeTimeActivitesHobbiesDislike_Label { get; set; }
        public static string Q009_FreeTimeActivitesHobbiesDislike { get; set; }
        public static string Q010_HasTraveledOutsideCalifornia_Label { get; set; }
        public static string Q010_HasTraveledOutsideCalifornia { get; set; }
        public static string Q011_ListUsStatesVisitedOutsideCalifornia_Label { get; set; }
        public static string Q011_ListUsStatesVisitedOutsideCalifornia { get; set; }
        public static string Q012_HasEverTraveledOutsideUnitedStatesBorder_Label { get; set; }
        public static string Q012_HasEverTraveledOutsideUnitedStatesBorder { get; set; }
        public static string Q013_ListCountriesVisitedOutsideUnitedStates_Label { get; set; }
        public static string Q013_ListCountriesVisitedOutsideUnitedStates { get; set; }
        public static string Q014_Ethnicity_Label { get; set; }
        public static string Q014_Ethnicity { get; set; }

        public new static string ToString()
        {
            return
                "<q>" + Q001_Age_Label + "</q>" + Environment.NewLine +
                "<a>" + Q001_Age + "</a>" + Environment.NewLine +
                "<q>" + Q002_Gender_Label + "</q>" + Environment.NewLine +
                "<a>" + Q002_Gender + "</a>" + Environment.NewLine +
                "<q>" + Q003_CurrentCollegeLevel_Label + "</q>" + Environment.NewLine +
                "<a>" + Q003_CurrentCollegeLevel + "</a>" + Environment.NewLine +
                "<q>" + Q004_CollegeMajor_Label + "</q>" + Environment.NewLine +
                "<a>" + Q004_CollegeMajor + "</a>" + Environment.NewLine +
                "<q>" + Q005_StudentTimeStatus_Label + "</q>" + Environment.NewLine +
                "<a>" + Q005_StudentTimeStatus + "</a>" + Environment.NewLine +
                "<q>" + Q006_MaritalStatus_Label + "</q>" + Environment.NewLine +
                "<a>" + Q006_MaritalStatus + "</a>" + Environment.NewLine +
                "<q>" + Q007_WasSanDiegoCountyHighSchoolAttendant_Label + "</q>" + Environment.NewLine +
                "<a>" + Q007_WasSanDiegoCountyHighSchoolAttendant + "</a>" + Environment.NewLine +
                "<q>" + Q008_FreeTimeActivitiesHobbies_Label + "</q>" + Environment.NewLine +
                "<a>" + Q008_FreeTimeActivitiesHobbies + "</a>" + Environment.NewLine +
                "<q>" + Q009_FreeTimeActivitesHobbiesDislike_Label + "</q>" + Environment.NewLine +
                "<a>" + Q009_FreeTimeActivitesHobbiesDislike + "</a>" + Environment.NewLine +
                "<q>" + Q010_HasTraveledOutsideCalifornia_Label + "</q>" + Environment.NewLine +
                "<a>" + Q010_HasTraveledOutsideCalifornia + "</a>" + Environment.NewLine +
                "<q>" + Q011_ListUsStatesVisitedOutsideCalifornia_Label + "</q>" + Environment.NewLine +
                "<a>" + Q011_ListUsStatesVisitedOutsideCalifornia + "</a>" + Environment.NewLine +
                "<q>" + Q012_HasEverTraveledOutsideUnitedStatesBorder_Label + "</q>" + Environment.NewLine +
                "<a>" + Q012_HasEverTraveledOutsideUnitedStatesBorder + "</a>" + Environment.NewLine +
                "<q>" + Q013_ListCountriesVisitedOutsideUnitedStates_Label + "</q>" + Environment.NewLine +
                "<a>" + Q013_ListCountriesVisitedOutsideUnitedStates + "</a>" + Environment.NewLine +
                "<q>" + Q014_Ethnicity_Label + "</q>" + Environment.NewLine + 
                "<a>" + Q014_Ethnicity + "</a>";
        }
    }

    public static class MainFormExtensions
    {
        public static List<TabPage> AllowedTabs = new List<TabPage>();

        public static void EmulateTimeDelay(this int @int, bool isEmulatingTimeDelay = true)
        {
            if (isEmulatingTimeDelay)
            {
                Thread.Sleep(@int);
                Application.DoEvents();
            }
        }

        public static void SetTextInvoke(this Button button, string text, params object[] @params)
        {
            button.Invoke(new Action(() => button.SetText(text, @params)));
        }

        public static void SetTextInvoke(this TextBox textBox, string text, params object[] @params)
        {
            textBox.Invoke(new Action(() => textBox.SetText(text, @params)));
        }

        public static void StepInvoke(this ProgressBar progressBar, int fromPercentage = -1, int toPercentage = 100, int millisMin = 50, int millisMax = 200, bool isEmulatingTimeDelay = true)
        {
            progressBar.Invoke(new Action(() => progressBar.Step(fromPercentage, toPercentage, millisMin, millisMax, isEmulatingTimeDelay)));
        }

        public static void SetText(this TextBox textBox, string text, params object[] @params)
        {
            textBox.Text = string.Format(text, @params ?? new object[] { text });
            Application.DoEvents();
        }

        public static void SetText(this Button button, string text, params object[] @params)
        {
            button.Text = string.Format(text, @params ?? new object[] { text });
            Application.DoEvents();
        }

        public static void Step(this ProgressBar progressBar, int fromPercentage = -1, int toPercentage = 100, int millisMin = 50, int millisMax = 200, bool isEmulatingTimeDelay = true)
        {
            if(isEmulatingTimeDelay)
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

                Application.DoEvents();
            }
        }

        public static void AddToAllowedTabs(this TabPage tab)
        {
            if(!AllowedTabs.Contains(tab))
            {
                AllowedTabs.Add(tab);
            }
        }

        public static void RemoveFromAllowedTabs(this TabPage tab)
        {
            if (AllowedTabs.Contains(tab))
            {
                AllowedTabs.Remove(tab);
            }
        }
    }
}
