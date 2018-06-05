using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace snake_program
{
    public class GameEngine // objects of this type will be responsible for running the game
    {
        // constructor must take the custom form
        public GameEngine(CoreForm form)
        {
            mainForm = form;
        }
        public virtual void CoreRun() // a timer in the main form will repeatedly call this method to make the engine work
        {
            // dont run if paused
            if (paused)
            {
                return;
            }
            // run all the snakes
            foreach (ContinuousSnake snake in snakes)
            {
                snake.run(); // run the snake
                if (!snake.Alive)
                {
                    continue; // dont run anything else if the snake is not alive
                }
                // ------- START OF OBSTACLE COLLISION CODE
                foreach (Obstacle obstacle in obstacles) // loop through all obstacles to check for collision with snake
                {
                    if (snake.Alive) // only run if snake isn't dead
                    {
                        if (snake.Collided(obstacle) != 0 || CheckSnakeCollisions(snake)) // non zero means collision (also check for self collision
                        {
                            Death(snake);
                        }
                    }
                }
                // make sure length is not less than two (or kill the snake)
                if ((snake.Length / BodyPart.SIZE) < 2)
                {
                    Death(snake);
                }
                // ------- END OF OBSTACLE COLLISION CODE
                foodCollision(snake);// food collsions handler
                RunEnemies(snake);
                RunMysteryBoxes(snake);
                CheckFollowerCollision(snake);
                CheckProjectileCollision(snake);
            }
            RunFoodSpawners();
            CheckWin();
            RunEnemyFollowers();
            RunProjectiles();
        }
        // check for snake collisions
        bool CheckSnakeCollisions(ContinuousSnake testSnake)
        {
            foreach (ContinuousSnake snake in snakes)
            {
                // run self collision code on the snake if the snakes are the same
                if (snake == testSnake)
                {
                    if (snake.SelfCollision())
                    {
                        return true; // if snake collided with itself,  the snake definetly collided
                    }
                } else
                {
                    foreach (BodySegment segment in snake.bodySegments) // check for collision with all body segments of other snakes
                    {
                        if ((testSnake.Collided(segment.picBox) & ContinuousSnake.HEAD_COLLISION) != 0)
                        {
                            return true; // snake head collided with another snake so return true
                        }
                    }
                }
            }
            // if the function reached this point, not collision occured
            return false;
        }
        void RunEnemyFollowers()
        {
            foreach (EnemyFollower enemy in followers)
            {
                enemy.Run(snakes, obstacles);
            }
        }
        void CheckFollowerCollision(ContinuousSnake snake) // kill snake whos head collides with enemy
        {
            foreach(EnemyFollower enemy in followers)
            {
                if ((snake.Collided(enemy.picBox) & ContinuousSnake.HEAD_COLLISION) != 0) // kill the snake if head collision
                {
                    Death(snake);
                }
            }
        }
        // check for projectile collision with snake
        void CheckProjectileCollision(ContinuousSnake snake)
        {
            foreach (Projectile projectile in projectiles)
            {
                if (projectile.finalized)
                {
                    continue; // don't check the projectile if its finalized
                }
                // only kill the snake if it collides with the snake head
                if ((snake.Collided(projectile.picBox) & ContinuousSnake.HEAD_COLLISION) != 0)
                {
                    Death(snake);
                }
            }
        }
        // optional override to add more stuff to snake loop
        public virtual void ExtraWork()
        {
            
        }
        // run mysterbox handler
        void RunMysteryBoxes(ContinuousSnake snake)
        {
            List<MysteryBox> removeQueue = new List<MysteryBox>();
            foreach (MysteryBox box in mysteryBoxes) // check all boxes for collision
            {
                if (snake.Collided(box.picBox) != 0)
                {
                    box.Invoke(snake); // run the box's function if collided
                }
                if (box.Finalized)
                {
                    removeQueue.Add(box);
                }
            }
            foreach (MysteryBox box in removeQueue)
            {
                mysteryBoxes.Remove(box);
            }

            RunMysterySpawners();
        }
        int targetMysteryInterval = 100; // spawn rate of mystery boxes
        int mysteryIncrement = 0;// current increment of mystery interval
        void SetMysteryInterval(int interval)
        {
            targetMysteryInterval = interval;
        }
        // run mystery spawnwers
        void RunMysterySpawners()
        {
            mysteryIncrement++;
            if (mysteryIncrement < targetMysteryInterval)
            {
                return;
            }
            mysteryIncrement = 0;
            // only run if interval has been reached
            foreach (MysteryBoxSpawnPad spawnPad in mysterySpawnPads)
            {
                spawnPad.Run(snakes, mysteryBoxes);
            }
        }
        // check if won by checking if snake collided with gate
        void CheckWin()
        {
            if (gate_collider != null)
            {
                foreach (ContinuousSnake snake in snakes) // check all snakes
                {
                    if (snake.Collided(gate_collider) != 0) // if its above zero, snake collided with gate
                    {
                        Console.WriteLine("Apparently you won");
                        mainForm.CoreWin();
                    }
                }
            }
        }
        // for when the snake dies
        public void Death(ContinuousSnake snake)
        {
            // make all the segments invisible
            foreach (BodySegment segment in snake.bodySegments)
            {
                segment.picBox.Visible = false;
            }
            snake.glorifiedDestroy(); // destroy the whole snake - 0 means the index which is the head
                                      // run the lose method on the main form
            mainForm.CoreLose();
        }
        void foodCollision(ContinuousSnake snake)
        {
            // check for collision with food
            for (int i = 0; i < foodItems.Count; i++)
            {
                // retrieve current food item
                Food food = foodItems[i];
                if ((snake.Collided(food.picBox) & ContinuousSnake.HEAD_COLLISION) != 0) // check for head collision
                {
                    snake.ChangeSize(food.Change); // change snake size depending on food
                    // delete the food item
                    food.finalize();
                    foodItems.Remove(food);
                    // subtract one from i to stop from skipping food items
                    i--;
                }
            }
        }
        // run all enemies (movement, collision checking)
        void RunEnemies(ContinuousSnake snake)
        {
            foreach (EnemyStationary enemy in stationaryEnemies) // run all stationary enemies
            {
                // check if the snake head is in position to eat the enemy
                if (enemy.HeadCollision(snake)) 
                {
                    if (enemy.Alive) // dont attempt to eat if the enemy is dead!
                    {
                        // try eating the enemy
                        if (enemy.AttemptEat(snake)) 
                        {
                        }
                        else
                        {
                            Death(snake); // kill the snake if the eating failed
                        }
                    }
                }
                enemy.Run();
            }
        }
        // runs all the projectiles
        void RunProjectiles()
        {
            List<Projectile> removeQueue = new List<Projectile>();
            foreach (Projectile projectile in projectiles)
            {
                if (projectile.finalized) // dont run but add to delete queue if finalized
                {
                    removeQueue.Add(projectile);
                }
                else // run if not finalized
                {
                    projectile.Run(obstacles);
                }
            }
            // remove the projectiles from the projectiles list for those that have been marked for deletion
            foreach (Projectile projectile in removeQueue)
            {
                projectiles.Remove(projectile);
            }
        }
        // keeps track of intervals between food spawns
        int foodSpawnInterval;
        // target interval between food spawns
        int TargetFoodInterval = 100;
        // run all food spawners
        void RunFoodSpawners()
        {
            // don't run if too many food items (don't bother checking if negative one which means no limit)
            if (maxFoodItems != -1 && (foodItems.Count >= maxFoodItems))
            {
                if (foodItems.Count > maxFoodItems)
                {
                    // delete foods most recently added if there is a surplus
                    for (int i = foodItems.Count - 1; i >= maxFoodItems - 1; i--)
                    {
                        // remove the food item chosen
                        foodItems[i].finalize();
                        foodItems.RemoveAt(i);
                    }
                }
                return; // end function here (remember if the function entered this if-block, there are too many food items)
            }
            if (foodSpawnInterval < TargetFoodInterval) // keep quitting until interval overflow is reached
            {
                foodSpawnInterval++;
                return;
            }
            // reset interval
            foodSpawnInterval = 0;
            SpawnFood();
        }
        public void SetFoodInterval(int newInterval) // set new interval for food spawn
        {
            TargetFoodInterval = newInterval;
        }
        public void SpawnFood() // make any of the spawners spawn food
        {
            foreach (FoodSpawnPad spawner in foodSpawners)
            {
                spawner.TrySpawn(snakes, foodItems);
            }
            // run the group spawners
            foreach (FoodSpawnGroup spawnGroup in groupSpawners)
            {
                spawnGroup.RunSpawn(snakes, foodItems);
            }
        }
        public void AddFoodSpawner(FoodSpawnPad spawnPad) // any food spawner inputted here will be run by the game engine
        {
            foodSpawners.Add(spawnPad);
        }
        public void AddFood(Food food)
        {
            foreach (Food foodItem in foodItems) // check if any food item is intersecting with the food item and set as parent to make
                                                 //background transparent
            {
                if (foodItem.picBox.Bounds.IntersectsWith(food.picBox.Bounds))
                {
                    food.picBox.Parent = foodItem.picBox;
                }
            }

            foodItems.Add(food); // add item to list of food items to be checked
        }
        public void AddObstacle(Obstacle obstacle) // add an obstacle to be checked by the engine
        {
            obstacles.Add(obstacle);
        }
        public void AddSnake(ContinuousSnake snake)
        {
            snakes.Add(snake);
        }
        public void AddStationary(EnemyStationary enemy) // add enemy to generic enemy checker (followers may not be in this one)
        {
            stationaryEnemies.Add(enemy);
        }
        public void AddFoodGroupSpawner(FoodSpawnGroup spawnGroup)
        {
            groupSpawners.Add(spawnGroup);
        }
        public int GetRandom(int begin, int end) // random number service
        {
            return random.Next(begin, end);
        }
        // toggle pausing
        public void TogglePause()
        {
            paused = !paused; // toggle the pause
            if (paused) // code to run if paused
            {
                foreach (EnemyStationary enemy in stationaryEnemies) // pause each enemy
                {
                    enemy.animaTimer.Stop(); // stop animation timers if running
                }
            }
            else
            {
                // start animation timers in enemies
                foreach (EnemyStationary enemy in stationaryEnemies)
                {
                    enemy.animaTimer.Start();
                }
            }
        }
        // set maximum number of food items (set to -1 for no maximum)
        public void SetMaxFoodItems(int max)
        {
            maxFoodItems = max;
        }
        // all stationary enemies (for checking)
        public List<EnemyStationary> stationaryEnemies = new List<EnemyStationary>();
        // store all food spawn pads here
        public List<FoodSpawnGroup> groupSpawners = new List<FoodSpawnGroup>();
        // list of all food
        public List<Food> foodItems = new List<Food>();
        // list of all snakes
        public List<ContinuousSnake> snakes = new List<ContinuousSnake>();
        // list of all food spawners
        public List<FoodSpawnPad> foodSpawners = new List<FoodSpawnPad>();
        // list of all obstacles
        public List<Obstacle> obstacles = new List<Obstacle>();
        // reference to the main form
        public CoreForm mainForm;
        // random number generator - same for all to stop repeated occurances
        Random random = new Random();
        // true if the game is paused (engine will not run if game is paused)
        public bool paused = false;
        // gate collider - snake collides with this and game end
        public PictureBox gate_collider;
        // maximum number of food items at a single time (-1 by default)
        int maxFoodItems = -1;
        public List<MysteryBox> mysteryBoxes = new List<MysteryBox>(); // list of mysterboes
        public List<MysteryBoxSpawnPad> mysterySpawnPads = new List<MysteryBoxSpawnPad>();
        public List<EnemyFollower> followers = new List<EnemyFollower>();
        public List<Projectile> projectiles = new List<Projectile>(); // list of all projectiles to be run
    }
    // ------ Responsibilities of Game Engine Objects
    // * run the snake
    // * food operations
    //      -collisions with food
    // * enemy operations
    //      -enemy collisions
    //      -enemy following (if added to the game)
}
