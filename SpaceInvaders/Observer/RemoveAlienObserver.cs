using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveAlienObserver : ColObserver
    {
        public RemoveAlienObserver(SceneState scenePlay)
        {
            this.pAlien = null;

            this.scenePlay = scenePlay;
        }

        public RemoveAlienObserver(RemoveAlienObserver m)
        {
            Debug.Assert(m.pAlien != null);
            this.pAlien = m.pAlien;

            this.pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            Debug.Assert(this.pSB_Aliens != null);

            this.pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            Debug.Assert(this.pSB_Boxes != null);

            this.scenePlay = m.scenePlay;
        }

        public override void Notify()
        {
            // At this point we have two game objects
            // Actually we can control the objects in the visitor
            // Alphabetical ordering... A is missile,  B is wall

            // This cast will throw an exception if I'm wrong
            this.pAlien = (AlienCategory)this.pSubject.pObjB;

            if (pAlien.bMarkForDeath == false)
            {
                pAlien.bMarkForDeath = true;

                // Delay - remove object later
                // TODO - reduce the new functions
                RemoveAlienObserver pObserver = new RemoveAlienObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
            //this.pAlien.Remove();

            GameObject pA = (GameObject)this.pAlien;
            GameObject pB = (GameObject)Iterator.GetParent(pA);

            pA.Remove();

            // TODO: Need a better way... 
            if (privCheckParent(pB) == true)
            {
                GameObject pC = (GameObject)Iterator.GetParent(pB);
                pB.Remove();

                if (privCheckParent(pC) == true)
                {
                    //pC.Remove();

                    // Recreate Grid on last alien delete
                    AlienGrid pGrid = (AlienGrid)pC;
                    pGrid.GenerateAlien(GameObjectMan.GetActive());
                    pGrid.ResetSpeed();
                    this.scenePlay.AddLife();
                }
            }

            // TODO:  update score - may need a better way (maybe an observer)            
            SceneContext sc = SceneContext.GetInstance();
            sc.GetState().UpdateScore(this.pAlien.scoreValue);


            // TODO: Splat Alien - needs a better way
            this.pSplat = new Splat(GameObject.Name.Splat, GameSprite.Name.SplatAlien, pAlien.x, pAlien.y);
            pSplat.ActivateCollisionSprite(this.pSB_Boxes);
            pSplat.ActivateGameSprite(this.pSB_Aliens);

            GameObject pSplatbRoot = GameObjectMan.Find(GameObject.Name.SplatRoot);
            Debug.Assert(pSplatbRoot != null);
            pSplatbRoot.Add(pSplat);

            TimerMan.Add(TimeEvent.Name.SplatRemoveAlien, new SplatRemoveEvent(this.pSplat), 0.5f);
        }

        private bool privCheckParent(GameObject pObj)
        {
            GameObject pGameObj = (GameObject)Iterator.GetChild(pObj);
            if (pGameObj == null)
            {
                return true;
            }

            return false;
        }

        // data
        private SpriteBatch pSB_Aliens;
        private SpriteBatch pSB_Boxes;
        private AlienCategory pAlien;
        private SplatCategory pSplat;
        private SceneState scenePlay;
    }
}
