using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SndAlienHitObserver : ColObserver
    {
        public SndAlienHitObserver()
        {
        }

        public override void Notify()
        {
            this.pSoundEngine = SoundEngineMan.Find(SoundEngine.Name.Invaderkilled);
            Debug.Assert(pSoundEngine != null);

            this.pSoundEngine.PlaySound();
        }

        // Data
        private SoundEngine pSoundEngine;
    }
}
