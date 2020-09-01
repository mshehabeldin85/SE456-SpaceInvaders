using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveShipObserver : ColObserver
    {
        public RemoveShipObserver(SceneState scenePlay)
        {
            this.pShip = null;

            this.scenePlay = scenePlay;
        }

        public RemoveShipObserver(RemoveShipObserver m)
        {
            Debug.Assert(m.pShip != null);
            this.pShip = m.pShip;

            this.scenePlay = m.scenePlay;
        }

        public override void Notify()
        {
            // At this point we have two game objects
            // Actually we can control the objects in the visitor
            // Alphabetical ordering... A is missile,  B is wall

            // This cast will throw an exception if I'm wrong
            this.pShip = (Ship)this.pSubject.pObjB;

            if (pShip.bMarkForDeath == false)
            {
                pShip.bMarkForDeath = true;

                // Delay - remove object later
                // TODO - reduce the new functions
                RemoveShipObserver pObserver = new RemoveShipObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            if (scenePlay.numLives > 1)
            {
                GameObject pA = (GameObject)this.pShip;
                GameObject pB = (GameObject)Iterator.GetParent(pA);

                pA.Remove();

                this.pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);
                Debug.Assert(this.pSB_Aliens != null);

                this.pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
                Debug.Assert(this.pSB_Boxes != null);

                // TODO: Splat Alien - needs a better way
                this.pSplat = new Splat(GameObject.Name.Splat, GameSprite.Name.SplatShip, pShip.x, pShip.y);
                pSplat.ActivateCollisionSprite(this.pSB_Boxes);
                pSplat.ActivateGameSprite(this.pSB_Aliens);

                GameObject pSplatbRoot = GameObjectMan.Find(GameObject.Name.SplatRoot);
                Debug.Assert(pSplatbRoot != null);
                pSplatbRoot.Add(pSplat);

                TimerMan.Add(TimeEvent.Name.SplatRemoveShip, new SplatRemoveEvent(this.pSplat), 0.6f);

                this.scenePlay.RemoveLife();
        }
    }

        // data
        private Ship pShip;
        private SpriteBatch pSB_Aliens;
        private SpriteBatch pSB_Boxes;
        private SplatCategory pSplat;
        private SceneState scenePlay;

    }
}
