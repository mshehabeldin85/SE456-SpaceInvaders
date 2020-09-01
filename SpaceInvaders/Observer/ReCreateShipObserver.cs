using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ReCreateShipObserver : ColObserver
    {
        public ReCreateShipObserver(SceneState scenePlay)
        {
            this.scenePlay = scenePlay;
        }

        public override void Notify()
        {
            if (scenePlay.numLives > 1)
            {
                TimerMan.Add(TimeEvent.Name.RecreateShip, new ReCreateShipEvent(), 0.7f);
            }
            else
            {
                SceneContext sc = SceneContext.GetInstance();
                sc.UpdateScreens();
            }
        }

        // Data
        SceneState scenePlay;
    }
}
