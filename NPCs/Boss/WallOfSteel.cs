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
    class WallOfSteel : ModNPC
    {
        int spaz;
        int ret;

        Vector2 spazPos;
        Vector2 retPos;

        public override void AI()
        {
            npc.TargetClosest(true);
            Player tar = Main.player[npc.target];

            npc.ai[0]++;    //60 = 1sec

            if (npc.ai[0] == 1)
            {   //should only be called once - resets to 2 rather than 1
                spaz = NPC.NewNPC(new Vector2(npc.Center.X, npc.Center.Y + 250), NPCDef.byName["MysticalMagics:SpazWoS"].type, 0);
                ret = NPC.NewNPC(new Vector2(npc.Center.X, npc.Center.Y - 250), NPCDef.byName["MysticalMagics:RetWoS"].type, 0);
            }

            spazPos = new Vector2(npc.position.X, npc.position.Y + 200);
            retPos = new Vector2(npc.position.X, npc.position.Y - 400);

            Main.npc[spaz].position = spazPos;
            Main.npc[ret].position = retPos;

            Main.npc[spaz].rotation = (float)Math.Atan2(npc.Centre.Y - tar.Centre.Y, npc.Centre.X - tar.Centre.X) - 90;
            Main.npc[ret].rotation = (float)Math.Atan2(npc.Centre.Y - tar.Centre.Y, npc.Centre.X - tar.Centre.X) + 90;

            if (npc.ai[0] > 600)
            {   //if the ai counter is greater than 600
                if (Main.npc[spaz].active || Main.npc[ret].active)
                {
                    npc.dontTakeDamage = true;
                }
                else npc.dontTakeDamage = false;
                npc.ai[0] = 2;  //reset the counter
            }

            npc.position.Y = tar.Center.Y;
            
            npc.velocity.X += 0.1f;

            if (npc.velocity.X > 5f)
            {
                npc.velocity.X = 5f;
            }

            /*if (tar.position.Y < Main.cloudLimit)
            {   //todo: Implement killing below certian level (Force players to stay in cloud layer)
                tar.dead = true;
            }*/
        }

        public override bool PreDraw(SpriteBatch batch)
        {
            Texture2D tex = modBase.textures["Textures/NPCs/WallOfSteelWall"];

            batch.Draw(tex, new Vector2(npc.position.X + 100, npc.position.Y) - Main.screenPosition, null, Color.White, (float)Math.PI * 15f, Vector2.Zero, 2.5f, SpriteEffects.None, 0f);

            return true;
        }

        public override void SelectFrame(int frameSize)
        {
            if (npc.dontTakeDamage)
            {
                npc.frame.Y = 110;
            }
            else npc.frame.Y = 0;
        }
    }
}
