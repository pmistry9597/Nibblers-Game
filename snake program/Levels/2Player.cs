using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snake_program
{
    public partial class _2Player : CoreForm
    {
        public _2Player()
        {
            InitializeComponent();

            // set the banner
            LevelBanner = Properties.Resources.two_player;

            base.CoreBuild();

            // add the obstacles
            engine.obstacles.Add(Obstacle.FromPicture(obstacle1, Color.Brown));
            engine.obstacles.Add(Obstacle.FromPicture(obstacle2, Color.Brown));
            engine.obstacles.Add(Obstacle.FromPicture(obstacle3, Color.Brown));
            engine.obstacles.Add(Obstacle.FromPicture(obstacle4, Color.Brown));
            engine.obstacles.Add(Obstacle.FromPicture(obstacle5, Color.Brown));
            engine.obstacles.Add(Obstacle.FromPicture(obstacle6, Color.Brown));
            engine.obstacles.Add(Obstacle.FromPicture(obstacle7, Color.Brown));
            engine.obstacles.Add(Obstacle.FromPicture(obstacle8, Color.Brown));
            engine.obstacles.Add(Obstacle.FromPicture(obstacle9, Color.Brown));
            engine.obstacles.Add(Obstacle.FromPicture(obstacle10, Color.Brown));
            engine.obstacles.Add(Obstacle.FromPicture(obstacle11, Color.Brown));
            engine.obstacles.Add(Obstacle.FromPicture(obstacle12, Color.Brown));
            engine.obstacles.Add(Obstacle.FromPicture(obstacle13, Color.Brown));
            engine.obstacles.Add(Obstacle.FromPicture(obstacle14, Color.Brown));
            engine.obstacles.Add(Obstacle.FromPicture(obstacle15, Color.Brown));
            engine.obstacles.Add(Obstacle.FromPicture(obstacle16, Color.Brown));
            engine.obstacles.Add(Obstacle.FromPicture(obstacle17, Color.Brown));
            engine.obstacles.Add(Obstacle.FromPicture(obstacle18, Color.Brown));
            engine.obstacles.Add(Obstacle.FromPicture(obstacle19, Color.Brown));
            engine.obstacles.Add(Obstacle.FromPicture(obstacle20, Color.Brown));
            engine.obstacles.Add(Obstacle.FromPicture(obstacle21, Color.Brown));
            // add the snakes
            snake = new ContinuousSnake(spawnPoint.Location.X, spawnPoint.Location.Y, this, new Vector(3, 90), 3, false);
            engine.AddSnake(snake);
            // add the red snake
            snakeRed = new ContinuousSnake(spawnPointRed.Location.X, spawnPointRed.Location.Y, this, new Vector(3, 90), 3, true);
            engine.AddSnake(snakeRed);

            // make the spawnpoints invisible
            spawnPoint.Visible = false;
            spawnPointRed.Visible = false;

            // add the food spawners to a single spawn group
            FoodSpawnGroup spawnGroup = new FoodSpawnGroup(engine, 0.9);
            spawnGroup.Add(new AppleSpawnPad(applespawn1, 1, engine));
            spawnGroup.Add(new AppleSpawnPad(applespawn2, 1, engine));
            spawnGroup.Add(new AppleSpawnPad(applespawn3, 1, engine));
            spawnGroup.Add(new AppleSpawnPad(applespawn4, 1, engine));
            spawnGroup.Add(new AppleSpawnPad(applespawn5, 1, engine));
            spawnGroup.Add(new AppleSpawnPad(applespawn6, 1, engine));
            spawnGroup.Add(new AppleSpawnPad(applespawn7, 1, engine));
            spawnGroup.Add(new AppleSpawnPad(applespawn8, 1, engine));
            spawnGroup.Add(new AppleSpawnPad(applespawn9, 1, engine));
            // add it to the engine
            engine.AddFoodGroupSpawner(spawnGroup);
        }
        ContinuousSnake snakeRed; // the second player that uses the arrow keys

        public override void ExtraKeyWork(ref Message msg, Keys keyData)
        {
            base.ExtraKeyWork(ref msg, keyData);

            // process keys for red snake (arrow keys)
            switch (keyData)
            {
                case Keys.Up:
                    ShouldRun(snakeRed.RequestUp, snakeRed);
                    break;
                case Keys.Down:
                    ShouldRun(snakeRed.RequestDown, snakeRed);
                    break;
                case Keys.Right:
                    ShouldRun(snakeRed.RequestRight, snakeRed);
                    break;
                case Keys.Left:
                    ShouldRun(snakeRed.RequestLeft, snakeRed);
                    break;
            }
        }

        public override void debugR()
        {
            base.debugR();
        }

        // show scores of the snakes
        void DisplayScoreBoard()
        {
            // set the back color of the backboard
            scoreBack.BackColor = Color.Brown;
            // make the scoreboard controls visible
            scoreBack.Visible = true;
            scoreBackWhite.Visible = true;
            RedScore.Visible = true;
            RedScore.BringToFront();
            GreenScore.Visible = true;
            GreenScore.BringToFront();
            lblRed.Visible = true;
            lblRed.BringToFront();
            lblGreen.Visible = true;
            lblGreen.BringToFront();
            winBanner.Visible = true;
            winBanner.BringToFront();
            // show the scores in the score board
            RedScore.Text = string.Format("{0:0.#}", (double)snakeRed.Length / (double)BodyPart.SIZE);
            GreenScore.Text = string.Format("{0:0.#}", (double)snake.Length / (double)BodyPart.SIZE);
        }

        public override void CoreLose() // override lose command to display who won and display the scoreboard
        {
            
            EndGame();
        }
        // hide snakes and their particles
        void HideStuff()
        {
            // hide all snakes and particles
            foreach (ContinuousSnake snake in engine.snakes)
            {
                snake.snakeHead.picBox.SendToBack();
                foreach (BodySegment segment in snake.bodySegments) // loop through all body segments and hide them as well
                {
                    segment.picBox.SendToBack();
                }
                // hide all particles of the snake if they exist
                if (snake.snakeHead.particles != null)
                {
                    foreach (BodyPart particle in snake.snakeHead.particles)
                    {
                        particle.picBox.SendToBack();
                    }
                }
            }
            foreach (BodySegment segment in snakeRed.bodySegments)
            {
                segment.picBox.SendToBack();
                segment.picBox.Visible = false;
                
            }
            // hide all obstacles
            foreach (Obstacle obstacle in engine.obstacles)
            {
                obstacle.picBox.SendToBack();
            }
        }
        // end of timer function - check who won
        void EndGame()
        {
            if (snake.Length == snakeRed.Length) // if snake lengths are same, its a tie
            {
                winBanner.Image = Properties.Resources.Tie;
            } else if (snake.Length > snakeRed.Length) // if green snake won, show the green banner
            {
                winBanner.Image = Properties.Resources.Green_Won;
            } else // if red snake won (only possible last case), then show the red banner
            {
                winBanner.Image = Properties.Resources.Red_Won;
            }
            HideStuff(); // hide the snakes since game is over
            // show the end banner
            DisplayScoreBoard();
            // stop the run timer
            runTimer.Stop();
            // stop the game timer
            gameTimer.Stop();
            // set game end to true
            endGame = true;
        }

        // banner timer tick function for 2 player
        void endTick(Object o, EventArgs e)
        {
            bannerTimer.Stop();
            DisplayScoreBoard(); // display the scoreboard
            // stop the run timer
            runTimer.Stop();
            // stop the game timer
            gameTimer.Stop();
            
        }

        public override void debugP() // although its called debugP, its actually the pause thing
        {
            // dont run pause if game has ended or if countdown is happening
            if (endGame || countDownTimer.Enabled)
            {
                return;
            }
            base.debugP();
            // pause or start game timer depending on whether the game is paused or not
            if (engine.paused)
            {
                gameTimer.Stop();
            } else
            {
                gameTimer.Start();
            }
        }

        // override lose function to show snake that won

        private void _2Player_Load(object sender, EventArgs e)
        {

        }

        private void scoreBackWhite_Click(object sender, EventArgs e)
        {

        }

        private void RedScore_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
        // start timer after countdown for the game
        public override void RunAfterCountdown()
        {
            base.RunAfterCountdown();
            gameTimer.Start(); // start timer that limits game time
        }

        // time stored here
        int time = 120; // game lasts two minutes so start time is 120 seconds
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // subtract one from the time at every second (every tick of this timer)
            time--;
            // get the time in conventional form
            double premin = time / (double)60; // get the "premin," which is the time in minutes plus the decimals
            int minutes = (int)premin; // cut the decimals out of the premin
            // get seconds by getting the decimal number and multplying by 60 to reverse the division
            int seconds = (int)((premin - minutes) * 60);
            // put the result into a string and display it
            String conventionalTime = minutes.ToString() + " : " + string.Format("{0:00}", seconds);
            txtTime.Text = conventionalTime;
            // end the game if timer is at zero or lower
            if (time <= 0)
            {
                EndGame();
                endGame = true;
            }
            
        }
        
    }
}
