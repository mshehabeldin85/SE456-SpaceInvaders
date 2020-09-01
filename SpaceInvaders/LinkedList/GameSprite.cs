using System.Diagnostics;

namespace SpaceInvaders
{
    public class GameSprite : SpriteBase
    {
        public enum Name
        {
            // ----- Hot Pink (Default) -----
            Default,

            // ----- Aliens -----
            SquidOpen,
            SquidClosed,
            CrabOpen,
            CrabClosed,
            OctopusOpen,
            OctopusClosed,

            // ----- UFO -----
            UFO,
            UFO_Destroy,

            // --- BoxSprites ---
            WhiteBox,
            AnimatedColorBox,

            // ----- Missile -----
            Missile,

            // ----- Splats -----
            SplatBomb,
            SplatAlien,
            SplatMissile,
            SplatShip,
            SplatUFO,

            // ----- Bombs -----
            BombDragger,
            BombZigZag,
            BombStraight,
            BombRolling,

            // ----- Shield Bricks -----
            Brick,
            Brick_LeftTop0,
            Brick_LeftTop1,
            Brick_LeftBottom,
            Brick_RightTop0,
            Brick_RightTop1,
            Brick_RightBottom,

            // ----- Ship -----
            Ship,

            // ----- Wall -----
            Wall,

            NullObject,
            Uninitialized
        }

        //----------------------------------------------------------------------
        // Constructors
        //----------------------------------------------------------------------
        public GameSprite()
            : base()
        {
            this.privClear();

            this.name = GameSprite.Name.Uninitialized;

            // Use the default - it will be replaced in the Set
            this.pImage = ImageMan.Find(Image.Name.Default);
            Debug.Assert(this.pImage != null);

            this.poScreenRect = new Azul.Rect();
            Debug.Assert(this.poScreenRect != null);
            this.poScreenRect.Clear();

            this.poAzulColor = new Azul.Color(1, 1, 1);
            Debug.Assert(this.poAzulColor != null);

            // here is the actual new
            this.poAzulSprite = new Azul.Sprite(pImage.GetAzulTexture(), pImage.GetAzulRect(), this.poScreenRect, psTmpColor);
            Debug.Assert(this.poAzulSprite != null);

            this.x = poAzulSprite.x;
            this.y = poAzulSprite.y;
            this.sx = poAzulSprite.sx;
            this.sy = poAzulSprite.sy;
            this.angle = poAzulSprite.angle;
        }

        //----------------------------------------------------------------------
        // Public Methods
        //----------------------------------------------------------------------
        public void Set(GameSprite.Name name, Image pImage, float x, float y, float width, float height, Azul.Color pColor)
        {
            Debug.Assert(pImage != null);
            Debug.Assert(this.poScreenRect != null);
            Debug.Assert(this.poAzulSprite != null);

            this.poScreenRect.Set(x, y, width, height);
            this.pImage = pImage;
            this.name = name;

            if (pColor == null)
            {
                GameSprite.psTmpColor.Set(1, 1, 1);
                this.poAzulSprite.Swap(pImage.GetAzulTexture(), pImage.GetAzulRect(), this.poScreenRect, psTmpColor);
            }
            else
            {
                this.poAzulSprite.Swap(pImage.GetAzulTexture(), pImage.GetAzulRect(), this.poScreenRect, pColor);
            }

            this.x = poAzulSprite.x;
            this.y = poAzulSprite.y;
            this.sx = poAzulSprite.sx;
            this.sy = poAzulSprite.sy;
            this.angle = poAzulSprite.angle;
        }

        public void Wash()
        {
            this.privClear();
        }

        public void SwapImage(Image pNewImage)
        {
            Debug.Assert(this.poAzulSprite != null);
            Debug.Assert(pNewImage != null);
            this.pImage = pNewImage;

            this.poAzulSprite.SwapTexture(this.pImage.GetAzulTexture());
            this.poAzulSprite.SwapTextureRect(this.pImage.GetAzulRect());
        }

        public Azul.Rect GetScreenRect()
        {
            Debug.Assert(this.poScreenRect != null);
            return this.poScreenRect;
        }

        public void Dump()
        {
            // Dump - Print contents to the debug output window
            //        Using HASH code as its unique identifier 
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("             Image: {0} ({1})", this.pImage.name, this.pImage.GetHashCode());
            Debug.WriteLine("        AzulSprite: ({0})", this.poAzulSprite.GetHashCode());
            Debug.WriteLine("             (x,y): {0},{1}", this.x, this.y);
            Debug.WriteLine("           (sx,sy): {0},{1}", this.sx, this.sy);
            Debug.WriteLine("           (angle): {0}", this.angle);


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

        private void privClear()
        {
            this.pImage = null;
            this.name = GameSprite.Name.Uninitialized;

            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.angle = 0.0f;
        }

        //---------------------------------------------------------------------------------------------------------
        // Override Abstract methods
        //---------------------------------------------------------------------------------------------------------
        public override void Update()
        {
            this.poAzulSprite.x = this.x;
            this.poAzulSprite.y = this.y;
            this.poAzulSprite.sx = this.sx;
            this.poAzulSprite.sy = this.sy;
            this.poAzulSprite.angle = this.angle;

            //this.poAzulSprite.SwapTexture(this.pImage.GetAzulTexture());
            //this.poAzulSprite.SwapTextureRect(this.pImage.GetAzulRect());

            this.poAzulSprite.Update();
        }

        public override void Render()
        {
            this.poAzulSprite.Render();
        }

        public override string GetName()
        {
            return this.name.ToString();
        }

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        public float x;
        public float y;
        public float sx;
        public float sy;
        public float angle;

        public Name name;
        public Image pImage;
        private readonly Azul.Color poAzulColor;
        private Azul.Sprite poAzulSprite;
        private Azul.Rect poScreenRect;

        //---------------------------------------------------------------------------------------------------------
        // Static Data - prevent unecessary "new" in the above methods
        //---------------------------------------------------------------------------------------------------------
        static private Azul.Rect psTmpRect = new Azul.Rect();
        static private Azul.Color psTmpColor = new Azul.Color(1, 1, 1);
    }
}
