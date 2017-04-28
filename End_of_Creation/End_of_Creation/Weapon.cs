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
    class Weapon
    {
        public string type;
        public double fireRate;
        public Texture2D text;
        public Rectangle rect;
        public int shots;
        public double reloadTime;
        public Weapon(string t, double fR, Texture2D te, Rectangle r, int s, double rT)
        {
            type = t;
            fireRate = fR;
            text = te;
            rect = r;
            shots = s;
            reloadTime = rT;
        }

        public void reload()
        {
            if (shots == 0)
            {
                //figure this out later...
            }
        }
        public void upgrade()
        {
            fireRate *= .2;
            shots += 5;
            reloadTime -= (reloadTime * .1);
        }
    }
}
