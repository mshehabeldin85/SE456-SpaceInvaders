using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpriteBatch : DLink
    {
        public enum Name
        {
            Aliens,
            AngryBirds,
            PacMan,
            Boxes,
            Shields,
            Texts,

            Uninitialized
        }

        public SpriteBatch()
            : base()
        {
            this.drawSprite = true;
            this.name = SpriteBatch.Name.Uninitialized;
            this.poSpriteNodeMan = new SpriteNodeMan();
            Debug.Assert(this.poSpriteNodeMan != null);
        }

        public void Set(SpriteBatch.Name name, int priority, int reserveNum = 3, int reserveGrow = 1)
        {
            this.name = name;
            this.priority = priority;
            this.poSpriteNodeMan.Set(name, reserveNum, reserveGrow);
        }

        public void Attach(SpriteBase pNode)
        {
            Debug.Assert(pNode != null);

            // Go to Man, get a node from reserve, add to active, return it
            SpriteNode pSpriteNode = (SpriteNode)this.poSpriteNodeMan.Attach(pNode);
            Debug.Assert(pSpriteNode != null);

            // Initialize SpriteBatchNode
            pSpriteNode.Set(pNode, this.poSpriteNodeMan);

            // Back pointer
            this.poSpriteNodeMan.SetSpriteBatch(this);
        }

        public void Wash()
        {
            // TODO: To be implemented later
        }

        public void Dump()
        {
            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("             Priority: {0}", this.priority);


            if (this.pNext == null)
            {
                Debug.WriteLine("              next: null");
            }
            else
            {
                SpriteBatch pTmp = (SpriteBatch)this.pNext;
                Debug.WriteLine("              next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("              prev: null");
            }
            else
            {
                SpriteBatch pTmp = (SpriteBatch)this.pPrev;
                Debug.WriteLine("              prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }

        public void SetName(SpriteBatch.Name inName)
        {
            this.name = inName;
        }

        public SpriteBatch.Name GetName()
        {
            return this.name;
        }

        public SpriteNodeMan GetSBNodeMan()
        {
            return this.poSpriteNodeMan;
        }

        // Data -------------------------------
        public SpriteBatch.Name name;
        public int priority;
        public bool drawSprite;
        private readonly SpriteNodeMan poSpriteNodeMan;

    }
}

// End of File
