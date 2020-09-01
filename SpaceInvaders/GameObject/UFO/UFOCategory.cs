using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class UFOCategory : Leaf
    {
        public enum Type
        {
            UFO,
            UFORoot,
            Unitialized
        }

        protected UFOCategory(GameObject.Name name, GameSprite.Name spriteName, UFOCategory.Type UFOType)
            : base(name, spriteName)
        {
            this.type = UFOType;
        }

        // Data: ---------------

        // this is just a placeholder, who knows what data will be stored here
        protected UFOCategory.Type type;

    }
}

// End of File
