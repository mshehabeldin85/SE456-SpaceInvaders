using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipMan
    {
        public enum State
        {
            Ready,
            MissileFlying,
            End,

            NotMovingRight,
            NotMovingLeft
        }

        public ShipMan()
        {
            // Store the states
            this.pStateReady = new ShipStateReady();
            this.pStateMissileFlying = new ShipStateMissileFlying();
            this.pStateEnd = new ShipStateEnd();
            this.pStateNotMovingRight = new ShipStateNotMovingRight();
            this.pStateNotMovingLeft = new ShipStateNotMovingLeft();

            // set active
            this.pShip = null;
            this.pMissile = null;
        }

        public static void Create()
        {
            // make sure its the first time
            //Debug.Assert(instance == null);

            // Do the initialization
            if (instance == null)
            {
                instance = new ShipMan();
            }

            Debug.Assert(instance != null);

            // Stuff to initialize after the instance was created
            instance.pShip = ActivateShip();
            instance.pShip.SetState(ShipMan.State.Ready);

        }

        public static void SetActive(ShipMan pShipMan)
        {
            Debug.Assert(pShipMan != null);
            ShipMan.instance = pShipMan;
        }

        private static ShipMan PrivInstance()
        {
            Debug.Assert(instance != null);

            return instance;
        }

        public static Ship GetShip()
        {
            //ShipMan pShipMan = ShipMan.PrivInstance();
            ShipMan pShipMan = ShipMan.instance;

            Debug.Assert(pShipMan != null);
            Debug.Assert(pShipMan.pShip != null);

            return pShipMan.pShip;
        }

        public static ShipState GetState(State state)
        {
            //ShipMan pShipMan = ShipMan.PrivInstance();
            ShipMan pShipMan = ShipMan.instance;
            Debug.Assert(pShipMan != null);

            ShipState pShipState = null;

            switch (state)
            {
                case ShipMan.State.Ready:
                    pShipState = pShipMan.pStateReady;
                    break;

                case ShipMan.State.MissileFlying:
                    pShipState = pShipMan.pStateMissileFlying;
                    break;

                case ShipMan.State.End:
                    pShipState = pShipMan.pStateEnd;
                    break;

                case ShipMan.State.NotMovingRight:
                    pShipState = pShipMan.pStateNotMovingRight;
                    break;

                case ShipMan.State.NotMovingLeft:
                    pShipState = pShipMan.pStateNotMovingLeft;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            return pShipState;
        }

        public static Missile GetMissile()
        {
            //ShipMan pShipMan = ShipMan.PrivInstance();
            ShipMan pShipMan = ShipMan.instance;

            Debug.Assert(pShipMan != null);
            Debug.Assert(pShipMan.pMissile != null);

            return pShipMan.pMissile;
        }

        public static Missile ActivateMissile()
        {
            //ShipMan pShipMan = ShipMan.PrivInstance();
            ShipMan pShipMan = ShipMan.instance;
            Debug.Assert(pShipMan != null);

            // copy over safe copy
            Missile pMissile = new Missile(GameObject.Name.Missile, GameSprite.Name.Missile, 400, 100);
            pShipMan.pMissile = pMissile;

            // Attached to SpriteBatches
            SpriteBatch pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);

            pMissile.ActivateCollisionSprite(pSB_Boxes);
            pMissile.ActivateGameSprite(pSB_Aliens);

            // Attach the missile to the missile root
            GameObject pMissileGroup = GameObjectMan.Find(GameObject.Name.MissileGroup);
            Debug.Assert(pMissileGroup != null);

            // Add to GameObject Tree - {update and collisions}
            pMissileGroup.Add(pShipMan.pMissile);

            return pShipMan.pMissile;
        }


        public static Ship ActivateShip()
        {
            //ShipMan pShipMan = ShipMan.PrivInstance();
            ShipMan pShipMan = ShipMan.instance;
            Debug.Assert(pShipMan != null);

            // copy over safe copy
            Ship pShip = new Ship(GameObject.Name.Ship, GameSprite.Name.Ship, 100, 100);
            pShipMan.pShip = pShip;

            // Attach the sprite to the correct sprite batch
            SpriteBatch pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            pSB_Aliens.Attach(pShip.pProxySprite);

            SpriteBatch pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            pShip.ActivateCollisionSprite(pSB_Boxes);

            // Attach the missile to the missile root
            GameObject pShipRoot = GameObjectMan.Find(GameObject.Name.ShipRoot);
            Debug.Assert(pShipRoot != null);

            // Add to GameObject Tree - {update and collisions}
            pShipRoot.Add(pShipMan.pShip);

            return pShipMan.pShip;
        }

        // Data: ----------------------------------------------
        private static ShipMan instance = null;

        // Active
        public Ship pShip;
        private Missile pMissile;

        // Reference
        private ShipStateReady pStateReady;
        private ShipStateMissileFlying pStateMissileFlying;
        private ShipStateEnd pStateEnd;
        private ShipStateNotMovingRight pStateNotMovingRight;
        private ShipStateNotMovingLeft pStateNotMovingLeft;
    }
}
