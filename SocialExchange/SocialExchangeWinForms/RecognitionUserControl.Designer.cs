﻿namespace SocialExchangeWinForms
{
    partial class RecognitionUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Image = new System.Windows.Forms.PictureBox();
            this.Panel = new System.Windows.Forms.Panel();
            this.RadioButton3 = new System.Windows.Forms.RadioButton();
            this.RadioButton2 = new System.Windows.Forms.RadioButton();
            this.RadioButton1 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.Image)).BeginInit();
            this.Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Image
            // 
            this.Image.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Image.Location = new System.Drawing.Point(2, 2);
            this.Image.Name = "Image";
            this.Image.Size = new System.Drawing.Size(338, 268);
            this.Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Image.TabIndex = 0;
            this.Image.TabStop = false;
            // 
            // Panel
            // 
            this.Panel.AutoSize = true;
            this.Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel.Controls.Add(this.RadioButton3);
            this.Panel.Controls.Add(this.RadioButton2);
            this.Panel.Controls.Add(this.RadioButton1);
            this.Panel.Controls.Add(this.Image);
            this.Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel.Location = new System.Drawing.Point(3, 3);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(344, 344);
            this.Panel.TabIndex = 1;
            // 
            // RadioButton3
            // 
            this.RadioButton3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.RadioButton3.AutoSize = true;
            this.RadioButton3.Location = new System.Drawing.Point(2, 322);
            this.RadioButton3.Name = "RadioButton3";
            this.RadioButton3.Size = new System.Drawing.Size(85, 17);
            this.RadioButton3.TabIndex = 3;
            this.RadioButton3.TabStop = true;
            this.RadioButton3.Text = "radioButton3";
            this.RadioButton3.UseVisualStyleBackColor = true;
            // 
            // RadioButton2
            // 
            this.RadioButton2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.RadioButton2.AutoSize = true;
            this.RadioButton2.Location = new System.Drawing.Point(2, 299);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(85, 17);
            this.RadioButton2.TabIndex = 2;
            this.RadioButton2.TabStop = true;
            this.RadioButton2.Text = "radioButton2";
            this.RadioButton2.UseVisualStyleBackColor = true;
            // 
            // RadioButton1
            // 
            this.RadioButton1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.RadioButton1.AutoSize = true;
            this.RadioButton1.Location = new System.Drawing.Point(3, 276);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(85, 17);
            this.RadioButton1.TabIndex = 1;
            this.RadioButton1.TabStop = true;
            this.RadioButton1.Text = "radioButton1";
            this.RadioButton1.UseVisualStyleBackColor = true;
            // 
            // RecognitionUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Panel);
            this.Name = "RecognitionUserControl";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(350, 350);
            this.ClientSizeChanged += new System.EventHandler(this.RecognitionUserControl_ClientSizeChanged);
            this.SizeChanged += new System.EventHandler(this.RecognitionUserControl_SizeChanged);
            this.Resize += new System.EventHandler(this.RecognitionUserControl_Resize);
            this.StyleChanged += new System.EventHandler(this.RecognitionUserControl_StyleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.Image)).EndInit();
            this.Panel.ResumeLayout(false);
            this.Panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Image;
        private System.Windows.Forms.Panel Panel;
        private System.Windows.Forms.RadioButton RadioButton3;
        private System.Windows.Forms.RadioButton RadioButton2;
        private System.Windows.Forms.RadioButton RadioButton1;
    }
}
