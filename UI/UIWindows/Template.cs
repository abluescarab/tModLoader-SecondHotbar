using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Terraria;
using Terraria.ModLoader;

namespace TerraUI {
    public class Template {
        public UIObject obj;
        public Vector2 distanceVector;
        public bool canMove = false;
        public Template() {
            Mod mod = ModLoader.GetMod(UIParameters.MODNAME);

            UIPanel background = new UIPanel(new Vector2(Main.screenWidth / 2 - 150, Main.screenHeight / 2 - 75), new Vector2(300, 150), null);

            //background.children.Add(something); 

            this.obj = background;
        }
        public void Draw(SpriteBatch sb) {
            this.DoMovement();
            this.obj.Draw(sb);
        }
        public void DoMovement() {
            if(new Rectangle(UIParameters.lastMouseState.X, UIParameters.lastMouseState.Y, 1, 1).Intersects(this.obj.rectangle) && new Rectangle(UIParameters.mouseState.X, UIParameters.mouseState.Y, 1, 1).Intersects(this.obj.rectangle) && UIParameters.NoChildrenIntersect(this.obj, new Rectangle(UIParameters.mouseState.X, UIParameters.mouseState.Y, 1, 1))) {
                if(UIParameters.lastMouseState.LeftButton == ButtonState.Released && UIParameters.mouseState.LeftButton == ButtonState.Pressed) {
                    distanceVector = new Vector2(Main.mouseX, Main.mouseY) - this.obj.position;
                    canMove = true;
                }
            }
            if(UIParameters.lastMouseState.LeftButton == ButtonState.Pressed && UIParameters.mouseState.LeftButton == ButtonState.Pressed) {
                if(canMove)
                    this.obj.position = new Vector2(Main.mouseX, Main.mouseY) - distanceVector;
            }
            if(UIParameters.mouseState.LeftButton == ButtonState.Released) {
                canMove = false;
            }
        }
    }
}
