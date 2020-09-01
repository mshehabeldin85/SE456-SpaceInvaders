using System.Diagnostics;

namespace SpaceInvaders
{
    public class Texture : DLink
    {
        public enum Name
        {
            // ----- Hot Pink (Default) -----
            Default,

            // ----- Aliens -----
            Aliens,

            // ----- Shield -----
            Shield,

            // ----- Font -----
            Consolas36pt,
            Consolas48pt,

            NullObject,
            Uninitialized
        }

        //----------------------------------------------------------------------
        // Constructors
        //----------------------------------------------------------------------
        public Texture()
        : base()   // <--- Delegate (kick the can)
        {
            // Class should only initialize variables that it owns
            // Delegate the initialization to other classes
            this.privClear();
        }
        public Texture(Name name, string textureName)
            : base()   // <--- base class do your thing
        {
            this.Set(name, textureName);
        }

        //----------------------------------------------------------------------
        // Methods
        //----------------------------------------------------------------------
        public void Set(Name name, string pTextureName)
        {
            // Set - Node data  (only Node level)
            this.name = name;

            Debug.Assert(pTextureName != null);
            Debug.Assert(this.poAzulTexture != null);

            // Default Texture Swap by the new one
            if (System.IO.File.Exists(pTextureName))
            {
                // Replace the Default with the new one
                this.poAzulTexture = new Azul.Texture(pTextureName, Azul.Texture_Filter.NEAREST, Azul.Texture_Filter.NEAREST);
            }
            Debug.Assert(this.poAzulTexture != null);
        }

        public void Wash()
        {
            this.privClear();
        }

        private void privClear()
        {
            Debug.Assert(Texture.psDefaultAzulTexture != null);

            // Set texture to safety texture Pink
            this.poAzulTexture = psDefaultAzulTexture;
            Debug.Assert(this.poAzulTexture != null);

            this.name = Name.Default;
        }

        public Azul.Texture GetAzulTexture()
        {
            Debug.Assert(this.poAzulTexture != null);
            return this.poAzulTexture;
        }

        public void Dump()
        {
            Debug.WriteLine("   {0} ({1})", this.name, this.GetHashCode());

            if (this.pPrev == null)
            {
                Debug.WriteLine("      prev: null");
            }
            else
            {
                Texture pTmp = (Texture)this.pPrev;
                Debug.WriteLine("      prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.pNext == null)
            {
                Debug.WriteLine("      next: null");
            }
            else
            {
                Texture pTmp = (Texture)this.pNext;
                Debug.WriteLine("      next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }

        public Texture.Name GetName()
        {
            return this.name;
        }

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        public Name name;
        public Azul.Texture poAzulTexture;

        //---------------------------------------------------------------------------------------------------------
        // Static Data
        //---------------------------------------------------------------------------------------------------------
        static private readonly Azul.Texture psDefaultAzulTexture = new Azul.Texture("HotPink.tga");

    }
}
