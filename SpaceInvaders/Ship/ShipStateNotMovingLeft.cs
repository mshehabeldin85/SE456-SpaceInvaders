using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipStateNotMovingLeft : ShipState
    {
        public override void Handle(Ship pShip)
        {
            pShip.SetState(ShipMan.State.Ready);
        }

        public override void MoveRight(Ship pShip)
        {
            pShip.x += pShip.shipSpeed;
            this.Handle(pShip);
        }

        public override void MoveLeft(Ship pShip)
        {
            
        }

        public override void ShootMissile(Ship pShip)
        {
            Missile pMissile = ShipMan.ActivateMissile();

            pMissile.SetPos(pShip.x, pShip.y + 20);
            pMissile.SetActive(true);

            // switch states
            this.Handle(pShip);
        }

    }
}
