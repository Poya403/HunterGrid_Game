using EnemyAl.UI_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemyAl.Class
{
    public class Enemy
    {
        public int speed { get; private set; }
        private int chaseDistance;
        private int fleeDistance;

        public Enemy(int speed = 7, int chaseDistance = 1000, int fleeDistance = 200)
        {
            this.speed = speed;
            this.chaseDistance = chaseDistance;
            this.fleeDistance = fleeDistance;
        }

        public (int dx, int dy) MakeDecision(Point enemyPos, Point playerPos, List<Point> otherEnemies)
        {
            int dx = playerPos.X - enemyPos.X;
            int dy = playerPos.Y - enemyPos.Y;

            double distance = Math.Sqrt(dx * dx + dy * dy);

            foreach (var other in otherEnemies) {
                int ox = other.X - enemyPos.X;
                int oy = other.Y - enemyPos.Y;
                double d = Math.Sqrt(ox * ox + oy * oy);

                if (d < 80 && d > 0)
                {
                    dx -= ox / 3;
                    dy -= oy / 3;
                }
            }

            if (distance < chaseDistance)
            {
                if (Math.Abs(dx) > speed) dx = dx > 0 ? speed : -speed;
                if (Math.Abs(dy) > speed) dy = dy > 0 ? speed : -speed;
            }
            else
            {
                dx = 0;
                dy = 0;
            }

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                dy = 0;
            }
            else dx = 0;

            return (dx, dy);
        }
        public void IncreaseSpeed(int value) {
            if (speed + value < 0) return;
            speed += value;
        }
        public void ResetSpeed() => speed = 7;
    }
}
