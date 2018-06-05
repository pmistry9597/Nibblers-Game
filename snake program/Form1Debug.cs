using System;
using System.Drawing;
using System.Windows.Forms;

namespace snake_program
{
    partial class Form1
    {
        TurnPoint tp; // turnpoint to be used in testing whether cross check works
        BodyPart bdPart; // body part to be used in testing
        void debugGarbage() // this time we are testing the method of checking whether a part crossed a TurnPoint
        {
            bdPart = new BodyPart(0, 0, new Vector(10, 0), this); // make a body part with velocity
            BodyPart marker = new BodyPart(BodyPart.SIZE * 3, BodyPart.SIZE * 3, new Vector(0, 0), this); // will mark the turnpoint
            tp = new TurnPoint(marker, this);
            // change the mouse movement handler to our own custom handler
            MouseMove += new MouseEventHandler(mouseMoveEvent);
            MouseClick += new MouseEventHandler(runBdPart); // move body part on mouse click
            marker.picBox.MouseClick += new MouseEventHandler(runBdPart);
            bdPart.picBox.MouseClick += new MouseEventHandler(runBdPart); // move body part on mouse click
        }
        void runBdPart(Object o, MouseEventArgs e) // make body part move (for mouse click event)
        {
            Console.WriteLine("Just clicked!");
            redIfCrossed();
            bdPart.run();
        }
        void mouseMoveEvent(Object o, MouseEventArgs eArgs) // will keep putting body part at proper turn point and run turnpoint check
        {
            // get coords
            int x = eArgs.X;
            int y = eArgs.Y;
            bdPart.picBox.Location = new System.Drawing.Point(x, y); // set location to mouse coords
            Console.WriteLine("Mouse just moved"); // tell user of mouse movement event
            redIfCrossed();
        }
        void redIfCrossed() // make red if crossed
        {
            if (tp.Crossed(bdPart)) // testing if this method correct checks if crossed turn point
            {
                // --- SETTING PICBOX TO RED CODE-----
                bdPart.picBox.Image = null;
                bdPart.picBox.BackColor = Color.Red; // ---- END OF RED SET CODE
            }
            else if (bdPart.picBox.Image == null) // reset image appearance if not crossed
            {
                bdPart.picBox.Image = Properties.Resources.green_box_hi;
            }
        }
        Point pointForBdPart(BodyPart bdPart)
        {
            return bdPart.picBox.Location;
        }
    }
}
