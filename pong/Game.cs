using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace pong
{
    class Game
    {
        private int windowSizeX;
        private int windowSizeY;

        private Font font;

        private Form form;

        private Panel MainPanel;

        //UI
        private Label startButton;
        private Label exitButton;
        private Label gameTitle;
        private Panel P1;
        private Panel P2;

        //

        //Game
        private bool GameStatus;
        private Panel gameExitButton;
        private GameField gameField;
        //

        private void createLbels(Panel panel, Font font, string pnametxt, string pmove, string pacs)
        {
            Label pname = new Label();
            pname.Width = panel.Width;
            pname.TextAlign = ContentAlignment.TopCenter;
            pname.Height = panel.Height / 5;
            pname.ForeColor = Color.White;
            pname.Text = pnametxt;
            pname.Font = font;
            panel.Controls.Add(pname);

            Label movemant = new Label();
            movemant.Width = panel.Width;
            movemant.Height = panel.Height / 5;
            movemant.ForeColor = Color.White;
            movemant.TextAlign = ContentAlignment.TopCenter;
            movemant.Left = panel.Width / 10;
            movemant.Top = panel.Height / 2;
            movemant.Font = font;
            movemant.Text = pmove;
            panel.Controls.Add(movemant);

            Label acsel = new Label();
            acsel.Width = panel.Width;
            acsel.Height = panel.Height / 5;
            acsel.ForeColor = Color.White;
            acsel.TextAlign = ContentAlignment.TopCenter;
            acsel.Text = pacs;
            acsel.Font = font;
            acsel.Left = panel.Width / 10;
            acsel.Top = panel.Width / 10 * 9;
            panel.Controls.Add(acsel);
        }

        public Game(Control parent, Form form)
        {
            GameStatus = false;
            this.form = form;
            parent.BackColor = Color.Black;
            form.WindowState = FormWindowState.Maximized;
            form.WindowState = FormWindowState.Maximized;
            form.FormBorderStyle = FormBorderStyle.None;
            windowSizeX = form.Width;
            windowSizeY = form.Height;
            font = new Font("Microsoft Sans Serif", windowSizeX / 70);
            gameField = new GameField();
            createField();
            createUI();
        }

        private void createGame()
        {
            GameStatus = true;
            gameExitButton = new Panel();
            gameExitButton.Paint += PaintCross;
            gameExitButton.Width = windowSizeX / 50;
            gameExitButton.Height = windowSizeX / 50;
            gameExitButton.Top = windowSizeX / 50;
            gameExitButton.Left = windowSizeX - gameExitButton.Width - windowSizeX / 50;
            gameExitButton.Click += GameExitButton;
            MainPanel.Controls.Add(gameExitButton);

            gameField.StartGame(MainPanel);
            gameField.StartTimer();

        }

        private void PaintCross(object sender, PaintEventArgs e)
        {
            Panel s = sender as Panel;
            Graphics g = e.Graphics;
            Pen pan = new Pen(Color.Red, 5);
            g.DrawLine(pan, 0, 0, s.Width, s.Height);
            g.DrawLine(pan, 0, s.Width, s.Width, 0);
        }

        private void PaintBorders(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pan = new Pen(Color.White, 2);
            g.DrawRectangle(pan, 0, 0, windowSizeX / 5, windowSizeY / 10);
        }

        private void GameExitButton(object sender, EventArgs e)
        {
            GameStatus = false;
            MainPanel.Controls.Clear();
            gameField.StopTimer();
            createUI();

            SaveFileDialog ofd = new SaveFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(ofd.FileName);
                sw.WriteLine(Convert.ToString(gameField.score1n));
                sw.WriteLine(Convert.ToString(gameField.score2n));
                sw.Close(); 
            }
        }

        private void StartButton(object sender, EventArgs e)
        {
            MainPanel.Controls.Clear();
            createGame();
            gameField.StopTimer();

            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(ofd.FileName);

                gameField.score1n = Convert.ToInt32(sr.ReadLine());
                gameField.score1.Text = Convert.ToString(gameField.score1n);

                gameField.score2n = Convert.ToInt32(sr.ReadLine());
                gameField.score2.Text = Convert.ToString(gameField.score2n);

                sr.Close();
            }
            gameField.StartTimer();
        }

        private void ExitButton(object sender, EventArgs e)
        {
            form.Close();
        }

        private void createField()
        {
            MainPanel = new Panel();
            MainPanel.Width = windowSizeX;
            MainPanel.Height = windowSizeY;
            MainPanel.Top = 0;
            MainPanel.Left = 0;
            form.Controls.Add(MainPanel);
        }
        
        private void createUI()
        {
            startButton = new Label();
            startButton.Height = windowSizeY / 10;
            startButton.Width = windowSizeX / 5;
            startButton.Left = windowSizeX / 10 * 4;
            startButton.Top = windowSizeY / 10 * 3;
            startButton.Click += StartButton;
            startButton.Text = "start";
            startButton.Font = font;
            startButton.ForeColor = Color.White;
            startButton.TextAlign = ContentAlignment.MiddleCenter;
            startButton.Paint += PaintBorders;
            MainPanel.Controls.Add(startButton);

            exitButton = new Label();
            exitButton.Height = windowSizeY / 10;
            exitButton.Width = windowSizeX / 5;
            exitButton.Left = windowSizeX / 10 * 4;
            exitButton.Top = windowSizeY / 10 * 5;
            exitButton.Click += ExitButton;
            exitButton.Text = "exit";
            exitButton.Font = font;
            exitButton.ForeColor = Color.White;
            exitButton.TextAlign = ContentAlignment.MiddleCenter;
            exitButton.Paint += PaintBorders;
            MainPanel.Controls.Add(exitButton);

            gameTitle = new Label();
            gameTitle.Width = windowSizeX;
            gameTitle.Height = windowSizeY / 10;
            gameTitle.Top = windowSizeX / 20;
            gameTitle.Font = font;
            gameTitle.Text = "PONG";
            gameTitle.TextAlign = ContentAlignment.TopCenter;
            gameTitle.ForeColor = Color.White;
            MainPanel.Controls.Add(gameTitle);

            P1 = new Panel();
            P1.Width = windowSizeX / 10 * 2;
            P1.Height = windowSizeY / 10 * 4;
            P1.Top = windowSizeY / 10 * 3;
            P1.Left = windowSizeX / 10;
            MainPanel.Controls.Add(P1);
            //createLbels(P1, font, "P1", "movement: W S", "acceleration: D");

            BigLabel bl1 = new BigLabel(P1, "P1", "movement: W S", "acceleration: D");
            P1.Controls.Add(bl1);




            P2 = new Panel();
            P2.Width = windowSizeX / 10 * 2;
            P2.Height = windowSizeY / 10 * 4;
            P2.Top = windowSizeY / 10 * 3;
            P2.Left = windowSizeX / 10 * 7;
            MainPanel.Controls.Add(P2);
            //createLbels(P2, font, "P2", "movement: UP Down", "acceleration: Left");

            BigLabel bl2 = new BigLabel(P2, "P2", "movement: UP Down", "acceleration: Left");
            P2.Controls.Add(bl2);

        }


        public void KeyDown(KeyEventArgs e)
        {
            if (GameStatus)
            {
                gameField.KeyDown(e);
            }
        }

        public void KeyUp(KeyEventArgs e)
        {
            if (GameStatus)
            {
                gameField.KeyUp(e);
            }
        }

    }
}
