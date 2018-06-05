using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace snake_program
{
    public class BodySegment // body part for continuous snake
    {
        public BodySegment(int width, int height, int[] coords, Vector velocity, CoreForm form)
        {
            picBox = new PictureBox // new picture box for the visual part of this segment
            {
                Name = "picBox",
                Size = new Size(width, height),
                Location = new Point(coords[0], coords[1]),
                Image = BodyPart.DEFAULT_IMAGE
            };
            picBox.SizeMode = PictureBoxSizeMode.StretchImage; // make the picture scale the image
            form.Controls.Add(picBox); // add it to the form
            Bounds = new Rectangle(new Point(coords[0], coords[1]), new Size(width, height));// create the rectangle for collisions
            this.velocity = velocity; // save the velocity
            mainForm = form; // save the form for later
            mainForm.Controls.SetChildIndex(picBox, 20);// set proper z order
        }
        public void finalize() // cleanup for this object
        {
            mainForm.removeQueue.Enqueue(picBox); // add the picture box to queue of things to be deleted
        }
        void move() // for when the segment is in movement mode
        {
            int x = (int)(picBox.Location.X + velocity.X); // new x coord
            int y = (int)(picBox.Location.Y - velocity.Y); // new y coord (subtract because y is upside in screen coords)

            picBox.Location = new Point(x, y);// change the location of the picbox
            Bounds.Location = new Point(x, y); // change the collision bounds as well
        }

        public void shrinkBounds(int dx, int dy) // for shrink
        {
            int width = (int)(Bounds.Width - Math.Abs(dx));// new width for the segment
            int height = (int)(Bounds.Height - Math.Abs(dy)); // new height for the segment
            Bounds.Size = new Size(width, height); // set the new dimensions

            // -- start of the real stuff
            int x = Bounds.Location.X;
            int y = Bounds.Location.Y;
            if (dx > 0) // if above zero, the xposition needs to change
            {
                x += (int)(dx);
            }
            if (dy < 0) // if above zero, the y position needs to change
            {
                y -= (int)(dy);
            }
            // new position
            Bounds.Location = new Point(x, y);
        }
        public void shrink(int dx, int dy) // shrink collision rectangle
        {
            int width = (int)(picBox.Width - Math.Abs(dx));// new width for the segment
            int height = (int)(picBox.Height - Math.Abs(dy)); // new height for the segment
            picBox.Size = new Size(width, height); // set the new dimensions

            // -- start of the real stuff
            int x = picBox.Location.X;
            int y = picBox.Location.Y;
            if (dx > 0) // if above zero, the xposition needs to change
            {
                x += (int)(dx);
            }
            if (dy < 0) // if above zero, the y position needs to change
            {
                y -= (int)(dy);
            }
            // new position
            picBox.Location = new Point(x, y);
        }


        public void grow(int dx, int dy) // for growth mode
        {
            int width = (int)(picBox.Width + Math.Abs(dx));// new width for the segment
            int height = (int)(picBox.Height + Math.Abs(dy)); // new height for the segment
            picBox.Size = new Size(width, height); // set the new dimensions
            Bounds.Size = new Size(width, height); // set the new dimensions

            // -- start of the real stuff
            int x = picBox.Location.X;
            int y = picBox.Location.Y;
            if (dx < 0) // if above zero, the xposition needs to change
            {
                x += (int)(dx);
            }
            if (dy > 0) // if less than zero, the y position needs to change
            {
                y -= (int)(dy);
            }
            // new position
            picBox.Location = new Point(x, y);
            Bounds.Location = new Point(x, y);
        }

        void growActive() // active growth mode
        {
            grow((int)velocity.X, (int)velocity.Y);
        }

        void shrinkActive() // shrink mode for snake run
        {
            shrink((int)velocity.X, (int)velocity.Y);
            shrinkBounds((int)velocity.X, (int)velocity.Y);
        }

        public void SetState(SnakeModeModel state) // set state of snake from outside
        {
            this.state = state;
        }

        public void run() // active part of body segment object (movement, shrink, etc)
        {
            // action depends on the body segment's state
            switch (state)
            {
                case SnakeModeModel.GROW: // growth mode if set to grow
                    growActive();
                    break;
                case SnakeModeModel.MOVE:
                    move();
                    break;
                case SnakeModeModel.SHRINK:
                    shrinkActive();
                    break;
            }
        }
        public int Width // public accessor for width
        {
            get
            {
                return picBox.Width;
            }
        }
        public int Height // public accessor for height
        {
            get
            {
                return picBox.Height;
            }
        }
        public int Length
        {
            get
            {
                // return the number that isn't equal to BodyPart.SIZE
                return Width == BodyPart.SIZE ? Height : Width;
            }
        }
        public int X
        {
            get
            {
                return picBox.Location.X;
            }
        }
        public int Y
        {
            get
            {
                return picBox.Location.Y;
            }
        }
        public SnakeModeModel State
        {
            get
            {
                return this.state;
            }
        }
        // rectangle for collisions
        public Rectangle Bounds;
        // picturebox that represents the snake segment
        public PictureBox picBox;
        // reference to the form
        public CoreForm mainForm;
        // velocity of the segment (also used for shrinking)
        public Vector velocity;
        // enumerator that represents state of snake
        public enum SnakeModeModel { SHRINK = -1, MOVE = 0, GROW = 1, STATIONARY };
        // state of the snake
        SnakeModeModel state = SnakeModeModel.MOVE;
    }
}
