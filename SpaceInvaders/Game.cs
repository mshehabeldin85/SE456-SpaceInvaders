using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpaceInvaders : Azul.Game
    {
        SceneContext pSceneContext = null;

        //-----------------------------------------------------------------------------
        // Game::Initialize()
        //		Allows the engine to perform any initialization it needs to before 
        //      starting to run.  This is where it can query for any required services 
        //      and load any non-graphic related content. 
        //-----------------------------------------------------------------------------
        public override void Initialize()
        {
            // Game Window Device setup
            this.SetWindowName("Space Invaders");
            this.SetWidthHeight(800, 600);
            //this.SetWidthHeight(896, 1024);
            this.SetClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        }

        //-----------------------------------------------------------------------------
        // Game::LoadContent()
        //		Allows you to load all content needed for your engine,
        //	    such as objects, graphics, etc.
        //-----------------------------------------------------------------------------
        public override void LoadContent()
        {
            //---------------------------------------------------------------------------------------------------------
            // Setup Managers
            //---------------------------------------------------------------------------------------------------------

            TextureMan.Create(1, 1);
            ImageMan.Create(5, 2);
            //GameSpriteMan.Create(4, 2);
            BoxSpriteMan.Create(3, 1);
            //TimerMan.Create(3, 1);
            ProxySpriteMan.Create(10, 1);
            //GameObjectMan.Create(3, 1);
            ColPairMan.Create(1, 1);
            GlyphMan.Create(3, 1);
            FontMan.Create(1, 1);
            SoundEngineMan.Create(3, 1);

            // Player Unique Systems
            SpriteBatchMan.Create();
            GameSpriteMan.Create();
            GameObjectMan.Create();
            TimerMan.Create();
            DelayedObjectMan.Create();

            //---------------------------------------------------------------------------------------------------------
            // Load the Textures
            //---------------------------------------------------------------------------------------------------------

            TextureMan.Add(Texture.Name.Aliens, "Invaders_0.tga");
            TextureMan.Add(Texture.Name.Shield, "Birds_N_shield.tga");

            //---------------------------------------------------------------------------------------------------------
            // Create Images
            //---------------------------------------------------------------------------------------------------------

            // ----- Aliens -----
            ImageMan.Add(Image.Name.SquidOpen, Texture.Name.Aliens, 616, 28, 112, 112);
            ImageMan.Add(Image.Name.SquidClosed, Texture.Name.Aliens, 616, 182, 112, 112);
            ImageMan.Add(Image.Name.CrabOpen, Texture.Name.Aliens, 321, 28, 155, 112);
            ImageMan.Add(Image.Name.CrabClosed, Texture.Name.Aliens, 321, 182, 155, 112);
            ImageMan.Add(Image.Name.OctopusOpen, Texture.Name.Aliens, 56, 28, 167, 112);
            ImageMan.Add(Image.Name.OctopusClosed, Texture.Name.Aliens, 56, 182, 167, 112);

            // ----- UFO -----
            ImageMan.Add(Image.Name.UFO, Texture.Name.Aliens, 83, 503, 225, 98);

            // ----- Missile -----
            ImageMan.Add(Image.Name.Missile, Texture.Name.Aliens, 420, 700, 15, 55);

            // ----- Ship -----
            ImageMan.Add(Image.Name.Ship, Texture.Name.Aliens, 56, 336, 182, 112);

            // ----- Splats -----
            ImageMan.Add(Image.Name.AlienSplat, Texture.Name.Aliens, 574, 490, 182, 112);
            ImageMan.Add(Image.Name.MissleSplat, Texture.Name.Aliens, 405, 490, 112, 112);
            ImageMan.Add(Image.Name.BombSplat, Texture.Name.Aliens, 699, 798, 84, 112);
            ImageMan.Add(Image.Name.UFOSplat, Texture.Name.Aliens, 41, 643, 294, 112);
            ImageMan.Add(Image.Name.ShipSplat, Texture.Name.Aliens, 307, 335, 210, 112);

            // ----- Bombs -----
            ImageMan.Add(Image.Name.BombDragger, Texture.Name.Aliens, 280, 798, 42, 84);
            ImageMan.Add(Image.Name.BombZigZag, Texture.Name.Aliens, 574, 644, 42, 98);
            ImageMan.Add(Image.Name.BombRolling, Texture.Name.Aliens, 447, 797, 42, 98);
            ImageMan.Add(Image.Name.BombStraight, Texture.Name.Aliens, 377, 798, 14, 98);

            // ----- Shields -----
            ImageMan.Add(Image.Name.Brick, Texture.Name.Shield, 20, 210, 10, 5);
            ImageMan.Add(Image.Name.BrickLeft_Top0, Texture.Name.Shield, 15, 180, 10, 5);
            ImageMan.Add(Image.Name.BrickLeft_Top1, Texture.Name.Shield, 15, 185, 10, 5);
            ImageMan.Add(Image.Name.BrickLeft_Bottom, Texture.Name.Shield, 35, 215, 10, 5);
            ImageMan.Add(Image.Name.BrickRight_Top0, Texture.Name.Shield, 75, 180, 10, 5);
            ImageMan.Add(Image.Name.BrickRight_Top1, Texture.Name.Shield, 75, 185, 10, 5);
            ImageMan.Add(Image.Name.BrickRight_Bottom, Texture.Name.Shield, 55, 215, 10, 5);

            //---------------------------------------------------------------------------------------------------------
            // Create Scenes
            //---------------------------------------------------------------------------------------------------------
            pSceneContext = SceneContext.GetInstance();
        }

        //-----------------------------------------------------------------------------
        // Game::Update()
        //      Called once per frame, update data, tranformations, etc
        //      Use this function to control process order
        //      Input, AI, Physics, Animation, and Graphics
        //-----------------------------------------------------------------------------

        public override void Update()
        {
            // Update the scene
            pSceneContext.GetState().Update(this.GetTime());
        }

        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------
        public override void Draw()
        {
            // draw all objects
            pSceneContext.GetState().Draw();
        }

        //-----------------------------------------------------------------------------
        // Game::UnLoadContent()
        //       unload content (resources loaded above)
        //       unload all content that was loaded before the Engine Loop started
        //-----------------------------------------------------------------------------
        public override void UnLoadContent()
        {

        }

    }
}

