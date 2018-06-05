using System;

namespace snake_program
{
    partial class Snake // this file has movement methods for the snake
    {
        void queueTurnPoint() // adds a turn point to the queue so the snake body can turn at the right time
        {
            // make a new turn point for the snake
            TurnPoint newTP = new TurnPoint(snakeHead, mainForm);
            // add it to the queue of turn points
            turnPoints.Add(newTP);
        }
        public void right()
        {
            velocity.Degrees = 0;
            queueTurnPoint(); // add new turn point
        }
        public void up() // make the snake go up
        {
            velocity.Degrees = 90;
            queueTurnPoint(); // add new turn point
        }
        public void left()
        {
            velocity.Degrees = 180;
            queueTurnPoint(); // add new turn point
        }
        public void down()
        {
            velocity.Degrees = -90;
            queueTurnPoint(); // add new turn point
        }

        // turn request methods - these ensure only logical turns are made
        public void requestUp()
        {
            // if the velocity angle is up or down, the cosine of it will always be zero
            bool notUpOrDown = (int)Math.Cos(velocity.Degrees * Math.PI / 180) != 0;
            if (notUpOrDown)
            {
                up();
            }
        }
        public void requestDown()
        {
            // if the velocity angle is up or down, the cosine of it will always be zero
            bool notUpOrDown = (int)Math.Cos(velocity.Degrees * Math.PI / 180) != 0; // int cast because
            // the angle is not perfect and often returns a view decimal points from zero
            if (notUpOrDown)
            {
                down();
            }
        }
        public void requestRight()
        {
            // if the velocity angle is left or right, the sine of it will always be zero
            bool notLeftorRight = (int)Math.Sin(velocity.Degrees * Math.PI / 180) != 0; // int cast because
            // the angle is not perfect and often returns a view decimal points from zero
            if (notLeftorRight)
            {
                right();
            }
        }
        public void requestLeft()
        {
            // if the velocity angle is left or right, the sine of it will always be zero
            bool notLeftorRight = (int)Math.Sin(velocity.Degrees * Math.PI / 180) != 0; // int cast because
            // the angle is not perfect and often returns a view decimal points from zero
            if (notLeftorRight)
            {
                left();
            }
        }
        // puts a body part at proper position
        void fixPos(BodyPart bdPart)
        {
            // get index to get body part in front
            int index = bodyParts.IndexOf(bdPart);
            int frontIndex = index - 1;
            BodyPart frontPart = bodyParts[frontIndex];
            // velocity angle of part in front in radians
            double angle = frontPart.velocity.Degrees * Math.PI / 180;
            // reverse the angle to get angle on the other side
            angle += Math.PI;
            // magnitude of body part offset
            double totOff = offset + BodyPart.SIZE;
            // get new x and y offset for the body part being readjusted
            int xOff = (int) ( totOff * Math.Cos(angle) );
            int yOff = (int) ( totOff * -Math.Sin(angle) ); // must be reversed because y cords are upside on screen
            // add the coords of the frontal body part to get proper position
            xOff += frontPart.picBox.Location.X;
            yOff += frontPart.picBox.Location.Y;
            // set new offset for the body part
            bdPart.picBox.Location = new System.Drawing.Point(xOff, yOff);
            bdPart.velocity.Degrees = frontPart.velocity.Degrees; // set velocity to previous body part
        }
    }
}