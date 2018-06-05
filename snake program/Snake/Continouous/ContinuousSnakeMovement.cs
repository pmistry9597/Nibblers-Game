using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace snake_program
{
    public partial class ContinuousSnake // movement stuff goes here
    {
        // movement corrector - with multiple segments, first segment must be growth and last must be shrink
        // in single segment cases - first is always moving
        void correctMovement()
        {
            if (bodySegments.Count > 1)
            { // if greater than one
                bodySegments[0].SetState(BodySegment.SnakeModeModel.GROW); // first one must be growing
                for (int i = 1; i < (bodySegments.Count - 1); i++) // loop through all in between
                {
                    bodySegments[i].SetState(BodySegment.SnakeModeModel.STATIONARY); // all in between are stationary
                }
                bodySegments[bodySegments.Count - 1].SetState(BodySegment.SnakeModeModel.SHRINK); // last one is shrinking
            }
            else
            {
                try
                {
                    bodySegments[0].SetState(BodySegment.SnakeModeModel.MOVE);// first one must be moving since its the only segment
                }
                catch (ArgumentOutOfRangeException e)
                {
                    // do nothing if non existent
                }
            }
        }
        void turn(double degrees) // turn in a certain direction
        {
            // dont turn if dead or timer from last time is still ticking
            if (!Alive && !interval.Enabled)
            {
                return;
            }
            // dont turn if the front-most body segment length is not equal or greater than two times the standard
            // body part size
            try
            {
                if ((2 * BodyPart.SIZE) > bodySegments[0].Length)
                {
                    return;
                }
            } catch (IndexOutOfRangeException e)
            {
                return;
            }
            // create new body segment with velocity upward
            // coords for the segment (same as head)
            int[] coords = new int[] { snakeHead.picBox.Location.X, snakeHead.picBox.Location.Y };
            Vector newVelocity = new Vector(Velocity.Magnitude, degrees);// velocity for new segment (same magnitude but different velocity)
            BodySegment newSegment = new BodySegment(BodyPart.SIZE, BodyPart.SIZE, coords, newVelocity, mainForm);
            // make the new segment red if the snake is red
            if (red)
            {
                newSegment.picBox.BackColor = System.Drawing.Color.Red;
                newSegment.picBox.Image = null;
            }
            bodySegments.Insert(0, newSegment); // add it as the first item
            clipPrev();// clip off the previous first item to correct for size
            snakeHead.velocity = newVelocity.Clone(); // snakehead velocity same as the first part
            correctMovement(); // correct all movement
            interval.Start(); // start timer
            FixHead();// correct snake head
        }
        void clipPrev()
        {
            // get the previous item (second item)
            BodySegment previous = bodySegments[1];
            double removeAngle = previous.velocity.Degrees * Math.PI / 180 + Math.PI; // angle to remove material (add pi to flip around)
            // amount to shrink in each dimension
            int dx = (int)((BodyPart.SIZE - overlap) * Math.Cos(removeAngle));
            int dy = (int)((BodyPart.SIZE - overlap) * Math.Sin(removeAngle));
            previous.shrink(dx, dy); // shrink it

            // clip the collider for this segment to the proper size
            // amount to shrink in each dimension
            dx = (int)((BodyPart.SIZE) * Math.Cos(removeAngle));
            dy = (int)((BodyPart.SIZE) * Math.Sin(removeAngle));
            previous.shrinkBounds(dx, dy); // shrink it
        }
        public void Up() // force movement upwards
        {
            turn(90); // turn at 90 degrees, which is upward
        }
        public void Right() // force movement rightward
        {
            turn(0); // 0 degrees is right
        }
        public void Down() // force movement downward
        {
            turn(270); // 270 degrees is down
        }
        public void Left() // force movement righward
        {
            turn(180); // 180 degrees is left
        }
        // turn request methods - these ensure only logical turns are made
        public void RequestUp()
        {
            // if the velocity angle is up or down, the cosine of it will always be zero
            bool notUpOrDown = (int)Math.Cos(Velocity.Degrees * Math.PI / 180) != 0;
            if (notUpOrDown)
            {
                Up();
            }
        }
        public void RequestDown()
        {
            // if the velocity angle is up or down, the cosine of it will always be zero
            bool notUpOrDown = (int)Math.Cos(Velocity.Degrees * Math.PI / 180) != 0; // int cast because
            // the angle is not perfect and often returns a view decimal points from zero
            if (notUpOrDown)
            {
                Down();
            }
        }
        public void RequestRight()
        {
            // if the velocity angle is left or right, the sine of it will always be zero
            bool notLeftorRight = (int)Math.Sin(Velocity.Degrees * Math.PI / 180) != 0; // int cast because
            // the angle is not perfect and often returns a view decimal points from zero
            if (notLeftorRight)
            {
                Right();
            }
        }
        public void RequestLeft()
        {
            // if the velocity angle is left or right, the sine of it will always be zero
            bool notLeftorRight = (int)Math.Sin(Velocity.Degrees * Math.PI / 180) != 0; // int cast because
            // the angle is not perfect and often returns a view decimal points from zero
            if (notLeftorRight)
            {
                Left();
            }
        }

        void intervalFunc(Object o, EventArgs e) // for timer that stop too fast player movement
        {
            interval.Stop(); // stop the timer since its one shot
        }
        // this timer will be used to stop users from making moves too fast (no self collisions)
        Timer interval = new Timer(200);
    }
}
