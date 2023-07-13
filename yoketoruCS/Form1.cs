using Microsoft.VisualBasic.ApplicationServices;
using System.Data;
using System.Runtime.InteropServices;

namespace yoketoruCS
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        int ScoreMax => 99999;
        static int SpeedMax => 10;
        static int PointRate => 100;

        static int PlayerMax => 1;
        static int ItemMax => 8;
        static int ObstacleMax => 8;
        static int PlayerIndex => 0;
        static int ObstacleIndex => PlayerIndex + PlayerMax;
        static int ItemIndex => ObstacleIndex + ObstacleMax;
        static int LabelMax => ItemIndex + ItemMax;
        Label[] chrLabels = new Label[LabelMax];
        int itemCount;

        int[] vx = new int[LabelMax];
        int[] vy = new int[LabelMax];

        static Random random = new Random();

        //列挙子enum
        enum State
        {
            None = -1,
            Title,
            Game,
            Gameover,
            Clear
        }

        State nextState = State.Title;
        State currentState = State.None;

        int score;
        int timer;
        int highScore = 100;
        int StateTimer => 200;

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
                    chrLabels[i].Text = "(・ω・)"/*tempPlayer.Text*/;
                    //chrLabels[i].Font = tempPlayer.Font;
                    //chrLabels[i].ForeColor = tempPlayer.ForeColor;
                }
                else if (i < ItemIndex)
                {
                    chrLabels[i].Text = "💣"/*tempObstacle.Text*/;
                    //chrLabels[i].Font = tempObstacle.Font;
                    //chrLabels[i].ForeColor = tempObstacle.ForeColor;
                }
                else
                {
                    chrLabels[i].Text = "★"/*tempItem.Text*/;
                    //chrLabels[i].Font = tempItem.Font;
                    //chrLabels[i].ForeColor = tempItem.ForeColor;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ChangeState();
            UpdateState();
        }

        void ChangeState()
        {
            if (nextState == State.None) return;

            currentState = nextState;
            nextState = State.None;

            switch (currentState)
            {
                case State.Title:
                    labelTitle.Visible = true;
                    labelTitle.Left = (ClientSize.Width / 2) - (labelTitle.Width / 2);
                    labelTitle.Top = (ClientSize.Height / 3) - (labelTitle.Height / 2);
                    buttonState.Visible = true;
                    buttonState.Left = (ClientSize.Width / 2) - (buttonState.Width / 2);
                    buttonState.Top = ((ClientSize.Height / 3) - (buttonState.Height / 2)) + (ClientSize.Height / 3);
                    labelGameover.Visible = false;
                    buttonTitle.Visible = false;
                    labelClear.Visible = false;
                    break;

                case State.Game:
                    currentState = State.Game;
                    labelTitle.Visible = false;
                    buttonState.Visible = false;
                    labelTimer.Visible = true;
                    labelTimer.Left = ClientSize.Width - (labelTimer.Width + 10);
                    labelTimer.Top = ClientSize.Height - (labelTimer.Height + 10);
                    score = 0;
                    itemCount = ItemMax;
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

                case State.Clear:
                    labelClear.Visible = true;
                    labelClear.Left = (ClientSize.Width / 2) - (labelClear.Width / 2);
                    labelClear.Top = (ClientSize.Height / 3) - (labelClear.Height / 2);
                    buttonTitle.Visible = true;
                    buttonTitle.Left = (ClientSize.Width / 2) - (buttonTitle.Width / 2);
                    buttonTitle.Top = ((ClientSize.Height / 3) - (buttonTitle.Height / 2)) + (ClientSize.Height / 3);
                    break;

            }
        }

        void UpdateState()
        {
            switch (currentState)
            {
                case State.Game:
                    UpdateGame();
                    break;
            }
        }

        void UpdateGame()
        {
            if (GetAsyncKeyState((int)Keys.O) < 0)
            {
                nextState = State.Gameover;
            }
            if (GetAsyncKeyState((int)Keys.C) < 0)
            {
                nextState = State.Clear;
            }

            UpdatePlayer();
            UpdateObstacleAndItem();
            UpdateTimer();
        }

        private void buttonState_Click(object sender, EventArgs e)
        {
            nextState = State.Game;
        }

        private void buttonTitle_Click(object sender, EventArgs e)
        {
            nextState = State.Title;
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
                        //アイテム
                        AddScore(timer * PointRate);
                        itemCount--;
                        chrLabels[i].Visible = false;
                        if (itemCount <= 0)
                        {
                            nextState = State.Clear;
                        }
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
            //得点加算
            score = Math.Min(score + point, ScoreMax);

            UpdateScore();
        }

        void UpdateScore()
        {
            labelScore.Text = $"{score:00000}";
        }
    }
}