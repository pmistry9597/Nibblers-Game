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
    
    public partial class StartScreen : Form
    {
        public StartScreen()
        {
            InitializeComponent();

            // set events for single player button for mouse events
            btnSinglePlayer.MouseHover += new EventHandler(MouseHoverSingle);
            btnSinglePlayer.MouseLeave += new EventHandler(MouseLeaveSingle);
            btnSinglePlayer.MouseClick += new MouseEventHandler(OnClickSingle);

            // set events for 2 player button for mouse events
            btnTwoPlayer.MouseHover += new EventHandler(MouseHoverTwo);
            btnTwoPlayer.MouseLeave += new EventHandler(MouseLeaveTwo);
            btnTwoPlayer.MouseClick += new MouseEventHandler(OnClickTwo);

            // set global variable to this start screen
            Global.startScreen = this;
        }

        // mouse hover event for single player - show outline
        void MouseHoverSingle(Object o, EventArgs e)
        {
            outlineSingle.Visible = true;
        }
        // mouse leave event for single player - remove outline
        void MouseLeaveSingle(Object o, EventArgs e)
        {
            outlineSingle.Visible = false;
        }

        // mouse hover event for single player - show outline
        void MouseHoverTwo(Object o, EventArgs e)
        {
            outlineTwo.Visible = true;
        }
        // mouse leave event for single player - remove outline
        void MouseLeaveTwo(Object o, EventArgs e)
        {
            outlineTwo.Visible = false;
        }
        // onclick method for single player button
        void OnClickSingle(Object o, EventArgs e)
        {
            (new LevelDisplay()).Show(); // show the level display
            Hide();
        }
        // onclick method for 2 player button
        void OnClickTwo(Object o, EventArgs e)
        {
            (new _2Player()).Show(); // show mulitplayer game
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void StartScreen_Load(object sender, EventArgs e)
        {

        }
    }
    public class Global // global variables here
    {
        public static StartScreen startScreen; // reference to beginning form
    }
}
