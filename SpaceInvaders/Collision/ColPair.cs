using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ColPair : DLink
    {
        public enum Name
        {
            Alien_Wall,
            Ship_Wall,

            Bomb_Wall,
            Bomb_Shield,
            Bomb_Ship,

            Missile_Alien,
            Missile_UFO,
            Misslie_Shield,
            Missile_Wall,
            Missile_Bomb,

            NullObject,
            Not_Initialized
        }

        public ColPair()
            : base()
        {
            this.treeA = null;
            this.treeB = null;
            this.name = ColPair.Name.Not_Initialized;

            this.poSubject = new ColSubject();
            Debug.Assert(this.poSubject != null);
        }

        public void Set(ColPair.Name colpairName, GameObject pTreeRootA, GameObject pTreeRootB)
        {
            Debug.Assert(pTreeRootA != null);
            Debug.Assert(pTreeRootB != null);

            this.treeA = pTreeRootA;
            this.treeB = pTreeRootB;
            this.name = colpairName;
        }
        public void Wash()
        {
            this.treeA = null;
            this.treeB = null;
            this.name = ColPair.Name.Not_Initialized;

        }

        public ColPair.Name GetName()
        {
            return this.name;
        }

        public void Process()
        {
            Collide(this.treeA, this.treeB);
        }

        static public void Collide(GameObject pSafeTreeA, GameObject pSafeTreeB)
        {
            // A vs B
            GameObject pNodeA = pSafeTreeA;
            GameObject pNodeB = pSafeTreeB;

            while (pNodeA != null)
            {
                // Restart compare
                pNodeB = pSafeTreeB;

                while (pNodeB != null)
                {
                    // Get rectangles
                    ColRect rectA = pNodeA.poColObj.poColRect;
                    ColRect rectB = pNodeB.poColObj.poColRect;

                    // test them
                    if (ColRect.Intersect(rectA, rectB))
                    {
                        // Boom - it works (Visitor in Action)
                        pNodeA.Accept(pNodeB);
                        break;
                    }

                    pNodeB = (GameObject)Iterator.GetSibling(pNodeB);
                }

                pNodeA = (GameObject)Iterator.GetSibling(pNodeA);
            }
        }

        public void SetName(ColPair.Name inName)
        {
            this.name = inName;
        }

        public void Attach(ColObserver observer)
        {
            this.poSubject.Attach(observer);
        }

        public void NotifyListeners()
        {
            this.poSubject.Notify();
        }

        public void SetCollision(GameObject pObjA, GameObject pObjB)
        {
            Debug.Assert(pObjA != null);
            Debug.Assert(pObjB != null);

            // GameObject pAlien = AlienCategory.GetAlien(objA, objB);
            this.poSubject.pObjA = pObjA;
            this.poSubject.pObjB = pObjB;
        }

        public void Dump()
        {
            // TO DO ...
        }

        // Data: ---------------
        public ColPair.Name name;
        public GameObject treeA;
        public GameObject treeB;
        public ColSubject poSubject;
    }
}