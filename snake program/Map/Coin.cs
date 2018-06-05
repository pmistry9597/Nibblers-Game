using System;
using System.Windows.Forms;
using System.Drawing;

namespace snake_program.Map
{
    public class Coin // coin object on screen
    {
        public Coin(int x, int y, Form form)
        {
            picBox = new PictureBox // create a picture of the coin on screen
            {
                Name = "picBox",
                Size = new Size(BodyPart.SIZE, BodyPart.SIZE),
                Location = new Point(x, y),
                Image = coin_image
            };
            picBox.SizeMode = PictureBoxSizeMode.StretchImage; // make the image scale
            form.Controls.Add(picBox); // add it to the main form
            mainForm = (CoreForm)form;
        }
        public Coin(PictureBox picCoin, Form form) // accepts picturebox for coin instead of making one
        {
            picBox = picCoin;
            mainForm = (CoreForm)form;
        }
        public void finalize() // cleanup function
        {
            mainForm.removeQueue.Enqueue(picBox); // add the picbox to list of things to be deleted
        }
        CoreForm mainForm;// reference to main form
        // image of the coin
        Image coin_image = Properties.Resources.coin1;
        // reference to the picturebox
        public PictureBox picBox;
    }
}
