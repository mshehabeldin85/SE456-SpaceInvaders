using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class GameObject : Component
    { 
        public enum Name
        {
            SquidAlien,
            CrabAlien,
            OctopusAlien,

            AlienGrid,
            AlienColumn,

            WallGroup,
            WallRight,
            WallLeft,
            WallTop,
            WallBottom,

            Missile,
            MissileGroup,

            UFO,
            UFORoot,

            ShieldRoot,
            ShieldColumn_0,
            ShieldColumn_1,
            ShieldColumn_2,
            ShieldColumn_3,
            ShieldColumn_4,
            ShieldColumn_5,
            ShieldColumn_6,
            ShieldBrick,

            Ship,
            ShipRoot,

            Bomb,
            BombRoot,

            Splat,
            SplatRoot,

            NoScoreGroup,

            NullObject,
            Uninitialized
        }

        protected GameObject()
        {

        }

        protected GameObject(GameObject.Name gameName)
        {
            this.name = gameName;
            this.x = 0.0f;
            this.y = 0.0f;
            this.pProxySprite = null;
            this.onScreen = true;
        }

        protected GameObject(GameObject.Name gameName, GameSprite.Name spriteName)
        {
            this.name = gameName;
            this.x = 0.0f;
            this.y = 0.0f;
            this.bMarkForDeath = false;
            this.pProxySprite = ProxySpriteMan.Add(spriteName);

            this.poColObj = new ColObject(this.pProxySprite);
            Debug.Assert(this.poColObj != null);
        }

        protected void BaseUpdateBoundingBox(Component pStart)
        {
            GameObject pNode = (GameObject)pStart;

            // point to ColTotal
            ColRect ColTotal = this.poColObj.poColRect;

            // Get the first child
            pNode = (GameObject)Iterator.GetChild(pNode);

            if (pNode != null)
            {
                // Initialized the union to the first block
                ColTotal.Set(pNode.poColObj.poColRect);

                // loop through sliblings
                while (pNode != null)
                {
                    ColTotal.Union(pNode.poColObj.poColRect);

                    // go to next sibling
                    pNode = (GameObject)Iterator.GetSibling(pNode);
                }

                this.x = this.poColObj.poColRect.x;
                this.y = this.poColObj.poColRect.y;
            }
        }

        public virtual void Remove()
        {
            // -----------------------------------------------------------------
            // Very difficult at first... if you are messy, you will pay here!
            // Given a game object....
            // -----------------------------------------------------------------

            // Remove from SpriteBatch

            // Find the SpriteNode
            Debug.Assert(this.pProxySprite != null);
            SpriteNode pSpriteNode = this.pProxySprite.GetSpriteNode();

            // Remove it from the manager
            Debug.Assert(pSpriteNode != null);
            SpriteBatchMan.Remove(pSpriteNode);

            // Remove collision sprite from spriteBatch

            Debug.Assert(this.poColObj != null);
            Debug.Assert(this.poColObj.pColSprite != null);
            pSpriteNode = this.poColObj.pColSprite.GetSpriteNode();

            Debug.Assert(pSpriteNode != null);
            SpriteBatchMan.Remove(pSpriteNode);

            // Remove from GameObjectMan

            GameObjectMan.Remove(this);

            //GhostMan.Add(this);
        }

        public virtual void Update()
        {
            Debug.Assert(this.pProxySprite != null);
            this.pProxySprite.x = this.x;
            this.pProxySprite.y = this.y;

            Debug.Assert(this.poColObj != null);
            this.poColObj.UpdatePos(this.x, this.y);
            Debug.Assert(this.poColObj.pColSprite != null);
            this.poColObj.pColSprite.Update();
        }

        public void ActivateCollisionSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            Debug.Assert(this.poColObj != null);
            pSpriteBatch.Attach(this.poColObj.pColSprite);
        }

        public void ActivateGameSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            pSpriteBatch.Attach(this.pProxySprite);
        }

        public void SetCollisionColor(float red, float green, float blue)
        {
            Debug.Assert(this.poColObj != null);
            Debug.Assert(this.poColObj.pColSprite != null);

            this.poColObj.pColSprite.SetLineColor(red, green, blue);
        }

        public void Dump()
        {
            // Data:
            Debug.WriteLine("\t\t\t       name: {0} ({1})", this.name, this.GetHashCode());

            if (this.pProxySprite != null)
            {
                Debug.WriteLine("\t\t   pProxySprite: {0}", this.pProxySprite.name);
                Debug.WriteLine("\t\t    pRealSprite: {0}", this.pProxySprite.pSprite.GetName());
            }
            else
            {
                Debug.WriteLine("\t\t   pProxySprite: null");
                Debug.WriteLine("\t\t    pRealSprite: null");
            }
            Debug.WriteLine("\t\t\t      (x,y): {0}, {1}", this.x, this.y);

        }

        public GameObject.Name GetName()
        {
            return this.name;
        }

        // Data: ---------------
        public GameObject.Name name;

        public float x;
        public float y;

        public bool bMarkForDeath;
            
        public ProxySprite pProxySprite;
        public ColObject poColObj;

        public bool onScreen;

    }
}
