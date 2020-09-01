using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipRoot : Composite
    {
        public ShipRoot(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetLineColor(0, 0, 1);
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitShipRoot(this);
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        public override void VisitBombRoot(BombRoot b)
        {
            // BombRoot vs ShipRoot
            ColPair.Collide((GameObject)Iterator.GetChild(b), this);
        }
        public override void VisitBomb(Bomb b)
        {
            // Bomb vs Ship
            ColPair.Collide(b, (GameObject)Iterator.GetChild(this));
        }

        // Data: ---------------


    }
}
