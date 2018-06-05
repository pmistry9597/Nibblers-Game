using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_program
{
    class MysteryBoxEngine : GameEngine // game engine with mysteryboxes
    {
        public MysteryBoxEngine(CoreForm form) : base(form)
        {

        }

        public override void ExtraWork()
        {
            foreach (ContinuousSnake snake in snakes) // go through all the snakes
            {
                
            }
        }

        
    }
}
