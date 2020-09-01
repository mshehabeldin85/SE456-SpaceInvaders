using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class UFOSpawnEvent : Command
    {
        public enum Direction
        {
            Right,
            Left,
        }

        public UFOSpawnEvent(Random pRandom)
        {
            this.pUFORoot = GameObjectMan.Find(GameObject.Name.UFORoot);
            Debug.Assert(this.pUFORoot != null);

            this.pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            Debug.Assert(this.pSB_Aliens != null);

            this.pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            Debug.Assert(this.pSB_Boxes != null);

            this.direction = Direction.Right;
            this.UFODelta = 2.7f;

            this.time = 15.0f;

            this.pRandom = pRandom;
        }

        override public void Execute(float deltaTime)
        {
            this.pUFO = new UFO(GameObject.Name.UFO, GameSprite.Name.UFO, -20, 530);
            Debug.Assert(this.pUFO != null);
            pUFO.ActivateCollisionSprite(this.pSB_Boxes);
            pUFO.ActivateGameSprite(this.pSB_Aliens);

            pUFO.animate = true;

            SwapDirection();

            // Attach the missile to the Bomb root
            GameObject pUFORoot = GameObjectMan.Find(GameObject.Name.UFORoot);
            Debug.Assert(pUFORoot != null);

            // Add to GameObject Tree - {update and collisions}
            pUFORoot.Add(pUFO);

            // Add timer event UFO Tempo
            UFOTempoEvent pUFOTempo = new UFOTempoEvent(pUFO);
            pUFOTempo.Attach(SoundEngine.Name.UFO_HighPitch);
            TimerMan.Add(TimeEvent.Name.UFOSoundTempo, pUFOTempo, 0.5f);

            // Add timer event
            TimerMan.Add(TimeEvent.Name.UFORandom, this, pRandom.Next(8, 25));

            // Add timer event for UFO Bombs
            TimerMan.Add(TimeEvent.Name.BombRandomUFO, new BombSpawnEvent(pRandom, pUFO), pRandom.Next(1, 4));
        }

        private void SwapDirection()
        {
            if (this.direction == Direction.Right)
            {
                this.direction = Direction.Left;
                this.pUFO.x = 820;
            }
            else
            {
                this.direction = Direction.Right;
                this.pUFO.x = -20;
            }

            this.UFODelta *= -1;
            this.pUFO.delta = this.UFODelta;
        }

        GameObject pUFORoot;
        UFO pUFO;
        SpriteBatch pSB_Aliens;
        SpriteBatch pSB_Boxes;
        float time;
        public UFOSpawnEvent.Direction direction;
        float UFODelta;
        Random pRandom;
    }
}

// End of File
