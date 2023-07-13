using Microsoft.VisualBasic.ApplicationServices;
using System.Data;
using System.Runtime.InteropServices;

namespace yoketoruCS
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        static int PlayerMax => 1;
        static int ItemMax => 3;
        static int ObstacleMax => 3;
        static int PlayerIndex => 0;
        static int ObstacleIndex => PlayerIndex + PlayerMax;
        static int ItemIndex => ObstacleIndex + ObstacleMax;
        static int LabelMax => ItemIndex + ItemMax;
        Label[] chrLabels = new Label[LabelMax];
        int itemCount;

        int[] vx = new int[LabelMax];
        int[] vy = new int[LabelMax];

        static Random random = new Random();

        static int SpeedMax => 10;
        static int PointRate => 100;

        int score;
        int ScoreMax => 99999;
        int highScore = 100;
        int timer;
        static int StateTimer => 200;

        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < LabelMax; i++)
            {
                chrLabels[i] = new Label();
                chrLabels[i].AutoSize = true;
                chrLabels[i].Top = i * 24;
                Controls.Add(chrLabels[i]);
                if (i < ObstacleIndex)
                {
                    chrLabels[i].Text = "(・ω・)";
                }
                else if (i < ItemIndex)
                {
                    chrLabels[i].Text = "◆";
                }
                else
                {
                    chrLabels[i].Text = "★";
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        enum State
        {
            None = -1,
            Title,
            Game,
            Gameover,
            GameClear
        }

        State nextState = State.Title;
        State currentState = State.None;

        private void timer1_Tick(object sender, EventArgs e)
        {
            ChangeState();
            UpdateState();
        }

        void ChangeState()
        {
            switch (nextState)
            {
                case State.Title:
                    labelGameover.Visible = false;
                    labelClear.Visible = false;
                    buttonTitle.Visible = false;
                    labelTitle.Visible = true;
                    labelTitle.Left = (ClientSize.Width / 2) - (labelTitle.Width / 2);
                    labelTitle.Top = (ClientSize.Height / 3) - (labelTitle.Height / 2);
                    buttonState.Visible = true;
                    buttonState.Left = (ClientSize.Width / 2) - (buttonState.Width / 2);
                    buttonState.Top = ((ClientSize.Height / 3) - (buttonState.Height / 2)) + (ClientSize.Height / 3);
                    break;

                case State.Game:
                    currentState = State.Game;
                    labelTitle.Visible = false;
                    buttonState.Visible = false;
                    labelTimer.Visible = true;
                    labelTimer.Left = ClientSize.Width - (labelTimer.Width + 10);
                    labelTimer.Top = ClientSize.Height - (labelTimer.Height + 10);
                    score = 0;
                    timer = StateTimer;
                    for (int i = ObstacleIndex; i < vx.Length; i++)
                    {
                        vx[i] = random.Next(-SpeedMax, SpeedMax + 1);
                        vy[i] = random.Next(-SpeedMax, SpeedMax + 1);
                    }
                    RandomObstacleAndItemPosition();
                    break;

                case State.Gameover:
                    labelGameover.Visible = true;
                    labelGameover.Left = (ClientSize.Width / 2) - (labelGameover.Width / 2);
                    labelGameover.Top = (ClientSize.Height / 3) - (labelGameover.Height / 2);
                    buttonTitle.Visible = true;
                    buttonTitle.Left = (ClientSize.Width / 2) - (buttonTitle.Width / 2);
                    buttonTitle.Top = ((ClientSize.Height / 3) - (buttonTitle.Height / 2)) + (ClientSize.Height / 3);
                    break;

                case State.GameClear:
                    labelClear.Visible = true;
                    labelClear.Left = (ClientSize.Width / 2) - (labelClear.Width / 2);
                    labelClear.Top = (ClientSize.Height / 3) - (labelClear.Height / 2);
                    buttonTitle.Visible = true;
                    buttonTitle.Left = (ClientSize.Width / 2) - (buttonTitle.Width / 2);
                    buttonTitle.Top = ((ClientSize.Height / 3) - (buttonTitle.Height / 2)) + (ClientSize.Height / 3);
                    break;

            }
        }

        private void buttonState_Click(object sender, EventArgs e)
        {
            nextState = State.Game;
        }

        void UpdateState()
        {
            switch (currentState)
            {
                case State.Game:
                    UpdateGame();
                    break;

                case 0:
                    break;
            }
        }

        void UpdateGame()
        {
            if (GetAsyncKeyState((int)Keys.O) < 0)
            {
                nextState = State.Gameover;
            }
            UpdatePlayer();
            UpdateObstacleAndItem();
            UpdateTimer();
        }

        void UpdatePlayer()
        {
            var mpos = PointToClient(MousePosition);

            chrLabels[PlayerIndex].Left = mpos.X - chrLabels[PlayerIndex].Width / 2;
            chrLabels[PlayerIndex].Top = mpos.Y - chrLabels[PlayerIndex].Height / 2;
        }

        void UpdateObstacleAndItem()
        {
            for (int i = ObstacleIndex; i < chrLabels.Length; i++)
            {
                chrLabels[i].Left += vx[i];
                chrLabels[i].Top += vy[i];

                if (chrLabels[i].Left < 0)
                {
                    vx[i] = Math.Abs(vx[i]);
                }
                else if (chrLabels[i].Right > ClientSize.Width)
                {
                    vx[i] = -Math.Abs(vx[i]);
                }
                if (chrLabels[i].Top < 0)
                {
                    vy[i] = Math.Abs(vy[i]);
                }
                else if (chrLabels[i].Bottom > ClientSize.Height)
                {
                    vy[i] = -Math.Abs(vy[i]);
                }

                //当たり判定
                if (IsHit(chrLabels[i]))
                {
                    if (IsObstacle(i))
                    {
                        //障害物にぶつかった
                        nextState = State.Gameover;
                    }
                    else
                    {
                        //TODO アイテム
                        AddScore(timer * PointRate);
                    }
                }
            }
        }

        void RandomObstacleAndItemPosition()
        {
            for (int i = ObstacleIndex; i < chrLabels.Length; i++)
            {
                chrLabels[i].Left = random.Next(ClientSize.Width - chrLabels[i].Width);
                chrLabels[i].Top = random.Next(ClientSize.Height - chrLabels[i].Height);
            }
        }

        void UpdateTimer()
        {
            timer--;
            if (timer <= 0)
            {
                timer = 0;
                nextState = State.Gameover;
            }
            labelTimer.Text = $"{timer:000}";
        }

        bool IsHit(Label target)
        {
            var mpos = PointToClient(MousePosition);

            return ((mpos.X >= target.Left) && (mpos.X < target.Right) && (mpos.Y >= target.Top) && (mpos.Y < target.Bottom));
        }

        bool IsObstacle(int index)
        {
            return (index >= ObstacleIndex) && (index < ItemIndex);
        }

        void AddScore(int point)
        {
            //TODO 得点加算
            score = Math.Min(score + point, ScoreMax);

            UpdateScore();
        }

        void UpdateScore()
        {
            labelScore.Text = $"{score:00000}";
        }

        private void buttonTitle_Click(object sender, EventArgs e)
        {
            nextState = State.Title;
        }
    }
}