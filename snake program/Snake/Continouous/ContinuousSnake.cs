using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace snake_program
{
    public partial class ContinuousSnake // snake except with continuous body parts
    {
        // temporary constructor
        public ContinuousSnake(int x, int y, CoreForm _mainForm, Vector _velocity, double size, bool red = false)
        {
            // assign all variables
            this.Size = (int)size;
            this.mainForm = _mainForm;
            this.red = red;
            // find angle to build the body in rads
            double angle = _velocity.Degrees * Math.PI / 180 + Math.PI; // add PI radians to flip the angle around
            // since the build must occur in opposite direction of velocity
            int[] coords = new int[] { x, y }; // coords in proper form to be used
            BodySegment segment = new BodySegment(0, 0, coords, _velocity.Clone(), mainForm); // set size to zero for now
            int dx = (int)(BodyPart.SIZE * size * Math.Cos(angle)); // get change in each direction for the new size
            int dy = (int)(BodyPart.SIZE * size * Math.Sin(angle)); // add negative because coords are reversed on screen
            // whichever is zero will be default width
            if (dx == 0)
            {
                dx = BodyPart.SIZE;
            }
            else
            {
                dy = BodyPart.SIZE;
            }
            segment.grow(dx, dy);
            if (_velocity.Degrees == 180 || _velocity.Degrees == 90) // make the snake head at proper coords
            {
                snakeHead = new BodyPart(segment.X, segment.Y, _velocity.Clone(), mainForm);
            }
            else if (_velocity.Degrees == 0)
            {
                snakeHead = new BodyPart((int)(segment.X + BodyPart.SIZE * size - BodyPart.SIZE), segment.Y, _velocity.Clone(), mainForm);
            }
            else if (_velocity.Degrees == 0)
            {
                snakeHead = new BodyPart(segment.X, (int)(segment.Y + BodyPart.SIZE * size - BodyPart.SIZE), _velocity.Clone(), mainForm);
            }
            FixHead(); // fix head orientation based on velocity
            // add the segment to the bodySegments list to keep track of it
            bodySegments.Add(segment);
            // set the timer event for preventing too fast player responses
            interval.Elapsed += new ElapsedEventHandler(intervalFunc);
            
            // do the red colour thing
            if (red)
            {
                // make the segment red if the red flag is set
                bodySegments[0].picBox.BackColor = Color.Red;
                bodySegments[0].picBox.Image = null; 
            }
            
        }
        // readjust snake head orientation based on velocity angle
        void FixHead()
        {
            // get snake head velocity
            Vector velocity = snakeHead.velocity;
            Image headImage = (Image)Properties.Resources.snake_head.Clone(); // get the image for the snake head
            // if the snake is red, get the red snake head instead
            if (red)
            {
                headImage = (Image)Properties.Resources.snake_headRed.Clone();
            }
            if ((int)velocity.Degrees == 180) // if snake is going left
            {
                headImage.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
            else if ((int)velocity.Degrees == 90)
            {
                headImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            else if (velocity.Degrees == 0)
            {
                headImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            else if ((int)velocity.Degrees == 270 || (int)velocity.Degrees == -90)
            {
                headImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            snakeHead.picBox.Image = headImage; // set the image to the final result
        }
        public void run() // active part of the snake
        {
            snakeHead.run(); // run the snake head
            // keep running snake head after death to keep particles going
            if (!Alive) // but everything else must stop running
            {
                return;
            }
            // run each body segment
            foreach (BodySegment segment in bodySegments)
            {
                // keep velocity magnitude same as snake head
                segment.velocity.Magnitude = Velocity.Magnitude;
                segment.run();
            }
            // runs checks on the last segment
            LastSegmentCheck();
            // reset movement
            correctMovement();
        }
        void LastSegmentCheck()
        {
            // retrieve the last segment
            BodySegment last;
            try
            {
                last = bodySegments[bodySegments.Count - 1];
            }
            catch (ArgumentOutOfRangeException e) //if nothing in the list, just quit the method
            {
                return;
            }

            // if area is equal to or below clip buffer, it must be deleted and snake movement must be reset
            int area = last.picBox.Width * last.picBox.Height;
            if (area <= (overlap * BodyPart.SIZE))
            {
                // remove the last segment
                last.finalize();
                bodySegments.Remove(last);
            }
        }
        public Vector Velocity // leading velocity (same as snakehead velocity)
        {
            get
            {
                return snakeHead.velocity;
            }
        }
        public int Length // length of snake
        {
            get
            {
                // get all body segments' length and add them together*
                if (bodySegments.Count > 0)
                {
                    int sum = bodySegments[0].Length;
                    for (int i = 1; i < bodySegments.Count; i++) // *subtract overlap for every extra body segment because there is an overlap of 2
                    {
                        sum += bodySegments[i].Length;
                        sum -= overlap;
                    }
                    return sum; // return end result
                } else
                {
                    return 0; // return zero because snake must be zero size if there are no segments left over
                }
  
            }
        }
        void addLength(int size) // add a length to the snake in pixels
        {
            BodySegment trailing = bodySegments[bodySegments.Count - 1]; // retrieve the last body segment since we're building off the tail of the snake
            // get the buildup angle
            double buildAngle = trailing.velocity.Degrees * Math.PI / 180 + Math.PI; // flip it around
            // get the change in each dimension
            int dx = (int)(size * Math.Cos(buildAngle));
            int dy = (int)(size * Math.Sin(buildAngle));
            // add the new dimensions
            trailing.grow(dx, dy);
        }
        // remove a length from the snake (uses recursion to get the right body part)
        void removeLength(int size)
        {
            // retrieve the last body part
            BodySegment last;
            try
            {
                last = bodySegments[bodySegments.Count - 1];
            }
            catch (ArgumentOutOfRangeException e)
            {
                return;
            }
            // if the size is less or than the length, simple clipping off the end will do
            if (size <= last.Length)
            {
                // get right dimensions for the shrink (same direction as velocity)
                double angle = last.velocity.Degrees * Math.PI / 180;
                // get the change in each dimension
                int dx = (int)(size * Math.Cos(angle));
                int dy = (int)(size * Math.Sin(angle));
                // shrink it
                last.shrink(dx, dy);
            }
            else // if greater than body part, delete the last one, get the new size to remove and recurse the method
            // with the new size
            {
                // get the new size
                int newSize = size - last.Length;
                // delete the last body part
                last.finalize();
                bodySegments.Remove(last);
                correctMovement();// correct movements with new snake
                // recurs
                removeLength(newSize);
            }
        }
        // change size of the snake by inputted amount
        public void ChangeSize(int change)
        {
            if (change > 0) // if above zero, add the amount
            {
                addLength(change);
            }
            else if (change < 0) // if less than zero, remove that quantity
            {
                removeLength(-change); // negatve sign because the change is less than zero
            }
        }
        // check for any collisions with snake (returns zero if no collision)
        public int Collided(PictureBox collider)
        {
            // if the picturebox is null, obviously no collision
            if (collider == null)
            {
                return 0;
            }
            // final result
            int final = 0;
            if (snakeHead.picBox.Bounds.IntersectsWith(collider.Bounds)) // check for head collision first
            {
                final |= HEAD_COLLISION; // shift the bits into the final result for head collision
            }
            // check for self collision
            // loop through entire snake body to check for collisions
            for (int i = 0; i < bodySegments.Count; i++) // start at 1 to skip the head
            {
                // retrieve current body part
                BodySegment bdPart = bodySegments[i];
                if (bdPart.Bounds.IntersectsWith(collider.Bounds)) // check for collision
                {
                    final |= BODY_COLLISION; // shift the bits for body collision
                    break; // end the loop as no need to check other parts
                }
                // check for self collisions as well (head and body)
                /*if (bdPart.Bounds.IntersectsWith(snakeHead.picBox.Bounds))
                {
                    final |= SELF_COLLISION; // shift bits for head collision
                }*/
            }
            return final;
        }
        // check for any collisions with a gameobject
        public int Collided(GameObject gameObject)
        {
            // final result
            int final = 0;
            if (gameObject.Collided(snakeHead.picBox)) // check for head collision first
            {
                final |= HEAD_COLLISION; // shift the bits into the final result for head collision
            }
            // loop through entire snake body to check for collisions
            for (int i = 1; i < bodySegments.Count; i++) // start at 1 to skip the head
            {
                // retrieve current body part
                BodySegment bdPart = bodySegments[i];
                if (gameObject.Collided(bdPart.picBox)) // check for collision
                {
                    final |= BODY_COLLISION; // shift the bits for body collision
                    break; // end the loop as no need to check other parts
                }
            }
            return final;
        }
        // return which body segment collided with
        public BodySegment CollidedWith(PictureBox pic)
        {
            foreach (BodySegment bdSeg in bodySegments) // check each body segment
            {
                if (pic.Bounds.IntersectsWith(bdSeg.picBox.Bounds)) // check for collision
                {
                    return bdSeg;
                }
            }
            return null; // if reached this point no collision occurred
        }
        // check for self-collision
        public bool SelfCollision()
        {
            for (int i = 2; i < bodySegments.Count; i++) // start at the third body part (index 2) since thats the only
                                                         // logical self-collision
            {
                // retrieve the target body part
                BodySegment target = bodySegments[i];
                // will be tested with head (also only logical means of collision)
                if (snakeHead.picBox.Bounds.IntersectsWith(target.Bounds))
                {
                    // return true as no need to test any longer
                    return true;
                }
            }
            // if the function reached this point, no collision occurred
            return false;
        }
        // glorified destruction - with fireworks!
        public void glorifiedDestroy()
        {
            Alive = false; // since snake will be dead, set the living flag to false
            // destroy all body segments
            while (bodySegments.Count > 0)
            {
                // remove first item until done
                bodySegments[0].finalize();
                bodySegments.RemoveAt(0);
            }
            snakeHead.velocity = new Vector(0, 0); // give it zero velocity to stop it moving
            snakeHead.glorifiedDestroy(); // make the head blow up in a nice way
            // make all the snakeHead's particles red if the snake is supposed to be red
            if (red)
            {
                foreach (BodyPart particle in snakeHead.particles) // loop through all the particles
                {
                    particle.picBox.BackColor = Color.Red; // change the colour to red
                    particle.picBox.Image = null; // remove any picture
                }
            }
        }
        // overlap of body segments at intersection
        int overlap = 4;
        // reference to main form containing the snake
        CoreForm mainForm;
        // reference to head of the snake (its a body part)
        public BodyPart snakeHead;
        public int Size; // size of the snake (in pixels) (size of all body segments excluding overlapped regions
        // list of all body segments
        public List<BodySegment> bodySegments = new List<BodySegment>();
        // if true, the snake should be red
        public bool red = false;
        // true if the snake is alive
        public bool Alive = true;
        public const int HEAD_COLLISION = 0b00000100; // constant that means only snakehead collision
        public const int BODY_COLLISION = 0b00001000; // constant that means non-snakehead collision
        public const int SELF_COLLISION = 0b00010000;
    }
}
