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
    class HomingFireBall : ModProjectile
    {   //this projectile does not innately home into enemies
        public override void AI()
        {
            projectile.rotation = ((float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f);

            projectile.ai[1]++;

            if (projectile.ai[1] > 4)
                Dust.NewDust(projectile.Hitbox, 6, projectile.velocity * -1, 0, new Color(), 0.7f);

            if (projectile.friendly)
            {
                Vector2 posnpc = Main.npc[(int) projectile.ai[0]].position;

                Vector2 missle = posnpc - projectile.position;
                missle.Normalize();

                if (missle == Vector2.Zero) missle = -Vector2.UnitY;
                
                projectile.velocity = 2 * missle;
            }
        }

        public override void DamagePlayer(Player p, int hitDir, ref int damage, ref bool crit, ref float critMult)
        {
            if (crit)
                p.AddBuff(24, 120, true);
            else p.AddBuff(24, 60, true);

            projectile.timeLeft = 0;

            for (double d = 0d; d < MathHelper.TwoPi; d += 0.2d)
                Dust.NewDust(projectile.Hitbox, 6, new Vector2((float)Math.Cos(d) * -40f, (float)Math.Sin(d) * -40f), 0, new Color(), 0.7f);
        }

        public override void DamageNPC(NPC npc, int hitDir, ref int damage, ref float knockback, ref bool crit, ref float critMult)
        {
            if (Main.rand.Next(1, 2) == 1)
            {
                if (crit)
                    npc.AddBuff(24, 240, true);
                else
                    npc.AddBuff(24, 120, true);
            }

            projectile.timeLeft = 0;

            for (double d = 0d; d < MathHelper.TwoPi; d += 0.2d)
                Dust.NewDust(projectile.Hitbox, 6, new Vector2((float)Math.Cos(d) * -40f, (float)Math.Sin(d) * -40f), 0, new Color(), 0.7f);
        }
    }
}
