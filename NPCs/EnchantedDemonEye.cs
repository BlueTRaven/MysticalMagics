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
{   //A miniboss. TODO: Drop custom soul
    class EnchantedDemonEye : ModNPC
    {
        bool isHalfHP = false;
        bool stopFacingPlayer = false;
        bool canCharge = false;
        MNpc modnpc;

        public override void AI()
        {
            Lighting.AddLight(npc.Center, new Color(73, 220, 255)); //Light up the area it's around

            npc.TargetClosest(true);
            Player target = Main.player[npc.target]; //shorter, easier way to get the player the npc is targeting

            npc.ai[0]++;

            if (!stopFacingPlayer)
                npc.rotation = Helper.GetRotation(target, npc, 0);  //look at the player at all times
            if (!target.dead && !Main.dayTime)
            {
                if (npc.life < npc.lifeMax / 2) //if at half hp
                {
                    npc.damage = 75;    //increase its damage
                    npc.ai[1]++;

                    if (npc.ai[1] <= 90)
                    {   //the part where the eye becomes the mouth thing
                        npc.rotation += 0.5f;

                        stopFacingPlayer = true;

                        npc.velocity.X *= 0.98f;
                        npc.velocity.Y *= 0.98f;

                        npc.defense = 60;

                        if (npc.ai[1] == 90)
                        {
                            npc.defense = 11;

                            Gore.NewGore(npc.position, npc.velocity, 8, 0.5f);
                            Gore.NewGore(npc.position, npc.velocity, 9, 0.5f);

                            Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 1);
                            Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);

                            isHalfHP = true;
                        }
                    }

                    if (npc.ai[1] > 120)
                    {
                        stopFacingPlayer = false;

                        if (npc.ai[1] > 100)
                            npc.ai[1] = 121;

                        npc.velocity.X *= 0.98f;
                        npc.velocity.Y *= 0.98f;

                        if (npc.ai[0] > 40)
                        {
                            if (canCharge)
                            {
                                float rot = (float)Math.Atan2(npc.Centre.Y - target.Centre.Y, npc.Centre.X - target.Centre.X);

                                Helper.ChargeTarget(target, npc, 20f);
                            }
                            
                            npc.ai[0] = 0;
                        }
                    }
                }
                else
                {
                    npc.velocity.X *= 0.98f;
                    npc.velocity.Y *= 0.98f;

                    if (npc.ai[0] > 65)
                    {
                        if (canCharge)
                        {
                            float rot = (float)Math.Atan2(npc.Centre.Y - target.Centre.Y, npc.Centre.X - target.Centre.X);

                            npc.velocity.X = (float)Math.Cos(rot) * -22f;
                            npc.velocity.Y = (float)Math.Sin(rot) * -22f;
                        }
                        
                        npc.ai[0] = 0;
                    }
                }
            }
            else if (target.dead || Main.dayTime)
            {
                npc.velocity.X += 0.12f;

                if (npc.timeLeft > 10)
                    npc.timeLeft = 10;
            }

            if ((npc.position - target.position).Length() < 450 && !isHalfHP)
                canCharge = true;
            else
            {
                if (!stopFacingPlayer)
                {
                    if (npc.velocity.X < 3f && npc.velocity.X > -3f && npc.velocity.Y < 3f && npc.velocity.Y > -3f)
                    {
                        Vector2 missle = target.position - new Vector2(npc.position.X, npc.position.Y + 100);
                        missle.Normalize();

                        if (missle == Vector2.Zero) missle = -Vector2.UnitY;

                        npc.velocity = 12 * missle;
                    }
                }
                
                canCharge = false;
            }
        }

        public override void PostNPCLoot()
        {
            Gore.NewGore(npc.position, npc.velocity, 9, 0.5f);
            Gore.NewGore(npc.position, npc.velocity, 10, 0.5f);
        }

        public override bool CanSpawn(int x, int y, int type, Player spawnedOn)
        {
            if (Main.hardMode && (spawnedOn.zoneEvil || spawnedOn.zoneBlood) && !Main.dayTime && spawnedOn.zone["Surface"] && MWorld.SarcophogusBeaten)
            {   //only spawn in hardmode, in the crimson/corruption, when it's nighttime and the player is on the surface.
                return Main.rand.Next(20) == 1;
            }
            else return false;
        }

        public override void SelectFrame(int frameSize)
        {   //complicated stuff for moving frames n stuff
            npc.frameCounter++;

            if (isHalfHP)
            {
                if (npc.frameCounter >= 3)
                {
                    npc.frame.Y += frameSize;
                    npc.frameCounter = 0;
                }
                if (npc.frame.Y >= frameSize * 6)
                    npc.frame.Y = 498;
            }
            else
            {
                if (npc.frameCounter >= 3)
                {
                    npc.frame.Y += frameSize;
                    npc.frameCounter = 0;
                }
                if (npc.frame.Y >= frameSize * 3)
                    npc.frame.Y = 0;
            }
        }

        public override void NPCLoot()
        {   //drop teh l00tz
            Item.NewItem(npc.Center, npc.Size, ItemDef.byName["MysticalMagics:SoulOfObservance"].type, Main.rand.Next(12), false, 0, false);
        }
    }
}
