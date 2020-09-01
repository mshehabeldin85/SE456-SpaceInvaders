using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SceneContext
    {
        public enum Scene
        {
            Select,
            Player1,
            Player2,
            Over
        }

        public SceneContext()
        {
            // reserve the states
            this.poSceneSelect = new SceneSelect();
            this.poScenePlayer1 = new ScenePlay();
            this.poScenePlayer2 = new ScenePlay();
            this.poSceneOver = new SceneOver();

            // initialize to the select state
            this.pSceneState = this.poSceneSelect;
            //this.pSceneState.Transition();

            this.pSceneState.Entering();

            this.Initialize();
        }

        public static SceneContext GetInstance()
        {
            if (pInstance == null)
            {
                pInstance = new SceneContext();
            }
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        private void Initialize()
        {
            this.poSceneSelect.name = Scene.Select;
            this.poScenePlayer1.name = Scene.Player1;
            this.poScenePlayer2.name = Scene.Player2;
            this.poSceneOver.name = Scene.Over;
        }

        public SceneState GetState()
        {
            return this.pSceneState;
        }

        public void SetState(Scene eScene)
        {
            switch (eScene)
            {
                case Scene.Select:
                    this.pSceneState.Leaving();
                    this.pSceneState = this.poSceneSelect;
                    this.pSceneState.Entering();
                    break;

                case Scene.Player1:
                    this.pSceneState.Leaving();
                    this.pSceneState = this.poScenePlayer1;
                    this.pSceneState.Entering();
                    break;

                case Scene.Player2:
                    this.pSceneState.Leaving();
                    this.pSceneState = this.poScenePlayer2;
                    this.pSceneState.Entering();
                    break;

                case Scene.Over:
                    this.pSceneState.Leaving();
                    this.pSceneState = this.poSceneOver;
                    this.pSceneState.Entering();
                    break;

            }
        }

        public void SetHighhScore(int newHighScore)
        {
            if (newHighScore > highScore)
            {
                highScore = newHighScore;
            }

            Font pFont = FontMan.Find(Font.Name.HighScore);
            pFont.UpdateMessage(highScore.ToString("D4"));
        }

        public void UpdateScreens()
        {
            if (poScenePlayer1.numLives == 1 && poScenePlayer2.numLives > 1)
            {
                SetHighhScore(score1);
                this.SetState(SceneContext.Scene.Player2);
            }

            if (poScenePlayer1.numLives > 1 && poScenePlayer2.numLives == 1)
            {
                SetHighhScore(score2);
                this.SetState(SceneContext.Scene.Player1);
            }

            if (poScenePlayer1.numLives == 1 && poScenePlayer2.numLives == 1)
            {
                if (score1 > score2)
                {
                    SetHighhScore(score1);
                }
                else if (score2 > score1)
                {
                    SetHighhScore(score2);
                }
                else
                {
                    SetHighhScore(score1);
                }

                this.SetState(SceneContext.Scene.Over);
            }
        }

        // ----------------------------------------------------
        // Data: 
        // -----------------------------------------------------
        private static SceneContext pInstance = null;
        SceneState pSceneState;
        SceneSelect poSceneSelect;
        SceneOver poSceneOver;
        public ScenePlay poScenePlayer1;
        public ScenePlay poScenePlayer2;

        public static int score1;
        public static int score2;
        public static int highScore;
    }
}
