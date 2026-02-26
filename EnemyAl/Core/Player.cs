using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemyAl.Core
{
    public class Player
    {
        public int speed { get; private set; }

        public Player(int speed = 20) {
            this.speed = speed;
        }
    }
}
