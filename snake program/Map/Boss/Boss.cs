using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snake_program
{
    public class Boss
    {
        public Boss(PictureBox picBox, int interval, int lifetime, CoreForm form) // angle is average of all projectile velocity angles, interval is time between spawns
        {
            TargetTime = interval;
            mainForm = form;
            projectileLife = lifetime;
            picBox.Image = Properties.Resources.boss;
            picBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.picBox = picBox;
        }
        // lifetime of projectiles
        int projectileLife = 2000;
        // counter for time between projectile spawns
        int increment = 0;
        // target time between intervals
        int TargetTime;
        public void Run()
        {
            if (particles != null) // run the particles container if it exists
            {
                particles.run();
            }
            if (!Alive) // don't run if the boss is dead
            {
                return;
            }
            if (increment >= TargetTime) // timer overflow - time to reset and spawn projectiles
            {
                increment = 0;
                // get spawn x and y coords for projectiles
                int spawnX = picBox.Location.X + picBox.Width / 2 - BodyPart.SIZE / 2; // subtract body part size to accomodate for the projectile's size to center it
                int spawnY = picBox.Location.Y + picBox.Height + 10;
                // get angle starting point
                int startingAngle = 180; // 180 because that is total left side, and that is one point of the entire sweep toward the bottom of the form
                
                // get random number of projectiles to spawn
                int quantity = mainForm.engine.GetRandom(3, 6);
                double iAngle = (double)180 / (double)quantity; // get increment angle
                for (int i = 0; i <= quantity; i++)
                {
                    Console.WriteLine("Spawning projectile!");
                    double angle = startingAngle + i * iAngle;
                    // make a new picturebox for the projectile
                    PictureBox projectilePic = new PictureBox
                    {
                        Location = new System.Drawing.Point(spawnX, spawnY)
                    };
                    mainForm.Controls.Add(projectilePic);
                    Projectile projectile = new Projectile(projectilePic,new Vector(5,angle),projectileLife, mainForm);
                    mainForm.engine.projectiles.Add(projectile);// register the projectile with the engine
                }
            }
            increment++;
        }
        // glorified destruction of the boss
        public void glorifiedDestroy()
        {
            if (Alive)
            {
                particles = new BodyPart(picBox.Location.X + picBox.Width / 2, picBox.Location.Y + picBox.Height / 2, new Vector(0, 0), mainForm);
                particles.glorifiedDestroy(Color.Black);
                // make all the particles black
                foreach (BodyPart particle in particles.particles)
                {
                    particle.picBox.BackColor = Color.Black;
                }
                // remove the boss from the form
                mainForm.removeQueue.Enqueue(picBox);
                Alive = false;
            } else
            {
                return;
            }
        }
        // picture that represents the boss
        public PictureBox picBox;
        // reference to main form
        CoreForm mainForm;
        // container for the particles in the boss's death
        BodyPart particles;
        // true if the boss if alive
        public bool Alive = true;
    }
}
