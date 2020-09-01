using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BombObserver : ColObserver
    {
        public BombObserver(bool showSplat)
        {
            this.pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            Debug.Assert(this.pSB_Aliens != null);

            this.pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            Debug.Assert(this.pSB_Boxes != null);

            this.showSplat = showSplat;
        }

        public override void Notify()
        {
            Bomb pBomb = null;

            if (this.pSubject.pObjA.name == GameObject.Name.Bomb)
            {
                pBomb = (Bomb)this.pSubject.pObjA;
            }
            else if (this.pSubject.pObjB.name == GameObject.Name.Bomb)
            {
                pBomb = (Bomb)this.pSubject.pObjB;
            }

            Debug.Assert(pBomb != null);

            pBomb.Reset();

            pBomb.Remove();

            if (showSplat == true)
            {
                // TODO: Splat Alien - needs a better way
                this.pSplat = new Splat(GameObject.Name.Splat, GameSprite.Name.SplatBomb, pBomb.x, pBomb.y);
                pSplat.ActivateCollisionSprite(this.pSB_Boxes);
                pSplat.ActivateGameSprite(this.pSB_Aliens);

                GameObject pSplatbRoot = GameObjectMan.Find(GameObject.Name.SplatRoot);
                Debug.Assert(pSplatbRoot != null);
                pSplatbRoot.Add(pSplat);

                TimerMan.Add(TimeEvent.Name.SplatRemoveBomb, new SplatRemoveEvent(this.pSplat), 0.5f);
            }
        }

        // ------------------------------------
        // Data
        // ------------------------------------
        private SpriteBatch pSB_Aliens;
        private SpriteBatch pSB_Boxes;
        private SplatCategory pSplat;
        private bool showSplat;
    }
}

// End of File
