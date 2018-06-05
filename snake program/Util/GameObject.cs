using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace snake_program
{
    public class GameObject
    {
        // create game object with an array of picture boxes representing bounds
        public GameObject(PictureBox[] bounds)
        {
            Bounds = new List<PictureBox>(bounds);
        }
        public void addBounds(PictureBox[] bounds) // add bounds from an array of pic boxes
        {
            foreach (PictureBox pic in bounds) // loop through boxes
            {
                Bounds.Add(pic);
            }
        }
        public void removeBound(PictureBox bound) // remove a bound represented by a picturebox
        {
            Bounds.Remove(bound);
        }
        public static bool NotNull(Object o) // function that allows to test any reference for being null
        {
            return o != null;
        }
        public bool Collided(PictureBox testBox) // check for colliison with bounds
        {
            foreach (PictureBox pic in Bounds) // test all pic box bounds for collisions
            {
                if (pic.Bounds.IntersectsWith(testBox.Bounds))
                {
                    return true; // true if intersection happened
                }
            }
            return false; // return false since no intersection if reached this point
        }
        public bool Collided(GameObject testObject) // checkk for collision with game object
        {
            foreach (PictureBox pic in Bounds) // test all pic bounds for collisions
            {
                if (testObject.Collided(pic))
                {
                    return true;
                }
            }
            return false; // no collision if reached this point
        }
        // boundaries for the object
        public List<PictureBox> Bounds;
    }
}
