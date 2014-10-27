using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace MysticalMagics.Projectiles
{
    public class ProjTide : ModProjectile
    {
        public override void AI()
        {
            projectile.rotation = ((float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f);
            projectile.tileCollide = false;

            projectile.ai[0]++;
            if (projectile.ai[0] > 2)
            {
                int dust = Dust.NewDust(projectile.Hitbox, 16, projectile.velocity, 0, new Color(102, 102, 198), 0.7f);
                Main.dust[dust].fadeIn = 0.1f;

                projectile.ai[0] = 0;
            }
        }
    }
}