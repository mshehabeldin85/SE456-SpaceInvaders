using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienGrid : Composite
    {
        public AlienGrid(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetLineColor(1, 1, 1);

            this.delta = 7.0f;

            this.isDropping = false;

            this.pRandom = new Random();

            this.onScreen = true;

            this.orgTime = 0.5f;
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an AlienGroup
            // Call the appropriate collision reaction            
            other.VisitGroup(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        public void MoveGrid()
        {
            this.isDropping = false;

            ForwardIterator pFor = new ForwardIterator(this);

            Component pNode = pFor.First();
            while (!pFor.IsDone())
            {
                GameObject pGameObj = (GameObject)pNode;
                pGameObj.x += this.delta;

                pNode = pFor.Next();
            }
        }

        public void DropGrid()
        {
            if (!this.isDropping)
            {
                this.isDropping = true;

                ForwardIterator pFor = new ForwardIterator(this);

                Component pNode = pFor.First();
                while (!pFor.IsDone())
                {
                    GameObject pGameObj = (GameObject)pNode;
                    pGameObj.y -= 10.0f;

                    pNode = pFor.Next();
                }

                this.SetDelta(-1.0f);
                this.MoveGrid();
            }
        }

        public GameObject GetRandomAlienBombPos()
        {
            int colNum = 1;
            int randColNum = pRandom.Next(1, 11);

            ForwardIterator pFor = new ForwardIterator(this);

            Component pNode = pFor.First();
            GameObject pGameObj = (GameObject)pNode;

            while (!pFor.IsDone())
            {
                pGameObj = (GameObject)pNode;

                if (pGameObj.name == Name.AlienColumn)
                {
                    if (colNum == randColNum)
                    {
                        pGameObj = (AlienCategory)pGameObj.GetFirstChild();
                        break;
                    }

                    colNum++;
                }

                pNode = pFor.Next();
            }

            return pGameObj;
        }

        public float GetDelta()
        {
            return this.delta;
        }

        public void SetDelta(float inDelta)
        {
            this.delta *= inDelta;
        }

        public void GenerateAlien(GameObjectMan poGameObjectMan)
        {
            AlienFactory AF = new AlienFactory(SpriteBatch.Name.Aliens, SpriteBatch.Name.Boxes);

            GameObject pGameObj;

            for (int i = 0; i < 11; i++)
            {
                GameObject pCol = AF.Create(GameObject.Name.AlienColumn, AlienCategory.Type.Column);

                pGameObj = AF.Create(GameObject.Name.SquidAlien, AlienCategory.Type.Squid, 250.0f + i * 40.0f, 470.0f);
                pCol.Add(pGameObj);

                pGameObj = AF.Create(GameObject.Name.CrabAlien, AlienCategory.Type.Crab, 250.0f + i * 40.0f, 430.0f);
                pCol.Add(pGameObj);

                pGameObj = AF.Create(GameObject.Name.CrabAlien, AlienCategory.Type.Crab, 250.0f + i * 40.0f, 390.0f);
                pCol.Add(pGameObj);

                pGameObj = AF.Create(GameObject.Name.OctopusAlien, AlienCategory.Type.Octopus, 250.0f + i * 40.0f, 350.0f);
                pCol.Add(pGameObj);

                pGameObj = AF.Create(GameObject.Name.OctopusAlien, AlienCategory.Type.Octopus, 250.0f + i * 40.0f, 310.0f);
                pCol.Add(pGameObj);

                this.Add(pCol);
            }

            GameObjectMan.Attach(this);
        }

        public void ResetSpeed()
        {
            this.delta = 7.0f;
            this.orgTime -= 0.01f;

            TimeEvent pMoveGrid = TimerMan.Find(TimeEvent.Name.MoveGrid);
            pMoveGrid.deltaTime = orgTime;

            TimeEvent pSwapAliens = TimerMan.Find(TimeEvent.Name.SwapAliens);
            pSwapAliens.deltaTime = orgTime;

            TimeEvent pGridSoundTempo = TimerMan.Find(TimeEvent.Name.GridSoundTempo);
            pGridSoundTempo.deltaTime = orgTime;
        }

        // Data: ---------------
        private float delta;
        private bool isDropping;
        private readonly Random pRandom;
        private float orgTime;
    }
}
