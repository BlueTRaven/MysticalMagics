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
    class MegaBlaster : ModItem
    {
        public override bool PreShoot(Player player, Vector2 position, Vector2 velocity, int projType, int damage, float knockback)
        {
            for (int i = 0; i < 5; i++ )
            {
                float velX = velocity.X + (float)Main.rand.Next(-15, 15 + 1) * 0.08f;
                float velY = velocity.Y + (float)Main.rand.Next(-15, 15 + 1) * 0.08f;

                Projectile.NewProjectile(position.X, position.Y, velX, velY, 207, damage - 10, knockback, Main.myPlayer);
            }
            return false;
        }
    }
}
