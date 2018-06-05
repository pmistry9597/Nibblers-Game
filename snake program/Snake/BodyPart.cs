using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace snake_program
{
    public class BodyPart
    {

        public BodyPart(int x, int y, Vector _velocity, Form mainForm) // create body part with x and y coords and velocity
        {
            picBox = new PictureBox
            {
                Name = "picBox",
                Size = new Size(SIZE, SIZE),
                Location = new Point(x, y),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            //picBox.Image = Image.FromFile(IMAGE_LOCATION); // set image
            picBox.Image = Properties.Resources.green_box_hi;
            mainForm.Controls.Add(picBox);

            this.velocity = _velocity;
            this.mainForm = (CoreForm)mainForm;
            mainForm.Controls.SetChildIndex(picBox, 4);// set proper z order
        }
        // glorified destruction of body part
        public void glorifiedDestroy(Nullable<Color> color = null)
        {
            particles = new List<BodyPart>(); // make new list to hold particles
            particleLife = new System.Timers.Timer(1500); // make new timer for lifetime of particles (CURRENTLY 1 SECOND)
            particleLife.Elapsed += new System.Timers.ElapsedEventHandler(partLifeEnd);
            // get the coords of the center of the picture box for this body part
            int x = picBox.Location.X + picBox.Width / 2;
            int y = picBox.Location.Y + picBox.Height / 2;
            // random number generator for velocity
            Random rand = new Random();
            // height and width of new body parts to act as particles
            int size = 3;
            int quantity = rand.Next(20, 30);// number of particles // 35, 50
            // generate a body part to fly off at every eighth of a circle
            for (int i = 1; i <= quantity; i++)
            {
                int angle = (int)(360 * i / 8); // get the angle (in degrres)
                int magnitude = rand.Next(3, 10);// generate random velocity magnitude
                BodyPart particle = new BodyPart(x, y, new Vector(magnitude, angle), mainForm);
                particle.picBox.Size = new Size(size, size);
                // change color if not null
                if (color != null)
                {
                    particle.picBox.Image = null;
                    particle.picBox.BackColor = color.Value;
                }
                particles.Add(particle); // add it to the list of particles
            }
            // make the main image box invisible
            picBox.Visible = false;
            // start the particle lifetime
            particleLife.Start();
        }
        void partLifeEnd(Object o, EventArgs e) // ending event for timer to stop ticking
        {
            particleLife.Stop(); // end the timer
        }
        // this will run every frame
        public void run()
        {
            double y = picBox.Location.Y;
            y -= velocity.Y; // direction must be reversed as y is positive going down
            double x = picBox.Location.X;
            x += velocity.X;

            // new position
            picBox.Location = new Point((int)x, (int)y);
            // run particles (if not null)
            if (particles != null)
            {
                // if timer is not running anymore, destroy all the particles
                if (!particleLife.Enabled)
                {
                    // kill all the particles since lifetime is over
                    while (particles.Count > 0)
                    {
                        BodyPart particle = particles[0]; // get current particle
                        mainForm.removeQueue.Enqueue(particle.picBox); // put it into queue of things to be removed
                        particles.Remove(particle); // destroy it from the list
                    }
                }
                foreach (BodyPart particle in particles) // loop through all the particles
                {
                    particle.run();// run each particle
                }
            }
        }

        // constant for size of picturebox (it's a square)
        public static int SIZE = 18;
        // default size for body part
        public const int DEFAULT_SIZE = 18;
        // stores velocity vector
        public Vector velocity;
        // picturebox for body part
        public PictureBox picBox;
        // reference to main form
        public CoreForm mainForm;
        // list of particles (for glorified destroy)
        public List<BodyPart> particles;
        // timer for duration of particle life
        public System.Timers.Timer particleLife;
        // reference to default image for body part
        public static Image DEFAULT_IMAGE = Properties.Resources.green_box_hi;
    }
}
