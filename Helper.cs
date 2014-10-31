using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace MysticalMagics
{
    class Helper
    {
        /*
         *  Gets the rotation to its target.
         *  @Param pos: 4 possible ints
         */
        public static float GetRotation(Player target, NPC npc, int pos)
        {
            if (pos == 0)
                return (target.Center - npc.Center).ToRotation() + -MathHelper.PiOver2;
            else if (pos == 1)
                return (target.Center - npc.Center).ToRotation() + MathHelper.PiOver2;
            else if (pos == 2)
                return (target.Center - npc.Center).ToRotation() + ((float)-Math.PI);
            else if (pos == 3)
                return (target.Center - npc.Center).ToRotation() + ((float)Math.PI);
            
            else return (target.Center - npc.Center).ToRotation() + -MathHelper.PiOver2;
        }

        public static double GetCosSin(float rot, bool cosSin)
        {
            if (cosSin)
                return Math.Cos(rot);
            else return Math.Sin(rot);
        }

        /*
         *  Charges the Target.
         *  @Param target: the player the npc is targeting.
         *  @Param npc: the npc.
         *  @Param speed: the speed at which to charge. 
         */
        public static void ChargeTarget(Player target, NPC npc, float speed)
        {
            float rot = GetRotation(target, npc, 0);

            npc.velocity.X = (float)Math.Cos(rot) * -speed;
            npc.velocity.Y = (float)Math.Sin(rot) * -speed;
        }

        /*  Checks if an npc is in a certain range of the player.
         *  @Param target: The target of the npc
         *  @Param npc: the npc itsself.
         *  @Param range: the distance you want to check.
         *  @Param inout: whether or not to check if the target is inside the range our outside the range.
         */
        public static bool IsInRange(Player target, NPC npc, int range, bool insideRange)
        {
            if (insideRange)
            {
                if ((npc.Center - target.Center).Length() < range)
                    return true;
            }
            if (!insideRange)
            {
                if ((npc.Center - target.Center).Length() > range)
                    return true;
            }

            return false;
        }

        /*
         *  Slows down the npc.
         */
        public static void SlowDown(NPC npc)
        {
            npc.velocity.X *= 0.98f;
            npc.velocity.Y *= 0.98f;
        }

        /*
         *  Kills the npc if it's active and its life is greater than 0.
         */
        public static void SetDead(NPC npc)
        {
            if (npc.active && npc.life > 0)
            {
                npc.active = false;
                npc.checkDead();
            }
        }
    }
}
