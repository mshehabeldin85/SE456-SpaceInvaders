
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SceneSelect : SceneState
    {
        public SceneSelect()
        {
            this.Initialize();
        }

        public override void Handle()
        {
            Debug.WriteLine("Handle");
        }

        public override void Initialize()
        {
            this.name = SceneContext.Scene.Select;

            this.poSpriteBatchMan = new SpriteBatchMan(3, 1);
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);

            this.poGameObjectMan = new GameObjectMan(3, 1);
            GameObjectMan.SetActive(this.poGameObjectMan);

            SpriteBatch pSB_Texts = this.poSpriteBatchMan.Add(SpriteBatch.Name.Texts, 100);

            TextureMan.Add(Texture.Name.Consolas36pt, "Consolas36pt.tga");
            GlyphMan.AddXml(Glyph.Name.Consolas36pt, "Consolas36pt.xml", Texture.Name.Consolas36pt);

            TextureMan.Add(Texture.Name.Consolas48pt, "Consolas48pt.tga");
            GlyphMan.AddXml(Glyph.Name.Consolas48pt, "Consolas48pt.xml", Texture.Name.Consolas48pt);

            FontMan.Add(Font.Name.Select, SpriteBatch.Name.Texts, "SPACE INVADERS", Glyph.Name.Consolas48pt, 280, 480);
            FontMan.Add(Font.Name.Select, SpriteBatch.Name.Texts, "PRESS <1> and <2> TO SWITCH PLAYERS", Glyph.Name.Consolas36pt, 100, 400);
            FontMan.Add(Font.Name.Select, SpriteBatch.Name.Texts, "*SCORE ADVANCE TABLE*", Glyph.Name.Consolas36pt, 220, 350);
            FontMan.Add(Font.Name.Select, SpriteBatch.Name.Texts, "? MYSTERY", Glyph.Name.Consolas36pt, 370, 300);
            FontMan.Add(Font.Name.Select, SpriteBatch.Name.Texts, "30 POINTS", Glyph.Name.Consolas36pt, 370, 270);
            FontMan.Add(Font.Name.Select, SpriteBatch.Name.Texts, "20 POINTS", Glyph.Name.Consolas36pt, 370, 240);
            FontMan.Add(Font.Name.Select, SpriteBatch.Name.Texts, "10 POINTS", Glyph.Name.Consolas36pt, 370, 210);


            SpriteBatch pSB_Aliens = this.poSpriteBatchMan.Add(SpriteBatch.Name.Aliens, 200);
            SpriteBatch pSB_Boxes = this.poSpriteBatchMan.Add(SpriteBatch.Name.Boxes, 300);

            TextureMan.Add(Texture.Name.Aliens, "Invaders_0.tga");

            ImageMan.Add(Image.Name.SquidOpen, Texture.Name.Aliens, 616, 28, 112, 112);
            ImageMan.Add(Image.Name.CrabOpen, Texture.Name.Aliens, 321, 28, 155, 112);
            ImageMan.Add(Image.Name.OctopusOpen, Texture.Name.Aliens, 56, 28, 167, 112);
            ImageMan.Add(Image.Name.UFO, Texture.Name.Aliens, 83, 503, 225, 98);

            this.poGameSpriteMan = new GameSpriteMan(3, 1);
            GameSpriteMan.SetActive(this.poGameSpriteMan);

            GameSpriteMan.Add(GameSprite.Name.SquidOpen, Image.Name.SquidOpen, 50, 550, 25, 25);
            GameSpriteMan.Add(GameSprite.Name.CrabOpen, Image.Name.CrabOpen, 50, 510, 25, 25);
            GameSpriteMan.Add(GameSprite.Name.OctopusOpen, Image.Name.OctopusOpen, 50, 430, 25, 25);
            GameSpriteMan.Add(GameSprite.Name.UFO, Image.Name.UFO, 50, 500, 40, 18);

            // Aliens
            AlienFactory AF = new AlienFactory(SpriteBatch.Name.Aliens, SpriteBatch.Name.Boxes);

            GameObject pCol = AF.Create(GameObject.Name.AlienColumn, AlienCategory.Type.Column);

            GameObject pSquidObj = AF.Create(GameObject.Name.SquidAlien, AlienCategory.Type.Squid, 330, 270);
            pCol.Add(pSquidObj);

            GameObject pCrabObj = AF.Create(GameObject.Name.CrabAlien, AlienCategory.Type.Crab, 330, 240);
            pCol.Add(pCrabObj);

            GameObject pOctopusObj = AF.Create(GameObject.Name.OctopusAlien, AlienCategory.Type.Octopus, 330, 210);
            pCol.Add(pOctopusObj);

            GameObjectMan.Attach(pCol);

            // UFO
            UFORoot pUFORoot = new UFORoot(GameObject.Name.UFORoot, GameSprite.Name.NullObject, 0.0f, 0.0f);

            UFO pUFO = new UFO(GameObject.Name.UFO, GameSprite.Name.UFO, 330, 300);
            pUFO.ActivateGameSprite(pSB_Aliens);

            pUFORoot.Add(pUFO);

            GameObjectMan.Attach(pUFORoot);

            FontMan.Add(Font.Name.ScoreText, SpriteBatch.Name.Texts, "HI-SCORE: ", Glyph.Name.Consolas36pt, 300, 150);
            pFontHighScore = FontMan.Add(Font.Name.HighScore, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 470, 150);
        }

        public override void Update(float systemTime)
        {
            // Input
            InputMan.Update();

            // walk through all objects and push to flyweight
            GameObjectMan.Update();

            string updatedScore = (SceneContext.highScore).ToString("D4");
            this.pFontHighScore.UpdateMessage(updatedScore);
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
        }

        public override void Leaving()
        {
            // update SpriteBatchMan()
            this.TimeAtPause = TimerMan.GetCurrTime();
        }

        public override void RemoveLife()
        {

        }

        public override void AddLife()
        {

        }

        public override void UpdateScore(int score)
        {
        }

        // Data
        public Font pFontHighScore;
    }
}
