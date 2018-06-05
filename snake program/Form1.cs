using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using snake_program.Map;
using System.Windows.Forms;

namespace snake_program
{
    public partial class Form1 : CoreForm
    {
     
        // ---------------------- BEGINNING OF GAME ENGINE PRECURSORS ---------------------------
        // worker for timer - keep the game running
        void worker()
        {
            // move the thing upward
            //snakeHeadPart.run(runTimer.Interval); // interval / 1000 is time in seconds
            //snake.run(runTimer.Interval);
            // check if anything to be deleted in the form
            // ----- START OF DELETION QUEUE END
            while (removeQueue.Count() > 0)
            {
                // try to the current item to be deleted
                if (removeQueue.TryDequeue(out Control item))
                {
                    Controls.Remove(item);
                }
            }
            // run the engine
            engine.CoreRun();
        }

        // ---------------------- END OF GAME ENGINE PRECURSORS ---------------------------
        private void runTimer_Tick(object sender, EventArgs e)
        {
            worker();
        }
       
        // constructor
        public Form1()
        {
            InitializeComponent();

            // create new object for bodyPart
            // start timer
            runTimer.Start();

            gameConstruction(); // add stuff in form defined by me in the function

        }
        Image bannerImg;// image for the banner (only set for win or loss)
        public void CoreWin() // bannerTimer method for when the player wins
        {
            // set the image for the banner
            bannerImg = Properties.Resources.you_win;
            bannerTimer.Tick += new EventHandler(bannerTick);// set the method for the timer
            bannerTimer.Start(); // run the timer
        }
        public void CoreLose() // bannerTimer method for when the player loses
        {
            // set the image for the banner
            bannerImg = Properties.Resources.you_lose;
            bannerTimer.Tick += new EventHandler(bannerTick);// set the method for the timer
            bannerTimer.Start(); // run the timer
        }
        void bannerTick(Object o, EventArgs e)
        {
            // make the banner with the image for winning
            PictureBox banner = new PictureBox
            {
                Image = bannerImg,
                SizeMode = PictureBoxSizeMode.AutoSize // scale picturebox to image
            };
            // center the image
            int x = (this.Width - banner.Width) / 2;
            int y = (this.Height - banner.Height) / 2;
            banner.Location = new Point(x, y);
            // add the image to the form
            Controls.Add(banner);
            banner.BringToFront(); // make the banner visible above everything else
            runTimer.Stop();// stop the game engine timer
            bannerTimer.Stop(); // stop the timer
        }
        // create objects in the form (coins, obstacles)
        void gameConstruction()
        { 
            // make the paused banner
            pausedBanner = new PictureBox
                {
                    Image = Properties.Resources.paused,
                    // resize box to fit image
                    SizeMode = PictureBoxSizeMode.AutoSize
            };
            pausedBanner.Location = new Point((Width - pausedBanner.Width) / 2, (Height - pausedBanner.Height) / 2); // center the image
            // create the game engine
            engine = new GameEngine(this); 
            // add existent coins
            engine.AddFood(new Apple(coin1, this));
            engine.AddFood(new Apple(coin2, this));
            engine.AddFood(new Apple(coin3, this));
            engine.AddFood(new Apple(coin4, this));
            engine.AddFood(new Apple(coin5, this));
            engine.AddFood(new Apple(coin6, this));
            engine.AddFood(new Apple(coin7, this));
            

            // add obstacles existent on the map
            engine.AddObstacle(Obstacle.FromPicture(obstacle1, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle2, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle3, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle4, Color.Red));
            // create and add food spawner group
            FoodSpawnGroup group = new FoodSpawnGroup(engine);
            group.Add(new AppleSpawnPad(spawnPadPic2, 0.00001, engine));
            group.Add(new AppleSpawnPad(spawnPadPic, 0.00001, engine)); // add the second spawner
            engine.groupSpawners.Add(group);

            // Add enemies
            engine.AddStationary(new EnemyStationary(enemy1, 16, this));

            // make a new snake
            snake = new ContinuousSnake(Size.Width / 2, Size.Height / 2, this, new Vector(2, 90), 300);
            // add it to the engine
            engine.AddSnake(snake);            
        }        
        void debugR() // R press
        {
            
        }
        void debugT() // T press
        {
            
        }
        void debugSpace() // space press
        {

        }
        
        void debugP() // on p press
        {
            // toggle pause the game and start or stop timer based on pause
            engine.TogglePause();
            if (engine.paused)
            {
                // display the paused banner
                Controls.Add(pausedBanner);
                pausedBanner.BringToFront();
                runTimer.Stop();
            } else
            {
                // remove the paused banner
                Controls.Remove(pausedBanner);
                runTimer.Start();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        // handle key pressing
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.A: // left button
                    snake.RequestLeft();
                    break;
                case Keys.D: // right button
                    snake.RequestRight();
                    break;
                case Keys.W: // up button
                    snake.RequestUp();
                    break;
                case Keys.S: // down button
                    snake.RequestDown();
                    break;
                case Keys.R:  // currently, the R button is mostly for debugging
                    debugR();
                    break;
                case Keys.T: // also for debugging
                    debugT();
                    break;
                case Keys.P:
                    debugP();
                    break;
                case Keys.Space:
                    debugSpace();
                    break;
            }
            
            return base.ProcessCmdKey(ref msg, keyData);
        }
        // banner for paused
        PictureBox pausedBanner;
        // game engine object - runs the game
        GameEngine engine;
        ContinuousSnake snake; // snake object reference
        public ConcurrentQueue<Control> removeQueue = new ConcurrentQueue<Control>(); // queue of objects to be deleted
    }
}
