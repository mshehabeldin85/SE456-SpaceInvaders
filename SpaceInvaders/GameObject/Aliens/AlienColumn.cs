using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienColumn : Composite
    {
        public AlienColumn(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetLineColor(1, 0, 0);
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an AlienColumn
            // Call the appropriate collision reaction            
            other.VisitColumn(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObj);

            // Missile vs Column
            //if (this.lastChild == true)
            //{
            //    pGameObj = (GameObject)Iterator.GetChild(m);
            //    ColPair.Collide(pGameObj, this);
            //}
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs Column
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }
    }
}
