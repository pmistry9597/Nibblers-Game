namespace snake_program
{
    partial class StartScreen
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
            this.Title = new System.Windows.Forms.PictureBox();
            this.btnTwoPlayer = new System.Windows.Forms.PictureBox();
            this.btnSinglePlayer = new System.Windows.Forms.PictureBox();
            this.outlineSingle = new System.Windows.Forms.PictureBox();
            this.outlineTwo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Title)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTwoPlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSinglePlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outlineSingle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outlineTwo)).BeginInit();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.Image = global::snake_program.Properties.Resources.Nibblers;
            this.Title.Location = new System.Drawing.Point(174, 47);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(316, 107);
            this.Title.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Title.TabIndex = 3;
            this.Title.TabStop = false;
            // 
            // btnTwoPlayer
            // 
            this.btnTwoPlayer.Image = global::snake_program.Properties.Resources.two_player;
            this.btnTwoPlayer.Location = new System.Drawing.Point(260, 297);
            this.btnTwoPlayer.Name = "btnTwoPlayer";
            this.btnTwoPlayer.Size = new System.Drawing.Size(150, 74);
            this.btnTwoPlayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnTwoPlayer.TabIndex = 2;
            this.btnTwoPlayer.TabStop = false;
            // 
            // btnSinglePlayer
            // 
            this.btnSinglePlayer.Image = global::snake_program.Properties.Resources.Single_Player;
            this.btnSinglePlayer.Location = new System.Drawing.Point(260, 190);
            this.btnSinglePlayer.Name = "btnSinglePlayer";
            this.btnSinglePlayer.Size = new System.Drawing.Size(150, 74);
            this.btnSinglePlayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnSinglePlayer.TabIndex = 1;
            this.btnSinglePlayer.TabStop = false;
            // 
            // outlineSingle
            // 
            this.outlineSingle.BackColor = System.Drawing.Color.Brown;
            this.outlineSingle.Location = new System.Drawing.Point(251, 181);
            this.outlineSingle.Name = "outlineSingle";
            this.outlineSingle.Size = new System.Drawing.Size(168, 91);
            this.outlineSingle.TabIndex = 4;
            this.outlineSingle.TabStop = false;
            this.outlineSingle.Visible = false;
            // 
            // outlineTwo
            // 
            this.outlineTwo.BackColor = System.Drawing.Color.Brown;
            this.outlineTwo.Location = new System.Drawing.Point(251, 290);
            this.outlineTwo.Name = "outlineTwo";
            this.outlineTwo.Size = new System.Drawing.Size(168, 89);
            this.outlineTwo.TabIndex = 5;
            this.outlineTwo.TabStop = false;
            this.outlineTwo.Visible = false;
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(694, 504);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.btnTwoPlayer);
            this.Controls.Add(this.btnSinglePlayer);
            this.Controls.Add(this.outlineSingle);
            this.Controls.Add(this.outlineTwo);
            this.Name = "StartScreen";
            this.Text = "Nibblers";
            this.Load += new System.EventHandler(this.StartScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Title)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTwoPlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSinglePlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outlineSingle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outlineTwo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox btnSinglePlayer;
        private System.Windows.Forms.PictureBox btnTwoPlayer;
        private System.Windows.Forms.PictureBox Title;
        private System.Windows.Forms.PictureBox outlineSingle;
        private System.Windows.Forms.PictureBox outlineTwo;
    }
}