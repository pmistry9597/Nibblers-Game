namespace snake_program
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.runTimer = new System.Windows.Forms.Timer(this.components);
            this.bannerTimer = new System.Windows.Forms.Timer(this.components);
            this.enemy1 = new System.Windows.Forms.PictureBox();
            this.coin7 = new System.Windows.Forms.PictureBox();
            this.coin6 = new System.Windows.Forms.PictureBox();
            this.coin5 = new System.Windows.Forms.PictureBox();
            this.obstacle4 = new System.Windows.Forms.PictureBox();
            this.obstacle3 = new System.Windows.Forms.PictureBox();
            this.obstacle2 = new System.Windows.Forms.PictureBox();
            this.obstacle1 = new System.Windows.Forms.PictureBox();
            this.coin2 = new System.Windows.Forms.PictureBox();
            this.coin3 = new System.Windows.Forms.PictureBox();
            this.coin4 = new System.Windows.Forms.PictureBox();
            this.coin1 = new System.Windows.Forms.PictureBox();
            this.snakeHead = new System.Windows.Forms.PictureBox();
            this.spawnPadPic2 = new System.Windows.Forms.PictureBox();
            this.spawnPadPic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.enemy1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coin7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coin6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coin5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.obstacle4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.obstacle3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.obstacle2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.obstacle1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coin2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coin3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coin4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.coin1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.snakeHead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spawnPadPic2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spawnPadPic)).BeginInit();
            this.SuspendLayout();
            // 
            // runTimer
            // 
            this.runTimer.Interval = 1;
            this.runTimer.Tick += new System.EventHandler(this.runTimer_Tick);
            // 
            // bannerTimer
            // 
            this.bannerTimer.Interval = 2000;
            // 
            // enemy1
            // 
            this.enemy1.Image = global::snake_program.Properties.Resources.enemy_guardian;
            this.enemy1.Location = new System.Drawing.Point(180, 268);
            this.enemy1.Name = "enemy1";
            this.enemy1.Size = new System.Drawing.Size(36, 37);
            this.enemy1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.enemy1.TabIndex = 14;
            this.enemy1.TabStop = false;
            // 
            // coin7
            // 
            this.coin7.Image = global::snake_program.Properties.Resources.coin1;
            this.coin7.Location = new System.Drawing.Point(213, 234);
            this.coin7.Name = "coin7";
            this.coin7.Size = new System.Drawing.Size(20, 20);
            this.coin7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.coin7.TabIndex = 11;
            this.coin7.TabStop = false;
            // 
            // coin6
            // 
            this.coin6.Image = global::snake_program.Properties.Resources.coin1;
            this.coin6.Location = new System.Drawing.Point(180, 95);
            this.coin6.Name = "coin6";
            this.coin6.Size = new System.Drawing.Size(20, 20);
            this.coin6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.coin6.TabIndex = 10;
            this.coin6.TabStop = false;
            // 
            // coin5
            // 
            this.coin5.Image = global::snake_program.Properties.Resources.coin1;
            this.coin5.Location = new System.Drawing.Point(869, 285);
            this.coin5.Name = "coin5";
            this.coin5.Size = new System.Drawing.Size(20, 20);
            this.coin5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.coin5.TabIndex = 9;
            this.coin5.TabStop = false;
            // 
            // obstacle4
            // 
            this.obstacle4.Location = new System.Drawing.Point(260, 268);
            this.obstacle4.Name = "obstacle4";
            this.obstacle4.Size = new System.Drawing.Size(156, 239);
            this.obstacle4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.obstacle4.TabIndex = 8;
            this.obstacle4.TabStop = false;
            // 
            // obstacle3
            // 
            this.obstacle3.Location = new System.Drawing.Point(80, 285);
            this.obstacle3.Name = "obstacle3";
            this.obstacle3.Size = new System.Drawing.Size(70, 200);
            this.obstacle3.TabIndex = 7;
            this.obstacle3.TabStop = false;
            // 
            // obstacle2
            // 
            this.obstacle2.Location = new System.Drawing.Point(28, 147);
            this.obstacle2.Name = "obstacle2";
            this.obstacle2.Size = new System.Drawing.Size(374, 63);
            this.obstacle2.TabIndex = 6;
            this.obstacle2.TabStop = false;
            // 
            // obstacle1
            // 
            this.obstacle1.Location = new System.Drawing.Point(709, 147);
            this.obstacle1.Name = "obstacle1";
            this.obstacle1.Size = new System.Drawing.Size(100, 312);
            this.obstacle1.TabIndex = 5;
            this.obstacle1.TabStop = false;
            // 
            // coin2
            // 
            this.coin2.Image = global::snake_program.Properties.Resources.coin1;
            this.coin2.Location = new System.Drawing.Point(396, 95);
            this.coin2.Name = "coin2";
            this.coin2.Size = new System.Drawing.Size(20, 20);
            this.coin2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.coin2.TabIndex = 4;
            this.coin2.TabStop = false;
            // 
            // coin3
            // 
            this.coin3.Image = global::snake_program.Properties.Resources.coin1;
            this.coin3.Location = new System.Drawing.Point(670, 224);
            this.coin3.Name = "coin3";
            this.coin3.Size = new System.Drawing.Size(20, 20);
            this.coin3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.coin3.TabIndex = 3;
            this.coin3.TabStop = false;
            // 
            // coin4
            // 
            this.coin4.Image = global::snake_program.Properties.Resources.coin1;
            this.coin4.Location = new System.Drawing.Point(740, 112);
            this.coin4.Name = "coin4";
            this.coin4.Size = new System.Drawing.Size(20, 20);
            this.coin4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.coin4.TabIndex = 2;
            this.coin4.TabStop = false;
            // 
            // coin1
            // 
            this.coin1.Image = global::snake_program.Properties.Resources.coin1;
            this.coin1.Location = new System.Drawing.Point(562, 147);
            this.coin1.Name = "coin1";
            this.coin1.Size = new System.Drawing.Size(20, 20);
            this.coin1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.coin1.TabIndex = 1;
            this.coin1.TabStop = false;
            // 
            // snakeHead
            // 
            this.snakeHead.Image = global::snake_program.Properties.Resources.green_box_hi;
            this.snakeHead.Location = new System.Drawing.Point(562, 23);
            this.snakeHead.Name = "snakeHead";
            this.snakeHead.Size = new System.Drawing.Size(50, 46);
            this.snakeHead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.snakeHead.TabIndex = 0;
            this.snakeHead.TabStop = false;
            // 
            // spawnPadPic2
            // 
            this.spawnPadPic2.Location = new System.Drawing.Point(719, 23);
            this.spawnPadPic2.Name = "spawnPadPic2";
            this.spawnPadPic2.Size = new System.Drawing.Size(170, 77);
            this.spawnPadPic2.TabIndex = 13;
            this.spawnPadPic2.TabStop = false;
            // 
            // spawnPadPic
            // 
            this.spawnPadPic.Location = new System.Drawing.Point(455, 234);
            this.spawnPadPic.Name = "spawnPadPic";
            this.spawnPadPic.Size = new System.Drawing.Size(168, 285);
            this.spawnPadPic.TabIndex = 12;
            this.spawnPadPic.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(920, 556);
            this.Controls.Add(this.enemy1);
            this.Controls.Add(this.coin7);
            this.Controls.Add(this.coin6);
            this.Controls.Add(this.coin5);
            this.Controls.Add(this.obstacle4);
            this.Controls.Add(this.obstacle3);
            this.Controls.Add(this.obstacle2);
            this.Controls.Add(this.obstacle1);
            this.Controls.Add(this.coin2);
            this.Controls.Add(this.coin3);
            this.Controls.Add(this.coin4);
            this.Controls.Add(this.coin1);
            this.Controls.Add(this.snakeHead);
            this.Controls.Add(this.spawnPadPic2);
            this.Controls.Add(this.spawnPadPic);
            this.Name = "Form1";
            this.Text = "Test Frame";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.enemy1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coin7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coin6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coin5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.obstacle4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.obstacle3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.obstacle2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.obstacle1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coin2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coin3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coin4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.coin1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.snakeHead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spawnPadPic2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spawnPadPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer runTimer;
        private System.Windows.Forms.PictureBox snakeHead;
        private System.Windows.Forms.PictureBox obstacle1;
        private System.Windows.Forms.PictureBox obstacle2;
        private System.Windows.Forms.PictureBox obstacle3;
        private System.Windows.Forms.PictureBox obstacle4;
        private System.Windows.Forms.PictureBox coin4;
        private System.Windows.Forms.PictureBox coin1;
        private System.Windows.Forms.PictureBox coin2;
        private System.Windows.Forms.PictureBox coin3;
        private System.Windows.Forms.PictureBox coin5;
        private System.Windows.Forms.PictureBox coin6;
        private System.Windows.Forms.PictureBox coin7;
        private System.Windows.Forms.PictureBox spawnPadPic;
        private System.Windows.Forms.PictureBox spawnPadPic2;
        private System.Windows.Forms.Timer bannerTimer;
        private System.Windows.Forms.PictureBox enemy1;
    }
}

