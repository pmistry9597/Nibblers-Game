using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace snake_program
{
    public class Obstacle : GameObject
    {
        
        // create obstacle from single picturebox for visual and bounds
        public static Obstacle FromPicture(PictureBox picture, Color color)
        {
            // set color if given
            if (color != null)
            {
                picture.BackColor = color;
            }
            // pic array for creating the obstacle (contains the inputted picture)
            PictureBox[] picArray = new PictureBox[] { picture };
            return new Obstacle(picArray, picArray); // create the new obstacle
        }
        public Obstacle(PictureBox[] bounds) : base(bounds) // get inputted bounds
        {

        }
        // for accepting visuals
        public Obstacle(PictureBox[] bounds, PictureBox[] visuals) : base(bounds)
        {
            this.visuals = new List<PictureBox>(visuals); // convert visuals to list
        }

        public PictureBox picBox
        {
            get
            {
                return visuals[0];
            } set
            {
                visuals[0] = value;
            }
        }

        List<PictureBox> visuals;// all of the visual parts of the game object (can be identical to bounds)
    }
}
