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
    class ProjSpiral : ModProjectile
    {
        Vector2 fireAt;

        int time;
        float rotation;

        public override void AI()
        {
            time++;

            if (time > 8)
            {
                rotation += 0.5f;
                fireAt = new Vector2(projectile.Center.X + (float)(250 * Math.Cos(rotation)), projectile.Center.Y + (float)(250 * Math.Sin(rotation)));
                
                time = 0;
            }

            float rot = (float)Math.Atan2(projectile.Centre.Y - fireAt.Y, projectile.Centre.X - fireAt.X);

            int proj = Projectile.NewProjectile(projectile.Center, new Vector2((float)Math.Cos(rot) * -2f, (float)Math.Sin(rot) * -2f), "MysticalMagics:SpiralMinion", 26, 0, 0, 1.1f, Main.myPlayer);
            int d = Dust.NewDust(projectile.Center, 1, 1, 12, (float)Math.Cos(rot) * -3f, (float)Math.Sin(rot) * -3f);
        }
    }
}
