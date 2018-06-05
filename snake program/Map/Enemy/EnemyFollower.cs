using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace snake_program
{
    public class EnemyFollower
    {
        public EnemyFollower(PictureBox picBox, int speed, CoreForm form)
        {
            Setup(picBox, form, speed);
        }
        public EnemyFollower(int x, int y, int speed, CoreForm form)
        {
            PictureBox picBox = new PictureBox
            {
                Location = new System.Drawing.Point(x, y), 
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            // add it to the form
            form.Controls.Add(picBox);
            Setup(picBox, form, speed);
        }
        public void Setup(PictureBox picBox, CoreForm form, int speed)
        {
            picBox.Size = new System.Drawing.Size(BodyPart.SIZE, BodyPart.SIZE);
            // moar setup here
            mainForm = form;
            this.picBox = picBox;
            velocityMag = speed;
        }

        public void Run(List<ContinuousSnake> snakes, List<Obstacle> obstacles)
        {
            ContinuousSnake targetSnake = null; // target snake
            int targetDistance = 0;
            foreach (ContinuousSnake snake in snakes)
            {
                if (targetSnake == null)
                {
                    targetSnake = snake;
                    // replace target with shortest distance to it's head
                    int thisX = picBox.Location.X + picBox.Width / 2;
                    int thisY = picBox.Location.Y + picBox.Height / 2;
                    int snakeX = snake.snakeHead.picBox.Location.X + snake.snakeHead.picBox.Width / 2;
                    int snakeY = snake.snakeHead.picBox.Location.Y + snake.snakeHead.picBox.Height / 2;
                    // get the distance
                    int deltaX = snakeX - thisX;
                    int deltaY = snakeY - thisY;
                    int distance = (int)(0.5 + Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2)));
                    targetDistance = distance;
                } else
                {
                    /*// replace target with shortest distance to it's head
                    int thisX = picBox.Location.X + picBox.Width / 2;
                    int thisY = picBox.Location.Y + picBox.Height / 2;
                    int snakeX = snake.snakeHead.picBox.Location.X + snake.snakeHead.picBox.Width / 2;
                    int snakeY = snake.snakeHead.picBox.Location.Y + snake.snakeHead.picBox.Height / 2;
                    // get the distance
                    int deltaX = snakeX - thisX;
                    int deltaY = snakeY - thisY;
                    int distance = (int) (0.5 + Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2)));
                    // if the distance is less, replace the snake and the distance
                    if (distance < targetDistance)
                    {
                        targetSnake = snake;
                        targetDistance = distance;
                    }*/
                }
            }
            ContinuedRun(targetSnake.snakeHead, snakes, obstacles);
        }

        void ContinuedRun(BodyPart snakeHead, List<ContinuousSnake> snakes, List<Obstacle> obstacles)
        {
            // get x and y comps of velocity
            double thisX = picBox.Location.X + (double)picBox.Width / (double)2;
            double thisY = picBox.Location.Y + (double)picBox.Height / (double)2;
            double snakeX = snakeHead.picBox.Location.X + (double)snakeHead.picBox.Width / (double)2;
            double snakeY = snakeHead.picBox.Location.Y + (double)snakeHead.picBox.Height / (double)2;
            // get the comps
            double deltaX = snakeX - thisX;
            double deltaY = snakeY - thisY;
            // scale them according to target velocity magnitude
            double triMag = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
            deltaX = deltaX * velocityMag / triMag;
            deltaY = deltaY * velocityMag / triMag;

            // run checks here for obstacles
            foreach (Obstacle obstacle in obstacles)
            {
                // get the rectangle that represents collision area with an obstacle
                Rectangle collision = Rectangle.Intersect(obstacle.picBox.Bounds, picBox.Bounds);
                if (collision != Rectangle.Empty) // only run if collision is existent
                {
                    if (collision.Width > collision.Height)
                    {
                        deltaY = 0;
                        // push the enemy out by the height
                        if (collision.Y > picBox.Location.Y)
                        {
                            picBox.Location = new Point(picBox.Location.X, picBox.Location.Y - collision.Height - 1);
                        } else if (collision.Y < picBox.Location.Y)
                        {
                            picBox.Location = new Point(picBox.Location.X, picBox.Location.Y + collision.Height + 1);
                        }
                    } else if (collision.Height > collision.Width)
                    {
                        deltaX = 0;
                        if (collision.X > picBox.Location.X)
                        {
                            picBox.Location = new Point(picBox.Location.X - collision.Width - 1, picBox.Location.Y);
                        }
                        else if (collision.Y < picBox.Location.Y)
                        {
                            picBox.Location = new Point(picBox.Location.X + collision.Width + 1, picBox.Location.Y);
                        }
                    } else
                    {
                        //deltaX = 0;
                        //deltaY = 0;
                    }
                }
            }
            // run checks here for snakes
            foreach (ContinuousSnake snake in snakes)
            {
                // get body segment that the enemy collided with
                BodySegment segment = snake.CollidedWith(picBox);
                // end the current loop if no segment returned
                if (segment == null)
                {
                    continue;
                }
                // get the rectangle that represents collision area with an obstacle
                Rectangle collision = Rectangle.Intersect(segment.picBox.Bounds, picBox.Bounds);
                if (collision != Rectangle.Empty) // only run if collision is existent
                {
                    if (collision.Width > collision.Height)
                    {
                        deltaY = 0;
                    }
                    else if (collision.Height > collision.Width)
                    {
                        deltaX = 0;
                    }
                    else
                    {
                        //deltaX = 0;
                        //deltaY = 0;
                    }
                }
            }

            // move the picturebox
            picBox.Location = new System.Drawing.Point((int)(picBox.Location.X + deltaX), (int)(picBox.Location.Y + deltaY));
        }

        // reference to the engine
        public GameEngine engine;
        // reference to main form
        public CoreForm mainForm;
        // picturebox that represents the enemy
        public PictureBox picBox;
        // true if enemy should move
        public bool Active = true;
        // store target velocity magnitude
        public int velocityMag;
    }
}
