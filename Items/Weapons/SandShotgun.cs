using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace MysticalMagics.Items
{
    class SandShotgun : ModItem
    {
        public override bool PreShoot(Player player, Vector2 position, Vector2 velocity, int projType, int damage, float knockback)
        {
            for (int i = 0; i < 5; i++)
            {
                float velX = velocity.X + (float)Main.rand.Next(-15, 15 + 1) * 0.08f;
                float velY = velocity.Y + (float)Main.rand.Next(-15, 15 + 1) * 0.08f;

                int proj = Projectile.NewProjectile(position.X, position.Y, velX, velY, projType, damage, knockback, Main.myPlayer);
                Main.projectile[proj].friendly = true;  //So it doesn't hurt the player
                Main.projectile[proj].hostile = false;
            }
            return false;
        }
    }
}
