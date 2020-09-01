using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SndShipHitObserver : ColObserver
    {
        public SndShipHitObserver()
        {
        }

        public override void Notify()
        {
            this.pSoundEngine = SoundEngineMan.Find(SoundEngine.Name.Explosion);
            Debug.Assert(pSoundEngine != null);

            this.pSoundEngine.PlaySound();
        }

        // Data
        private SoundEngine pSoundEngine;
    }
}
