using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snake_program
{
    public partial class Level3 : CoreForm
    {
        //int maxFoods = 10; // maximum foods on screen
        int threshold = 18; // threshold needed to beat level by eating the enemy
        /*int minFoodLife = 4000; // minimum food lifetime in milliseconds
        int maxFoodLife = 5000; // max food lifetime in milliseconds*/
        double prob = 0.6;// probability of spawn for apple spawn pads blocked by mystery boxes
        public Level3()
        {
            InitializeComponent();
            // add all mystery spawns to an array to keep track of them
            AddMysterySpawns();

            

            spawnPoint.Visible = false; // make spawn point invisible
            txtSize.Text = "3"; // initial size of snake is 3
            txtSizeNeeded.Text = threshold.ToString(); // size needed is same as the threshold amount

            base.CoreBuild();
        }

        // add food spawn pads
        void AddFoodSpawns()
        {
            foreach (AppleSpawnPad spawn in fixedSpawns) // add the fixed spawns
            {
                engine.AddFoodSpawner(spawn);
            }
        }

        void AddMysterySpawns()
        {
            mysterySpawns = new PictureBox[]
            {
                mysteryspawn1,
                mysteryspawn2,
                mysteryspawn3,
                mysteryspawn4,
                mysteryspawn5,
                mysteryspawn6,
                mysteryspawn7,
                mysteryspawn8,
                mysteryspawn9
            };
            // make them all invisible
            foreach (PictureBox box in mysterySpawns)
            {
                box.Visible = false;
            }
        }

        // perform extrawork during frame update
        public override void ExtraWork(object o, EventArgs e)
        {
            base.ExtraWork(o, e);
            for (int i = 0; i < fixedBoxes.Length; i++) // check all mysteryboxes
            {
                // retrieve the box
                MysteryBox box = fixedBoxes[i];                
                if (box.Finalized) // if its finalized and no snake is in the location, make a new mystery box in the location
                {
                    // check if a snake is the location of the box
                    bool snakePresent = false;
                    foreach (ContinuousSnake snake in engine.snakes)
                    {
                        if (snake.Collided(mysterySpawns[i]) != 0) // if its not zero, a snake collided with the box
                        {
                            snakePresent = true;
                            break;
                        }
                    }
                    // if a snake is not present, spawn a mysterybox there
                    if (!snakePresent)
                    {
                        // retrieve the location of this mysterybox spawn
                        Point spawn = mysterySpawns[i].Location;
                        // make a new mysterbox in the location of the spawn point
                        fixedBoxes[i] = new MysteryBox(spawn.X, spawn.Y, this);
                        engine.mysteryBoxes.Add(fixedBoxes[i]);// register it with the engine
                    }                    
                }
                if (snake.Collided(Gate) != 0)
                {
                    snake.snakeHead.picBox.SendToBack();
                }
            }
            // show the size of the snake
            txtSize.Text = string.Format("{0:0.#}", (double)snake.Length / (double)BodyPart.SIZE);
        }

        void InitialMysterySpawn()
        {
            // spawn mysteryboxes in the spawn areas at beginning of game
            for (int i = 0; i < mysterySpawns.Length; i++)
            {
                // retrieve the spawn
                PictureBox spawn = mysterySpawns[i];
                MysteryBox box = new MysteryBox(spawn.Location.X, spawn.Location.Y, this);// spawn a mysterbox in the location
                engine.mysteryBoxes.Add(box); // register it with the engine
                // add it to the list of fixed mysterboxes
                fixedBoxes[i] = box;
            }
        }
        public override void gameConstruction()
        {
            LevelBanner = Properties.Resources.Level_3; // set right start banner for this level

            base.gameConstruction();

            // add blocked apple spawns

            fixedSpawns = new AppleSpawnPad[]
            {
                new AppleSpawnPad(applespawnpad1, prob, engine),
                new AppleSpawnPad(applespawnpad2, prob, engine),
                new AppleSpawnPad(applespawnpad3, prob, engine)
            };
            AddObstacles();
            AddFoodSpawns();

            // get coordinates for snake spawn location
            int x = spawnPoint.Location.X; int y = spawnPoint.Location.Y;
            snake = new ContinuousSnake(x, y, this, new Vector(2, 90), 3);
            engine.AddSnake(snake); // register it with the engine

            InitialMysterySpawn(); // spawn mysteryboxes in fixed areas

            engine.AddStationary(new EnemyStationary(guardian,threshold,this)); // register the guardian
            engine.gate_collider = gate_collider; // register gate collider

        }

        void AddObstacles()
        {
            engine.AddObstacle(Obstacle.FromPicture(obstacle1, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle2, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle3, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle4, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle5, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle6, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle7, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle8, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle9, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle10, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle11, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle12, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle13, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle14, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle15, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle16, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle17, Color.Brown));
        }

        private void Level3_Load(object sender, EventArgs e)
        {

        }

        // array of all mystery spawn locations
        PictureBox[] mysterySpawns;
        // array of apple spawn locations blocked by mysteryboxes
        AppleSpawnPad[] fixedSpawns;
        // keep track of all mystery boxes that are spawned in certain fixed areas in this level
        MysteryBox[] fixedBoxes = new MysteryBox[9];
    }
}
