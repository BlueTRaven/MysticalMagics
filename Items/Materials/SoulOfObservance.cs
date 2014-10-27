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
    class SoulOfObservance : ModItem
    { 
        public override void MidUpdate(ref float gravity, ref float maxVelocity) 
        {
            maxVelocity = 0f;
            gravity = 0.1f;
        }
    }
}
