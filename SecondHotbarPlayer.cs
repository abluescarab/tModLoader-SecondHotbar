﻿using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using TerraUI.Objects;
using TerraUI.Utilities;

namespace SecondHotbar {
    internal class SecondHotbarPlayer : ModPlayer {
        private const string ItemPrefix = "ITEM";
        private List<UIItemSlot> slots;

        public override void Initialize() {
            slots = new List<UIItemSlot>(10);

            float slotW = 52 * 0.85f;
            float offset = 5f * 0.85f;
            float slotX = 20f + 560f * 0.85f;
            float slotY = 20f;

            for(int i = 0; i < 10; i++) {
                UIItemSlot slot = new UIItemSlot(
                    new Vector2(slotX, slotY),
                    size: (int)slotW,
                    context: ItemSlot.Context.InventoryItem,
                    conditions: Slot_Conditions);
                slots.Add(slot);

                slotX += slotW + offset;
            }
        }

        private static bool Slot_Conditions(Item item) {
            if(item.stack > 0) {
                return true;
            }

            return false;
        }

        public override void PreUpdate() {
            if(Main.playerInventory) {
                foreach(UIItemSlot slot in slots) {
                    slot.Update();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            if(Main.playerInventory) {
                foreach(UIItemSlot slot in slots) {
                    slot.Draw(spriteBatch);
                }
            }
        }

        public void SwapHotbars() {
            for(int i = 0; i < 10; i++) {
                Item item = slots[i].Item;
                UIUtils.SwitchItems(ref player.inventory[i], ref item);
                slots[i].Item = item;
            }
        }

        public void SwapItem(int slot) {
            Item item = slots[slot - 1].Item;
            UIUtils.SwitchItems(ref player.inventory[slot - 1], ref item);
            slots[slot - 1].Item = item;
        }

        public override TagCompound Save() {
            TagCompound tags = new TagCompound();

            for(int i = 0; i < 10; i++) {
                tags.Add(ItemPrefix + (i + 1),
                         ItemIO.Save(slots[i].Item));
            }

            return tags;
        }

        public override void Load(TagCompound tag) {
            for(int i = 0; i < 10; i++) {
                slots[i].Item = ItemIO.Load(tag.GetCompound(ItemPrefix + (i + 1)));
            }
            base.Load(tag);
        }

        public bool IsInHotbar(Item item, out UIItemSlot slot) {
            foreach(UIItemSlot s in slots) {
                if(item.Name.Equals(s.Item.Name)) {
                    slot = s;
                    return true;
                }
            }

            slot = null;
            return false;
        }
    }
}
