using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace MysticalMagics.Projectiles
{
    class SandColumn : ModProjectile
    {
        public override void AI()
        {
            projectile.Hitbox = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height + 200);
            //for (int i = 0; i < 60; i++)
                Dust.NewDust(new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.Hitbox.Width, projectile.Hitbox.Height), 19, projectile.velocity, 0, Color.White, Main.rand.NextFloat());

            for (int i = 0; i < 200; i++)
            {
                Lighting.AddLight(new Vector2(projectile.position.X, projectile.position.Y + i), Color.Yellow);
            }

            projectile.velocity *= 0.2f;
        }
    }
}
