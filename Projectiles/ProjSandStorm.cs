using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace MysticalMagics.Projectiles
{
    class ProjSandStorm : ModProjectile
    {
        bool flag;
        public override void AI()
        {
            Helper.SlowDown(projectile, 0.96f);

            if (projectile.velocity.X < 2f && projectile.velocity.X > -2f && projectile.velocity.Y < 2f && projectile.velocity.Y > -2f)
            {
                if (!flag)
                {
                    flag = true;
                    projectile.timeLeft = 60;
                }
            }
            else
                projectile.timeLeft = 2;

            if (projectile.timeLeft > 1)
                Dust.NewDust(projectile.Hitbox, 11, new Vector2(Main.rand.Next(-4,4), Main.rand.Next(-4,4)), 0, Color.LightGoldenrodYellow, Main.rand.NextFloat()); //duuuuuuust
            else
            {
                for (int i = 0; i < 60; i++)
                    Dust.NewDust(projectile.Hitbox, 11, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), 0, Color.LightGoldenrodYellow, Main.rand.NextFloat());
            }

            Lighting.AddLight(projectile.position, Color.Yellow);
        }
    }
}
