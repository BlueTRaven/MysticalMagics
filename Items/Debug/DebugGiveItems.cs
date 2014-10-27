using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TAPI;
using Terraria;

namespace MysticalMagics.Items
{
    class DebugGiveItems : ModItem
    {
        public override bool? UseItem(Player player)
        {
            player.statLifeMax = 200;
            player.statManaMax = 200;

            int slot = 3;

            // SetDefaults sets an item's default properties depending on the name or type given
            player.inventory[slot].SetDefaults("Picksaw");
            // Prefix (when provided with -1) sets the item's prefix to a random prefix
            player.inventory[slot++].Prefix(-1);

            player.inventory[slot].SetDefaults("Pwnhammer");
            player.inventory[slot++].Prefix(-1);

            player.inventory[slot].SetDefaults("Ankh Shield");
            player.inventory[slot++].Prefix(-1);

            player.inventory[slot].SetDefaults("Magic Cuffs");
            player.inventory[slot++].Prefix(-1);

            player.inventory[slot].SetDefaults("Magnet Sphere");
            player.inventory[slot++].Prefix(-1);

            player.inventory[slot].SetDefaults("Vampire Knives");
            player.inventory[slot++].Prefix(-1);

            player.inventory[slot].SetDefaults("S.D.M.G.");
            player.inventory[slot++].Prefix(-1);

            player.inventory[slot].SetDefaults("Chlorophyte Bullet");
            player.inventory[slot++].stack = 999;

            player.inventory[slot].SetDefaults("Chlorophyte Bullet");
            player.inventory[slot++].stack = 999;

            player.inventory[slot].SetDefaults("Terra Blade");
            player.inventory[slot++].Prefix(-1);

            player.inventory[slot].SetDefaults("Fairy Wings");
            player.inventory[slot++].Prefix(-1);

            player.inventory[slot].SetDefaults("Nimbus Rod");
            player.inventory[slot++].Prefix(-1);

            player.inventory[slot].SetDefaults("Bundle of Balloons");
            player.inventory[slot++].Prefix(-1);

            player.inventory[slot].SetDefaults("Rainbow Gun");
            player.inventory[slot++].Prefix(-1);

            player.inventory[slot].SetDefaults("Rod of Discord");
            player.inventory[slot++].Prefix(-1);

            player.inventory[slot].SetDefaults("Golden Shower");
            player.inventory[slot++].Prefix(-1);

            player.inventory[slot].SetDefaults("Frostspark Boots");
            player.inventory[slot++].Prefix(-1);

            return true;
        }
    }
}
