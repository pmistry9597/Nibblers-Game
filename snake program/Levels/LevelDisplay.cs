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
    public partial class LevelDisplay : Form
    {
        public LevelDisplay()
        {
            InitializeComponent();

            // mouse events for level 1 button
            btnLevel1.MouseHover += new EventHandler(MouseHover1);
            btnLevel1.MouseLeave += new EventHandler(MouseLeave1);
            btnLevel1.MouseClick += new MouseEventHandler(OnLevel1Click);

            // mouse events for level 2 button
            btnLevel2.MouseHover += new EventHandler(MouseHover2);
            btnLevel2.MouseLeave += new EventHandler(MouseLeave2);
            btnLevel2.MouseClick += new MouseEventHandler(OnLevel2Click);

            // mouse events for level 3 button
            btnLevel3.MouseHover += new EventHandler(MouseHover3);
            btnLevel3.MouseLeave += new EventHandler(MouseLeave3);
            btnLevel3.MouseClick += new MouseEventHandler(OnLevel3Click);

            // mouse events for boss button
            btnLevel4.MouseHover += new EventHandler(MouseHover4);
            btnLevel4.MouseLeave += new EventHandler(MouseLeave4);
            btnLevel4.MouseClick += new MouseEventHandler(OnLevel4Click);

            // mouse events for boss button
            btnBossLevel.MouseHover += new EventHandler(MouseHoverBoss);
            btnBossLevel.MouseLeave += new EventHandler(MouseLeaveBoss);
            btnBossLevel.MouseClick += new MouseEventHandler(OnBossLevelClick);

            // mouse events for return button
            btnReturn.MouseHover += new EventHandler(MouseHoverReturn);
            btnReturn.MouseLeave += new EventHandler(MouseLeaveReturn);
            btnReturn.MouseClick += new MouseEventHandler(OnClickReturn);
        }
        // mouse hover event for level 1 - show outline
        void MouseHover1(Object o, EventArgs e)
        {
            outline1.Visible = true;
        }
        // mouse leave event for level 1 - remove outline
        void MouseLeave1(Object o, EventArgs e)
        {
            outline1.Visible = false;
        }
        // mouse hover event for level 2 - show outline
        void MouseHover2(Object o, EventArgs e)
        {
            outline2.Visible = true;
        }
        // mouse leave event for level 2 - remove outline
        void MouseLeave2(Object o, EventArgs e)
        {
            outline2.Visible = false;
        }
        // mouse hover event for level 3 - show outline
        void MouseHover3(Object o, EventArgs e)
        {
            outline3.Visible = true;
        }
        // mouse leave event for level 3 - remove outline
        void MouseLeave3(Object o, EventArgs e)
        {
            outline3.Visible = false;
        }
        // mouse hover event for level 4 - show outline
        void MouseHover4(Object o, EventArgs e)
        {
            outline4.Visible = true;
        }
        // mouse leave event for level 2 - remove outline
        void MouseLeave4(Object o, EventArgs e)
        {
            outline4.Visible = false;
        }
        // mouse hover event for level 4 - show outline
        void MouseHoverBoss(Object o, EventArgs e)
        {
            outlineBoss.Visible = true;
        }
        // mouse leave event for level 2 - remove outline
        void MouseLeaveBoss(Object o, EventArgs e)
        {
            outlineBoss.Visible = false;
        }
        // mouse hover event for level 4 - show outline
        void MouseHoverReturn(Object o, EventArgs e)
        {
            outlineReturn.Visible = true;
        }
        // mouse leave event for level 2 - remove outline
        void MouseLeaveReturn(Object o, EventArgs e)
        {
            outlineReturn.Visible = false;
        }
        // return on click method
        void OnClickReturn(Object o, EventArgs e)
        {
            Global.startScreen.Show(); // show the start screen when clicked
            Close();
        }
        // level 1 button on click method
        void OnLevel1Click(Object o, EventArgs e)
        {
            (new Level1()).Show(); // make a new level 1 form
        }
        // level 2 button on click method
        void OnLevel2Click(Object o, EventArgs e)
        {
            (new Level2()).Show(); // make a new level 2 form
        }
        // level 3 button on click method
        void OnLevel3Click(Object o, EventArgs e)
        {
            (new Level3()).Show(); // make a new level 3 form
        }
        // level 4 button on click method
        void OnLevel4Click(Object o, EventArgs e)
        {
            (new Level4()).Show(); // make a new level 4 form
        }
        // boss level button on click method
        void OnBossLevelClick(Object o, EventArgs e)
        {
            (new BossLevel()).Show(); // make a new level 4 form
        }

        private void LevelDisplay_Load(object sender, EventArgs e)
        {

        }
    }
}
