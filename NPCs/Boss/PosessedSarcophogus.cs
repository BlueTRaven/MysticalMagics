﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;
using Terraria.DataStructures;

namespace MysticalMagics.NPCs
{
    public class PosessedSarcophogus : ModNPC
    {
        int speed;
        int maxSpeed = 2;
        int lazerFrequency = 1;
        bool isDeadOnce;

        int flagCount = 0, flagCount2 = 0;
        bool flag;
        Vector2 origPos;

        public override void HitEffect(int hitDirection, double damage, bool isDead)
        {
            if (isDead && !isDeadOnce)
            {
                npc.life = npc.lifeMax;
                npc.defense /= 2;
                npc.active = true;
                isDeadOnce = true;
                origPos = npc.position;

                    //freaking love dust
                for (int i = 0; i < 60; i++) Dust.NewDust(npc.Hitbox, 6, new Vector2(Main.rand.Next(-16, 16), Main.rand.Next(-16, 16)), 0, Color.White, Main.rand.Next(2));
            }
        }

        public override void AI()
        {
            npc.TargetClosest(true);
            Player target = Main.player[npc.target]; //shorter, easier way to get the player the npc is targeting

            if (!flag && isDeadOnce)
            {
                npc.dontTakeDamage = true;

                flagCount++;

                if (flagCount < 240)
                {
                    flagCount2++;
                    if (flagCount2 > 30)
                    {
                        npc.position = new Vector2(origPos.X + Main.rand.Next(-200, 200), origPos.Y + Main.rand.Next(-200, 200));
                        for (int i = 0; i < 20; i++)
                            Dust.NewDust(npc.Hitbox, 6, new Vector2(Main.rand.Next(-16, 16), Main.rand.Next(-16, 16)), 0, Color.White, Main.rand.Next(2));  //needs more dust
                        flagCount2 = 0;
                    }
                }
                if (flagCount >= 240 && flagCount < 300)
                    npc.position = origPos;
                else if (flagCount >= 300)
                {
                    flag = true;
                    flagCount = 0;
                }
            }

            if (flag)
            {
                npc.dontTakeDamage = false;

                int dust = Dust.NewDust(npc.Hitbox, 6, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), 0, Color.White, Main.rand.Next(2));   //aogagifghufghaoh
                Lighting.AddLight(Main.dust[dust].position, Color.SteelBlue);
                maxSpeed = 8;

                Lighting.AddLight(npc.Center, Color.DarkOrange);

                npc.ai[0]++;
                if (npc.ai[0] < 600)    //Ai loop
                    PhaseTwo(target);
                else if (npc.ai[0] >= 600 && npc.ai[0] < 900)
                    PhaseOne(target);
                else
                    npc.ai[0] = 0;

                lazerFrequency = 45;
            }
            else
            {
                PhaseOne(target);
                int dust = Dust.NewDust(npc.Hitbox, 76, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), 0, Color.LightGoldenrodYellow, Main.rand.Next(2));   //mmmmmmmmmmmmm...
                Lighting.AddLight(Main.dust[dust].position, Color.LightGoldenrodYellow);

                Lighting.AddLight(npc.Center, Color.Orange);

            }

            if (npc.velocity.X > 0)
            {
                npc.rotation += 0.09f;
                if (npc.rotation > 1.4)
                    npc.rotation = 1.4f;
            }
            if (npc.velocity.X < 0)
            {
                npc.rotation -= 0.09f;
                if (npc.rotation > -1.4)
                    npc.rotation = -1.4f;
            }
        }

        public void PhaseOne(Player target)
        {
            bool directionX = (npc.Center.X - target.Center.X) > 0;
            bool directionY = (npc.Center.Y - target.Center.Y + 200) > 0;

            if ((npc.Center - new Vector2 (target.Center.X, target.Center.Y + 200)).Length() > 50)
            {
                if (directionX)
                {
                    npc.velocity.X -= 0.1f;

                    if (npc.velocity.X < -maxSpeed)
                        npc.velocity.X = -maxSpeed;
                }

                if (!directionX)
                {
                    npc.velocity.X += 0.1f;

                    if (npc.velocity.X > maxSpeed)
                        npc.velocity.X = maxSpeed;
                }

                if (directionY)
                {
                    npc.velocity.Y -= 0.3f;

                    if (npc.velocity.Y < -maxSpeed)
                        npc.velocity.Y = -maxSpeed;
                }

                if (!directionY)
                {
                    npc.velocity.Y += 0.3f;

                    if (npc.velocity.Y > maxSpeed)
                        npc.velocity.Y = maxSpeed;
                }
            }
            else
                Helper.SlowDown(npc, 0.09f);

            npc.ai[2]++;

            float rot = Helper.GetRotation(target, npc, 2);

            if (npc.ai[2] > 120 - lazerFrequency * 2)
            {
                int proj = Projectile.NewProjectile(npc.Center, new Vector2((float) Helper.GetCosSin(rot, true) * -20f, (float) Helper.GetCosSin(rot, false) * -20f/*(float)Math.Cos(rot) * -20f, (float)Math.Sin(rot) * -20f*/), "Eye Laser", 60, 1.2f, Main.myPlayer);
                Main.projectile[proj].hostile = true;
                Main.projectile[proj].friendly = false;
                
                npc.ai[2] = 0;
            }

            if (Main.rand.Next(100) == 7)
            {
                int proj = Projectile.NewProjectile(npc.Center, new Vector2(0, 2), "Sand Ball", 12, 1.2f, Main.myPlayer);
                Main.projectile[proj].hostile = true;
                Main.projectile[proj].friendly = false;
            }
            npc.ai[1] = 0;
            npc.alpha = 0;
        }

        public void PhaseTwo(Player target)
        {
            float rot = (float)Math.Atan2(npc.Centre.Y - target.Centre.Y, npc.Centre.X - target.Centre.X);

            Helper.SlowDown(npc, 0.98f);

            npc.ai[1]++;

            if (npc.ai[1] <= 120)
                npc.alpha += 2;

            if (npc.ai[1] > 120)
            {
                npc.position = new Vector2(target.Center.X + Main.rand.Next(-200, 200), target.Center.Y + Main.rand.Next(-200, 200));

                for (int i = 0; i < 4; i++)
                {
                    int proj = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, npc.velocity.X + Main.rand.Next(-4, 4), npc.velocity.Y + Main.rand.Next(-4, 4), 293, 40, 0, 0, 1.1f, Main.myPlayer);
                    Main.projectile[proj].timeLeft = 600;
                }

                Main.PlaySound(2, (int) npc.Center.X, (int) npc.position.Y, 8);
                
                Helper.ChargeTarget(target, npc, 2f);

                if (lazerFrequency > 15)
                    lazerFrequency = 15;

                npc.alpha = 0;

                npc.ai[1] = 0;
            }

            if (npc.ai[2] > 120 - lazerFrequency)
            {
                int proj = Projectile.NewProjectile(npc.Center, new Vector2((float)Math.Cos(rot) * -20f, (float)Math.Sin(rot) * -20f), "Eye Laser", 60, 1.2f, Main.myPlayer);
                Main.projectile[proj].hostile = true;
                Main.projectile[proj].friendly = false;
                Main.projectile[proj].tileCollide = false;

                npc.ai[2] = 0;
            }
        }

        public override void PostNPCLoot()
        {
            switch (Main.rand.Next(0, 3))
            {
                case 0: Item.NewItem(npc.Hitbox, ItemDef.byName["MysticalMagics:Sandstorm"].type, 1, false, -1, false); 
                    break;
                case 1: Item.NewItem(npc.Hitbox, ItemDef.byName["MysticalMagics:SandShotgun"].type, 1, false, -1, false); 
                    break;  //TODO: add custom drop set: Sand themed
                case 2: break;
                case 3: break;
            }

            Item.NewItem(npc.Hitbox, ItemDef.byName["MysticalMagics:EnchantedSandstone"].type, Main.rand.Next(15, 40), false, 0, false);    //used to make armor set?
        }
    }
}
