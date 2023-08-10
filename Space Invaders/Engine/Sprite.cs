using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Space_Invaders.Engine;
using System;
using System.Collections.Generic;

namespace Space_Invaders
{
    internal class Sprite
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 speed;
        public bool alive = true;
        public Rectangle HitBox { get { return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); } }

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
        }
        public virtual void Update(GameTime gameTime,ref  List<Sprite> sprites) { }
        protected virtual void Move(GameTime gameTime) { }
        protected virtual void Stop() { }
        protected virtual void Shoot(ref List<Sprite> sprites) {}
        protected virtual void GettingHit(ref List<Sprite> sprites) 
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                if ((sprites[i] is Bullet) && this.HitBox.Intersects(sprites[i].HitBox))
                {
                    alive = false;
                    sprites[i].alive = false;
                }
            }       
        }
        public bool CollisionCheck(Rectangle hitbox) 
        { 
            return HitBox.Intersects(hitbox); 
        }
        protected bool NoScreenCollision(Vector2 position)
        {
            return (position.X + texture.Width <= 640 && position.X >= 0) && (position.Y>=0 && position.Y <= 720);
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            if (alive) 
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }
    }
}
