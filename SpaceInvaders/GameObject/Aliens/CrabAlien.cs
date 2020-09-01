using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class CrabAlien : AlienCategory
    {
        public CrabAlien(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.scoreValue = 20;
        }

        override public void Move()
        {
            this.x += delta;

            if (this.x < 0.0f || this.x > 800.0f)
            {
                delta *= -1;
            }
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an SquidAlien
            // Call the appropriate collision reaction            
            other.VisitCrabAlien(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // Missile vs Alien
            GameObject pGameObj = (GameObject)Iterator.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs Alien
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        override public void Update()
        {
            base.Update();
        }

        // Data: ---------------
        private float delta;
    }
}
