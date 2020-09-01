using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SpriteBatchMan_Abstract : Manager
    {
        public SpriteBatchMan_Abstract poActive;
        public SpriteBatchMan_Abstract poReserve;
    }

    public class SpriteBatchMan : SpriteBatchMan_Abstract
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public SpriteBatchMan(int reserveNum = 3, int reserveGrow = 1)
        : base()
        {
            // At this point SpriteBatchMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            // initialize derived data here
            //this.poNodeCompare = new SpriteBatch();
        }

        private SpriteBatchMan()
            : base() // <--- Kick the can (delegate)
        {
            SpriteBatchMan.pActiveSBMan = null;
            // initialize derived data here
            SpriteBatchMan.poNodeCompare = new SpriteBatch();
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        //public static void Create(int reserveNum = 3, int reserveGrow = 1)
        //{
        //    // make sure values are ressonable 
        //    Debug.Assert(reserveNum > 0);
        //    Debug.Assert(reserveGrow > 0);

        //    // initialize the singleton here
        //    Debug.Assert(pInstance == null);

        //    // Do the initialization
        //    if (pInstance == null)
        //    {
        //        pInstance = new SpriteBatchMan(reserveNum, reserveGrow);
        //    }
        //}

        public static void Create()
        {
            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new SpriteBatchMan();
            }
        }

        public static void Destroy()
        {
            //SpriteBatchMan pMan = SpriteBatchMan.privGetInstance();
            SpriteBatchMan pMan = SpriteBatchMan.pActiveSBMan;
            Debug.Assert(pMan != null);

            // Print stats on destroy
            pMan.baseDump();

            // Invalidate the singleton
            if (pInstance != null)
            {
                pInstance = null;
            }
        }

        public SpriteBatch Add(SpriteBatch.Name name, int priority, int reserveNum = 3, int reserveGrow = 1)
        {
            //SpriteBatchMan pMan = SpriteBatchMan.privGetInstance();
            SpriteBatchMan pMan = SpriteBatchMan.pActiveSBMan;
            Debug.Assert(pMan != null);

            SpriteBatch pNode = SpriteBatchMan.privSortedAdd(priority);

            if (name == SpriteBatch.Name.Boxes)
            {
                pNode.drawSprite = false;
            }

            pNode.Set(name, priority, reserveNum, reserveGrow);

            return pNode;
        }

        public static void Draw()
        {
            //SpriteBatchMan pMan = SpriteBatchMan.privGetInstance();
            SpriteBatchMan pMan = SpriteBatchMan.pActiveSBMan;
            Debug.Assert(pMan != null);

            SpriteBatch pSpriteBatch = (SpriteBatch)pMan.baseGetActive();

            while (pSpriteBatch != null)
            {
                SpriteNodeMan pSBNodeMan = pSpriteBatch.GetSBNodeMan();
                Debug.Assert(pSBNodeMan != null);

                if (pSpriteBatch.drawSprite == true)
                {
                    pSBNodeMan.Draw();
                }

                pSpriteBatch = (SpriteBatch)pSpriteBatch.pNext;
            }
        }

        public static SpriteBatch Find(SpriteBatch.Name name)
        {
            //SpriteBatchMan pMan = SpriteBatchMan.privGetInstance();
            SpriteBatchMan pMan = SpriteBatchMan.pActiveSBMan;
            Debug.Assert(pMan != null);

            SpriteBatchMan.poNodeCompare.SetName(name);

            SpriteBatch pData = (SpriteBatch)pMan.baseFind(SpriteBatchMan.poNodeCompare);
            return pData;
        }

        public static void Remove(SpriteBatch pNode)
        {
            //SpriteBatchMan pMan = SpriteBatchMan.privGetInstance();
            SpriteBatchMan pMan = SpriteBatchMan.pActiveSBMan;
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.baseRemove(pNode);
        }

        public static void ToggleView(SpriteBatch.Name name)
        {
            SpriteBatch pData = SpriteBatchMan.Find(name);
            Debug.Assert(pData != null);

            if (pData.drawSprite == true)
            {
                pData.drawSprite = false;
            }
            else if (pData.drawSprite == false)
            {
                pData.drawSprite = true;
            }
        }

        public static void Remove(SpriteNode pSpriteBatchNode)
        {
            Debug.Assert(pSpriteBatchNode != null);
            SpriteNodeMan pSpriteNodeMan = pSpriteBatchNode.GetSBNodeMan();

            Debug.Assert(pSpriteNodeMan != null);
            pSpriteNodeMan.Remove(pSpriteBatchNode);
        }

        public static void Dump()
        {
            //SpriteBatchMan pMan = SpriteBatchMan.privGetInstance();
            SpriteBatchMan pMan = SpriteBatchMan.pActiveSBMan;

            Debug.Assert(pMan != null);

            pMan.baseDump();
        }

        public SpriteBatch GetSpriteBatch()
        {
            return this.pBackSpriteBatch;
        }
        public void SetSpriteBatch(SpriteBatch _pSpriteBatch)
        {
            this.pBackSpriteBatch = _pSpriteBatch;
        }

        public static void SetActive(SpriteBatchMan pSBMan)
        {
            SpriteBatchMan pMan = SpriteBatchMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pSBMan != null);
            SpriteBatchMan.pActiveSBMan = pSBMan;
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink derivedCreateNode()
        {
            DLink pNode = new SpriteBatch();
            Debug.Assert(pNode != null);

            return pNode;
        }

        override protected Boolean derivedCompare(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            SpriteBatch pDataA = (SpriteBatch)pLinkA;
            SpriteBatch pDataB = (SpriteBatch)pLinkB;

            Boolean status = false;

            if (pDataA.name == pDataB.name)
            {
                status = true;
            }

            return status;
        }

        override protected void derivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            SpriteBatch pNode = (SpriteBatch)pLink;
            pNode.Wash();
        }

        override protected void derivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            SpriteBatch pData = (SpriteBatch)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static SpriteBatchMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        private static SpriteBatch privSortedAdd(int priority)
        {
            DLink pLink;
            SpriteBatch pCur;
            SpriteBatch pTmp;
            SpriteBatch pNode;

            //SpriteBatchMan pMan = SpriteBatchMan.privGetInstance();
            SpriteBatchMan pMan = SpriteBatchMan.pActiveSBMan;

            Debug.Assert(pMan != null);

            SpriteBatch pSpriteBatch = (SpriteBatch)pMan.baseGetActive();

            if (pSpriteBatch == null || pSpriteBatch.priority >= priority)
            {
                pNode = (SpriteBatch)pMan.baseAdd();
                Debug.Assert(pNode != null);
            }
            else
            {
                pNode = (SpriteBatch)pMan.pullNodeFromReserve();
                Debug.Assert(pNode != null);

                pCur = pSpriteBatch;

                while (pCur.pNext != null)
                {
                    pTmp = (SpriteBatch)pCur.pNext;

                    if (pTmp.priority < priority)
                    {
                        pCur = (SpriteBatch)pCur.pNext;
                        break;
                    }
                }

                pLink = (DLink)pCur;
                pNode = (SpriteBatch)pMan.baseAddAfter(ref pLink, pNode);
                Debug.Assert(pNode != null);
            }

            return pNode;
        }

        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        private static SpriteBatchMan pInstance = null;
        private static SpriteBatch poNodeCompare;
        private SpriteBatch pBackSpriteBatch;
        private static SpriteBatchMan pActiveSBMan = null;

    }
}

// End of File
