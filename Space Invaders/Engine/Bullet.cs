using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Space_Invaders.Engine
{
    internal class Bullet : Sprite
    {
        public Bullet(Texture2D texture) : base(texture)
        {
            speed.Y = -200f;
        }
        public override void Update(GameTime gameTime,ref  List<Sprite> sprites)
        {
            Move(gameTime);
        }
        protected override void Move(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (position.Y > 0)
            {
                position.Y = position.Y - speed.Y * dt;
            }
            if (!NoScreenCollision(new Vector2(position.X, position.Y - speed.Y * dt)))
            {
                alive = false;
            }
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
