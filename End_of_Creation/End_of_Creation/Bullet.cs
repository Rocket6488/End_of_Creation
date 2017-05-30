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
    class Bullet
    {
        public int width;
        public int height;
        public float xPos;
        public float yPos;
        public float vX;
        public float vY;
        public Bullet(int x, int y, float _vX, float _vY)
        {
            width = 4;
            height = 4;
            xPos = x;
            yPos = y;
            vX = _vX;
            vY = _vY;
        }
        public void update()
        {
            xPos += vX*14;
            yPos += vY*14;
        }
    }
}