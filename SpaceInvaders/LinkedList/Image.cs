﻿using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Image_Abstract : DLink
    {
    }

    public class Image : Image_Abstract
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

            // ----- Splats -----
            AlienSplat,
            MissleSplat,
            BombSplat,
            UFOSplat,
            ShipSplat,

            // ----- Bombs -----
            BombDragger,
            BombZigZag,
            BombStraight,
            BombRolling,

            // ----- Shield -----
            Brick,
            BrickLeft_Top0,
            BrickLeft_Top1,
            BrickLeft_Bottom,
            BrickRight_Top0,
            BrickRight_Top1,
            BrickRight_Bottom,

            // ----- Missile -----
            Missile,

            // ----- Ship -----
            Ship,

            NullObject,
            Uninitialized
        }

        //----------------------------------------------------------------------
        // Constructors
        //----------------------------------------------------------------------
        public Image()
        : base()   // <--- Delegate (kick the can)
        {
            this.poRect = new Azul.Rect();
            Debug.Assert(this.poRect != null);

            this.privClear();
        }

        //----------------------------------------------------------------------
        // Methods
        //----------------------------------------------------------------------
        public void Set(Name name, Texture pTexture, float x, float y, float width, float height)
        {
            // Copy the data over
            this.name = name;

            Debug.Assert(pTexture != null);
            this.pTexture = pTexture;

            this.poRect.Set(x, y, width, height);
        }

        private void privClear()
        {
            this.pTexture = null;
            this.name = Name.Uninitialized;
            this.poRect.Clear();
        }

        public void Wash()
        {
            this.privClear();
        }

        public Azul.Rect GetAzulRect()
        {
            Debug.Assert(this.poRect != null);
            return this.poRect;
        }

        public Azul.Texture GetAzulTexture()
        {
            return this.pTexture.GetAzulTexture();
        }

        public void Dump()
        {
            Debug.WriteLine("   Name: {0} ({1})", this.name, this.GetHashCode());
            Debug.WriteLine("      Rect: [{0} {1} {2} {3}] ", this.poRect.x, this.poRect.y, this.poRect.width, this.poRect.height);

            if (this.pTexture != null)
            {
                Debug.WriteLine("   Texture: {0} ", this.pTexture.name);
            }
            else
            {
                Debug.WriteLine("   Texture: null ");
            }


            if (this.pNext == null)
            {
                Debug.WriteLine("      next: null");
            }
            else
            {
                Image pTmp = (Image)this.pNext;
                Debug.WriteLine("      next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.pPrev == null)
            {
                Debug.WriteLine("      prev: null");
            }
            else
            {
                Image pTmp = (Image)this.pPrev;
                Debug.WriteLine("      prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        public Name name;
        private readonly Azul.Rect poRect;
        private Texture pTexture;

    }
}
