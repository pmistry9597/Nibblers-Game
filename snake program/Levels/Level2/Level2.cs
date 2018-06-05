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
    public partial class Level2 : CoreForm
    {
        int maxFoods = 10; // maximum foods on screen
        int threshold = 10; // threshold needed to beat level by eating the enemy
        int minFoodLife = 4000; // minimum food lifetime in milliseconds
        int maxFoodLife = 5000; // max food lifetime in milliseconds

        public Level2()
        {
            //BodyPart.SIZE = 15; // change body size for this level
            InitializeComponent();
            txtSize.BackColor = Color.Transparent;
            txtSize.Text = "4";
            lblSize.BackColor = Color.Transparent;
            // call "constructor" in base class
            base.CoreBuild();
        }


        public override void gameConstruction()
        { 
            // set correct level banner to be displayed at the beginning
            LevelBanner = Properties.Resources.Level_2;

            base.gameConstruction();

            AddObstacles();

            // get coord for spawn location
            int spawnX = spawnPoint.Location.X; int spawnY = spawnPoint.Location.Y;
            snake = new ContinuousSnake(spawnX, spawnY, this, new Vector(2, 90), 4);
            engine.AddSnake(snake); // register the snake with the engine

            // add the standalone spawnpad
            DespawnAppleSpawnPad standalone = new DespawnAppleSpawnPad(standalonespawnpad, 0.3, 6000, 6000, this);
            engine.AddFoodSpawner(standalone);

            engine.SetFoodInterval(50);
            engine.SetMaxFoodItems(maxFoods);// set max number of foods onsreen

            // register the guardian and gate collider
            engine.AddStationary(new EnemyStationary(guardian, threshold, this));
            engine.gate_collider = gate_collider;

            // add the giant food spawn group
            CreateFoodSpawnGroup();
            txtSizeNeeded.Text = threshold.ToString();// tell the user of the size needed
            spawnPoint.Visible = false; // make the spawn point invisible  

            // create mystery spawn pad
            engine.mysterySpawnPads.Add(new MysteryBoxSpawnPad(mysteryspawn, 0.4, this));

            // test mystery spawn pad
            /*MysteryBoxSpawnPad spawnTest = new MysteryBoxSpawnPad(testmysteryspawn, 1, this);
            engine.mysterySpawnPads.Add(spawnTest);*/
        }

        void HidePictureBox(PictureBox pic) // accepts a picturebox and hides it by sending to back and making inivisble
        {
            pic.SendToBack();
            pic.Visible = false;
        }

        // create the massive food spawn group
        void CreateFoodSpawnGroup()
        {
            FoodSpawnGroup foodSpawnGroup = new FoodSpawnGroup(engine, .8);

            foodSpawnGroup.Add(new DespawnAppleSpawnPad(applespawnpad1, 1, minFoodLife, maxFoodLife, this));
            foodSpawnGroup.Add(new DespawnAppleSpawnPad(applespawnpad2, 1, minFoodLife, maxFoodLife, this));
            foodSpawnGroup.Add(new DespawnAppleSpawnPad(applespawnpad3, 1, minFoodLife, maxFoodLife, this));
            foodSpawnGroup.Add(new DespawnAppleSpawnPad(applespawnpad4, 1, minFoodLife, maxFoodLife, this));
            foodSpawnGroup.Add(new DespawnAppleSpawnPad(applespawnpad5, 1, minFoodLife, maxFoodLife, this));
            foodSpawnGroup.Add(new DespawnAppleSpawnPad(applespawnpad6, 1, minFoodLife, maxFoodLife, this));
            //foodSpawnGroup.Add(new DespawnAppleSpawnPad(applespawnpad7, 1, minFoodLife, maxFoodLife, this));
            foodSpawnGroup.Add(new DespawnAppleSpawnPad(applespawnpad8, 1, minFoodLife, maxFoodLife, this));

            engine.AddFoodGroupSpawner(foodSpawnGroup);
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
            engine.AddObstacle(Obstacle.FromPicture(obstacle18, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle19, Color.Brown));
            engine.AddObstacle(Obstacle.FromPicture(obstacle20, Color.Brown));
        }
        

        private void Level2_Load(object sender, EventArgs e)
        {

        }
    }
}

// how to run engines with extra work:
// 1. put reference with type of the inherited class
// 2. run the engine's ExtraWork function in an overrided ExtraWork function of coreform