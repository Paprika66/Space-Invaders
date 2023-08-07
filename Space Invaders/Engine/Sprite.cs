using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    internal class Sprite
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 speed;
        protected GraphicsDeviceManager _graphics;
        public Rectangle HitBox { get { return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); } }

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
        }
        public virtual void Update() { }
        protected virtual void Move() { }
        protected virtual void Stop() { }
        public bool CollisionCheck(Rectangle hitbox) 
        { 
            return HitBox.Intersects(hitbox); 
        }
        protected bool NoScreenCollision(int positionX)
        {
            return positionX <= _graphics.PreferredBackBufferWidth && positionX >= 0;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
