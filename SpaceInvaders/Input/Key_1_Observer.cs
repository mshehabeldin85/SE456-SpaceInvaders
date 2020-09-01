using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class Key_1_Observer : InputObserver
    {
        public override void Notify()
        {
            SceneContext pSceneContext = SceneContext.GetInstance();
            Debug.Assert(pSceneContext != null);

            SceneState sceneState = pSceneContext.GetState();

            if (pSceneContext.poScenePlayer1.numLives > 1)
            {
                pSceneContext.SetState(SceneContext.Scene.Player1);
            }
        }
    }
}
