using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomSlot;
using SecondHotbar.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace SecondHotbar {
    public class SecondHotbarPlayer : ModPlayer {
        private const string ItemTag = "Item";

        public Item[] Items;

        public override void Initialize() {
            Items = new Item[10];

            for(int i = 0; i < 10; i++) {
                Items[i] = new Item();
                Items[i].SetDefaults();
            }
        }

        public override void OnEnterWorld(Player player) {
            if(SecondHotbar.UI == null) return;

            for(int i = 0; i < 10; i++) {
                SecondHotbar.UI.Slots[i].SetItem(Items[i].Clone(), false);

                SecondHotbar.UI.Slots[i].ItemChanged += (sender, e) => {
                    CustomItemSlot slot = (CustomItemSlot)sender;

                    if(int.TryParse(slot.Id.Replace(SecondHotbarUI.IdPrefix, ""), out int postfix)) {
                        Items[postfix] = slot.Item.Clone();
                    }
                };
            }
        }

        public void SwapHotbars() {
            for(int i = 0; i < 10; i++) {
                SwitchItems(ref player.inventory[i], ref Items[i]);
                SecondHotbar.UI.Slots[i].SetItem(Items[i].Clone());
            }
        }

        public void SwapItem(int slot) {
            if(slot <= 0 || slot > 9) return;

            SwitchItems(ref player.inventory[slot], ref Items[slot]);
            SecondHotbar.UI.Slots[slot].SetItem(Items[slot].Clone());
        }

        public bool IsInHotbar(Item item, out CustomItemSlot slot) {
            foreach(CustomItemSlot s in SecondHotbar.UI.Slots) {
                if(!item.Name.Equals(s.Item.Name)) continue;

                slot = s;
                return true;
            }

            slot = null;
            return false;
        }

        public override TagCompound Save() {
            TagCompound tags = new TagCompound();

            for(int i = 0; i < 10; i++) {
                tags.Add(ItemTag + i, ItemIO.Save(Items[i]));
            }

            return tags;
        }

        public override void Load(TagCompound tag) {
            for(int i = 0; i < 10; i++) {
                Items[i] = ItemIO.Load(tag.GetCompound(ItemTag + i));
            }
        }

        /// <summary>
        /// Switch two items.
        /// </summary>
        /// <param name="item1">first item</param>
        /// <param name="item2">second item</param>
        public static void SwitchItems(ref Item item1, ref Item item2) {
            if((item1.type == ItemID.None || item1.stack < 1) && (item2.type != ItemID.None || item2.stack > 0)) //if item2 is mouseitem, then if item slot is empty and item is picked up
            {
                item1 = item2;
                item2 = new Item();
                item2.SetDefaults();
            }
            else if((item1.type != ItemID.None || item1.stack > 0) && (item2.type == ItemID.None || item2.stack < 1)) //if item2 is mouseitem, then if item slot is empty and item is picked up
            {
                item2 = item1;
                item1 = new Item();
                item1.SetDefaults();
            }
            else if((item1.type != ItemID.None || item1.stack > 0) && (item2.type != ItemID.None || item2.stack > 0)) //if item2 is mouseitem, then if item slot is empty and item is picked up
            {
                Item item3 = item2;
                item2 = item1;
                item1 = item3;
            }
        }
    }
}
