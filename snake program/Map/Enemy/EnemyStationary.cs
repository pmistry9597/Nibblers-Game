using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace snake_program
{
    public class EnemyStationary : Enemy
    {
        public EnemyStationary(PictureBox picBox, int threshold, CoreForm form, Image img = null) // optional image to set for picBox
        {
            // assign internal vars for later use
            Bounds = picBox;
            this.threshold = threshold;
            this.form = form;
            // if image parameter was inputted, change the picturebox image to the inputted image
            if (img != null)
            {
                Bounds.Image = img;
            }
            // make animation display for a second (until enemy image returns to normal)
            animaTimer.Interval = 1000;
            animaTimer.Tick += new EventHandler(resetImage);// set event for timer to reset image
        }
        void resetImage(Object o, EventArgs e) // event for the animation timer (returns image to normal)
        {
            Bounds.Image = Properties.Resources.enemy_guardian;
            animaTimer.Stop(); // stop the timer from ticking since it should only be one shot
        }
        public override void Death() // for when the enemy dies
        {
            base.Death();
            // make the enemy invisible
            Bounds.Visible = false;
            // make particle containe and particles
            int x = Bounds.Location.X + (Bounds.Width - BodyPart.SIZE) / 2; int y = Bounds.Location.Y + (Bounds.Height - BodyPart.SIZE) / 2;
            particleContainer = new BodyPart(x, y, new Vector(0, 0), form);
            particleContainer.picBox.Visible = false;
            particleContainer.glorifiedDestroy(Color.Black); // make black particles
        }
        public void Run()
        {
            if (particleContainer != null) // particleContainer is null until death occurs
            {
                particleContainer.run(); // run the particles
            }
        }
        public override void Attack()
        {
            // put in the attack image
            Bounds.Image = Properties.Resources.enemy_guardianAttack;
            animaTimer.Stop();
            animaTimer.Start(); // start the timer for the animation
        }
        // timer to return enemy pic to normal pic
        public Timer animaTimer = new Timer();
        // reference to body part for explosion
        BodyPart particleContainer;
    }
}