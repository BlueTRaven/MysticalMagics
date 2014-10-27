using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace MysticalMagics.NPCs
{
    class FallenAngel : ModNPC
    {
        int randomnum = 0;
        int ai = 0;
        int ai2 = 0;
        bool hpChangeFirst = false;
        int proj;

        public override void AI()
        {
            Player tar = Main.player[npc.target];

            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
                npc.TargetClosest(true);

            ai++;

            float rot = (float)Math.Atan2(npc.Centre.Y - tar.Centre.Y, npc.Centre.X - tar.Centre.X);

            if(ai > 120 && !(npc.life < npc.lifeMax / 3))
            {
                proj = Projectile.NewProjectile(npc.Center, new Vector2(0,0), "MysticalMagics:HomingFireBall", 1, 1.2f, Main.myPlayer);
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                Main.projectile[proj].friendly = false;
                Main.projectile[proj].hostile = true;
                ai = 0;
            }

            Vector2 missle = tar.position - Main.projectile[proj].position;
            missle.Normalize();

            Main.projectile[proj].velocity = 2 * missle;

            if (npc.life < npc.lifeMax / 3)
            {
                ai2++;
                npc.knockBackResist = 0.2f;
                npc.damage = 90;
                npc.defense = 15;

                npc.position += npc.velocity * 1.2f;

                if (!hpChangeFirst)
                {
                    Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                    randomnum = Main.rand.Next(0, 100);

                    hpChangeFirst = true;
                }

                Dust.NewDust(npc.Hitbox, 6, npc.velocity * -1, 0, new Color(), 1f);
                Lighting.AddLight(npc.position, new Color(255, 0, 0));

                if (ai2 > 150 + randomnum)
                {
                    npc.aiStyle = -1;

                    npc.velocity.X = (float)Math.Cos(rot) * -15f;
                    npc.velocity.Y = (float)Math.Sin(rot) * -15f;
                    Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 5   );

                    if (ai2 > 250)
                    {
                        npc.aiStyle = 14;
                        ai2 = 0;
                    }
                }
            }
        }

        public override void SelectFrame(int frameSize)
        {
            npc.frameCounter++;

            if (npc.life < npc.lifeMax / 3)
            {
                if (npc.frameCounter >= 6)
                {
                    npc.frame.Y += frameSize;
                    npc.frameCounter = 0;
                }
                if (npc.frame.Y >= frameSize * 8)
                    npc.frame.Y = 344;
            }
            else
            {
                if (npc.frameCounter >= 6)
                {
                    npc.frame.Y += frameSize;
                    npc.frameCounter = 0;
                }
                if (npc.frame.Y >= frameSize * 4)
                    npc.frame.Y = 0;
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
