using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ScenePlay : SceneState
    {
        public ScenePlay()
        {
            this.Initialize();
        }

        public override void Handle()
        {

        }

        public override void Initialize()
        {
            //this.name = SceneContext.Scene.Play;

            this.numLives = 3;
            this.isDead = false;

            //---------------------------------------------------------------------------------------------------------
            // Unique Managers
            //---------------------------------------------------------------------------------------------------------

            this.poSpriteBatchMan = new SpriteBatchMan(3, 1);
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);

            this.poGameObjectMan = new GameObjectMan(3, 1);
            GameObjectMan.SetActive(this.poGameObjectMan);

            this.poTimerMan = new TimerMan(3, 1);
            TimerMan.SetActive(this.poTimerMan);

            this.poDelayedObjectMan = new DelayedObjectMan();
            DelayedObjectMan.SetActive(this.poDelayedObjectMan);

            //---------------------------------------------------------------------------------------------------------
            // Initialize Input Keys
            //---------------------------------------------------------------------------------------------------------

            this.poInputMan = new InputMan();
            InputMan.SetActive(this.poInputMan);

            InputSubject pInputSubject;
            pInputSubject = InputMan.GetArrowRightSubject();
            pInputSubject.Attach(new MoveRightObserver());

            pInputSubject = InputMan.GetArrowLeftSubject();
            pInputSubject.Attach(new MoveLeftObserver());

            pInputSubject = InputMan.GetSpaceSubject();
            pInputSubject.Attach(new ShootObserver());

            pInputSubject = InputMan.GetKey_B_Subject();
            pInputSubject.Attach(new Key_B_Observer());

            pInputSubject = InputMan.GetKey_1_Subject();
            pInputSubject.Attach(new Key_1_Observer());

            pInputSubject = InputMan.GetKey_2_Subject();
            pInputSubject.Attach(new Key_2_Observer());

            pInputSubject = InputMan.GetKey_Q_Subject();
            pInputSubject.Attach(new Key_Q_Observer());

            //---------------------------------------------------------------------------------------------------------
            // Sound
            //---------------------------------------------------------------------------------------------------------

            // start up the engine
            SoundEngineMan.Add(SoundEngine.Name.AlienA, "fastinvader1.wav", 0.2f);
            SoundEngineMan.Add(SoundEngine.Name.AlienB, "fastinvader2.wav", 0.2f);
            SoundEngineMan.Add(SoundEngine.Name.AlienC, "fastinvader3.wav", 0.2f);
            SoundEngineMan.Add(SoundEngine.Name.AlienD, "fastinvader4.wav", 0.2f);
            SoundEngineMan.Add(SoundEngine.Name.Shoot, "shoot.wav", 0.2f);
            SoundEngineMan.Add(SoundEngine.Name.Invaderkilled, "invaderkilled.wav", 0.2f);
            SoundEngineMan.Add(SoundEngine.Name.Explosion, "explosion.wav", 0.2f);
            SoundEngineMan.Add(SoundEngine.Name.UFO_HighPitch, "ufo_highpitch.wav", 0.5f);
            SoundEngineMan.Add(SoundEngine.Name.UFO_LowPitch, "ufo_lowpitch.wav", 0.2f);

            //---------------------------------------------------------------------------------------------------------
            // Font
            //---------------------------------------------------------------------------------------------------------

            Texture pTexture = TextureMan.Add(Texture.Name.Consolas36pt, "Consolas36pt.tga");
            GlyphMan.AddXml(Glyph.Name.Consolas36pt, "Consolas36pt.xml", Texture.Name.Consolas36pt);

            //---------------------------------------------------------------------------------------------------------
            // Create Sprites
            //---------------------------------------------------------------------------------------------------------
            this.poGameSpriteMan = new GameSpriteMan(3, 1);
            GameSpriteMan.SetActive(this.poGameSpriteMan);

            // ----- Aliens -----
            GameSpriteMan.Add(GameSprite.Name.SquidOpen, Image.Name.SquidOpen, 50, 550, 22, 22);
            GameSpriteMan.Add(GameSprite.Name.CrabOpen, Image.Name.CrabOpen, 50, 510, 25, 25);
            GameSpriteMan.Add(GameSprite.Name.OctopusOpen, Image.Name.OctopusOpen, 50, 430, 28, 28);

            // ----- UFO -----
            GameSpriteMan.Add(GameSprite.Name.UFO, Image.Name.UFO, 0, 500, 40, 18);

            // ----- Missile -----
            GameSpriteMan.Add(GameSprite.Name.Missile, Image.Name.Missile, 50, 430, 4, 15);

            // ----- Ship -----
            GameSpriteMan.Add(GameSprite.Name.Ship, Image.Name.Ship, 50, 430, 50, 25);

            // ----- Splats -----
            GameSpriteMan.Add(GameSprite.Name.SplatAlien, Image.Name.AlienSplat, 100, 100, 22, 22);
            GameSpriteMan.Add(GameSprite.Name.SplatBomb, Image.Name.BombSplat, 100, 100, 7, 17);
            GameSpriteMan.Add(GameSprite.Name.SplatMissile, Image.Name.MissleSplat, 100, 100, 20, 15);
            GameSpriteMan.Add(GameSprite.Name.SplatUFO, Image.Name.UFOSplat, 100, 100, 40, 18);
            GameSpriteMan.Add(GameSprite.Name.SplatShip, Image.Name.ShipSplat, 100, 100, 50, 25);

            // ----- Bombs -----
            GameSpriteMan.Add(GameSprite.Name.BombDragger, Image.Name.BombDragger, 100, 100, 7, 17);
            GameSpriteMan.Add(GameSprite.Name.BombZigZag, Image.Name.BombZigZag, 100, 100, 7, 17);
            GameSpriteMan.Add(GameSprite.Name.BombRolling, Image.Name.BombRolling, 100, 100, 7, 17);
            GameSpriteMan.Add(GameSprite.Name.BombStraight, Image.Name.BombStraight, 100, 100, 5, 17);

            // ----- Shields -----
            float brick_W = 15.0f;
            float brick_H = 6.0f;
            GameSpriteMan.Add(GameSprite.Name.Brick, Image.Name.Brick, 50, 25, brick_W, brick_H);
            GameSpriteMan.Add(GameSprite.Name.Brick_LeftTop0, Image.Name.BrickLeft_Top0, 50, 25, brick_W, brick_H);
            GameSpriteMan.Add(GameSprite.Name.Brick_LeftTop1, Image.Name.BrickLeft_Top1, 50, 25, brick_W, brick_H);
            GameSpriteMan.Add(GameSprite.Name.Brick_LeftBottom, Image.Name.BrickLeft_Bottom, 50, 25, brick_W, brick_H);
            GameSpriteMan.Add(GameSprite.Name.Brick_RightTop0, Image.Name.BrickRight_Top0, 50, 25, brick_W, brick_H);
            GameSpriteMan.Add(GameSprite.Name.Brick_RightTop1, Image.Name.BrickRight_Top1, 50, 25, brick_W, brick_H);
            GameSpriteMan.Add(GameSprite.Name.Brick_RightBottom, Image.Name.BrickRight_Bottom, 50, 25, brick_W, brick_H);

            //---------------------------------------------------------------------------------------------------------
            // Create SpriteBatch
            //---------------------------------------------------------------------------------------------------------

            SpriteBatch pSB_Aliens = this.poSpriteBatchMan.Add(SpriteBatch.Name.Aliens, 100);
            SpriteBatch pSB_Shields = this.poSpriteBatchMan.Add(SpriteBatch.Name.Shields, 200);
            SpriteBatch pSB_Texts = this.poSpriteBatchMan.Add(SpriteBatch.Name.Texts, 300);
            SpriteBatch pSB_Box = this.poSpriteBatchMan.Add(SpriteBatch.Name.Boxes, 400);

            //---------------------------------------------------------------------------------------------------------
            // Create Aliens
            //---------------------------------------------------------------------------------------------------------

            // Create the Aliens factory 
            AlienFactory AF = new AlienFactory(SpriteBatch.Name.Aliens, SpriteBatch.Name.Boxes);

            // Initialize Grid
            AlienGrid pGrid = (AlienGrid)AF.Create(GameObject.Name.AlienGrid, AlienCategory.Type.Grid);

            // Generate Aliens
            pGrid.GenerateAlien(this.poGameObjectMan);

            MoveCommand pMoveGrid = new MoveCommand(pGrid);
            TimerMan.Add(TimeEvent.Name.MoveGrid, pMoveGrid, 0.5f);

            //---------------------------------------------------------------------------------------------------------
            // Timer
            //---------------------------------------------------------------------------------------------------------

            // Create animation for Squid sprite
            AnimationSprite pAnimSquid = new AnimationSprite(GameSprite.Name.SquidOpen);

            // Attach Squid images to cycle
            pAnimSquid.Attach(Image.Name.SquidOpen);
            pAnimSquid.Attach(Image.Name.SquidClosed);


            // Create animation for Crab sprite
            AnimationSprite pAnimCrab = new AnimationSprite(GameSprite.Name.CrabOpen);

            // Attach Crab images to cycle
            pAnimCrab.Attach(Image.Name.CrabOpen);
            pAnimCrab.Attach(Image.Name.CrabClosed);


            // Create animation for Octopus sprite
            AnimationSprite pAnimOctopus = new AnimationSprite(GameSprite.Name.OctopusOpen);

            // Attach Octopus images to cycle
            pAnimOctopus.Attach(Image.Name.OctopusOpen);
            pAnimOctopus.Attach(Image.Name.OctopusClosed);


            // Add AnimationSprite to timer
            TimerMan.Add(TimeEvent.Name.SwapAliens, pAnimSquid, 0.5f);
            TimerMan.Add(TimeEvent.Name.SwapAliens, pAnimCrab, 0.5f);
            TimerMan.Add(TimeEvent.Name.SwapAliens, pAnimOctopus, 0.5f);

            // Add Grid Tempo
            GridSoundTempoEvent pGridTempo = new GridSoundTempoEvent();
            pGridTempo.Attach(SoundEngine.Name.AlienA);
            pGridTempo.Attach(SoundEngine.Name.AlienB);
            pGridTempo.Attach(SoundEngine.Name.AlienC);
            pGridTempo.Attach(SoundEngine.Name.AlienD);

            TimerMan.Add(TimeEvent.Name.GridSoundTempo, pGridTempo, 0.5f);

            //---------------------------------------------------------------------------------------------------------
            // Create Missile
            //---------------------------------------------------------------------------------------------------------

            // Missile Root
            MissileGroup pMissileGroup = new MissileGroup(GameObject.Name.MissileGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pMissileGroup.ActivateGameSprite(pSB_Aliens);
            pMissileGroup.ActivateCollisionSprite(pSB_Box);

            GameObjectMan.Attach(pMissileGroup);

            //---------------------------------------------------------------------------------------------------------
            // Splat Root
            //---------------------------------------------------------------------------------------------------------
            BombRoot pSplatRoot = new BombRoot(GameObject.Name.SplatRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pSplatRoot.ActivateCollisionSprite(pSB_Box);

            GameObjectMan.Attach(pSplatRoot);

            //---------------------------------------------------------------------------------------------------------
            // Bomb Root
            //---------------------------------------------------------------------------------------------------------
            BombRoot pBombRoot = new BombRoot(GameObject.Name.BombRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pBombRoot.ActivateCollisionSprite(pSB_Box);

            GameObjectMan.Attach(pBombRoot);
            TimerMan.Add(TimeEvent.Name.BombRandom, new BombSpawnEvent(pRandom, pGrid), 1.0f);

            //---------------------------------------------------------------------------------------------------------
            // Create Walls
            //---------------------------------------------------------------------------------------------------------

            // Wall Root
            WallGroup pWallGroup = new WallGroup(GameObject.Name.WallGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pWallGroup.ActivateGameSprite(pSB_Aliens);
            pWallGroup.ActivateCollisionSprite(pSB_Box);

            WallRight pWallRight = new WallRight(GameObject.Name.WallRight, GameSprite.Name.NullObject, 775, 290, 20, 500);
            pWallRight.ActivateCollisionSprite(pSB_Box);

            WallLeft pWallLeft = new WallLeft(GameObject.Name.WallLeft, GameSprite.Name.NullObject, 25, 290, 20, 500);
            pWallLeft.ActivateCollisionSprite(pSB_Box);

            // Add to the composite the children
            pWallGroup.Add(pWallRight);
            pWallGroup.Add(pWallLeft);

            GameObjectMan.Attach(pWallGroup);

            //---------------------------------------------------------------------------------------------------------
            // Create No Score Items
            //---------------------------------------------------------------------------------------------------------

            WallGroup pNoScoreGroup = new WallGroup(GameObject.Name.NoScoreGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pNoScoreGroup.ActivateGameSprite(pSB_Aliens);
            pNoScoreGroup.ActivateCollisionSprite(pSB_Box);

            WallTop pWallTop = new WallTop(GameObject.Name.WallTop, GameSprite.Name.NullObject, 400, 540, 700, 0);
            pWallTop.ActivateCollisionSprite(pSB_Aliens);

            WallBottom pWallBottom = new WallBottom(GameObject.Name.WallBottom, GameSprite.Name.NullObject, 400, 40, 730, 0);
            pWallBottom.ActivateGameSprite(pSB_Aliens);
            pWallBottom.ActivateCollisionSprite(pSB_Box);

            pNoScoreGroup.Add(pWallTop);
            pNoScoreGroup.Add(pWallBottom);

            GameObjectMan.Attach(pNoScoreGroup);

            //---------------------------------------------------------------------------------------------------------
            // Ship
            //---------------------------------------------------------------------------------------------------------

            ShipRoot pShipRoot = new ShipRoot(GameObject.Name.ShipRoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pShipRoot.ActivateGameSprite(pSB_Aliens);
            pShipRoot.ActivateCollisionSprite(pSB_Box);

            GameObjectMan.Attach(pShipRoot);

            //ShipMan.Create();

            this.poShipMan = new ShipMan();
            ShipMan.SetActive(this.poShipMan);
            this.poShipMan.pShip = ShipMan.ActivateShip();
            this.poShipMan.pShip.SetState(ShipMan.State.Ready);


            //---------------------------------------------------------------------------------------------------------
            // UFO
            //---------------------------------------------------------------------------------------------------------
            UFORoot pUFORoot = new UFORoot(GameObject.Name.UFORoot, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pUFORoot.ActivateGameSprite(pSB_Aliens);
            pUFORoot.ActivateCollisionSprite(pSB_Box);

            GameObjectMan.Attach(pUFORoot);
            TimerMan.Add(TimeEvent.Name.UFORandom, new UFOSpawnEvent(pRandom), 5.0f);

            //---------------------------------------------------------------------------------------------------------
            // Shields
            //---------------------------------------------------------------------------------------------------------

            Composite pShieldRoot = ShieldsGroupFactory.Create(100, 130, this.poGameObjectMan);

            //---------------------------------------------------------------------------------------------------------
            // ColPair 
            //---------------------------------------------------------------------------------------------------------

            // Grid vs Right Left Walls
            ColPair pColPair_GW = ColPairMan.Add(ColPair.Name.Alien_Wall, pGrid, pWallGroup);
            Debug.Assert(pColPair_GW != null);

            pColPair_GW.Attach(new GridDropObserver());

            // Ship vs Right Left Walls
            ColPair pColPair_SW = ColPairMan.Add(ColPair.Name.Ship_Wall, pShipRoot, pWallGroup);
            Debug.Assert(pColPair_SW != null);

            pColPair_SW.Attach(new StopShipMoveObserver());

            // Grid vs Shield
            ColPair pColPair_AS = ColPairMan.Add(ColPair.Name.Bomb_Shield, pGrid, pShieldRoot);
            Debug.Assert(pColPair_AS != null);

            pColPair_AS.Attach(new RemoveBrickObserver());

            // Missile vs Alien
            ColPair pColPair_MA = ColPairMan.Add(ColPair.Name.Missile_Alien, pMissileGroup, pGrid);
            Debug.Assert(pColPair_MA != null);

            pColPair_MA.Attach(new ShipReadyObserver());
            pColPair_MA.Attach(new RemoveAlienObserver(this));
            pColPair_MA.Attach(new ShipRemoveMissileObserver(false));
            pColPair_MA.Attach(new SndAlienHitObserver());
            pColPair_MA.Attach(new SpeedGridObserver());

            // Missile vs UFO
            ColPair pColPair_MU = ColPairMan.Add(ColPair.Name.Missile_UFO, pMissileGroup, pUFORoot);
            Debug.Assert(pColPair_MU != null);

            pColPair_MU.Attach(new ShipReadyObserver());
            pColPair_MU.Attach(new ShipRemoveMissileObserver(false));
            pColPair_MU.Attach(new RemoveUFOObserver());
            pColPair_MU.Attach(new SndAlienHitObserver());

            // Missle vs Bomb
            ColPair pColPair_MB = ColPairMan.Add(ColPair.Name.Missile_Bomb, pMissileGroup, pBombRoot);
            Debug.Assert(pColPair_MB != null);

            pColPair_MB.Attach(new ShipReadyObserver());
            pColPair_MB.Attach(new ShipRemoveMissileObserver(false));
            pColPair_MB.Attach(new BombObserver(true));

            // Missile vs Top Wall
            ColPair pColPair_MW = ColPairMan.Add(ColPair.Name.Missile_Wall, pMissileGroup, pNoScoreGroup);
            Debug.Assert(pColPair_MW != null);

            pColPair_MW.Attach(new ShipReadyObserver());
            pColPair_MW.Attach(new ShipRemoveMissileObserver(true));

            // Missile vs Shield
            ColPair pColPair_MS = ColPairMan.Add(ColPair.Name.Misslie_Shield, pMissileGroup, pShieldRoot);
            Debug.Assert(pColPair_MS != null);

            pColPair_MS.Attach(new ShipRemoveMissileObserver(false));
            pColPair_MS.Attach(new RemoveBrickObserver());
            pColPair_MS.Attach(new ShipReadyObserver());
            pColPair_MS.Attach(new SndAlienHitObserver());

            // Bomb vs Bottom Wall
            ColPair pColPair_BW = ColPairMan.Add(ColPair.Name.Bomb_Wall, pBombRoot, pNoScoreGroup);
            Debug.Assert(pColPair_BW != null);

            pColPair_BW.Attach(new BombObserver(true));

            // Bomb vs Shield
            ColPair pColPair_BS = ColPairMan.Add(ColPair.Name.Bomb_Shield, pBombRoot, pShieldRoot);
            Debug.Assert(pColPair_BS != null);

            pColPair_BS.Attach(new RemoveBombObserver(this));
            pColPair_BS.Attach(new RemoveBrickObserver());
            pColPair_BS.Attach(new SndAlienHitObserver());

            // Bomb vs Player's Ship
            ColPair pColPair_BP = ColPairMan.Add(ColPair.Name.Bomb_Ship, pBombRoot, pShipRoot);
            Debug.Assert(pColPair_BP != null);

            pColPair_BP.Attach(new RemoveShipObserver(this));
            pColPair_BP.Attach(new RemoveBombObserver(this));
            pColPair_BP.Attach(new SndShipHitObserver());
            pColPair_BP.Attach(new ReCreateShipObserver(this));

            //---------------------------------------------------------------------------------------------------------
            // Fonts
            //---------------------------------------------------------------------------------------------------------

            FontMan.Add(Font.Name.ScoreText, SpriteBatch.Name.Texts, "SCORE<1>", Glyph.Name.Consolas36pt, 60, 585);
            pFontScore1 = FontMan.Add(Font.Name.Score1, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 85, 558);

            FontMan.Add(Font.Name.ScoreText, SpriteBatch.Name.Texts, "HI-SCORE", Glyph.Name.Consolas36pt, 325, 585);
            pFontHighScore = FontMan.Add(Font.Name.HighScore, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 350, 558);

            FontMan.Add(Font.Name.ScoreText, SpriteBatch.Name.Texts, "SCORE<2>", Glyph.Name.Consolas36pt, 600, 585);
            pFontScore2 = FontMan.Add(Font.Name.Score2, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 625, 558);

            FontMan.Add(Font.Name.ScoreText, SpriteBatch.Name.Texts, "LIVES: ", Glyph.Name.Consolas36pt, 50, 25);
            pFontLives = FontMan.Add(Font.Name.Lives, SpriteBatch.Name.Texts, "3", Glyph.Name.Consolas36pt, 160, 25);
        }

        public override void Update(float systemTime)
        {
            this.UpdateStats();

            // Fire off the timer events
            TimerMan.Update(systemTime);

            // Input
            InputMan.Update();

            // walk through all objects and push to proxy
            GameObjectMan.Update();

            // Do the collision checks
            ColPairMan.Process();

            // Delete any objects here...
            DelayedObjectMan.Process();
        }

        public override void Draw()
        {
            // draw all objects
            SpriteBatchMan.Draw();
        }

        //public override void Transition()
        //{
        //    // update SpriteBatchMan()
        //    SpriteBatchMan.SetActive(this.poSpriteBatchMan);
        //}

        public override void Entering()
        {
            // update SpriteBatchMan()
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);
            GameObjectMan.SetActive(this.poGameObjectMan);
            TimerMan.SetActive(this.poTimerMan);
            ShipMan.SetActive(this.poShipMan);
            DelayedObjectMan.SetActive(this.poDelayedObjectMan);

            // Update timer since last pause
            float t0 = GlobalTimer.GetTime();
            float t1 = this.TimeAtPause;
            float delta = t0 - t1;
            TimerMan.PauseUpdate(delta);
        }

        public override void Leaving()
        {

            // Need a better way to do this
            this.TimeAtPause = GlobalTimer.GetTime();
        }

        public override void RemoveLife()
        {
            this.numLives--;

            pFontLives.UpdateMessage(numLives.ToString());
        }

        public override void AddLife()
        {
            this.numLives++;

            pFontLives.UpdateMessage(numLives.ToString());
        }

        public override void UpdateScore(int score)
        {
            if (name == SceneContext.Scene.Player1)
            {
                SceneContext.score1 += score;
            }

            if (name == SceneContext.Scene.Player2)
            {
                SceneContext.score2 += score;
            }
        }

        public void UpdateStats()
        {
            String updatedScore;

            updatedScore = (SceneContext.score1).ToString("D4");
            this.pFontScore1.UpdateMessage(updatedScore);

            updatedScore = (SceneContext.score2).ToString("D4");
            this.pFontScore2.UpdateMessage(updatedScore);

            updatedScore = (SceneContext.highScore).ToString("D4");
            this.pFontHighScore.UpdateMessage(updatedScore);
        }

        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        public ProxySpriteMan poProxySpriteMan;
        public InputMan poInputMan;
        readonly Random pRandom = new Random();
        public bool isDead;
        public ShipMan poShipMan;

        public Font pFontLives;
        public Font pFontScore1;
        public Font pFontScore2;
        public Font pFontHighScore;
    }
}
