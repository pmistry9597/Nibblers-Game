using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace snake_program
{
    class DespawnAppleSpawnPad : AppleSpawnPad
    {
        public DespawnAppleSpawnPad(PictureBox picBox, double probability, int minLife, int maxLife, CoreForm form) : base(picBox, probability, form.engine)
        {
            mainForm = form;
            this.minLife = minLife;
            this.maxLife = maxLife;
        }

        public override void FoodSpawn(List<ContinuousSnake> snakes, List<Food> foods) // spawn an apple in the bounds when called
        {
            int x = engine.GetRandom(Bounds.X, Bounds.X + Bounds.Width - BodyPart.SIZE); // subtract bodypart size because apple could be out of bounds
            int y = engine.GetRandom(Bounds.Y, Bounds.Height + Bounds.Y - BodyPart.SIZE);

            // make random life time for apple based on min and max life setting
            int lifeTime = engine.GetRandom(minLife, maxLife);

            // make new despawn apple
            DespawnApple apple = new DespawnApple(x, y, lifeTime,engine.mainForm);

            // make sure not collided with snakes or other food items
            if (Collided(apple, snakes, foods))
            {
                // delete the food item
                apple.finalize();
                // run function again to try to get another position
                FoodSpawn(snakes, foods);
            }
            else
            {
               
                apple.StartLife(); // start the apple's life
                engine.AddFood(apple); // register it
            }
        }
        // storing reference to main form
        CoreForm mainForm;
        int minLife;// minimum lifetime of apples
        int maxLife;// maximum life of apples
    }
}
