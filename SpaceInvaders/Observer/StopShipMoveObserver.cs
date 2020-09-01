using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class StopShipMoveObserver : ColObserver
    {
        public StopShipMoveObserver()
        {
        }

        public override void Notify()
        {
            WallCategory pWall = (WallCategory)this.pSubject.pObjB;
            //Debug.WriteLine("Ship hits " + pWall.name);

            Ship pShip = ShipMan.GetShip();

            if (pWall.name == GameObject.Name.WallRight)
            {
                pShip.SetState(ShipMan.State.NotMovingRight);
            }
            else if (pWall.name == GameObject.Name.WallLeft)
            {
                pShip.SetState(ShipMan.State.NotMovingLeft);
            }
        }
    }
}
