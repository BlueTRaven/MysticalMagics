using System;
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
        public override void PostNPCLoot()
        {
            if ((npc.type == NPCDef.byName["MysticalMagics:PosessedSarcophogus"].type) && !MWorld.SarcophogusBeaten)
            {
                Main.NewText("Three enchanted spirits have been released...", Color.Turquoise);
                MWorld.SarcophogusBeaten = true;
            }
        }
    }
}
