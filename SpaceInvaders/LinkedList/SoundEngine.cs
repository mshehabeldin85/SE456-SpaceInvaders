using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SoundEngine : DLink
    {
        public enum Name
        {
            AlienA,
            AlienB,
            AlienC,
            AlienD,
            Shoot,
            Invaderkilled,
            Explosion,
            UFO_HighPitch,
            UFO_LowPitch,  

            NullObject,
            Uninitialized
        }

        //----------------------------------------------------------------------
        // Constructors
        //----------------------------------------------------------------------
        public SoundEngine()
        : base()   // <--- Delegate (kick the can)
        {
            // Class should only initialize variables that it owns
            // Delegate the initialization to other classes
            this.privClear();
        }
        public SoundEngine(Name name, string SoundEngineName, float soundVolume)
            : base()   // <--- base class do your thing
        {
            this.Set(name, SoundEngineName, soundVolume);
        }

        //----------------------------------------------------------------------
        // Methods
        //----------------------------------------------------------------------
        public void Set(Name name, string pSoundName, float soundVolume)
        {
            // Set - Node data  (only Node level)
            this.name = name;

            Debug.Assert(pSoundName != null);

            if (System.IO.File.Exists(pSoundName))
            {
                this.pSoundName = pSoundName;
            }

            this.soundVolume = soundVolume;
        }

        public void PlaySound()
        {
            this.poSoundEngine.SoundVolume = soundVolume;
            this.poSound = poSoundEngine.Play2D(pSoundName);
        }

        public void Wash()
        {
            this.privClear();
        }

        private void privClear()
        {
            this.name = Name.Uninitialized;
            this.poSoundEngine = new IrrKlang.ISoundEngine();
            this.poSound = null;
            this.soundVolume = 0.0f;
        }

        public IrrKlang.ISound GetSound()
        {
            Debug.Assert(this.poSound != null);
            return this.poSound;
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
                SoundEngine pTmp = (SoundEngine)this.pPrev;
                Debug.WriteLine("      prev: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }

            if (this.pNext == null)
            {
                Debug.WriteLine("      next: null");
            }
            else
            {
                SoundEngine pTmp = (SoundEngine)this.pNext;
                Debug.WriteLine("      next: {0} ({1})", pTmp.name, pTmp.GetHashCode());
            }
        }

        public SoundEngine.Name GetName()
        {
            return this.name;
        }

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        public Name name;
        public String pSoundName;
        private float soundVolume;
        public IrrKlang.ISoundEngine poSoundEngine;
        public IrrKlang.ISound poSound;

    }
}
