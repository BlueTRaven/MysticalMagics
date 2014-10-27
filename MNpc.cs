﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace MysticalMagics
{
    [GlobalMod]

    class MNpc : ModNPC
    {
        public bool hasSaidText = false;

        public override void PostNPCLoot()
        {
            if ((npc.type == 126 || npc.type == 125 || npc.type == 127 || (npc.type == 134 || npc.type == 135 || npc.type == 136)) && !hasSaidText)
            {
                Main.NewText("Three enchanted spirits have been released...", Color.Turquoise);

                Main.NewText("" + hasSaidText);

                hasSaidText = true;

                Main.NewText("" + hasSaidText);
            }
        }
    }
}
