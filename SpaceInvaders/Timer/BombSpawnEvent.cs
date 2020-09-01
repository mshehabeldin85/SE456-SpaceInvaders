using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BombSpawnEvent : Command
    {
        public BombSpawnEvent(Random pRandom, GameObject gameObj)
        {
            this.pBombRoot = GameObjectMan.Find(GameObject.Name.BombRoot);
            Debug.Assert(this.pBombRoot != null);

            this.pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            Debug.Assert(this.pSB_Aliens != null);

            this.pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            Debug.Assert(this.pSB_Boxes != null);

            this.pRandom = pRandom;

            this.time = 2.0f;

            this.pGrid = null;
            this.pAlienBomb = null;
            this.pGameObj = gameObj;
        }

        override public void Execute(float deltaTime)
        {
            // Get random Bomb pos
            if (this.pGameObj.name == GameObject.Name.AlienGrid)
            {
                this.pGrid = (AlienGrid)this.pGameObj;
                this.pAlienBomb = this.pGrid.GetRandomAlienBombPos();
            }
            else if (this.pGameObj.name == GameObject.Name.UFO)
            {
                this.pUFO = (UFO)this.pGameObj;
                this.pAlienBomb = this.pUFO.GetGameObject();
            }

            if (pGameObj.onScreen == true)
            {
                // Create Bomb
                this.SetRandomBomb();

                Debug.Assert(this.pBomb != null);
                pBomb.ActivateCollisionSprite(this.pSB_Boxes);
                pBomb.ActivateGameSprite(this.pSB_Aliens);

                // Attach the missile to the Bomb root
                GameObject pBombRoot = GameObjectMan.Find(GameObject.Name.BombRoot);
                Debug.Assert(pBombRoot != null);

                // Add to GameObject Tree - {update and collisions}
                pBombRoot.Add(pBomb);


                SoundEngine pSoundEngine = SoundEngineMan.Find(SoundEngine.Name.Shoot);
                Debug.Assert(pSoundEngine != null);

                pSoundEngine.PlaySound();

                // Add timer event
                TimerMan.Add(TimeEvent.Name.BombRandom, this, time);
            }
        }

        private void SetRandomBomb()
        {
            bombNum = this.pRandom.Next(1, 5);

            switch (bombNum)
            {
                case 1:
                    this.pBomb = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombDragger, new FallDagger(), pAlienBomb.x, pAlienBomb.y);
                    break;
                case 2:
                    this.pBomb = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombZigZag, new FallZigZag(), pAlienBomb.x, pAlienBomb.y);
                    break;
                case 3:
                    this.pBomb = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombRolling, new FallRolling(), pAlienBomb.x, pAlienBomb.y);
                    break;
                case 4:
                    this.pBomb = new Bomb(GameObject.Name.Bomb, GameSprite.Name.BombStraight, new FallStraight(), pAlienBomb.x, pAlienBomb.y);
                    break;
                default:
                    Debug.Assert(false, "Random Bomb generation not working");
                    break;
            }
        }

        GameObject pBombRoot;
        Bomb pBomb;
        SpriteBatch pSB_Aliens;
        SpriteBatch pSB_Boxes;
        Random pRandom;
        AlienGrid pGrid;
        UFO pUFO;
        GameObject pAlienBomb;
        float time;
        static int bombNum;
        GameObject pGameObj;
    }
}

// End of File
