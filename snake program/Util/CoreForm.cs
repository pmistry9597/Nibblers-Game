using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Concurrent;

namespace snake_program
{
    public class CoreForm : Form
    {
        // ---------------------- BEGINNING OF GAME ENGINE PRECURSORS ---------------------------
        // worker for timer - keep the game running
        void worker(Object o, EventArgs e)
        {
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
            engine.ExtraWork(); // work that inherited classes may want to do
            // run extra work that inherited classes may want to do
            ExtraWork(o, e);

            if (snake.Collided(Gate) != 0)
            {
                snake.snakeHead.picBox.SendToBack();
            }
        }
        public virtual void ExtraWork(Object o, EventArgs e) // optional override for inherited classes to do extra work in each frame update
        {

        }

        // ---------------------- END OF GAME ENGINE PRECURSORS ---------------------------

        // constructor (for inherited classes)
        public void CoreBuild()
        {
            // create the timers
            runTimer = new Timer();
            bannerTimer = new Timer();
            countDownTimer = new Timer();
            runTimer.Interval = 1;// run timer runs every millisecond
            bannerTimer.Interval = 2000; // bannerTimer ticks after 2 seconds
            countDownTimer.Interval = 1500; // countdown timer will initially be 1.5 seconds (will be set to 1 second after first tick)
            // make the runTimer run the worker function
            runTimer.Tick += new EventHandler(worker);
            // countdown timer must run the countdown function
            countDownTimer.Tick += new EventHandler(countdownTick);

            gameConstruction(); // add stuff in form defined by me in the function

            // start the countdown
            countDownTimer.Start();

        }
        public Image bannerImg;// image for the banner (only set for win or loss)
        public virtual void CoreWin() // bannerTimer method for when the player wins
        {
            // set the image for the banner
            bannerImg = Properties.Resources.you_win;
            bannerTimer.Tick += new EventHandler(bannerTick);// set the method for the timer
            bannerTimer.Start(); // run the timer
            // set end game to true
            endGame = true;
        }
        public virtual void CoreLose() // bannerTimer method for when the player loses
        {
            // set the image for the banner
            bannerImg = Properties.Resources.you_lose;
            bannerTimer.Tick += new EventHandler(bannerTick);// set the method for the timer
            bannerTimer.Start(); // run the timer
            // set end game to true
            endGame = true;
        }
        public void bannerTick(Object o, EventArgs e)
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
        public virtual void gameConstruction()
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
            // make the start Banner
            startBanner = new PictureBox
            {
                Image = LevelBanner,
                SizeMode = PictureBoxSizeMode.AutoSize
            };
            // center the banner
            int x = (Width - startBanner.Width) / 2; int y = (Height - startBanner.Height) / 2;
            startBanner.Location = new Point(x, y);
            // add it to the form
            Controls.Add(startBanner);
            startBanner.BringToFront(); // make it visible above all
            
        }
        public virtual void debugR() // R press
        {

        }
        public virtual void debugT() // T press
        {

        }
        public virtual void debugSpace() // space press
        {

        }

        public virtual void debugP() // on p press
        {
            // dont run pause if game has ended
            if (endGame)
            {
                return;
            }
            // dont pause if during countdown
            if (countDownTimer.Enabled)
            {
                return;
            }
            // toggle pause the game and start or stop timer based on pause
            engine.TogglePause();
            if (engine.paused)
            {
                // display the paused banner
                Controls.Add(pausedBanner);
                pausedBanner.BringToFront();
                runTimer.Stop();
            }
            else
            {
                // remove the paused banner
                Controls.Remove(pausedBanner);
                runTimer.Start();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        public delegate void VoidFunction();
        public void ShouldRun(VoidFunction f, ContinuousSnake snake) // true if game is running and is used to make descisions whether or not to run something related to game play
        {
            if (snake != null && (snake.Alive && runTimer.Enabled)) // run only if gameplay is should happen
            {
                f();
            }
        }
        // handle key pressing
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.A: // left button
                    ShouldRun(snake.RequestLeft, snake);
                    break;
                case Keys.D: // right button
                    ShouldRun(snake.RequestRight, snake);
                    break;
                case Keys.W: // up button
                    ShouldRun(snake.RequestUp, snake);
                    break;
                case Keys.S: // down button
                    ShouldRun(snake.RequestDown, snake);                    
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
            ExtraKeyWork(ref msg, keyData);

            return base.ProcessCmdKey(ref msg, keyData);
        }
        public virtual void ExtraKeyWork(ref Message msg, Keys keyData)
        {

        }
        void countdownTick(Object o, EventArgs e) // for the countdown timer
        {
            // if the timer is past 3, time to start the time instead and delete the start banner
            if (countdown > 3)
            {
                startBanner.Visible = false;
                Controls.Remove(startBanner);
                startBanner = null;
                runTimer.Start(); // start the game
                countDownTimer.Stop(); // stop the countdown - no longer needed
                RunAfterCountdown();
                return;
            }
            countDownTimer.Interval = 1000; //reset interval to one second
            // set start banner to correct banner
            startBanner.Image = startBanners[countdown];
            // reposition the banner to the center
            int x = (Width - startBanner.Width) / 2; ; int y = (Height - startBanner.Height) / 2;
            startBanner.Location = new Point(x, y);
            countdown++;
        }
        public virtual void RunAfterCountdown() // optional overridable function to do stuff after countdown has ended and game starts
        {

        }

        public Image LevelBanner; // first image to be displayed (tells which level the player is on)
        PictureBox startBanner; // picturebox that displays banners at the beginning
        int countdown = 0; // countdown used to display right banners at beginning and start game at right time
        Image[] startBanners = new Image[] {
            Properties.Resources.countdown_3,
            Properties.Resources.countdown_2, 
            Properties.Resources.countdown_1, 
            Properties.Resources.countdown_go
        }; // count down banners displayed at beginning of the round (number countdown and go sign)
        // banner for paused
        public PictureBox pausedBanner;
        // game engine object - runs the game
        public GameEngine engine;
        public ContinuousSnake snake; // snake object reference
        public ConcurrentQueue<Control> removeQueue = new ConcurrentQueue<Control>(); // queue of objects to be deleted
        public Timer runTimer; // timer to run the actual game (including the engine)
        public Timer bannerTimer; // timer to run the win and lose operations (one shot)
        public Timer countDownTimer; // timer the runs at beginning to do countdown and start game

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CoreForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "CoreForm";
            this.Load += new System.EventHandler(this.CoreForm_Load);
            this.ResumeLayout(false);

        }

        private void CoreForm_Load(object sender, EventArgs e)
        {

        }
        // this is true if game has ended
        public bool endGame = false;
        public PictureBox Gate;
    }
}
