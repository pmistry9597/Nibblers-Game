using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace snake_program
{
    public abstract class FoodSpawnPad
    {
        public FoodSpawnPad(PictureBox pic, double probability, GameEngine engine) // probability will be given in percents
        {
            picBox = pic;
            Bounds = picBox.Bounds;
            // make the picBox transparent
            picBox.Visible = false;
            this.engine = engine;
            // get the probability based on probability density
            this.probability = probability;
        }

        public void TrySpawn(List<ContinuousSnake> snakes, List<Food> foods)
        {
            // dont spawn if ShouldSpawn is not true
            if (!ShouldSpawn)
            {
                return;
            }
            double percent = (probability * 100);
            int random = engine.GetRandom(1, 100);
            if (random > percent) // only spawn if the random number is less than or equal to the percent probability
            {
                return;
            }
            FoodSpawn(snakes, foods);
        }
        public abstract void FoodSpawn(List<ContinuousSnake> snakes, List<Food> foods); // spawn food in the area
        // run method - run spawner
        public void Run(List<ContinuousSnake> snakes, List<Food> foods)
        {
            TrySpawn(snakes, foods);
        }
        // check if collided with snake or food
        public bool Collided(Food foodItem,List<ContinuousSnake> snakes, List<Food> foods)
        {
            foreach (ContinuousSnake snake in snakes) // check all snakes
            {
                if (snake.Collided(foodItem.picBox) != 0) // if not zero, collided with food
                {
                    return true;
                }
            }
            foreach (Food food in foods) // check all food items
            {
                if (food.picBox.Bounds.IntersectsWith(foodItem.picBox.Bounds))
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
        // true if this spawn pad should spawn any food items
        public bool ShouldSpawn = true;
    }
}
