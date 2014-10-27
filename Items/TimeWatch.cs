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
    class TimeWatch : ModItem
    {
        public override bool? UseItem(Player player)
        {
            if (Main.dayTime)
                Main.dayTime = false;
            else Main.dayTime = true;

            Main.NewText("It is now " + (Main.dayTime ? "Day" : "Night"));

            return true;
        }
    }
}
