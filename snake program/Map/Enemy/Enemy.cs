using System;
using System.Windows.Forms;

namespace snake_program
{
    public abstract class Enemy
    {
        // true if snake can eat the enemy
        public bool CanEat(ContinuousSnake snake)
        {
            double unitsLength = (double)snake.Length / BodyPart.SIZE;// get the snake length in body part size units
            return unitsLength >= threshold;
        }
        // check for head collisions with snake
        public bool HeadCollision(ContinuousSnake snake)
        {
            return (snake.Collided(Bounds) & ContinuousSnake.HEAD_COLLISION) > 0; // true if head collision occurred
        }
        // check for body collision with snake
        public bool BodyCollision(ContinuousSnake snake)
        {
            return (snake.Collided(Bounds) & ContinuousSnake.BODY_COLLISION) > 0; // true if body collision occurred
        }
        // for when the snake attempts to eat the enemy (returns true if eat was successful)
        public bool AttemptEat(ContinuousSnake snake)
        {
            if (CanEat(snake)) // make sure the snake can kill the enemy
            {
                // run death animations/functions blahblahblah
                Death();
                return true;
            }
            else
            {
                // run attack animations/functions
                Attack();
                return false;
            }
        }
        public virtual void Death() { Alive = false; } // run when the enemy dies
        public abstract void Attack(); // run for when the enemy attacks
        // threshold for snake to eat the enemy (in body part size units)
        public int threshold;
        // picture that represents bounds of the enemy
        public PictureBox Bounds;
        //reference to main form
        public CoreForm form;
        // true if the enemy is alive
        public bool Alive = true;
    }
}
