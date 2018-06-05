using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snake_program
{
    partial class Snake
    {
        // ---- SIZE CHANGING CODE
        // for adding more body segments
        public void changeSize(int change)
        {
            // figure out if to add or remove parts
            if (change > 0) // if above zero, add segments
            {
                addParts(change);
            } else if (change < 0) // if less, remove parts
            {
                // make sure the change does not cause errors due to the change being bigger than snake size (without the head)
                if (Size < -change)
                {
                    return;
                }
                // find index of the front most part to be removed
                int frontIndex = bodyParts.Count() + change; // add it instead of subtract because the change is negative
                clip(frontIndex); // clip from this index
            }
        }
        void addParts(int count) // add certain number of parts to snake
        {
            for (int i = 1; i <= count; i++) 
            {
                BodyPart trailing = bodyParts.Last();// get last body part
                // get direction of buildup for the body part
                double buildUpAngle = trailing.velocity.Degrees * Math.PI / 180 + Math.PI; // pi is added to to build in opposite
                                                                                           // angle of velocity
                int xOffset = (int)((BodyPart.SIZE + offset) * Math.Cos(buildUpAngle));
                int yOffset = (int)((BodyPart.SIZE + offset) * -Math.Sin(buildUpAngle));
                int xCord = trailing.picBox.Location.X; // get x cord of last segment
                int yCord = trailing.picBox.Location.Y; // get y cord of last segment
                Vector lastVelocity = trailing.velocity.Clone();// get velocity of previous part
                BodyPart newPart = new BodyPart(xCord + xOffset, yCord + yOffset, lastVelocity, mainForm); // change this depending on velocity direction
                // add the new part
                bodyParts.Add(newPart);
            }
        }
        // clip the snake off at a certain point (removes the trailing not the leading part after that clip point)
        public void clip(int index)
        {
            for (int i = (bodyParts.Count() - 1); i >= index; i--) // start the the proper index, and only run if its within the snake's bounds
            {
                Console.WriteLine(bodyParts.Count());
                BodyPart bdPart = bodyParts[i]; // get the body part
                // tell the main form to delete unused picboxes
                mainForm.removeQueue.Enqueue(bdPart.picBox);
                bodyParts.Remove(bdPart);
            }
        }
        // glorified clip (explosion of particles)
        public void glorifiedDestroy()
        {
            clip(0);
            fireworks = new BodyPart(snakeHead.picBox.Location.X, snakeHead.picBox.Location.Y, new Vector(0, 0), mainForm);
            fireworks.glorifiedDestroy();
            Dead = true; // tell everyone this is dead
        }

        // ----- END OF SIZE CHANGING CODE
        public void run(double time) // time is interval between each trigger of this function
        {                           //
            if (fireworks != null) // if firework is not null, the snake was destroyed in glorified fashion
            {
                fireworks.run();
                // check if particles are done
                if (!(fireworks.particles.Count > 0))
                {
                    // delete it now the particles have finished
                    fireworks = null;
                }
            }
            // loop throuhg all the snakes and trigger their run functions
            foreach (BodyPart bdPart in bodyParts)
            {
                bdPart.run(); // each bodypart moves
            }
            // check each turn point's current to-be-checked body part is ready to be influenced
            int i = 0;
            while (i < turnPoints.Count()) // loop through all turn points
            {
                TurnPoint tp = turnPoints[i];
                // retrieve the current body part to-be-checked
                BodyPart bdPart;
                try
                {
                    bdPart = bodyParts[tp.PartsCount];
                } catch (ArgumentOutOfRangeException e) // for when the clipping occurs randomly
                {
                    // delete the turn point
                    tp.finalizer(); // call ending function in the turnpoint
                    turnPoints.Remove(tp); // don't increment i so we dont skip an item as we just removed one
                    continue;
                }
                // put bdPart the coords in an array for checking
                int[] picCoords = new int[] { bdPart.picBox.Location.X, bdPart.picBox.Location.Y };
                // if the part occupies the turning point, change its velocity
                //if (picCoords.SequenceEqual(tp.Coords)) // check if coords are same HERE
                if (tp.Crossed(bdPart))
                {
                    // change the direction of the body part's velocity
                    bdPart.velocity.Degrees = tp.Angle;
                    // add to the count
                    tp.PartsCount += 1;
                    fixPos(bdPart);
                    Console.WriteLine("Turning point collision!");
                }
                // delete the turn point if all the parts have been influenced
                if (tp.PartsCount >= bodyParts.Count())
                {
                    // call ending function in the turnpoint
                    tp.finalizer();
                    turnPoints.Remove(tp); // don't increment i so we dont skip an item as we just removed one
                } else
                {
                    i++; // incrememnt i because there is no risk of skipping a turn point here
                }
            }
        }
        
        // constructor
        public Snake(Form _mainForm, Vector _velocity, int _offset, int size)
        {
            // assigning vars blah blah bla
            this.velocity = _velocity.Clone();
            this.offset = _offset; // double the offset to make it even
            this.mainForm = (Form1)_mainForm;
            bodyParts = new List<BodyPart>();
            turnPoints = new List<TurnPoint>();
            // end of blah blah            
            // coords of head
            int xCord = this.mainForm.Width / 2;
            int yCord = this.mainForm.Height / 2;

            // add it to the List and it is first added so zero index will be the head
            bodyParts.Add(new BodyPart(xCord, yCord, velocity, mainForm));
            snakeHead = bodyParts[0];

            // add other body parts
            changeSize(size);
        }

        // access size of snake
        public int Size
        {
            get
            {
                return this.bodyParts.Count();
            }
        }

        // check for any collisions with snake (returns zero if no collision)
        public int Collided(PictureBox collider)
        {
            // final result
            int final = 0;
            if (snakeHead.picBox.Bounds.IntersectsWith(collider.Bounds)) // check for head collision first
            {
                final |= HEAD_COLLISION; // shift the bits into the final result for head collision
            }
            // loop through entire snake body to check for collisions
            for (int i = 1; i < bodyParts.Count(); i++) // start at 1 to skip the head
            {
                // retrieve current body part
                BodyPart bdPart = bodyParts[i];
                if (bdPart.picBox.Bounds.IntersectsWith(collider.Bounds)) // check for collision
                {
                    final |= BODY_COLLISION; // shift the bits for body collision
                    break; // end the loop as no need to check other parts
                }
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
            for (int i = 1; i < bodyParts.Count(); i++) // start at 1 to skip the head
            {
                // retrieve current body part
                BodyPart bdPart = bodyParts[i];
                if (gameObject.Collided(bdPart.picBox)) // check for collision
                {
                    final |= BODY_COLLISION; // shift the bits for body collision
                    break; // end the loop as no need to check other parts
                }
            }
            return final;
        }
        
        public const int HEAD_COLLISION = 0b00000100; // constant that means only snakehead collision
        public const int BODY_COLLISION = 0b00001000; // constant that means non-snakehead collision
        // offset distance between body parts
        int offset;
        public BodyPart snakeHead;// snakeHead reference
        Form1 mainForm;// reference to main form that holds the snake
        Vector velocity;// velocity of the snake
        // list of all body parts, where 0 is head and last is the trailing-most body part
        List<BodyPart> bodyParts;
        // list of all turning points
        List<TurnPoint> turnPoints; // each array will store 2 numbers, first is x last is y
        // reference for body part to explode
        BodyPart fireworks;
        // true if dead
        public bool Dead;
    }
}
