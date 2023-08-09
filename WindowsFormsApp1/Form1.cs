using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int gravity = 10;
        int speed = 25;
        int score = 0;
        Random rnd = new Random();
        int pipeGap = 111;

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                gravity = 11;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                gravity = -15;
            else if (e.KeyCode == Keys.Enter)
                timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bird.Top += gravity;
            pipeDown.Left -= speed;
            
            pipeTop.Left -= speed;
            
            lblScore.Text = $"Score: {score}";

            if (pipeDown.Left < -84)
            {
                pipeDown.Left = rnd.Next(100, 600);
                score++;
            }
            
            
            if (pipeTop.Left < -84)
            {
                pipeTop.Left = rnd.Next(300, 700);
                score++;
            }
           

            if (bird.Bounds.IntersectsWith(pipeDown.Bounds) || bird.Bounds.IntersectsWith(pipeTop.Bounds) || bird.Bounds.IntersectsWith(ground.Bounds))
            {
                EndGame();
            }
            

            if (score > 10)
            {
                speed += 5;
            }
        }

        private void EndGame()
        {
            timer1.Stop();
            DialogResult result = MessageBox.Show("Game Over! Your Score: " + score, "Flappy Bird", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                RestartGame();
            }
            else if (result == DialogResult.No)
            {
                Application.Exit();
            }
        }

        private void RestartGame()
        {
            bird.Top = (this.Height - bird.Height) / 2;
            pipeDown.Left = this.Width;
            
            pipeTop.Left = this.Width;
            
            score = 0;
            speed = 25;

            timer1.Start();
        }
    }
}
