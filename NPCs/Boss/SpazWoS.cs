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
    class SpazWoS : ModNPC
    {
        public override void AI()
        {

            if (npc.life < npc.lifeMax / 3)
            {
                int nnpc = NPC.NewNPC(npc.Center, 125, 0);
                Main.npc[nnpc].defense = 60;
                Main.npc[nnpc].life = npc.life - 1;
                npc.active = false;
            }
        }
    }
}
