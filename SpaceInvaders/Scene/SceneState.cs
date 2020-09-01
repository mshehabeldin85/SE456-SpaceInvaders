using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class SceneState
    {
        public abstract void Handle();
        public abstract void Initialize();
        public abstract void Update(float systemTime);
        public abstract void Draw();
        //public abstract void Transition();
        public abstract void Entering();
        public abstract void Leaving();
        public abstract void RemoveLife();
        public abstract void AddLife();
        public abstract void UpdateScore(int score);

        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        public int numLives;
        public float TimeAtPause;
        public SceneContext.Scene name;
        public SpriteBatchMan poSpriteBatchMan;
        public GameSpriteMan poGameSpriteMan;
        public GameObjectMan poGameObjectMan;
        public TimerMan poTimerMan;
        public DelayedObjectMan poDelayedObjectMan;

        public int score1;
        public int score2;
    }
}
