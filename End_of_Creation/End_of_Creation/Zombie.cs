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
    class Zombie
    {
        public int health;
        public int speed;
        public Texture2D text;
        public int damage;
        Random rand = new Random();
        public int xPos;
        public int yPos;
        public int width;
        public int height;
        public int target;
        public Rectangle bounds;

        public Zombie(int swidth)
        {
            health = 100 + (int)(rand.Next(0, 50));
            speed = (int)(rand.Next(1, 3));
            damage = (int)(rand.Next(5, 10));
            xPos = (int)(rand.NextDouble() * swidth);
            yPos = (int)(rand.Next(20, 30));
            setBody();
            setHead();
            bounds = new Rectangle(xPos, yPos, width, height);
            target = rand.Next(1, 3);
        }

        public void setBody()
        {
            int col = rand.Next(0, 8);
            if(col<4)
            {
                height = 10;
                width = 16;
            }
            else
            {
                height = 11;
                width = 16;
            }
        }

        public void update(int pXPos, int pYPos)
        {
            if (xPos > pXPos)
                xPos -= speed;
            if (xPos < pXPos)
                xPos += speed;
            if (yPos > pYPos)
                yPos -= speed;
            if (yPos < pYPos)
                yPos += speed;
        }

        public void setHead()
        {

        }
    }
}
