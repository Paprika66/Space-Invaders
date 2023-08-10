using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;

namespace Space_Invaders.Engine
{
    internal class Player : Sprite
    {
        public Bullet bullet;
        Stopwatch sw = new Stopwatch();
        public int shootingSpeedDelay = 1000;
        float shootingBullettSpeed = 1f;
        public Player(Texture2D texture) : base(texture)
        {
            speed.X = 0.1f;
        }
        public override void Update(GameTime gameTime, ref List<Sprite> sprites)
        {
            Move(gameTime);
            Shoot(ref sprites);
            GettingHit(ref sprites);
        }
        protected override void Move(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;
            //
            if (Keyboard.GetState().IsKeyDown(Keys.D) && NoScreenCollision(new Vector2((int)(position.X + speed.X*dt),position.Y)))
            {
                position.X = (float)(position.X + speed.X*dt);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A) && NoScreenCollision(new Vector2((int)(position.X - speed.X * dt), position.Y)))
            {
                position.X = (float)(position.X - speed.X * dt);
            }
        }
        protected override void Shoot(ref List<Sprite> sprites)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (sw.ElapsedMilliseconds > shootingSpeedDelay || sw.ElapsedMilliseconds == 0)
                {
                    sw.Restart();
                    Bullet newbullet = bullet.Clone() as Bullet;
                    newbullet.position = new Vector2(this.HitBox.Center.X-bullet.texture.Width,position.Y-40);
                    newbullet.speed.Y = shootingBullettSpeed;

                    sprites.Add(newbullet);
                }
            }
        }
    }
}
