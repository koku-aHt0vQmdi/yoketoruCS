namespace yoketoruCS
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Timer1 = new System.Windows.Forms.Timer(components);
            labelTitle = new Label();
            buttonState = new Button();
            labelGameover = new Label();
            buttonTitle = new Button();
            labelClear = new Label();
            labelTimer = new Label();
            labelScore = new Label();
            SuspendLayout();
            // 
            // Timer1
            // 
            Timer1.Enabled = true;
            Timer1.Interval = 50;
            Timer1.Tick += timer1_Tick;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Yu Gothic UI", 75F, FontStyle.Regular, GraphicsUnit.Point);
            labelTitle.Location = new Point(12, 9);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(357, 133);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "よけとる";
            // 
            // buttonState
            // 
            buttonState.Font = new Font("Yu Gothic UI", 25F, FontStyle.Regular, GraphicsUnit.Point);
            buttonState.Location = new Point(12, 145);
            buttonState.Name = "buttonState";
            buttonState.Size = new Size(250, 100);
            buttonState.TabIndex = 1;
            buttonState.Text = "ゲームスタート";
            buttonState.UseVisualStyleBackColor = true;
            buttonState.Click += buttonState_Click;
            // 
            // labelGameover
            // 
            labelGameover.AutoSize = true;
            labelGameover.Font = new Font("Yu Gothic UI", 50F, FontStyle.Regular, GraphicsUnit.Point);
            labelGameover.Location = new Point(375, 44);
            labelGameover.Name = "labelGameover";
            labelGameover.Size = new Size(377, 89);
            labelGameover.TabIndex = 2;
            labelGameover.Text = "ゲームオーバー";
            labelGameover.Visible = false;
            // 
            // buttonTitle
            // 
            buttonTitle.Font = new Font("Yu Gothic UI", 25F, FontStyle.Regular, GraphicsUnit.Point);
            buttonTitle.Location = new Point(375, 145);
            buttonTitle.Name = "buttonTitle";
            buttonTitle.Size = new Size(250, 100);
            buttonTitle.TabIndex = 3;
            buttonTitle.Text = "タイトルへ";
            buttonTitle.UseVisualStyleBackColor = true;
            buttonTitle.Visible = false;
            buttonTitle.Click += buttonTitle_Click;
            // 
            // labelClear
            // 
            labelClear.AutoSize = true;
            labelClear.Font = new Font("Yu Gothic UI", 50F, FontStyle.Regular, GraphicsUnit.Point);
            labelClear.Location = new Point(375, 248);
            labelClear.Name = "labelClear";
            labelClear.Size = new Size(335, 89);
            labelClear.TabIndex = 4;
            labelClear.Text = "ゲームクリア";
            labelClear.Visible = false;
            // 
            // labelTimer
            // 
            labelTimer.AutoSize = true;
            labelTimer.Font = new Font("Yu Gothic UI", 25F, FontStyle.Regular, GraphicsUnit.Point);
            labelTimer.Location = new Point(12, 248);
            labelTimer.Name = "labelTimer";
            labelTimer.Size = new Size(74, 46);
            labelTimer.TabIndex = 5;
            labelTimer.Text = "000";
            labelTimer.Visible = false;
            // 
            // labelScore
            // 
            labelScore.AutoSize = true;
            labelScore.Font = new Font("Yu Gothic UI", 25F, FontStyle.Regular, GraphicsUnit.Point);
            labelScore.Location = new Point(12, 294);
            labelScore.Name = "labelScore";
            labelScore.Size = new Size(110, 46);
            labelScore.TabIndex = 6;
            labelScore.Text = "00000";
            labelScore.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(944, 501);
            Controls.Add(labelScore);
            Controls.Add(labelTimer);
            Controls.Add(labelClear);
            Controls.Add(buttonTitle);
            Controls.Add(labelGameover);
            Controls.Add(buttonState);
            Controls.Add(labelTitle);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer Timer1;
        private Label labelTitle;
        private Button buttonState;
        private Label labelGameover;
        private Button buttonTitle;
        private Label labelClear;
        private Label labelTimer;
        private Label labelScore;
    }
}