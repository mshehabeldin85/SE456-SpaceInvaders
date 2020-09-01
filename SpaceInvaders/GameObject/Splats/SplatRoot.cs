using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SplatRoot : Composite
    {
        public SplatRoot(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetLineColor(0, 0, 1);
        }

        public override void Accept(ColVisitor other)
        {
            // do nothing
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }



        // Data: ---------------


    }
}
