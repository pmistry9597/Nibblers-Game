using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace snake_program
{
    public partial class Level4 : CoreForm
    {
        int threshold = 20;

        public Level4()
        {
            InitializeComponent();

            base.CoreBuild();

        }

        public override void gameConstruction()
        {
            // set the banner for this level
            LevelBanner = Properties.Resources.Level_4;

            base.gameConstruction();

            AddObstacles();

            snake = new ContinuousSnake(spawnPoint.Location.X, spawnPoint.Location.Y, this, new Vector(3, 90), 3);
            engine.AddSnake(snake); // register the snake

            engine.AddStationary(new EnemyStationary(guardian, threshold, this)); // create the guardian
            engine.gate_collider = gate_collider; // register the gate collider

            spawnPoint.Visible = false; // make the spawn point invisible

            // add followers
            follower = new EnemyFollower(follower1, 2, this);
            engine.followers.Add(follower);
            engine.followers.Add(new EnemyFollower(follower2, 2, this));
            engine.followers.Add(new EnemyFollower(follower3, 2, this));
            engine.followers.Add(new EnemyFollower(follower4, 2, this));

            // show threshold needed to beat the game
            txtSizeNeeded.Text = threshold.ToString();

            // add apple spawn pads to a single spawn group
            FoodSpawnGroup spawnGroup = new FoodSpawnGroup(engine, 0.6);
            spawnGroup.Add(new AppleSpawnPad(spawnpad1, 0.6, engine));
            spawnGroup.Add(new AppleSpawnPad(spawnpad2, 0.6, engine));
            spawnGroup.Add(new AppleSpawnPad(spawnpad3, 0.6, engine));
            spawnGroup.Add(new AppleSpawnPad(spawnpad4, 0.6, engine));
            spawnGroup.Add(new AppleSpawnPad(spawnpad5, 0.6, engine));
            spawnGroup.Add(new AppleSpawnPad(spawnpad6, 0.6, engine));
            spawnGroup.Add(new AppleSpawnPad(spawnpad7, 0.6, engine));
            spawnGroup.Add(new AppleSpawnPad(spawnpad8, 0.6, engine));
            spawnGroup.Add(new AppleSpawnPad(spawnpad9, 0.6, engine));
            engine.AddFoodGroupSpawner(spawnGroup);

            
        }

        EnemyFollower follower;

        void AddObstacles() // add all obstacles in this level
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

        public override void ExtraWork(object o, EventArgs e)
        {
            base.ExtraWork(o, e);
            txtSize.Text = string.Format("{0:0.#}", (double)snake.Length / (double)BodyPart.SIZE);
            if (snake.Collided(Gate) != 0)
            {
                snake.snakeHead.picBox.SendToBack();
            }
            // test the follower
            //follower.Run(engine.snakes, engine.obstacles);
        }

        private void Level4_Load(object sender, EventArgs e)
        {

        }
    }
}
