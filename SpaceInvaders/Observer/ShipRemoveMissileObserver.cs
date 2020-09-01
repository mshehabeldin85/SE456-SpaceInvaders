using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipRemoveMissileObserver : ColObserver
    {
        public ShipRemoveMissileObserver(bool showSplat)
        {
            this.pMissile = null;
            this.showSplat = showSplat;
        }

        public ShipRemoveMissileObserver(ShipRemoveMissileObserver m)
        {
            Debug.Assert(m.pMissile != null);
            this.pMissile = m.pMissile;

            this.pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            Debug.Assert(this.pSB_Aliens != null);

            this.pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            Debug.Assert(this.pSB_Boxes != null);

            this.showSplat = m.showSplat;
        }

        public override void Notify()
        {
            // At this point we have two game objects
            // Actually we can control the objects in the visitor
            // Alphabetical ordering... A is missile,  B is wall

            // This cast will throw an exception if I'm wrong
            this.pMissile = (Missile)this.pSubject.pObjA;

            if (pMissile.bMarkForDeath == false)
            {
                pMissile.bMarkForDeath = true;

                // Delay - remove object later
                // TODO - reduce the new functions
                ShipRemoveMissileObserver pObserver = new ShipRemoveMissileObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
            this.pMissile.Remove();

            if (showSplat == true)
            {
                // TODO: Splat Alien - needs a better way
                this.pSplat = new Splat(GameObject.Name.Splat, GameSprite.Name.SplatMissile, pMissile.x, pMissile.y);
                pSplat.ActivateCollisionSprite(this.pSB_Boxes);
                pSplat.ActivateGameSprite(this.pSB_Aliens);

                GameObject pSplatbRoot = GameObjectMan.Find(GameObject.Name.SplatRoot);
                Debug.Assert(pSplatbRoot != null);
                pSplatbRoot.Add(pSplat);

                TimerMan.Add(TimeEvent.Name.SplatRemoveMissile, new SplatRemoveEvent(this.pSplat), 0.5f);
            }
        }

        // data
        private GameObject pMissile;
        private SpriteBatch pSB_Aliens;
        private SpriteBatch pSB_Boxes;
        private SplatCategory pSplat;
        private bool showSplat;
    }
}
