using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders.Engine
{
    internal class Player : Sprite
    {
        public Player(Texture2D texture) : base(texture)
        {
            position = new Vector2(_graphics.PreferredBackBufferWidth/2 - texture.Width, _graphics.PreferredBackBufferHeight - 20);
        }
        public override void Update()
        {

            base.Update();
        }
        protected override void Move()
        {
            GameTime gameTime = new GameTime();
            double dt = gameTime.ElapsedGameTime.TotalMilliseconds;



        }
    }
}
