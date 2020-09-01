using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SoundEngineMan : Manager
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public SoundEngineMan(int reserveNum = 3, int reserveGrow = 1)
            : base()   // <--- Kick the can (delegate)
        {
            // Initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            // Initialize derived data here
            this.poNodeCompare = new SoundEngine();
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
                pInstance = new SoundEngineMan(reserveNum, reserveGrow);
            }

            // NullObject SoundEngine
            SoundEngine pSoundEngine = SoundEngineMan.Add(SoundEngine.Name.NullObject, "invaderkilled.wav", 0.0f);
            Debug.Assert(pSoundEngine != null);
        }

        public static void Destroy()
        {
            //SoundEngineMan pMan = SoundEngineMan.privGetInstance();
            //Debug.Assert(pMan != null);

            //// Print stats on destroy
            //pMan.baseDump();

            // Invalidate the singleton
            if (pInstance != null)
            {
                pInstance = null;
            }
        }

        public static SoundEngine Add(SoundEngine.Name name, string pSoundName, float soundVolume)
        {
            SoundEngineMan pMan = SoundEngineMan.privGetInstance();
            Debug.Assert(pMan != null);

            SoundEngine pNode = (SoundEngine)pMan.baseAdd();
            Debug.Assert(pNode != null);

            // Check the data initialization
            Debug.Assert(pSoundName != null);

            pNode.Set(name, pSoundName, soundVolume);
            return pNode;
        }

        public static SoundEngine Find(SoundEngine.Name name)
        {
            SoundEngineMan pMan = SoundEngineMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.poNodeCompare.name = name;

            SoundEngine pData = (SoundEngine)pMan.baseFind(pMan.poNodeCompare);
            return pData;
        }

        public static void Remove(SoundEngine pNode)
        {
            SoundEngineMan pMan = SoundEngineMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.baseRemove(pNode);
        }

        public static void Dump()
        {
            SoundEngineMan pMan = SoundEngineMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink derivedCreateNode()
        {
            DLink pNode = new SoundEngine();
            Debug.Assert(pNode != null);

            return pNode;
        }

        override protected bool derivedCompare(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            SoundEngine pDataA = (SoundEngine)pLinkA;
            SoundEngine pDataB = (SoundEngine)pLinkB;

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
            SoundEngine pNode = (SoundEngine)pLink;
            pNode.Wash();
        }

        override protected void derivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            SoundEngine pData = (SoundEngine)pLink;
            pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static SoundEngineMan privGetInstance()
        {
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Data: unique data for this manager 
        //----------------------------------------------------------------------
        private static SoundEngineMan pInstance = null;
        private readonly SoundEngine poNodeCompare;
    }

}

// End of Files

