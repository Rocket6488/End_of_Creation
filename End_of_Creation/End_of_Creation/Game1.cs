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

        SpriteFont font;

        Texture2D sampleTex;
        Texture2D spritesheet;

        List<Weapon> weapons;
        List<Weapon> weapons2;
        List<Zombie> zombies = new List<Zombie>();
        List<Bullet> bullets;

        bool twoPlayer;
        bool keyboard;

        int mouseX;
        int mouseY;
        int count;
        int w1;
        int w2;
        int rate;
        int ups;
        int load;
        int load2;

        double xSpd;
        double ySpd;

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
            weapons = new List<Weapon>();
            weapons2 = new List<Weapon>();
            weapons.Add(new Weapon("Pistol", 50, 20, (int)(60*4.5), 10));
            weapons.Add(new Weapon("Rifle", 75, 10, 60*7, 25));
            weapons.Add(new Weapon("Machine Pistol", 10, 50, (int)(60*.5), 7));
            weapons.Add(new Weapon("Machine Gun", 5, 200, (int)(60*1.75), 5));
            weapons2.Add(new Weapon("Pistol", 50, 20, (int)(60*4.5), 10));
            weapons2.Add(new Weapon("Rifle", 75, 10, 60*7, 25));
            weapons2.Add(new Weapon("Machine Pistol", 10, 50, (int)(60*.75), 7));
            weapons2.Add(new Weapon("Machine Gun", 5, 200, (int)(60*2.75), 5));
            keyboard = false;
            twoPlayer = false;
            player = new Player();
            player2 = new Player();
            count = 0;
            rate = 60;
            ups = 0;
            load = 0;
            load2 = 0;
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
            font = this.Content.Load<SpriteFont>("SpriteFont1");
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
            if (GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed)
            {
                twoPlayer = true;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
            {
                keyboard = true;
            }
            if (kb.IsKeyDown(Keys.X))
            {
                keyboard = false;
            }
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
                if (player.xPos < 0)
                {
                    player.xPos = 0;
                }
                if (player.yPos < 0)
                {
                    player.yPos = 0;
                }
                if (player.xPos > GraphicsDevice.Viewport.Width-player.width)
                {
                    player.xPos = GraphicsDevice.Viewport.Width - player.width;
                }
                if (player.yPos > GraphicsDevice.Viewport.Height-player.height)
                {
                    player.yPos = GraphicsDevice.Viewport.Height - player.height;
                }
                if (twoPlayer)
                {
                    player2.xPos += (int)(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X * player2.speed);
                    player2.yPos -= (int)(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * player2.speed);
                    if (player2.xPos < 0)
                    {
                        player2.xPos = 0;
                    }
                    if (player2.yPos < 0)
                    {
                        player2.yPos = 0;
                    }
                    if (player2.xPos > GraphicsDevice.Viewport.Width-player2.width)
                    {
                        player2.xPos = GraphicsDevice.Viewport.Width - player2.width;
                    }
                    if (player2.yPos > GraphicsDevice.Viewport.Height-player2.height)
                    {
                        player2.yPos = GraphicsDevice.Viewport.Height - player2.height;
                    }
                }
                player.bounds = new Rectangle(player.xPos, player.yPos, player.width, player.height);
                player2.bounds = new Rectangle(player2.xPos, player2.yPos, player2.width, player2.height);
            }
            else
            {
                player.xPos += (int)(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X * player.speed);
                player.yPos -= (int)(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * player.speed);
                if (player.xPos < 0)
                {
                    player.xPos = 0;
                }
                if (player.yPos < 0)
                {
                    player.yPos = 0;
                }
                if (player.xPos > GraphicsDevice.Viewport.Width-player.width)
                {
                    player.xPos = GraphicsDevice.Viewport.Width - player.width;
                }
                if (player.yPos > GraphicsDevice.Viewport.Height-player.height)
                {
                    player.yPos = GraphicsDevice.Viewport.Height - player.height;
                }
                if (twoPlayer)
                {
                    player2.xPos += (int)(GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X * player2.speed);
                    player2.yPos -= (int)(GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y * player2.speed);
                    if (player2.xPos < 0)
                    {
                        player2.xPos = 0;
                    }
                    if (player2.yPos < 0)
                    {
                        player2.yPos = 0;
                    }
                    if (player2.xPos > GraphicsDevice.Viewport.Width-player2.width)
                    {
                        player2.xPos = GraphicsDevice.Viewport.Width - player2.width;
                    }
                    if (player2.yPos > GraphicsDevice.Viewport.Height-player2.height)
                    {
                        player2.yPos = GraphicsDevice.Viewport.Height - player2.height;
                    }
                }
                player.bounds = new Rectangle(player.xPos, player.yPos, player.width, player.height);
                player2.bounds = new Rectangle(player2.xPos, player2.yPos, player2.width, player2.height);
            }
            if(count%rate == 0)
                zombies.Add(new Zombie(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
            if (keyboard)
            {
                if (mouse.LeftButton == ButtonState.Pressed && count % weapons[w1].fireRate == 0)
                {
                    if (load == 0)
                    {
                        load = weapons[w1].reload();
                        if (mouse.X <= (player.xPos) && mouse.Y <= (player.yPos))
                        {
                            mouseX = ((player.xPos) - mouse.X);
                            mouseY = ((player.yPos) - mouse.Y);
                            xSpd = -Math.Cos(Math.Atan(mouseY / mouseX));
                            ySpd = -Math.Sin(Math.Atan(mouseY / mouseX));
                            bullets.Add(new Bullet((player.xPos + (player.width / 2)), (player.yPos + (player.height / 2)), (float)xSpd, (float)ySpd));
                        }
                        else if (mouse.X <= (player.xPos) && mouse.Y > (player.yPos))
                        {
                            mouseX = ((player.xPos) - mouse.X);
                            mouseY = (mouse.Y - (player.yPos));
                            xSpd = -Math.Cos(Math.Atan(mouseY / mouseX));
                            ySpd = Math.Sin(Math.Atan(mouseY / mouseX));
                            bullets.Add(new Bullet((player.xPos + (player.width / 2)), (player.yPos + (player.height / 2)), (float)xSpd, (float)ySpd));
                        }
                        else if (mouse.X > (player.xPos) && mouse.Y <= (player.yPos))
                        {
                            mouseX = (mouse.X - (player.xPos));
                            mouseY = ((player.yPos) - mouse.Y);
                            xSpd = Math.Cos(Math.Atan(mouseY / mouseX));
                            ySpd = -Math.Sin(Math.Atan(mouseY / mouseX));
                            bullets.Add(new Bullet((player.xPos + (player.width / 2)), (player.yPos + (player.height / 2)), (float)xSpd, (float)ySpd));
                        }
                        else if (mouse.X > (player.xPos) && mouse.Y > (player.yPos))
                        {
                            mouseX = (mouse.X - (player.xPos - 8));
                            mouseY = (mouse.Y - (player.yPos - 8));
                            xSpd = Math.Cos(Math.Atan(mouseY / mouseX));
                            ySpd = Math.Sin(Math.Atan(mouseY / mouseX));
                            bullets.Add(new Bullet((player.xPos + (player.width / 2)), (player.yPos + (player.height / 2)), (float)xSpd, (float)ySpd));
                        }
                    }
                }
                else
                {
                    load--;
                }
                if (twoPlayer)
                {
                    if (load2 == 0)
                    {
                        if (GamePad.GetState(PlayerIndex.One).Triggers.Right > .5f && count % weapons[w2].fireRate == 0)
                        {
                            load2 = weapons2[w2].reload();
                            bullets.Add(new Bullet((player.xPos + (player.width / 2)), (player.yPos + (player.height / 2)), GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X, -GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y));
                        }
                    }
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
                    {
                        if (count % 60 == 0)
                            weapons[w1].upgrade();
                    }
                    if (GamePad.GetState(PlayerIndex.One).Buttons.RightShoulder == ButtonState.Pressed)
                    {
                        if (count % 15 == 0)
                        {
                            if (w1 < weapons.Count - 1)
                                w1++;
                        }
                    }
                    if (GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Pressed)
                    {
                        if (count % 15 == 0)
                        {
                            if (w1 > 0)
                                w1--;
                        }
                    }
                }
            }
            else
            {
                if (load == 0)
                {
                    if (GamePad.GetState(PlayerIndex.One).Triggers.Right > .5f && count % weapons[w1].fireRate == 0)
                    {
                        load = weapons[w1].reload();
                        bullets.Add(new Bullet((player.xPos + (player.width / 2)), (player.yPos + (player.height / 2)), GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X, -GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y));
                    }
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
                {
                    if (count % 60 == 0)
                        weapons[w1].upgrade();
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.RightShoulder == ButtonState.Pressed)
                {
                    if (count % 15 == 0)
                    {
                        if (w1 < weapons.Count - 1)
                        {
                            w1++;
                            load = 0;
                        }
                    }
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    if (count % 15 == 0)
                    {
                        if (w1 > 0)
                        {
                            w1--;
                            load = 0;
                        }
                    }
                }
                if (twoPlayer)
                {
                    if (load2 == 0)
                    {
                        if (GamePad.GetState(PlayerIndex.Two).Triggers.Right > .5f && count % weapons[w2].fireRate == 0)
                        {
                            load2 = weapons2[w2].reload();
                            bullets.Add(new Bullet((player2.xPos + (player2.width / 2)), (player2.yPos + (player2.height / 2)), GamePad.GetState(PlayerIndex.Two).ThumbSticks.Right.X, -GamePad.GetState(PlayerIndex.Two).ThumbSticks.Right.Y));
                        }
                    }
                    else
                    {
                        load2--;
                    }
                    if (GamePad.GetState(PlayerIndex.Two).Buttons.Start == ButtonState.Pressed)
                    {
                        if (count % 60 == 0)
                            weapons2[w2].upgrade();
                    }
                    if (GamePad.GetState(PlayerIndex.Two).Buttons.RightShoulder == ButtonState.Pressed)
                    {
                        if (count % 15 == 0)
                        {
                            if (w2 < weapons2.Count - 1)
                            {
                                w2++;
                                load2 = 0;
                            }
                        }
                    }
                    if (GamePad.GetState(PlayerIndex.Two).Buttons.LeftShoulder == ButtonState.Pressed)
                    {
                        if (count % 15 == 0)
                        {
                            if (w2 > 0)
                            {
                                w2--;
                                load2 = 0;
                            }
                        }
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
                            zombies[j].health -= weapons[w1].damage;
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
                    if (zombies[k].bounds.Intersects(player.bounds) && count % 10 == 0)
                        player.health -= zombies[k].damage;
                    else
                        zombies[k].update(player.xPos, player.yPos);
                }
                else
                {
                    if (zombies[k].target == 1)
                    {
                        if (zombies[k].bounds.Intersects(player.bounds) && count % 10 == 0)
                            player.health -= zombies[k].damage;
                        else
                            zombies[k].update(player.xPos, player.yPos);
                    }
                    else if (zombies[k].target == 2)
                    {
                        if (zombies[k].bounds.Intersects(player2.bounds) && count % 10 == 0)
                            player2.health -= zombies[k].damage;
                        else
                            zombies[k].update(player2.xPos, player2.yPos);
                    }
                }
                    zombies[k].bounds = new Rectangle((int)zombies[k].xPos, (int)zombies[k].yPos, zombies[k].width, zombies[k].height);
            }
            if (ups == 100)
                rate -= 3;
            if (ups == 150)
                rate -= 3;
            if (ups == 200)
                rate -= 3;
            if (ups == 250)
                rate -= 3;
            if (twoPlayer)
            {
                if (player2.health < 1)
                {
                    twoPlayer = false;
                }
            }
            if (player.health < 1)
            {
                this.Exit();
            }
            if (load > 0)
            {
                load--;
            }
            if (load2 > 0)
            {
                load2--;
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
            spriteBatch.Draw(sampleTex, new Rectangle(0, GraphicsDevice.Viewport.Height-10, 1*player.health, 3), Color.Green);
            if (twoPlayer)
            {
                spriteBatch.Draw(spritesheet, player2.bounds, new Rectangle(0, 14, player2.width, player2.height), Color.White);
                spriteBatch.Draw(sampleTex, new Rectangle(0, GraphicsDevice.Viewport.Height - 20, 1* player2.health, 3), Color.Green);
            }
            for (int i = 0; i < zombies.Count; i++)
            {
                spriteBatch.Draw(sampleTex, zombies[i].bounds, Color.Green);
            }
            if (bullets.Count != 0)
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    spriteBatch.Draw(sampleTex, new Rectangle((int)bullets[i].xPos, (int)bullets[i].yPos, bullets[i].width, bullets[i].height), Color.Yellow);
                }
            }
            spriteBatch.DrawString(font, "Player1: " + weapons[w1].type, new Vector2(0, 0), Color.White);
            if (twoPlayer)
            {
                spriteBatch.DrawString(font, "Player2: " + weapons2[w2].type, new Vector2(0, 20), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
