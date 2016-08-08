using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TerraUI {
    public abstract class UIObject {
        public Vector2 position; //x and y pos
        public Vector2 size; //width and height
        public Rectangle rectangle;

        public List<UIObject> children;
        public UIObject parent;

        public UIObject(Vector2 pos, Vector2 size, UIObject parent = null) {
            this.position = pos;
            this.size = size;
            this.children = new List<UIObject>();
            if(parent != null) {
                this.parent = parent;
            }
        }

        public virtual void Draw(SpriteBatch sb) {
            foreach(UIObject obj in this.children) {
                obj.Draw(sb);
            }
        }
    }
}
