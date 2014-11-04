using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace MysticalMagics.Projectiles
{
    class ProjFlier : ModProjectile
    {
        int time;
        public override void AI()
        {
            projectile.velocity.Y *= 0.95f;

            if (projectile.velocity.Y < 2 || projectile.velocity.Y > -2 || projectile.velocity.Y == 0)
            {
                projectile.velocity.Y = 0;

                time++;

                if (time > 60)
                {
                    int proj = Projectile.NewProjectile(projectile.Center, new Vector2(0, projectile.velocity.Y + 7f), ProjDef.byType[187].name, 60, 2, 0, 1.2f, Main.myPlayer);
                    time = 0;
                }
            }
            else
                projectile.velocity.Y = 0;

            projectile.velocity.X *= 0.999f;
        }
    }
}