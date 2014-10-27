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
    public class BookOfTheTide : ModItem
    {
        public override bool? UseItem(Player player)
        {
            Main.NewText("pickles");
            return true;
        }
        //nothing in here gets called
        public override bool OnPickup(Player player)
        {
            Main.NewText("This");  
            return true;
        }

        public override bool PreShoot(Player player, Vector2 position, Vector2 velocity, int projType, int damage, float knockback)
        {
            for (double d = -0.2d; d < 0.3d; d += 0.05d)
            {
                int proj = Projectile.NewProjectile(player.Center, new Vector2(velocity.X * (float)Math.Cos(Math.PI * d) - velocity.Y * (float)Math.Sin(Math.PI * d), velocity.X * (float)Math.Sin(Math.PI * d) + velocity.Y * (float)Math.Cos(Math.PI * d)),
                    "MysticalMagics:ProjTide", (int)(50f * player.magicDamage), 1.1f, player.whoAmI);
                Main.projectile[proj].timeLeft = 120;
            }

            return true;
        }
    }
}
