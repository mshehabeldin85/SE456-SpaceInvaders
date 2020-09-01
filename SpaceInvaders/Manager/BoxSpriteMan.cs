using System.Diagnostics;

namespace SpaceInvaders
{
    public class BoxSpriteMan : Manager
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private BoxSpriteMan(int reserveNum = 3, int reserveGrow = 1)
        : base()
        {
            this.BaseInitialize(reserveNum, reserveGrow);

            this.poNodeCompare = new BoxSprite();
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new BoxSpriteMan(reserveNum, reserveGrow);
            }
        }

        public static void Destroy()
        {
            BoxSpriteMan pMan = BoxSpriteMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Print stats on destroy
            pMan.baseDump();

            // Invalidate the singleton
            if (pInstance != null)
            {
                pInstance = null;
            }
        }

        public static BoxSprite Add(BoxSprite.Name name, float x, float y, float width, float height, Azul.Color pColor = null)
        {
            BoxSpriteMan pMan = BoxSpriteMan.privGetInstance();
            Debug.Assert(pMan != null);

            BoxSprite pNode = (BoxSprite)pMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name, x, y, width, height, pColor);

            return pNode;
        }

        public static BoxSprite Find(BoxSprite.Name name)
        {
            BoxSpriteMan pMan = BoxSpriteMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.poNodeCompare.SetName(name);

            BoxSprite pData = (BoxSprite)pMan.baseFind(pMan.poNodeCompare);
            return pData;
        }

        public static void Remove(BoxSprite pNode)
        {
            BoxSpriteMan pMan = BoxSpriteMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.baseRemove(pNode);
        }

        public static void Dump()
        {
            BoxSpriteMan pMan = BoxSpriteMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink derivedCreateNode()
        {
            DLink pNode = new BoxSprite();
            Debug.Assert(pNode != null);

            return pNode;
        }

        override protected bool derivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            BoxSprite pDataA = (BoxSprite)pLinkA;
            BoxSprite pDataB = (BoxSprite)pLinkB;

            bool status = false;

            if (pDataA.name == pDataB.name)
            {
                status = true;
            }

            return status;
        }

        override protected void derivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            BoxSprite pNode = (BoxSprite)pLink;
            pNode.Wash();
        }

        override protected void derivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            BoxSprite pData = (BoxSprite)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static BoxSpriteMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        private static BoxSpriteMan pInstance = null;
        private readonly BoxSprite poNodeCompare;
    }
}
