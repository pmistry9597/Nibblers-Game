using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace snake_program
{
    public class AppleSpawnPad : FoodSpawnPad
    {
        public AppleSpawnPad(PictureBox picBox, double probDensity, GameEngine engine) : base(picBox, probDensity, engine)
        {
        }

        public override void FoodSpawn(List<ContinuousSnake> snakes, List<Food> foods) // spawn an apple in the bounds when called
        {
            /*double percent = (probability * 100);
            int random = engine.GetRandom(1, 100);
            if (random > percent) // only spawn if the random number is less than or equal to the percent probability
            {
                return;
            }*/
            // get random x and yvalue within bounds
            int x = engine.GetRandom(Bounds.X, Bounds.X + Bounds.Width - BodyPart.SIZE); // subtract bodypart size because apple could be out of bounds
            int y = engine.GetRandom(Bounds.Y, Bounds.Height + Bounds.Y - BodyPart.SIZE);
            // make new apple
            Apple apple = new Apple(x, y, engine.mainForm);
            
            // make sure not collided with snakes or other food items
            if (Collided(apple, snakes, foods))
            {
                // delete the food item
                apple.finalize();
                // run function again to try to get another position
                FoodSpawn(snakes, foods);
            } else
            {
                engine.AddFood(apple); // register it
            }
        }
    }
}
