using CustomSlot.UI;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;

namespace SecondHotbar.UI {
    public class SecondHotbarUI : UIState {
        private const float SlotMargin = 5f;

        public List<CustomItemSlot> Slots;
        public float CustomPanelX;
        public float CustomPanelY;
        public DraggableUIPanel Panel { get; private set; }

        public bool IsVisible => Main.playerInventory;

        public override void OnInitialize() {
            Slots = new List<CustomItemSlot>(10);
            Panel = new DraggableUIPanel();

            float slotSize = 0f;
            float slotX = 0f;
            float slotY = 0f;

            for(int i = 0; i < 10; i++) {
                CustomItemSlot slot = new CustomItemSlot(ItemSlot.Context.InventoryItem, 0.85f) {
                    Left = new StyleDimension(slotX, 0),
                    Top = new StyleDimension(slotY, 0)
                };

                Slots.Add(slot);
                Panel.Append(slot);

                slotSize = slot.Width.Pixels;
                slotX += slotSize + SlotMargin;
            }

            if(SecondHotbarConfig.Instance.HotbarLocation == SecondHotbarConfig.Location.Custom)
                MoveToCustomPosition();
            else
                SetPosition();

            Panel.Width.Set((slotSize * 10) + (SlotMargin * 9) + Panel.PaddingLeft + Panel.PaddingRight, 0);
            Panel.Height.Set(slotSize + Panel.PaddingTop + Panel.PaddingBottom, 0);

            Append(Panel);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
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
            Panel.Top.Set(20f - Panel.PaddingTop, 0);
        }
    }
}
