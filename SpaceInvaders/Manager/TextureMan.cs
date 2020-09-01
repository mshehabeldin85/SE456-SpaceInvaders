using System.Diagnostics;

namespace SpaceInvaders
{
    class TextureMan : Manager
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public TextureMan(int reserveNum = 3, int reserveGrow = 1)
            : base()   // <--- Kick the can (delegate)
        {
            // Initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            // Initialize derived data here
            this.poNodeCompare = new Texture();
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create(int reserveNum = 1, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new TextureMan(reserveNum, reserveGrow);
            }

            // NullObject texture
            Texture pTexture = TextureMan.Add(Texture.Name.NullObject, "HotPink.tga");
            Debug.Assert(pTexture != null);

            // Default texture
            TextureMan.Add(Texture.Name.Default, "HotPink.tga");
            Debug.Assert(pTexture != null);
        }

        public static void Destroy()
        {
            TextureMan pMan = TextureMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Print stats on destroy
            pMan.baseDump();

            // Invalidate the singleton
            if (pInstance != null)
            {
                pInstance = null;
            }
        }

        public static Texture Add(Texture.Name name, string pTextureName)
        {
            TextureMan pMan = TextureMan.privGetInstance();
            Debug.Assert(pMan != null);

            Texture pNode = (Texture)pMan.baseAdd();
            Debug.Assert(pNode != null);

            // Check the data initialization
            Debug.Assert(pTextureName != null);

            pNode.Set(name, pTextureName);
            return pNode;
        }

        public static Texture Find(Texture.Name name)
        {
            TextureMan pMan = TextureMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.poNodeCompare.name = name;

            Texture pData = (Texture)pMan.baseFind(pMan.poNodeCompare);
            return pData;
        }

        public static void Remove(Texture pNode)
        {
            TextureMan pMan = TextureMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.baseRemove(pNode);
        }

        public static void Dump()
        {
            TextureMan pMan = TextureMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink derivedCreateNode()
        {
            DLink pNode = new Texture();
            Debug.Assert(pNode != null);

            return pNode;
        }

        override protected bool derivedCompare(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            Texture pDataA = (Texture)pLinkA;
            Texture pDataB = (Texture)pLinkB;

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
            Texture pNode = (Texture)pLink;
            pNode.Wash();
        }

        override protected void derivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Texture pData = (Texture)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static TextureMan privGetInstance()
        {
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Data: unique data for this manager 
        //----------------------------------------------------------------------
        private static TextureMan pInstance = null;
        private readonly Texture poNodeCompare;
    }

}

// End of Files

