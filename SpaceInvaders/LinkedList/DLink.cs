
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class DLink
    {

        protected DLink()
        {
            this.Clear();
        }

        public void Clear()
        {
            this.pNext = null;
            this.pPrev = null;
        }

        public static void AddToFront(ref DLink pHead, DLink pNode)
        {
            // add to front
            Debug.Assert(pNode != null);

            // add node
            if (pHead == null)
            {
                // push to the front
                pHead = pNode;
                pNode.pNext = null;
                pNode.pPrev = null;
            }
            else
            {
                // push to front
                pNode.pPrev = null;
                pNode.pNext = pHead;

                // update head
                pHead.pPrev = pNode;
                pHead = pNode;
            }

            // worst case, pHead was null initially, now we added a node so... this is true
            Debug.Assert(pHead != null);
        }

        public static void AddToEnd(ref DLink pHead, DLink pNode)
        {
            if (pHead != null)
            {
                DLink tmpLink = pHead;
                while (tmpLink != null)
                {
                    if (tmpLink.pNext == null)
                    {
                        break;
                    }
                    tmpLink = tmpLink.pNext;
                }

                pNode.pPrev = tmpLink;
                tmpLink.pNext = pNode;
            }
            else
            {
                pHead = pNode;
            }
        }

        public static void AddBefore(ref DLink pHead, ref DLink pCur, DLink pNew)
        {
            if (pCur != null)
            {
                pNew.pPrev = pCur.pPrev;
                pNew.pNext = pCur;
                pCur.pPrev = pNew;

                if (pCur == pHead)
                {
                    pHead = pNew;
                }
                else
                {
                    pNew.pPrev.pNext = pNew;
                }
            }
        }

        public static void AddAfter(ref DLink pCur, DLink pNew)
        {
            if (pCur != null)
            {
                pNew.pNext = pCur.pNext;
                pNew.pPrev = pCur;
                pCur.pNext = pNew;

                if (pNew.pNext != null)
                {
                    pNew.pNext.pPrev = pNew;
                }
            }
        }

        public static DLink PullFromFront(ref DLink pHead)
        {
            // There should always be something on list
            Debug.Assert(pHead != null);

            // return node
            DLink pNode = pHead;

            // Update head
            pHead = pHead.pNext;
            if (pHead != null)
            {
                pHead.pPrev = null;
            }

            // Set prev and next links
            pNode.Clear();

            return pNode;
        }

        public static void RemoveNode(ref DLink pHead, DLink pNode)
        {
            // protection
            Debug.Assert(pHead != null);
            Debug.Assert(pNode != null);

            // 4 different conditions... 
            if (pNode.pPrev != null)
            {	// middle part 1/2
                pNode.pPrev.pNext = pNode.pNext;
            }
            else
            {  // first
                pHead = pNode.pNext;
            }

            if (pNode.pNext != null)
            {	// middle node part 2/2
                pNode.pNext.pPrev = pNode.pPrev;
            }

            // Set prev and next links
            pNode.Clear();
        }

        // Data: -----------------------------
        public DLink pNext;
        public DLink pPrev;

    }
}
