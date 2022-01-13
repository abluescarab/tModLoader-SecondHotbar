using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SecondHotbar.UI;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;

namespace SecondHotbar {
	public class SecondHotbar : Mod {
        private const string SwapHotbarKeyName = "Swap Hotbar";
        private const string SwapItemModifierKeyName = "Swap Item Modifier";

        private UserInterface secondHotbarInterface;
        private ModHotKey swapHotbarKey;
        private ModHotKey swapItemModifierKey;

        public static SecondHotbarUI UI;

        public override void Load() {
            swapHotbarKey = RegisterHotKey(SwapHotbarKeyName, Keys.Tab.ToString());
            swapItemModifierKey = RegisterHotKey(SwapItemModifierKeyName, Keys.LeftAlt.ToString());

            if(Main.dedServ) return;

            secondHotbarInterface = new UserInterface();
            UI = new SecondHotbarUI();

            UI.Activate();
            secondHotbarInterface.SetState(UI);
        }

        public override void Unload() {
            UI = null;
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

        public override object Call(params object[] args) {
            try {
                string keyword = args[0] as string;

                if(string.IsNullOrEmpty(keyword)) {
                    return "Error: no command provided";
                }

                switch(keyword.ToLower()) {
                    case "getitem":
                        if(!(args[1] is int)) {
                            return "Error: not a valid integer";
                        }

                        return UI.Slots[(int)args[1]].Item;
                    default:
                        return "Error: not a valid command";
                }
            }
            catch {
                return null;
            }
        }

        public override void HotKeyPressed(string name) {
            SecondHotbarPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<SecondHotbarPlayer>();

            if(swapHotbarKey.JustPressed) {
                modPlayer.SwapHotbars();
            }
            else if(swapItemModifierKey.Current) {
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
    }
}