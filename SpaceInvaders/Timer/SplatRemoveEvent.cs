using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SplatRemoveEvent : Command
    {
        public SplatRemoveEvent(SplatCategory splat)
        {
            this.pSplat = splat;
        }

        override public void Execute(float deltaTime)
        {
            pSplat.Remove();
        }

        private SplatCategory pSplat;
    }
}
