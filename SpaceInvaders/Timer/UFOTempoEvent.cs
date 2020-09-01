using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class UFOTempoEvent : Command
    {
        public UFOTempoEvent(GameObject gameObj)
        {
            this.pUFO = gameObj;
        }

        public void Attach(SoundEngine.Name soundName)
        {
            // Get the Sound
            SoundEngine pSound = SoundEngineMan.Find(soundName);
            Debug.Assert(pSound != null);

            // Create a new holder
            SoundHolder pSoundHolder = new SoundHolder(pSound);
            Debug.Assert(pSoundHolder != null);

            // Attach it to the Animation Sprite ( Push to front )
            DLink.AddToEnd(ref this.poFirstSound, pSoundHolder);

            // Set the first one to this Sound
            this.pCurrSound = pSoundHolder;
        }

        public override void Execute(float deltaTime)
        {
            // advance to next Sound 
            SoundHolder pSoundHolder = (SoundHolder)this.pCurrSound.pNext;

            // if at end of list, set to first
            if (pSoundHolder == null)
            {
                pSoundHolder = (SoundHolder)poFirstSound;
            }

            // squirrel away for next timer event
            this.pCurrSound = pSoundHolder;

            // change Sound
            pSoundHolder.pSound.PlaySound();

            if (pUFO.onScreen == true)
            {
                // Add itself back to timer
                TimerMan.Add(TimeEvent.Name.GridSoundTempo, this, deltaTime);
            }
        }

        // Data: ---------------
        private DLink pCurrSound;
        private DLink poFirstSound;
        private GameObject pUFO;
    }
}

// End of File
