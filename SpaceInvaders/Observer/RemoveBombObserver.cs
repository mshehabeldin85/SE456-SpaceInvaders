using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveBombObserver : ColObserver
    {
        public RemoveBombObserver(SceneState scenePlay)
        {
            this.pBomb = null;

            this.scenePlay = scenePlay;
        }
        public RemoveBombObserver(RemoveBombObserver b)
        {
            this.pBomb = b.pBomb;

            this.scenePlay = b.scenePlay;
        }
        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("RemoveBombObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);

            this.pBomb = (Bomb)this.pSubject.pObjA;
            Debug.Assert(this.pBomb != null);

            if (pBomb.bMarkForDeath == false)
            {
                pBomb.bMarkForDeath = true;
                //   Delay
                RemoveBombObserver pObserver = new RemoveBombObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            //if (scenePlay.numLives > 1)
            //{
                // Let the gameObject deal with this... 
                this.pBomb.Remove();
            //}
        }

        // --------------------------------------
        // data:
        // --------------------------------------

        private GameObject pBomb;
        private SceneState scenePlay;
    }
}

// End of File
