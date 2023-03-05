using Terraria.ModLoader;

namespace SecondHotbar {
    public class SecondHotbar : Mod {
        public SecondHotbar() {
            ContentAutoloadingEnabled = true;
            BackgroundAutoloadingEnabled = true;
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

                        return SecondHotbarSystem.UI.Slots[(int)args[1]].Item;
                    default:
                        return "Error: not a valid command";
                }
            }
            catch {
                return null;
            }
        }
    }
}