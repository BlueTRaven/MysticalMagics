using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace MysticalMagics.NPCs
{
    class VomitingZombie : ModNPC
    {
        int time = 0;
        public override void AI()
        {
            Player target = Main.player[npc.target];

            float rotTo = (float)Math.Atan2(npc.Center.Y - target.Center.Y, npc.Center.X - target.Center.X);

            if ((target.position - npc.position).Length() < 200)
            {
                npc.velocity.X = 0;

                time++;

                if(time > 120)
                {
                    int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 15, (float)Math.Cos(rotTo) * -3f, (float)Math.Sin(rotTo) * -3f, 109, 15, 1, 0, 1.2f, Main.myPlayer);
                    Main.projectile[proj].friendly = false;
                    Main.projectile[proj].hostile = true;
                    time = 0;
                }
            }
        }
    }
}
