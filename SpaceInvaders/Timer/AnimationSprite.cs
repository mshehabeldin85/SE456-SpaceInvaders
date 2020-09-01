using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AnimationSprite : Command
    {
        public AnimationSprite(GameSprite.Name spriteName)
        {
            // initialized the sprite animation is attached to
            this.pSprite = GameSpriteMan.Find(spriteName);
            Debug.Assert(this.pSprite != null);

            // initialize references
            this.pCurrImage = null;

            // list
            this.poFirstImage = null;
        }

        public void Attach(Image.Name imageName)
        {
            // Get the image
            Image pImage = ImageMan.Find(imageName);
            Debug.Assert(pImage != null);

            // Create a new holder
            ImageHolder pImageHolder = new ImageHolder(pImage);
            Debug.Assert(pImageHolder != null);

            // Attach it to the Animation Sprite ( Push to front )
            DLink.AddToFront(ref this.poFirstImage, pImageHolder);

            // Set the first one to this image
            this.pCurrImage = pImageHolder;
        }

        public override void Execute(float deltaTime)
        {
            // advance to next image 
            ImageHolder pImageHolder = (ImageHolder)this.pCurrImage.pNext;

            // if at end of list, set to first
            if (pImageHolder == null)
            {
                pImageHolder = (ImageHolder)poFirstImage;
            }

            // squirrel away for next timer event
            this.pCurrImage = pImageHolder;

            // change image
            this.pSprite.SwapImage(pImageHolder.pImage);

            // Add itself back to timer
            TimerMan.Add(TimeEvent.Name.SwapAliens, this, deltaTime);
        }

        // Data: ---------------
        private GameSprite pSprite;
        private DLink pCurrImage;
        private DLink poFirstImage;
    }
}

// End of File
