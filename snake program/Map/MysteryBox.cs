using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace snake_program
{
    public class MysteryBox // object on screen that does something random to the snake when the snake collides with it
    {
        public MysteryBox(PictureBox picBox, CoreForm form)
        {
            Setup(picBox, form);
        }
        public MysteryBox(int x, int y, CoreForm form)
        {
            PictureBox picBox = new PictureBox
            {
                Location = new System.Drawing.Point(x,y)
            };
            form.Controls.Add(picBox);
            Setup(picBox, form);
        }
        public void Setup(PictureBox picBox, CoreForm form) // generic setup for this object
        {
            mainForm = form;
            this.picBox = picBox;
            picBox.SizeMode = PictureBoxSizeMode.StretchImage;
            picBox.Size = new System.Drawing.Size(BodyPart.SIZE, BodyPart.SIZE);

            // load image into picturebox
            picBox.Image = Properties.Resources.question_mark;
        }
        public void finalize()
        {
            mainForm.removeQueue.Enqueue(picBox);
            Finalized = true;
        }
        void speedChange(ContinuousSnake snake)
        {
            this.snake = snake;
            // store previous speed for reset
            prevSpeed = snake.Velocity.Magnitude;
            // get random speed
            int speed = mainForm.engine.GetRandom(1, 4);
            snake.Velocity.Magnitude = speed;

            // timer stuff here
            speedTimer = new System.Timers.Timer();
            // get random interval to make the speed for (between 1 and 5 seconds)
            int speedDuration = mainForm.engine.GetRandom(1000, 5000);
            speedTimer.Interval = speedDuration;
            // run event at timer's first and only tick to reset snake speed and setup for deletion
            speedTimer.Elapsed += new System.Timers.ElapsedEventHandler(speedTimerTick);
            // start the timer
            speedTimer.Start();
            Finalized = true;
            finalize();
        }
        
        void sizeChange(ContinuousSnake snake) // random amount of size change to snake
        {
            // get random amount to add or remove
            int delta = mainForm.engine.GetRandom(-BodyPart.SIZE * 4, BodyPart.SIZE * 4);
            snake.ChangeSize(delta);
            finalize();
            Finalized = true; // set flag up for deletion by game engine
        }
        void sizeScale(ContinuousSnake snake) // randomly scale snake size
        {
            // list of possible scales
            double[] validScales = new double[] {  0.8 , 1.5, 2, 2.5}; // 6 possible scales
            // get random scale factor
            int random = mainForm.engine.GetRandom(0, 5); double scale = validScales[random];
            // get current length of snake and get size change
            int length = snake.Length;
            int newLength = (int)(length * scale + 0.5); // get new length (add 0.5 to round)
            int delta = newLength - length;
            snake.ChangeSize(delta);
            finalize();
            Finalized = true; 
        }
        public void Invoke(ContinuousSnake snake) // run the mystery box on a snake
        {
            // get random number to choose an action
            int random = mainForm.engine.GetRandom(1, 100);
            if (random > 0 && (random <= 50)) // do speed change
            {
                Console.Write("Speed change!");
                speedChange(snake);
            } /*else if (random > 33 && (random <= 67)) // snake size scaling
            {
                Console.Write("Size scaled!");
                sizeScale(snake);
            }*/ else // add/subtract from snake
            {
                Console.Write("Size change!");
                sizeChange(snake);
            }
        }
        ContinuousSnake snake;// snake reference for speed change
        public bool Finalized = false; // if true, the game engine should delete this
        CoreForm mainForm; // reference to main form
        public PictureBox picBox; // picturebox that represents this image

        System.Timers.Timer speedTimer; // timer that will reset speed after certain amount of time
        double prevSpeed;// previous snake speed
        // run by timer to reset speed (only if speed change was randomed)
        void speedTimerTick(Object o, EventArgs e)
        {
            speedTimer.Stop(); // stop timer
            speedTimer = null;// delete it
            // make snake's speed back to normal
            snake.Velocity.Magnitude = prevSpeed;
            Finalized = true; // set up object for deletion
        }
    }
}
