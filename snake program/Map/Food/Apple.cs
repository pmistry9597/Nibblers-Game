using System;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Drawing;

namespace snake_program
{
    class Apple : Food
    {
        public Apple(int x, int y, CoreForm form) : base(form)
        {
            // set size change when eaten by snake
            Change = BodyPart.SIZE / 2;
            // make picture box for the apple
            picBox = new PictureBox
            {
                Name = "picBox",
                Size = new Size(BodyPart.DEFAULT_SIZE, BodyPart.DEFAULT_SIZE),
                Location = new Point(x, y),
                Image = IMAGE,
                SizeMode = PictureBoxSizeMode.StretchImage // make the image fit the picturebox
            };
            // add the image
            form.Controls.Add(picBox);
            form.Controls.SetChildIndex(picBox, 0);
            picBox.SendToBack();
        }
        public Apple(PictureBox pic, CoreForm form) : base(form)
        {
            Change = BodyPart.SIZE / 2; // size change when snake eats apple
            // setup the picturebox
            pic.Size = new Size(BodyPart.SIZE, BodyPart.SIZE);
            pic.Image = IMAGE;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            // set picBox in food object
            picBox = pic;
        }

        // image for apple objects
        public Image IMAGE = Properties.Resources.apple;
    }
}
