using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Space_Invaders.Engine
{
    internal class Enemy : Sprite
    {
        public delegate void PowerupHandler();
        public event PowerupHandler ShipPowerup;
        public Bullet bullet;
        bool movingLeft = false;
        float startingPositionX;
        float startingPositionY;
        float targetPositionY;
        int moveDownTime;
        bool canShoot = false;
        bool trigger = false;
        Stopwatch sw = new Stopwatch();
        public Enemy(Texture2D texture, Vector2 position, int level) : base(texture)
        {
            level = MathHelper.Clamp(level, 0, 9);

            moveDownTime = 5000 - level * 500;
            speed.X = 0.15f + (float)level/20;
            speed.Y = 0.1f;
            this.position = position;
            startingPositionX = position.X;
            startingPositionY = position.Y;
            targetPositionY = position.Y;
        }
        public override void Update(GameTime gameTime, ref List<Sprite> sprites)
        {
            Move(gameTime);
            GettingHit(ref sprites);
            Shoot(ref sprites);
        }
        protected override void GettingHit(ref List<Sprite> sprites)
        {
            base.GettingHit(ref sprites);
            if (!alive)
            {
                Globals.score += (int)position.Y * Globals.level+1; 
                int i = 0;
                while (i <sprites.Count && !(sprites[i] is Player))
                {
                    i++;
                }
                if ((sprites[i] as Player).speed.X != 0.5f || (sprites[i] as Player).shootingSpeedDelay!=300)
                {
                    int value = Globals.rng.Next(0, 101);
                    if (value >= 0 && value <= 10)
                    {
                        ShipPowerup();
                    }
                }
            }
        }
        protected override void Move(GameTime gameTime)
        {
            sw.Start();
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;
            if (movingLeft)
            {
                position.X = (float)Math.Round((float)(position.X - speed.X * dt), 1);
                if (position.X <= startingPositionX)
                {
                    movingLeft = false;
                }
            }
            else
            {
                position.X = (float)Math.Round((float)(position.X + speed.X * dt),1);
                if (position.X - startingPositionX > 150)
                {
                    movingLeft = true;
                }
            }
            if (targetPositionY > position.Y)
            {
                position.Y = (float)(position.Y + speed.Y * dt);
            } 
            if (sw.ElapsedMilliseconds > moveDownTime && position.Y < 550)
            {
                sw.Restart();
                targetPositionY = position.Y + 50;
            }
            if (position.Y >= 550)
            {
                canShoot = true;
                if (!trigger)
                {
                    sw.Restart();
                    trigger = true;
                }
            }
        }
        protected override void Shoot(ref List<Sprite> sprites)
        {
            if (sw.ElapsedMilliseconds > 1000 && canShoot)
            {
                sw.Reset();
                Bullet newbullet = bullet.Clone() as Bullet;
                newbullet.position = new Vector2(this.HitBox.Center.X - 10, position.Y + texture.Height + 2);
                newbullet.speed.Y = -0.6f;

                sprites.Add(newbullet);
            }
        }
    }
}
