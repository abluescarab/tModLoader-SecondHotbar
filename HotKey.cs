using Microsoft.Xna.Framework.Input;

namespace SecondHotbar {
    public class HotKey {
        private string name;
        private Keys defaultKey;
        
        public string Name { get { return name; } }
        public Keys DefaultKey { get { return defaultKey; } }

        public HotKey(string name, Keys defaultKey) {
            this.name = name;
            this.defaultKey = defaultKey;
        }
    }
}
