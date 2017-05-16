﻿using System;
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
    class Player
    {
        public int xPos;
        public int yPos;
        public int health;
        public int speed;
        public int width;
        public int height;
        public Rectangle bounds;
        public Player()
        {
            health = 100;
            speed = 6;
            width = 16;
            height = 13;
            bounds = new Rectangle(xPos, yPos, width, height);
        }
    }
}
