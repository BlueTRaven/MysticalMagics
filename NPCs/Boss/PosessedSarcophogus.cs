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
            npc.ai[2]++;
            if (npc.ai[2] >= 60) npc.ai[2] = 0;

            if (npc.ai[0] < 600)
            {   //run phase one
                PhaseOne(target);
            }
            else if (npc.ai[0] >= 600 && npc.ai[0] < 900)
                PhaseOne(target);   //run phase two
            else
                npc.ai[0] = 0;  //reset the counter
            //runs phase one for 10 seconds, then phase two for 5.

            Lighting.AddLight(npc.Center, Color.LightYellow);   //make it give off light
        }

        public void PhaseOne(Player target)
        {
            if ((npc.Center - new Vector2(target.Center.X, target.Center.Y)).Length() > 130)
            {
                if (npc.ai[2] == 59)
                {
                    if (speed < 12)
                        speed++;
                    else speed = 12;

                    Main.NewText("" + speed);
                }
            }
            else if ((npc.Center - new Vector2(target.Center.X, target.Center.Y)).Length() < 130) speed = 0;
                
            Vector2 missle = target.Center - new Vector2(npc.Center.X, npc.Center.Y + 200);
            missle.Normalize();

            if (missle == Vector2.Zero) missle = -Vector2.UnitY;    //some complicated code to make it home in on the player
            npc.velocity = speed * missle;
        }

        public void PhaseTwo(Player target)
        {
            npc.ai[1]++;    //increase another counter

            if(npc.ai[1] >= 200 && npc.ai[1] < 260)
            {   //increase the alpha - make it phade out
                npc.alpha++;
            }
            if (npc.ai[1] >= 260 && npc.ai[1] < 265)
            {
                npc.alpha = 0;  //reset the alpha
                npc.position = new Vector2(target.Center.X, target.Center.Y + 200); //Teleport to a posisition above the player
            }
            else npc.ai[1] = 0; //reset the counter
        }
    }
}
