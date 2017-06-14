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
        public int fireRate;
        public int shots;
        public int maxShots;
        public int reloadTime;
        public int damage;
        public int upgrades;
        public Weapon(string t, int fR, int s, int rT, int dmg)
        {
            type = t;
            fireRate = fR;
            maxShots = s;
            reloadTime = rT;
            damage = dmg;
            shots = maxShots;
        }

        public int reload()
        {
            if (shots == 0)
            {
                shots = maxShots;
                return reloadTime;
            }
            shots--;
            return 0;
        }
        public void upgrade()
        {
            if (upgrades < 3)
            {
                fireRate = (int)(fireRate * .8);
                maxShots += 5;
                reloadTime -= (int)(reloadTime * .1);
                upgrades++;
            }
        }
    }
}
