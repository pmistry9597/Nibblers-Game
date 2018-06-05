using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_program
{
    public class FoodSpawnGroup
    {
        public FoodSpawnGroup(GameEngine engine, double probability) // store the engine
        {
            this.engine = engine;
            this.probability = probability;
        }
        // add spawn area and re-adjust probabilitites for new list of spawn pads
        public void Add(FoodSpawnPad foodSpawnPad)
        {
            spawnPads.Add(foodSpawnPad);
            // remove all items in probabilities list to reset
            probabilities.RemoveRange(0, probabilities.Count);
            int[] prevProb = null; // previous spawn pad in loop
            for (int i = 0; i < spawnPads.Count; i++) // loop through all spawn pads to readjust area
            {
                FoodSpawnPad spawnPad = spawnPads[i]; // get current probability
                // get probability for spawn pad
                double probability = 0.005 + (double)spawnPad.Bounds.Width * (double)spawnPad.Bounds.Height / (double)Area;
                int percent = (int)(probability * 100); // get percent from probability
                int[] probRange;
                if (prevProb == null) // if null, this is the first spawn pad in the list
                {
                    // make array to represent probability range (in percents)
                    probRange = new int[] { 1, percent };
                }
                else
                {
                    probRange = new int[] { prevProb[1] + 1, prevProb[1] + percent };

                }
                probabilities.Add(probRange);// add it to the probabilities list
                prevProb = probRange;
            }
        }
        // RUN FUNCTION TO PICK A SPAWN AT RANDOM
        public void RunSpawn(List<ContinuousSnake> snakes, List<Food> foods)
        {
            double percent = (probability * 100);
            int random = engine.GetRandom(1, 100);
            if (random > percent) // only spawn if the random number is less than or equal to the percent probability
            {
                return;
            }
            // get random number between 1 and 100
            random = engine.GetRandom(1, 100);
            Console.WriteLine("Number Generated: " + random.ToString());
            for (int i = 0; i < probabilities.Count; i++) // use indexes in a for loop to get the spawn pad as well
            {

                // get the probability range
                int[] prob = probabilities[i];
                foreach (int c in prob)
                {
                    Console.WriteLine(c);
                }
                // check if the random is within range of the probability
                if ((random >= prob[0]) && (random <= prob[1]))
                {
                    spawnPads[i].FoodSpawn(snakes, foods);
                    return;
                }
            }
        }
        // total area of the food spawn pad group
        public int Area
        {
            get
            {
                int area = 0; // total area stored here
                foreach (FoodSpawnPad pad in spawnPads) // loop through all spawn pads
                {
                    area += pad.Bounds.Width * pad.Bounds.Height;
                }
                // return the area
                return area;
            }
        }
        // probability of spawn
        double probability;
        // list of all food spawn pads
        List<FoodSpawnPad> spawnPads = new List<FoodSpawnPad>();
        // probabilitites (index number is the same index for the corresponding spawn pad in spawnPads list)
        List<int[]> probabilities = new List<int[]>();
        // reference to game engine
        GameEngine engine;
    }
}
