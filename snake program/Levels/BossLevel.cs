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
    public partial class BossLevel : CoreForm
    {
        int threshold = 19; // threshold to kill the boss in this level
        public BossLevel()
        {
            InitializeComponent();

            txtSizeNeeded.Text = threshold.ToString(); // show size needed to defeat the boss
            txtSize.Text = "0";

            base.CoreBuild();
        }

        public override void gameConstruction()
        {
            // set the level banner
            LevelBanner = Properties.Resources.Level_Boss;
            base.gameConstruction();

            // rotate gate image and add it
            Image gateImg = Properties.Resources.Gate;
            gateImg.RotateFlip(RotateFlipType.Rotate270FlipXY);
            Gate.Image = gateImg;
            Gate.SizeMode = PictureBoxSizeMode.StretchImage;

            // add obstacles
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

            boss = new Boss(bossPic, 100,8000, this); // create the boss
            // add the snake
            snake = new ContinuousSnake(spawnPoint.Location.X, spawnPoint.Location.Y, this, new Vector(3, 90), 3);
            engine.AddSnake(snake); // register the snake with the engine

            engine.gate_collider = gate_collider; // add the gate collider

            // add the apple spawn pads
            engine.AddFoodSpawner(new AppleSpawnPad(applespawnpad1, 0.5, engine));
            engine.AddFoodSpawner(new AppleSpawnPad(applespawnpad2, 0.5, engine));
            engine.AddFoodSpawner(new AppleSpawnPad(applespawnpad3, 0.5, engine));

            // make the spawn point invisible
            spawnPoint.Visible = false;
        }
        Boss boss; // reference to the boss of the this level
        public override void ExtraWork(object o, EventArgs e)
        {
            base.ExtraWork(o, e);
            txtSize.Text = string.Format("{0:0.#}", (double)snake.Length / (double)BodyPart.SIZE); // update the size of the snake shown
            if (!boss.Alive || !snake.Alive)
            {
                return;
            }
            // if snake collides with boss, decide who dies
            if (snake.Collided(boss.picBox) != 0)
            {
                if (threshold > (double)snake.Length / (double)BodyPart.SIZE) // if the snake hasn't reached the threshold, kill the snake
                {
                    engine.Death(snake);
                } else
                {
                    boss.glorifiedDestroy(); // kill the boss if the snake is big enough
                }
            }
            boss.Run(); // run the boss
                      

        }

        private void BossLevel_Load(object sender, EventArgs e)
        {
        }
    }
}
