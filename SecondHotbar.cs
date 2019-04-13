/*
 * todo: update description.txt
 */

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace SecondHotbar {
    public class SecondHotbar : Mod {
        private const string ChangeKeyName = "Swap Hotbar";
        private const string SwapItemKeyName = "Swap Item Modifier";

        private ModHotKey changeKey;
        private ModHotKey swapItemKey;

        public override void Load() {
            Properties = new ModProperties() {
                Autoload = true,
                AutoloadBackgrounds = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };

            changeKey = RegisterHotKey(ChangeKeyName, Keys.Tab.ToString());
            swapItemKey = RegisterHotKey(SwapItemKeyName, Keys.LeftAlt.ToString());

            AddGlobalItem("GlobalHotbarItem", new GlobalHotbarItem());
        }

        public override void HotKeyPressed(string name) {
            SecondHotbarPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<SecondHotbarPlayer>(this);

            if(changeKey.JustPressed) {
                modPlayer.SwapHotbars();
            }
            else if(swapItemKey.Current) {
                if(PlayerInput.Triggers.JustPressed.Hotbar1) {
                    modPlayer.SwapItem(1);
                }
                else if(PlayerInput.Triggers.JustPressed.Hotbar2) {
                    modPlayer.SwapItem(2);
                }
                else if(PlayerInput.Triggers.JustPressed.Hotbar3) {
                    modPlayer.SwapItem(3);
                }
                else if(PlayerInput.Triggers.JustPressed.Hotbar4) {
                    modPlayer.SwapItem(4);
                }
                else if(PlayerInput.Triggers.JustPressed.Hotbar5) {
                    modPlayer.SwapItem(5);
                }
                else if(PlayerInput.Triggers.JustPressed.Hotbar6) {
                    modPlayer.SwapItem(6);
                }
                else if(PlayerInput.Triggers.JustPressed.Hotbar7) {
                    modPlayer.SwapItem(7);
                }
                else if(PlayerInput.Triggers.JustPressed.Hotbar8) {
                    modPlayer.SwapItem(8);
                }
                else if(PlayerInput.Triggers.JustPressed.Hotbar9) {
                    modPlayer.SwapItem(9);
                }
                else if(PlayerInput.Triggers.JustPressed.Hotbar10) {
                    modPlayer.SwapItem(10);
                }
            }
        }

        public override void PostDrawInterface(SpriteBatch spriteBatch) {
            SecondHotbarPlayer player = Main.player[Main.myPlayer].GetModPlayer<SecondHotbarPlayer>(this);
            player.Draw(spriteBatch);
            base.PostDrawInterface(spriteBatch);
        }

        public static string GetTriggerName(Mod mod, string name) {
            return mod.Name + ": " + name;
        }
    }
}
