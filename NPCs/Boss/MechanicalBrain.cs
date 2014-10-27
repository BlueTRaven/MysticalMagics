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
    class MechanicalBrain : ModNPC
    {
        int minionA;
        int minionB;
        float rotposX;
        float rotposY;

        public override void AI()
        {
            npc.TargetClosest(true);
            Player tar = Main.player[npc.target];

            npc.ai[0]++;
            
            if (npc.ai[0] == 1)
            {
                minionA = NPC.NewNPC(new Vector2(npc.Center.X - 200, npc.Center.Y), NPCDef.byName["MysticalMagics:MechBrainMinion"].type, 0);
                minionB = NPC.NewNPC(new Vector2(npc.Center.X + 200, npc.Center.Y), NPCDef.byName["MysticalMagics:MechBrainMinion"].type, 0);   
            }

            npc.position.X = tar.Center.X;
            npc.position.Y = tar.Center.Y - 350;

            npc.ai[2] += 0.2f;
            
            Vector2 position = new Vector2(npc.Center.X + (float)(400 * Math.Cos(npc.ai[2])), npc.Center.Y + (float)(400 * Math.Sin(npc.ai[2])));
    
            Vector2 missle = position - Main.npc[minionA].Center;
            missle.Normalize();

            Main.npc[minionA].velocity = 14 * missle;
            Main.npc[minionB].velocity = 14 * missle;


            if (npc.ai[0] > 600)
            {
                npc.ai[0] = 2;
            }

            if (Main.npc[minionA].life > 0 || Main.npc[minionB].life > 0)
            {
                npc.dontTakeDamage = true;
            }
            else npc.dontTakeDamage = false;
            

            if (!(npc.life < npc.lifeMax / 2))
            {
            }
        }
    }
}
