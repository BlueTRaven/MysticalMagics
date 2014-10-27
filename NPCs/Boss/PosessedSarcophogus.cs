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
    public class PosessedSarcophogus : ModNPC
    {
        int speed;

        public override void AI()
        {
            npc.TargetClosest(true);
            Player target = Main.player[npc.target]; //shorter, easier way to get the player the npc is targeting
            npc.ai[0]++;

            if (npc.ai[0] < 600)
            {
                PhaseOne(target);
            }
            else if (npc.ai[0] >= 600 && npc.ai[0] < 900)
                PhaseTwo(target);
            else
                npc.ai[0] = 0;

            Lighting.AddLight(npc.Center, Color.LightYellow);
        }

        public void PhaseOne(Player target)
        {
            if ((npc.Center - target.position).Length() > 200)
            {
                Vector2 missle = target.position - new Vector2(npc.position.X, npc.position.Y + 200);
                missle.Normalize();

                if (speed < 12)
                    speed++;
                if (missle == Vector2.Zero) missle = -Vector2.UnitY;
                npc.velocity = speed * missle;
            }
        }

        public void PhaseTwo(Player Target)
        {
            speed = 0;
            npc.ai[1]++;

            if(npc.ai[1] >= 200 && npc.ai[1] < 260)
            {
                npc.alpha++;
            }
            if (npc.ai[1] >= 260 && npc.ai[1] < 265)
            {
                npc.position = new Vector2(Target.position.X, Target.position.Y + 200);
            }
            else npc.ai[1] = 0;
        }
    }
}
