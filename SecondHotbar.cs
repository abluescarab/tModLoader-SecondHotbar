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
        public const string changeKeyName = "Swap Hotbar";
        public const string swapItemKeyName = "Swap Item Modifier";

        private HotKey changeKey = new HotKey(changeKeyName, Keys.Tab);
        private HotKey swapItemKey = new HotKey(swapItemKeyName, Keys.LeftAlt);

        public override void Load() {
            Properties = new ModProperties() {
                Autoload = true,
                AutoloadBackgrounds = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };

            RegisterHotKey(changeKey.Name, changeKey.DefaultKey.ToString());
            RegisterHotKey(swapItemKey.Name, swapItemKey.DefaultKey.ToString());

            AddGlobalItem("GlobalHotbarItem", new GlobalHotbarItem());
        }

        public override void HotKeyPressed(string name) {
            if(PlayerInput.Triggers.JustPressed.KeyStatus[GetTriggerName(this, name)]) {
                if(name.Equals(changeKey.Name)) {
                    SecondHotbarPlayer player = Main.player[Main.myPlayer].GetModPlayer<SecondHotbarPlayer>(this);
                    player.SwapHotbars();
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
