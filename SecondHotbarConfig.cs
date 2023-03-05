using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace SecondHotbar {
    public class SecondHotbarConfig : ModConfig {
        public enum Location {
            Default,
            Custom
        }

        private Location lastLocation = Location.Default;

        public override ConfigScope Mode => ConfigScope.ClientSide;

        public static SecondHotbarConfig Instance;

        [DefaultValue(Location.Default)]
        [Label("Hotbar location")]
        [DrawTicks]
        public Location HotbarLocation;

        [DefaultValue(false)]
        [Tooltip("Show the draggable panel for a custom location")]
        [Label("Show custom location panel")]
        public bool ShowCustomLocationPanel;

        public override void OnChanged() {
            if(SecondHotbarSystem.UI == null) return;

            if(lastLocation == Location.Custom && HotbarLocation != Location.Custom)
                ShowCustomLocationPanel = false;

            SecondHotbarSystem.UI.Panel.Visible = ShowCustomLocationPanel;
            SecondHotbarSystem.UI.Panel.CanDrag = ShowCustomLocationPanel;

            if(ShowCustomLocationPanel)
                HotbarLocation = Location.Custom;

            if(HotbarLocation == Location.Custom)
                SecondHotbarSystem.UI.MoveToCustomPosition();

            lastLocation = HotbarLocation;
        }
    }
}
