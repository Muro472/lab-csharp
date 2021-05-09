using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pong
{
    class GameField
    {
        private int MaxDownForP;
        private Panel parent;

        private Label score1;
        private int score1n;

        private Label score2;
        private int score2n;

        private Panel P1;
        private int P1Speed;

        private Panel P2;
        private int P2Speed;

        private Panel Ball;
        private int BallSpeed;
        private int BallVerticalSpeed;
        private int BallHorisontalSpeed;
        private int BallFlor;
        private int BallLeft;
        private int BallRight;

        private Random rnd;


        private Timer timer;
        private delegate void da();
        private event da P1Events;
        private event da P2Events;
        private event da BallEvents;
        public GameField()
        {
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += Tick;
            rnd = new Random();
        }
        private void Tick(object sender, EventArgs e)
        {
            P1Events?.Invoke();
            P2Events?.Invoke();
            BallEvents?.Invoke();
        }

        public void StartGame(Panel parent)
        {
            this.parent = parent;
            P1Events = null;
            P2Events = null;
            BallEvents = null;

            P1 = new Panel();
            P1.Width = parent.Width / 70;
            P1.Height = parent.Height / 10;
            P1.Left = parent.Width / 10 - P1.Width / 2;
            P1.Top = parent.Height / 2 - P1.Height / 2;
            P1.Paint += PaintBorder;
            parent.Controls.Add(P1);
            P1Speed = 20;

            MaxDownForP = parent.Height - P1.Height;

            P2 = new Panel();
            P2.Width = parent.Width / 70;
            P2.Height = parent.Height / 10;
            P2.Left = parent.Width / 10 * 9 - P2.Width / 2;
            P2.Top = parent.Height / 2 - P2.Height / 2;
            P2.Paint += PaintBorder;
            parent.Controls.Add(P2);
            P2Speed = 20;

            Ball = new Panel();
            Ball.Width = parent.Width / 50;
            Ball.Height = parent.Width / 50;
            Ball.Left = parent.Width / 2 - Ball.Width / 2;
            Ball.Top = parent.Height / 2 - Ball.Height / 2;
            Ball.Paint += PaintBorder;
            parent.Controls.Add(Ball);
            BallSpeed = parent.Width / 200;
            RandomizeBall();
            BallEvents += MoveBall;

            BallFlor = parent.Height - Ball.Height;
            BallLeft = P1.Left + P1.Width;
            BallRight = P2.Left - Ball.Width;

            score1n = 0;
            score1 = new Label();
            score1.Text = Convert.ToString(score1n);
            score1.TextAlign = ContentAlignment.TopCenter;
            score1.ForeColor = Color.White;
            score1.Font = new Font("Microsoft Sans Serif", parent.Height / 50);
            score1.Top = parent.Height / 10 * 2;
            score1.Width = parent.Width / 10;
            score1.Left = parent.Width / 10 * 3;
            score1.Height = parent.Height / 25;
            parent.Controls.Add(score1);


            score2n = 0;
            score2 = new Label();
            score2.Text = Convert.ToString(score2n);
            score2.TextAlign = ContentAlignment.TopCenter;
            score2.ForeColor = Color.White;
            score2.Font = new Font("Microsoft Sans Serif", parent.Height / 50);
            score2.Top = parent.Height / 10 * 2;
            score2.Width = parent.Width / 10;
            score2.Left = parent.Width / 10 * 6;
            score2.Height = parent.Height / 25;
            parent.Controls.Add(score2);

        }



        private void MoveBall()
        {
            if (Ball.Top <= 0 || Ball.Top >= BallFlor)
            {
                BallVerticalSpeed *= -1;
            }
            if (Ball.Left <= BallLeft && Ball.Left >= BallLeft - P1.Width || Ball.Left >= BallRight && Ball.Left <= BallRight + P2.Width)
            {
                if (P1.Top - Ball.Height < Ball.Top && P1.Top + P1.Height - Ball.Height > Ball.Top)
                {
                    BallHorisontalSpeed *= -1;
                }

                if (P2.Top - Ball.Height < Ball.Top && P2.Top + P2.Height - Ball.Height > Ball.Top)
                {
                    BallHorisontalSpeed *= -1;
                }

            }

            if(Ball.Left <= 0) 
            {
                score2n++;
                RandomizeBall();
                score2.Text = Convert.ToString(score2n);
            }
            if(Ball.Left + Ball.Width > parent.Width)
            {
                score1n++;
                RandomizeBall();
                score1.Text = Convert.ToString(score1n);
            }

            Ball.Top += BallVerticalSpeed;
            Ball.Left += BallHorisontalSpeed;
        }

        private bool w = true;
        private bool s = true;
        private bool d = true;
        private bool up = true;
        private bool down = true;
        private bool left = true;

        public void KeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    if (w)
                    {
                        P1Events += ToUPP1;
                        w = false;
                    }
                    break;
                case Keys.S:
                    if (s)
                    {
                        P1Events += ToDownP1;
                        s = false;
                    }
                    break;
                case Keys.D:
                    if (d)
                    {
                        P1Speed += 20;
                        d = false;
                    }
                    break;

                case Keys.Up:
                    if (up)
                    {
                        P2Events += ToUPP2;
                        up = false;
                    }
                    break;
                case Keys.Down:
                    if (down)
                    {
                        P2Events += ToDownP2;
                        down = false;
                    }
                    break;
                case Keys.Left:
                    if (left)
                    {
                        P2Speed += 20;
                        left = false;
                    }
                    break;
            }
        }

        public void KeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    P1Events = null;
                    w = true;
                    break;
                case Keys.S:
                    P1Events = null;
                    s = true;
                    break;
                case Keys.D:
                    P1Speed -= 20;
                    d = true;
                    break;

                case Keys.Up:
                    P2Events = null;
                    up = true;
                    break;
                case Keys.Down:
                    P2Events = null;
                    down = true;
                    break;
                case Keys.Left:
                    P2Speed -= 20;
                    left = true;
                    break;
            }
        }
        private void ToUPP1()
        {
            if(P1.Top >= 0)
            {
                P1.Top -= P1Speed;
            }
        }

        private void ToDownP1()
        {
            if (P1.Top <= MaxDownForP)
            {
                P1.Top += P1Speed;
            }
        }

        private void ToUPP2()
        {
            if (P2.Top >= 0)
            {
                P2.Top -= P2Speed;
            }
        }

        private void ToDownP2()
        {
            if (P2.Top <= MaxDownForP)
            {
                P2.Top += P2Speed;
            }
        }

        private void PaintBorder(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Panel s = sender as Panel;
            Pen pen = new Pen(Color.White, 2);
            g.DrawRectangle(pen, 1, 1, s.Width - 1, s.Height - 1);
        }

        public void StartTimer()
        {
            timer.Enabled = true;
        }

        public void StopTimer()
        {
            timer.Enabled = true;
        }
        
        private void RandomizeBall()
        {
            int rand = 0;
            rnd = new Random();
            rand = rnd.Next(1, 5);

            Ball.Left = parent.Width / 2 - Ball.Width / 2;
            Ball.Top = parent.Height / 2 - Ball.Height / 2;

            switch (rand)
            {
                case 1:
                    BallHorisontalSpeed = BallSpeed / 2;
                    BallVerticalSpeed = BallSpeed / 2;
                    break;
                case 2:
                    BallHorisontalSpeed = -(BallSpeed / 2);
                    BallVerticalSpeed = BallSpeed / 2;
                    break;
                case 3:
                    BallHorisontalSpeed = -(BallSpeed / 2);
                    BallVerticalSpeed = -(BallSpeed / 2);
                    break;
                case 4:
                    BallHorisontalSpeed = BallSpeed / 2;
                    BallVerticalSpeed = -(BallSpeed / 2);
                    break;
                default:
                    break;
            }
        }
    }
}
