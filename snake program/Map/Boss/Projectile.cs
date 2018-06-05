using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace snake_program
{
    public class Projectile
    {
        public Projectile(PictureBox picBox, Vector velocity, int lifetime, CoreForm form)
        {
            picBox.Size = new System.Drawing.Size(BodyPart.SIZE, BodyPart.SIZE);
            picBox.Image = Properties.Resources.projectile;
            picBox.SizeMode = PictureBoxSizeMode.StretchImage;
            // above sets up the image for the projectile
            this.picBox = picBox;
            mainForm = form;
            this.velocity = velocity;

            // setup life timer to end life when inputted lifetime is over
            lifeTimer = new Timer();
            lifeTimer.Interval = lifetime;
            lifeTimer.Tick += new EventHandler(endLifeTick);
            lifeTimer.Start();
        }
        // end lifetime timer
        void endLifeTick(Object o, EventArgs e)
        {
            lifeTimer.Stop(); // stop ticking, no longer needed
            finalize(); // kill the projectile
        }
        public void finalize()
        {
            mainForm.removeQueue.Enqueue(picBox);
            finalized = true;
        }
        public void Run(List<Obstacle> obstacles)
        {
            if (finalized) // dont do anything if finalized
            {
                return;
            }
            // check for collisions with obstacles
            foreach (Obstacle obstacle in obstacles)
            {
                // get collision area with obstacle
                Rectangle collision = Rectangle.Intersect(picBox.Bounds, obstacle.picBox.Bounds);
                // change velocity only if collided
                if (collision != Rectangle.Empty)
                {
                    if (collision.Width > collision.Height)
                    {
                        velocity.Y = -velocity.Y;
                    } else if (collision.Width < collision.Height)
                    {
                        velocity.X = -velocity.X;
                    }
                }
            }
            int x = (int)(picBox.Location.X + velocity.X);
            int y = (int)(picBox.Location.Y - velocity.Y);
            picBox.Location = new System.Drawing.Point(x, y);
        }
        // velocity vector
        Vector velocity;
        // picturebox that represents this object
        public PictureBox picBox;
        // reference to form this is in
        CoreForm mainForm;
        // timer that represents life time
        Timer lifeTimer;
        // true when this should be deleted
        public bool finalized = false;
    }
}
