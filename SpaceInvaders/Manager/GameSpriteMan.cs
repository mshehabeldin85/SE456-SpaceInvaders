using System.Diagnostics;

namespace SpaceInvaders
{
    public class GameSpriteMan : Manager
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public GameSpriteMan(int reserveNum = 3, int reserveGrow = 1)
            : base()
        {
            this.BaseInitialize(reserveNum, reserveGrow);

            this.poNodeCompare = new GameSprite();
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        //public static void Create(int reserveNum = 3, int reserveGrow = 1)
        //{
        //    Debug.Assert(reserveNum > 0);
        //    Debug.Assert(reserveGrow > 0);

        //    Debug.Assert(pInstance == null);

        //    if (pInstance == null)
        //    {
        //        pInstance = new GameSpriteMan(reserveNum, reserveGrow);
        //    }

        //    // Add a NULL Sprite into the Manager, allows find 
        //    GameSprite pGSprite = GameSpriteMan.Add(GameSprite.Name.NullObject, Image.Name.NullObject, 0, 0, 0, 0);
        //    Debug.Assert(pGSprite != null);
        //}

        public static void Create()
        {
            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new GameSpriteMan();
            }
        }

        public static void Destroy()
        {
            //GameSpriteMan pMan = GameSpriteMan.privGetInstance();
            GameSpriteMan pMan = GameSpriteMan.pActiveGSMan;
            Debug.Assert(pMan != null);

            // Print stats on destroy
            pMan.baseDump();

            // Invalidate the singleton
            if (pInstance != null)
            {
                pInstance = null;
            }
        }

        public static GameSprite Add(GameSprite.Name name, Image.Name ImageName, float x, float y, float width, float height, Azul.Color pColor = null)
        {
            //GameSpriteMan pMan = GameSpriteMan.privGetInstance();
            GameSpriteMan pMan = GameSpriteMan.pActiveGSMan;
            Debug.Assert(pMan != null);

            GameSprite pNode = (GameSprite)pMan.baseAdd();
            Debug.Assert(pNode != null);

            // Initialize the data
            Image pImage = ImageMan.Find(ImageName);
            Debug.Assert(pImage != null);

            pNode.Set(name, pImage, x, y, width, height, pColor);

            return pNode;
        }

        public static GameSprite Find(GameSprite.Name name)
        {
            //GameSpriteMan pMan = GameSpriteMan.privGetInstance();
            GameSpriteMan pMan = GameSpriteMan.pActiveGSMan;
            Debug.Assert(pMan != null);

            pMan.poNodeCompare.name = name;

            GameSprite pData = (GameSprite)pMan.baseFind(pMan.poNodeCompare);
            return pData;
        }

        public void Remove(GameSprite pNode)
        {
            //GameSpriteMan pMan = GameSpriteMan.privGetInstance();
            GameSpriteMan pMan = GameSpriteMan.pActiveGSMan;
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.baseRemove(pNode);
        }

        public void Dump()
        {
            //GameSpriteMan pMan = GameSpriteMan.privGetInstance();
            GameSpriteMan pMan = GameSpriteMan.pActiveGSMan;
            Debug.Assert(pMan != null);

            pMan.baseDump();
        }

        public static void SetActive(GameSpriteMan pGSMan)
        {
            GameSpriteMan pMan = GameSpriteMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pGSMan != null);
            GameSpriteMan.pActiveGSMan = pGSMan;

            // Add a NULL Sprite into the Manager, allows find 
            GameSprite pGSprite = GameSpriteMan.Add(GameSprite.Name.NullObject, Image.Name.NullObject, 0, 0, 0, 0);
            Debug.Assert(pGSprite != null);
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink derivedCreateNode()
        {
            DLink pNode = new GameSprite();
            Debug.Assert(pNode != null);

            return pNode;
        }
        override protected bool derivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            GameSprite pDataA = (GameSprite)pLinkA;
            GameSprite pDataB = (GameSprite)pLinkB;

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
            GameSprite pNode = (GameSprite)pLink;
            pNode.Wash();
        }
        override protected void derivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            GameSprite pData = (GameSprite)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static GameSpriteMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Data: unique data for this manager 
        //----------------------------------------------------------------------
        private static GameSpriteMan pInstance = null;
        private readonly GameSprite poNodeCompare;
        private static GameSpriteMan pActiveGSMan = null;
    }

}

// End of Files

