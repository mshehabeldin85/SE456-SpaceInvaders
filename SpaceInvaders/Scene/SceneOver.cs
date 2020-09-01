using System.Diagnostics;

namespace SpaceInvaders
{
    public class SceneOver : SceneState
    {
        public SceneOver()
        {
            this.Initialize();
        }

        public override void Handle()
        {
        }

        public override void Initialize()
        {
            this.name = SceneContext.Scene.Over;

            this.poSpriteBatchMan = new SpriteBatchMan(3, 1);
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);

            SpriteBatch pSB_Texts = this.poSpriteBatchMan.Add(SpriteBatch.Name.Texts, 100);

            Texture pTexture = TextureMan.Add(Texture.Name.Consolas36pt, "Consolas36pt.tga");
            GlyphMan.AddXml(Glyph.Name.Consolas36pt, "Consolas36pt.xml", Texture.Name.Consolas36pt);

            Font pFont = FontMan.Add(Font.Name.GameOver, SpriteBatch.Name.Texts, "GAME OVER", Glyph.Name.Consolas36pt, 310, 350);
            pFont.SetColor(1.0f, 0.0f, 0.0f);
        }

        public override void Update(float systemTime)
        {
            // Input
            InputMan.Update();

            // Do the collision checks
            ColPairMan.Process();

            // walk through all objects and push to flyweight
            GameObjectMan.Update();

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

        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------

    }
}
