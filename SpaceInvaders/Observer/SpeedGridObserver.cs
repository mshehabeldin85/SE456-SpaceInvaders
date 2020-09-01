using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpeedGridObserver : ColObserver
    {
        public SpeedGridObserver()
        {
        }

        public override void Notify()
        {
            TimeEvent pMoveGrid = TimerMan.Find(TimeEvent.Name.MoveGrid);
            pMoveGrid.deltaTime -= 0.009f;

            TimeEvent pSwapAliens = TimerMan.Find(TimeEvent.Name.SwapAliens);
            pSwapAliens.deltaTime -= 0.009f;

            TimeEvent pGridSoundTempo = TimerMan.Find(TimeEvent.Name.GridSoundTempo);
            pGridSoundTempo.deltaTime -= 0.009f;
        }
    }
}
