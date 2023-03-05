using CustomSlot.UI;
using Terraria;
using Terraria.ModLoader;

namespace SecondHotbar {
    public class GlobalHotbarItem : GlobalItem {
        public override bool OnPickup(Item item, Player player) {
            SecondHotbarPlayer modPlayer = player.GetModPlayer<SecondHotbarPlayer>();

            if(!modPlayer.IsInHotbar(item, out CustomItemSlot slot)) 
                return base.OnPickup(item, player);

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
    }
}
