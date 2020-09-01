using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ShipCategory : Leaf
    {
        public enum Type
        {
            Ship,
            ShipRoot,
            Unitialized
        }

        protected ShipCategory(GameObject.Name name, GameSprite.Name spriteName, ShipCategory.Type shipType)
            : base(name, spriteName)
        {
            this.type = shipType;
        }

        // Data: ---------------
        // this is just a placeholder, who knows what data will be stored here
        protected ShipCategory.Type type;

    }
}