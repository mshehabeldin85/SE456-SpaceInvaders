using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    //---------------------------------------------------------------------------------------------------------
    // Design Notes:
    //---------------------------------------------------------------------------------------------------------

    public class ProxySprite : SpriteBase
    {
        //---------------------------------------------------------------------------------------------------------
        // Enum
        //---------------------------------------------------------------------------------------------------------
        public enum Name
        {
            Proxy,
            Uninitialized
        }

        //---------------------------------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------------------------------

        // Create a single sprite and all dynamic objects ONCE and ONLY ONCE
        public ProxySprite()
            : base()
        {
            this.name = ProxySprite.Name.Uninitialized;

            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;

            this.pSprite = null;
        }

        public ProxySprite(GameSprite.Name name)
        {
            this.name = ProxySprite.Name.Proxy;

            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;

            this.pSprite = GameSpriteMan.Find(name);
            Debug.Assert(this.pSprite != null);
        }

        //---------------------------------------------------------------------------------------------------------
        // Public Methods
        //---------------------------------------------------------------------------------------------------------

        public void Set(GameSprite.Name name)
        {
            this.name = ProxySprite.Name.Proxy;

            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;

            this.pSprite = GameSpriteMan.Find(name);
            Debug.Assert(this.pSprite != null);
        }

        public new void Clear()   // the "new" is there to shut up warning - overriding at derived class
        {
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.name = Name.Uninitialized;
            this.pSprite = null;
        }

        public void Wash()
        {
            // Wash - clear the entire hierarchy
            base.Clear();
            this.Clear();
        }

        public void SetName(Name inName)
        {
            this.name = inName;
        }

        public void Dump()
        {

            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("             (x,y): {0},{1}", this.x, this.y);


            if (this.pNext == null)
            {
                Debug.WriteLine("              next: null");
            }
            else
            {
                GameSprite pTmp = (GameSprite)this.pNext;
                Debug.WriteLine("              next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("              prev: null");
            }
            else
            {
                GameSprite pTmp = (GameSprite)this.pPrev;
                Debug.WriteLine("              prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }

        //---------------------------------------------------------------------------------------------------------
        // Private Methods
        //---------------------------------------------------------------------------------------------------------
        private void PrivPushToReal()
        {
            // push the data from proxy to Real GameSprite
            Debug.Assert(this.pSprite != null);

            this.pSprite.x = this.x;
            this.pSprite.y = this.y;
            this.pSprite.sx = this.sx;
            this.pSprite.sy = this.sy;
        }

        //---------------------------------------------------------------------------------------------------------
        // Override Abstract methods
        //---------------------------------------------------------------------------------------------------------
        public override void Update()
        {
            // push the data from proxy to Real GameSprite
            this.PrivPushToReal();
            this.pSprite.Update();
        }

        public override void Render()
        {
            // move the values over to Real GameSprite
            this.PrivPushToReal();

            // update and draw real sprite 
            // Seems redundant - Real Sprite might be stale
            this.pSprite.Update();
            this.pSprite.Render();
        }

        public override string GetName()
        {
            return this.name.ToString();
        }

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------

        public ProxySprite.Name name;
        public float x;
        public float y;
        public float sx;
        public float sy;
        public GameSprite pSprite;
    }
}
