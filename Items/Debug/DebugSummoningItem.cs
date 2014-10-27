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
    class DebugSummoningItem : ModItem
    {
        public override bool? UseItem(Player player)
        {

            NPC.NewNPC(new Vector2(player.Center.X, player.Center.Y - 400), NPCDef.byName["MysticalMagics:PosessedSarcophogus"].type, 0);

            return true;
        }
    }
}
