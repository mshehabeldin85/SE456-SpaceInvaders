using System;

namespace SpaceInvaders
{
    class SoundHolder : DLink
    {
        public SoundHolder(SoundEngine sound)
            : base()
        {
            this.pSound = sound;
        }

        // Data: ---------------
        public SoundEngine pSound;
    }
}

// End of File
