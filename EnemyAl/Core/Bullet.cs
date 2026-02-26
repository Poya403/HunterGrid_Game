using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemyAl.Core
{
    public class Bullet
    {
        public PictureBox pictureBox;
        public int speed = 20;
        public bool isActive = true;
        private int direction = 1;
        public Bullet(Point startPos, int direction)
        {
            pictureBox = new PictureBox()
            {
                Size = new Size(10, 5),
                BackColor = Color.Red,
                Location = startPos
            };
            this.direction = direction;
        }

        public void Move()
        {
            if (!isActive) return;
            pictureBox.Left += direction * speed;
        }
    }
}
