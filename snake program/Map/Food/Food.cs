using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace snake_program
{
    public class Food
    {
        public Food(Form form) // accepts picturebox for coin instead of making one
        {
            mainForm = (CoreForm)form;
        }
        public virtual void finalize() // cleanup function
        {
            mainForm.removeQueue.Enqueue(picBox); // add the picbox to list of things to be deleted
        }
        // coords of the thing
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
        CoreForm mainForm;// reference to main form
        // reference to the picturebox - this is used for collision checking
        public PictureBox picBox;
        // number that indicates size change
        public int Change;
    }
}
