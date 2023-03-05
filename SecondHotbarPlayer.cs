using CustomSlot.UI;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace SecondHotbar {
    public class SecondHotbarPlayer : ModPlayer {
        private const string ItemTag = "Item";
        private const string PanelXTag = "PanelX";
        private const string PanelYTag = "PanelY";

        public Item[] Items;

        public override void Initialize() {
            Items = new Item[10];

            for(int i = 0; i < 10; i++) {
                Items[i] = new Item();
                Items[i].SetDefaults();
            }
        }

        public override void OnEnterWorld(Player player) {
            if(SecondHotbarSystem.UI == null)
                return;

            for(int i = 0; i < 10; i++) {
                SecondHotbarSystem.UI.Slots[i].SetItem(Items[i].Clone(), false);

                SecondHotbarSystem.UI.Slots[i].ItemChanged += (sender, e) => {
                    int index = SecondHotbarSystem.UI.Slots.FindIndex(s => s.UniqueId == sender.UniqueId);
                    Items[index] = sender.Item.Clone();
                };
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet) {
            SecondHotbarPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<SecondHotbarPlayer>();

            if(SecondHotbarSystem.SwapHotbarKey.JustPressed) {
                modPlayer.SwapHotbars();
            }
            else if(SecondHotbarSystem.SwapItemModifierKey.Current) {
                if(PlayerInput.Triggers.JustPressed.Hotbar1) {
                    modPlayer.SwapItem(0);
                }
                if(PlayerInput.Triggers.JustPressed.Hotbar2) {
                    modPlayer.SwapItem(1);
                }
                if(PlayerInput.Triggers.JustPressed.Hotbar3) {
                    modPlayer.SwapItem(2);
                }
                if(PlayerInput.Triggers.JustPressed.Hotbar4) {
                    modPlayer.SwapItem(3);
                }
                if(PlayerInput.Triggers.JustPressed.Hotbar5) {
                    modPlayer.SwapItem(4);
                }
                if(PlayerInput.Triggers.JustPressed.Hotbar6) {
                    modPlayer.SwapItem(5);
                }
                if(PlayerInput.Triggers.JustPressed.Hotbar7) {
                    modPlayer.SwapItem(6);
                }
                if(PlayerInput.Triggers.JustPressed.Hotbar8) {
                    modPlayer.SwapItem(7);
                }
                if(PlayerInput.Triggers.JustPressed.Hotbar9) {
                    modPlayer.SwapItem(8);
                }
                if(PlayerInput.Triggers.JustPressed.Hotbar10) {
                    modPlayer.SwapItem(9);
                }
            }
        }

        public void SwapHotbars() {
            for(int i = 0; i < 10; i++) {
                SwitchItems(ref Player.inventory[i], ref Items[i]);
                SecondHotbarSystem.UI.Slots[i].SetItem(Items[i].Clone());
            }
        }

        public void SwapItem(int slot) {
            if(slot < 0 || slot > 9) return;

            SwitchItems(ref Player.inventory[slot], ref Items[slot]);
            SecondHotbarSystem.UI.Slots[slot].SetItem(Items[slot].Clone());
        }

        public bool IsInHotbar(Item item, out CustomItemSlot slot) {
            foreach(CustomItemSlot s in SecondHotbarSystem.UI.Slots) {
                if(!item.Name.Equals(s.Item.Name)) continue;

                slot = s;
                return true;
            }

            slot = null;
            return false;
        }

        public override void SaveData(TagCompound tag) {
            for(int i = 0; i < 10; i++) {
                tag.Add(ItemTag + i, ItemIO.Save(Items[i]));
            }

            tag.Add(PanelXTag, SecondHotbarSystem.UI.CustomPanelX);
            tag.Add(PanelYTag, SecondHotbarSystem.UI.CustomPanelY);
        }

        public override void LoadData(TagCompound tag) {
            for(int i = 0; i < 10; i++) {
                Items[i] = ItemIO.Load(tag.GetCompound(ItemTag + i));
            }

            SecondHotbarSystem.UI.CustomPanelX = tag.GetFloat(PanelXTag);
            SecondHotbarSystem.UI.CustomPanelY = tag.GetFloat(PanelYTag);
        }

        /// <summary>
        /// Switch two items.
        /// </summary>
        /// <param name="item1">first item</param>
        /// <param name="item2">second item</param>
        public static void SwitchItems(ref Item item1, ref Item item2) {
            Item item3 = item2.Clone();
            item2 = item1;
            item1 = item3;
        }
    }
}
