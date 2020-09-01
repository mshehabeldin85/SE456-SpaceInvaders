using System.Diagnostics;

namespace SpaceInvaders
{

    public abstract class Manager
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        protected Manager()
        {
            this.mDeltaGrow = 0;
            this.mNumReserved = 0;
            this.mInitialNumReserved = 0;

            this.mNumActive = 0;
            this.mNumActivePeak = 0;
            this.mTotalNumNodes = 0;
            this.poActive = null;
            this.poReserve = null;
        }

        protected void BaseInitialize(int InitialNumReserved = 3, int DeltaGrow = 1)
        {
            // Check now or pay later
            Debug.Assert(InitialNumReserved >= 0);
            Debug.Assert(DeltaGrow > 0);

            // Squirrel away these
            this.mDeltaGrow = DeltaGrow;
            this.mInitialNumReserved = InitialNumReserved;

            // Preload the reserve
            this.privFillReservedPool(InitialNumReserved);
        }

        //----------------------------------------------------------------------
        // Base methods - called in Derived class but lives in Base
        //----------------------------------------------------------------------
        protected void baseSetReserve(int reserveNum, int reserveGrow)
        {
            this.mDeltaGrow = reserveGrow;

            if (reserveNum > this.mNumReserved)
            {
                // Preload the reserve
                this.privFillReservedPool(reserveNum - this.mNumReserved);
            }
        }

        protected DLink baseAdd()
        {
            // Are there any nodes on the Reserve list?
            if (this.poReserve == null)
            {
                // refill the reserve list by the DeltaGrow
                this.privFillReservedPool(this.mDeltaGrow);
            }

            // Always take from the reserve list
            DLink pLink = Manager.privPullFromFront(ref this.poReserve);
            Debug.Assert(pLink != null);

            // Wash it
            this.derivedWash(pLink);

            // copy to active
            Manager.privAddToFront(ref this.poActive, pLink);

            // Update stats
            this.mNumActive++;
            this.mNumReserved--;
            // --- Track peak number of active nodes
            this.mNumActivePeak++;

            return pLink;
        }

        protected DLink baseAddEnd()
        {
            // Are there any nodes on the Reserve list?
            if (this.poReserve == null)
            {
                // refill the reserve list by the DeltaGrow
                this.privFillReservedPool(this.mDeltaGrow);
            }

            // Always take from the reserve list
            DLink pLink = Manager.privPullFromFront(ref this.poReserve);
            Debug.Assert(pLink != null);

            // Wash it
            this.derivedWash(pLink);

            // copy to active
            Manager.privAddToEnd(ref this.poActive, pLink);

            // Update stats
            this.mNumActive++;
            this.mNumReserved--;
            // --- Track peak number of active nodes
            this.mNumActivePeak++;

            return pLink;
        }

        protected DLink baseAddBefore(ref DLink pCur, DLink pNode)
        {
            // copy to active
            this.privAddBefore(ref pCur, pNode);

            // Update stats
            this.mNumActive++;
            this.mNumReserved--;

            // --- Track peak number of active nodes
            this.mNumActivePeak++;

            return pNode;
        }

        protected DLink baseAddAfter(ref DLink pCur, DLink pNode)
        {
            // copy to active
            Manager.privAddAfter(ref pCur, pNode);

            // Update stats
            this.mNumActive++;
            this.mNumReserved--;

            // --- Track peak number of active nodes
            this.mNumActivePeak++;

            return pNode;
        }

        protected DLink pullNodeFromReserve()
        {
            // Are there any nodes on the Reserve list?
            if (this.poReserve == null)
            {
                // refill the reserve list by the DeltaGrow
                this.privFillReservedPool(this.mDeltaGrow);
            }

            // Always take from the reserve list
            DLink pLink = Manager.privPullFromFront(ref this.poReserve);
            Debug.Assert(pLink != null);

            // Wash it
            this.derivedWash(pLink);

            return pLink;
        }

        protected DLink baseFind(DLink pNodeTarget)
        {
            // search the active list
            DLink pLink = this.poActive;

            // Walk through the nodes
            while (pLink != null)
            {
                if (derivedCompare(pLink, pNodeTarget))
                {
                    // found
                    break;
                }
                pLink = pLink.pNext;
            }

            return pLink;
        }

        protected void baseRemove(DLink pNode)
        {
            Debug.Assert(pNode != null);

            // Don't do the work here... delegate it
            Manager.privRemoveNode(ref this.poActive, pNode);

            // wash it before returning to reserve list
            this.derivedWash(pNode);

            // add it to the return list
            Manager.privAddToFront(ref this.poReserve, pNode);

            // stats update
            this.mNumActive--;
            this.mNumReserved++;
        }

        protected DLink baseGetActive()
        {
            return this.poActive;
        }

        protected void baseDump()
        {
            Debug.WriteLine("");
            Debug.WriteLine("   ****** Manager Begin ****************\n");

            Debug.WriteLine("         mDeltaGrow: {0} ", mDeltaGrow);
            Debug.WriteLine("     mTotalNumNodes: {0} ", mTotalNumNodes);
            Debug.WriteLine("       mNumReserved: {0} ", mNumReserved);
            Debug.WriteLine("         mNumActive: {0} \n", mNumActive);
            Debug.WriteLine("         mNumActivePeak: {0} \n", mNumActivePeak);
            Debug.WriteLine("         mInitialNumReserved: {0} \n", mInitialNumReserved);

            DLink pNode = null;

            if (this.poActive == null)
            {
                Debug.WriteLine("    Active Head: null");
            }
            else
            {
                pNode = this.poActive;
                Debug.WriteLine("    Active Head: ({0})", pNode.GetHashCode());
            }

            if (this.poReserve == null)
            {
                Debug.WriteLine("   Reserve Head: null\n");
            }
            else
            {
                pNode = this.poReserve;
                Debug.WriteLine("   Reserve Head: ({0})\n", pNode.GetHashCode());
            }

            Debug.WriteLine("   ------ Active List: -----------\n");

            pNode = this.poActive;

            int i = 0;
            while (pNode != null)
            {
                Debug.WriteLine("   {0}: -------------", i);
                this.derivedDumpNode(pNode);
                i++;
                pNode = pNode.pNext;
            }

            Debug.WriteLine("");
            Debug.WriteLine("   ------ Reserve List: ----------\n");

            pNode = this.poReserve;
            i = 0;
            while (pNode != null)
            {
                Debug.WriteLine("   {0}: -------------", i);
                this.derivedDumpNode(pNode);
                i++;
                pNode = pNode.pNext;
            }
            Debug.WriteLine("\n   ****** Manager End ******************\n");
        }

        //----------------------------------------------------------------------
        // Abstract methods - the "contract" Derived class must implement
        //----------------------------------------------------------------------
        abstract protected DLink derivedCreateNode();
        abstract protected bool derivedCompare(DLink pLinkA, DLink pLinkB);
        abstract protected void derivedWash(DLink pLink);
        abstract protected void derivedDumpNode(DLink pLink);

        //----------------------------------------------------------------------
        // Private methods - helpers
        //----------------------------------------------------------------------
        private void privFillReservedPool(int count)
        {
            // doesn't make sense if its not at least 1
            Debug.Assert(count >= 1);

            this.mTotalNumNodes += count;
            this.mNumReserved += count;

            // Preload the reserve
            for (int i = 0; i < count; i++)
            {
                DLink pNode = this.derivedCreateNode();
                Debug.Assert(pNode != null);

                Manager.privAddToEnd(ref this.poReserve, pNode);
            }
        }

        private static void privAddToFront(ref DLink pHead, DLink pNode)
        {
            // Will work for Active or Reserve List
            Debug.Assert(pNode != null);

            DLink.AddToFront(ref pHead, pNode);
            
            // worst case, pHead was null initially, now we added a node so... this is true
            Debug.Assert(pHead != null);
        }

        private static void privAddToEnd(ref DLink pHead, DLink pNode)
        {
            // Will work for Active or Reserve List
            Debug.Assert(pNode != null);

            DLink.AddToEnd(ref pHead, pNode);

            // worst case, pHead was null initially, now we added a node so... this is true
            Debug.Assert(pHead != null);
        }

        private void privAddBefore(ref DLink pCur, DLink pNode)
        {
            // Will work for Active or Reserve List
            Debug.Assert(pCur != null);
            Debug.Assert(pNode != null);

            DLink.AddBefore(ref this.poActive, ref pCur, pNode);

            // worst case, pCur was null initially, now we added a node so... this is true
            Debug.Assert(pCur != null);
        }

        private static void privAddAfter(ref DLink pCur, DLink pNode)
        {
            // Will work for Active or Reserve List
            Debug.Assert(pCur != null);
            Debug.Assert(pNode != null);

            DLink.AddAfter(ref pCur, pNode);

            // worst case, pCur was null initially, now we added a node so... this is true
            Debug.Assert(pCur != null);
        }

        private static DLink privPullFromFront(ref DLink pHead)
        {
            // There should always be something on list
            Debug.Assert(pHead != null);

            DLink pNode;
            pNode = DLink.PullFromFront(ref pHead);

            Debug.Assert(pNode != null);
            return pNode;
        }

        private static void privRemoveNode(ref DLink pHead, DLink pNode)
        {
            // protection
            Debug.Assert(pHead != null);
            Debug.Assert(pNode != null);

            DLink.RemoveNode(ref pHead, pNode);
        }

        //----------------------------------------------------------------------
        // Data:
        //----------------------------------------------------------------------
        private DLink poActive; 
        private DLink poReserve;
        private int mDeltaGrow;
        private int mTotalNumNodes;
        private int mNumReserved;
        private int mNumActive;
        private int mNumActivePeak;
        private int mInitialNumReserved;

    }
}
