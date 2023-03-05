using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SecondHotbar.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace SecondHotbar {
    internal class SecondHotbarSystem : ModSystem {
        private const string SwapHotbarKeyName = "Swap Hotbar";
        private const string SwapItemModifierKeyName = "Swap Item Modifier";

        private UserInterface secondHotbarInterface;
        public static ModKeybind SwapHotbarKey;
        public static ModKeybind SwapItemModifierKey;

        public static SecondHotbarUI UI;

        public override void Load() {
            SwapHotbarKey = KeybindLoader.RegisterKeybind(Mod, SwapHotbarKeyName, Keys.Tab.ToString());
            SwapItemModifierKey = KeybindLoader.RegisterKeybind(Mod, SwapItemModifierKeyName, Keys.LeftAlt.ToString());

            if(Main.dedServ)
                return;

            secondHotbarInterface = new UserInterface();
            UI = new SecondHotbarUI();

            UI.Activate();
            secondHotbarInterface.SetState(UI);
        }

        public override void UpdateUI(GameTime gameTime) {
            if(UI.IsVisible)
                secondHotbarInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
            int inventoryLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));

            if(inventoryLayer != -1) {
                layers.Insert(
                    inventoryLayer,
                    new LegacyGameInterfaceLayer(
                        "Second Hotbar: Custom Slot UI",
                        () => {
                            if(UI.IsVisible)
                                secondHotbarInterface.Draw(Main.spriteBatch, new GameTime());

                            return true;
                        },
                        InterfaceScaleType.UI));
            }
        }

        public override void Unload() {
            UI = null;
        }
    }
}
