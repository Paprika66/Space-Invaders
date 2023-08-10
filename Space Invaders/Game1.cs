using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Space_Invaders.Engine;
using System.Collections.Generic;
using System.Diagnostics;

namespace Space_Invaders
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;
        Stopwatch sw = new Stopwatch();


        Player player;
        Sprite bg;
        ShipPowerup shipPowerup;
        List<Sprite> sprites = new List<Sprite>();
        //List<Enemy> enemies = new List<Enemy>();
        //List<Bullet> bullets = new List<Bullet>();
        Color backgroundColor = new Color(76, 76, 76);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            Window.Title = "Space Invaders";
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>("font");
            shipPowerup = new ShipPowerup(Content.Load<Texture2D>("UFO"));
            bg = new Sprite(Content.Load<Texture2D>("Earth"));
            bg.position = new Vector2(0, _graphics.PreferredBackBufferHeight - bg.texture.Height);
            player = new Player(Content.Load<Texture2D>("UFO"));
            player.position = new Vector2(_graphics.PreferredBackBufferWidth / 2 - player.texture.Width, _graphics.PreferredBackBufferHeight - bg.texture.Height - player.texture.Height);
            player.bullet = new Bullet(Content.Load<Texture2D>("UFO"));

            NewWave();

            sprites.Add(player);
            sprites.Add(bg);
        }



        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here

            for (int i = 0; i < sprites.Count; i++)
            {
                sprites[i].Update(gameTime, ref sprites);
                if (!sprites[i].alive) { sprites.Remove(sprites[i]); }
            }

            if (NoMoreEnemy())
            {
                NewWave();
                Globals.score += 1000;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            _spriteBatch.Begin();
            // TODO: Add your drawing code here
            foreach (var sprite in sprites)
            {
                sprite.Draw(_spriteBatch);
            }
            _spriteBatch.DrawString(font, Globals.score.ToString(), new Vector2(5,5), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void SpawnPowerup()
        {
            ShipPowerup newPowerup = shipPowerup.Clone() as ShipPowerup;
            newPowerup.position.Y = Globals.rng.Next(0,500);
            newPowerup.position.X = Globals.rng.Next(100, 500);

            sprites.Add(newPowerup);
        }
        private bool NoMoreEnemy()
        {
            int i = 0;
            while (i<sprites.Count && !(sprites[i] is Enemy))
            {
                i++;
            }
            return i>=sprites.Count;
        }
        private void NewWave()
        {
            for (int i = 10; i < _graphics.PreferredBackBufferHeight / 4; i += 60)
            {
                for (int j = 10; j < _graphics.PreferredBackBufferWidth - 200; j += 100)
                {
                    Enemy newenemy = new Enemy(Content.Load<Texture2D>("UFO"), new Vector2(j, i), Globals.level);
                    newenemy.ShipPowerup += SpawnPowerup;
                    newenemy.bullet = new Bullet(Content.Load<Texture2D>("UFO"));
                    sprites.Add(newenemy);
                }
            }
            Globals.level++;
        }
    }
}