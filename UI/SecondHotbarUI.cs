using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomSlot;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace SecondHotbar.UI {
    public class SecondHotbarUI : UIState {
        private const float SlotScale = 0.85f;
        private const float SlotMargin = 5f;

        public List<CustomItemSlot> Slots;
        public float CustomPanelX;
        public float CustomPanelY;
        public CustomUIPanel Panel { get; private set; }

        public bool IsVisible => Main.playerInventory;

        public override void OnInitialize() {
            Slots = new List<CustomItemSlot>(10);
            Panel = new CustomUIPanel();

            float slotSize = 0f;
            float slotX = 0f;
            float slotY = 0f;
            float offset = SlotMargin * SlotScale;

            for(int i = 0; i < 10; i++) {
                CustomItemSlot slot = new CustomItemSlot(ItemSlot.Context.HotbarItem, SlotScale) {
                    Left = new StyleDimension(slotX, 0),
                    Top = new StyleDimension(slotY, 0)
                };

                Panel.Append(slot);

                if(slotSize < 0.1f)
                    slotSize = slot.Width.Pixels;

                slotX += slotSize + offset;
            }

            if(SecondHotbarConfig.Instance.HotbarLocation == SecondHotbarConfig.Location.Custom)
                MoveToCustomPosition();
            else
                SetPosition();

            Panel.Width.Set((slotSize * 10) + (SlotMargin * 9) + Panel.PaddingLeft + Panel.PaddingRight, 0);
            Panel.Height.Set(slotSize + Panel.PaddingTop + Panel.PaddingBottom, 0);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);

            if(SecondHotbarConfig.Instance.HotbarLocation == SecondHotbarConfig.Location.Custom) {
                CustomPanelX = Panel.Left.Pixels;
                CustomPanelY = Panel.Top.Pixels;
                return;
            }

            SetPosition();
        }

        public void MoveToCustomPosition() {
            Panel.Left.Set(CustomPanelX, 0);
            Panel.Top.Set(CustomPanelY, 0);
        }

        public void SetPosition() {
            Panel.Left.Set(580f, 0);
            Panel.Top.Set(20f, 0);
        }
    }
}
