using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerraUI.Objects;

namespace SecondHotbar {
    class GlobalHotbarItem : GlobalItem {
        public override bool Autoload(ref string name) {
            return true;
        }

        public override bool OnPickup(Item item, Player player) {
            SecondHotbarPlayer plr = player.GetModPlayer<SecondHotbarPlayer>(mod);
            UIItemSlot slot;

            if(plr.IsInHotbar(item, out slot)) {
                int stack = slot.Item.stack;
                
                if((slot.Item.stack + item.stack) >= item.maxStack) {
                    item.stack -= (item.maxStack - slot.Item.stack);
                    slot.Item.stack = item.maxStack;
                }
                else {
                    slot.Item.stack += item.stack;
                    item.stack = 0;
                }

                return true;
            }
            else {
                return base.OnPickup(item, player);
            }
        }
    }
}
