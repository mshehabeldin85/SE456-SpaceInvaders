using System;
using System.Diagnostics;


namespace SpaceInvaders
{
    public class Splat : SplatCategory
    {

        public Splat(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
         : base(name, spriteName, SplatCategory.Type.Splat)
        {
            this.x = posX;
            this.y = posY;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Accept(ColVisitor other)
        {
            // do nothing
        }

        // Data: --------------------

    }
}
