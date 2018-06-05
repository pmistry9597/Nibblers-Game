using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace snake_program
{
    public class MysteryBoxSpawnPad
    {
        public MysteryBoxSpawnPad(PictureBox pic, double probability, CoreForm form) // probability will be given in percents
        {
            picBox = pic;
            Bounds = picBox.Bounds;
            // make the picBox transparent
            picBox.Visible = false;
            this.engine = form.engine;
            // get the probability based on probability density
            this.probability = probability;
        }

        public void TrySpawn(List<ContinuousSnake> snakes, List<MysteryBox> boxes)
        {
            double percent = (probability * 100);
            int random = engine.GetRandom(1, 100);
            if (random > percent) // only spawn if the random number is less than or equal to the percent probability
            {
                return;
            }
            Spawn(snakes, boxes);
        }
        public void Spawn(List<ContinuousSnake> snakes, List<MysteryBox> boxes) // spawn food in the area
        {
            int x = engine.GetRandom(Bounds.X, Bounds.X + Bounds.Width - BodyPart.SIZE); // subtract bodypart size because apple could be out of bounds
            int y = engine.GetRandom(Bounds.Y, Bounds.Height + Bounds.Y - BodyPart.SIZE);
            // make new apple
            MysteryBox box = new MysteryBox(x, y, engine.mainForm);

            // make sure not collided with snakes or other food items
            if (Collided(box, snakes, boxes))
            {
                // delete the food item
                box.finalize();
                // run function again to try to get another position
                Spawn(snakes, boxes);
            }
            else
            {
                engine.mysteryBoxes.Add(box); // register it
            }
        }
        // run method - run spawner
        public void Run(List<ContinuousSnake> snakes, List<MysteryBox> boxes)
        {
            TrySpawn(snakes, boxes);
        }
        // check if collided with snake or food
        public bool Collided(MysteryBox box, List<ContinuousSnake> snakes, List<MysteryBox> boxes)
        {
            foreach (ContinuousSnake snake in snakes) // check all snakes
            {
                if (snake.Collided(box.picBox) != 0) // if not zero, collided with food
                {
                    return true;
                }
            }
            foreach (MysteryBox mbox in boxes) // check all food items
            {
                if (mbox.picBox.Bounds.IntersectsWith(box.picBox.Bounds))
                {
                    return true;
                }
            }
            return false; // if function reached here, no collision happened
        }

        // picturebox that represents pad spawn area
        public PictureBox picBox;
        // bounds of the box
        public Rectangle Bounds;
        // stores probability of spawning something
        public double probability;
        // reference to main engine
        public GameEngine engine;
    }
}
