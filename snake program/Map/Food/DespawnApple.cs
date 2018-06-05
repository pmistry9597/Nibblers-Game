using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace snake_program
{
    class DespawnApple : Apple // after a certain amount of time, delete itself from the engine and remove itself from the form (ie finalize for latter)
    {
        public DespawnApple(int x, int y, int millisecondsLife, CoreForm form) : base(x, y, form)
        {
            mainForm = form;
            // make the timer run end of life event (to delete apple)
            lifeTimer.Elapsed += new ElapsedEventHandler(EndLife);
            lifeTimer.Interval = millisecondsLife; // set interval to lifetime
        }

        // start lifetime timer
        public void StartLife()
        {
            lifeTimer.Start();
        }

        public override void finalize()
        {
            base.finalize();
            // end timer in case finalize happened early
            try
            {
                lifeTimer.Stop();
            } catch (NullReferenceException e)
            {

            }
            lifeTimer = null;
        }

        // this will be run by the timer to delete the apple
        void EndLife(Object o, EventArgs e)
        {
            mainForm.engine.foodItems.Remove(this); // unregister the apple from the engine
            base.finalize();
        }

        Timer lifeTimer = new Timer(); // timer that will be responsible for deleting the apple
        CoreForm mainForm;// reference to main form
    }
}
