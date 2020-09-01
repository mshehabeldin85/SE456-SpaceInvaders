using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallRight : WallCategory
    {
        public WallRight(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, WallCategory.Type.Right)
        {
            this.poColObj.poColRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetLineColor(1, 1, 0);
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitWallRight(this);
        }

        public override void Update()
        {
            // Go to first child
            base.Update();
        }

        public override void VisitGroup(AlienGrid a)
        {
            //a.DropGrid();
            //Debug.WriteLine("Drop grid");

            //a.SetDelta(-1.0f);
            //Debug.WriteLine("reverse grid left");

            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(a, this);
            pColPair.NotifyListeners();
        }

        public override void VisitShip(Ship s)
        {
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(s, this);
            pColPair.NotifyListeners();
        }

        // Data: ---------------


    }
}
