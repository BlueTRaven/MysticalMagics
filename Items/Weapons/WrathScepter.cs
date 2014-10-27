using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace MysticalMagics.Items
{
    class WrathScepter : ModItem
    {
        int proj;
        int ai;

        public override bool PreShoot(Player player, Vector2 position, Vector2 velocity, int projType, int damage, float knockback)
        {
            proj = Projectile.NewProjectile(player.Center, new Vector2(velocity.X, velocity.Y),
                                "MysticalMagics:HomingFireBall", (int)(50f * player.magicDamage), 1.1f, player.whoAmI);
            Main.projectile[proj].timeLeft = 10000;
            Main.projectile[proj].friendly = true;


            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active && !Main.npc[i].friendly && !(Main.npc[i].life < 0))
                {
                    Main.projectile[proj].ai[0] = i;
                }
            }
            return true;
        }
    }
}
