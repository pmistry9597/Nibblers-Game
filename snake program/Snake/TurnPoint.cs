using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snake_program
{
    class TurnPoint
    {
        // accept values in constructor
        public TurnPoint(int x, int y, int _angle)
        {
            this.angle = _angle;
            this.coords = new int[] { x, y };
        }
        // accept values in constructor except with coords array for the position
        public TurnPoint(int [] pos, int _angle)
        {
            this.angle = _angle;
            this.coords = pos;
        }
        // for ease of coding, constructor can take a body part as a turn point
        // (assuming the head is leading a snake body, and this head has already turned in the proper direction)
        public TurnPoint(BodyPart leadPart, Form form)
        {
            mainForm = (Form1)form;// store the form for later
            this.angle = leadPart.velocity.Degrees;
            // make a new array to store the coords
            this.coords = new int[] { leadPart.picBox.Location.X, leadPart.picBox.Location.Y };
            elbowPart = new BodyPart(coords[0], coords[1], new Vector(0, 0), mainForm); // make a body part in the location
        }
        // finalizer - called by the snake
        public void finalizer()
        {
            mainForm.removeQueue.Enqueue(elbowPart.picBox); // remove the elbow part as its not needed anymore
        }
        public int[] Coords // public accessor for coords
        {
            get
            {
                return this.coords;
            }
        }
        public double Angle // public accessor for angle
        {
            get
            {
                return this.angle;
            }
        }
        public int PartsCount // public accessor for partscount
        {
            get
            {
                return this.partsCount;
            }
            set
            {
                this.partsCount = value;
            }
        }
        // check if a body part should be influenced by this turn point
        // due to crossing
        public bool Crossed(BodyPart bdPart)
        {
            double rads = bdPart.velocity.Degrees * Math.PI / 180;
            // this is true if the x comp has crossed
            bool xComp = ((int)Math.Cos(rads) >= 0) == (bdPart.picBox.Location.X >= this.coords[0]);
            bool yComp = (-(int)Math.Sin(rads) >= 0) == (bdPart.picBox.Location.Y >= this.coords[1]);

            return xComp && yComp;
            //return yComp;
        }
        // direction of turn
        double angle;
        // coords in one array where [0] is x and [1] is y
        int[] coords;
        // which body part is being checked in the snake (starts at one because the first
        // body part is the head, and the head does not need to be influenced by TurnPoints)
        int partsCount = 1;
        // reference to body part in the turnpoint location
        BodyPart elbowPart;
        // reference to form with the picture box
        Form1 mainForm;
    }
}
