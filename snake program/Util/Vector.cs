using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_program
{
    public class Vector
    {
        // internal component
        double angle;
        double magnitude;
        public double x;
        public double y; // internal storage of y compnent
        // return a clone of the current object
        public Vector Clone()
        {
            Vector newVect = new Vector(this.magnitude, this.Degrees);
            return newVect;
        }
        public Vector(double mag, double ang) // takes a magnitude and an angle
        {
            magnitude = mag;
            angle = ang * Math.PI / 180;
            directionChange(0); // update x and y comps without changing the angle
        }
        // update internal magnitude
        void updateMag()
        {
            magnitude = Math.Sqrt(x * x + y * y);
        }
        void updateAngle()
        {
            double slope = y / x;
            angle = Math.Atan(slope);
        }
        void updateComps()
        {
            updateAngle();
            updateMag();
        }
        public double X // public access of x copamdfadfadf
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
                updateComps();
            }
        }
        public double Y // puhlci adfl;aj;lkcf a;dlfkj al;k
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
                updateComps();
            }
        }
        public double Magnitude // outside velocity component
        {
            get
            {
                return this.magnitude;
            }
            set
            {
                double scaleFact = value / this.magnitude;
                this.x *= scaleFact; // scale up x comp
                this.y *= scaleFact;  // scale up y comp
                this.magnitude = value; // update magnitude
            }
        }
        public double Degrees
        {
            get
            {
                // get degree
                double degrees = 180 * this.angle / Math.PI;
                return degrees;
            }
            set
            {
                double rads = Math.PI * value / 180;
                angle = rads;
                directionChange(0);
            }
        }
        public void multiply (double scalar) { // multiple scalar by vector
            X *= scalar;
            Y *= scalar;
        }
        public void directionChange(double ang) // change direction of vector with angle
        {
            // get radians from degrees angle
            double rads = ang * Math.PI / 180;
            this.angle += rads;
            // get angle and magnitude and calculate new components
            double newY = magnitude * Math.Sin(this.angle);
            double newX = magnitude * Math.Cos(this.angle);
            // set the new components
            this.x = newX;
            this.y = newY;
        }

    }
}
