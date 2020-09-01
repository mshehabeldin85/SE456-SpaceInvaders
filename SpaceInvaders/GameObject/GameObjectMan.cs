using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //---------------------------------------------------------------------------------------------------------
    // Design Notes:
    //---------------------------------------------------------------------------------------------------------
    public class GameObjectMan : Manager
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        public GameObjectMan(int reserveNum = 3, int reserveGrow = 1)
            : base()
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(reserveNum, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new GameObjectNode();
            this.poNullGameObject = new NullGameObject();

            this.poNodeCompare.pGameObj = this.poNullGameObject; ;
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
        //        pInstance = new GameObjectMan(reserveNum, reserveGrow);
        //    }
        //}

        public static void Create()
        {
            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new GameObjectMan();
            }
        }

        public static void Destroy()
        {
            //GameObjectMan pMan = GameObjectMan.privGetInstance();
            GameObjectMan pMan = GameObjectMan.pActiveGOMan;
            Debug.Assert(pMan != null);

            // Print stats on destroy
            pMan.baseDump();

            // Invalidate the singleton
            if (pInstance != null)
            {
                pInstance = null;
            }
        }

        public static GameObjectNode Attach(GameObject pGameObject)
        {
            //GameObjectMan pMan = GameObjectMan.privGetInstance();
            GameObjectMan pMan = GameObjectMan.pActiveGOMan;
            Debug.Assert(pMan != null);

            GameObjectNode pNode = (GameObjectNode)pMan.baseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(pGameObject);
            return pNode;
        }

        public static GameObject Find(GameObject.Name name)
        {
            //GameObjectMan pMan = GameObjectMan.privGetInstance();
            GameObjectMan pMan = GameObjectMan.pActiveGOMan;
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes
            pMan.poNodeCompare.pGameObj.name = name;

            GameObjectNode pNode = (GameObjectNode)pMan.baseFind(pMan.poNodeCompare);
            Debug.Assert(pNode != null);

            return pNode.pGameObj;
        }

        public static void Remove(GameObjectNode pNode)
        {
            //GameObjectMan pMan = GameObjectMan.privGetInstance();
            GameObjectMan pMan = GameObjectMan.pActiveGOMan;
            Debug.Assert(pMan != null);

            Debug.Assert(pNode != null);
            pMan.baseRemove(pNode);
        }

        public static void Remove(GameObject pNode)
        {
            // Keenan(delete.E)
            Debug.Assert(pNode != null);
            //GameObjectMan pMan = GameObjectMan.privGetInstance();
            GameObjectMan pMan = GameObjectMan.pActiveGOMan;

            GameObject pSafetyNode = pNode;

            // OK so we have a linked list of trees (Remember that)

            // 1) find the tree root (we already know its the most parent)

            GameObject pTmp = pNode;
            GameObject pRoot = null;
            while (pTmp != null)
            {
                pRoot = pTmp;
                pTmp = (GameObject)Iterator.GetParent(pTmp);
            }

            // 2) pRoot is the tree we are looking for
            // now walk the active list looking for pRoot

            GameObjectNode pTree = (GameObjectNode)pMan.baseGetActive();

            while (pTree != null)
            {
                if (pTree.pGameObj == pRoot)
                {
                    // found it
                    break;
                }
                // Goto Next tree
                pTree = (GameObjectNode)pTree.pNext;
            }

            // 3) pTree is the tree that holds pNode
            //  Now remove the node from that tree
            //Debug.Assert(pTree != null);
            //Debug.Assert(pTree.pGameObj != null);

            // Is pTree.poGameObj same as the node we are trying to delete?
            // Answer: should be no... since we always have a group (that was a good idea)
            //Debug.Assert(pTree.pGameObj != pNode);

            GameObject pParent = (GameObject)Iterator.GetParent(pNode);
            Debug.Assert(pParent != null);

            GameObject pChild = (GameObject)Iterator.GetChild(pNode);
            Debug.Assert(pChild == null);

            // remove the node
            pParent.Remove(pNode);

            // TODO - Recycle pNode

        }

        public static void Update()
        {
            //GameObjectMan pMan = GameObjectMan.privGetInstance();
            GameObjectMan pMan = GameObjectMan.pActiveGOMan;
            Debug.Assert(pMan != null);

            GameObjectNode pGameObjectNode = (GameObjectNode)pMan.baseGetActive();

            while (pGameObjectNode != null)
            {
                ReverseIterator pRev = new ReverseIterator(pGameObjectNode.pGameObj);

                Component pNode = pRev.First();
                while (!pRev.IsDone())
                {
                    GameObject pGameObj = (GameObject)pNode;

                    //Debug.WriteLine("update: {0} ({1})", pGameObj, pGameObj.GetHashCode());
                    pGameObj.Update();

                    pNode = pRev.Next();
                }

                pGameObjectNode = (GameObjectNode)pGameObjectNode.pNext;
            }
        }

        public static void Dump()
        {
            //GameObjectMan pMan = GameObjectMan.privGetInstance();
            GameObjectMan pMan = GameObjectMan.pActiveGOMan;
            Debug.Assert(pMan != null);

            pMan.baseDump();
        }

        public static void SetActive(GameObjectMan pGOMan)
        {
            GameObjectMan pMan = GameObjectMan.privGetInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pGOMan != null);
            GameObjectMan.pActiveGOMan = pGOMan;
        }

        public static GameObjectMan GetActive()
        {
            return pActiveGOMan;
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected DLink derivedCreateNode()
        {
            DLink pNode = new GameObjectNode();
            Debug.Assert(pNode != null);

            return pNode;
        }

        override protected Boolean derivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            GameObjectNode pDataA = (GameObjectNode)pLinkA;
            GameObjectNode pDataB = (GameObjectNode)pLinkB;

            Boolean status = false;

            if (pDataA.pGameObj.GetName() == pDataB.pGameObj.GetName())
            {
                status = true;
            }

            return status;
        }

        override protected void derivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            GameObjectNode pNode = (GameObjectNode)pLink;
            pNode.Wash();
        }

        override protected void derivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            GameObjectNode pData = (GameObjectNode)pLink;
            //pData.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static GameObjectMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Data - unique data for this manager 
        //----------------------------------------------------------------------
        private static GameObjectMan pInstance = null;
        private GameObjectNode poNodeCompare;
        private readonly NullGameObject poNullGameObject;
        private static GameObjectMan pActiveGOMan = null;
    }
}
