using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI.Chat;
using TerraUI;

namespace SecondHotbar {
    internal class MPlayer : ModPlayer {
        public const ushort Installed = 0;

        private List<UIItemSlot> slots;

        public override bool Autoload(ref string name) {
            return true;
        }

        public override void Initialize() {
            slots = new List<UIItemSlot>(10);

            for(int i = 0; i < 10; i++) {
                slots.Add(new UIItemSlot(Vector2.Zero,
                    drawBackground: delegate (SpriteBatch spriteBatch, UIItemSlot slot) {
                        spriteBatch.Draw(
                            Main.inventoryBackTexture,
                            new Vector2(slot.position.X, slot.position.Y),
                            null,
                            Color.White * 0.7f,
                            0f,
                            Vector2.Zero,
                            Main.inventoryScale,
                            SpriteEffects.None,
                            1f);
                    },
                    drawItem: delegate (SpriteBatch spriteBatch, UIItemSlot slot) {
                        Texture2D texture2D = Main.itemTexture[slot.item.type];
                        Rectangle rectangle2;
                        Vector2 origin;
                        float scale = 1f;

                        if(Main.itemAnimations[slot.item.type] != null) {
                            rectangle2 = Main.itemAnimations[slot.item.type].GetFrame(texture2D);
                        }
                        else {
                            rectangle2 = texture2D.Frame(1, 1, 0, 0);
                        }

                        origin = new Vector2(rectangle2.Width / 2, rectangle2.Height / 2);

                        if(rectangle2.Width > 32 || rectangle2.Height > 32) {
                            if(rectangle2.Width > rectangle2.Height) {
                                scale = 32f / rectangle2.Width;
                            }
                            else {
                                scale = 32f / rectangle2.Height;
                            }
                        }
                        scale *= Main.inventoryScale;

                        spriteBatch.Draw(Main.itemTexture[slot.item.type],
                            new Vector2(
                                slot.rectangle.X + slot.rectangle.Width / 2,
                                slot.rectangle.Y + slot.rectangle.Height / 2),
                            new Rectangle?(rectangle2),
                            Color.White,
                            0f,
                            origin,
                            scale,
                            SpriteEffects.None,
                            0f);
                    },
                    postDrawItem: delegate (SpriteBatch spriteBatch, UIItemSlot slot) {
                        if(slot.item.stack > 1) {
                            float x = slot.position.X;
                            float y = slot.position.Y + (slot.size.Y / 2) * Main.inventoryScale * (1f - 0.75f);

                            Vector2 position = new Vector2(x, y);

                            ChatManager.DrawColorCodedStringWithShadow(
                                spriteBatch,
                                Main.fontItemStack,
                                slot.item.stack.ToString(),
                                position + new Vector2(10f, 21f) * Main.inventoryScale,
                                Color.White,
                                0f,
                                Vector2.Zero,
                                new Vector2(Main.inventoryScale),
                                -1f,
                                Main.inventoryScale);
                        }
                    },
                    leftClick: delegate (UIItemSlot slot) {
                        if(slot.item.stack > 0 && PlayerInput.Triggers.Current.SmartSelect) {
                            int toSlot = Array.FindIndex(player.inventory, 10, s => s.stack == 0);
                            
                            if(toSlot > -1 && toSlot < 50) {
                                player.inventory[toSlot] = slot.item.Clone();
                                Main.PlaySound(7, -1, -1, 1);
                                Recipe.FindRecipes();
                                slot.item = new Item();
                                slot.item.SetDefaults();
                            }

                            return true;
                        }
                        else {
                            return false;
                        }
                    }));
            }
        }

        public override bool PreItemCheck() {
            if(PlayerInput.Triggers.Current.KeyStatus[SecondHotbar.GetTriggerName(
                mod, SecondHotbar.swapItemKeyName)]) {
                if(PlayerInput.Triggers.JustPressed.Hotbar1) {
                    SwapItem(1);
                }
                else if(PlayerInput.Triggers.JustPressed.Hotbar2) {
                    SwapItem(2);
                }
                else if(PlayerInput.Triggers.JustPressed.Hotbar3) {
                    SwapItem(3);
                }
                else if(PlayerInput.Triggers.JustPressed.Hotbar4) {
                    SwapItem(4);
                }
                else if(PlayerInput.Triggers.JustPressed.Hotbar5) {
                    SwapItem(5);
                }
                else if(PlayerInput.Triggers.JustPressed.Hotbar6) {
                    SwapItem(6);
                }
                else if(PlayerInput.Triggers.JustPressed.Hotbar7) {
                    SwapItem(7);
                }
                else if(PlayerInput.Triggers.JustPressed.Hotbar8) {
                    SwapItem(8);
                }
                else if(PlayerInput.Triggers.JustPressed.Hotbar9) {
                    SwapItem(9);
                }
                else if(PlayerInput.Triggers.JustPressed.Hotbar10) {
                    SwapItem(10);
                }
            }

            return true;
        }

        public void Draw(SpriteBatch spriteBatch) {
            if(Main.playerInventory) {
                float slotW = Main.inventoryBackTexture.Width * Main.inventoryScale;
                float offset = 5f * Main.inventoryScale;
                float slotX = 20f + (800f * Main.inventoryScale);
                float slotY = 20f;

                foreach(UIItemSlot slot in slots) {
                    slot.position = new Vector2(slotX, slotY);
                    slot.size = new Vector2(52 * Main.inventoryScale);
                    slotX += slotW + offset;

                    slot.Draw(spriteBatch);
                }
            }
        }

        public void SwapHotbars() {
            for(int i = 0; i < 10; i++) {
                Utils.Swap(ref player.inventory[i], ref slots[i].item);
            }
        }

        public void SwapItem(int slot) {
            Utils.Swap(ref player.inventory[slot - 1], ref slots[slot - 1].item);
        }

        public override void SaveCustomData(BinaryWriter writer) {
            writer.Write(Installed);
            WriteHotbar(slots, writer, true, false);
        }

        public override void LoadCustomData(BinaryReader reader) {
            ushort installFlag = reader.ReadUInt16();

            if(installFlag == 0) {
                ReadHotbar(slots, reader, true, false);
            }
        }

        internal static bool WriteHotbar(List<UIItemSlot> itemSlots, BinaryWriter writer, bool writeStack = false, bool writeFavorite = false) {
            ushort count = 0;
            byte[] data;

            using(MemoryStream stream = new MemoryStream()) {
                using(BinaryWriter w = new BinaryWriter(stream)) {
                    for(int i = 0; i < itemSlots.Count; i++) {
                        w.Write((ushort)i);
                        ItemIO.WriteItem(itemSlots[i].item, w, writeStack, writeFavorite);
                        count++;
                    }
                }

                data = stream.ToArray();
            }

            if(count > 0) {
                writer.Write(count);
                writer.Write(data);

                return true;
            }

            return false;
        }

        internal static void ReadHotbar(List<UIItemSlot> itemSlots, BinaryReader reader, bool readStack = false, bool readFavorite = false) {
            ushort count = reader.ReadUInt16();

            for(int i = 0; i < count; i++) {
                ushort index = reader.ReadUInt16();
                ItemIO.ReadItem(itemSlots[i].item, reader, readStack, readFavorite);
            }
        }
    }
}
