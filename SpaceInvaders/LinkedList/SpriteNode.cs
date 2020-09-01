using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SpriteNode_Abstract : DLink
    {
    }

    public class SpriteNode : SpriteNode_Abstract
    {
        public SpriteNode()
        : base()
        {
            this.pSpriteBase = null;
        }

        public void Set(SpriteBase pNode, SpriteNodeMan _pSpriteNodeMan)
        {
            Debug.Assert(pNode != null);
            this.pSpriteBase = pNode;

            // Set the back pointer
            // Allows easier deletion in the future
            Debug.Assert(pSpriteBase != null);
            this.pSpriteBase.SetSpriteNode(this);

            Debug.Assert(_pSpriteNodeMan != null);
            this.pBackSpriteNodeMan = _pSpriteNodeMan;
        }

        public SpriteBase GetSpriteBase()
        {
            return this.pSpriteBase;
        }

        public SpriteNodeMan GetSBNodeMan()
        {
            Debug.Assert(this.pBackSpriteNodeMan != null);
            return this.pBackSpriteNodeMan;
        }
        public SpriteBatch GetSpriteBatch()
        {
            Debug.Assert(this.pBackSpriteNodeMan != null);
            return this.pBackSpriteNodeMan.GetSpriteBatch();
        }

        public void Wash()
        {
            this.pSpriteBase = null;
        }

        public void Dump()
        {
            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.pSpriteBase.GetName(), this.GetHashCode());

            if (this.pNext == null)
            {
                Debug.WriteLine("              next: null");
            }
            else
            {
                SpriteNode pTmp = (SpriteNode)this.pNext;
                Debug.WriteLine("              next: {0} ({1})", pTmp.pSpriteBase.GetName(), pTmp.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("              prev: null");
            }
            else
            {
                SpriteNode pTmp = (SpriteNode)this.pPrev;
                Debug.WriteLine("              prev: {0} ({1})", pTmp.pSpriteBase.GetName(), pTmp.GetHashCode());
            }
        }

        // Data: ----------------------------------------------
        public SpriteBase pSpriteBase;
        private SpriteNodeMan pBackSpriteNodeMan;
    }
}
