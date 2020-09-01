using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SplatCategory : Leaf
    {
        public enum Type
        {
            Splat,
            SplatRoot,
            Unitialized
        }

        protected SplatCategory(GameObject.Name name, GameSprite.Name spriteName, SplatCategory.Type splatType)
            : base(name, spriteName)
        {
            this.type = splatType;
        }

        // Data: ---------------
        // this is just a placeholder, who knows what data will be stored here
        protected SplatCategory.Type type;

    }
}