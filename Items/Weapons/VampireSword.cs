using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace MysticalMagics.Items
{
    class VampireSword : ModItem
    {
        public override void DealtNPC(Player owner, NPC npc, int hitDir, int dmgDealt, float knockback, bool crit)
        {
            if (crit)
            {
                owner.HealEffect(Main.rand.Next(2, 3), false);
            }
            else
            {
                owner.HealEffect(Main.rand.Next(1, 2), false);
            }
            Lighting.AddLight(npc.Center, Color.DarkRed);
        }
    }
}
