namespace MinesweeperGUI
{
    partial class HighScore
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HighScore));
            label1 = new Label();
            scoresListBox = new ListBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(314, 36);
            label1.Name = "label1";
            label1.Size = new Size(146, 32);
            label1.TabIndex = 0;
            label1.Text = "High Scores:";
            // 
            // scoresListBox
            // 
            scoresListBox.FormattingEnabled = true;
            scoresListBox.ItemHeight = 32;
            scoresListBox.Location = new Point(144, 91);
            scoresListBox.Name = "scoresListBox";
            scoresListBox.Size = new Size(515, 260);
            scoresListBox.TabIndex = 1;
            // 
            // HighScore
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 230, 242);
            ClientSize = new Size(800, 450);
            Controls.Add(scoresListBox);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "HighScore";
            Text = "High Score";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ListBox scoresListBox;
    }
}