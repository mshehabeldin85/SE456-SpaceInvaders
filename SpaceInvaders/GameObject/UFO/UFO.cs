using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class UFO : UFOCategory
    {
        public UFO(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName, UFOCategory.Type.UFO)
        {
            this.x = posX;
            this.y = posY;
            this.delta = 2.7f;
            this.pRandom = new Random();

            this.poColObj.pColSprite.SetLineColor(1, 1, 0);
        }

        public override void Remove()
        {
            // Since the Root object is being drawn
            // 1st set its size to zero
            this.poColObj.poColRect.Set(0, 0, 0, 0);
            base.Update();

            // Update the parent (missile root)
            GameObject pParent = (GameObject)this.pParent;
            pParent.Update();

            // Now remove it
            base.Remove();

            this.onScreen = false;
        }

        public override void Update()
        {
            base.Update();

            if (animate == true)
            {
                this.x += delta;

                if (this.x <= 20 || this.x >= 780)
                {
                    this.onScreen = false;
                }
                else
                {
                    this.onScreen = true;
                }
            }
        }

        public float GetBoundingBoxHeight()
        {
            return this.poColObj.poColRect.height;
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitUFO(this);
        }

        public override void VisitMissile(Missile m)
        {
            // Missile vs UFO
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        public int GetScore()
        {
            int score = 0;
            int randNum = pRandom.Next(50, 300);

            if (randNum <= 50)
            {
                score = 50;
            }
            else if (randNum > 50 && randNum <= 100)
            {
                score = 100;
            }
            else if (randNum > 100 && randNum <= 150)
            {
                score = 150;
            }
            else if (randNum > 150 && randNum <= 200)
            {
                score = 200;
            }
            else if (randNum > 200 && randNum <= 2500)
            {
                score = 250;
            }
            else if (randNum > 250 && randNum <= 300)
            {
                score = 300;
            }

            return score;
        }

        public GameObject GetGameObject()
        {
            return this;
        }

        // Data
        public float delta;
        private Random pRandom;
        public bool animate;
    }
}

// End of File
