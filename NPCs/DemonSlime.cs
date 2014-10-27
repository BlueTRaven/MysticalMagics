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
    class DemonSlime : ModNPC
    {
        public override void HitEffect(int hitDirection, double damage, bool isDead)
        {
            if (isDead)
            {
                NPC.NewNPC(npc.Center, NPCDef.byName["MysticalMagics:DemonSlime2"].type, 0);
            }
        }

        public override bool CanSpawn(int x, int y, int type, Player spawnedOn)
        {
            if (!(spawnedOn.zone["Hell"] && Main.hardMode && NPC.downedMechBossAny))
            {
                return false;
            }
            else return true;
        }
    }
}
