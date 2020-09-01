using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class Key_2_Observer : InputObserver
    {
        public override void Notify()
        {
            SceneContext pSceneContext = SceneContext.GetInstance();
            Debug.Assert(pSceneContext != null);

            SceneState sceneState = pSceneContext.GetState();

            if (pSceneContext.poScenePlayer2.numLives > 1)
            {
                pSceneContext.SetState(SceneContext.Scene.Player2);
            }
        }
    }
}
