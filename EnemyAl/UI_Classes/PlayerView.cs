using EnemyAl.Class;
using EnemyAl.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemyAl.UI_Classes
{
    public class PlayerView
    {
        public PictureBox pictureBox;
        public PictureBox gunStatus;
        public Player player;
        public PlayerView(Player player)
        {
            this.player = player;
            pictureBox = BuildPictureBox(image: Properties.Resources.Player);
            gunStatus = BuildPictureBox(image: Properties.Resources.Gun_Empty);
            gunStatus.Visible = false;
            gunStatus.Size = new Size(pictureBox.Size.Width / 2, pictureBox.Size.Height / 2);
        }
        public PictureBox BuildPictureBox(Image image)
        {
            return new PictureBox()
            {
                Image = image,
                Size = new Size(50, 55),
                SizeMode = PictureBoxSizeMode.Zoom
            };
        }
        public void Move(Keys key) {
            Point pos = pictureBox.Location;

            switch (key)
            {
                case Keys.Up:
                    pos.Y -= player.speed;
                    break;
                case Keys.Down:
                    pos.Y += player.speed;
                    break;
                case Keys.Left:
                    pos.X -= player.speed;
                    pictureBox.Image = Properties.Resources.Player_Left;
                    break;
                case Keys.Right:
                    pos.X += player.speed;
                    pictureBox.Image = Properties.Resources.Player;
                    break;
            }

            UpdateLocations(pos);
        }
        private void UpdateLocations(Point pos) {
            pictureBox.Location = pos;
            gunStatus.Location = new Point(pos.X, pos.Y - 50);
            gunStatus.Visible = true;
        }
        public void ChangeGunStatus(Image image) {
            gunStatus.Image = image;
        }
    }
}
