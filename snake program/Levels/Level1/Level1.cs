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
    public partial class Level1 : CoreForm
    {
        int maxFoods = 5; // maximum foods on screen
        int threshold = 10; // threshold needed to beat level by eating the enemy

        public Level1() : base()
        {
            InitializeComponent();
            txtSize.BackColor = Color.Transparent;
            txtSize.Text = "0";
            lblSize.BackColor = Color.Transparent;
            // call "constructor" in base class
            base.CoreBuild();
        }

        public override void gameConstruction()
        {
            // set the Level Banner to right one
            LevelBanner = Properties.Resources.Level_1;

            base.gameConstruction(); // run base class game construction

            AddObstacles(); 
            AddSpawnPads();
            engine.AddStationary(new EnemyStationary(guardian, threshold, this)); // register guardian (the integer is - 
            // the size needed to kill the enemy guardian by ramming it

            // make snake at the "spawn point"(which is really a picturebox i made to be the spawn point)
            int spawnX = spawnPoint.Location.X; int spawnY = spawnPoint.Location.Y;
            snake = new ContinuousSnake(spawnX, spawnY, this, new Vector(1, 90), 3);
            engine.AddSnake(snake);// let the engine know about the snake (so it can run/handle it)
            // register gate collider wth engine
            engine.gate_collider = gate_collider;

            engine.SetMaxFoodItems(maxFoods); // maximum number of food items on screen

            spawnPoint.Visible = false; // make the spawn point invisible

            txtSizeNeeded.Text = threshold.ToString();// show the threshold needed
            
        }

        public override void ExtraWork(object o, EventArgs e)  // extra stuff to do at end of frame update cycle
        {
            base.ExtraWork(o, e);
            // update size in textbox
            txtSize.Text = string.Format("{0:0.#}", (double)snake.Length / (double)BodyPart.SIZE);
            if (snake.Collided(Gate) != 0)
            {
                snake.snakeHead.picBox.SendToBack();
            }
        }

        void AddObstacles() // get all picturebox obstacles in the form and register them as obstacles
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
            
        }

        void AddSpawnPads() // setup spawn pads (register with game engine, set visibility to false)
        {
            engine.AddFoodSpawner(new AppleSpawnPad(applespawnpad1, 0.1, engine));
            applespawnpad1.SendToBack();
            applespawnpad1.Visible = false;

            foodSpawnGroup = new FoodSpawnGroup(engine, 1); // make a new spawn group with 100 percent spawn probability - 
            // in the area
            AddSpawnGroupPad(applespawngroup1);
            AddSpawnGroupPad(applespawngroup2);
            AddSpawnGroupPad(applespawngroup3);
            AddSpawnGroupPad(applespawngroup4);
            AddSpawnGroupPad(applespawngroup5);
            AddSpawnGroupPad(applespawngroup6);
            AddSpawnGroupPad(applespawngroup7);
            AddSpawnGroupPad(applespawngroup8);
            // register the spawn group with the engine
            engine.AddFoodGroupSpawner(foodSpawnGroup);

        }
        FoodSpawnGroup foodSpawnGroup;
        // add a spawner to the spawn group
        void AddSpawnGroupPad(PictureBox spawnPic)
        {
            foodSpawnGroup.Add(new AppleSpawnPad(spawnPic,1,engine));
            spawnPic.SendToBack();
            spawnPic.Visible = false;
        }
        private void Level1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

// ---------------- LEVEL 1 NOTES --------------
// - 15  obstacles  with following naming scheme:
//      obstacle1
// - 8  apple spawn pads  part of a spawn group
// - a single independent  apple spawn pad  in them middle
