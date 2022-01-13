using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomSlot;
using Terraria;
using Terraria.ModLoader;

namespace SecondHotbar {
    public class GlobalHotbarItem : GlobalItem {
        public override bool OnPickup(Item item, Player player) {
            SecondHotbarPlayer modPlayer = player.GetModPlayer<SecondHotbarPlayer>();
            CustomItemSlot slot;

            if(!modPlayer.IsInHotbar(item, out slot)) return base.OnPickup(item, player;

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
