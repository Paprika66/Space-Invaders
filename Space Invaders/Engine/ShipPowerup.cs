using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders.Engine
{
    class ShipPowerup : Sprite
    {

        PowerUpType type;
        public ShipPowerup(Texture2D texture) : base(texture)
        {
        }
        public override void Update(GameTime gameTime, ref List<Sprite> sprites)
        {
            base.Update(gameTime, ref sprites);
            GettingHit(ref sprites);
            if (!alive)
            {
                type = (PowerUpType)Globals.rng.Next(0, 2);
                int i = 0;
                while (i<sprites.Count && !(sprites[i] is Player))
                {
                    i++;
                }
                if (type == PowerUpType.ShootingSpeed)
                {
                    if ((sprites[i] as Player).shootingSpeedDelay > 300)
                    {
                        (sprites[i] as Player).shootingSpeedDelay -= 100;
                    }
                }
                else if (type == PowerUpType.Movingspeed)
                {
                    if ((sprites[i] as Player).speed.X < 0.4f )
                    {
                        (sprites[i] as Player).speed.X += 0.1f;
                    }
                }
            }
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    enum PowerUpType
    {
        ShootingSpeed = 0,
        Movingspeed = 1,
    }
}
