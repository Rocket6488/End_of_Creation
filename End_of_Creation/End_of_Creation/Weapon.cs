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
        public int shots;
        public double reloadTime;
        public int damage;
        public Weapon(string t, double fR, int s, double rT, int dmg)
        {
            type = t;
            fireRate = fR;
            shots = s;
            reloadTime = rT;
            damage = dmg;
        }

        public double reload()
        {
            if (shots == 0)
            {
                return reloadTime;
            }
            return 0.0;
        }
        public void upgrade()
        {
            fireRate *= .2;
            shots += 5;
            reloadTime -= (reloadTime * .1);
        }
    }
}
