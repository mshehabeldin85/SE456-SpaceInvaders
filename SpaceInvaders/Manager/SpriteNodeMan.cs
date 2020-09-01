﻿using System.Diagnostics;

namespace SpaceInvaders
{

    abstract public class SpriteNodeMan_Abstract : Manager
    {
        public SpriteNode_Abstract poActive;
        public SpriteNode_Abstract poReserve;
    }

    public class SpriteNodeMan : SpriteNodeMan_Abstract
    {

        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public SpriteNodeMan(int reserveNum = 3, int reserveGrow = 1)
        : base() // <--- Kick the can (delegate)
        {
            // At this point SBMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new SpriteNode();
        }

        //----------------------------------------------------------------------
        // Public Methods
        //----------------------------------------------------------------------
        public void Set(SpriteBatch.Name name, int reserveNum, int reserveGrow)
        {
            this.name = name;

            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            this.baseSetReserve(reserveNum, reserveGrow);
        }

        public SpriteNode Attach(SpriteBase pNode)
        {
            // Go to Man, get a node from reserve, add to active, return it
            SpriteNode pSBNode = (SpriteNode)this.baseAdd();
            Debug.Assert(pSBNode != null);

            // Initialize SpriteBatchNode
            pSBNode.Set(pNode, this);

            return pSBNode;
        }

        public void Draw()
        {
            // walk through the list and render
            SpriteNode pNode = (SpriteNode)this.baseGetActive();

            while (pNode != null)
            {
                // Assumes someone before here called update() on each sprite
                // Draw me.
                pNode.pSpriteBase.Render();

                pNode = (SpriteNode)pNode.pNext;
            }
        }

        public void Remove(SpriteNode pNode)
        {
            Debug.Assert(pNode != null);
            this.baseRemove(pNode);
        }

        public void Dump()
        {
            this.baseDump();
        }

        public SpriteBatch GetSpriteBatch()
        {
            return this.pBackSpriteBatch;
        }

        public void SetSpriteBatch(SpriteBatch _pSpriteBatch)
        {
            this.pBackSpriteBatch = _pSpriteBatch;
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink derivedCreateNode()
        {
            DLink pNode = new SpriteNode();
            Debug.Assert(pNode != null);

            return pNode;
        }

        override protected bool derivedCompare(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            SpriteNode pDataA = (SpriteNode)pLinkA;
            SpriteNode pDataB = (SpriteNode)pLinkB;

            bool status = false;

            if (pLinkB == pLinkA)
            {
                status = false;
            }
            else
            {
                status = false;
            }

            return status;
        }

        override protected void derivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            SpriteNode pNode = (SpriteNode)pLink;
            pNode.Wash();
        }

        override protected void derivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            SpriteNode pData = (SpriteNode)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        private SpriteBatch.Name name;
        private readonly SpriteNode poNodeCompare;
        private SpriteBatch pBackSpriteBatch;
    }
}

// End of File
