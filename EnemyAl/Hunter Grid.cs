using EnemyAl.Class;
using EnemyAl.Core;
using EnemyAl.UI_Classes;
using System;

namespace EnemyAl
{
    public partial class Form1 : Form
    {
        private int enemeyCount = 3;
        private int playerCount = 1;
        private int level = 1;
        private bool isGameOver = false;
        private List<Bullet> bullets = new List<Bullet>();
        private DateTime lastShotTime = DateTime.MinValue;
        private int shootCooldown = 6;
        private int score = 0;

        private Dictionary<Enemy, EnemyView> enemyViews = new Dictionary<Enemy, EnemyView>();
        private PlayerView playerView;
        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            KeyDown += Form1_KeyDown;

            Shown += (s, e) =>
            {
                ActiveControl = null;
                Focus();
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeCharacters();
            gameTimer.Interval = 50;
            gameTimer.Tick += gameTimer_Tick;
            gameTimer.Start();
            btnRestart.TabStop = false;
        }

        private void UpdateLabel() {
            lblScore.Text = $"Score : {score}";
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            playerView.Move(e.KeyCode);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
            {
                playerView.Move(keyData);
                return true;
            }
            if (keyData == Keys.PageUp || keyData == Keys.PageDown)
            {
                var now = DateTime.Now;
                if ((now - lastShotTime).TotalSeconds >= shootCooldown)
                {
                    if(keyData == Keys.PageUp) ShootBullet(direction: -1);
                    else ShootBullet(direction: 1);
                    lastShotTime = now;
                }
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (isGameOver) return;

            Point playerPos = playerView.pictureBox.Location;

            foreach (var enemy in enemyViews.Keys)
            {
                Point enemyPos = enemyViews[enemy].pictureBox.Location;

                List<Point> otherEnemies = enemyViews
                    .Where(kv => kv.Key != enemy)
                    .Select(kv => kv.Value.pictureBox.Location)
                    .ToList();

                var (dx, dy) = enemy.MakeDecision(enemyPos, playerPos, otherEnemies);

                enemyViews[enemy].pictureBox.Location = new Point(enemyPos.X + dx, enemyPos.Y + dy);
                enemyViews[enemy].UpdateDirectionImage(dx, dy);

                if (enemyViews[enemy].CollidesWith(playerView))
                {
                    isGameOver = true;
                    gameTimer.Stop();
                    MessageBox.Show("Game Over!");
                    return;
                }
            }
            double secondsPassed = (DateTime.Now - lastShotTime).TotalSeconds;

            if (secondsPassed >= shootCooldown)
                playerView.ChangeGunStatus(Properties.Resources.Gun_Full);
            else
                playerView.ChangeGunStatus(Properties.Resources.Gun_Empty);

            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                Bullet b = bullets[i];
                if (!b.isActive)
                    continue;

                b.Move();

                foreach (var enemy in enemyViews.Keys.ToList())
                {
                    if (b.pictureBox.Bounds.IntersectsWith(enemyViews[enemy].pictureBox.Bounds))
                    {
                        score++;
                        if (score >= level * 4)
                        {
                            level++;
                            IncreaseEnemySpeed();
                        }
                        UpdateLabel();

                        Controls.Remove(enemyViews[enemy].pictureBox);
                        enemyViews.Remove(enemy);

                        Controls.Remove(b.pictureBox);
                        b.isActive = false;

                        SpawnNewEnemy();
                        break;
                    }
                }

                if (b.pictureBox.Right > this.ClientSize.Width)
                {
                    Controls.Remove(b.pictureBox);
                    b.isActive = false;
                }
            }
        }
        private void InitializeCharacters()
        {
            Random rnd = new Random();
            playerView = new PlayerView(new Player());
            playerView.pictureBox.Location = new Point(31, 320);
            Controls.Add(playerView.pictureBox);
            Controls.Add(playerView.gunStatus);

            List<Enemy> enemies = new List<Enemy>();

            for (int i = 0; i < enemeyCount; i++)
                enemies.Add(new Enemy());

            int spacing = 0;
            foreach (Enemy e in enemies)
            {
                var view = new EnemyView(e);
                view.pictureBox.Location = new Point(800, 100 + rnd.Next(50, ClientSize.Height - 150));
                Controls.Add(view.pictureBox);
                enemyViews[e] = view;
                spacing += 110;
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            playerView.pictureBox.Location = new Point(31, 320);
            playerView.gunStatus.Visible = false;

            int spacing = 0;
            level = 1;
            foreach (var key in enemyViews.Keys)
            {
                enemyViews[key].pictureBox.Location = new Point(800, 100 + spacing);
                enemyViews[key].enemy.ResetSpeed();
                spacing += 110;
            }

            isGameOver = false;
            gameTimer.Tick -= gameTimer_Tick;
            gameTimer.Tick += gameTimer_Tick;
            gameTimer.Start();
            score = 0;
            UpdateLabel();
        }

        private void ShootBullet(int direction)
        {
            Point startPos = new Point(
                playerView.pictureBox.Right,
                playerView.pictureBox.Top + playerView.pictureBox.Height / 2);

            Bullet bullet = new Bullet(startPos, direction);
            bullets.Add(bullet);
            Controls.Add(bullet.pictureBox);
            bullet.pictureBox.BringToFront();
        }

        private void SpawnNewEnemy()
        {
            Enemy e = new Enemy();
            var view = new EnemyView(e);

            Random rnd = new Random();
            int x = ClientSize.Width - 100;
            int y = rnd.Next(50, ClientSize.Height - 150);
            view.pictureBox.Location = new Point(x, y);

            Controls.Add(view.pictureBox);
            enemyViews[e] = view;
        }

        private void IncreaseEnemySpeed()
        {
            foreach (var enemy in enemyViews.Keys)
            {
                enemy.IncreaseSpeed(1);
            }
        }
    }
}
