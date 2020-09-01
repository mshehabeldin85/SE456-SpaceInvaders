using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class Key_Q_Observer : InputObserver
    {
        public override void Notify()
        {
            SceneContext pSceneContext = SceneContext.GetInstance();
            Debug.Assert(pSceneContext != null);

            SceneState sceneState = pSceneContext.GetState();

            if (sceneState.name == SceneContext.Scene.Over)
            {
                pSceneContext.SetState(SceneContext.Scene.Select);
            }
        }
    }
}
