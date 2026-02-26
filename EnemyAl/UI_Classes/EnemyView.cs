using EnemyAl.Class;
using EnemyAl.Core;

namespace EnemyAl.UI_Classes
{
    public class EnemyView
    {
        public PictureBox pictureBox;
        public Enemy enemy;
        public EnemyView(Enemy enemy) {
            this.enemy = enemy;
            pictureBox = BuildPictureBox();
        }
        public PictureBox BuildPictureBox() {
            return new PictureBox()
            {
                Image = Properties.Resources.Enemy_Left,
                Size = new Size(65, 75),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent,
            };
        }
        public bool CollidesWith(PlayerView player)
        {
            Rectangle enemyRect = new Rectangle(
                pictureBox.Location, new Size(pictureBox.Width, pictureBox.Height));

            var playerPb = player.pictureBox;
            Rectangle playerRect = new Rectangle(
                playerPb.Location, new Size(playerPb.Width, playerPb.Height));

            return enemyRect.IntersectsWith(playerRect);
        }
        public void UpdateDirectionImage(int dx, int dy)
        {
            if (dx < 0)
                pictureBox.Image = Properties.Resources.Enemy_Left;
            else if (dx > 0)
                pictureBox.Image = Properties.Resources.Enemy;
        }
    }
}
