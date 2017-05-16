using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace End_of_Creation
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;
        Player player2;

        Weapon weapon;

        Texture2D sampleTex;
        Texture2D spritesheet;

        List<Zombie> zombies = new List<Zombie>();
        List<Bullet> bullets;

        bool twoPlayer;
        bool keyboard;

        int count;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            bullets = new List<Bullet>();
            weapon = new Weapon("Pistol", 2.5, 20, 4.5, 20);
            keyboard = false;
            twoPlayer = false;
            player = new Player();
            player2 = new Player();
            count = 0;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            sampleTex = this.Content.Load<Texture2D>("Square");
            spritesheet = this.Content.Load<Texture2D>("EOC_Sprites");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            // Allows the game to exiti
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            count++;
            if (keyboard)
            {
                if (kb.IsKeyDown(Keys.Up) || kb.IsKeyDown(Keys.W))
                    player.yPos -= player.speed;
                if (kb.IsKeyDown(Keys.Down) || kb.IsKeyDown(Keys.S))
                    player.yPos += player.speed;
                if (kb.IsKeyDown(Keys.Right) || kb.IsKeyDown(Keys.D))
                    player.xPos += player.speed;
                if (kb.IsKeyDown(Keys.Left) || kb.IsKeyDown(Keys.A))
                    player.xPos -= player.speed;
                if (twoPlayer)
                {
                    player2.xPos += (int)(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X * player2.speed);
                    player2.yPos -= (int)(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * player2.speed);
                }
                player.bounds = new Rectangle(player.xPos, player.yPos, player.width, player.height);
                player2.bounds = new Rectangle(player2.xPos, player2.yPos, player2.width, player2.height);
            }
            else
            {
                player.xPos += (int)(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X * player.speed);
                player.yPos -= (int)(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * player.speed);
                if (twoPlayer)
                {
                    player2.xPos += (int)(GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X * player2.speed);
                    player2.yPos -= (int)(GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y * player2.speed);
                }
                player.bounds = new Rectangle(player.xPos, player.yPos, player.width, player.height);
                player2.bounds = new Rectangle(player2.xPos, player2.yPos, player2.width, player2.height);
            }
            if(count%60 == 0)
                zombies.Add(new Zombie(GraphicsDevice.Viewport.Width));
            if (keyboard)
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    if (mouse.X <= (player.xPos) && mouse.Y <= (player.yPos))
                    {
                        bullets.Add(new Bullet((player.xPos + (player.width / 2)), (player.yPos + (player.height / 2)),
                            (float)-Math.Cos(Math.Atan((player.yPos) - mouse.Y / ((player.xPos) - mouse.X))),
                            (float)-Math.Sin(Math.Atan((player.yPos) - mouse.Y / ((player.xPos) - mouse.X)))));
                    }
                    else if (mouse.X <= (player.xPos) && mouse.Y > (player.yPos))
                    {
                        bullets.Add(new Bullet((player.xPos + (player.width / 2)), (player.yPos + (player.height / 2)),
                            (float)-Math.Cos(Math.Atan((player.yPos) - mouse.Y / ((player.xPos) - mouse.X))),
                            (float)Math.Sin(Math.Atan((player.yPos) - mouse.Y / ((player.xPos) - mouse.X)))));
                    }
                    else if (mouse.X > (player.xPos) && mouse.Y <= (player.yPos))
                    {
                        bullets.Add(new Bullet((player.xPos + (player.width / 2)), (player.yPos + (player.height / 2)),
                               (float)Math.Cos(Math.Atan((player.yPos) - mouse.Y / ((player.xPos) - mouse.X))),
                               (float)-Math.Sin(Math.Atan((player.yPos - 8) - mouse.Y / ((player.xPos) - mouse.X)))));
                    }
                    else if (mouse.X > (player.xPos) && mouse.Y > (player.yPos))
                    {
                        bullets.Add(new Bullet((player.xPos + (player.width / 2)), (player.yPos + (player.height / 2)),
                            (float)Math.Cos(Math.Atan((player.yPos) - mouse.Y / ((player.xPos) - mouse.X))),
                            (float)Math.Sin(Math.Atan((player.yPos - 8) - mouse.Y / ((player.xPos) - mouse.X)))));
                    }
                }
            if (twoPlayer)
                {
                    if (GamePad.GetState(PlayerIndex.One).Triggers.Right > .5f && count%(int)weapon.fireRate == 0)
                    {
                            bullets.Add(new Bullet((player.xPos + (player.width / 2)), (player.yPos + (player.height / 2)), GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X, -GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y));
                    }
                }
            }
            else
            {
                if (GamePad.GetState(PlayerIndex.One).Triggers.Right > .5f && count % (int)weapon.fireRate == 0)
                {
                        bullets.Add(new Bullet((player.xPos + (player.width / 2)), (player.yPos + (player.height / 2)), GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X, -GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y));
                }
                if (twoPlayer)
                {
                    if (GamePad.GetState(PlayerIndex.Two).Triggers.Right > .5f && count %(int)weapon.fireRate == 0)
                    {
                            bullets.Add(new Bullet((player2.xPos + (player2.width / 2)), (player2.yPos + (player2.height / 2)), GamePad.GetState(PlayerIndex.Two).ThumbSticks.Right.X, -GamePad.GetState(PlayerIndex.Two).ThumbSticks.Right.Y));
                    }
                }
            }
            if (bullets.Count != 0)
            {
                for (int i = bullets.Count-1; i > -1; i--)
                {
                    for (int j = zombies.Count-1; j > -1; j--)
                    {
                        if (new Rectangle((int)bullets[i].xPos, (int)bullets[i].yPos, bullets[i].width, bullets[i].height).Intersects(zombies[j].bounds))
                        {
                            zombies[j].health -= weapon.damage;
                            if (zombies[j].health < 1)
                            {
                                zombies.RemoveAt(j);
                                j--;
                            }
                        }
                    }
                    bullets[i].update();
                    if (bullets[i].yPos < 0)
                    {
                        bullets.RemoveAt(i);
                    }
                    else if (bullets[i].yPos > GraphicsDevice.Viewport.Height)
                    {
                        bullets.RemoveAt(i);
                    }
                    else if (bullets[i].xPos < 0)
                    {
                        bullets.RemoveAt(i);
                    }
                    else if (bullets[i].xPos > GraphicsDevice.Viewport.Width)
                    {
                        bullets.RemoveAt(i);
                    }
                    else if (Math.Sqrt(Math.Pow(bullets[i].vX, 2) + Math.Pow(bullets[i].vY, 2)) < .5)
                    {
                        bullets.RemoveAt(i);
                    }
                }
            }
            for (int k = zombies.Count - 1; k > -1; k--)
            {
                if (!twoPlayer)
                {
                    zombies[k].update(player.xPos, player.yPos);
                }
                else
                {
                    if (zombies[k].target == 1)
                    {
                        zombies[k].update(player.xPos, player.yPos);
                    }
                    else if (zombies[k].target == 2)
                    {
                        zombies[k].update(player2.xPos, player2.yPos);
                    }
                }
                    zombies[k].bounds = new Rectangle(zombies[k].xPos, zombies[k].yPos, zombies[k].width, zombies[k].height);
            }
#if XBOX
            player.xPos = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X * player.speed;
            player.yPos = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * player.speed;
            if(twoPlayer)
            {
                player2.xPos = GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X * player2.speed;
                player2.yPos = GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y * player2.speed;
            }
#endif
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(spritesheet, player.bounds, new Rectangle(0, 0, player.width, player.height), Color.White);
            if (twoPlayer)
                spriteBatch.Draw(spritesheet, player2.bounds, new Rectangle(0, 14, player2.width, player2.height), Color.White);
            for (int i = 0; i < zombies.Count; i++)
            {
                spriteBatch.Draw(sampleTex, zombies[i].bounds, Color.Green);
            }
            if (bullets.Count != 0)
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    spriteBatch.Draw(sampleTex, new Rectangle((int)bullets[i].xPos, (int)bullets[i].yPos, bullets[i].width, bullets[i].height), Color.Orange);
                }
            }
            Console.WriteLine(player.health);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
