using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveUFOObserver : ColObserver
    {
        public RemoveUFOObserver()
        {
            this.pUFO = null;
        }

        public RemoveUFOObserver(RemoveUFOObserver m)
        {
            Debug.Assert(m.pUFO != null);
            this.pUFO = m.pUFO;

            this.pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            Debug.Assert(this.pSB_Aliens != null);

            this.pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            Debug.Assert(this.pSB_Boxes != null);
        }

        public override void Notify()
        {
            // At this point we have two game objects
            // Actually we can control the objects in the visitor
            // Alphabetical ordering... A is missile,  B is wall

            // This cast will throw an exception if I'm wrong
            this.pUFO = (UFO)this.pSubject.pObjB;

            if (pUFO.bMarkForDeath == false)
            {
                pUFO.bMarkForDeath = true;

                // Delay - remove object later
                // TODO - reduce the new functions
                RemoveUFOObserver pObserver = new RemoveUFOObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
            //this.pAlien.Remove();

            GameObject pA = (GameObject)this.pUFO;
            GameObject pB = (GameObject)Iterator.GetParent(pA);

            pA.Remove();

            // TODO:  update score - may need a better way (maybe an observer)
            SceneContext sc = SceneContext.GetInstance();
            sc.GetState().UpdateScore(this.pUFO.GetScore());

            // TODO: Splat Alien - needs a better way
            this.pSplat = new Splat(GameObject.Name.Splat, GameSprite.Name.SplatUFO, pUFO.x, pUFO.y);
            pSplat.ActivateCollisionSprite(this.pSB_Boxes);
            pSplat.ActivateGameSprite(this.pSB_Aliens);

            GameObject pSplatbRoot = GameObjectMan.Find(GameObject.Name.SplatRoot);
            Debug.Assert(pSplatbRoot != null);
            pSplatbRoot.Add(pSplat);

            TimerMan.Add(TimeEvent.Name.SplatRemoveUFO, new SplatRemoveEvent(this.pSplat), 0.5f);
        }

        // data
        private UFO pUFO;
        private SpriteBatch pSB_Aliens;
        private SpriteBatch pSB_Boxes;
        private SplatCategory pSplat;

    }
}
